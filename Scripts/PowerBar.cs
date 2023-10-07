using Godot;
using System;

public partial class PowerBar : Node
{

	[Export] ProgressBar bar;
	[Export] Sprite3D sprite;
	public override void _Ready(){
		UiGlobal.UpdatePowerUI += UpdatePower;
	}

	
	public override void _Process(double delta){

	}

	void UpdatePower(float amt) {
		if (amt > 0){
			sprite.Show();
			bar.value = amt;
		}
		else {
			sprite.Hide();
		}
	}

}
