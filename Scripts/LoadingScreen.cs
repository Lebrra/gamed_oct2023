using Godot;
using System;

public partial class LoadingScreen : Control
{

	private string path;
	private bool loading;
	[Export] ProgressBar progressBar;
	[Export] Label loadingLabel;
	[Export] Label pressAnyKeyLabel;
	[Export] bool waitForInputOnLoad;
	[Export] string[] tips;
	[Export] Label tipsLabel;

	private bool waitingForInput;
	private bool inputKeyPressed;
	private float visualLoadScaler = 0f;

	public static LoadingScreen instance;
	public override void _Ready() {
		if (instance != null) {
			instance.QueueFree();
		}
		
		instance = this;
	}

	public override void _Process(double delta){

		if (loading) {
			var progress = new Godot.Collections.Array();
			var status = ResourceLoader.LoadThreadedGetStatus(path, progress);
			visualLoadScaler = Mathf.Min(1f, visualLoadScaler + (float)delta);

			if (status == ResourceLoader.ThreadLoadStatus.InProgress || visualLoadScaler < 1f)
			{
				progressBar.Value = (double)progress[0] * 10 * visualLoadScaler + 90 * visualLoadScaler;
				//GD.Print((int)(visualLoadScaler * 100) + "%, " + progress[0]);
			}
			else if (status == ResourceLoader.ThreadLoadStatus.InvalidResource)
			{
				ResourceLoader.LoadThreadedRequest(path);
			}
			else if (status == ResourceLoader.ThreadLoadStatus.Loaded && visualLoadScaler >= .99f)
			{
				progressBar.Value = 100;
				SetProcess(false);

				if (waitForInputOnLoad)
				{
					waitingForInput = true;
					loadingLabel.Text = "Loaded!";
					pressAnyKeyLabel.Show();
				}
				else
				{
					ChangeScene(ResourceLoader.LoadThreadedGet(path) as PackedScene);
				}
			}
			
		}
	}

	public override void _Input(InputEvent @event) {
		base._Input(@event);
		if (waitingForInput) {
			if (@event is InputEventKey) {
				InputEventKey key = (InputEventKey)@event;
				if (inputKeyPressed) {
					ChangeScene(ResourceLoader.LoadThreadedGet(path) as PackedScene);
				}
				if (key.Pressed)
				{
					inputKeyPressed = true;
				}
				else {
					inputKeyPressed = false;
				}
			}
		}
	}

	public void ChangeScene(PackedScene resource) {
		GetTree().Paused = false;
		var rootNode = GetTree().Root;
		foreach (var item in GetTree().Root.GetChildren()) {
			if (item is Node3D || item is Node2D || item is Control) { //Dont delete any autoload scripts
				GetTree().Root.RemoveChild(item);
				item.QueueFree();

			}
			
		}
		Node currentNode = resource.Instantiate();
		rootNode.AddChild(currentNode);

		
		QueueFree();
	}

	public void LoadLevel(string path) {
		this.path = path;
		Show();

		if (tips.Length != 0) {
			tipsLabel.Text = tips[GD.Randi() % tips.Length];
		}

		if (ResourceLoader.HasCached(path)){
			ResourceLoader.LoadThreadedGet(path);
			loading = true;
			visualLoadScaler = 0f;
		}
		else {
			ResourceLoader.LoadThreadedRequest(path);
			loading = true;
			visualLoadScaler = 0f;

		}
	}

}
