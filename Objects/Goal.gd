extends StaticBody3D

func _on_area_3d_body_entered(body: RigidBody3D):
	print("Entered")
	if body.is_in_group("Player"):
		print("Win")
		body.position = global_position
		body.freeze = true
		GameManager.LevelEnd.emit()
