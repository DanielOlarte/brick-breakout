using System.Collections;

public class InverseBrick : Brick {
	
	public InverseBrick() {
		typeBrick = TypeBrick.INVERSE;
		objectBrick = ObjectBrick.INVERSE;
		currentHits = 0;
		neededHits = 2;
		points = 150;
	}
}
