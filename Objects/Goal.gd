extends StaticBody3D

func _on_area_3d_body_entered(body):
	print("Entered")
	if body.get_tree().has_group("Player"):
		print("Win")
