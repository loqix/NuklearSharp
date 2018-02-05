using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class Style
	{
		public Vec2 PanelGetPadding(int type)
		{
			switch (type)
			{
				default:
				case Nuklear.NK_PANEL_WINDOW:
					return (Vec2) (this.window.padding);
				case Nuklear.NK_PANEL_GROUP:
					return (Vec2) (this.window.group_padding);
				case Nuklear.NK_PANEL_POPUP:
					return (Vec2) (this.window.popup_padding);
				case Nuklear.NK_PANEL_CONTEXTUAL:
					return (Vec2) (this.window.contextual_padding);
				case Nuklear.NK_PANEL_COMBO:
					return (Vec2) (this.window.combo_padding);
				case Nuklear.NK_PANEL_MENU:
					return (Vec2) (this.window.menu_padding);
				case Nuklear.NK_PANEL_TOOLTIP:
					return (Vec2) (this.window.menu_padding);
			}

		}

		public float PanelGetBorder(uint flags, int type)
		{
			if ((flags & Nuklear.NK_WINDOW_BORDER) != 0)
			{
				switch (type)
				{
					default:
					case Nuklear.NK_PANEL_WINDOW:
						return (float) (this.window.border);
					case Nuklear.NK_PANEL_GROUP:
						return (float) (this.window.group_border);
					case Nuklear.NK_PANEL_POPUP:
						return (float) (this.window.popup_border);
					case Nuklear.NK_PANEL_CONTEXTUAL:
						return (float) (this.window.contextual_border);
					case Nuklear.NK_PANEL_COMBO:
						return (float) (this.window.combo_border);
					case Nuklear.NK_PANEL_MENU:
						return (float) (this.window.menu_border);
					case Nuklear.NK_PANEL_TOOLTIP:
						return (float) (this.window.menu_border);
				}
			}
			else return (float) (0);
		}

		public Color PanelGetBorderColor(int type)
		{
			switch (type)
			{
				default:
				case Nuklear.NK_PANEL_WINDOW:
					return (Color) (this.window.border_color);
				case Nuklear.NK_PANEL_GROUP:
					return (Color) (this.window.group_border_color);
				case Nuklear.NK_PANEL_POPUP:
					return (Color) (this.window.popup_border_color);
				case Nuklear.NK_PANEL_CONTEXTUAL:
					return (Color) (this.window.contextual_border_color);
				case Nuklear.NK_PANEL_COMBO:
					return (Color) (this.window.combo_border_color);
				case Nuklear.NK_PANEL_MENU:
					return (Color) (this.window.menu_border_color);
				case Nuklear.NK_PANEL_TOOLTIP:
					return (Color) (this.window.menu_border_color);
			}

		}

		public float LayoutRowCalculateUsableSpace(int type, float total_space, int columns)
		{
			float panel_padding;
			float panel_spacing;
			float panel_space;
			Vec2 spacing = new Vec2();
			Vec2 padding = new Vec2();
			spacing = (Vec2) (this.window.spacing);
			padding = (Vec2) (PanelGetPadding((int) (type)));
			panel_padding = (float) (2*padding.x);
			panel_spacing = (float) ((float) ((columns - 1) < (0) ? (0) : (columns - 1))*spacing.x);
			panel_space = (float) (total_space - panel_padding - panel_spacing);
			return (float) (panel_space);
		}
	}
}