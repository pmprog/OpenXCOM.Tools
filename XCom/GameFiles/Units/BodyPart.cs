using System;

namespace XCom
{
	public class BodyPart
	{
		private PartDirection notMoving;
		private MovingPartDirection moving;

		public BodyPart(int[] stationary,int[,]moving)
		{	
			this.notMoving = new PartDirection(stationary);			
			this.moving = new MovingPartDirection(moving);
		}

		public BodyPart(int[] stationary)
		{
			this.notMoving=new PartDirection(stationary);
			moving=null;
		}

		public PartDirection Stationary
		{
			get{return notMoving;}
		}

		public MovingPartDirection Moving
		{
			get{return moving;}
		}
	}
}
