using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct DrawCommand
	{
		public uint elem_count;
		public Rect clip_rect;
		public Handle texture;
		public Handle userdata;

		public DrawCommand* DrawListNext(Buffer buffer, DrawList canvas)
		{
			DrawCommand* end;
			if (((buffer== null)) || (canvas== null)) return null;
			end = canvas.End(buffer);
			if (this <= end) return null;
			return (this - 1);
		}

		public DrawCommand* DrawNext(Buffer buffer, Context ctx)
		{
			return DrawListNext(buffer, ctx.draw_list);
		}

	}
}