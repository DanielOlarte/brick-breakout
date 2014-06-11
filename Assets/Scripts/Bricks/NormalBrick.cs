using System.Collections;

public class NormalBrick : Brick {

	public NormalBrick() {
		typeBrick = TypeBrick.NORMAL;
		objectBrick = ObjectBrick.NONE;
		currentHits = 0;
		neededHits = 1;
		points = 100;
	}
}
