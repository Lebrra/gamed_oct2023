using Godot;
using System;

public partial class ArrowPowerup : Area3D
{
	[Export]
	private float spinSpeed = 2F;
	[Export]
	private bool invert = false;
	[Export]
	private float force = 5F;
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		RotateZ(invert ? (float)delta * spinSpeed : (float)delta * spinSpeed * -1F);
	}

	void OnBodyEntered(Node3D node)
	{
		if (node.IsInGroup("Player") && node is RigidBody3D rb)
		{
			var direction = new Vector3(Mathf.Cos(Rotation.Z), Mathf.Sin(Rotation.Z), 0F);
			GD.Print(direction);
			rb.ApplyForce(direction * force);

			BodyEntered -= OnBodyEntered;
			QueueFree();
		}
	}
}
