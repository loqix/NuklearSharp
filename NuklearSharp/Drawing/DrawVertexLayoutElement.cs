using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct DrawVertexLayoutElement
	{
		public int attribute;
		public int format;
		public ulong offset;

		public int IsEndOfLayout()
		{
			return (int)(((this.attribute) == (Nuklear.NK_VERTEX_ATTRIBUTE_COUNT)) || ((this.format) == (Nuklear.NK_FORMAT_COUNT))?1:0);
		}

	}
}