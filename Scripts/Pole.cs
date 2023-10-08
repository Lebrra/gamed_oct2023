using Godot;
using System;

public partial class Pole : Node3D
{
	[Export] RigidBody3D rb;

	[Export] Node3D umbrellaClosed;
	[Export] Node3D umbrellaOpen;

	Camera3D cam;
	Vector2 mousePos;
	Plane plane;

	Vector3 directionTo;

	public override void _Ready() {
		cam = GetViewport().GetCamera3D();
		plane = new Plane(Vector3.Back, Vector3.Zero);
	}

	public override void _PhysicsProcess(double delta){
		LookAtCursor();
		DragQueen(delta);
	}
	
	void LookAtCursor(){
		mousePos = GetViewport().GetMousePosition();
		var from = cam.ProjectRayOrigin(mousePos);
		var to = cam.ProjectRayNormal(mousePos) * 1000;
		var cursor = plane.IntersectsRay(from, to);

		if (cursor != null){
			directionTo = GlobalPosition.DirectionTo(cursor.Value);
			var angle = Mathf.Atan2(directionTo.Y, directionTo.X);
			Rotation = new Vector3(0, 0, angle);

		}

	}

	void DragQueen(double delta) {
		Vector3 velocity = rb.LinearVelocity;
		float dragCoefficient;

		if (velocity.AngleTo(directionTo) > Mathf.Pi/2f){
			dragCoefficient = 1f;
			if (rb.LinearVelocity.LengthSquared() > 0.025f) {
				ToggleUmbrellaModel(true);
			}
			
		}
		else {
			dragCoefficient = -.25f;
			ToggleUmbrellaModel(false);
		}

		rb.LinearVelocity *= (1 - dragCoefficient * (float)delta);
	}

	void ToggleUmbrellaModel(bool open) {
		if (open){
			if (umbrellaClosed != null) { umbrellaClosed.Hide(); }
			if (umbrellaOpen != null) { umbrellaOpen.Show(); }
		}
		else {
			if (umbrellaClosed != null) { umbrellaClosed.Show(); }
			if (umbrellaOpen != null) { umbrellaOpen.Hide(); }
		}
	}

}
