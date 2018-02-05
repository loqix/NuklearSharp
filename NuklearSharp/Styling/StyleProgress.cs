using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleProgress
	{
		public StyleItem normal = new StyleItem();
		public StyleItem hover = new StyleItem();
		public StyleItem active = new StyleItem();
		public Color border_color = new Color();
		public StyleItem cursor_normal = new StyleItem();
		public StyleItem cursor_hover = new StyleItem();
		public StyleItem cursor_active = new StyleItem();
		public Color cursor_border_color = new Color();
		public float rounding;
		public float border;
		public float cursor_border;
		public float cursor_rounding;
		public Vec2 padding = new Vec2();
		public Handle userdata = new Handle();
		public Nuklear.NkDrawNotify draw_begin;
		public  Nuklear.NkDrawNotify draw_end;

	}
}