extends CollisionShape3D

@export var text: String

func _on_area_3d_body_entered(body):
	if body.is_in_group("Player"):
		UiGlobal.UpdatePopupLabel.emit(text)
		queue_free()
