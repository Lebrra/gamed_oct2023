using Godot;
using System;

public partial class PowerBar : Node
{

	[Export] ProgressBar bar;
	[Export] Sprite3D sprite;
	[Export] SubViewport viewPort;
	public override void _Ready(){
		sprite.Texture = viewPort.GetTexture(); ;
	}

	
	public override void _Process(double delta){

	}

	public void UpdatePower(float amt) {
		if (amt > 0){
			sprite.Show();
			bar.Value = amt;
		}
		else {
			sprite.Hide();
			bar.Value = 0;
		}
	}

}
