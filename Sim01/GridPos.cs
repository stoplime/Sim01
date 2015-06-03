using System;

using Sce.PlayStation.Core;

namespace Sim01
{
	public struct GridPos
	{
		/// <summary>
		/// The x in grid form (meters)
		/// </summary>
		public float X;
		/// <summary>
		/// The y in grid form (meters)
		/// </summary>
		public float Y;
		/// <summary>
		/// The pixel position in sim.
		/// </summary>
		public Vector3 Pos;
	}
}

