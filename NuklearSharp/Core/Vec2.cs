using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Vec2
	{
		public float x;
		public float y;

		public Rect Recta(Vec2 size)
		{
			return (Rect)(Nuklear.Rectz((float)(this.x), (float)(this.y), (float)(size.x), (float)(size.y)));
		}

		public void TriangleFromDirection(Rect r, float pad_x, float pad_y, int direction)
		{
			float w_half;float h_half;
			r.w = (float)((2 * pad_x) < (r.w)?(r.w):(2 * pad_x));
			r.h = (float)((2 * pad_y) < (r.h)?(r.h):(2 * pad_y));
			r.w = (float)(r.w - 2 * pad_x);
			r.h = (float)(r.h - 2 * pad_y);
			r.x = (float)(r.x + pad_x);
			r.y = (float)(r.y + pad_y);
			w_half = (float)(r.w / 2.0f);
			h_half = (float)(r.h / 2.0f);
			if ((direction) == (Nuklear.NK_UP)) {
this[0] = (Vec2)(Nuklear.Vec2z((float)(r.x + w_half), (float)(r.y)));this[1] = (Vec2)(Nuklear.Vec2z((float)(r.x + r.w), (float)(r.y + r.h)));this[2] = (Vec2)(Nuklear.Vec2z((float)(r.x), (float)(r.y + r.h)));}
 else if ((direction) == (Nuklear.NK_RIGHT)) {
this[0] = (Vec2)(Nuklear.Vec2z((float)(r.x), (float)(r.y)));this[1] = (Vec2)(Nuklear.Vec2z((float)(r.x + r.w), (float)(r.y + h_half)));this[2] = (Vec2)(Nuklear.Vec2z((float)(r.x), (float)(r.y + r.h)));}
 else if ((direction) == (Nuklear.NK_DOWN)) {
this[0] = (Vec2)(Nuklear.Vec2z((float)(r.x), (float)(r.y)));this[1] = (Vec2)(Nuklear.Vec2z((float)(r.x + r.w), (float)(r.y)));this[2] = (Vec2)(Nuklear.Vec2z((float)(r.x + w_half), (float)(r.y + r.h)));}
 else {
this[0] = (Vec2)(Nuklear.Vec2z((float)(r.x), (float)(r.y + h_half)));this[1] = (Vec2)(Nuklear.Vec2z((float)(r.x + r.w), (float)(r.y)));this[2] = (Vec2)(Nuklear.Vec2z((float)(r.x + r.w), (float)(r.y + r.h)));}

		}

	}
}
