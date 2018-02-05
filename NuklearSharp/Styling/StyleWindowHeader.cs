using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleWindowHeader
	{
		public StyleItem normal = new StyleItem();
		public StyleItem hover = new StyleItem();
		public StyleItem active = new StyleItem();
		public StyleButton close_button = new StyleButton();
		public StyleButton minimize_button = new StyleButton();
		public int close_symbol;
		public int minimize_symbol;
		public int maximize_symbol;
		public Color label_normal = new Color();
		public Color label_hover = new Color();
		public Color label_active = new Color();
		public int align;
		public Vec2 padding = new Vec2();
		public Vec2 label_padding = new Vec2();
		public Vec2 spacing = new Vec2();

	}
}
