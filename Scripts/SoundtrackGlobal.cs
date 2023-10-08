using Godot;
using System;

public partial class SoundtrackGlobal : AudioStreamPlayer
{
	/*

	public static SoundtrackGlobal instance;


	bool rootSet;
	float timer = 0f;

	public override void _Ready()
	{
		if (instance != null && instance != this) {
			instance.QueueFree();
		}
		instance = this;

	}

	public override void _PhysicsProcess(double delta) {

		if (timer >= .1f && rootSet == false) {
			GetParent().RemoveChild(this);
			GetTree().Root.AddChild(this);
			rootSet = true;
		}
		timer += (float)delta;

	}

	*/

}
