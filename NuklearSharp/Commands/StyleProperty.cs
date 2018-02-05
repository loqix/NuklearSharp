using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleProperty
	{
		public StyleItem normal = new StyleItem();
		public StyleItem hover = new StyleItem();
		public StyleItem active = new StyleItem();
		public Color border_color = new Color();
		public Color label_normal = new Color();
		public Color label_hover = new Color();
		public Color label_active = new Color();
		public int sym_left;
		public int sym_right;
		public float border;
		public float rounding;
		public Vec2 padding = new Vec2();
		public StyleEdit edit = new StyleEdit();
		public StyleButton inc_button = new StyleButton();
		public StyleButton dec_button = new StyleButton();
		public Handle userdata = new Handle();
		public Nuklear.NkDrawNotify draw_begin;
		public  Nuklear.NkDrawNotify draw_end;

	}
}
