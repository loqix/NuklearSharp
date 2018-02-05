using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Colorf
	{
		public float r;
		public float g;
		public float b;
		public float a;

		public Color RgbaCf()
		{
			return (Color)(Nuklear.RgbaF((float)(this.r), (float)(this.g), (float)(this.b), (float)(this.a)));
		}

		public Color RgbCf()
		{
			return (Color)(Nuklear.RgbF((float)(this.r), (float)(this.g), (float)(this.b)));
		}

	}
}
