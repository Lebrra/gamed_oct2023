using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	public static Action<bool> OnPoleCollision;
	private bool poleColliding = false;
	
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready()
	{
		base._Ready();
		OnPoleCollision += PoleCollision;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (poleColliding)
		{
			MoveAndSlide();
			return;
		}
		
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	void PoleCollision(bool isColliding)
	{
		poleColliding = isColliding;
		GD.Print(isColliding);
	}

	public void SetVelocity(Vector3 vel)
	{
		Velocity = vel;
	}
}
