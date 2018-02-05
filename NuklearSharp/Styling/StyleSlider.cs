using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleSlider
	{
		public StyleItem normal = new StyleItem();
		public StyleItem hover = new StyleItem();
		public StyleItem active = new StyleItem();
		public Color border_color = new Color();
		public Color bar_normal = new Color();
		public Color bar_hover = new Color();
		public Color bar_active = new Color();
		public Color bar_filled = new Color();
		public StyleItem cursor_normal = new StyleItem();
		public StyleItem cursor_hover = new StyleItem();
		public StyleItem cursor_active = new StyleItem();
		public float border;
		public float rounding;
		public float bar_height;
		public Vec2 padding = new Vec2();
		public Vec2 spacing = new Vec2();
		public Vec2 cursor_size = new Vec2();
		public int show_buttons;
		public StyleButton inc_button = new StyleButton();
		public StyleButton dec_button = new StyleButton();
		public int inc_symbol;
		public int dec_symbol;
		public Handle userdata = new Handle();
		public Nuklear.NkDrawNotify draw_begin;
		public  Nuklear.NkDrawNotify draw_end;

	}
}