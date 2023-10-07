using Godot;
using System;

public partial class Pole : CharacterBody3D
{
	private bool isCollided = false;
	private float errorThresh = 0.01F;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//BodyShapeEntered += OnBodyEntered;
		//BodyShapeExited += OnBodyExited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		LookAtCursor();
		if (isCollided && Mathf.Abs(GlobalPosition.Y - GetParentNode3D().GlobalPosition.Y) > errorThresh)
			GetParentNode3D().GlobalPosition = GlobalPosition;
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

	void OnBodyEntered(Rid r, Node n, long one, long two)
	{
		isCollided = true;
	}

	void OnBodyExited(Rid r, Node n, long one, long two)
	{
		isCollided = false;
	}
}
