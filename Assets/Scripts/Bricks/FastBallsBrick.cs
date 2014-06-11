using System.Collections;

public class FastBallsBrick : Brick {
	
	public FastBallsBrick() {
		typeBrick = TypeBrick.FAST_BALLS;
		objectBrick = ObjectBrick.FAST_BALLS;
		currentHits = 0;
		neededHits = 2;
		points = 100;
	}
}
