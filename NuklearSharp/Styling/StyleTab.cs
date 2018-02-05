using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleTab
	{
		public StyleItem background = new StyleItem();
		public Color border_color = new Color();
		public Color text = new Color();
		public StyleButton tab_maximize_button = new StyleButton();
		public StyleButton tab_minimize_button = new StyleButton();
		public StyleButton node_maximize_button = new StyleButton();
		public StyleButton node_minimize_button = new StyleButton();
		public int sym_minimize;
		public int sym_maximize;
		public float border;
		public float rounding;
		public float indent;
		public Vec2 padding = new Vec2();
		public Vec2 spacing = new Vec2();

	}
}
