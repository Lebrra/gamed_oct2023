using Godot;
using System;

public partial class LoadingScreen : Control
{

	private string path;
	private bool loading;
	[Export] ProgressBar progressBar;
	[Export] bool waitForInputOnLoad;
	private bool waitingForInput;
	private bool inputKeyPressed;
	public override void _Ready(){

	}

	public override void _Process(double delta){

		if (loading) {
			var progress = new Godot.Collections.Array();
			var status = ResourceLoader.LoadThreadedGetStatus(path, progress);
			if (status == ResourceLoader.ThreadLoadStatus.InProgress){
				progressBar.Value = (double)progress[0] * 100;
			}
			else if (status == ResourceLoader.ThreadLoadStatus.Loaded) {
				progressBar.Value = 100;
				SetProcess(false);

				if (waitForInputOnLoad){
					waitingForInput = true;
					//pressAnyButtonLabel.UnHide();
				}
				else {
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
		if (ResourceLoader.HasCached(path)){
			ResourceLoader.LoadThreadedGet(path);
		}
		else {
			ResourceLoader.LoadThreadedRequest(path);
			loading = true;

		}
	}

}
