extends Camera3D

@export var player: NodePath
@export var speedOfCameraX: float
@export var speedOfCameraY: float
@export var lookAheadAmountX: float
@export var lookAheadAmountY: float
var levelEnd: bool = false
var animPlaying: bool = false
@export var playerNode: Node3D
@export var zoomToNode: Node3D
var currentNode
func _ready():
	#playerNode = get_node(player)
	currentNode = playerNode
	GameManager.LevelEnd.connect(LevelEnd)
	
func LevelEnd():
	levelEnd = true
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	cameraFollow(delta)
	setCameraFollowNode()
	
func cameraFollow(delta):
	if !levelEnd:
		var v
		if(currentNode is RigidBody3D):
			v = currentNode.linear_velocity
		else:
			v = Vector3.ZERO
			
		v.x = clampf(v.x, -1, 1)
		v.y = clampf(v.y, -1, 1)
		var direction = Vector3(v.x * lookAheadAmountX, v.y * lookAheadAmountY, v.z)
		global_transform.origin.x = lerp(global_transform.origin.x, currentNode.global_transform.origin.x + direction.x, speedOfCameraX * delta)
		global_transform.origin.y = lerp(global_transform.origin.y, currentNode.global_transform.origin.y + direction.y, speedOfCameraY * delta)
		if v.y < 0:
			global_transform.origin.y = lerp(global_transform.origin.y, currentNode.global_transform.origin.y, speedOfCameraY * 10 * delta)
	else:
		
		global_transform.origin.y = lerp(global_transform.origin.y, currentNode.global_transform.origin.y, speedOfCameraY * delta)
		global_transform.origin.x = lerp(global_transform.origin.x, currentNode.global_transform.origin.x, speedOfCameraX * delta)
		if !animPlaying:
			animPlaying = true
			var tween = get_tree().create_tween()
			tween = create_tween()
			tween.tween_property(self, "position", Vector3(global_position.x, global_position.y, global_position.z / 2), 2)


func setCameraFollowNode():
	if(Input.is_action_pressed("Look Ahead")):
		if(zoomToNode != null):
			currentNode = zoomToNode
	else:
		if(playerNode != null):
			currentNode = playerNode
		else:
			printerr("No reference to player node")




