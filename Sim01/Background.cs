using System;
using Sce.PlayStation.Core;

namespace Sim01
{
	public class Background
	{
		private Vector3 initPos;
		
		private Sprite[,] grids;
		private Sprite redLine;
		private Sprite greenLine;
		
		public Background ()
		{
			initPos = new Vector3(Global.Graphics.Screen.Width/2-400,0,0);
			
			grids = new Sprite[20,20];
			for (int i = 0; i < 20; i++) {
				for (int j = 0; j < 20; j++) {
					grids[i,j] = new Sprite(Global.Graphics,Global.Textures[0]);
					grids[i,j].Position = initPos+new Vector3(40*i,40*j,0);
					
				}
			}
			
			redLine = new Sprite(Global.Graphics, Global.Textures[1]);
			greenLine = new Sprite(Global.Graphics, Global.Textures[1]);
			
			redLine.SetColor(1f,0f,0f,1f);
			greenLine.SetColor(0f,1f,0f,1f);
			
			redLine.Position = initPos;
			greenLine.Position = initPos+ new Vector3(0,798,0);
		}
		
		public void Render ()
		{
			foreach(Sprite g in grids)
			{
				g.Render();
			}
			redLine.Render();
			greenLine.Render();
		}
	}
}

