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
		PlayerMovement.OnPoleCollision += AddPoleForce;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		LookAtCursor(delta);
	}

	void LookAtCursor(double delta)
	{
		var cam = GetViewport().GetCamera3D();
		var mousePos = GetViewport().GetMousePosition();
		var from = cam.ProjectRayOrigin(mousePos);
		var to = from + cam.ProjectRayNormal(mousePos) * 1000;
		var cursor = new Plane(Vector3.Back, GlobalTransform.Origin.Y).IntersectsRay(from, to);

		if (cursor != null)
		{
			var dist = GlobalPosition.DirectionTo(cursor.Value);
			var finalAngle = Mathf.Atan2(dist.Y, dist.X);
			Rotation = new Vector3(0F, 0F, Mathf.Lerp(Rotation.Z, finalAngle, (float)delta * 20F));
			
		}
	}

	//float ChooseClosestAngle(float angle)
	//{
	//	var oneRad = 2 * Mathf.Pi;
	//	var diff = Mathf.Abs(angle - Rotation.Z);
//
	//	var plus = angle;
	//	while (Mathf.Abs((plus + oneRad) - Rotation.Z) < diff)
	//	{
	//		plus += oneRad;
	//		GD.Print(plus);
	//	}
	//	if (plus > angle) return plus;
//
	//	var minus = angle;
	//	while (Mathf.Abs((plus + oneRad) - Rotation.Z) < diff)
	//	{
	//		minus -= oneRad;
	//		GD.Print(minus);
	//	}
	//	if (minus < angle) return minus;
//
	//	return angle;
	//}
	
	void OnAreaEnter(Node node)
	{
		playerMov.poleColliding = true;
	}
	
	void OnAreaExit(Node node)
	{
		playerMov.poleColliding = false;
	}

	void AddPoleForce()
	{
		var charVel = new Vector3(Mathf.Cos(Rotation.Z), Mathf.Sin(Rotation.Z), 0F);
		playerMov.SetVelocity(charVel * PlayerMovement.Speed);
	}
}
