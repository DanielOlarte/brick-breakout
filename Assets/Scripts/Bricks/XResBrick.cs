using System.Collections;

public class XResBrick : Brick {
	
	public XResBrick() {
		typeBrick = TypeBrick.XRES;
		objectBrick = ObjectBrick.NONE;
		currentHits = 0;
		neededHits = 2;
	}
}