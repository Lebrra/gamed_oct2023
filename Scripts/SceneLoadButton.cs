using Godot;
using System;

public partial class SceneLoadButton : Button
{

	[Export] Resource levelPath;
	[Export] Control[] controlsToHide;

	public override void _Ready(){
		this.Pressed += ButtonPressed;
	}


	public override void _Process(double delta)
	{
	}

	void ButtonPressed() {
		if (controlsToHide.Length > 0) {
			foreach (Control c in controlsToHide) {
				if (c != null) {
					c.Hide();
				}
				
			}
		}
		GD.Print("Button pressed");
		LoadingScreen.instance.LoadLevel(levelPath.ResourcePath);
	}
}
