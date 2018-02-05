using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleCombo
	{
		public StyleItem normal = new StyleItem();
		public StyleItem hover = new StyleItem();
		public StyleItem active = new StyleItem();
		public Color border_color = new Color();
		public Color label_normal = new Color();
		public Color label_hover = new Color();
		public Color label_active = new Color();
		public Color symbol_normal = new Color();
		public Color symbol_hover = new Color();
		public Color symbol_active = new Color();
		public StyleButton button = new StyleButton();
		public int sym_normal;
		public int sym_hover;
		public int sym_active;
		public float border;
		public float rounding;
		public Vec2 content_padding = new Vec2();
		public Vec2 button_padding = new Vec2();
		public Vec2 spacing = new Vec2();

	}
}
