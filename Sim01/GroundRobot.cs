using System;
using Sce.PlayStation.Core;

namespace Sim01
{
	public class GroundRobot
	{
		private Vector3 pos;
		protected float direction;
		protected float speed;
		protected Sprite sprite;
		
		private float counter5;
		private float counter20;
		private float counterRotate;
		
		private float density;
		public float Density
		{
			get{return density;}
			set{density = value;}
		}
		private Text densityDisplay;
		
		protected float diviation;
		private float angularSpeed;
		
		public Vector3 Pos
		{
			get{return pos;}
			set{pos = value;}
		}
		
		private bool preCollide;
		private bool collide;
		public bool Collide
		{
			get{return collide;}
			set{collide = value;}
		}
		
		public GroundRobot (Vector3 initPos, float dir)
		{
			pos = initPos;
			direction = dir;
			angularSpeed = FMath.PI/2;//roughly 180 in 2 secs (in rads)
			counter5 = 0;
			counter20 = 0;
			density = 0;
			
			collide = false;
			preCollide = false;
			
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
		
		public void ReColor (bool red)
		{
			if (red) {
				sprite.SetColor(1,0,0,1);
			}else{
				sprite.SetColor(0,1,0,1);
			}
		}
		
		
		public virtual void Update (float time)
		{
			density = 0;
			counter5 += time;  
			counter20 += time;
			if (counterRotate > 0) {
				counterRotate -= time;
				float deltaAngle = direction-sprite.Rotation;
				sprite.Rotation += angularSpeed*deltaAngle/FMath.Abs(deltaAngle)*time;
			}else{
				sprite.Rotation = direction;
				pos += new Vector3(FMath.Cos(direction+diviation)*speed*time,FMath.Sin(direction+diviation)*speed*time,0);
			}
			
			if (counter5 >= 5) {
				counter5 -= 5;
				trajectoryNoise();
			}
			if (counter20 >= 20) {
				counter20 -= 20;
				trajectoryNoise();
				direction += FMath.PI;
				counterRotate += FMath.PI/angularSpeed;
			}
			
			//handle collision
			if (collide && !preCollide) {
				counter20 = 0;
				trajectoryNoise();
				direction += FMath.PI;
				counterRotate += FMath.PI/angularSpeed;
			}
			preCollide = collide;
			if (counterRotate <= 0) {
				collide = false;
			}
			
			sprite.Position = pos;
		}
		
		public void trajectoryNoise ()
		{
			diviation = newDiviation(0.05f);// 3 degrees in rad 
			float deltaDir = newDiviation(0.349f);// 20 degrees in rad
			direction += deltaDir;
			counterRotate = deltaDir/angularSpeed;
		}
		
		public void Render ()
		{
			sprite.Render();
		}
	}
}

