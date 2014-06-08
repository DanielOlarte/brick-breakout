﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brick {
	public enum TypeBrick {NONE, NORMAL, XRES, XXRES, INVERSE, MULTIPLE_BALLS, SLOW_BALLS, FAST_BALLS};
	public enum ObjectBrick {
		[StringValue("")] NONE = 0,
		[StringValue("InversePaddleObj")] INVERSE = 1,
		[StringValue("MultipleBallsObj")] MULTIPLE_BALLS = 2,
		[StringValue("SlowBallsObj")] SLOW_BALLS = 3,
		[StringValue("FastBallsObj")] FAST_BALLS = 4,
	};

	public TypeBrick typeBrick;
	public ObjectBrick objectBrick;

	protected int neededHits;
	protected int currentHits;

	public Brick() {
		typeBrick = TypeBrick.NONE;
		objectBrick = ObjectBrick.NONE;
		neededHits = 0;
		currentHits = 0;
	}

	public Brick Clone()
	{
		return (Brick)MemberwiseClone();
	}

	public TypeBrick getType() {
		return typeBrick;
	}

	public ObjectBrick getObject() {
		return objectBrick;
	}

	public int getNeededHits() {
		return neededHits;
	}

	public int getCurrentHits() {
		return currentHits;
	}

	public virtual void addHits() {
		currentHits += 1;
	}
}
