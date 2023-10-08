extends Control


# Called when the node enters the scene tree for the first time.
func _ready():
	GameManager.LevelEnd.connect(ShowMe)
func ShowMe():
	visible = true
