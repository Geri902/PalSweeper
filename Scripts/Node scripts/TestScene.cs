using Godot;
using System;

public partial class TestScene : Node3D
{
	[Export]
	Button generateButton;
	[Export]
	LineEdit widthLine;
	[Export]
	LineEdit heightLine;
	[Export]
	LineEdit mineLine;
	[Export]
	PackedScene GroundScene;
	[Export]
	CanvasLayer Ui;
	[Export]
	Node GroundCollection;
	[Export]
	CameraControlls camera;
	BasicGameMaster game = null;
	GroundPrefab[,] groundMap;
	int width;
	int height;
	public override void _Ready()
	{
		generateButton.ButtonDown += generateGame;
	}

	public override void _Process(double delta)
	{
		
	}

	private void generateGame()
	{
		width = int.Parse(widthLine.Text);
		height = int.Parse(heightLine.Text);
		int mines = int.Parse(mineLine.Text);

		groundMap = new GroundPrefab[width,height];

		if (game is null)
		{
			game = new BasicGameMaster(width, height, mines);
		}
		else
		{
			game.RegenerateMap(width, height, mines);
		}

		game.PrintMap();

		Ui.Visible = false;

		GenerateGrid();
		SetCamera();
	}

	private void GenerateGrid()
	{
		(int width, int height) = game.GetSize();

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				GroundPrefab ground = GroundScene.Instantiate<GroundPrefab>();
				ground.Position = new Vector3(x*2, 0, y*2);
				groundMap[x,y] = ground;
				GroundCollection.AddChild(ground);
			}
		}

	}

	private void SetCamera()
	{
		Vector3 center = new Vector3(x: width, y: 0, z: height);
		camera.SetProperties(width * 2, height * 2, center);
	}
}
