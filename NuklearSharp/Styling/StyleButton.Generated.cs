// Generated by Sichem at 2/7/2018 6:17:24 PM

using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleButton
	{
		public StyleItem normal = new StyleItem();
		public StyleItem hover = new StyleItem();
		public StyleItem active = new StyleItem();
		public Color border_color = new Color();
		public Color text_background = new Color();
		public Color text_normal = new Color();
		public Color text_hover = new Color();
		public Color text_active = new Color();
		public uint text_alignment;
		public float border;
		public float rounding;
		public Vec2 padding = new Vec2();
		public Vec2 image_padding = new Vec2();
		public Vec2 touch_padding = new Vec2();
		public Handle userdata = new Handle();
		public Nuklear.NkDrawNotify draw_begin;
		public Nuklear.NkDrawNotify draw_end;
	}
}