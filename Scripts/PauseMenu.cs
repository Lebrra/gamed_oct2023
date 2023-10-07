using Godot;
using System;

public partial class PauseMenu : Control
{

	[Export] Key pauseKey;
	public bool paused { private set; get; }

	/*
	public static PauseMenu instance;
	public override void _Ready(){
		if (instance != null){
			instance.QueueFree();
			instance = this;
		}
		else {
			instance = this;
		}
	}
	*/

	public override void _Input(InputEvent @event) {
		base._Input(@event);
		if (@event is InputEventKey eventKey) {
			if (eventKey.Pressed && eventKey.Keycode == pauseKey) {
				TogglePause();
			}
		}
	}

	void _on_resume_button_down() {
		UnPause();
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
