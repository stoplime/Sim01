using System;
using Sce.PlayStation.Core;

namespace Sim01
{
	public class GroundRobot
	{
		private Vector3 pos;
		private float direction;
		private float speed;
		private Sprite sprite;
		private float counter5;
		private float counter20;
		private float diviation;
		
		public GroundRobot (Vector3 initPos, float dir)
		{
			pos = initPos;
			direction = dir;
			counter5 = 0;
			counter20 = 0;
			
			diviation = newDiviation(0.05f);// 3 degrees in rad
			speed = 13.33f;	// pixels per second ~~ 0.33 m/s
			
			sprite = new Sprite(Global.Graphics,Global.Textures[2]);
			sprite.Center = new Vector2(0.5f,0.5f);
			sprite.Rotation = dir;
			sprite.Position = initPos;
		}
		
		public float newDiviation (float magnitude)
		{
			return (float)Global.Rand.NextDouble()*magnitude*((Global.Rand.Next(0,2)==0)?1:-1);
		}
		
		public void Update (float time)
		{
			counter5 += time;  
			counter20 += time;
			pos += new Vector3(FMath.Cos(direction+diviation)*speed*time,FMath.Sin(direction+diviation)*speed*time,0);
			
			if (counter5 >= 5) {
				counter5 -= 5;
				trajectoryNoise();
			}
			if (counter20 >= 20) {
				counter20 -= 20;
				trajectoryNoise();
				direction += FMath.PI;
			}
			
			sprite.Position = pos;
			sprite.Rotation = direction;
		}
		
		public void trajectoryNoise ()
		{
			diviation = newDiviation(0.05f);// 3 degrees in rad 
			direction += newDiviation(0.349f);// 20 degrees in rad
		}
		
		public void Render ()
		{
			sprite.Render();
		}
	}
}

