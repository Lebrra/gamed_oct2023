extends RigidBody3D

var fired = false
@export var Arrow: Node3D
var power: float = 0
@export var powerBuildUpSpeed = 100
@export var jumpMultiplier = .25
@export var anim: AnimationPlayer
@export var powerBar: Node3D

func _physics_process(delta):
	if linear_velocity.y < 0.0001 && linear_velocity.y > -0.0001:
		anim.play("Idle_001")
	else:
		anim.play("Flying")
		
	if Input.is_action_pressed("Fire"):
		power = clampf(power + delta * powerBuildUpSpeed, 0, 100)
		
	if Input.is_action_just_released("Fire"):
		fired = true
		var direction = position.direction_to(Arrow.global_position)
		linear_velocity = power * jumpMultiplier * direction
		power = 0
		anim.play("Flying")
	
	powerBar.UpdatePower(power)
	
