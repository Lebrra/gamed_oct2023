extends Button

@export var screenToHide: Control
@export var screenToEnable: Control
@export var isQuitButton: bool
func _ready():
	button_up.connect(buttonWasPressed)
func buttonWasPressed():
	if isQuitButton:
		get_tree().quit()
		return
	screenToHide.visible = false
	screenToEnable.visible = true
	
