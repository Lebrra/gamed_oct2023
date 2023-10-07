using Godot;
using System;

public partial class ModularButton : Button
{
	[Export] 
	private IButton action;

	[Export] 
	private Button pauseButton;

	public override void _Ready()
	{
		//base._Process(delta);

		ButtonDown += action.DoThing;
		pauseButton.ButtonDown += Pause;
	}

	void Pause()
	{
		
	}
}

interface IButton
{
	public void DoThing();
}
