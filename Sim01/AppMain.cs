using System;
using System.Diagnostics;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace Sim01
{
	public class AppMain
	{
		public static void Main (string[] args)
		{
			Stopwatch s = new Stopwatch ();
			s.Start ();
			Initialize ();

			while (true) {
				SystemEvents.CheckEvents ();
				float time = s.ElapsedMilliseconds / 1000f;
				s.Reset ();
				s.Start ();
				Update (time);
				Render ();
				Console.WriteLine("Time: "+Global.Time);//before update and render
			}
		}

		public static void Initialize ()
		{
			// Set up the graphics system
			Global.Graphics = new GraphicsContext ();
			Global.Time = 0;
			Global.Rand = new Random();
			Global.Control = new Player();
			Global.Textures = new List<Texture2D>();
			initTextures();
			
			Global.Bg = new Background();
			
			int GRnum = 10;
			Global.GRobots = new GroundRobot[GRnum];
			
			Vector3 center = new Vector3(Global.Graphics.Screen.Width/2,400,0);
			for (int i = 0; i < GRnum; i++) {
				float angle = i*FMath.PI*2/GRnum;
				Vector3 offset = new Vector3(FMath.Cos(angle),FMath.Sin(angle),0)*40;
				Global.GRobots[i] = new GroundRobot(center+offset,angle);
			}
			
			Global.ORobots = new Obsticle[4];
			
			for (int i = 0; i < 4; i++) {
				float angle = i * FMath.PI/2;
				Global.ORobots[i] = new Obsticle(center+new Vector3(40*5*FMath.Sin(angle),-40*5*FMath.Cos(angle),0), angle);
			}
			
		}
		
		private static void initTextures ()
		{
			Global.Textures.Add (new Texture2D ("/Application/assets/Grid.png", false));			//	0	Grid
			Global.Textures.Add (new Texture2D ("/Application/assets/ColoredLine.png", false));		//	1	Colored Line
			Global.Textures.Add (new Texture2D ("/Application/assets/GroundRobot.png", false));		//	2	Ground Robots
			
		}

		public static void Update (float time)
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
			
			Global.Control.Update(gamePadData,time);
			
			//collisions
			for (int i = 0; i < Global.GRobots.Length; i++) {
				for (int j = 0; j < Global.GRobots.Length; j++) {
					if (i!=j) {
						if (Global.GRobots[i].Pos.Distance(Global.GRobots[j].Pos) <= 10) {
							Global.GRobots[i].Collide = true;
							Global.GRobots[j].Collide = true;
						}
					}
				}
			}
			
			if (Global.Control.Play) {
				Global.Time += time;
				foreach (GroundRobot g in Global.GRobots) {
					g.Update(time);
				}
				foreach (GroundRobot o in Global.ORobots) {
					((Obsticle)o).Update(time);
				}
			}
		}

		public static void Render ()
		{
			// Clear the screen
			Global.Graphics.SetClearColor (0.0f, 0.3f, 0.5f, 0.0f);
			Global.Graphics.Clear ();
			
			Global.Bg.Render();
			
			foreach (GroundRobot g in Global.GRobots) {
				g.Render();
			}
			foreach (GroundRobot o in Global.ORobots) {
				o.Render();
			}
			
			// Present the screen
			Global.Graphics.SwapBuffers ();
		}
	}
}
