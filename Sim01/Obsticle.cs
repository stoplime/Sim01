using System;
using Sce.PlayStation.Core;

namespace Sim01
{
	public class Obsticle : GroundRobot
	{
		private float angularVel;
		
		public Obsticle (Vector3 initPos, float dir)
			:base(initPos,dir)
		{
			sprite.SetColor(1,1,1,1);
			angularVel = speed/(40*5);
		}
		
		public override void Update (float time)
		{
			direction += angularVel*time;
			if (!Collide) {
				Pos += new Vector3(FMath.Cos(direction+diviation)*speed*time,FMath.Sin(direction+diviation)*speed*time,0);
			}else{
				Collide = false;
			}
			
			sprite.Position = Pos;
			sprite.Rotation = direction;
		}
		
	}
}

