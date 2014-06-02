using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brick {
	public enum TypeBrick {NONE, NORMAL, XRES, XXRES, INVERSE};
	public enum ObjectBrick {
		[StringValue("")] NONE = 0,
		[StringValue("InversePaddleObj")] INVERSE = 1,
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

	public void addHits() {
		currentHits += 1;
	}
}
