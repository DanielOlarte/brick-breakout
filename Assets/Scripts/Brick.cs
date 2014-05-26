using UnityEngine;
using System.Collections;

public class Brick {
	public enum TypeBrick {NONE, NORMAL, XRES, XXRES};
	public TypeBrick typeBrick;

	public int neededHits;
	public int currentHits;

	public Brick() {
		typeBrick = TypeBrick.NONE;
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
