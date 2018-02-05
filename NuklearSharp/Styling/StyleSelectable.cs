using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleSelectable
	{
		public StyleItem normal = new StyleItem();
		public StyleItem hover = new StyleItem();
		public StyleItem pressed = new StyleItem();
		public StyleItem normal_active = new StyleItem();
		public StyleItem hover_active = new StyleItem();
		public StyleItem pressed_active = new StyleItem();
		public Color text_normal = new Color();
		public Color text_hover = new Color();
		public Color text_pressed = new Color();
		public Color text_normal_active = new Color();
		public Color text_hover_active = new Color();
		public Color text_pressed_active = new Color();
		public Color text_background = new Color();
		public uint text_alignment;
		public float rounding;
		public Vec2 padding = new Vec2();
		public Vec2 touch_padding = new Vec2();
		public Vec2 image_padding = new Vec2();
		public Handle userdata = new Handle();
		public Nuklear.NkDrawNotify draw_begin;
		public  Nuklear.NkDrawNotify draw_end;

	}
}
