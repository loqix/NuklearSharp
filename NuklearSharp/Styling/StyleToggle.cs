using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleToggle
	{
		public StyleItem normal = new StyleItem();
		public StyleItem hover = new StyleItem();
		public StyleItem active = new StyleItem();
		public Color border_color = new Color();
		public StyleItem cursor_normal = new StyleItem();
		public StyleItem cursor_hover = new StyleItem();
		public Color text_normal = new Color();
		public Color text_hover = new Color();
		public Color text_active = new Color();
		public Color text_background = new Color();
		public uint text_alignment;
		public Vec2 padding = new Vec2();
		public Vec2 touch_padding = new Vec2();
		public float spacing;
		public float border;
		public Handle userdata = new Handle();
		public Nuklear.NkDrawNotify draw_begin;
		public  Nuklear.NkDrawNotify draw_end;

	}
}
