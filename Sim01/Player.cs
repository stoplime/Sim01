using System;

using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core;

namespace Sim01
{
	public class Player
	{
		public bool Play;
		
		public Player ()
		{
			Play = false;
		}
		
		public void Update (GamePadData gpd, float time)
		{
			if ((gpd.Buttons & GamePadButtons.Right) != 0 && (gpd.ButtonsPrev & GamePadButtons.Right) == 0) {
				Play = !Play;
			}
		}
	}
}

