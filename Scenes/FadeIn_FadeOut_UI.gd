extends Label

@export var speedOfFade: float
# Called when the node enters the scene tree for the first time.
func _ready():
	UiGlobal.UpdatePopupLabel.connect(StartFade)
	StartFade(text)
	
func StartFade(newText: String)-> void:
	text = newText
	var tween = get_tree().create_tween()
	tween = create_tween()
	
	tween.parallel().tween_property(self, "theme_override_colors/font_color", Color.WHITE, speedOfFade)
	tween.parallel().tween_property(self, "theme_override_colors/font_outline_color", Color.BLACK, speedOfFade)
	tween.chain().tween_property(self, "theme_override_colors/font_color", Color(1,1,1,0), speedOfFade)
	tween.parallel().tween_property(self, "theme_override_colors/font_outline_color", Color(0,0,0,0), speedOfFade)
	
