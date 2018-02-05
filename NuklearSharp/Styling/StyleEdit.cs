using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleEdit
	{
		public StyleItem normal = new StyleItem();
		public StyleItem hover = new StyleItem();
		public StyleItem active = new StyleItem();
		public Color border_color = new Color();
		public StyleScrollbar scrollbar = new StyleScrollbar();
		public Color cursor_normal = new Color();
		public Color cursor_hover = new Color();
		public Color cursor_text_normal = new Color();
		public Color cursor_text_hover = new Color();
		public Color text_normal = new Color();
		public Color text_hover = new Color();
		public Color text_active = new Color();
		public Color selected_normal = new Color();
		public Color selected_hover = new Color();
		public Color selected_text_normal = new Color();
		public Color selected_text_hover = new Color();
		public float border;
		public float rounding;
		public float cursor_size;
		public Vec2 scrollbar_size = new Vec2();
		public Vec2 padding = new Vec2();
		public float row_padding;

	}
}
