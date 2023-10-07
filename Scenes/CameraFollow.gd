extends Camera3D

@export var player: NodePath
@export var speedOfCameraX: float
@export var speedOfCameraY: float
@export var lookAheadAmountX: float
@export var lookAheadAmountY: float
var playerNode
func _ready():
	playerNode = get_node(player)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	var v = playerNode.velocity
	v.x = clampf(v.x, -1, 1)
	v.y = clampf(v.y, -1, 1)
	var direction = Vector3(v.x * lookAheadAmountX, v.y * lookAheadAmountY, v.z)
	global_transform.origin.x = lerp(global_transform.origin.x, playerNode.global_transform.origin.x + direction.x, speedOfCameraX * delta)
	global_transform.origin.y = lerp(global_transform.origin.y, playerNode.global_transform.origin.y + direction.y, speedOfCameraY * delta)
	if v.y < 0:
		global_transform.origin.y = lerp(global_transform.origin.y, playerNode.global_transform.origin.y, speedOfCameraY * 10 * delta)
