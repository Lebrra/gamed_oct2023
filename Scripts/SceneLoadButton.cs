using Godot;
using System;

public partial class SceneLoadButton : Control
{

	[Export] string levelPath;
	[Export] Control[] controlsToHide;
	[Export] LoadingScreen loadingScreen;

	public override void _Ready(){
	}


	public override void _Process(double delta)
	{
	}

	void _on_button_down() {
		if (controlsToHide.Length > 0) {
			foreach (Control c in controlsToHide) {
				c.Hide();
			}
		}
		
		loadingScreen.LoadLevel(levelPath);
	}
}
