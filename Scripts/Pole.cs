using Godot;
using System;

public partial class Pole : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
}
