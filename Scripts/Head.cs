using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Head : StaticBody3D
{
	[Export] 
	private Area3D hatZone;
	
	private Node3D player = null;
	private const float halfPi = Mathf.Pi / 2F;
	private const float twoPi = Mathf.Pi * 2F;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		hatZone.AreaEntered += OnAreaEnter;
		hatZone.AreaExited += OnAreaExit;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (player != null)
		{
			var simpleZ = GetSimplestRadian(player.Rotation.Z);
			if (simpleZ is < halfPi and > -halfPi)
			{
				GD.Print("WIN");
				player = null;
			}
		}
	}

	float GetSimplestRadian(float radian)
	{
		while (radian > twoPi) radian -= twoPi;
		while (radian < 0F) radian += twoPi;
		return radian;
	}
	
	void OnAreaEnter(Node3D node)
	{
		if (node.GetGroups().Contains("Player")) player = node;
		}

	void OnAreaExit(Node3D node)
	{
		player = null;
	}
	
}