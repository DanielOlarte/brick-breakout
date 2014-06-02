using UnityEngine;
using System.Collections;

public class NonDestroyableBrick : Brick {
	
	public NonDestroyableBrick() {
		typeBrick = TypeBrick.NONE;
		objectBrick = ObjectBrick.NONE;
		currentHits = 0;
		neededHits = -1;
	}

	public override void addHits() {
	}
}
