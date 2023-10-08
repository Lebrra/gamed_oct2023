using Godot;
using System;

public partial class PauseMenu : Control
{

	[Export] Key pauseKey;
	[Export] Button resumeButton;
	public bool paused { private set; get; }


	public override void _Ready(){
		resumeButton.Pressed += UnPause;
	}
	

	public override void _Input(InputEvent @event) {
		base._Input(@event);
		if (@event is InputEventKey eventKey) {
			if (eventKey.Pressed && eventKey.Keycode == pauseKey) {
				TogglePause();
			}
		}
	}

	public void TogglePause() {
		if (!paused){
			Pause();
		}
		else {
			UnPause();
		}
	}

	void Pause() {
		paused = true;
		GetTree().Paused = true;
		this.Show();
	}

	void UnPause() {
		paused = false;
		GetTree().Paused = false;
		this.Hide();
	}



}
