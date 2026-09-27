using Godot;
using System;

public partial class GroundPrefab : Node3D
{
	[Export]
	AnimationPlayer AnimPlayer;
	[Export]
	Label NumberLabel;
	[Export]
	Area3D PlayerHitbox;
	[Export]
	Sprite3D InsideSprite;
	public override void _Ready()
	{
	}

	
	public override void _Process(double delta)
	{
	}
}
