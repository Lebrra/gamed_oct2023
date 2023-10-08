extends Label
var score: int
func _ready():
	GameManager.StrokeIncrement.connect(UpdateText)
func UpdateText(amt: int):
	score += amt
	text = "Strokes: " + str(score)
