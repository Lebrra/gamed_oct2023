using Godot;
using System;

public partial class Pole : Node3D
{
	[Export] 
	public PlayerMovement playerMov;
	[Export] 
	public Area3D poleColl;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		poleColl.BodyEntered += OnAreaEnter;
		poleColl.BodyExited += OnAreaExit;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		LookAtCursor();
	}

	void LookAtCursor()
	{
		var cam = GetViewport().GetCamera3D();
		var mousePos = GetViewport().GetMousePosition();
		var from = cam.ProjectRayOrigin(mousePos);
		var to = from + cam.ProjectRayNormal(mousePos) * 1000;
		var cursor = new Plane(Vector3.Back, GlobalTransform.Origin.Y).IntersectsRay(from, to);

		if (cursor != null)
		{
			var dist = GlobalPosition.DirectionTo(cursor.Value);
			var angle = Mathf.Atan2(dist.Y, dist.X);
			Rotation = new Vector3(0, 0, angle);
			
		}
	}
	
	void OnAreaEnter(Node node)
	{
		PlayerMovement.OnPoleCollision(true);
	}
	
	void OnAreaExit(Node node)
	{
		PlayerMovement.OnPoleCollision(false);
		
		Vector3 force = Vector3.Up * 5F;
		var charVel = force.Rotated(Vector3.Right, Rotation.Z);
		playerMov.SetVelocity(charVel);
		//GD.Print(Rotation.Z);
		//GD.Print(charVel);
	}
}
