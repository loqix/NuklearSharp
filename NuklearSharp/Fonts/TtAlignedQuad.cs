using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtAlignedQuad
	{
		public float x0;
		public float y0;
		public float s0;
		public float t0;
		public float x1;
		public float y1;
		public float s1;
		public float t1;

	}
}