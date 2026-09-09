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
	BasicGameMaster game = null;
	public override void _Ready()
	{
		generateButton.ButtonDown += generateGame;
	}
	public override void _Process(double delta)
	{
	}

	private void generateGame()
	{
		int width = int.Parse(widthLine.Text);
		int height = int.Parse(heightLine.Text);
		int mines = int.Parse(mineLine.Text);


		if (game is null)
		{
			game = new BasicGameMaster(width, height, mines);
		}
		else
		{
			game.RegenerateMap(width, height, mines);
		}

		game.PrintMap();
	}
}
