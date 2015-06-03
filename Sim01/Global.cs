using System;
using System.Collections.Generic;

using Sce.PlayStation.Core.Graphics;

namespace Sim01
{
	public static class Global
	{
		public static GraphicsContext Graphics;
		public static List<Texture2D> Textures;
		public static float Time;
		public static Random Rand;
		
		public static Player Control;
		
		public static Background Bg;
		public static GroundRobot[] GRobots;
	}
}

