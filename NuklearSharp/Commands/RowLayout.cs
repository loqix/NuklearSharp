using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class RowLayout
	{
		public int type;
		public int index;
		public float height;
		public float min_height;
		public int columns;
		public float* ratio;
		public float item_width;
		public float item_height;
		public float item_offset;
		public float filled;
		public Rect item = new Rect();
		public int tree_depth;
		public PinnedArray<float> templates = new PinnedArray<float>(16);

	}
}
