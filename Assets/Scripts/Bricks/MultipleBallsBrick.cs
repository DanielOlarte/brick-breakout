using System.Collections;

public class MultipleBallsBrick : Brick {
	
	public MultipleBallsBrick() {
		typeBrick = TypeBrick.MULTIPLE_BALLS;
		objectBrick = ObjectBrick.MULTIPLE_BALLS;
		currentHits = 0;
		neededHits = 2;
		points = 150;
	}
}