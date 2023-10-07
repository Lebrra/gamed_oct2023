extends RigidBody3D

var fired = false
@export var Arrow: Node3D
@export var power: float = 5
@export var powerBuildUpSpeed = 25
@export var maxPower = 100

func _physics_process(delta):
	if Input.is_action_pressed("Fire"):
		power += delta * powerBuildUpSpeed
	if Input.is_action_just_released("Fire"):
		fired = true
		var direction = position.direction_to(Arrow.global_position)
		linear_velocity = power * direction
		power = 0
		
