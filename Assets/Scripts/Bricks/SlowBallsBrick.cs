using System.Collections;

public class SlowBallsBrick : Brick {
	
	public SlowBallsBrick() {
		typeBrick = TypeBrick.SLOW_BALLS;
		objectBrick = ObjectBrick.SLOW_BALLS;
		currentHits = 0;
		neededHits = 2;
		points = 150;
	}
}
