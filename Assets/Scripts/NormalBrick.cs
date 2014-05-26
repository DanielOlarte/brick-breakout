using System.Collections;

public class NormalBrick : Brick {

	public NormalBrick() {
		typeBrick = TypeBrick.NORMAL;
		currentHits = 0;
		neededHits = 1;
	}
}
