using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class ChartSlot
	{
		public int type;
		public Color color = new Color();
		public Color highlight = new Color();
		public float min;
		public float max;
		public float range;
		public int count;
		public Vec2 last = new Vec2();
		public int index;

	}
}
