using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickFactory {

	private static readonly Dictionary<Brick.TypeBrick, Brick> bricks
		= new Dictionary<Brick.TypeBrick, Brick>
	{
		{ Brick.TypeBrick.NONE, new Brick() },
		{ Brick.TypeBrick.NORMAL, new NormalBrick() },
		{ Brick.TypeBrick.XRES, new XResBrick() },
		{ Brick.TypeBrick.XXRES, new XXResBrick() },
		{ Brick.TypeBrick.INVERSE, new InverseBrick() },
		{ Brick.TypeBrick.MULTIPLE_BALLS, new MultipleBallsBrick() },
		{ Brick.TypeBrick.SLOW_BALLS, new SlowBallsBrick() },
		{ Brick.TypeBrick.FAST_BALLS, new FastBallsBrick() },
	};

	public static Brick getBrick(Brick.TypeBrick type) {
		Brick brick = bricks[type];
		return brick.Clone();
	}
}
