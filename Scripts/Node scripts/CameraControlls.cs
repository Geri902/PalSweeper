using Godot;
using System;

public partial class CameraControlls : Camera3D
{
	int mapWidth;
	int mapHeight;
	Vector3 centerPoint;
	bool propsSet = false;
	[Export]
	int angleInDeg = 30;
	[Export]
	float offset = 0;

	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
		
	}

	public void SetProperties(int mapWidth, int mapHeight, Vector3 centerPoint)
	{
		this.mapWidth = mapHeight;
		this.mapHeight = mapHeight;
		this.centerPoint = centerPoint;

		float a;
		float centerPivot;
		if (mapHeight > mapWidth)
		{
			a = mapHeight;
			centerPivot = centerPoint.Z;
		}
		else
		{
			a = mapWidth;
			centerPivot = centerPoint.X;
		}

		float rad = Mathf.DegToRad(angleInDeg);

		//trust me bro, I have the schematics, I have done the math on paper ... it works
		float c = (a / 2) / Mathf.Cos(rad);
		GD.Print(c);
		float r = c + offset;
		GD.Print(r);
		float p = Mathf.Cos(rad) * r;
		GD.Print(p);
		float k = centerPivot + p;
		GD.Print(k);

		float h = Mathf.Sqrt((Mathf.Pow(r,2) - Mathf.Pow(p, 2)));
		GD.Print(h);

		if (mapHeight > mapWidth)
		{
			Position = new Vector3(k, centerPoint.Y + h, mapHeight / 2);
		}
		else
		{
			Position = new Vector3(mapWidth / 2, centerPoint.Y + h, k);
		}

		RotationDegrees = new Vector3(x: angleInDeg * -1, y: 0, z: 0);

		propsSet = true;
	}
}
