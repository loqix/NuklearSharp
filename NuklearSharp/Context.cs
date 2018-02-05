using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class Context
	{
		public uint Convert(Buffer cmds, Buffer vertices, Buffer elements, ConvertConfig config)
		{
			uint res = (uint) (Nuklear.NK_CONVERT_SUCCESS);

			if ((((((cmds == null)) || (vertices == null)) || (elements == null)) || (config == null)) ||
			    (config.vertex_layout == null)) return (uint) (Nuklear.NK_CONVERT_INVALID_PARAM);
			this.draw_list.Setup(config, cmds, vertices, elements, (int) (config.line_AA), (int) (config.shape_AA));
			var top_window = Begin();
			foreach (var cmd in top_window.buffer.commands)
			{
				this.draw_list.userdata = (Handle) (cmd.userdata);
				switch (cmd.header.type)
				{
					case Nuklear.NK_COMMAND_NOP:
						break;
					case Nuklear.NK_COMMAND_SCISSOR:
					{
						CommandScissor s = (CommandScissor) (cmd);
						this.draw_list.AddClip((Rect) (Nuklear.Rectz((float) (s.x), (float) (s.y), (float) (s.w), (float) (s.h))));
					}
						break;
					case Nuklear.NK_COMMAND_LINE:
					{
						CommandLine l = (CommandLine) (cmd);
						this.draw_list.StrokeLine((Vec2) (Nuklear.Vec2z((float) (l.begin.x), (float) (l.begin.y))),
							(Vec2) (Nuklear.Vec2z((float) (l.end.x), (float) (l.end.y))), (Color) (l.color), (float) (l.line_thickness));
					}
						break;
					case Nuklear.NK_COMMAND_CURVE:
					{
						CommandCurve q = (CommandCurve) (cmd);
						this.draw_list.StrokeCurve((Vec2) (Nuklear.Vec2z((float) (q.begin.x), (float) (q.begin.y))),
							(Vec2) (Nuklear.Vec2z((float) (q.ctrl_0.x), (float) (q.ctrl_0.y))),
							(Vec2) (Nuklear.Vec2z((float) (q.ctrl_1.x), (float) (q.ctrl_1.y))),
							(Vec2) (Nuklear.Vec2z((float) (q.end.x), (float) (q.end.y))), (Color) (q.color),
							(uint) (config.curve_segment_count), (float) (q.line_thickness));
					}
						break;
					case Nuklear.NK_COMMAND_RECT:
					{
						CommandRect r = (CommandRect) (cmd);
						this.draw_list.StrokeRect((Rect) (Nuklear.Rectz((float) (r.x), (float) (r.y), (float) (r.w), (float) (r.h))),
							(Color) (r.color), (float) (r.rounding), (float) (r.line_thickness));
					}
						break;
					case Nuklear.NK_COMMAND_RECT_FILLED:
					{
						CommandRectFilled r = (CommandRectFilled) (cmd);
						this.draw_list.FillRect((Rect) (Nuklear.Rectz((float) (r.x), (float) (r.y), (float) (r.w), (float) (r.h))),
							(Color) (r.color), (float) (r.rounding));
					}
						break;
					case Nuklear.NK_COMMAND_RECT_MULTI_COLOR:
					{
						CommandRectMultiColor r = (CommandRectMultiColor) (cmd);
						this.draw_list.FillRectMultiColor(
							(Rect) (Nuklear.Rectz((float) (r.x), (float) (r.y), (float) (r.w), (float) (r.h))), (Color) (r.left),
							(Color) (r.top), (Color) (r.right), (Color) (r.bottom));
					}
						break;
					case Nuklear.NK_COMMAND_CIRCLE:
					{
						CommandCircle c = (CommandCircle) (cmd);
						this.draw_list.StrokeCircle(
							(Vec2) (Nuklear.Vec2z((float) ((float) (c.x) + (float) (c.w)/2), (float) ((float) (c.y) + (float) (c.h)/2))),
							(float) ((float) (c.w)/2), (Color) (c.color), (uint) (config.circle_segment_count), (float) (c.line_thickness));
					}
						break;
					case Nuklear.NK_COMMAND_CIRCLE_FILLED:
					{
						CommandCircleFilled c = (CommandCircleFilled) (cmd);
						this.draw_list.FillCircle(
							(Vec2) (Nuklear.Vec2z((float) ((float) (c.x) + (float) (c.w)/2), (float) ((float) (c.y) + (float) (c.h)/2))),
							(float) ((float) (c.w)/2), (Color) (c.color), (uint) (config.circle_segment_count));
					}
						break;
					case Nuklear.NK_COMMAND_ARC:
					{
						CommandArc c = (CommandArc) (cmd);
						this.draw_list.PathLineTo((Vec2) (Nuklear.Vec2z((float) (c.cx), (float) (c.cy))));
						this.draw_list.PathArcTo((Vec2) (Nuklear.Vec2z((float) (c.cx), (float) (c.cy))), (float) (c.r), (float) (c.a[0]),
							(float) (c.a[1]), (uint) (config.arc_segment_count));
						this.draw_list.PathStroke((Color) (c.color), (int) (Nuklear.NK_STROKE_CLOSED), (float) (c.line_thickness));
					}
						break;
					case Nuklear.NK_COMMAND_ARC_FILLED:
					{
						CommandArcFilled c = (CommandArcFilled) (cmd);
						this.draw_list.PathLineTo((Vec2) (Nuklear.Vec2z((float) (c.cx), (float) (c.cy))));
						this.draw_list.PathArcTo((Vec2) (Nuklear.Vec2z((float) (c.cx), (float) (c.cy))), (float) (c.r), (float) (c.a[0]),
							(float) (c.a[1]), (uint) (config.arc_segment_count));
						this.draw_list.PathFill((Color) (c.color));
					}
						break;
					case Nuklear.NK_COMMAND_TRIANGLE:
					{
						CommandTriangle t = (CommandTriangle) (cmd);
						this.draw_list.StrokeTriangle((Vec2) (Nuklear.Vec2z((float) (t.a.x), (float) (t.a.y))),
							(Vec2) (Nuklear.Vec2z((float) (t.b.x), (float) (t.b.y))),
							(Vec2) (Nuklear.Vec2z((float) (t.c.x), (float) (t.c.y))), (Color) (t.color), (float) (t.line_thickness));
					}
						break;
					case Nuklear.NK_COMMAND_TRIANGLE_FILLED:
					{
						CommandTriangleFilled t = (CommandTriangleFilled) (cmd);
						this.draw_list.FillTriangle((Vec2) (Nuklear.Vec2z((float) (t.a.x), (float) (t.a.y))),
							(Vec2) (Nuklear.Vec2z((float) (t.b.x), (float) (t.b.y))),
							(Vec2) (Nuklear.Vec2z((float) (t.c.x), (float) (t.c.y))), (Color) (t.color));
					}
						break;
					case Nuklear.NK_COMMAND_POLYGON:
					{
						int i;
						CommandPolygon p = (CommandPolygon) (cmd);
						for (i = (int) (0); (i) < (p.point_count); ++i)
						{
							Vec2 pnt = (Vec2) (Nuklear.Vec2z((float) (p.points[i].x), (float) (p.points[i].y)));
							this.draw_list.PathLineTo((Vec2) (pnt));
						}
						this.draw_list.PathStroke((Color) (p.color), (int) (Nuklear.NK_STROKE_CLOSED), (float) (p.line_thickness));
					}
						break;
					case Nuklear.NK_COMMAND_POLYGON_FILLED:
					{
						int i;
						CommandPolygonFilled p = (CommandPolygonFilled) (cmd);
						for (i = (int) (0); (i) < (p.point_count); ++i)
						{
							Vec2 pnt = (Vec2) (Nuklear.Vec2z((float) (p.points[i].x), (float) (p.points[i].y)));
							this.draw_list.PathLineTo((Vec2) (pnt));
						}
						this.draw_list.PathFill((Color) (p.color));
					}
						break;
					case Nuklear.NK_COMMAND_POLYLINE:
					{
						int i;
						CommandPolyline p = (CommandPolyline) (cmd);
						for (i = (int) (0); (i) < (p.point_count); ++i)
						{
							Vec2 pnt = (Vec2) (Nuklear.Vec2z((float) (p.points[i].x), (float) (p.points[i].y)));
							this.draw_list.PathLineTo((Vec2) (pnt));
						}
						this.draw_list.PathStroke((Color) (p.color), (int) (Nuklear.NK_STROKE_OPEN), (float) (p.line_thickness));
					}
						break;
					case Nuklear.NK_COMMAND_TEXT:
					{
						CommandText t = (CommandText) (cmd);
						this.draw_list.AddText(t.font, (Rect) (Nuklear.Rectz((float) (t.x), (float) (t.y), (float) (t.w), (float) (t.h))),
							t._string_, (int) (t.length), (float) (t.height), (Color) (t.foreground));
					}
						break;
					case Nuklear.NK_COMMAND_IMAGE:
					{
						CommandImage i = (CommandImage) (cmd);
						this.draw_list.AddImage((Image) (i.img),
							(Rect) (Nuklear.Rectz((float) (i.x), (float) (i.y), (float) (i.w), (float) (i.h))), (Color) (i.col));
					}
						break;
					case Nuklear.NK_COMMAND_CUSTOM:
					{
						CommandCustom c = (CommandCustom) (cmd);
						c.callback(this.draw_list, (short) (c.x), (short) (c.y), (ushort) (c.w), (ushort) (c.h),
							(Handle) (c.callback_data));
					}
						break;
					default:
						break;
				}
			}
			res |=
				(uint)
					(((cmds.needed) > (cmds.allocated + (cmds.memory.size - cmds.size))) ? Nuklear.NK_CONVERT_COMMAND_BUFFER_FULL : 0);
			res |= (uint) (((vertices.needed) > (vertices.allocated)) ? Nuklear.NK_CONVERT_VERTEX_BUFFER_FULL : 0);
			res |= (uint) (((elements.needed) > (elements.allocated)) ? Nuklear.NK_CONVERT_ELEMENT_BUFFER_FULL : 0);
			return (uint) (res);
		}

		public DrawCommand* DrawBegin(Buffer buffer)
		{
			return this.draw_list.Begin(buffer);
		}

		public DrawCommand* DrawEnd(Buffer buffer)
		{
			return this.draw_list.End(buffer);
		}

		public void InputBegin()
		{
			int i;
			Input _in_;
			if (this == null) return;
			_in_ = this.input;
			for (i = (int) (0); (i) < (Nuklear.NK_BUTTON_MAX); ++i)
			{
				((MouseButton*) _in_.mouse.buttons + i)->clicked = (uint) (0);
			}
			_in_.keyboard.text_len = (int) (0);
			_in_.mouse.scroll_delta = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			_in_.mouse.prev.x = (float) (_in_.mouse.pos.x);
			_in_.mouse.prev.y = (float) (_in_.mouse.pos.y);
			_in_.mouse.delta.x = (float) (0);
			_in_.mouse.delta.y = (float) (0);
			for (i = (int) (0); (i) < (Nuklear.NK_KEY_MAX); i++)
			{
				((Key*) _in_.keyboard.keys + i)->clicked = (uint) (0);
			}
		}

		public void InputEnd()
		{
			Input _in_;
			if (this == null) return;
			_in_ = this.input;
			if ((_in_.mouse.grab) != 0) _in_.mouse.grab = (byte) (0);
			if ((_in_.mouse.ungrab) != 0)
			{
				_in_.mouse.grabbed = (byte) (0);
				_in_.mouse.ungrab = (byte) (0);
				_in_.mouse.grab = (byte) (0);
			}

		}

		public void InputMotion(int x, int y)
		{
			Input _in_;
			if (this == null) return;
			_in_ = this.input;
			_in_.mouse.pos.x = ((float) (x));
			_in_.mouse.pos.y = ((float) (y));
			_in_.mouse.delta.x = (float) (_in_.mouse.pos.x - _in_.mouse.prev.x);
			_in_.mouse.delta.y = (float) (_in_.mouse.pos.y - _in_.mouse.prev.y);
		}

		public void InputKey(int key, int down)
		{
			Input _in_;
			if (this == null) return;
			_in_ = this.input;
			if (((Key*) _in_.keyboard.keys + key)->down != down) ((Key*) _in_.keyboard.keys + key)->clicked++;
			((Key*) _in_.keyboard.keys + key)->down = (int) (down);
		}

		public void InputButton(int id, int x, int y, int down)
		{
			MouseButton* btn;
			Input _in_;
			if (this == null) return;
			_in_ = this.input;
			if ((_in_.mouse.buttons[id].down) == (down)) return;
			btn = (MouseButton*) _in_.mouse.buttons + id;
			btn->clicked_pos.x = ((float) (x));
			btn->clicked_pos.y = ((float) (y));
			btn->down = (int) (down);
			btn->clicked++;
		}

		public void InputScroll(Vec2 val)
		{
			if (this == null) return;
			this.input.mouse.scroll_delta.x += (float) (val.x);
			this.input.mouse.scroll_delta.y += (float) (val.y);
		}

		public void InputGlyph(char* glyph)
		{
			int len = (int) (0);
			char unicode;
			Input _in_;
			if (this == null) return;
			_in_ = this.input;
			len = (int) (Nuklear.UtfDecode(glyph, &unicode, (int) (4)));
			if (((len) != 0) && ((_in_.keyboard.text_len + len) < (16)))
			{
				Nuklear.UtfEncode(unicode, (char*) _in_.keyboard.text + _in_.keyboard.text_len, (int) (16 - _in_.keyboard.text_len));
				_in_.keyboard.text_len += (int) (len);
			}

		}

		public void InputChar(char c)
		{
			char* glyph = stackalloc char[4];
			if (this == null) return;
			glyph[0] = c;
			InputGlyph(glyph);
		}

		public void InputUnicode(char unicode)
		{
			char* rune = stackalloc char[4];
			if (this == null) return;
			Nuklear.UtfEncode(unicode, rune, (int) (4));
			InputGlyph(rune);
		}

		public void StyleDefault()
		{
			StyleFromTable(null);
		}

		public void StyleFromTable(Color* table)
		{
			Style style;
			StyleText text;
			StyleButton button;
			StyleToggle toggle;
			StyleSelectable select;
			StyleSlider slider;
			StyleProgress prog;
			StyleScrollbar scroll;
			StyleEdit edit;
			StyleProperty property;
			StyleCombo combo;
			StyleChart chart;
			StyleTab tab;
			StyleWindow win;
			if (this == null) return;
			style = this.style;
			table = (table == null) ? Nuklear.nk_default_color_style : table;
			text = style.text;
			text.color = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			text.padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			button = style.button;

			button.normal = (StyleItem) (table[Nuklear.NK_COLOR_BUTTON].StyleItemColor());
			button.hover = (StyleItem) (table[Nuklear.NK_COLOR_BUTTON_HOVER].StyleItemColor());
			button.active = (StyleItem) (table[Nuklear.NK_COLOR_BUTTON_ACTIVE].StyleItemColor());
			button.border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			button.text_background = (Color) (table[Nuklear.NK_COLOR_BUTTON]);
			button.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.padding = (Vec2) (Nuklear.Vec2z((float) (2.0f), (float) (2.0f)));
			button.image_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (1.0f);
			button.rounding = (float) (4.0f);
			button.draw_begin = null;
			button.draw_end = null;
			button = style.contextual_button;

			button.normal = (StyleItem) (table[Nuklear.NK_COLOR_WINDOW].StyleItemColor());
			button.hover = (StyleItem) (table[Nuklear.NK_COLOR_BUTTON_HOVER].StyleItemColor());
			button.active = (StyleItem) (table[Nuklear.NK_COLOR_BUTTON_ACTIVE].StyleItemColor());
			button.border_color = (Color) (table[Nuklear.NK_COLOR_WINDOW]);
			button.text_background = (Color) (table[Nuklear.NK_COLOR_WINDOW]);
			button.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.padding = (Vec2) (Nuklear.Vec2z((float) (2.0f), (float) (2.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			button = style.menu_button;

			button.normal = (StyleItem) (table[Nuklear.NK_COLOR_WINDOW].StyleItemColor());
			button.hover = (StyleItem) (table[Nuklear.NK_COLOR_WINDOW].StyleItemColor());
			button.active = (StyleItem) (table[Nuklear.NK_COLOR_WINDOW].StyleItemColor());
			button.border_color = (Color) (table[Nuklear.NK_COLOR_WINDOW]);
			button.text_background = (Color) (table[Nuklear.NK_COLOR_WINDOW]);
			button.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.padding = (Vec2) (Nuklear.Vec2z((float) (2.0f), (float) (2.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (1.0f);
			button.draw_begin = null;
			button.draw_end = null;
			toggle = style.checkbox;

			toggle.normal = (StyleItem) (table[Nuklear.NK_COLOR_TOGGLE].StyleItemColor());
			toggle.hover = (StyleItem) (table[Nuklear.NK_COLOR_TOGGLE_HOVER].StyleItemColor());
			toggle.active = (StyleItem) (table[Nuklear.NK_COLOR_TOGGLE_HOVER].StyleItemColor());
			toggle.cursor_normal = (StyleItem) (table[Nuklear.NK_COLOR_TOGGLE_CURSOR].StyleItemColor());
			toggle.cursor_hover = (StyleItem) (table[Nuklear.NK_COLOR_TOGGLE_CURSOR].StyleItemColor());
			toggle.userdata = (Handle) (Nuklear.HandlePtr(null));
			toggle.text_background = (Color) (table[Nuklear.NK_COLOR_WINDOW]);
			toggle.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			toggle.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			toggle.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			toggle.padding = (Vec2) (Nuklear.Vec2z((float) (2.0f), (float) (2.0f)));
			toggle.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			toggle.border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			toggle.border = (float) (0.0f);
			toggle.spacing = (float) (4);
			toggle = style.option;

			toggle.normal = (StyleItem) (table[Nuklear.NK_COLOR_TOGGLE].StyleItemColor());
			toggle.hover = (StyleItem) (table[Nuklear.NK_COLOR_TOGGLE_HOVER].StyleItemColor());
			toggle.active = (StyleItem) (table[Nuklear.NK_COLOR_TOGGLE_HOVER].StyleItemColor());
			toggle.cursor_normal = (StyleItem) (table[Nuklear.NK_COLOR_TOGGLE_CURSOR].StyleItemColor());
			toggle.cursor_hover = (StyleItem) (table[Nuklear.NK_COLOR_TOGGLE_CURSOR].StyleItemColor());
			toggle.userdata = (Handle) (Nuklear.HandlePtr(null));
			toggle.text_background = (Color) (table[Nuklear.NK_COLOR_WINDOW]);
			toggle.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			toggle.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			toggle.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			toggle.padding = (Vec2) (Nuklear.Vec2z((float) (3.0f), (float) (3.0f)));
			toggle.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			toggle.border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			toggle.border = (float) (0.0f);
			toggle.spacing = (float) (4);
			select = style.selectable;

			select.normal = (StyleItem) (table[Nuklear.NK_COLOR_SELECT].StyleItemColor());
			select.hover = (StyleItem) (table[Nuklear.NK_COLOR_SELECT].StyleItemColor());
			select.pressed = (StyleItem) (table[Nuklear.NK_COLOR_SELECT].StyleItemColor());
			select.normal_active = (StyleItem) (table[Nuklear.NK_COLOR_SELECT_ACTIVE].StyleItemColor());
			select.hover_active = (StyleItem) (table[Nuklear.NK_COLOR_SELECT_ACTIVE].StyleItemColor());
			select.pressed_active = (StyleItem) (table[Nuklear.NK_COLOR_SELECT_ACTIVE].StyleItemColor());
			select.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			select.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			select.text_pressed = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			select.text_normal_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			select.text_hover_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			select.text_pressed_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			select.padding = (Vec2) (Nuklear.Vec2z((float) (2.0f), (float) (2.0f)));
			select.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			select.userdata = (Handle) (Nuklear.HandlePtr(null));
			select.rounding = (float) (0.0f);
			select.draw_begin = null;
			select.draw_end = null;
			slider = style.slider;

			slider.normal = (StyleItem) (Nuklear.StyleItemHide());
			slider.hover = (StyleItem) (Nuklear.StyleItemHide());
			slider.active = (StyleItem) (Nuklear.StyleItemHide());
			slider.bar_normal = (Color) (table[Nuklear.NK_COLOR_SLIDER]);
			slider.bar_hover = (Color) (table[Nuklear.NK_COLOR_SLIDER]);
			slider.bar_active = (Color) (table[Nuklear.NK_COLOR_SLIDER]);
			slider.bar_filled = (Color) (table[Nuklear.NK_COLOR_SLIDER_CURSOR]);
			slider.cursor_normal = (StyleItem) (table[Nuklear.NK_COLOR_SLIDER_CURSOR].StyleItemColor());
			slider.cursor_hover = (StyleItem) (table[Nuklear.NK_COLOR_SLIDER_CURSOR_HOVER].StyleItemColor());
			slider.cursor_active = (StyleItem) (table[Nuklear.NK_COLOR_SLIDER_CURSOR_ACTIVE].StyleItemColor());
			slider.inc_symbol = (int) (Nuklear.NK_SYMBOL_TRIANGLE_RIGHT);
			slider.dec_symbol = (int) (Nuklear.NK_SYMBOL_TRIANGLE_LEFT);
			slider.cursor_size = (Vec2) (Nuklear.Vec2z((float) (16), (float) (16)));
			slider.padding = (Vec2) (Nuklear.Vec2z((float) (2), (float) (2)));
			slider.spacing = (Vec2) (Nuklear.Vec2z((float) (2), (float) (2)));
			slider.userdata = (Handle) (Nuklear.HandlePtr(null));
			slider.show_buttons = (int) (Nuklear.nk_false);
			slider.bar_height = (float) (8);
			slider.rounding = (float) (0);
			slider.draw_begin = null;
			slider.draw_end = null;
			button = style.slider.inc_button;
			button.normal = (StyleItem) (Nuklear.Rgb((int) (40), (int) (40), (int) (40)).StyleItemColor());
			button.hover = (StyleItem) (Nuklear.Rgb((int) (42), (int) (42), (int) (42)).StyleItemColor());
			button.active = (StyleItem) (Nuklear.Rgb((int) (44), (int) (44), (int) (44)).StyleItemColor());
			button.border_color = (Color) (Nuklear.Rgb((int) (65), (int) (65), (int) (65)));
			button.text_background = (Color) (Nuklear.Rgb((int) (40), (int) (40), (int) (40)));
			button.text_normal = (Color) (Nuklear.Rgb((int) (175), (int) (175), (int) (175)));
			button.text_hover = (Color) (Nuklear.Rgb((int) (175), (int) (175), (int) (175)));
			button.text_active = (Color) (Nuklear.Rgb((int) (175), (int) (175), (int) (175)));
			button.padding = (Vec2) (Nuklear.Vec2z((float) (8.0f), (float) (8.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (1.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			style.slider.dec_button = (StyleButton) (style.slider.inc_button);
			prog = style.progress;

			prog.normal = (StyleItem) (table[Nuklear.NK_COLOR_SLIDER].StyleItemColor());
			prog.hover = (StyleItem) (table[Nuklear.NK_COLOR_SLIDER].StyleItemColor());
			prog.active = (StyleItem) (table[Nuklear.NK_COLOR_SLIDER].StyleItemColor());
			prog.cursor_normal = (StyleItem) (table[Nuklear.NK_COLOR_SLIDER_CURSOR].StyleItemColor());
			prog.cursor_hover = (StyleItem) (table[Nuklear.NK_COLOR_SLIDER_CURSOR_HOVER].StyleItemColor());
			prog.cursor_active = (StyleItem) (table[Nuklear.NK_COLOR_SLIDER_CURSOR_ACTIVE].StyleItemColor());
			prog.border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			prog.cursor_border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			prog.userdata = (Handle) (Nuklear.HandlePtr(null));
			prog.padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			prog.rounding = (float) (0);
			prog.border = (float) (0);
			prog.cursor_rounding = (float) (0);
			prog.cursor_border = (float) (0);
			prog.draw_begin = null;
			prog.draw_end = null;
			scroll = style.scrollh;

			scroll.normal = (StyleItem) (table[Nuklear.NK_COLOR_SCROLLBAR].StyleItemColor());
			scroll.hover = (StyleItem) (table[Nuklear.NK_COLOR_SCROLLBAR].StyleItemColor());
			scroll.active = (StyleItem) (table[Nuklear.NK_COLOR_SCROLLBAR].StyleItemColor());
			scroll.cursor_normal = (StyleItem) (table[Nuklear.NK_COLOR_SCROLLBAR_CURSOR].StyleItemColor());
			scroll.cursor_hover = (StyleItem) (table[Nuklear.NK_COLOR_SCROLLBAR_CURSOR_HOVER].StyleItemColor());
			scroll.cursor_active = (StyleItem) (table[Nuklear.NK_COLOR_SCROLLBAR_CURSOR_ACTIVE].StyleItemColor());
			scroll.dec_symbol = (int) (Nuklear.NK_SYMBOL_CIRCLE_SOLID);
			scroll.inc_symbol = (int) (Nuklear.NK_SYMBOL_CIRCLE_SOLID);
			scroll.userdata = (Handle) (Nuklear.HandlePtr(null));
			scroll.border_color = (Color) (table[Nuklear.NK_COLOR_SCROLLBAR]);
			scroll.cursor_border_color = (Color) (table[Nuklear.NK_COLOR_SCROLLBAR]);
			scroll.padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			scroll.show_buttons = (int) (Nuklear.nk_false);
			scroll.border = (float) (0);
			scroll.rounding = (float) (0);
			scroll.border_cursor = (float) (0);
			scroll.rounding_cursor = (float) (0);
			scroll.draw_begin = null;
			scroll.draw_end = null;
			style.scrollv = (StyleScrollbar) (style.scrollh);
			button = style.scrollh.inc_button;
			button.normal = (StyleItem) (Nuklear.Rgb((int) (40), (int) (40), (int) (40)).StyleItemColor());
			button.hover = (StyleItem) (Nuklear.Rgb((int) (42), (int) (42), (int) (42)).StyleItemColor());
			button.active = (StyleItem) (Nuklear.Rgb((int) (44), (int) (44), (int) (44)).StyleItemColor());
			button.border_color = (Color) (Nuklear.Rgb((int) (65), (int) (65), (int) (65)));
			button.text_background = (Color) (Nuklear.Rgb((int) (40), (int) (40), (int) (40)));
			button.text_normal = (Color) (Nuklear.Rgb((int) (175), (int) (175), (int) (175)));
			button.text_hover = (Color) (Nuklear.Rgb((int) (175), (int) (175), (int) (175)));
			button.text_active = (Color) (Nuklear.Rgb((int) (175), (int) (175), (int) (175)));
			button.padding = (Vec2) (Nuklear.Vec2z((float) (4.0f), (float) (4.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (1.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			style.scrollh.dec_button = (StyleButton) (style.scrollh.inc_button);
			style.scrollv.inc_button = (StyleButton) (style.scrollh.inc_button);
			style.scrollv.dec_button = (StyleButton) (style.scrollh.inc_button);
			edit = style.edit;

			edit.normal = (StyleItem) (table[Nuklear.NK_COLOR_EDIT].StyleItemColor());
			edit.hover = (StyleItem) (table[Nuklear.NK_COLOR_EDIT].StyleItemColor());
			edit.active = (StyleItem) (table[Nuklear.NK_COLOR_EDIT].StyleItemColor());
			edit.cursor_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.cursor_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.cursor_text_normal = (Color) (table[Nuklear.NK_COLOR_EDIT]);
			edit.cursor_text_hover = (Color) (table[Nuklear.NK_COLOR_EDIT]);
			edit.border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			edit.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.selected_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.selected_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.selected_text_normal = (Color) (table[Nuklear.NK_COLOR_EDIT]);
			edit.selected_text_hover = (Color) (table[Nuklear.NK_COLOR_EDIT]);
			edit.scrollbar_size = (Vec2) (Nuklear.Vec2z((float) (10), (float) (10)));
			edit.scrollbar = (StyleScrollbar) (style.scrollv);
			edit.padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			edit.row_padding = (float) (2);
			edit.cursor_size = (float) (4);
			edit.border = (float) (1);
			edit.rounding = (float) (0);
			property = style.property;

			property.normal = (StyleItem) (table[Nuklear.NK_COLOR_PROPERTY].StyleItemColor());
			property.hover = (StyleItem) (table[Nuklear.NK_COLOR_PROPERTY].StyleItemColor());
			property.active = (StyleItem) (table[Nuklear.NK_COLOR_PROPERTY].StyleItemColor());
			property.border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			property.label_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			property.label_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			property.label_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			property.sym_left = (int) (Nuklear.NK_SYMBOL_TRIANGLE_LEFT);
			property.sym_right = (int) (Nuklear.NK_SYMBOL_TRIANGLE_RIGHT);
			property.userdata = (Handle) (Nuklear.HandlePtr(null));
			property.padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			property.border = (float) (1);
			property.rounding = (float) (10);
			property.draw_begin = null;
			property.draw_end = null;
			button = style.property.dec_button;

			button.normal = (StyleItem) (table[Nuklear.NK_COLOR_PROPERTY].StyleItemColor());
			button.hover = (StyleItem) (table[Nuklear.NK_COLOR_PROPERTY].StyleItemColor());
			button.active = (StyleItem) (table[Nuklear.NK_COLOR_PROPERTY].StyleItemColor());
			button.border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (Color) (table[Nuklear.NK_COLOR_PROPERTY]);
			button.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			style.property.inc_button = (StyleButton) (style.property.dec_button);
			edit = style.property.edit;

			edit.normal = (StyleItem) (table[Nuklear.NK_COLOR_PROPERTY].StyleItemColor());
			edit.hover = (StyleItem) (table[Nuklear.NK_COLOR_PROPERTY].StyleItemColor());
			edit.active = (StyleItem) (table[Nuklear.NK_COLOR_PROPERTY].StyleItemColor());
			edit.border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			edit.cursor_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.cursor_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.cursor_text_normal = (Color) (table[Nuklear.NK_COLOR_EDIT]);
			edit.cursor_text_hover = (Color) (table[Nuklear.NK_COLOR_EDIT]);
			edit.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.selected_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.selected_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			edit.selected_text_normal = (Color) (table[Nuklear.NK_COLOR_EDIT]);
			edit.selected_text_hover = (Color) (table[Nuklear.NK_COLOR_EDIT]);
			edit.padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			edit.cursor_size = (float) (8);
			edit.border = (float) (0);
			edit.rounding = (float) (0);
			chart = style.chart;

			chart.background = (StyleItem) (table[Nuklear.NK_COLOR_CHART].StyleItemColor());
			chart.border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			chart.selected_color = (Color) (table[Nuklear.NK_COLOR_CHART_COLOR_HIGHLIGHT]);
			chart.color = (Color) (table[Nuklear.NK_COLOR_CHART_COLOR]);
			chart.padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			chart.border = (float) (0);
			chart.rounding = (float) (0);
			combo = style.combo;
			combo.normal = (StyleItem) (table[Nuklear.NK_COLOR_COMBO].StyleItemColor());
			combo.hover = (StyleItem) (table[Nuklear.NK_COLOR_COMBO].StyleItemColor());
			combo.active = (StyleItem) (table[Nuklear.NK_COLOR_COMBO].StyleItemColor());
			combo.border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			combo.label_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			combo.label_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			combo.label_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			combo.sym_normal = (int) (Nuklear.NK_SYMBOL_TRIANGLE_DOWN);
			combo.sym_hover = (int) (Nuklear.NK_SYMBOL_TRIANGLE_DOWN);
			combo.sym_active = (int) (Nuklear.NK_SYMBOL_TRIANGLE_DOWN);
			combo.content_padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			combo.button_padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (4)));
			combo.spacing = (Vec2) (Nuklear.Vec2z((float) (4), (float) (0)));
			combo.border = (float) (1);
			combo.rounding = (float) (0);
			button = style.combo.button;

			button.normal = (StyleItem) (table[Nuklear.NK_COLOR_COMBO].StyleItemColor());
			button.hover = (StyleItem) (table[Nuklear.NK_COLOR_COMBO].StyleItemColor());
			button.active = (StyleItem) (table[Nuklear.NK_COLOR_COMBO].StyleItemColor());
			button.border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (Color) (table[Nuklear.NK_COLOR_COMBO]);
			button.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.padding = (Vec2) (Nuklear.Vec2z((float) (2.0f), (float) (2.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			tab = style.tab;
			tab.background = (StyleItem) (table[Nuklear.NK_COLOR_TAB_HEADER].StyleItemColor());
			tab.border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			tab.text = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			tab.sym_minimize = (int) (Nuklear.NK_SYMBOL_TRIANGLE_RIGHT);
			tab.sym_maximize = (int) (Nuklear.NK_SYMBOL_TRIANGLE_DOWN);
			tab.padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			tab.spacing = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			tab.indent = (float) (10.0f);
			tab.border = (float) (1);
			tab.rounding = (float) (0);
			button = style.tab.tab_minimize_button;

			button.normal = (StyleItem) (table[Nuklear.NK_COLOR_TAB_HEADER].StyleItemColor());
			button.hover = (StyleItem) (table[Nuklear.NK_COLOR_TAB_HEADER].StyleItemColor());
			button.active = (StyleItem) (table[Nuklear.NK_COLOR_TAB_HEADER].StyleItemColor());
			button.border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (Color) (table[Nuklear.NK_COLOR_TAB_HEADER]);
			button.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.padding = (Vec2) (Nuklear.Vec2z((float) (2.0f), (float) (2.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			style.tab.tab_maximize_button = (StyleButton) (button);
			button = style.tab.node_minimize_button;

			button.normal = (StyleItem) (table[Nuklear.NK_COLOR_WINDOW].StyleItemColor());
			button.hover = (StyleItem) (table[Nuklear.NK_COLOR_WINDOW].StyleItemColor());
			button.active = (StyleItem) (table[Nuklear.NK_COLOR_WINDOW].StyleItemColor());
			button.border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (Color) (table[Nuklear.NK_COLOR_TAB_HEADER]);
			button.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.padding = (Vec2) (Nuklear.Vec2z((float) (2.0f), (float) (2.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			style.tab.node_maximize_button = (StyleButton) (button);
			win = style.window;
			win.header.align = (int) (Nuklear.NK_HEADER_RIGHT);
			win.header.close_symbol = (int) (Nuklear.NK_SYMBOL_X);
			win.header.minimize_symbol = (int) (Nuklear.NK_SYMBOL_MINUS);
			win.header.maximize_symbol = (int) (Nuklear.NK_SYMBOL_PLUS);
			win.header.normal = (StyleItem) (table[Nuklear.NK_COLOR_HEADER].StyleItemColor());
			win.header.hover = (StyleItem) (table[Nuklear.NK_COLOR_HEADER].StyleItemColor());
			win.header.active = (StyleItem) (table[Nuklear.NK_COLOR_HEADER].StyleItemColor());
			win.header.label_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			win.header.label_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			win.header.label_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			win.header.label_padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			win.header.padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			win.header.spacing = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			button = style.window.header.close_button;

			button.normal = (StyleItem) (table[Nuklear.NK_COLOR_HEADER].StyleItemColor());
			button.hover = (StyleItem) (table[Nuklear.NK_COLOR_HEADER].StyleItemColor());
			button.active = (StyleItem) (table[Nuklear.NK_COLOR_HEADER].StyleItemColor());
			button.border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (Color) (table[Nuklear.NK_COLOR_HEADER]);
			button.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			button = style.window.header.minimize_button;

			button.normal = (StyleItem) (table[Nuklear.NK_COLOR_HEADER].StyleItemColor());
			button.hover = (StyleItem) (table[Nuklear.NK_COLOR_HEADER].StyleItemColor());
			button.active = (StyleItem) (table[Nuklear.NK_COLOR_HEADER].StyleItemColor());
			button.border_color = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (Color) (table[Nuklear.NK_COLOR_HEADER]);
			button.text_normal = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_hover = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.text_active = (Color) (table[Nuklear.NK_COLOR_TEXT]);
			button.padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.touch_padding = (Vec2) (Nuklear.Vec2z((float) (0.0f), (float) (0.0f)));
			button.userdata = (Handle) (Nuklear.HandlePtr(null));
			button.text_alignment = (uint) (Nuklear.NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			win.background = (Color) (table[Nuklear.NK_COLOR_WINDOW]);
			win.fixed_background = (StyleItem) (table[Nuklear.NK_COLOR_WINDOW].StyleItemColor());
			win.border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			win.popup_border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			win.combo_border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			win.contextual_border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			win.menu_border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			win.group_border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			win.tooltip_border_color = (Color) (table[Nuklear.NK_COLOR_BORDER]);
			win.scaler = (StyleItem) (table[Nuklear.NK_COLOR_TEXT].StyleItemColor());
			win.rounding = (float) (0.0f);
			win.spacing = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			win.scrollbar_size = (Vec2) (Nuklear.Vec2z((float) (10), (float) (10)));
			win.min_size = (Vec2) (Nuklear.Vec2z((float) (64), (float) (64)));
			win.combo_border = (float) (1.0f);
			win.contextual_border = (float) (1.0f);
			win.menu_border = (float) (1.0f);
			win.group_border = (float) (1.0f);
			win.tooltip_border = (float) (1.0f);
			win.popup_border = (float) (1.0f);
			win.border = (float) (2.0f);
			win.min_row_height_padding = (float) (8);
			win.padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			win.group_padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			win.popup_padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			win.combo_padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			win.contextual_padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			win.menu_padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
			win.tooltip_padding = (Vec2) (Nuklear.Vec2z((float) (4), (float) (4)));
		}

		public void StyleSetFont(UserFont font)
		{
			Style style;
			if (this == null) return;
			style = this.style;
			style.font = font;
			this.stacks.fonts.head = (int) (0);
			if ((this.current) != null) LayoutResetMinRowHeight();
		}

		public int StylePushFont(UserFont font)
		{
			ConfigStackUserFont font_stack;
			ConfigStackUserFontElement element;
			if (this == null) return (int) (0);
			font_stack = this.stacks.fonts;
			if ((font_stack.head) >= (int) font_stack.elements.Length) return (int) (0);
			element = font_stack.elements[font_stack.head++];
			element.address = this.style.font;
			element.old_value = this.style.font;
			this.style.font = font;
			return (int) (1);
		}

		public int StylePopFont()
		{
			ConfigStackUserFont font_stack;
			ConfigStackUserFontElement element;
			if (this == null) return (int) (0);
			font_stack = this.stacks.fonts;
			if ((font_stack.head) < (1)) return (int) (0);
			element = font_stack.elements[--font_stack.head];
			element.address = element.old_value;
			return (int) (1);
		}

		public int StylePushStyleItem(StyleItem address, StyleItem value)
		{
			ConfigStackStyleItem type_stack;
			ConfigStackStyleItemElement element;
			if (this == null) return (int) (0);
			type_stack = this.stacks.style_items;
			if ((type_stack.head) >= (int) type_stack.elements.Length) return (int) (0);
			element = type_stack.elements[(type_stack.head++)];
			element.address = address;
			element.old_value = (StyleItem) (address);
			address = (StyleItem) (value);
			return (int) (1);
		}

		public int StylePushFloat(float* address, float value)
		{
			ConfigStackFloat type_stack;
			ConfigStackFloatElement element;
			if (this == null) return (int) (0);
			type_stack = this.stacks.floats;
			if ((type_stack.head) >= (int) type_stack.elements.Length) return (int) (0);
			element = type_stack.elements[(type_stack.head++)];
			element.address = address;
			element.old_value = (float) (*address);
			*address = (float) (value);
			return (int) (1);
		}

		public int StylePushVec2(Vec2* address, Vec2 value)
		{
			ConfigStackVec2 type_stack;
			ConfigStackVec2Element element;
			if (this == null) return (int) (0);
			type_stack = this.stacks.vectors;
			if ((type_stack.head) >= (int) type_stack.elements.Length) return (int) (0);
			element = type_stack.elements[(type_stack.head++)];
			element.address = address;
			element.old_value = (Vec2) (*address);
			*address = (Vec2) (value);
			return (int) (1);
		}

		public int StylePushFlags(uint* address, uint value)
		{
			ConfigStackFlags type_stack;
			ConfigStackFlagsElement element;
			if (this == null) return (int) (0);
			type_stack = this.stacks.flags;
			if ((type_stack.head) >= (int) type_stack.elements.Length) return (int) (0);
			element = type_stack.elements[(type_stack.head++)];
			element.address = address;
			element.old_value = (uint) (*address);
			*address = (uint) (value);
			return (int) (1);
		}

		public int StylePushColor(Color* address, Color value)
		{
			ConfigStackColor type_stack;
			ConfigStackColorElement element;
			if (this == null) return (int) (0);
			type_stack = this.stacks.colors;
			if ((type_stack.head) >= (int) type_stack.elements.Length) return (int) (0);
			element = type_stack.elements[(type_stack.head++)];
			element.address = address;
			element.old_value = (Color) (*address);
			*address = (Color) (value);
			return (int) (1);
		}

		public int StylePopStyleItem()
		{
			ConfigStackStyleItem type_stack;
			ConfigStackStyleItemElement element;
			if (this == null) return (int) (0);
			type_stack = this.stacks.style_items;
			if ((type_stack.head) < (1)) return (int) (0);
			element = type_stack.elements[(--type_stack.head)];
			element.address = (StyleItem) (element.old_value);
			return (int) (1);
		}

		public int StylePopFloat()
		{
			ConfigStackFloat type_stack;
			ConfigStackFloatElement element;
			if (this == null) return (int) (0);
			type_stack = this.stacks.floats;
			if ((type_stack.head) < (1)) return (int) (0);
			element = type_stack.elements[(--type_stack.head)];
			*element.address = (float) (element.old_value);
			return (int) (1);
		}

		public int StylePopVec2()
		{
			ConfigStackVec2 type_stack;
			ConfigStackVec2Element element;
			if (this == null) return (int) (0);
			type_stack = this.stacks.vectors;
			if ((type_stack.head) < (1)) return (int) (0);
			element = type_stack.elements[(--type_stack.head)];
			*element.address = (Vec2) (element.old_value);
			return (int) (1);
		}

		public int StylePopFlags()
		{
			ConfigStackFlags type_stack;
			ConfigStackFlagsElement element;
			if (this == null) return (int) (0);
			type_stack = this.stacks.flags;
			if ((type_stack.head) < (1)) return (int) (0);
			element = type_stack.elements[(--type_stack.head)];
			*element.address = (uint) (element.old_value);
			return (int) (1);
		}

		public int StylePopColor()
		{
			ConfigStackColor type_stack;
			ConfigStackColorElement element;
			if (this == null) return (int) (0);
			type_stack = this.stacks.colors;
			if ((type_stack.head) < (1)) return (int) (0);
			element = type_stack.elements[(--type_stack.head)];
			*element.address = (Color) (element.old_value);
			return (int) (1);
		}

		public int StyleSetCursor(int c)
		{
			Style style;
			if (this == null) return (int) (0);
			style = this.style;
			if ((style.cursors[c]) != null)
			{
				style.cursor_active = style.cursors[c];
				return (int) (1);
			}

			return (int) (0);
		}

		public void StyleShowCursor()
		{
			this.style.cursor_visible = (int) (Nuklear.nk_true);
		}

		public void StyleHideCursor()
		{
			this.style.cursor_visible = (int) (Nuklear.nk_false);
		}

		public void StyleLoadCursor(int cursor, Cursor c)
		{
			Style style;
			if (this == null) return;
			style = this.style;
			style.cursors[cursor] = c;
		}

		public void StyleLoadAllCursors(Cursor[] cursors)
		{
			int i = (int) (0);
			Style style;
			if (this == null) return;
			style = this.style;
			for (i = (int) (0); (i) < (Nuklear.NK_CURSOR_COUNT); ++i)
			{
				style.cursors[i] = cursors[i];
			}
			style.cursor_visible = (int) (Nuklear.nk_true);
		}

		public void Setup(UserFont font)
		{
			if (this == null) return;

			StyleDefault();
			this.seq = (uint) (1);
			if ((font) != null) this.style.font = font;
			this.draw_list.Init();
		}

		public void SetUserData(Handle handle)
		{
			if (this == null) return;
			this.userdata = (Handle) (handle);
			if ((this.current) != null) this.current.buffer.userdata = (Handle) (handle);
		}

		public void Clear()
		{
			Window iter;
			Window next;
			if (this == null) return;
			if ((this.use_pool) != 0) this.memory.Clear();
			else this.memory.Reset((int) (Nuklear.NK_BUFFER_FRONT));
			this.build = (int) (0);
			this.memory.calls = (ulong) (0);
			this.last_widget_state = (uint) (0);
			this.style.cursor_active = this.style.cursors[Nuklear.NK_CURSOR_ARROW];
			Nuklear.Memset(this.overlay, (int) (0), (ulong) (sizeof ((this.overlay))))
			;
			this.draw_list.Clear();
			iter = this.begin;
			while ((iter) != null)
			{
				if ((((iter.flags & Nuklear.NK_WINDOW_MINIMIZED) != 0) && ((iter.flags & Nuklear.NK_WINDOW_CLOSED) == 0)) &&
				    ((iter.seq) == (this.seq)))
				{
					iter = iter.next;
					continue;
				}
				if ((((iter.flags & Nuklear.NK_WINDOW_HIDDEN) != 0) || ((iter.flags & Nuklear.NK_WINDOW_CLOSED) != 0)) &&
				    ((iter) == (this.active)))
				{
					this.active = iter.prev;
					this.end = iter.prev;
					if ((this.active) != null) this.active.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_ROM));
				}
				if (((iter.popup.win) != null) && (iter.popup.win.seq != this.seq))
				{
					FreeWindow(iter.popup.win);
					iter.popup.win = null;
				}
				{
					Table n;
					Table it = iter.tables;
					while ((it) != null)
					{
						n = it.next;
						if (it.seq != this.seq)
						{
							iter.RemoveTable(it);
							if ((it) == (iter.tables)) iter.tables = n;
						}
						it = n;
					}
				}
				if ((iter.seq != this.seq) || ((iter.flags & Nuklear.NK_WINDOW_CLOSED) != 0))
				{
					next = iter.next;
					RemoveWindow(iter);
					FreeWindow(iter);
					iter = next;
				}
				else iter = iter.next;
			}
			this.seq++;
		}

		public void StartBuffer(CommandBuffer buffer)
		{
			if ((buffer == null)) return;
			buffer.begin = (ulong) (this.memory.allocated);
			buffer.end = (ulong) (buffer.begin);
			buffer.last = (ulong) (buffer.begin);
			buffer.clip = (Rect) (Nuklear.nk_null_rect);
		}

		public void Start(Window win)
		{
			StartBuffer(win.buffer);
		}

		public void StartPopup(Window win)
		{
			PopupBuffer buf;
			if ((win == null)) return;
			buf = win.popup.buf;
			buf.begin = (ulong) (win.buffer.end);
			buf.end = (ulong) (win.buffer.end);
			buf.parent = (ulong) (win.buffer.last);
			buf.last = (ulong) (buf.begin);
			buf.active = (int) (Nuklear.nk_true);
		}

		public void FinishPopup(Window win)
		{
			PopupBuffer buf;
			if ((win == null)) return;
			buf = win.popup.buf;
			buf.last = (ulong) (win.buffer.last);
			buf.end = (ulong) (win.buffer.end);
		}

		public void FinishBuffer(CommandBuffer buffer)
		{
			if ((buffer == null)) return;
			buffer.end = (ulong) (this.memory.allocated);
		}

		public void Finish(Window win)
		{
			PopupBuffer buf;
			Command* parent_last;
			void* memory;
			if ((win == null)) return;
			FinishBuffer(win.buffer);
			if (win.popup.buf.active == 0) return;
			buf = win.popup.buf;
			memory = this.memory.memory.ptr;
			parent_last = ((Command*) ((void*) ((byte*) (memory) + (buf.parent))));
			parent_last->next = (ulong) (buf.end);
		}

		public int PanelBegin(char* title, int panel_type)
		{
			Input _in_;
			Window win;
			Panel layout;
			CommandBuffer _out_;
			Style style;
			UserFont font;
			Vec2 scrollbar_size = new Vec2();
			Vec2 panel_padding = new Vec2();
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			Nuklear.Zero(this.current.layout, (ulong) (sizeof ((this.current.layout))))
			;
			if (((this.current.flags & Nuklear.NK_WINDOW_HIDDEN) != 0) || ((this.current.flags & Nuklear.NK_WINDOW_CLOSED) != 0))
			{
				Nuklear.Zero(this.current.layout, (ulong) (sizeof (Panel)));
				this.current.layout.type = (int) (panel_type);
				return (int) (0);
			}

			style = this.style;
			font = style.font;
			win = this.current;
			layout = win.layout;
			_out_ = win.buffer;
			_in_ = (win.flags & Nuklear.NK_WINDOW_NO_INPUT) != 0 ? null : this.input;
			win.buffer.userdata = (Handle) (this.userdata);
			scrollbar_size = (Vec2) (style.window.scrollbar_size);
			panel_padding = (Vec2) (style.PanelGetPadding((int) (panel_type)));
			if (((win.flags & Nuklear.NK_WINDOW_MOVABLE) != 0) && ((win.flags & Nuklear.NK_WINDOW_ROM) == 0))
			{
				int left_mouse_down;
				int left_mouse_click_in_cursor;
				Rect header = new Rect();
				header.x = (float) (win.bounds.x);
				header.y = (float) (win.bounds.y);
				header.w = (float) (win.bounds.w);
				if ((Nuklear.PanelHasHeader((uint) (win.flags), title)) != 0)
				{
					header.h = (float) (font.height + 2.0f*style.window.header.padding.y);
					header.h += (float) (2.0f*style.window.header.label_padding.y);
				}
				else header.h = (float) (panel_padding.y);
				left_mouse_down = (int) (((MouseButton*) _in_.mouse.buttons + Nuklear.NK_BUTTON_LEFT)->down);
				left_mouse_click_in_cursor =
					(int) (_in_.HasMouseClickDownInRect((int) (Nuklear.NK_BUTTON_LEFT), (Rect) (header), (int) (Nuklear.nk_true)));
				if (((left_mouse_down) != 0) && ((left_mouse_click_in_cursor) != 0))
				{
					win.bounds.x = (float) (win.bounds.x + _in_.mouse.delta.x);
					win.bounds.y = (float) (win.bounds.y + _in_.mouse.delta.y);
					((MouseButton*) _in_.mouse.buttons + Nuklear.NK_BUTTON_LEFT)->clicked_pos.x += (float) (_in_.mouse.delta.x);
					((MouseButton*) _in_.mouse.buttons + Nuklear.NK_BUTTON_LEFT)->clicked_pos.y += (float) (_in_.mouse.delta.y);
					this.style.cursor_active = this.style.cursors[Nuklear.NK_CURSOR_MOVE];
				}
			}

			layout.type = (int) (panel_type);
			layout.flags = (uint) (win.flags);
			layout.bounds = (Rect) (win.bounds);
			layout.bounds.x += (float) (panel_padding.x);
			layout.bounds.w -= (float) (2*panel_padding.x);
			if ((win.flags & Nuklear.NK_WINDOW_BORDER) != 0)
			{
				layout.border = (float) (style.PanelGetBorder((uint) (win.flags), (int) (panel_type)));
				layout.bounds = (Rect) (layout.bounds.ShrinkRectz((float) (layout.border)));
			}
			else layout.border = (float) (0);
			layout.at_y = (float) (layout.bounds.y);
			layout.at_x = (float) (layout.bounds.x);
			layout.max_x = (float) (0);
			layout.header_height = (float) (0);
			layout.footer_height = (float) (0);
			LayoutResetMinRowHeight();
			layout.row.index = (int) (0);
			layout.row.columns = (int) (0);
			layout.row.ratio = null;
			layout.row.item_width = (float) (0);
			layout.row.tree_depth = (int) (0);
			layout.row.height = (float) (panel_padding.y);
			layout.has_scrolling = (uint) (Nuklear.nk_true);
			if ((win.flags & Nuklear.NK_WINDOW_NO_SCROLLBAR) == 0) layout.bounds.w -= (float) (scrollbar_size.x);
			if (Nuklear.PanelIsNonblock((int) (panel_type)) == 0)
			{
				layout.footer_height = (float) (0);
				if (((win.flags & Nuklear.NK_WINDOW_NO_SCROLLBAR) == 0) || ((win.flags & Nuklear.NK_WINDOW_SCALABLE) != 0))
					layout.footer_height = (float) (scrollbar_size.y);
				layout.bounds.h -= (float) (layout.footer_height);
			}

			if ((Nuklear.PanelHasHeader((uint) (win.flags), title)) != 0)
			{
				Text text = new Text();
				Rect header = new Rect();
				StyleItem background = null;
				header.x = (float) (win.bounds.x);
				header.y = (float) (win.bounds.y);
				header.w = (float) (win.bounds.w);
				header.h = (float) (font.height + 2.0f*style.window.header.padding.y);
				header.h += (float) (2.0f*style.window.header.label_padding.y);
				layout.header_height = (float) (header.h);
				layout.bounds.y += (float) (header.h);
				layout.bounds.h -= (float) (header.h);
				layout.at_y += (float) (header.h);
				if ((this.active) == (win))
				{
					background = style.window.header.active;
					text.text = (Color) (style.window.header.label_active);
				}
				else if ((this.input.IsMouseHoveringRect((Rect) (header))) != 0)
				{
					background = style.window.header.hover;
					text.text = (Color) (style.window.header.label_hover);
				}
				else
				{
					background = style.window.header.normal;
					text.text = (Color) (style.window.header.label_normal);
				}
				header.h += (float) (1.0f);
				if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
				{
					text.background = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
					win.buffer.DrawImage((Rect) (header), background.data.image, (Color) (Nuklear.nk_white));
				}
				else
				{
					text.background = (Color) (background.data.color);
					_out_.FillRect((Rect) (header), (float) (0), (Color) (background.data.color));
				}
				{
					Rect button = new Rect();
					button.y = (float) (header.y + style.window.header.padding.y);
					button.h = (float) (header.h - 2*style.window.header.padding.y);
					button.w = (float) (button.h);
					if ((win.flags & Nuklear.NK_WINDOW_CLOSABLE) != 0)
					{
						uint ws = (uint) (0);
						if ((style.window.header.align) == (Nuklear.NK_HEADER_RIGHT))
						{
							button.x = (float) ((header.w + header.x) - (button.w + style.window.header.padding.x));
							header.w -= (float) (button.w + style.window.header.spacing.x + style.window.header.padding.x);
						}
						else
						{
							button.x = (float) (header.x + style.window.header.padding.x);
							header.x += (float) (button.w + style.window.header.spacing.x + style.window.header.padding.x);
						}
						if (
							((Nuklear.DoButtonSymbol(ref ws, win.buffer, (Rect) (button), (int) (style.window.header.close_symbol),
								(int) (Nuklear.NK_BUTTON_DEFAULT), style.window.header.close_button, _in_, style.font)) != 0) &&
							((win.flags & Nuklear.NK_WINDOW_ROM) == 0))
						{
							layout.flags |= (uint) (Nuklear.NK_WINDOW_HIDDEN);
							layout.flags &= ((uint) (~(uint) Nuklear.NK_WINDOW_MINIMIZED));
						}
					}
					if ((win.flags & Nuklear.NK_WINDOW_MINIMIZABLE) != 0)
					{
						uint ws = (uint) (0);
						if ((style.window.header.align) == (Nuklear.NK_HEADER_RIGHT))
						{
							button.x = (float) ((header.w + header.x) - button.w);
							if ((win.flags & Nuklear.NK_WINDOW_CLOSABLE) == 0)
							{
								button.x -= (float) (style.window.header.padding.x);
								header.w -= (float) (style.window.header.padding.x);
							}
							header.w -= (float) (button.w + style.window.header.spacing.x);
						}
						else
						{
							button.x = (float) (header.x);
							header.x += (float) (button.w + style.window.header.spacing.x + style.window.header.padding.x);
						}
						if (
							((Nuklear.DoButtonSymbol(ref ws, win.buffer, (Rect) (button),
								(int)
									((layout.flags & Nuklear.NK_WINDOW_MINIMIZED) != 0
										? style.window.header.maximize_symbol
										: style.window.header.minimize_symbol), (int) (Nuklear.NK_BUTTON_DEFAULT), style.window.header.minimize_button,
								_in_, style.font)) != 0) && ((win.flags & Nuklear.NK_WINDOW_ROM) == 0))
							layout.flags =
								(uint)
									((layout.flags & Nuklear.NK_WINDOW_MINIMIZED) != 0
										? layout.flags & (uint) (~(uint) Nuklear.NK_WINDOW_MINIMIZED)
										: layout.flags | Nuklear.NK_WINDOW_MINIMIZED);
					}
				}
				{
					int text_len = (int) (Nuklear.Strlen(title));
					Rect label = new Rect();
					float t = (float) (font.width((Handle) (font.userdata), (float) (font.height), title, (int) (text_len)));
					text.padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
					label.x = (float) (header.x + style.window.header.padding.x);
					label.x += (float) (style.window.header.label_padding.x);
					label.y = (float) (header.y + style.window.header.label_padding.y);
					label.h = (float) (font.height + 2*style.window.header.label_padding.y);
					label.w = (float) (t + 2*style.window.header.spacing.x);
					label.w =
						(float)
							(((label.w) < (header.x + header.w - label.x) ? (label.w) : (header.x + header.w - label.x)) < (0)
								? (0)
								: ((label.w) < (header.x + header.w - label.x) ? (label.w) : (header.x + header.w - label.x)));
					_out_.WidgetText((Rect) (label), title, (int) (text_len), &text, (uint) (Nuklear.NK_TEXT_LEFT), font);
				}
			}

			if (((layout.flags & Nuklear.NK_WINDOW_MINIMIZED) == 0) && ((layout.flags & Nuklear.NK_WINDOW_DYNAMIC) == 0))
			{
				Rect body = new Rect();
				body.x = (float) (win.bounds.x);
				body.w = (float) (win.bounds.w);
				body.y = (float) (win.bounds.y + layout.header_height);
				body.h = (float) (win.bounds.h - layout.header_height);
				if ((style.window.fixed_background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
					_out_.DrawImage((Rect) (body), style.window.fixed_background.data.image, (Color) (Nuklear.nk_white));
				else _out_.FillRect((Rect) (body), (float) (0), (Color) (style.window.fixed_background.data.color));
			}

			{
				Rect clip = new Rect();
				layout.clip = (Rect) (layout.bounds);
				clip.Unify(ref win.buffer.clip, (float) (layout.clip.x), (float) (layout.clip.y),
					(float) (layout.clip.x + layout.clip.w), (float) (layout.clip.y + layout.clip.h));
				_out_.PushScissor((Rect) (clip));
				layout.clip = (Rect) (clip);
			}

			return
				(int)
					(((layout.flags & Nuklear.NK_WINDOW_HIDDEN) == 0) && ((layout.flags & Nuklear.NK_WINDOW_MINIMIZED) == 0) ? 1 : 0);
		}

		public void PanelEnd()
		{
			Input _in_;
			Window window;
			Panel layout;
			Style style;
			CommandBuffer _out_;
			Vec2 scrollbar_size = new Vec2();
			Vec2 panel_padding = new Vec2();
			if (((this.current == null)) || (this.current.layout == null)) return;
			window = this.current;
			layout = window.layout;
			style = this.style;
			_out_ = window.buffer;
			_in_ = (((layout.flags & Nuklear.NK_WINDOW_ROM) != 0) || ((layout.flags & Nuklear.NK_WINDOW_NO_INPUT) != 0))
				? null
				: this.input;
			if (Nuklear.PanelIsSub((int) (layout.type)) == 0) _out_.PushScissor((Rect) (Nuklear.nk_null_rect));
			scrollbar_size = (Vec2) (style.window.scrollbar_size);
			panel_padding = (Vec2) (style.PanelGetPadding((int) (layout.type)));
			layout.at_y += (float) (layout.row.height);
			if (((layout.flags & Nuklear.NK_WINDOW_DYNAMIC) != 0) && ((layout.flags & Nuklear.NK_WINDOW_MINIMIZED) == 0))
			{
				Rect empty_space = new Rect();
				if ((layout.at_y) < (layout.bounds.y + layout.bounds.h)) layout.bounds.h = (float) (layout.at_y - layout.bounds.y);
				empty_space.x = (float) (window.bounds.x);
				empty_space.y = (float) (layout.bounds.y);
				empty_space.h = (float) (panel_padding.y);
				empty_space.w = (float) (window.bounds.w);
				_out_.FillRect((Rect) (empty_space), (float) (0), (Color) (style.window.background));
				empty_space.x = (float) (window.bounds.x);
				empty_space.y = (float) (layout.bounds.y);
				empty_space.w = (float) (panel_padding.x + layout.border);
				empty_space.h = (float) (layout.bounds.h);
				_out_.FillRect((Rect) (empty_space), (float) (0), (Color) (style.window.background));
				empty_space.x = (float) (layout.bounds.x + layout.bounds.w - layout.border);
				empty_space.y = (float) (layout.bounds.y);
				empty_space.w = (float) (panel_padding.x + layout.border);
				empty_space.h = (float) (layout.bounds.h);
				if (((layout.offset.y) == (0)) && ((layout.flags & Nuklear.NK_WINDOW_NO_SCROLLBAR) == 0))
					empty_space.w += (float) (scrollbar_size.x);
				_out_.FillRect((Rect) (empty_space), (float) (0), (Color) (style.window.background));
				if ((layout.offset.x != 0) && ((layout.flags & Nuklear.NK_WINDOW_NO_SCROLLBAR) == 0))
				{
					empty_space.x = (float) (window.bounds.x);
					empty_space.y = (float) (layout.bounds.y + layout.bounds.h);
					empty_space.w = (float) (window.bounds.w);
					empty_space.h = (float) (scrollbar_size.y);
					_out_.FillRect((Rect) (empty_space), (float) (0), (Color) (style.window.background));
				}
			}

			if ((((layout.flags & Nuklear.NK_WINDOW_NO_SCROLLBAR) == 0) && ((layout.flags & Nuklear.NK_WINDOW_MINIMIZED) == 0)) &&
			    ((window.scrollbar_hiding_timer) < (4.0f)))
			{
				Rect scroll = new Rect();
				int scroll_has_scrolling;
				float scroll_target;
				float scroll_offset;
				float scroll_step;
				float scroll_inc;
				if ((Nuklear.PanelIsSub((int) (layout.type))) != 0)
				{
					Window root_window = window;
					Panel root_panel = window.layout;
					while ((root_panel.parent) != null)
					{
						root_panel = root_panel.parent;
					}
					while ((root_window.parent) != null)
					{
						root_window = root_window.parent;
					}
					scroll_has_scrolling = (int) (0);
					if (((root_window) == (this.active)) && ((layout.has_scrolling) != 0))
					{
						if (((_in_.IsMouseHoveringRect((Rect) (layout.bounds))) != 0) &&
						    (!(((((root_panel.clip.x) > (layout.bounds.x + layout.bounds.w)) ||
						         ((root_panel.clip.x + root_panel.clip.w) < (layout.bounds.x))) ||
						        ((root_panel.clip.y) > (layout.bounds.y + layout.bounds.h))) ||
						       ((root_panel.clip.y + root_panel.clip.h) < (layout.bounds.y)))))
						{
							root_panel = window.layout;
							while ((root_panel.parent) != null)
							{
								root_panel.has_scrolling = (uint) (Nuklear.nk_false);
								root_panel = root_panel.parent;
							}
							root_panel.has_scrolling = (uint) (Nuklear.nk_false);
							scroll_has_scrolling = (int) (Nuklear.nk_true);
						}
					}
				}
				else if (Nuklear.PanelIsSub((int) (layout.type)) == 0)
				{
					scroll_has_scrolling = (int) (((window) == (this.active)) && ((layout.has_scrolling) != 0) ? 1 : 0);
					if ((((_in_) != null) && (((_in_.mouse.scroll_delta.y) > (0)) || ((_in_.mouse.scroll_delta.x) > (0)))) &&
					    ((scroll_has_scrolling) != 0)) window.scrolled = (uint) (Nuklear.nk_true);
					else window.scrolled = (uint) (Nuklear.nk_false);
				}
				else scroll_has_scrolling = (int) (Nuklear.nk_false);
				{
					uint state = (uint) (0);
					scroll.x = (float) (layout.bounds.x + layout.bounds.w + panel_padding.x);
					scroll.y = (float) (layout.bounds.y);
					scroll.w = (float) (scrollbar_size.x);
					scroll.h = (float) (layout.bounds.h);
					scroll_offset = ((float) (layout.offset.y));
					scroll_step = (float) (scroll.h*0.10f);
					scroll_inc = (float) (scroll.h*0.01f);
					scroll_target = ((float) ((int) (layout.at_y - scroll.y)));
					scroll_offset =
						(float)
							(Nuklear.DoScrollbarv(ref state, _out_, (Rect) (scroll), (int) (scroll_has_scrolling), (float) (scroll_offset),
								(float) (scroll_target), (float) (scroll_step), (float) (scroll_inc), this.style.scrollv, _in_, style.font));
					layout.offset.y = ((uint) (scroll_offset));
					if (((_in_) != null) && ((scroll_has_scrolling) != 0)) _in_.mouse.scroll_delta.y = (float) (0);
				}
				{
					uint state = (uint) (0);
					scroll.x = (float) (layout.bounds.x);
					scroll.y = (float) (layout.bounds.y + layout.bounds.h);
					scroll.w = (float) (layout.bounds.w);
					scroll.h = (float) (scrollbar_size.y);
					scroll_offset = ((float) (layout.offset.x));
					scroll_target = ((float) ((int) (layout.max_x - scroll.x)));
					scroll_step = (float) (layout.max_x*0.05f);
					scroll_inc = (float) (layout.max_x*0.005f);
					scroll_offset =
						(float)
							(Nuklear.DoScrollbarh(ref state, _out_, (Rect) (scroll), (int) (scroll_has_scrolling), (float) (scroll_offset),
								(float) (scroll_target), (float) (scroll_step), (float) (scroll_inc), this.style.scrollh, _in_, style.font));
					layout.offset.x = ((uint) (scroll_offset));
				}
			}

			if ((window.flags & Nuklear.NK_WINDOW_SCROLL_AUTO_HIDE) != 0)
			{
				int has_input =
					(int)
						(((this.input.mouse.delta.x != 0) || (this.input.mouse.delta.y != 0)) || (this.input.mouse.scroll_delta.y != 0)
							? 1
							: 0);
				int is_window_hovered = (int) (WindowIsHovered());
				int any_item_active = (int) (this.last_widget_state & Nuklear.NK_WIDGET_STATE_MODIFIED);
				if (((has_input == 0) && ((is_window_hovered) != 0)) || ((is_window_hovered == 0) && (any_item_active == 0)))
					window.scrollbar_hiding_timer += (float) (this.delta_time_seconds);
				else window.scrollbar_hiding_timer = (float) (0);
			}
			else window.scrollbar_hiding_timer = (float) (0);
			if ((layout.flags & Nuklear.NK_WINDOW_BORDER) != 0)
			{
				Color border_color = (Color) (style.PanelGetBorderColor((int) (layout.type)));
				float padding_y =
					(float)
						((layout.flags & Nuklear.NK_WINDOW_MINIMIZED) != 0
							? style.window.border + window.bounds.y + layout.header_height
							: (layout.flags & Nuklear.NK_WINDOW_DYNAMIC) != 0
								? layout.bounds.y + layout.bounds.h + layout.footer_height
								: window.bounds.y + window.bounds.h);
				_out_.StrokeLine((float) (window.bounds.x), (float) (window.bounds.y), (float) (window.bounds.x + window.bounds.w),
					(float) (window.bounds.y), (float) (layout.border), (Color) (border_color));
				_out_.StrokeLine((float) (window.bounds.x), (float) (padding_y), (float) (window.bounds.x + window.bounds.w),
					(float) (padding_y), (float) (layout.border), (Color) (border_color));
				_out_.StrokeLine((float) (window.bounds.x + layout.border*0.5f), (float) (window.bounds.y),
					(float) (window.bounds.x + layout.border*0.5f), (float) (padding_y), (float) (layout.border),
					(Color) (border_color));
				_out_.StrokeLine((float) (window.bounds.x + window.bounds.w - layout.border*0.5f), (float) (window.bounds.y),
					(float) (window.bounds.x + window.bounds.w - layout.border*0.5f), (float) (padding_y), (float) (layout.border),
					(Color) (border_color));
			}

			if ((((layout.flags & Nuklear.NK_WINDOW_SCALABLE) != 0) && ((_in_) != null)) &&
			    ((layout.flags & Nuklear.NK_WINDOW_MINIMIZED) == 0))
			{
				Rect scaler = new Rect();
				scaler.w = (float) (scrollbar_size.x);
				scaler.h = (float) (scrollbar_size.y);
				scaler.y = (float) (layout.bounds.y + layout.bounds.h);
				if ((layout.flags & Nuklear.NK_WINDOW_SCALE_LEFT) != 0) scaler.x = (float) (layout.bounds.x - panel_padding.x*0.5f);
				else scaler.x = (float) (layout.bounds.x + layout.bounds.w + panel_padding.x);
				if ((layout.flags & Nuklear.NK_WINDOW_NO_SCROLLBAR) != 0) scaler.x -= (float) (scaler.w);
				{
					StyleItem item = style.window.scaler;
					if ((item.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
						_out_.DrawImage((Rect) (scaler), item.data.image, (Color) (Nuklear.nk_white));
					else
					{
						if ((layout.flags & Nuklear.NK_WINDOW_SCALE_LEFT) != 0)
						{
							_out_.FillTriangle((float) (scaler.x), (float) (scaler.y), (float) (scaler.x), (float) (scaler.y + scaler.h),
								(float) (scaler.x + scaler.w), (float) (scaler.y + scaler.h), (Color) (item.data.color));
						}
						else
						{
							_out_.FillTriangle((float) (scaler.x + scaler.w), (float) (scaler.y), (float) (scaler.x + scaler.w),
								(float) (scaler.y + scaler.h), (float) (scaler.x), (float) (scaler.y + scaler.h), (Color) (item.data.color));
						}
					}
				}
				if ((window.flags & Nuklear.NK_WINDOW_ROM) == 0)
				{
					Vec2 window_size = (Vec2) (style.window.min_size);
					int left_mouse_down = (int) (((MouseButton*) _in_.mouse.buttons + Nuklear.NK_BUTTON_LEFT)->down);
					int left_mouse_click_in_scaler =
						(int) (_in_.HasMouseClickDownInRect((int) (Nuklear.NK_BUTTON_LEFT), (Rect) (scaler), (int) (Nuklear.nk_true)));
					if (((left_mouse_down) != 0) && ((left_mouse_click_in_scaler) != 0))
					{
						float delta_x = (float) (_in_.mouse.delta.x);
						if ((layout.flags & Nuklear.NK_WINDOW_SCALE_LEFT) != 0)
						{
							delta_x = (float) (-delta_x);
							window.bounds.x += (float) (_in_.mouse.delta.x);
						}
						if ((window.bounds.w + delta_x) >= (window_size.x))
						{
							if (((delta_x) < (0)) || (((delta_x) > (0)) && ((_in_.mouse.pos.x) >= (scaler.x))))
							{
								window.bounds.w = (float) (window.bounds.w + delta_x);
								scaler.x += (float) (_in_.mouse.delta.x);
							}
						}
						if ((layout.flags & Nuklear.NK_WINDOW_DYNAMIC) == 0)
						{
							if ((window_size.y) < (window.bounds.h + _in_.mouse.delta.y))
							{
								if (((_in_.mouse.delta.y) < (0)) || (((_in_.mouse.delta.y) > (0)) && ((_in_.mouse.pos.y) >= (scaler.y))))
								{
									window.bounds.h = (float) (window.bounds.h + _in_.mouse.delta.y);
									scaler.y += (float) (_in_.mouse.delta.y);
								}
							}
						}
						this.style.cursor_active = this.style.cursors[Nuklear.NK_CURSOR_RESIZE_TOP_RIGHT_DOWN_LEFT];
						((MouseButton*) _in_.mouse.buttons + Nuklear.NK_BUTTON_LEFT)->clicked_pos.x = (float) (scaler.x + scaler.w/2.0f);
						((MouseButton*) _in_.mouse.buttons + Nuklear.NK_BUTTON_LEFT)->clicked_pos.y = (float) (scaler.y + scaler.h/2.0f);
					}
				}
			}

			if (Nuklear.PanelIsSub((int) (layout.type)) == 0)
			{
				if ((layout.flags & Nuklear.NK_WINDOW_HIDDEN) != 0) window.buffer.Reset();
				else Finish(window);
			}

			if ((layout.flags & Nuklear.NK_WINDOW_REMOVE_ROM) != 0)
			{
				layout.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_ROM));
				layout.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_REMOVE_ROM));
			}

			window.flags = (uint) (layout.flags);
			if ((((window.property.active) != 0) && (window.property.old != window.property.seq)) &&
			    ((window.property.active) == (window.property.prev)))
			{
			}
			else
			{
				window.property.old = (uint) (window.property.seq);
				window.property.prev = (int) (window.property.active);
				window.property.seq = (uint) (0);
			}

			if ((((window.edit.active) != 0) && (window.edit.old != window.edit.seq)) &&
			    ((window.edit.active) == (window.edit.prev)))
			{
			}
			else
			{
				window.edit.old = (uint) (window.edit.seq);
				window.edit.prev = (int) (window.edit.active);
				window.edit.seq = (uint) (0);
			}

			if (((window.popup.active_con) != 0) && (window.popup.con_old != window.popup.con_count))
			{
				window.popup.con_count = (uint) (0);
				window.popup.con_old = (uint) (0);
				window.popup.active_con = (uint) (0);
			}
			else
			{
				window.popup.con_old = (uint) (window.popup.con_count);
				window.popup.con_count = (uint) (0);
			}

			window.popup.combo_count = (uint) (0);
		}

		public uint* AddValue(Window win, uint name, uint value)
		{
			if ((win == null) || (this == null)) return null;
			if ((win.tables == null) || ((win.tables.size) >= (51)))
			{
				Table tbl = CreateTable();
				if (tbl == null) return null;
				win.PushTable(tbl);
			}

			win.tables.seq = (uint) (win.seq);
			win.tables.keys[win.tables.size] = (uint) (name);
			win.tables.values[win.tables.size] = (uint) (value);
			return (uint*) win.tables.values + (win.tables.size++);
		}

		public Window FindWindow(uint hash, char* name)
		{
			Window iter;
			iter = this.begin;
			while ((iter) != null)
			{
				if ((iter.name) == (hash))
				{
					int max_len = (int) (Nuklear.Strlen(iter.name_string));
					if (Nuklear.Stricmpn(iter.name_string, name, (int) (max_len)) == 0) return iter;
				}
				iter = iter.next;
			}
			return null;
		}

		public void InsertWindow(Window win, int loc)
		{
			Window iter;
			if ((win == null) || (this == null)) return;
			iter = this.begin;
			while ((iter) != null)
			{
				if ((iter) == (win)) return;
				iter = iter.next;
			}
			if (this.begin == null)
			{
				win.next = null;
				win.prev = null;
				this.begin = win;
				this.end = win;
				this.count = (uint) (1);
				return;
			}

			if ((loc) == (Nuklear.NK_INSERT_BACK))
			{
				Window end;
				end = this.end;
				end.flags |= (uint) (Nuklear.NK_WINDOW_ROM);
				end.next = win;
				win.prev = this.end;
				win.next = null;
				this.end = win;
				this.active = this.end;
				this.end.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_ROM));
			}
			else
			{
				this.begin.prev = win;
				win.next = this.begin;
				win.prev = null;
				this.begin = win;
				this.begin.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_ROM));
			}

			this.count++;
		}

		public void RemoveWindow(Window win)
		{
			if (((win) == (this.begin)) || ((win) == (this.end)))
			{
				if ((win) == (this.begin))
				{
					this.begin = win.next;
					if ((win.next) != null) win.next.prev = null;
				}
				if ((win) == (this.end))
				{
					this.end = win.prev;
					if ((win.prev) != null) win.prev.next = null;
				}
			}
			else
			{
				if ((win.next) != null) win.next.prev = win.prev;
				if ((win.prev) != null) win.prev.next = win.next;
			}

			if (((win) == (this.active)) || (this.active == null))
			{
				this.active = this.end;
				if ((this.end) != null) this.end.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_ROM));
			}

			win.next = null;
			win.prev = null;
			this.count--;
		}

		public int Begin(char* title, Rect bounds, uint flags)
		{
			return (int) (BeginTitled(title, title, (Rect) (bounds), (uint) (flags)));
		}

		public int BeginTitled(char* name, char* title, Rect bounds, uint flags)
		{
			Window win;
			Style style;
			uint title_hash;
			int title_len;
			int ret = (int) (0);
			if (((((this.current) != null)) || (title == null)) || (name == null)) return (int) (0);
			style = this.style;
			title_len = (int) (Nuklear.Strlen(name));
			title_hash = (uint) (Nuklear.MurmurHash(name, (int) (title_len), (uint) (Nuklear.NK_WINDOW_TITLE)));
			win = FindWindow((uint) (title_hash), name);
			if (win == null)
			{
				ulong name_length = (ulong) (Nuklear.Strlen(name));
				win = (Window) (CreateWindow());
				if (win == null) return (int) (0);
				if ((flags & Nuklear.NK_WINDOW_BACKGROUND) != 0) InsertWindow(win, (int) (Nuklear.NK_INSERT_FRONT));
				else InsertWindow(win, (int) (Nuklear.NK_INSERT_BACK));
				win.buffer.Init(this.memory, (int) (Nuklear.NK_CLIPPING_ON));
				win.flags = (uint) (flags);
				win.bounds = (Rect) (bounds);
				win.name = (uint) (title_hash);
				name_length = (ulong) ((name_length) < (64 - 1) ? (name_length) : (64 - 1));
				Nuklear.Memcopy(win.name_string, name, (ulong) (name_length));
				win.name_string[name_length] = (char) (0);
				win.popup.win = null;
				if (this.active == null) this.active = win;
			}
			else
			{
				win.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_PRIVATE - 1));
				win.flags |= (uint) (flags);
				if ((win.flags & (Nuklear.NK_WINDOW_MOVABLE | Nuklear.NK_WINDOW_SCALABLE)) == 0) win.bounds = (Rect) (bounds);
				win.seq = (uint) (this.seq);
				if ((this.active == null) && ((win.flags & Nuklear.NK_WINDOW_HIDDEN) == 0))
				{
					this.active = win;
					this.end = win;
				}
			}

			if ((win.flags & Nuklear.NK_WINDOW_HIDDEN) != 0)
			{
				this.current = win;
				win.layout = null;
				return (int) (0);
			}
			else Start(win);
			if (((win.flags & Nuklear.NK_WINDOW_HIDDEN) == 0) && ((win.flags & Nuklear.NK_WINDOW_NO_INPUT) == 0))
			{
				int inpanel;
				int ishovered;
				Window iter = win;
				float h =
					(float) (this.style.font.height + 2.0f*style.window.header.padding.y + (2.0f*style.window.header.label_padding.y));
				Rect win_bounds =
					(Rect)
						(((win.flags & Nuklear.NK_WINDOW_MINIMIZED) == 0)
							? win.bounds
							: Nuklear.Rectz((float) (win.bounds.x), (float) (win.bounds.y), (float) (win.bounds.w), (float) (h)));
				inpanel =
					(int)
						(this.input.HasMouseClickDownInRect((int) (Nuklear.NK_BUTTON_LEFT), (Rect) (win_bounds), (int) (Nuklear.nk_true)));
				inpanel = (int) (((inpanel) != 0) && ((this.input.mouse.buttons[Nuklear.NK_BUTTON_LEFT].clicked) != 0) ? 1 : 0);
				ishovered = (int) (this.input.IsMouseHoveringRect((Rect) (win_bounds)));
				if (((win != this.active) && ((ishovered) != 0)) && (this.input.mouse.buttons[Nuklear.NK_BUTTON_LEFT].down == 0))
				{
					iter = win.next;
					while ((iter) != null)
					{
						Rect iter_bounds =
							(Rect)
								(((iter.flags & Nuklear.NK_WINDOW_MINIMIZED) == 0)
									? iter.bounds
									: Nuklear.Rectz((float) (iter.bounds.x), (float) (iter.bounds.y), (float) (iter.bounds.w), (float) (h)));
						if (
							(!(((((iter_bounds.x) > (win_bounds.x + win_bounds.w)) || ((iter_bounds.x + iter_bounds.w) < (win_bounds.x))) ||
							    ((iter_bounds.y) > (win_bounds.y + win_bounds.h))) || ((iter_bounds.y + iter_bounds.h) < (win_bounds.y)))) &&
							((iter.flags & Nuklear.NK_WINDOW_HIDDEN) == 0)) break;
						if (((((iter.popup.win) != null) && ((iter.popup.active) != 0)) && ((iter.flags & Nuklear.NK_WINDOW_HIDDEN) == 0)) &&
						    (!(((((iter.popup.win.bounds.x) > (win.bounds.x + win_bounds.w)) ||
						         ((iter.popup.win.bounds.x + iter.popup.win.bounds.w) < (win.bounds.x))) ||
						        ((iter.popup.win.bounds.y) > (win_bounds.y + win_bounds.h))) ||
						       ((iter.popup.win.bounds.y + iter.popup.win.bounds.h) < (win_bounds.y))))) break;
						iter = iter.next;
					}
				}
				if ((((iter) != null) && ((inpanel) != 0)) && (win != this.end))
				{
					iter = win.next;
					while ((iter) != null)
					{
						Rect iter_bounds =
							(Rect)
								(((iter.flags & Nuklear.NK_WINDOW_MINIMIZED) == 0)
									? iter.bounds
									: Nuklear.Rectz((float) (iter.bounds.x), (float) (iter.bounds.y), (float) (iter.bounds.w), (float) (h)));
						if (((((iter_bounds.x) <= (this.input.mouse.pos.x)) &&
						      ((this.input.mouse.pos.x) < (iter_bounds.x + iter_bounds.w))) &&
						     (((iter_bounds.y) <= (this.input.mouse.pos.y)) &&
						      ((this.input.mouse.pos.y) < (iter_bounds.y + iter_bounds.h)))) &&
						    ((iter.flags & Nuklear.NK_WINDOW_HIDDEN) == 0)) break;
						if (((((iter.popup.win) != null) && ((iter.popup.active) != 0)) && ((iter.flags & Nuklear.NK_WINDOW_HIDDEN) == 0)) &&
						    (!(((((iter.popup.win.bounds.x) > (win_bounds.x + win_bounds.w)) ||
						         ((iter.popup.win.bounds.x + iter.popup.win.bounds.w) < (win_bounds.x))) ||
						        ((iter.popup.win.bounds.y) > (win_bounds.y + win_bounds.h))) ||
						       ((iter.popup.win.bounds.y + iter.popup.win.bounds.h) < (win_bounds.y))))) break;
						iter = iter.next;
					}
				}
				if ((((iter) != null) && ((win.flags & Nuklear.NK_WINDOW_ROM) == 0)) &&
				    ((win.flags & Nuklear.NK_WINDOW_BACKGROUND) != 0))
				{
					win.flags |= ((uint) (Nuklear.NK_WINDOW_ROM));
					iter.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_ROM));
					this.active = iter;
					if ((iter.flags & Nuklear.NK_WINDOW_BACKGROUND) == 0)
					{
						RemoveWindow(iter);
						InsertWindow(iter, (int) (Nuklear.NK_INSERT_BACK));
					}
				}
				else
				{
					if ((iter == null) && (this.end != win))
					{
						if ((win.flags & Nuklear.NK_WINDOW_BACKGROUND) == 0)
						{
							RemoveWindow(win);
							InsertWindow(win, (int) (Nuklear.NK_INSERT_BACK));
						}
						win.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_ROM));
						this.active = win;
					}
					if ((this.end != win) && ((win.flags & Nuklear.NK_WINDOW_BACKGROUND) == 0))
						win.flags |= (uint) (Nuklear.NK_WINDOW_ROM);
				}
			}

			win.layout = (Panel) (CreatePanel());
			this.current = win;
			ret = (int) (PanelBegin(title, (int) (Nuklear.NK_PANEL_WINDOW)));
			win.layout.offset = win.scrollbar;

			return (int) (ret);
		}

		public void End()
		{
			Panel layout;
			if ((this.current == null)) return;
			layout = this.current.layout;
			if ((layout == null) ||
			    (((layout.type) == (Nuklear.NK_PANEL_WINDOW)) && ((this.current.flags & Nuklear.NK_WINDOW_HIDDEN) != 0)))
			{
				this.current = null;
				return;
			}

			PanelEnd();
			FreePanel(this.current.layout);
			this.current = null;
		}

		public Rect WindowGetBounds()
		{
			if ((this.current == null)) return (Rect) (Nuklear.Rectz((float) (0), (float) (0), (float) (0), (float) (0)));
			return (Rect) (this.current.bounds);
		}

		public Vec2 WindowGetPosition()
		{
			if ((this.current == null)) return (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			return (Vec2) (Nuklear.Vec2z((float) (this.current.bounds.x), (float) (this.current.bounds.y)));
		}

		public Vec2 WindowGetSize()
		{
			if ((this.current == null)) return (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			return (Vec2) (Nuklear.Vec2z((float) (this.current.bounds.w), (float) (this.current.bounds.h)));
		}

		public float WindowGetWidth()
		{
			if ((this.current == null)) return (float) (0);
			return (float) (this.current.bounds.w);
		}

		public float WindowGetHeight()
		{
			if ((this.current == null)) return (float) (0);
			return (float) (this.current.bounds.h);
		}

		public Rect WindowGetContentRegion()
		{
			if ((this.current == null)) return (Rect) (Nuklear.Rectz((float) (0), (float) (0), (float) (0), (float) (0)));
			return (Rect) (this.current.layout.clip);
		}

		public Vec2 WindowGetContentRegionMin()
		{
			if ((this.current == null)) return (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			return (Vec2) (Nuklear.Vec2z((float) (this.current.layout.clip.x), (float) (this.current.layout.clip.y)));
		}

		public Vec2 WindowGetContentRegionMax()
		{
			if ((this.current == null)) return (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			return
				(Vec2)
					(Nuklear.Vec2z((float) (this.current.layout.clip.x + this.current.layout.clip.w),
						(float) (this.current.layout.clip.y + this.current.layout.clip.h)));
		}

		public Vec2 WindowGetContentRegionSize()
		{
			if ((this.current == null)) return (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			return (Vec2) (Nuklear.Vec2z((float) (this.current.layout.clip.w), (float) (this.current.layout.clip.h)));
		}

		public CommandBuffer WindowGetCanvas()
		{
			if ((this.current == null)) return null;
			return this.current.buffer;
		}

		public Panel WindowGetPanel()
		{
			if ((this.current == null)) return null;
			return this.current.layout;
		}

		public int WindowHasFocus()
		{
			if ((this.current == null)) return (int) (0);
			return (int) ((this.current) == (this.active) ? 1 : 0);
		}

		public int WindowIsHovered()
		{
			if ((this.current == null)) return (int) (0);
			if ((this.current.flags & Nuklear.NK_WINDOW_HIDDEN) != 0) return (int) (0);
			return (int) (this.input.IsMouseHoveringRect((Rect) (this.current.bounds)));
		}

		public int WindowIsAnyHovered()
		{
			Window iter;
			if (this == null) return (int) (0);
			iter = this.begin;
			while ((iter) != null)
			{
				if ((iter.flags & Nuklear.NK_WINDOW_HIDDEN) == 0)
				{
					if ((((iter.popup.active) != 0) && ((iter.popup.win) != null)) &&
					    ((this.input.IsMouseHoveringRect((Rect) (iter.popup.win.bounds))) != 0)) return (int) (1);
					if ((iter.flags & Nuklear.NK_WINDOW_MINIMIZED) != 0)
					{
						Rect header = (Rect) (iter.bounds);
						header.h = (float) (this.style.font.height + 2*this.style.window.header.padding.y);
						if ((this.input.IsMouseHoveringRect((Rect) (header))) != 0) return (int) (1);
					}
					else if ((this.input.IsMouseHoveringRect((Rect) (iter.bounds))) != 0)
					{
						return (int) (1);
					}
				}
				iter = iter.next;
			}
			return (int) (0);
		}

		public int ItemIsAnyActive()
		{
			int any_hovered = (int) (WindowIsAnyHovered());
			int any_active = (int) (this.last_widget_state & Nuklear.NK_WIDGET_STATE_MODIFIED);
			return (int) (((any_hovered) != 0) || ((any_active) != 0) ? 1 : 0);
		}

		public int WindowIsCollapsed(char* name)
		{
			int title_len;
			uint title_hash;
			Window win;
			if (this == null) return (int) (0);
			title_len = (int) (Nuklear.Strlen(name));
			title_hash = (uint) (Nuklear.MurmurHash(name, (int) (title_len), (uint) (Nuklear.NK_WINDOW_TITLE)));
			win = FindWindow((uint) (title_hash), name);
			if (win == null) return (int) (0);
			return (int) (win.flags & Nuklear.NK_WINDOW_MINIMIZED);
		}

		public int WindowIsClosed(char* name)
		{
			int title_len;
			uint title_hash;
			Window win;
			if (this == null) return (int) (1);
			title_len = (int) (Nuklear.Strlen(name));
			title_hash = (uint) (Nuklear.MurmurHash(name, (int) (title_len), (uint) (Nuklear.NK_WINDOW_TITLE)));
			win = FindWindow((uint) (title_hash), name);
			if (win == null) return (int) (1);
			return (int) (win.flags & Nuklear.NK_WINDOW_CLOSED);
		}

		public int WindowIsHidden(char* name)
		{
			int title_len;
			uint title_hash;
			Window win;
			if (this == null) return (int) (1);
			title_len = (int) (Nuklear.Strlen(name));
			title_hash = (uint) (Nuklear.MurmurHash(name, (int) (title_len), (uint) (Nuklear.NK_WINDOW_TITLE)));
			win = FindWindow((uint) (title_hash), name);
			if (win == null) return (int) (1);
			return (int) (win.flags & Nuklear.NK_WINDOW_HIDDEN);
		}

		public int WindowIsActive(char* name)
		{
			int title_len;
			uint title_hash;
			Window win;
			if (this == null) return (int) (0);
			title_len = (int) (Nuklear.Strlen(name));
			title_hash = (uint) (Nuklear.MurmurHash(name, (int) (title_len), (uint) (Nuklear.NK_WINDOW_TITLE)));
			win = FindWindow((uint) (title_hash), name);
			if (win == null) return (int) (0);
			return (int) ((win) == (this.active) ? 1 : 0);
		}

		public Window WindowFind(char* name)
		{
			int title_len;
			uint title_hash;
			title_len = (int) (Nuklear.Strlen(name));
			title_hash = (uint) (Nuklear.MurmurHash(name, (int) (title_len), (uint) (Nuklear.NK_WINDOW_TITLE)));
			return FindWindow((uint) (title_hash), name);
		}

		public void WindowClose(char* name)
		{
			Window win;
			if (this == null) return;
			win = WindowFind(name);
			if (win == null) return;
			if ((this.current) == (win)) return;
			win.flags |= (uint) (Nuklear.NK_WINDOW_HIDDEN);
			win.flags |= (uint) (Nuklear.NK_WINDOW_CLOSED);
		}

		public void WindowSetBounds(char* name, Rect bounds)
		{
			Window win;
			if (this == null) return;
			win = WindowFind(name);
			if (win == null) return;
			win.bounds = (Rect) (bounds);
		}

		public void WindowSetPosition(char* name, Vec2 pos)
		{
			Window win = WindowFind(name);
			if (win == null) return;
			win.bounds.x = (float) (pos.x);
			win.bounds.y = (float) (pos.y);
		}

		public void WindowSetSize(char* name, Vec2 size)
		{
			Window win = WindowFind(name);
			if (win == null) return;
			win.bounds.w = (float) (size.x);
			win.bounds.h = (float) (size.y);
		}

		public void WindowCollapse(char* name, int c)
		{
			int title_len;
			uint title_hash;
			Window win;
			if (this == null) return;
			title_len = (int) (Nuklear.Strlen(name));
			title_hash = (uint) (Nuklear.MurmurHash(name, (int) (title_len), (uint) (Nuklear.NK_WINDOW_TITLE)));
			win = FindWindow((uint) (title_hash), name);
			if (win == null) return;
			if ((c) == (Nuklear.NK_MINIMIZED)) win.flags |= (uint) (Nuklear.NK_WINDOW_MINIMIZED);
			else win.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_MINIMIZED));
		}

		public void WindowCollapseIf(char* name, int c, int cond)
		{
			if ((cond == 0)) return;
			WindowCollapse(name, (int) (c));
		}

		public void WindowShow(char* name, int s)
		{
			int title_len;
			uint title_hash;
			Window win;
			if (this == null) return;
			title_len = (int) (Nuklear.Strlen(name));
			title_hash = (uint) (Nuklear.MurmurHash(name, (int) (title_len), (uint) (Nuklear.NK_WINDOW_TITLE)));
			win = FindWindow((uint) (title_hash), name);
			if (win == null) return;
			if ((s) == (Nuklear.NK_HIDDEN))
			{
				win.flags |= (uint) (Nuklear.NK_WINDOW_HIDDEN);
			}
			else win.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_HIDDEN));
		}

		public void WindowShowIf(char* name, int s, int cond)
		{
			if ((cond == 0)) return;
			WindowShow(name, (int) (s));
		}

		public void WindowSetFocus(char* name)
		{
			int title_len;
			uint title_hash;
			Window win;
			if (this == null) return;
			title_len = (int) (Nuklear.Strlen(name));
			title_hash = (uint) (Nuklear.MurmurHash(name, (int) (title_len), (uint) (Nuklear.NK_WINDOW_TITLE)));
			win = FindWindow((uint) (title_hash), name);
			if (((win) != null) && (this.end != win))
			{
				RemoveWindow(win);
				InsertWindow(win, (int) (Nuklear.NK_INSERT_BACK));
			}

			this.active = win;
		}

		public void MenubarBegin()
		{
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			layout = this.current.layout;
			if (((layout.flags & Nuklear.NK_WINDOW_HIDDEN) != 0) || ((layout.flags & Nuklear.NK_WINDOW_MINIMIZED) != 0)) return;
			layout.menu.x = (float) (layout.at_x);
			layout.menu.y = (float) (layout.at_y + layout.row.height);
			layout.menu.w = (float) (layout.bounds.w);
			layout.menu.offset = layout.offset;

			layout.offset.y = (uint) (0);
		}

		public void MenubarEnd()
		{
			Window win;
			Panel layout;
			CommandBuffer _out_;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			_out_ = win.buffer;
			layout = win.layout;
			if (((layout.flags & Nuklear.NK_WINDOW_HIDDEN) != 0) || ((layout.flags & Nuklear.NK_WINDOW_MINIMIZED) != 0)) return;
			layout.menu.h = (float) (layout.at_y - layout.menu.y);
			layout.bounds.y += (float) (layout.menu.h + this.style.window.spacing.y + layout.row.height);
			layout.bounds.h -= (float) (layout.menu.h + this.style.window.spacing.y + layout.row.height);
			layout.offset.x = (uint) (layout.menu.offset.x);
			layout.offset.y = (uint) (layout.menu.offset.y);
			layout.at_y = (float) (layout.bounds.y - layout.row.height);
			layout.clip.y = (float) (layout.bounds.y);
			layout.clip.h = (float) (layout.bounds.h);
			_out_.PushScissor((Rect) (layout.clip));
		}

		public void LayoutSetMinRowHeight(float height)
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			layout.row.min_height = (float) (height);
		}

		public void LayoutResetMinRowHeight()
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			layout.row.min_height = (float) (this.style.font.height);
			layout.row.min_height += (float) (this.style.text.padding.y*2);
			layout.row.min_height += (float) (this.style.window.min_row_height_padding*2);
		}

		public void PanelLayout(Window win, float height, int cols)
		{
			Panel layout;
			Style style;
			CommandBuffer _out_;
			Vec2 item_spacing = new Vec2();
			Color color = new Color();
			if (((this.current == null)) || (this.current.layout == null)) return;
			layout = win.layout;
			style = this.style;
			_out_ = win.buffer;
			color = (Color) (style.window.background);
			item_spacing = (Vec2) (style.window.spacing);
			layout.row.index = (int) (0);
			layout.at_y += (float) (layout.row.height);
			layout.row.columns = (int) (cols);
			if ((height) == (0.0f))
				layout.row.height =
					(float) (((height) < (layout.row.min_height) ? (layout.row.min_height) : (height)) + item_spacing.y);
			else layout.row.height = (float) (height + item_spacing.y);
			layout.row.item_offset = (float) (0);
			if ((layout.flags & Nuklear.NK_WINDOW_DYNAMIC) != 0)
			{
				Rect background = new Rect();
				background.x = (float) (win.bounds.x);
				background.w = (float) (win.bounds.w);
				background.y = (float) (layout.at_y - 1.0f);
				background.h = (float) (layout.row.height + 1.0f);
				_out_.FillRect((Rect) (background), (float) (0), (Color) (color));
			}

		}

		public void RowLayoutz(int fmt, float height, int cols, int width)
		{
			Window win;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			PanelLayout(win, (float) (height), (int) (cols));
			if ((fmt) == (Nuklear.NK_DYNAMIC)) win.layout.row.type = (int) (Nuklear.NK_LAYOUT_DYNAMIC_FIXED);
			else win.layout.row.type = (int) (Nuklear.NK_LAYOUT_STATIC_FIXED);
			win.layout.row.ratio = null;
			win.layout.row.filled = (float) (0);
			win.layout.row.item_offset = (float) (0);
			win.layout.row.item_width = ((float) (width));
		}

		public float LayoutRatioFromPixel(float pixel_width)
		{
			Window win;
			if (((this.current == null)) || (this.current.layout == null)) return (float) (0);
			win = this.current;
			return
				(float)
					(((pixel_width/win.bounds.x) < (1.0f) ? (pixel_width/win.bounds.x) : (1.0f)) < (0.0f)
						? (0.0f)
						: ((pixel_width/win.bounds.x) < (1.0f) ? (pixel_width/win.bounds.x) : (1.0f)));
		}

		public void LayoutRowDynamic(float height, int cols)
		{
			RowLayoutz((int) (Nuklear.NK_DYNAMIC), (float) (height), (int) (cols), (int) (0));
		}

		public void LayoutRowStatic(float height, int item_width, int cols)
		{
			RowLayoutz((int) (Nuklear.NK_STATIC), (float) (height), (int) (cols), (int) (item_width));
		}

		public void LayoutRowBegin(int fmt, float row_height, int cols)
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			PanelLayout(win, (float) (row_height), (int) (cols));
			if ((fmt) == (Nuklear.NK_DYNAMIC)) layout.row.type = (int) (Nuklear.NK_LAYOUT_DYNAMIC_ROW);
			else layout.row.type = (int) (Nuklear.NK_LAYOUT_STATIC_ROW);
			layout.row.ratio = null;
			layout.row.filled = (float) (0);
			layout.row.item_width = (float) (0);
			layout.row.item_offset = (float) (0);
			layout.row.columns = (int) (cols);
		}

		public void LayoutRowPush(float ratio_or_width)
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			if ((layout.row.type != Nuklear.NK_LAYOUT_STATIC_ROW) && (layout.row.type != Nuklear.NK_LAYOUT_DYNAMIC_ROW)) return;
			if ((layout.row.type) == (Nuklear.NK_LAYOUT_DYNAMIC_ROW))
			{
				float ratio = (float) (ratio_or_width);
				if ((ratio + layout.row.filled) > (1.0f)) return;
				if ((ratio) > (0.0f))
					layout.row.item_width =
						(float) ((0) < ((1.0f) < (ratio) ? (1.0f) : (ratio)) ? ((1.0f) < (ratio) ? (1.0f) : (ratio)) : (0));
				else layout.row.item_width = (float) (1.0f - layout.row.filled);
			}
			else layout.row.item_width = (float) (ratio_or_width);
		}

		public void LayoutRowEnd()
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			if ((layout.row.type != Nuklear.NK_LAYOUT_STATIC_ROW) && (layout.row.type != Nuklear.NK_LAYOUT_DYNAMIC_ROW)) return;
			layout.row.item_width = (float) (0);
			layout.row.item_offset = (float) (0);
		}

		public void LayoutRow(int fmt, float height, int cols, float* ratio)
		{
			int i;
			int n_undef = (int) (0);
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			PanelLayout(win, (float) (height), (int) (cols));
			if ((fmt) == (Nuklear.NK_DYNAMIC))
			{
				float r = (float) (0);
				layout.row.ratio = ratio;
				for (i = (int) (0); (i) < (cols); ++i)
				{
					if ((ratio[i]) < (0.0f)) n_undef++;
					else r += (float) (ratio[i]);
				}
				r = (float) ((0) < ((1.0f) < (1.0f - r) ? (1.0f) : (1.0f - r)) ? ((1.0f) < (1.0f - r) ? (1.0f) : (1.0f - r)) : (0));
				layout.row.type = (int) (Nuklear.NK_LAYOUT_DYNAMIC);
				layout.row.item_width = (float) ((((r) > (0)) && ((n_undef) > (0))) ? (r/(float) (n_undef)) : 0);
			}
			else
			{
				layout.row.ratio = ratio;
				layout.row.type = (int) (Nuklear.NK_LAYOUT_STATIC);
				layout.row.item_width = (float) (0);
				layout.row.item_offset = (float) (0);
			}

			layout.row.item_offset = (float) (0);
			layout.row.filled = (float) (0);
		}

		public void LayoutRowTemplateBegin(float height)
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			PanelLayout(win, (float) (height), (int) (1));
			layout.row.type = (int) (Nuklear.NK_LAYOUT_TEMPLATE);
			layout.row.columns = (int) (0);
			layout.row.ratio = null;
			layout.row.item_width = (float) (0);
			layout.row.item_height = (float) (0);
			layout.row.item_offset = (float) (0);
			layout.row.filled = (float) (0);
			layout.row.item.x = (float) (0);
			layout.row.item.y = (float) (0);
			layout.row.item.w = (float) (0);
			layout.row.item.h = (float) (0);
		}

		public void LayoutRowTemplatePushDynamic()
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			if (layout.row.type != Nuklear.NK_LAYOUT_TEMPLATE) return;
			if ((layout.row.columns) >= (16)) return;
			layout.row.templates[layout.row.columns++] = (float) (-1.0f);
		}

		public void LayoutRowTemplatePushVariable(float min_width)
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			if (layout.row.type != Nuklear.NK_LAYOUT_TEMPLATE) return;
			if ((layout.row.columns) >= (16)) return;
			layout.row.templates[layout.row.columns++] = (float) (-min_width);
		}

		public void LayoutRowTemplatePushStatic(float width)
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			if (layout.row.type != Nuklear.NK_LAYOUT_TEMPLATE) return;
			if ((layout.row.columns) >= (16)) return;
			layout.row.templates[layout.row.columns++] = (float) (width);
		}

		public void LayoutRowTemplateEnd()
		{
			Window win;
			Panel layout;
			int i = (int) (0);
			int variable_count = (int) (0);
			int min_variable_count = (int) (0);
			float min_fixed_width = (float) (0.0f);
			float total_fixed_width = (float) (0.0f);
			float max_variable_width = (float) (0.0f);
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			if (layout.row.type != Nuklear.NK_LAYOUT_TEMPLATE) return;
			for (i = (int) (0); (i) < (layout.row.columns); ++i)
			{
				float width = (float) (layout.row.templates[i]);
				if ((width) >= (0.0f))
				{
					total_fixed_width += (float) (width);
					min_fixed_width += (float) (width);
				}
				else if ((width) < (-1.0f))
				{
					width = (float) (-width);
					total_fixed_width += (float) (width);
					max_variable_width = (float) ((max_variable_width) < (width) ? (width) : (max_variable_width));
					variable_count++;
				}
				else
				{
					min_variable_count++;
					variable_count++;
				}
			}
			if ((variable_count) != 0)
			{
				float space =
					(float)
						(this.style.LayoutRowCalculateUsableSpace((int) (layout.type), (float) (layout.bounds.w),
							(int) (layout.row.columns)));
				float var_width =
					(float) (((space - min_fixed_width) < (0.0f) ? (0.0f) : (space - min_fixed_width))/(float) (variable_count));
				int enough_space = (int) ((var_width) >= (max_variable_width) ? 1 : 0);
				if (enough_space == 0)
					var_width =
						(float) (((space - total_fixed_width) < (0) ? (0) : (space - total_fixed_width))/(float) (min_variable_count));
				for (i = (int) (0); (i) < (layout.row.columns); ++i)
				{
					float* width = (float*) layout.row.templates + i;
					*width =
						(float) (((*width) >= (0.0f)) ? *width : (((*width) < (-1.0f)) && (enough_space == 0)) ? -(*width) : var_width);
				}
			}

		}

		public void LayoutSpaceBegin(int fmt, float height, int widget_count)
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			PanelLayout(win, (float) (height), (int) (widget_count));
			if ((fmt) == (Nuklear.NK_STATIC)) layout.row.type = (int) (Nuklear.NK_LAYOUT_STATIC_FREE);
			else layout.row.type = (int) (Nuklear.NK_LAYOUT_DYNAMIC_FREE);
			layout.row.ratio = null;
			layout.row.filled = (float) (0);
			layout.row.item_width = (float) (0);
			layout.row.item_offset = (float) (0);
		}

		public void LayoutSpaceEnd()
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			layout.row.item_width = (float) (0);
			layout.row.item_height = (float) (0);
			layout.row.item_offset = (float) (0);
			fixed (void* ptr = &layout.row.item)
			{
				Nuklear.Zero(ptr, (ulong) (sizeof (Rect)));
			}
		}

		public void LayoutSpacePush(Rect rect)
		{
			Window win;
			Panel layout;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			layout.row.item = (Rect) (rect);
		}

		public Rect LayoutSpaceBounds()
		{
			Rect ret = new Rect();
			Window win;
			Panel layout;
			win = this.current;
			layout = win.layout;
			ret.x = (float) (layout.clip.x);
			ret.y = (float) (layout.clip.y);
			ret.w = (float) (layout.clip.w);
			ret.h = (float) (layout.row.height);
			return (Rect) (ret);
		}

		public Rect LayoutWidgetBounds()
		{
			Rect ret = new Rect();
			Window win;
			Panel layout;
			win = this.current;
			layout = win.layout;
			ret.x = (float) (layout.at_x);
			ret.y = (float) (layout.at_y);
			ret.w = (float) (layout.bounds.w - ((layout.at_x - layout.bounds.x) < (0) ? (0) : (layout.at_x - layout.bounds.x)));
			ret.h = (float) (layout.row.height);
			return (Rect) (ret);
		}

		public Vec2 LayoutSpaceToScreen(Vec2 ret)
		{
			Window win;
			Panel layout;
			win = this.current;
			layout = win.layout;
			ret.x += (float) (layout.at_x - (float) (layout.offset.x));
			ret.y += (float) (layout.at_y - (float) (layout.offset.y));
			return (Vec2) (ret);
		}

		public Vec2 LayoutSpaceToLocal(Vec2 ret)
		{
			Window win;
			Panel layout;
			win = this.current;
			layout = win.layout;
			ret.x += (float) (-layout.at_x + (float) (layout.offset.x));
			ret.y += (float) (-layout.at_y + (float) (layout.offset.y));
			return (Vec2) (ret);
		}

		public Rect LayoutSpaceRectToScreen(Rect ret)
		{
			Window win;
			Panel layout;
			win = this.current;
			layout = win.layout;
			ret.x += (float) (layout.at_x - (float) (layout.offset.x));
			ret.y += (float) (layout.at_y - (float) (layout.offset.y));
			return (Rect) (ret);
		}

		public Rect LayoutSpaceRectToLocal(Rect ret)
		{
			Window win;
			Panel layout;
			win = this.current;
			layout = win.layout;
			ret.x += (float) (-layout.at_x + (float) (layout.offset.x));
			ret.y += (float) (-layout.at_y + (float) (layout.offset.y));
			return (Rect) (ret);
		}

		public void PanelAllocRow(Window win)
		{
			Panel layout = win.layout;
			Vec2 spacing = (Vec2) (this.style.window.spacing);
			float row_height = (float) (layout.row.height - spacing.y);
			PanelLayout(win, (float) (row_height), (int) (layout.row.columns));
		}

		public int TreeStateBase(int type, Image img, char* title, ref int state)
		{
			Window win;
			Panel layout;
			Style style;
			CommandBuffer _out_;
			Input _in_;
			StyleButton button;
			int symbol;
			float row_height;
			Vec2 item_spacing = new Vec2();
			Rect header = new Rect();
			Rect sym = new Rect();
			Text text = new Text();
			uint ws = (uint) (0);
			int widget_state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			layout = win.layout;
			_out_ = win.buffer;
			style = this.style;
			item_spacing = (Vec2) (style.window.spacing);
			row_height = (float) (style.font.height + 2*style.tab.padding.y);
			LayoutSetMinRowHeight((float) (row_height));
			LayoutRowDynamic((float) (row_height), (int) (1));
			LayoutResetMinRowHeight();
			widget_state = (int) (header.Widget(this));
			if ((type) == (Nuklear.NK_TREE_TAB))
			{
				StyleItem background = style.tab.background;
				if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
				{
					_out_.DrawImage((Rect) (header), background.data.image, (Color) (Nuklear.nk_white));
					text.background = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
				}
				else
				{
					text.background = (Color) (background.data.color);
					_out_.FillRect((Rect) (header), (float) (0), (Color) (style.tab.border_color));
					_out_.FillRect((Rect) (header.ShrinkRectz((float) (style.tab.border))), (float) (style.tab.rounding),
						(Color) (background.data.color));
				}
			}
			else text.background = (Color) (style.window.background);
			_in_ = ((layout.flags & Nuklear.NK_WINDOW_ROM) == 0) ? this.input : null;
			_in_ = (((_in_) != null) && ((widget_state) == (Nuklear.NK_WIDGET_VALID))) ? this.input : null;
			if ((Nuklear.ButtonBehavior(ref ws, (Rect) (header), _in_, (int) (Nuklear.NK_BUTTON_DEFAULT))) != 0)
				state = (int) (((state) == (Nuklear.NK_MAXIMIZED)) ? Nuklear.NK_MINIMIZED : Nuklear.NK_MAXIMIZED);
			if ((state) == (Nuklear.NK_MAXIMIZED))
			{
				symbol = (int) (style.tab.sym_maximize);
				if ((type) == (Nuklear.NK_TREE_TAB)) button = style.tab.tab_maximize_button;
				else button = style.tab.node_maximize_button;
			}
			else
			{
				symbol = (int) (style.tab.sym_minimize);
				if ((type) == (Nuklear.NK_TREE_TAB)) button = style.tab.tab_minimize_button;
				else button = style.tab.node_minimize_button;
			}

			{
				sym.w = (float) (sym.h = (float) (style.font.height));
				sym.y = (float) (header.y + style.tab.padding.y);
				sym.x = (float) (header.x + style.tab.padding.x);
				Nuklear.DoButtonSymbol(ref ws, win.buffer, (Rect) (sym), (int) (symbol), (int) (Nuklear.NK_BUTTON_DEFAULT), button,
					null, style.font);
				if ((img) != null)
				{
					sym.x = (float) (sym.x + sym.w + 4*item_spacing.x);
					win.buffer.DrawImage((Rect) (sym), img, (Color) (Nuklear.nk_white));
					sym.w = (float) (style.font.height + style.tab.spacing.x);
				}
			}

			{
				Rect label = new Rect();
				header.w = (float) ((header.w) < (sym.w + item_spacing.x) ? (sym.w + item_spacing.x) : (header.w));
				label.x = (float) (sym.x + sym.w + item_spacing.x);
				label.y = (float) (sym.y);
				label.w = (float) (header.w - (sym.w + item_spacing.y + style.tab.indent));
				label.h = (float) (style.font.height);
				text.text = (Color) (style.tab.text);
				text.padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
				_out_.WidgetText((Rect) (label), title, (int) (Nuklear.Strlen(title)), &text, (uint) (Nuklear.NK_TEXT_LEFT),
					style.font);
			}

			if ((state) == (Nuklear.NK_MAXIMIZED))
			{
				layout.at_x = (float) (header.x + (float) (layout.offset.x) + style.tab.indent);
				layout.bounds.w = (float) ((layout.bounds.w) < (style.tab.indent) ? (style.tab.indent) : (layout.bounds.w));
				layout.bounds.w -= (float) (style.tab.indent + style.window.padding.x);
				layout.row.tree_depth++;
				return (int) (Nuklear.nk_true);
			}
			else return (int) (Nuklear.nk_false);
		}

		public int TreeBase(int type, Image img, char* title, int initial_state, char* hash, int len, int line)
		{
			Window win = this.current;
			int title_len = (int) (0);
			uint tree_hash = (uint) (0);
			uint* state = null;
			if (hash == null)
			{
				title_len = (int) (Nuklear.Strlen(title));
				tree_hash = (uint) (Nuklear.MurmurHash(title, (int) (title_len), (uint) (line)));
			}
			else tree_hash = (uint) (Nuklear.MurmurHash(hash, (int) (len), (uint) (line)));
			state = win.FindValue((uint) (tree_hash));
			if (state == null)
			{
				state = AddValue(win, (uint) (tree_hash), (uint) (0));
				*state = (uint) (initial_state);
			}

			return (int) (TreeStateBase((int) (type), img, title, ref (int) (state)));
		}

		public int TreeStatePush(int type, char* title, ref int state)
		{
			return (int) (TreeStateBase((int) (type), null, title, ref state));
		}

		public int TreeStateImagePush(int type, Image img, char* title, ref int state)
		{
			return (int) (TreeStateBase((int) (type), img, title, ref state));
		}

		public void TreeStatePop()
		{
			Window win = null;
			Panel layout = null;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			layout.at_x -= (float) (this.style.tab.indent + this.style.window.padding.x);
			layout.bounds.w += (float) (this.style.tab.indent + this.style.window.padding.x);
			layout.row.tree_depth--;
		}

		public int TreePushHashed(int type, char* title, int initial_state, char* hash, int len, int line)
		{
			return (int) (TreeBase((int) (type), null, title, (int) (initial_state), hash, (int) (len), (int) (line)));
		}

		public int TreeImagePushHashed(int type, Image img, char* title, int initial_state, char* hash, int len, int seed)
		{
			return (int) (TreeBase((int) (type), img, title, (int) (initial_state), hash, (int) (len), (int) (seed)));
		}

		public void TreePop()
		{
			TreeStatePop();
		}

		public Rect WidgetBounds()
		{
			Rect bounds = new Rect();
			if ((this.current == null)) return (Rect) (Nuklear.Rectz((float) (0), (float) (0), (float) (0), (float) (0)));
			bounds.LayoutPeek(this);
			return (Rect) (bounds);
		}

		public Vec2 WidgetPosition()
		{
			Rect bounds = new Rect();
			if ((this.current == null)) return (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			bounds.LayoutPeek(this);
			return (Vec2) (Nuklear.Vec2z((float) (bounds.x), (float) (bounds.y)));
		}

		public Vec2 WidgetSize()
		{
			Rect bounds = new Rect();
			if ((this.current == null)) return (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			bounds.LayoutPeek(this);
			return (Vec2) (Nuklear.Vec2z((float) (bounds.w), (float) (bounds.h)));
		}

		public float WidgetWidth()
		{
			Rect bounds = new Rect();
			if ((this.current == null)) return (float) (0);
			bounds.LayoutPeek(this);
			return (float) (bounds.w);
		}

		public float WidgetHeight()
		{
			Rect bounds = new Rect();
			if ((this.current == null)) return (float) (0);
			bounds.LayoutPeek(this);
			return (float) (bounds.h);
		}

		public int WidgetIsHovered()
		{
			Rect c = new Rect();
			Rect v = new Rect();
			Rect bounds = new Rect();
			if (((this.current == null)) || (this.active != this.current)) return (int) (0);
			c = (Rect) (this.current.layout.clip);
			c.x = ((float) ((int) (c.x)));
			c.y = ((float) ((int) (c.y)));
			c.w = ((float) ((int) (c.w)));
			c.h = ((float) ((int) (c.h)));
			bounds.LayoutPeek(this);
			v.Unify(ref c, (float) (bounds.x), (float) (bounds.y), (float) (bounds.x + bounds.w), (float) (bounds.y + bounds.h));
			if (
				!(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
				    ((bounds.y + bounds.h) < (c.y))))) return (int) (0);
			return (int) (this.input.IsMouseHoveringRect((Rect) (bounds)));
		}

		public int WidgetIsMouseClicked(int btn)
		{
			Rect c = new Rect();
			Rect v = new Rect();
			Rect bounds = new Rect();
			if (((this.current == null)) || (this.active != this.current)) return (int) (0);
			c = (Rect) (this.current.layout.clip);
			c.x = ((float) ((int) (c.x)));
			c.y = ((float) ((int) (c.y)));
			c.w = ((float) ((int) (c.w)));
			c.h = ((float) ((int) (c.h)));
			bounds.LayoutPeek(this);
			v.Unify(ref c, (float) (bounds.x), (float) (bounds.y), (float) (bounds.x + bounds.w), (float) (bounds.y + bounds.h));
			if (
				!(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
				    ((bounds.y + bounds.h) < (c.y))))) return (int) (0);
			return (int) (this.input.MouseClicked((int) (btn), (Rect) (bounds)));
		}

		public int WidgetHasMouseClickDown(int btn, int down)
		{
			Rect c = new Rect();
			Rect v = new Rect();
			Rect bounds = new Rect();
			if (((this.current == null)) || (this.active != this.current)) return (int) (0);
			c = (Rect) (this.current.layout.clip);
			c.x = ((float) ((int) (c.x)));
			c.y = ((float) ((int) (c.y)));
			c.w = ((float) ((int) (c.w)));
			c.h = ((float) ((int) (c.h)));
			bounds.LayoutPeek(this);
			v.Unify(ref c, (float) (bounds.x), (float) (bounds.y), (float) (bounds.x + bounds.w), (float) (bounds.y + bounds.h));
			if (
				!(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
				    ((bounds.y + bounds.h) < (c.y))))) return (int) (0);
			return (int) (this.input.HasMouseClickDownInRect((int) (btn), (Rect) (bounds), (int) (down)));
		}

		public void Spacing(int cols)
		{
			Window win;
			Panel layout;
			Rect none = new Rect();
			int i;
			int index;
			int rows;
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			index = (int) ((layout.row.index + cols)%layout.row.columns);
			rows = (int) ((layout.row.index + cols)/layout.row.columns);
			if ((rows) != 0)
			{
				for (i = (int) (0); (i) < (rows); ++i)
				{
					PanelAllocRow(win);
				}
				cols = (int) (index);
			}

			if ((layout.row.type != Nuklear.NK_LAYOUT_DYNAMIC_FIXED) && (layout.row.type != Nuklear.NK_LAYOUT_STATIC_FIXED))
			{
				for (i = (int) (0); (i) < (cols); ++i)
				{
					none.PanelAllocSpace(this);
				}
			}

			layout.row.index = (int) (index);
		}

		public void TextColored(char* str, int len, uint alignment, Color color)
		{
			Window win;
			Style style;
			Vec2 item_padding = new Vec2();
			Rect bounds = new Rect();
			Text text = new Text();
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			style = this.style;
			bounds.PanelAllocSpace(this);
			item_padding = (Vec2) (style.text.padding);
			text.padding.x = (float) (item_padding.x);
			text.padding.y = (float) (item_padding.y);
			text.background = (Color) (style.window.background);
			text.text = (Color) (color);
			win.buffer.WidgetText((Rect) (bounds), str, (int) (len), &text, (uint) (alignment), style.font);
		}

		public void TextWrapColored(char* str, int len, Color color)
		{
			Window win;
			Style style;
			Vec2 item_padding = new Vec2();
			Rect bounds = new Rect();
			Text text = new Text();
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			style = this.style;
			bounds.PanelAllocSpace(this);
			item_padding = (Vec2) (style.text.padding);
			text.padding.x = (float) (item_padding.x);
			text.padding.y = (float) (item_padding.y);
			text.background = (Color) (style.window.background);
			text.text = (Color) (color);
			win.buffer.WidgetTextWrap((Rect) (bounds), str, (int) (len), &text, style.font);
		}

		public void Textz(char* str, int len, uint alignment)
		{
			if (this == null) return;
			TextColored(str, (int) (len), (uint) (alignment), (Color) (this.style.text.color));
		}

		public void TextWrap(char* str, int len)
		{
			if (this == null) return;
			TextWrapColored(str, (int) (len), (Color) (this.style.text.color));
		}

		public void Label(char* str, uint alignment)
		{
			Textz(str, (int) (Nuklear.Strlen(str)), (uint) (alignment));
		}

		public void LabelColored(char* str, uint align, Color color)
		{
			TextColored(str, (int) (Nuklear.Strlen(str)), (uint) (align), (Color) (color));
		}

		public void LabelWrap(char* str)
		{
			TextWrap(str, (int) (Nuklear.Strlen(str)));
		}

		public void LabelColoredWrap(char* str, Color color)
		{
			TextWrapColored(str, (int) (Nuklear.Strlen(str)), (Color) (color));
		}

		public void Imagez(Image img)
		{
			Window win;
			Rect bounds = new Rect();
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			if (bounds.Widget(this) == 0) return;
			win.buffer.DrawImage((Rect) (bounds), img, (Color) (Nuklear.nk_white));
		}

		public void ButtonSetBehavior(int behavior)
		{
			if (this == null) return;
			this.button_behavior = (int) (behavior);
		}

		public int ButtonPushBehavior(int behavior)
		{
			ConfigStackButtonBehavior button_stack;
			ConfigStackButtonBehaviorElement element;
			if (this == null) return (int) (0);
			button_stack = this.stacks.button_behaviors;
			if ((button_stack.head) >= ((int) ((int) button_stack.elements.Length))) return (int) (0);
			element = button_stack.elements[button_stack.head++];
			element.address = &this.button_behavior;
			element.old_value = (int) (this.button_behavior);
			this.button_behavior = (int) (behavior);
			return (int) (1);
		}

		public int ButtonPopBehavior()
		{
			ConfigStackButtonBehavior button_stack;
			ConfigStackButtonBehaviorElement element;
			if (this == null) return (int) (0);
			button_stack = this.stacks.button_behaviors;
			if ((button_stack.head) < (1)) return (int) (0);
			element = button_stack.elements[--button_stack.head];
			*element.address = (int) (element.old_value);
			return (int) (1);
		}

		public int ButtonTextStyled(StyleButton style, char* title, int len)
		{
			Window win;
			Panel layout;
			Input _in_;
			Rect bounds = new Rect();
			int state;
			if ((((style == null) || (this == null)) || (this.current == null)) || (this.current.layout == null))
				return (int) (0);
			win = this.current;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			return
				(int)
					(Nuklear.DoButtonText(ref this.last_widget_state, win.buffer, (Rect) (bounds), title, (int) (len),
						(uint) (style.text_alignment), (int) (this.button_behavior), style, _in_, this.style.font));
		}

		public int ButtonText(char* title, int len)
		{
			if (this == null) return (int) (0);
			return (int) (ButtonTextStyled(this.style.button, title, (int) (len)));
		}

		public int ButtonLabelStyled(StyleButton style, char* title)
		{
			return (int) (ButtonTextStyled(style, title, (int) (Nuklear.Strlen(title))));
		}

		public int ButtonLabel(char* title)
		{
			return (int) (ButtonText(title, (int) (Nuklear.Strlen(title))));
		}

		public int ButtonColor(Color color)
		{
			Window win;
			Panel layout;
			Input _in_;
			StyleButton button = new StyleButton();
			int ret = (int) (0);
			Rect bounds = new Rect();
			Rect content = new Rect();
			int state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			button = (StyleButton) (this.style.button);
			button.normal = (StyleItem) (color.StyleItemColor());
			button.hover = (StyleItem) (color.StyleItemColor());
			button.active = (StyleItem) (color.StyleItemColor());
			ret =
				(int)
					(Nuklear.DoButton(ref this.last_widget_state, win.buffer, (Rect) (bounds), button, _in_,
						(int) (this.button_behavior), &content));
			win.buffer.DrawButton(&bounds, (uint) (this.last_widget_state), button);
			return (int) (ret);
		}

		public int ButtonSymbolStyled(StyleButton style, int symbol)
		{
			Window win;
			Panel layout;
			Input _in_;
			Rect bounds = new Rect();
			int state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			return
				(int)
					(Nuklear.DoButtonSymbol(ref this.last_widget_state, win.buffer, (Rect) (bounds), (int) (symbol),
						(int) (this.button_behavior), style, _in_, this.style.font));
		}

		public int ButtonSymbol(int symbol)
		{
			if (this == null) return (int) (0);
			return (int) (ButtonSymbolStyled(this.style.button, (int) (symbol)));
		}

		public int ButtonImageStyled(StyleButton style, Image img)
		{
			Window win;
			Panel layout;
			Input _in_;
			Rect bounds = new Rect();
			int state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			return
				(int)
					(Nuklear.DoButtonImage(ref this.last_widget_state, win.buffer, (Rect) (bounds), (Image) (img),
						(int) (this.button_behavior), style, _in_));
		}

		public int ButtonImage(Image img)
		{
			if (this == null) return (int) (0);
			return (int) (ButtonImageStyled(this.style.button, (Image) (img)));
		}

		public int ButtonSymbolTextStyled(StyleButton style, int symbol, char* text, int len, uint align)
		{
			Window win;
			Panel layout;
			Input _in_;
			Rect bounds = new Rect();
			int state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			return
				(int)
					(Nuklear.DoButtonTextSymbol(ref this.last_widget_state, win.buffer, (Rect) (bounds), (int) (symbol), text,
						(int) (len), (uint) (align), (int) (this.button_behavior), style, this.style.font, _in_));
		}

		public int ButtonSymbolText(int symbol, char* text, int len, uint align)
		{
			if (this == null) return (int) (0);
			return (int) (ButtonSymbolTextStyled(this.style.button, (int) (symbol), text, (int) (len), (uint) (align)));
		}

		public int ButtonSymbolLabel(int symbol, char* label, uint align)
		{
			return (int) (ButtonSymbolText((int) (symbol), label, (int) (Nuklear.Strlen(label)), (uint) (align)));
		}

		public int ButtonSymbolLabelStyled(StyleButton style, int symbol, char* title, uint align)
		{
			return (int) (ButtonSymbolTextStyled(style, (int) (symbol), title, (int) (Nuklear.Strlen(title)), (uint) (align)));
		}

		public int ButtonImageTextStyled(StyleButton style, Image img, char* text, int len, uint align)
		{
			Window win;
			Panel layout;
			Input _in_;
			Rect bounds = new Rect();
			int state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			return
				(int)
					(Nuklear.DoButtonTextImage(ref this.last_widget_state, win.buffer, (Rect) (bounds), (Image) (img), text,
						(int) (len), (uint) (align), (int) (this.button_behavior), style, this.style.font, _in_));
		}

		public int ButtonImageText(Image img, char* text, int len, uint align)
		{
			return (int) (ButtonImageTextStyled(this.style.button, (Image) (img), text, (int) (len), (uint) (align)));
		}

		public int ButtonImageLabel(Image img, char* label, uint align)
		{
			return (int) (ButtonImageText((Image) (img), label, (int) (Nuklear.Strlen(label)), (uint) (align)));
		}

		public int ButtonImageLabelStyled(StyleButton style, Image img, char* label, uint text_alignment)
		{
			return
				(int) (ButtonImageTextStyled(style, (Image) (img), label, (int) (Nuklear.Strlen(label)), (uint) (text_alignment)));
		}

		public int SelectableText(char* str, int len, uint align, ref int value)
		{
			Window win;
			Panel layout;
			Input _in_;
			Style style;
			int state;
			Rect bounds = new Rect();
			if ((((this.current == null)) || (this.current.layout == null)) || (value == null)) return (int) (0);
			win = this.current;
			layout = win.layout;
			style = this.style;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			return
				(int)
					(Nuklear.DoSelectable(ref this.last_widget_state, win.buffer, (Rect) (bounds), str, (int) (len), (uint) (align),
						ref value, style.selectable, _in_, style.font));
		}

		public int SelectableImageText(Image img, char* str, int len, uint align, ref int value)
		{
			Window win;
			Panel layout;
			Input _in_;
			Style style;
			int state;
			Rect bounds = new Rect();
			if ((((this.current == null)) || (this.current.layout == null)) || (value == null)) return (int) (0);
			win = this.current;
			layout = win.layout;
			style = this.style;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			return
				(int)
					(Nuklear.DoSelectableImage(ref this.last_widget_state, win.buffer, (Rect) (bounds), str, (int) (len),
						(uint) (align), ref value, img, style.selectable, _in_, style.font));
		}

		public int SelectText(char* str, int len, uint align, int value)
		{
			SelectableText(str, (int) (len), (uint) (align), ref value);
			return (int) (value);
		}

		public int SelectableLabel(char* str, uint align, ref int value)
		{
			return (int) (SelectableText(str, (int) (Nuklear.Strlen(str)), (uint) (align), ref value));
		}

		public int SelectableImageLabel(Image img, char* str, uint align, ref int value)
		{
			return (int) (SelectableImageText((Image) (img), str, (int) (Nuklear.Strlen(str)), (uint) (align), ref value));
		}

		public int SelectLabel(char* str, uint align, int value)
		{
			SelectableText(str, (int) (Nuklear.Strlen(str)), (uint) (align), ref value);
			return (int) (value);
		}

		public int SelectImageLabel(Image img, char* str, uint align, int value)
		{
			SelectableImageText((Image) (img), str, (int) (Nuklear.Strlen(str)), (uint) (align), ref value);
			return (int) (value);
		}

		public int SelectImageText(Image img, char* str, int len, uint align, int value)
		{
			SelectableImageText((Image) (img), str, (int) (len), (uint) (align), ref value);
			return (int) (value);
		}

		public int CheckText(char* text, int len, int active)
		{
			Window win;
			Panel layout;
			Input _in_;
			Style style;
			Rect bounds = new Rect();
			int state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (active);
			win = this.current;
			style = this.style;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (active);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			Nuklear.DoToggle(ref this.last_widget_state, win.buffer, (Rect) (bounds), &active, text, (int) (len),
				(int) (Nuklear.NK_TOGGLE_CHECK), style.checkbox, _in_, style.font);
			return (int) (active);
		}

		public uint CheckFlagsText(char* text, int len, uint flags, uint value)
		{
			int old_active;
			if ((text == null)) return (uint) (flags);
			old_active = ((int) ((flags & value) & value));
			if ((CheckText(text, (int) (len), (int) (old_active))) != 0) flags |= (uint) (value);
			else flags &= (uint) (~value);
			return (uint) (flags);
		}

		public int CheckboxText(char* text, int len, int* active)
		{
			int old_val;
			if (((text == null)) || (active == null)) return (int) (0);
			old_val = (int) (*active);
			*active = (int) (CheckText(text, (int) (len), (int) (*active)));
			return (old_val != *active) ? 1 : 0;
		}

		public int CheckboxFlagsText(char* text, int len, uint* flags, uint value)
		{
			int active;
			if (((text == null)) || (flags == null)) return (int) (0);
			active = ((int) ((*flags & value) & value));
			if ((CheckboxText(text, (int) (len), &active)) != 0)
			{
				if ((active) != 0) *flags |= (uint) (value);
				else *flags &= (uint) (~value);
				return (int) (1);
			}

			return (int) (0);
		}

		public int CheckLabel(char* label, int active)
		{
			return (int) (CheckText(label, (int) (Nuklear.Strlen(label)), (int) (active)));
		}

		public uint CheckFlagsLabel(char* label, uint flags, uint value)
		{
			return (uint) (CheckFlagsText(label, (int) (Nuklear.Strlen(label)), (uint) (flags), (uint) (value)));
		}

		public int CheckboxLabel(char* label, int* active)
		{
			return (int) (CheckboxText(label, (int) (Nuklear.Strlen(label)), active));
		}

		public int CheckboxFlagsLabel(char* label, uint* flags, uint value)
		{
			return (int) (CheckboxFlagsText(label, (int) (Nuklear.Strlen(label)), flags, (uint) (value)));
		}

		public int OptionText(char* text, int len, int is_active)
		{
			Window win;
			Panel layout;
			Input _in_;
			Style style;
			Rect bounds = new Rect();
			int state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (is_active);
			win = this.current;
			style = this.style;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (state);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			Nuklear.DoToggle(ref this.last_widget_state, win.buffer, (Rect) (bounds), &is_active, text, (int) (len),
				(int) (Nuklear.NK_TOGGLE_OPTION), style.option, _in_, style.font);
			return (int) (is_active);
		}

		public int RadioText(char* text, int len, int* active)
		{
			int old_value;
			if (((text == null)) || (active == null)) return (int) (0);
			old_value = (int) (*active);
			*active = (int) (OptionText(text, (int) (len), (int) (old_value)));
			return (old_value != *active) ? 1 : 0;
		}

		public int OptionLabel(char* label, int active)
		{
			return (int) (OptionText(label, (int) (Nuklear.Strlen(label)), (int) (active)));
		}

		public int RadioLabel(char* label, int* active)
		{
			return (int) (RadioText(label, (int) (Nuklear.Strlen(label)), active));
		}

		public int SliderFloat(float min_value, ref float value, float max_value, float value_step)
		{
			Window win;
			Panel layout;
			Input _in_;
			Style style;
			int ret = (int) (0);
			float old_value;
			Rect bounds = new Rect();
			int state;
			if ((((this.current == null)) || (this.current.layout == null)) || (value == null)) return (int) (ret);
			win = this.current;
			style = this.style;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (ret);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			old_value = (float) (value);
			value =
				(float)
					(Nuklear.DoSlider(ref this.last_widget_state, win.buffer, (Rect) (bounds), (float) (min_value), (float) (old_value),
						(float) (max_value), (float) (value_step), style.slider, _in_, style.font));
			return (((old_value) > (value)) || ((old_value) < (value))) ? 1 : 0;
		}

		public float SlideFloat(float min, float val, float max, float step)
		{
			SliderFloat((float) (min), ref val, (float) (max), (float) (step));
			return (float) (val);
		}

		public int SlideInt(int min, int val, int max, int step)
		{
			float value = (float) (val);
			SliderFloat((float) (min), ref value, (float) (max), (float) (step));
			return (int) (value);
		}

		public int SliderInt(int min, ref int val, int max, int step)
		{
			int ret;
			float value = (float) (val);
			ret = (int) (SliderFloat((float) (min), ref value, (float) (max), (float) (step)));
			val = ((int) (value));
			return (int) (ret);
		}

		public int Progress(ulong* cur, ulong max, int is_modifyable)
		{
			Window win;
			Panel layout;
			Style style;
			Input _in_;
			Rect bounds = new Rect();
			int state;
			ulong old_value;
			if ((((this.current == null)) || (this.current.layout == null)) || (cur == null)) return (int) (0);
			win = this.current;
			style = this.style;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			old_value = (ulong) (*cur);
			*cur =
				(ulong)
					(Nuklear.DoProgress(ref this.last_widget_state, win.buffer, (Rect) (bounds), (ulong) (*cur), (ulong) (max),
						(int) (is_modifyable), style.progress, _in_));
			return (*cur != old_value) ? 1 : 0;
		}

		public ulong Prog(ulong cur, ulong max, int modifyable)
		{
			Progress(&cur, (ulong) (max), (int) (modifyable));
			return (ulong) (cur);
		}

		public void EditFocus(uint flags)
		{
			uint hash;
			Window win;
			if ((this.current == null)) return;
			win = this.current;
			hash = (uint) (win.edit.seq);
			win.edit.active = (int) (Nuklear.nk_true);
			win.edit.name = (uint) (hash);
			if ((flags & Nuklear.NK_EDIT_ALWAYS_INSERT_MODE) != 0) win.edit.mode = (byte) (Nuklear.NK_TEXT_EDIT_MODE_INSERT);
		}

		public void EditUnfocus()
		{
			Window win;
			if ((this.current == null)) return;
			win = this.current;
			win.edit.active = (int) (Nuklear.nk_false);
			win.edit.name = (uint) (0);
		}

		public uint EditString(uint flags, char* memory, ref int len, int max, int (
		const nk_text_edit* 
	,
		unsigned 
		int 
	)*
		filter 
	)
		{
			uint hash;
			uint state;
			TextEdit edit;
			Window win;
			if (((memory == null)) || (len == null)) return (uint) (0);
			filter = (filter == null) ? nk_filter_default : filter;
			win = this.current;
			hash = (uint) (win.edit.seq);
			edit = this.text_edit;
			this.text_edit.TexteditClearState(
				(int)
					((flags & Nuklear.NK_EDIT_MULTILINE) != 0 ? Nuklear.NK_TEXT_EDIT_MULTI_LINE : Nuklear.NK_TEXT_EDIT_SINGLE_LINE),
				filter);
			if (((win.edit.active) != 0) && ((hash) == (win.edit.name)))
			{
				if ((flags & Nuklear.NK_EDIT_NO_CURSOR) != 0) edit.cursor = (int) (Nuklear.UtfLen(memory, (int) (len)));
				else edit.cursor = (int) (win.edit.cursor);
				if ((flags & Nuklear.NK_EDIT_SELECTABLE) == 0)
				{
					edit.select_start = (int) (win.edit.cursor);
					edit.select_end = (int) (win.edit.cursor);
				}
				else
				{
					edit.select_start = (int) (win.edit.sel_start);
					edit.select_end = (int) (win.edit.sel_end);
				}
				edit.mode = (byte) (win.edit.mode);
				edit.scrollbar.x = ((float) (win.edit.scrollbar.x));
				edit.scrollbar.y = ((float) (win.edit.scrollbar.y));
				edit.active = (byte) (Nuklear.nk_true);
			}
			else edit.active = (byte) (Nuklear.nk_false);
			max = (int) ((1) < (max) ? (max) : (1));
			len = (int) ((len) < (max - 1) ? (len) : (max - 1));
			edit._string_.InitFixed(memory, (ulong) (max));
			edit._string_.buffer.allocated = ((ulong) (len));
			edit._string_.len = (int) (Nuklear.UtfLen(memory, (int) (len)));
			state = (uint) (EditBuffer((uint) (flags), edit, filter));
			len = ((int) (edit._string_.buffer.allocated));
			if ((edit.active) != 0)
			{
				win.edit.cursor = (int) (edit.cursor);
				win.edit.sel_start = (int) (edit.select_start);
				win.edit.sel_end = (int) (edit.select_end);
				win.edit.mode = (byte) (edit.mode);
				win.edit.scrollbar.x = ((uint) (edit.scrollbar.x));
				win.edit.scrollbar.y = ((uint) (edit.scrollbar.y));
			}

			return (uint) (state);
		}

		public uint EditBuffer(uint flags, TextEdit edit, int (
		const nk_text_edit* 
	,
		unsigned 
		int 
	)*
		filter 
	)
		{
			Window win;
			Style style;
			Input _in_;
			int state;
			Rect bounds = new Rect();
			uint ret_flags = (uint) (0);
			byte prev_state;
			uint hash;
			if (((this.current == null)) || (this.current.layout == null)) return (uint) (0);
			win = this.current;
			style = this.style;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (uint) (state);
			_in_ = (win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0 ? null : this.input;
			hash = (uint) (win.edit.seq++);
			if (((win.edit.active) != 0) && ((hash) == (win.edit.name)))
			{
				if ((flags & Nuklear.NK_EDIT_NO_CURSOR) != 0) edit.cursor = (int) (edit._string_.len);
				if ((flags & Nuklear.NK_EDIT_SELECTABLE) == 0)
				{
					edit.select_start = (int) (edit.cursor);
					edit.select_end = (int) (edit.cursor);
				}
				if ((flags & Nuklear.NK_EDIT_CLIPBOARD) != 0) edit.clip = (Clipboard) (this.clip);
				edit.active = ((byte) (win.edit.active));
			}
			else edit.active = (byte) (Nuklear.nk_false);
			edit.mode = (byte) (win.edit.mode);
			filter = (filter == null) ? nk_filter_default : filter;
			prev_state = (byte) (edit.active);
			_in_ = (flags & Nuklear.NK_EDIT_READ_ONLY) != 0 ? null : _in_;
			ret_flags =
				(uint)
					(Nuklear.DoEdit(ref this.last_widget_state, win.buffer, (Rect) (bounds), (uint) (flags), filter, edit, style.edit,
						_in_, style.font));
			if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0)
				this.style.cursor_active = this.style.cursors[Nuklear.NK_CURSOR_TEXT];
			if (((edit.active) != 0) && (prev_state != edit.active))
			{
				win.edit.active = (int) (Nuklear.nk_true);
				win.edit.name = (uint) (hash);
			}
			else if (((prev_state) != 0) && (edit.active == 0))
			{
				win.edit.active = (int) (Nuklear.nk_false);
			}

			return (uint) (ret_flags);
		}

		public uint EditStringZeroTerminated(uint flags, char* buffer, int max, int (
		const nk_text_edit* 
	,
		unsigned 
		int 
	)*
		filter 
	)
		{
			uint result;
			int len = (int) (Nuklear.Strlen(buffer));
			result = (uint) (EditString((uint) (flags), buffer, ref len, (int) (max), filter));
			buffer[(((max - 1) < (0) ? (0) : (max - 1)) < (len) ? ((max - 1) < (0) ? (0) : (max - 1)) : (len))] = ('\0');
			return (uint) (result);
		}

		public void Propertyz(char* name, PropertyVariant* variant, float inc_per_pixel, 
		enum nk_property_filter
		

		filter 
	)
		{
			Window win;
			Panel layout;
			Input _in_;
			Style style;
			Rect bounds = new Rect();
			int s;
			int* state = null;
			uint hash = (uint) (0);
			char* buffer = null;
			int* len = null;
			int* cursor = null;
			int* select_begin = null;
			int* select_end = null;
			int old_state;
			char* dummy_buffer = stackalloc char[64];
			int dummy_state = (int) (Nuklear.NK_PROPERTY_DEFAULT);
			int dummy_length = (int) (0);
			int dummy_cursor = (int) (0);
			int dummy_select_begin = (int) (0);
			int dummy_select_end = (int) (0);
			if (((this.current == null)) || (this.current.layout == null)) return;
			win = this.current;
			layout = win.layout;
			style = this.style;
			s = (int) (bounds.Widget(this));
			if (s == 0) return;
			if ((name[0]) == ('#'))
			{
				hash = (uint) (Nuklear.MurmurHash(name, (int) (Nuklear.Strlen(name)), (uint) (win.property.seq++)));
				name++;
			}
			else hash = (uint) (Nuklear.MurmurHash(name, (int) (Nuklear.Strlen(name)), (uint) (42)));
			if (((win.property.active) != 0) && ((hash) == (win.property.name)))
			{
				buffer = win.property.buffer;
				len = &win.property.length;
				cursor = &win.property.cursor;
				state = &win.property.state;
				select_begin = &win.property.select_start;
				select_end = &win.property.select_end;
			}
			else
			{
				buffer = dummy_buffer;
				len = &dummy_length;
				cursor = &dummy_cursor;
				state = &dummy_state;
				select_begin = &dummy_select_begin;
				select_end = &dummy_select_end;
			}

			old_state = (int) (*state);
			this.text_edit.clip = (Clipboard) (this.clip);
			_in_ = ((((s) == (Nuklear.NK_WIDGET_ROM)) && (win.property.active == 0)) ||
			        ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0))
				? null
				: this.input;
			Nuklear.DoProperty(ref this.last_widget_state, win.buffer, (Rect) (bounds), name, variant, (float) (inc_per_pixel),
				buffer, ref len, ref state, ref cursor, ref select_begin, ref select_end, style.property, (int) (filter), _in_,
				style.font, this.text_edit, (int) (this.button_behavior));
			if ((((_in_) != null) && (*state != Nuklear.NK_PROPERTY_DEFAULT)) && (win.property.active == 0))
			{
				win.property.active = (int) (1);
				Nuklear.Memcopy(win.property.buffer, buffer, (ulong) (*len));
				win.property.length = (int) (*len);
				win.property.cursor = (int) (*cursor);
				win.property.state = (int) (*state);
				win.property.name = (uint) (hash);
				win.property.select_start = (int) (*select_begin);
				win.property.select_end = (int) (*select_end);
				if ((*state) == (Nuklear.NK_PROPERTY_DRAG))
				{
					this.input.mouse.grab = (byte) (Nuklear.nk_true);
					this.input.mouse.grabbed = (byte) (Nuklear.nk_true);
				}
			}

			if (((*state) == (Nuklear.NK_PROPERTY_DEFAULT)) && (old_state != Nuklear.NK_PROPERTY_DEFAULT))
			{
				if ((old_state) == (Nuklear.NK_PROPERTY_DRAG))
				{
					this.input.mouse.grab = (byte) (Nuklear.nk_false);
					this.input.mouse.grabbed = (byte) (Nuklear.nk_false);
					this.input.mouse.ungrab = (byte) (Nuklear.nk_true);
				}
				win.property.select_start = (int) (0);
				win.property.select_end = (int) (0);
				win.property.active = (int) (0);
			}

		}

		public void PropertyInt(char* name, int min, ref int val, int max, int step, float inc_per_pixel)
		{
			PropertyVariant variant = new PropertyVariant();
			if ((((this.current == null)) || (name == null)) || (val == null)) return;
			variant = (PropertyVariant) (Nuklear.PropertyVariantInt((int) (val), (int) (min), (int) (max), (int) (step)));
			Propertyz(name, &variant, (float) (inc_per_pixel), (int) (Nuklear.NK_FILTER_INT));
			val = (int) (variant.value.i);
		}

		public void PropertyFloat(char* name, float min, ref float val, float max, float step, float inc_per_pixel)
		{
			PropertyVariant variant = new PropertyVariant();
			if ((((this.current == null)) || (name == null)) || (val == null)) return;
			variant =
				(PropertyVariant) (Nuklear.PropertyVariantFloat((float) (val), (float) (min), (float) (max), (float) (step)));
			Propertyz(name, &variant, (float) (inc_per_pixel), (int) (Nuklear.NK_FILTER_FLOAT));
			val = (float) (variant.value.f);
		}

		public void PropertyDouble(char* name, double min, ref double val, double max, double step, float inc_per_pixel)
		{
			PropertyVariant variant = new PropertyVariant();
			if ((((this.current == null)) || (name == null)) || (val == null)) return;
			variant =
				(PropertyVariant) (Nuklear.PropertyVariantDouble((double) (val), (double) (min), (double) (max), (double) (step)));
			Propertyz(name, &variant, (float) (inc_per_pixel), (int) (Nuklear.NK_FILTER_FLOAT));
			val = (double) (variant.value.d);
		}

		public int Propertyi(char* name, int min, int val, int max, int step, float inc_per_pixel)
		{
			PropertyVariant variant = new PropertyVariant();
			if (((this.current == null)) || (name == null)) return (int) (val);
			variant = (PropertyVariant) (Nuklear.PropertyVariantInt((int) (val), (int) (min), (int) (max), (int) (step)));
			Propertyz(name, &variant, (float) (inc_per_pixel), (int) (Nuklear.NK_FILTER_INT));
			val = (int) (variant.value.i);
			return (int) (val);
		}

		public float Propertyf(char* name, float min, float val, float max, float step, float inc_per_pixel)
		{
			PropertyVariant variant = new PropertyVariant();
			if (((this.current == null)) || (name == null)) return (float) (val);
			variant =
				(PropertyVariant) (Nuklear.PropertyVariantFloat((float) (val), (float) (min), (float) (max), (float) (step)));
			Propertyz(name, &variant, (float) (inc_per_pixel), (int) (Nuklear.NK_FILTER_FLOAT));
			val = (float) (variant.value.f);
			return (float) (val);
		}

		public double Propertyd(char* name, double min, double val, double max, double step, float inc_per_pixel)
		{
			PropertyVariant variant = new PropertyVariant();
			if (((this.current == null)) || (name == null)) return (double) (val);
			variant =
				(PropertyVariant) (Nuklear.PropertyVariantDouble((double) (val), (double) (min), (double) (max), (double) (step)));
			Propertyz(name, &variant, (float) (inc_per_pixel), (int) (Nuklear.NK_FILTER_FLOAT));
			val = (double) (variant.value.d);
			return (double) (val);
		}

		public int ColorPick(Colorf* color, int fmt)
		{
			Window win;
			Panel layout;
			Style config;
			Input _in_;
			int state;
			Rect bounds = new Rect();
			if ((((this.current == null)) || (this.current.layout == null)) || (color == null)) return (int) (0);
			win = this.current;
			config = this.style;
			layout = win.layout;
			state = (int) (bounds.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			return
				(int)
					(Nuklear.DoColorPicker(ref this.last_widget_state, win.buffer, color, (int) (fmt), (Rect) (bounds),
						(Vec2) (Nuklear.Vec2z((float) (0), (float) (0))), _in_, config.font));
		}

		public Colorf ColorPicker(Colorf color, int fmt)
		{
			ColorPick(&color, (int) (fmt));
			return (Colorf) (color);
		}

		public int ChartBeginColored(int type, Color color, Color highlight, int count, float min_value, float max_value)
		{
			Window win;
			Chart chart;
			Style config;
			StyleChart style;
			StyleItem background;
			Rect bounds = new Rect();
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			if (bounds.Widget(this) == 0)
			{
				chart = this.current.layout.chart;
				return (int) (0);
			}

			win = this.current;
			config = this.style;
			chart = win.layout.chart;
			style = config.chart;

			chart.x = (float) (bounds.x + style.padding.x);
			chart.y = (float) (bounds.y + style.padding.y);
			chart.w = (float) (bounds.w - 2*style.padding.x);
			chart.h = (float) (bounds.h - 2*style.padding.y);
			chart.w = (float) ((chart.w) < (2*style.padding.x) ? (2*style.padding.x) : (chart.w));
			chart.h = (float) ((chart.h) < (2*style.padding.y) ? (2*style.padding.y) : (chart.h));
			{
				ChartSlot slot = chart.slots[chart.slot++];
				slot.type = (int) (type);
				slot.count = (int) (count);
				slot.color = (Color) (color);
				slot.highlight = (Color) (highlight);
				slot.min = (float) ((min_value) < (max_value) ? (min_value) : (max_value));
				slot.max = (float) ((min_value) < (max_value) ? (max_value) : (min_value));
				slot.range = (float) (slot.max - slot.min);
			}

			background = style.background;
			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
			{
				win.buffer.DrawImage((Rect) (bounds), background.data.image, (Color) (Nuklear.nk_white));
			}
			else
			{
				win.buffer.FillRect((Rect) (bounds), (float) (style.rounding), (Color) (style.border_color));
				win.buffer.FillRect((Rect) (bounds.ShrinkRectz((float) (style.border))), (float) (style.rounding),
					(Color) (style.background.data.color));
			}

			return (int) (1);
		}

		public int ChartBegin(int type, int count, float min_value, float max_value)
		{
			return
				(int)
					(ChartBeginColored((int) (type), (Color) (this.style.chart.color), (Color) (this.style.chart.selected_color),
						(int) (count), (float) (min_value), (float) (max_value)));
		}

		public void ChartAddSlotColored(int type, Color color, Color highlight, int count, float min_value, float max_value)
		{
			if (((this.current == null)) || (this.current.layout == null)) return;
			if ((this.current.layout.chart.slot) >= (4)) return;
			{
				Chart chart = this.current.layout.chart;
				ChartSlot slot = chart.slots[chart.slot++];
				slot.type = (int) (type);
				slot.count = (int) (count);
				slot.color = (Color) (color);
				slot.highlight = (Color) (highlight);
				slot.min = (float) ((min_value) < (max_value) ? (min_value) : (max_value));
				slot.max = (float) ((min_value) < (max_value) ? (max_value) : (min_value));
				slot.range = (float) (slot.max - slot.min);
			}

		}

		public void ChartAddSlot(int type, int count, float min_value, float max_value)
		{
			ChartAddSlotColored((int) (type), (Color) (this.style.chart.color), (Color) (this.style.chart.selected_color),
				(int) (count), (float) (min_value), (float) (max_value));
		}

		public uint ChartPushLine(Window win, Chart g, float value, int slot)
		{
			Panel layout = win.layout;
			Input i = this.input;
			CommandBuffer _out_ = win.buffer;
			uint ret = (uint) (0);
			Vec2 cur = new Vec2();
			Rect bounds = new Rect();
			Color color = new Color();
			float step;
			float range;
			float ratio;
			step = (float) (g.w/(float) (g.slots[slot].count));
			range = (float) (g.slots[slot].max - g.slots[slot].min);
			ratio = (float) ((value - g.slots[slot].min)/range);
			if ((g.slots[slot].index) == (0))
			{
				g.slots[slot].last.x = (float) (g.x);
				g.slots[slot].last.y = (float) ((g.y + g.h) - ratio*g.h);
				bounds.x = (float) (g.slots[slot].last.x - 2);
				bounds.y = (float) (g.slots[slot].last.y - 2);
				bounds.w = (float) (bounds.h = (float) (4));
				color = (Color) (g.slots[slot].color);
				if (((layout.flags & Nuklear.NK_WINDOW_ROM) == 0) &&
				    ((((g.slots[slot].last.x - 3) <= (i.mouse.pos.x)) && ((i.mouse.pos.x) < (g.slots[slot].last.x - 3 + 6))) &&
				     (((g.slots[slot].last.y - 3) <= (i.mouse.pos.y)) && ((i.mouse.pos.y) < (g.slots[slot].last.y - 3 + 6)))))
				{
					ret = (uint) ((i.IsMouseHoveringRect((Rect) (bounds))) != 0 ? Nuklear.NK_CHART_HOVERING : 0);
					ret |=
						(uint)
							((((i.mouse.buttons[Nuklear.NK_BUTTON_LEFT].down) != 0) &&
							  ((i.mouse.buttons[Nuklear.NK_BUTTON_LEFT].clicked) != 0))
								? Nuklear.NK_CHART_CLICKED
								: 0);
					color = (Color) (g.slots[slot].highlight);
				}
				_out_.FillRect((Rect) (bounds), (float) (0), (Color) (color));
				g.slots[slot].index += (int) (1);
				return (uint) (ret);
			}

			color = (Color) (g.slots[slot].color);
			cur.x = (float) (g.x + (step*(float) (g.slots[slot].index)));
			cur.y = (float) ((g.y + g.h) - (ratio*g.h));
			_out_.StrokeLine((float) (g.slots[slot].last.x), (float) (g.slots[slot].last.y), (float) (cur.x), (float) (cur.y),
				(float) (1.0f), (Color) (color));
			bounds.x = (float) (cur.x - 3);
			bounds.y = (float) (cur.y - 3);
			bounds.w = (float) (bounds.h = (float) (6));
			if ((layout.flags & Nuklear.NK_WINDOW_ROM) == 0)
			{
				if ((i.IsMouseHoveringRect((Rect) (bounds))) != 0)
				{
					ret = (uint) (Nuklear.NK_CHART_HOVERING);
					ret |=
						(uint)
							(((i.mouse.buttons[Nuklear.NK_BUTTON_LEFT].down == 0) && ((i.mouse.buttons[Nuklear.NK_BUTTON_LEFT].clicked) != 0))
								? Nuklear.NK_CHART_CLICKED
								: 0);
					color = (Color) (g.slots[slot].highlight);
				}
			}

			_out_.FillRect((Rect) (Nuklear.Rectz((float) (cur.x - 2), (float) (cur.y - 2), (float) (4), (float) (4))),
				(float) (0), (Color) (color));
			g.slots[slot].last.x = (float) (cur.x);
			g.slots[slot].last.y = (float) (cur.y);
			g.slots[slot].index += (int) (1);
			return (uint) (ret);
		}

		public uint ChartPushColumn(Window win, Chart chart, float value, int slot)
		{
			CommandBuffer _out_ = win.buffer;
			Input _in_ = this.input;
			Panel layout = win.layout;
			float ratio;
			uint ret = (uint) (0);
			Color color = new Color();
			Rect item = new Rect();
			if ((chart.slots[slot].index) >= (chart.slots[slot].count)) return (uint) (Nuklear.nk_false);
			if ((chart.slots[slot].count) != 0)
			{
				float padding = (float) (chart.slots[slot].count - 1);
				item.w = (float) ((chart.w - padding)/(float) (chart.slots[slot].count));
			}

			color = (Color) (chart.slots[slot].color);
			item.h =
				(float)
					(chart.h*
					 (((value/chart.slots[slot].range) < (0)) ? -(value/chart.slots[slot].range) : (value/chart.slots[slot].range)));
			if ((value) >= (0))
			{
				ratio =
					(float)
						((value + (((chart.slots[slot].min) < (0)) ? -(chart.slots[slot].min) : (chart.slots[slot].min)))/
						 (((chart.slots[slot].range) < (0)) ? -(chart.slots[slot].range) : (chart.slots[slot].range)));
				item.y = (float) ((chart.y + chart.h) - chart.h*ratio);
			}
			else
			{
				ratio = (float) ((value - chart.slots[slot].max)/chart.slots[slot].range);
				item.y = (float) (chart.y + (chart.h*(((ratio) < (0)) ? -(ratio) : (ratio))) - item.h);
			}

			item.x = (float) (chart.x + ((float) (chart.slots[slot].index)*item.w));
			item.x = (float) (item.x + ((float) (chart.slots[slot].index)));
			if (((layout.flags & Nuklear.NK_WINDOW_ROM) == 0) &&
			    ((((item.x) <= (_in_.mouse.pos.x)) && ((_in_.mouse.pos.x) < (item.x + item.w))) &&
			     (((item.y) <= (_in_.mouse.pos.y)) && ((_in_.mouse.pos.y) < (item.y + item.h)))))
			{
				ret = (uint) (Nuklear.NK_CHART_HOVERING);
				ret |=
					(uint)
						(((((MouseButton*) _in_.mouse.buttons + Nuklear.NK_BUTTON_LEFT)->down == 0) &&
						  ((((MouseButton*) _in_.mouse.buttons + Nuklear.NK_BUTTON_LEFT)->clicked) != 0))
							? Nuklear.NK_CHART_CLICKED
							: 0);
				color = (Color) (chart.slots[slot].highlight);
			}

			_out_.FillRect((Rect) (item), (float) (0), (Color) (color));
			chart.slots[slot].index += (int) (1);
			return (uint) (ret);
		}

		public uint ChartPushSlot(float value, int slot)
		{
			uint flags;
			Window win;
			if (((this.current == null)) || ((slot) >= (4))) return (uint) (Nuklear.nk_false);
			if ((slot) >= (this.current.layout.chart.slot)) return (uint) (Nuklear.nk_false);
			win = this.current;
			if ((win.layout.chart.slot) < (slot)) return (uint) (Nuklear.nk_false);
			switch (win.layout.chart.slots[slot].type)
			{
				case Nuklear.NK_CHART_LINES:
					flags = (uint) (ChartPushLine(win, win.layout.chart, (float) (value), (int) (slot)));
					break;
				case Nuklear.NK_CHART_COLUMN:
					flags = (uint) (ChartPushColumn(win, win.layout.chart, (float) (value), (int) (slot)));
					break;
				default:
				case Nuklear.NK_CHART_MAX:
					flags = (uint) (0);
					break;
			}

			return (uint) (flags);
		}

		public uint ChartPush(float value)
		{
			return (uint) (ChartPushSlot((float) (value), (int) (0)));
		}

		public void ChartEnd()
		{
			Window win;
			Chart chart;
			if ((this.current == null)) return;
			win = this.current;
			chart = win.layout.chart;

			return;
		}

		public void Plot(int type, float* values, int count, int offset)
		{
			int i = (int) (0);
			float min_value;
			float max_value;
			if (((values == null)) || (count == 0)) return;
			min_value = (float) (values[offset]);
			max_value = (float) (values[offset]);
			for (i = (int) (0); (i) < (count); ++i)
			{
				min_value = (float) ((values[i + offset]) < (min_value) ? (values[i + offset]) : (min_value));
				max_value = (float) ((values[i + offset]) < (max_value) ? (max_value) : (values[i + offset]));
			}
			if ((ChartBegin((int) (type), (int) (count), (float) (min_value), (float) (max_value))) != 0)
			{
				for (i = (int) (0); (i) < (count); ++i)
				{
					ChartPush((float) (values[i + offset]));
				}
				ChartEnd();
			}

		}

		public void PlotFunction(int type, void* userdata, NkFloatValueGetter value_getter, int count, int offset)
		{
			int i = (int) (0);
			float min_value;
			float max_value;
			if (((value_getter == null)) || (count == 0)) return;
			max_value = (float) (min_value = (float) (value_getter(userdata, (int) (offset))));
			for (i = (int) (0); (i) < (count); ++i)
			{
				float value = (float) (value_getter(userdata, (int) (i + offset)));
				min_value = (float) ((value) < (min_value) ? (value) : (min_value));
				max_value = (float) ((value) < (max_value) ? (max_value) : (value));
			}
			if ((ChartBegin((int) (type), (int) (count), (float) (min_value), (float) (max_value))) != 0)
			{
				for (i = (int) (0); (i) < (count); ++i)
				{
					ChartPush((float) (value_getter(userdata, (int) (i + offset))));
				}
				ChartEnd();
			}

		}

		public int GroupScrolledOffsetBegin(Scroll offset, char* title, uint flags)
		{
			Rect bounds = new Rect();
			Window panel = new Window();
			Window win;
			win = this.current;
			bounds.PanelAllocSpace(this);
			{
				if (
					(!(!(((((bounds.x) > (win.layout.clip.x + win.layout.clip.w)) || ((bounds.x + bounds.w) < (win.layout.clip.x))) ||
					      ((bounds.y) > (win.layout.clip.y + win.layout.clip.h))) || ((bounds.y + bounds.h) < (win.layout.clip.y))))) &&
					((flags & Nuklear.NK_WINDOW_MOVABLE) == 0))
				{
					return (int) (0);
				}
			}

			if ((win.flags & Nuklear.NK_WINDOW_ROM) != 0) flags |= (uint) (Nuklear.NK_WINDOW_ROM);

			panel.bounds = (Rect) (bounds);
			panel.flags = (uint) (flags);
			panel.scrollbar.x = offset.x;
			panel.scrollbar.y = offset.y;
			panel.buffer = (CommandBuffer) (win.buffer);
			panel.layout = (Panel) (CreatePanel());
			this.current = panel;
			PanelBegin((flags & Nuklear.NK_WINDOW_TITLE) != 0 ? title : null, (int) (Nuklear.NK_PANEL_GROUP));
			win.buffer = (CommandBuffer) (panel.buffer);
			win.buffer.clip = (Rect) (panel.layout.clip);
			panel.layout.offset = offset;

			panel.layout.parent = win.layout;
			win.layout = panel.layout;
			this.current = win;
			if (((panel.layout.flags & Nuklear.NK_WINDOW_CLOSED) != 0) ||
			    ((panel.layout.flags & Nuklear.NK_WINDOW_MINIMIZED) != 0))
			{
				uint f = (uint) (panel.layout.flags);
				GroupScrolledEnd();
				if ((f & Nuklear.NK_WINDOW_CLOSED) != 0) return (int) (Nuklear.NK_WINDOW_CLOSED);
				if ((f & Nuklear.NK_WINDOW_MINIMIZED) != 0) return (int) (Nuklear.NK_WINDOW_MINIMIZED);
			}

			return (int) (1);
		}

		public void GroupScrolledEnd()
		{
			Window win;
			Panel parent;
			Panel g;
			Rect clip = new Rect();
			Window pan = new Window();
			Vec2 panel_padding = new Vec2();
			if ((this.current == null)) return;
			win = this.current;
			g = win.layout;
			parent = g.parent;

			panel_padding = (Vec2) (this.style.PanelGetPadding((int) (Nuklear.NK_PANEL_GROUP)));
			pan.bounds.y = (float) (g.bounds.y - (g.header_height + g.menu.h));
			pan.bounds.x = (float) (g.bounds.x - panel_padding.x);
			pan.bounds.w = (float) (g.bounds.w + 2*panel_padding.x);
			pan.bounds.h = (float) (g.bounds.h + g.header_height + g.menu.h);
			if ((g.flags & Nuklear.NK_WINDOW_BORDER) != 0)
			{
				pan.bounds.x -= (float) (g.border);
				pan.bounds.y -= (float) (g.border);
				pan.bounds.w += (float) (2*g.border);
				pan.bounds.h += (float) (2*g.border);
			}

			if ((g.flags & Nuklear.NK_WINDOW_NO_SCROLLBAR) == 0)
			{
				pan.bounds.w += (float) (this.style.window.scrollbar_size.x);
				pan.bounds.h += (float) (this.style.window.scrollbar_size.y);
			}

			pan.scrollbar.x = (uint) (g.offset.x);
			pan.scrollbar.y = (uint) (g.offset.y);
			pan.flags = (uint) (g.flags);
			pan.buffer = (CommandBuffer) (win.buffer);
			pan.layout = g;
			pan.parent = win;
			this.current = pan;
			clip.Unify(ref parent.clip, (float) (pan.bounds.x), (float) (pan.bounds.y), (float) (pan.bounds.x + pan.bounds.w),
				(float) (pan.bounds.y + pan.bounds.h + panel_padding.x));
			pan.buffer.PushScissor((Rect) (clip));
			End();
			win.buffer = (CommandBuffer) (pan.buffer);
			win.buffer.PushScissor((Rect) (parent.clip));
			this.current = win;
			win.layout = parent;
			g.bounds = (Rect) (pan.bounds);
			return;
		}

		public int GroupScrolledBegin(Scroll scroll, char* title, uint flags)
		{
			return (int) (GroupScrolledOffsetBegin(scroll, title, (uint) (flags)));
		}

		public int GroupBeginTitled(char* id, char* title, uint flags)
		{
			int id_len;
			uint id_hash;
			Window win;
			uint* x_offset;
			uint* y_offset;
			if ((((this.current == null)) || (this.current.layout == null)) || (id == null)) return (int) (0);
			win = this.current;
			id_len = (int) (Nuklear.Strlen(id));
			id_hash = (uint) (Nuklear.MurmurHash(id, (int) (id_len), (uint) (Nuklear.NK_PANEL_GROUP)));
			x_offset = win.FindValue((uint) (id_hash));
			if (x_offset == null)
			{
				x_offset = AddValue(win, (uint) (id_hash), (uint) (0));
				y_offset = AddValue(win, (uint) (id_hash + 1), (uint) (0));
				if ((x_offset == null) || (y_offset == null)) return (int) (0);
				*x_offset = (uint) (*y_offset = (uint) (0));
			}
			else y_offset = win.FindValue((uint) (id_hash + 1));
			return (int) (GroupScrolledOffsetBegin(new Scroll {x = *x_offset, y = *y_offset}, title, (uint) (flags)));
		}

		public int GroupBegin(char* title, uint flags)
		{
			return (int) (GroupBeginTitled(title, title, (uint) (flags)));
		}

		public void GroupEnd()
		{
			GroupScrolledEnd();
		}

		public int ListViewBegin(ListView view, char* title, uint flags, int row_height, int row_count)
		{
			int title_len;
			uint title_hash;
			uint* x_offset;
			uint* y_offset;
			int result;
			Window win;
			Panel layout;
			Style style;
			Vec2 item_spacing = new Vec2();
			if (((view == null)) || (title == null)) return (int) (0);
			win = this.current;
			style = this.style;
			item_spacing = (Vec2) (style.window.spacing);
			row_height += (int) ((0) < ((int) (item_spacing.y)) ? ((int) (item_spacing.y)) : (0));
			title_len = (int) (Nuklear.Strlen(title));
			title_hash = (uint) (Nuklear.MurmurHash(title, (int) (title_len), (uint) (Nuklear.NK_PANEL_GROUP)));
			x_offset = win.FindValue((uint) (title_hash));
			if (x_offset == null)
			{
				x_offset = AddValue(win, (uint) (title_hash), (uint) (0));
				y_offset = AddValue(win, (uint) (title_hash + 1), (uint) (0));
				if ((x_offset == null) || (y_offset == null)) return (int) (0);
				*x_offset = (uint) (*y_offset = (uint) (0));
			}
			else y_offset = win.FindValue((uint) (title_hash + 1));
			view.scroll_value = *y_offset;
			view.scroll_pointer = y_offset;
			*y_offset = (uint) (0);
			result = (int) (GroupScrolledOffsetBegin(new Scroll {x = *x_offset, y = *y_offset}, title, (uint) (flags)));
			win = this.current;
			layout = win.layout;
			view.total_height = (int) (row_height*((row_count) < (1) ? (1) : (row_count)));
			view.begin =
				((int)
					(((float) (view.scroll_value)/(float) (row_height)) < (0.0f)
						? (0.0f)
						: ((float) (view.scroll_value)/(float) (row_height))));
			view.count =
				(int)
					((Nuklear.Iceilf((float) ((layout.clip.h)/(float) (row_height)))) < (0)
						? (0)
						: (Nuklear.Iceilf((float) ((layout.clip.h)/(float) (row_height)))));
			view.end = (int) (view.begin + view.count);
			view.ctx = this;
			return (int) (result);
		}

		public int PopupBegin(int type, char* title, uint flags, Rect rect)
		{
			Window popup;
			Window win;
			Panel panel;
			int title_len;
			uint title_hash;
			ulong allocated;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			panel = win.layout;
			title_len = (int) (Nuklear.Strlen(title));
			title_hash = (uint) (Nuklear.MurmurHash(title, (int) (title_len), (uint) (Nuklear.NK_PANEL_POPUP)));
			popup = win.popup.win;
			if (popup == null)
			{
				popup = (Window) (CreateWindow());
				popup.parent = win;
				win.popup.win = popup;
				win.popup.active = (int) (0);
				win.popup.type = (int) (Nuklear.NK_PANEL_POPUP);
			}

			if (win.popup.name != title_hash)
			{
				if (win.popup.active == 0)
				{
					win.popup.name = (uint) (title_hash);
					win.popup.active = (int) (1);
					win.popup.type = (int) (Nuklear.NK_PANEL_POPUP);
				}
				else return (int) (0);
			}

			this.current = popup;
			rect.x += (float) (win.layout.clip.x);
			rect.y += (float) (win.layout.clip.y);
			popup.parent = win;
			popup.bounds = (Rect) (rect);
			popup.seq = (uint) (this.seq);
			popup.layout = (Panel) (CreatePanel());
			popup.flags = (uint) (flags);
			popup.flags |= (uint) (Nuklear.NK_WINDOW_BORDER);
			if ((type) == (Nuklear.NK_POPUP_DYNAMIC)) popup.flags |= (uint) (Nuklear.NK_WINDOW_DYNAMIC);
			popup.buffer = (CommandBuffer) (win.buffer);
			StartPopup(win);
			allocated = (ulong) (this.memory.allocated);
			popup.buffer.PushScissor((Rect) (Nuklear.nk_null_rect));
			if ((PanelBegin(title, (int) (Nuklear.NK_PANEL_POPUP))) != 0)
			{
				Panel root;
				root = win.layout;
				while ((root) != null)
				{
					root.flags |= (uint) (Nuklear.NK_WINDOW_ROM);
					root.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_REMOVE_ROM));
					root = root.parent;
				}
				win.popup.active = (int) (1);
				popup.layout.offset = popup.scrollbar;
				popup.layout.parent = win.layout;
				return (int) (1);
			}
			else
			{
				Panel root;
				root = win.layout;
				while ((root) != null)
				{
					root.flags |= (uint) (Nuklear.NK_WINDOW_REMOVE_ROM);
					root = root.parent;
				}
				win.popup.buf.active = (int) (0);
				win.popup.active = (int) (0);
				this.memory.allocated = (ulong) (allocated);
				this.current = win;
				FreePanel(popup.layout);
				popup.layout = null;
				return (int) (0);
			}

		}

		public int NonblockBegin(uint flags, Rect body, Rect header, int panel_type)
		{
			Window popup;
			Window win;
			Panel panel;
			int is_active = (int) (Nuklear.nk_true);
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			panel = win.layout;
			popup = win.popup.win;
			if (popup == null)
			{
				popup = (Window) (CreateWindow());
				popup.parent = win;
				win.popup.win = popup;
				win.popup.type = (int) (panel_type);
				popup.buffer.Init(this.memory, (int) (Nuklear.NK_CLIPPING_ON));
			}
			else
			{
				int pressed;
				int in_body;
				int in_header;
				pressed = (int) (this.input.IsMousePressed((int) (Nuklear.NK_BUTTON_LEFT)));
				in_body = (int) (this.input.IsMouseHoveringRect((Rect) (body)));
				in_header = (int) (this.input.IsMouseHoveringRect((Rect) (header)));
				if (((pressed) != 0) && ((in_body == 0) || ((in_header) != 0))) is_active = (int) (Nuklear.nk_false);
			}

			win.popup.header = (Rect) (header);
			if (is_active == 0)
			{
				Panel root = win.layout;
				while ((root) != null)
				{
					root.flags |= (uint) (Nuklear.NK_WINDOW_REMOVE_ROM);
					root = root.parent;
				}
				return (int) (is_active);
			}

			popup.bounds = (Rect) (body);
			popup.parent = win;
			popup.layout = (Panel) (CreatePanel());
			popup.flags = (uint) (flags);
			popup.flags |= (uint) (Nuklear.NK_WINDOW_BORDER);
			popup.flags |= (uint) (Nuklear.NK_WINDOW_DYNAMIC);
			popup.seq = (uint) (this.seq);
			win.popup.active = (int) (1);
			StartPopup(win);
			popup.buffer = (CommandBuffer) (win.buffer);
			popup.buffer.PushScissor((Rect) (Nuklear.nk_null_rect));
			this.current = popup;
			PanelBegin(null, (int) (panel_type));
			win.buffer = (CommandBuffer) (popup.buffer);
			popup.layout.parent = win.layout;
			popup.layout.offset = popup.scrollbar;

			{
				Panel root;
				root = win.layout;
				while ((root) != null)
				{
					root.flags |= (uint) (Nuklear.NK_WINDOW_ROM);
					root = root.parent;
				}
			}

			return (int) (is_active);
		}

		public void PopupClose()
		{
			Window popup;
			if ((this.current == null)) return;
			popup = this.current;
			popup.flags |= (uint) (Nuklear.NK_WINDOW_HIDDEN);
		}

		public void PopupEnd()
		{
			Window win;
			Window popup;
			if (((this.current == null)) || (this.current.layout == null)) return;
			popup = this.current;
			if (popup.parent == null) return;
			win = popup.parent;
			if ((popup.flags & Nuklear.NK_WINDOW_HIDDEN) != 0)
			{
				Panel root;
				root = win.layout;
				while ((root) != null)
				{
					root.flags |= (uint) (Nuklear.NK_WINDOW_REMOVE_ROM);
					root = root.parent;
				}
				win.popup.active = (int) (0);
			}

			popup.buffer.PushScissor((Rect) (Nuklear.nk_null_rect));
			End();
			win.buffer = (CommandBuffer) (popup.buffer);
			FinishPopup(win);
			this.current = win;
			win.buffer.PushScissor((Rect) (win.layout.clip));
		}

		public int TooltipBegin(float width)
		{
			int x;
			int y;
			int w;
			int h;
			Window win;
			Input _in_;
			Rect bounds = new Rect();
			int ret;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			_in_ = this.input;
			if (((win.popup.win) != null) && ((win.popup.type & Nuklear.NK_PANEL_SET_NONBLOCK) != 0)) return (int) (0);
			w = (int) (Nuklear.Iceilf((float) (width)));
			h = (int) (Nuklear.Iceilf((float) (Nuklear.nk_null_rect.h)));
			x = (int) (Nuklear.Ifloorf((float) (_in_.mouse.pos.x + 1)) - (int) (win.layout.clip.x));
			y = (int) (Nuklear.Ifloorf((float) (_in_.mouse.pos.y + 1)) - (int) (win.layout.clip.y));
			bounds.x = ((float) (x));
			bounds.y = ((float) (y));
			bounds.w = ((float) (w));
			bounds.h = ((float) (h));
			ret =
				(int)
					(PopupBegin((int) (Nuklear.NK_POPUP_DYNAMIC), "__##Tooltip##__",
						(uint) (Nuklear.NK_WINDOW_NO_SCROLLBAR | Nuklear.NK_WINDOW_BORDER), (Rect) (bounds)));
			if ((ret) != 0) win.layout.flags &= (uint) (~(uint) (Nuklear.NK_WINDOW_ROM));
			win.popup.type = (int) (Nuklear.NK_PANEL_TOOLTIP);
			this.current.layout.type = (int) (Nuklear.NK_PANEL_TOOLTIP);
			return (int) (ret);
		}

		public void TooltipEnd()
		{
			if ((this.current == null)) return;
			this.current.seq--;
			PopupClose();
			PopupEnd();
		}

		public void Tooltip(char* text)
		{
			Style style;
			Vec2 padding = new Vec2();
			int text_len;
			float text_width;
			float text_height;
			if ((((this.current == null)) || (this.current.layout == null)) || (text == null)) return;
			style = this.style;
			padding = (Vec2) (style.window.padding);
			text_len = (int) (Nuklear.Strlen(text));
			text_width =
				(float) (style.font.width((Handle) (style.font.userdata), (float) (style.font.height), text, (int) (text_len)));
			text_width += (float) (4*padding.x);
			text_height = (float) (style.font.height + 2*padding.y);
			if ((TooltipBegin((float) (text_width))) != 0)
			{
				LayoutRowDynamic((float) (text_height), (int) (1));
				Textz(text, (int) (text_len), (uint) (Nuklear.NK_TEXT_LEFT));
				TooltipEnd();
			}

		}

		public int ContextualBegin(uint flags, Vec2 size, Rect trigger_bounds)
		{
			Window win;
			Window popup;
			Rect body = new Rect();
			Rect null_rect = new Rect();
			int is_clicked = (int) (0);
			int is_active = (int) (0);
			int is_open = (int) (0);
			int ret = (int) (0);
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			++win.popup.con_count;
			popup = win.popup.win;
			is_open = (int) (((popup) != null) && ((win.popup.type) == (Nuklear.NK_PANEL_CONTEXTUAL)) ? 1 : 0);
			is_clicked = (int) (this.input.MouseClicked((int) (Nuklear.NK_BUTTON_RIGHT), (Rect) (trigger_bounds)));
			if (((win.popup.active_con) != 0) && (win.popup.con_count != win.popup.active_con)) return (int) (0);
			if (((((is_clicked) != 0) && ((is_open) != 0)) && (is_active == 0)) ||
			    (((is_open == 0) && (is_active == 0)) && (is_clicked == 0))) return (int) (0);
			win.popup.active_con = (uint) (win.popup.con_count);
			if ((is_clicked) != 0)
			{
				body.x = (float) (this.input.mouse.pos.x);
				body.y = (float) (this.input.mouse.pos.y);
			}
			else
			{
				body.x = (float) (popup.bounds.x);
				body.y = (float) (popup.bounds.y);
			}

			body.w = (float) (size.x);
			body.h = (float) (size.y);
			ret =
				(int)
					(NonblockBegin((uint) (flags | Nuklear.NK_WINDOW_NO_SCROLLBAR), (Rect) (body), (Rect) (null_rect),
						(int) (Nuklear.NK_PANEL_CONTEXTUAL)));
			if ((ret) != 0) win.popup.type = (int) (Nuklear.NK_PANEL_CONTEXTUAL);
			else
			{
				win.popup.active_con = (uint) (0);
				if ((win.popup.win) != null) win.popup.win.flags = (uint) (0);
			}

			return (int) (ret);
		}

		public int ContextualItemText(char* text, int len, uint alignment)
		{
			Window win;
			Input _in_;
			Style style;
			Rect bounds = new Rect();
			int state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			style = this.style;
			state = (int) (bounds.WidgetFitting(this, (Vec2) (style.contextual_button.padding)));
			if (state == 0) return (int) (Nuklear.nk_false);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0))
				? null
				: this.input;
			if (
				(Nuklear.DoButtonText(ref this.last_widget_state, win.buffer, (Rect) (bounds), text, (int) (len), (uint) (alignment),
					(int) (Nuklear.NK_BUTTON_DEFAULT), style.contextual_button, _in_, style.font)) != 0)
			{
				ContextualClose();
				return (int) (Nuklear.nk_true);
			}

			return (int) (Nuklear.nk_false);
		}

		public int ContextualItemLabel(char* label, uint align)
		{
			return (int) (ContextualItemText(label, (int) (Nuklear.Strlen(label)), (uint) (align)));
		}

		public int ContextualItemImageText(Image img, char* text, int len, uint align)
		{
			Window win;
			Input _in_;
			Style style;
			Rect bounds = new Rect();
			int state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			style = this.style;
			state = (int) (bounds.WidgetFitting(this, (Vec2) (style.contextual_button.padding)));
			if (state == 0) return (int) (Nuklear.nk_false);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0))
				? null
				: this.input;
			if (
				(Nuklear.DoButtonTextImage(ref this.last_widget_state, win.buffer, (Rect) (bounds), (Image) (img), text, (int) (len),
					(uint) (align), (int) (Nuklear.NK_BUTTON_DEFAULT), style.contextual_button, style.font, _in_)) != 0)
			{
				ContextualClose();
				return (int) (Nuklear.nk_true);
			}

			return (int) (Nuklear.nk_false);
		}

		public int ContextualItemImageLabel(Image img, char* label, uint align)
		{
			return (int) (ContextualItemImageText((Image) (img), label, (int) (Nuklear.Strlen(label)), (uint) (align)));
		}

		public int ContextualItemSymbolText(int symbol, char* text, int len, uint align)
		{
			Window win;
			Input _in_;
			Style style;
			Rect bounds = new Rect();
			int state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			style = this.style;
			state = (int) (bounds.WidgetFitting(this, (Vec2) (style.contextual_button.padding)));
			if (state == 0) return (int) (Nuklear.nk_false);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0))
				? null
				: this.input;
			if (
				(Nuklear.DoButtonTextSymbol(ref this.last_widget_state, win.buffer, (Rect) (bounds), (int) (symbol), text,
					(int) (len), (uint) (align), (int) (Nuklear.NK_BUTTON_DEFAULT), style.contextual_button, style.font, _in_)) != 0)
			{
				ContextualClose();
				return (int) (Nuklear.nk_true);
			}

			return (int) (Nuklear.nk_false);
		}

		public int ContextualItemSymbolLabel(int symbol, char* text, uint align)
		{
			return (int) (ContextualItemSymbolText((int) (symbol), text, (int) (Nuklear.Strlen(text)), (uint) (align)));
		}

		public void ContextualClose()
		{
			if (((this.current == null)) || (this.current.layout == null)) return;
			PopupClose();
		}

		public void ContextualEnd()
		{
			Window popup;
			Panel panel;
			if ((this.current == null)) return;
			popup = this.current;
			panel = popup.layout;
			if ((panel.flags & Nuklear.NK_WINDOW_DYNAMIC) != 0)
			{
				Rect body = new Rect();
				if ((panel.at_y) < (panel.bounds.y + panel.bounds.h))
				{
					Vec2 padding = (Vec2) (this.style.PanelGetPadding((int) (panel.type)));
					body = (Rect) (panel.bounds);
					body.y = (float) (panel.at_y + panel.footer_height + panel.border + padding.y + panel.row.height);
					body.h = (float) ((panel.bounds.y + panel.bounds.h) - body.y);
				}
				{
					int pressed = (int) (this.input.IsMousePressed((int) (Nuklear.NK_BUTTON_LEFT)));
					int in_body = (int) (this.input.IsMouseHoveringRect((Rect) (body)));
					if (((pressed) != 0) && ((in_body) != 0)) popup.flags |= (uint) (Nuklear.NK_WINDOW_HIDDEN);
				}
			}

			if ((popup.flags & Nuklear.NK_WINDOW_HIDDEN) != 0) popup.seq = (uint) (0);
			PopupEnd();
			return;
		}

		public int ComboBegin(Window win, Vec2 size, int is_clicked, Rect header)
		{
			Window popup;
			int is_open = (int) (0);
			int is_active = (int) (0);
			Rect body = new Rect();
			uint hash;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			popup = win.popup.win;
			body.x = (float) (header.x);
			body.w = (float) (size.x);
			body.y = (float) (header.y + header.h - this.style.window.combo_border);
			body.h = (float) (size.y);
			hash = (uint) (win.popup.combo_count++);
			is_open = (int) ((popup != null) ? Nuklear.nk_true : Nuklear.nk_false);
			is_active =
				(int)
					((((popup) != null) && ((win.popup.name) == (hash))) && ((win.popup.type) == (Nuklear.NK_PANEL_COMBO)) ? 1 : 0);
			if ((((((is_clicked) != 0) && ((is_open) != 0)) && (is_active == 0)) || (((is_open) != 0) && (is_active == 0))) ||
			    (((is_open == 0) && (is_active == 0)) && (is_clicked == 0))) return (int) (0);
			if (
				NonblockBegin((uint) (0), (Rect) (body),
					(Rect)
						((((is_clicked) != 0) && ((is_open) != 0))
							? Nuklear.Rectz((float) (0), (float) (0), (float) (0), (float) (0))
							: header), (int) (Nuklear.NK_PANEL_COMBO)) == 0) return (int) (0);
			win.popup.type = (int) (Nuklear.NK_PANEL_COMBO);
			win.popup.name = (uint) (hash);
			return (int) (1);
		}

		public int ComboBeginText(char* selected, int len, Vec2 size)
		{
			Input _in_;
			Window win;
			Style style;
			int s;
			int is_clicked = (int) (Nuklear.nk_false);
			Rect header = new Rect();
			StyleItem background;
			Text text = new Text();
			if ((((this.current == null)) || (this.current.layout == null)) || (selected == null)) return (int) (0);
			win = this.current;
			style = this.style;
			s = (int) (header.Widget(this));
			if ((s) == (Nuklear.NK_WIDGET_INVALID)) return (int) (0);
			_in_ = (((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0) || ((s) == (Nuklear.NK_WIDGET_ROM))) ? null : this.input;
			if ((Nuklear.ButtonBehavior(ref this.last_widget_state, (Rect) (header), _in_, (int) (Nuklear.NK_BUTTON_DEFAULT))) !=
			    0) is_clicked = (int) (Nuklear.nk_true);
			if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.combo.active;
				text.text = (Color) (style.combo.label_active);
			}
			else if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.combo.hover;
				text.text = (Color) (style.combo.label_hover);
			}
			else
			{
				background = style.combo.normal;
				text.text = (Color) (style.combo.label_normal);
			}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
			{
				text.background = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
				win.buffer.DrawImage((Rect) (header), background.data.image, (Color) (Nuklear.nk_white));
			}
			else
			{
				text.background = (Color) (background.data.color);
				win.buffer.FillRect((Rect) (header), (float) (style.combo.rounding), (Color) (background.data.color));
				win.buffer.StrokeRect((Rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(Color) (style.combo.border_color));
			}

			{
				Rect label = new Rect();
				Rect button = new Rect();
				Rect content = new Rect();
				int sym;
				if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.x);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				text.padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
				label.x = (float) (header.x + style.combo.content_padding.x);
				label.y = (float) (header.y + style.combo.content_padding.y);
				label.w = (float) (button.x - (style.combo.content_padding.x + style.combo.spacing.x) - label.x);
				label.h = (float) (header.h - 2*style.combo.content_padding.y);
				win.buffer.WidgetText((Rect) (label), selected, (int) (len), &text, (uint) (Nuklear.NK_TEXT_LEFT), this.style.font);
				win.buffer.DrawButtonSymbol(&button, &content, (uint) (this.last_widget_state), this.style.combo.button, (int) (sym),
					style.font);
			}

			return (int) (ComboBegin(win, (Vec2) (size), (int) (is_clicked), (Rect) (header)));
		}

		public int ComboBeginLabel(char* selected, Vec2 size)
		{
			return (int) (ComboBeginText(selected, (int) (Nuklear.Strlen(selected)), (Vec2) (size)));
		}

		public int ComboBeginColor(Color color, Vec2 size)
		{
			Window win;
			Style style;
			Input _in_;
			Rect header = new Rect();
			int is_clicked = (int) (Nuklear.nk_false);
			int s;
			StyleItem background;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			style = this.style;
			s = (int) (header.Widget(this));
			if ((s) == (Nuklear.NK_WIDGET_INVALID)) return (int) (0);
			_in_ = (((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0) || ((s) == (Nuklear.NK_WIDGET_ROM))) ? null : this.input;
			if ((Nuklear.ButtonBehavior(ref this.last_widget_state, (Rect) (header), _in_, (int) (Nuklear.NK_BUTTON_DEFAULT))) !=
			    0) is_clicked = (int) (Nuklear.nk_true);
			if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) background = style.combo.active;
			else if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) background = style.combo.hover;
			else background = style.combo.normal;
			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
			{
				win.buffer.DrawImage((Rect) (header), background.data.image, (Color) (Nuklear.nk_white));
			}
			else
			{
				win.buffer.FillRect((Rect) (header), (float) (style.combo.rounding), (Color) (background.data.color));
				win.buffer.StrokeRect((Rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(Color) (style.combo.border_color));
			}

			{
				Rect content = new Rect();
				Rect button = new Rect();
				Rect bounds = new Rect();
				int sym;
				if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.x);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				bounds.h = (float) (header.h - 4*style.combo.content_padding.y);
				bounds.y = (float) (header.y + 2*style.combo.content_padding.y);
				bounds.x = (float) (header.x + 2*style.combo.content_padding.x);
				bounds.w = (float) ((button.x - (style.combo.content_padding.x + style.combo.spacing.x)) - bounds.x);
				win.buffer.FillRect((Rect) (bounds), (float) (0), (Color) (color));
				win.buffer.DrawButtonSymbol(&button, &content, (uint) (this.last_widget_state), this.style.combo.button, (int) (sym),
					style.font);
			}

			return (int) (ComboBegin(win, (Vec2) (size), (int) (is_clicked), (Rect) (header)));
		}

		public int ComboBeginSymbol(int symbol, Vec2 size)
		{
			Window win;
			Style style;
			Input _in_;
			Rect header = new Rect();
			int is_clicked = (int) (Nuklear.nk_false);
			int s;
			StyleItem background;
			Color sym_background = new Color();
			Color symbol_color = new Color();
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			style = this.style;
			s = (int) (header.Widget(this));
			if ((s) == (Nuklear.NK_WIDGET_INVALID)) return (int) (0);
			_in_ = (((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0) || ((s) == (Nuklear.NK_WIDGET_ROM))) ? null : this.input;
			if ((Nuklear.ButtonBehavior(ref this.last_widget_state, (Rect) (header), _in_, (int) (Nuklear.NK_BUTTON_DEFAULT))) !=
			    0) is_clicked = (int) (Nuklear.nk_true);
			if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.combo.active;
				symbol_color = (Color) (style.combo.symbol_active);
			}
			else if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.combo.hover;
				symbol_color = (Color) (style.combo.symbol_hover);
			}
			else
			{
				background = style.combo.normal;
				symbol_color = (Color) (style.combo.symbol_hover);
			}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
			{
				sym_background = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
				win.buffer.DrawImage((Rect) (header), background.data.image, (Color) (Nuklear.nk_white));
			}
			else
			{
				sym_background = (Color) (background.data.color);
				win.buffer.FillRect((Rect) (header), (float) (style.combo.rounding), (Color) (background.data.color));
				win.buffer.StrokeRect((Rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(Color) (style.combo.border_color));
			}

			{
				Rect bounds = new Rect();
				Rect content = new Rect();
				Rect button = new Rect();
				int sym;
				if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.y);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				bounds.h = (float) (header.h - 2*style.combo.content_padding.y);
				bounds.y = (float) (header.y + style.combo.content_padding.y);
				bounds.x = (float) (header.x + style.combo.content_padding.x);
				bounds.w = (float) ((button.x - style.combo.content_padding.y) - bounds.x);
				win.buffer.DrawSymbol((int) (symbol), (Rect) (bounds), (Color) (sym_background), (Color) (symbol_color),
					(float) (1.0f), style.font);
				win.buffer.DrawButtonSymbol(&bounds, &content, (uint) (this.last_widget_state), this.style.combo.button, (int) (sym),
					style.font);
			}

			return (int) (ComboBegin(win, (Vec2) (size), (int) (is_clicked), (Rect) (header)));
		}

		public int ComboBeginSymbolText(char* selected, int len, int symbol, Vec2 size)
		{
			Window win;
			Style style;
			Input _in_;
			Rect header = new Rect();
			int is_clicked = (int) (Nuklear.nk_false);
			int s;
			StyleItem background;
			Color symbol_color = new Color();
			Text text = new Text();
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			style = this.style;
			s = (int) (header.Widget(this));
			if (s == 0) return (int) (0);
			_in_ = (((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0) || ((s) == (Nuklear.NK_WIDGET_ROM))) ? null : this.input;
			if ((Nuklear.ButtonBehavior(ref this.last_widget_state, (Rect) (header), _in_, (int) (Nuklear.NK_BUTTON_DEFAULT))) !=
			    0) is_clicked = (int) (Nuklear.nk_true);
			if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.combo.active;
				symbol_color = (Color) (style.combo.symbol_active);
				text.text = (Color) (style.combo.label_active);
			}
			else if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.combo.hover;
				symbol_color = (Color) (style.combo.symbol_hover);
				text.text = (Color) (style.combo.label_hover);
			}
			else
			{
				background = style.combo.normal;
				symbol_color = (Color) (style.combo.symbol_normal);
				text.text = (Color) (style.combo.label_normal);
			}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
			{
				text.background = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
				win.buffer.DrawImage((Rect) (header), background.data.image, (Color) (Nuklear.nk_white));
			}
			else
			{
				text.background = (Color) (background.data.color);
				win.buffer.FillRect((Rect) (header), (float) (style.combo.rounding), (Color) (background.data.color));
				win.buffer.StrokeRect((Rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(Color) (style.combo.border_color));
			}

			{
				Rect content = new Rect();
				Rect button = new Rect();
				Rect label = new Rect();
				Rect image = new Rect();
				int sym;
				if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.x);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				win.buffer.DrawButtonSymbol(&button, &content, (uint) (this.last_widget_state), this.style.combo.button, (int) (sym),
					style.font);
				image.x = (float) (header.x + style.combo.content_padding.x);
				image.y = (float) (header.y + style.combo.content_padding.y);
				image.h = (float) (header.h - 2*style.combo.content_padding.y);
				image.w = (float) (image.h);
				win.buffer.DrawSymbol((int) (symbol), (Rect) (image), (Color) (text.background), (Color) (symbol_color),
					(float) (1.0f), style.font);
				text.padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
				label.x = (float) (image.x + image.w + style.combo.spacing.x + style.combo.content_padding.x);
				label.y = (float) (header.y + style.combo.content_padding.y);
				label.w = (float) ((button.x - style.combo.content_padding.x) - label.x);
				label.h = (float) (header.h - 2*style.combo.content_padding.y);
				win.buffer.WidgetText((Rect) (label), selected, (int) (len), &text, (uint) (Nuklear.NK_TEXT_LEFT), style.font);
			}

			return (int) (ComboBegin(win, (Vec2) (size), (int) (is_clicked), (Rect) (header)));
		}

		public int ComboBeginImage(Image img, Vec2 size)
		{
			Window win;
			Style style;
			Input _in_;
			Rect header = new Rect();
			int is_clicked = (int) (Nuklear.nk_false);
			int s;
			StyleItem background;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			style = this.style;
			s = (int) (header.Widget(this));
			if ((s) == (Nuklear.NK_WIDGET_INVALID)) return (int) (0);
			_in_ = (((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0) || ((s) == (Nuklear.NK_WIDGET_ROM))) ? null : this.input;
			if ((Nuklear.ButtonBehavior(ref this.last_widget_state, (Rect) (header), _in_, (int) (Nuklear.NK_BUTTON_DEFAULT))) !=
			    0) is_clicked = (int) (Nuklear.nk_true);
			if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0) background = style.combo.active;
			else if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) background = style.combo.hover;
			else background = style.combo.normal;
			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
			{
				win.buffer.DrawImage((Rect) (header), background.data.image, (Color) (Nuklear.nk_white));
			}
			else
			{
				win.buffer.FillRect((Rect) (header), (float) (style.combo.rounding), (Color) (background.data.color));
				win.buffer.StrokeRect((Rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(Color) (style.combo.border_color));
			}

			{
				Rect bounds = new Rect();
				Rect content = new Rect();
				Rect button = new Rect();
				int sym;
				if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.y);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				bounds.h = (float) (header.h - 2*style.combo.content_padding.y);
				bounds.y = (float) (header.y + style.combo.content_padding.y);
				bounds.x = (float) (header.x + style.combo.content_padding.x);
				bounds.w = (float) ((button.x - style.combo.content_padding.y) - bounds.x);
				win.buffer.DrawImage((Rect) (bounds), img, (Color) (Nuklear.nk_white));
				win.buffer.DrawButtonSymbol(&bounds, &content, (uint) (this.last_widget_state), this.style.combo.button, (int) (sym),
					style.font);
			}

			return (int) (ComboBegin(win, (Vec2) (size), (int) (is_clicked), (Rect) (header)));
		}

		public int ComboBeginImageText(char* selected, int len, Image img, Vec2 size)
		{
			Window win;
			Style style;
			Input _in_;
			Rect header = new Rect();
			int is_clicked = (int) (Nuklear.nk_false);
			int s;
			StyleItem background;
			Text text = new Text();
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			style = this.style;
			s = (int) (header.Widget(this));
			if (s == 0) return (int) (0);
			_in_ = (((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0) || ((s) == (Nuklear.NK_WIDGET_ROM))) ? null : this.input;
			if ((Nuklear.ButtonBehavior(ref this.last_widget_state, (Rect) (header), _in_, (int) (Nuklear.NK_BUTTON_DEFAULT))) !=
			    0) is_clicked = (int) (Nuklear.nk_true);
			if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.combo.active;
				text.text = (Color) (style.combo.label_active);
			}
			else if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.combo.hover;
				text.text = (Color) (style.combo.label_hover);
			}
			else
			{
				background = style.combo.normal;
				text.text = (Color) (style.combo.label_normal);
			}

			if ((background.type) == (Nuklear.NK_STYLE_ITEM_IMAGE))
			{
				text.background = (Color) (Nuklear.Rgba((int) (0), (int) (0), (int) (0), (int) (0)));
				win.buffer.DrawImage((Rect) (header), background.data.image, (Color) (Nuklear.nk_white));
			}
			else
			{
				text.background = (Color) (background.data.color);
				win.buffer.FillRect((Rect) (header), (float) (style.combo.rounding), (Color) (background.data.color));
				win.buffer.StrokeRect((Rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(Color) (style.combo.border_color));
			}

			{
				Rect content = new Rect();
				Rect button = new Rect();
				Rect label = new Rect();
				Rect image = new Rect();
				int sym;
				if ((this.last_widget_state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.x);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				win.buffer.DrawButtonSymbol(&button, &content, (uint) (this.last_widget_state), this.style.combo.button, (int) (sym),
					style.font);
				image.x = (float) (header.x + style.combo.content_padding.x);
				image.y = (float) (header.y + style.combo.content_padding.y);
				image.h = (float) (header.h - 2*style.combo.content_padding.y);
				image.w = (float) (image.h);
				win.buffer.DrawImage((Rect) (image), img, (Color) (Nuklear.nk_white));
				text.padding = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
				label.x = (float) (image.x + image.w + style.combo.spacing.x + style.combo.content_padding.x);
				label.y = (float) (header.y + style.combo.content_padding.y);
				label.w = (float) ((button.x - style.combo.content_padding.x) - label.x);
				label.h = (float) (header.h - 2*style.combo.content_padding.y);
				win.buffer.WidgetText((Rect) (label), selected, (int) (len), &text, (uint) (Nuklear.NK_TEXT_LEFT), style.font);
			}

			return (int) (ComboBegin(win, (Vec2) (size), (int) (is_clicked), (Rect) (header)));
		}

		public int ComboBeginSymbolLabel(char* selected, int type, Vec2 size)
		{
			return (int) (ComboBeginSymbolText(selected, (int) (Nuklear.Strlen(selected)), (int) (type), (Vec2) (size)));
		}

		public int ComboBeginImageLabel(char* selected, Image img, Vec2 size)
		{
			return (int) (ComboBeginImageText(selected, (int) (Nuklear.Strlen(selected)), (Image) (img), (Vec2) (size)));
		}

		public int ComboItemText(char* text, int len, uint align)
		{
			return (int) (ContextualItemText(text, (int) (len), (uint) (align)));
		}

		public int ComboItemLabel(char* label, uint align)
		{
			return (int) (ContextualItemLabel(label, (uint) (align)));
		}

		public int ComboItemImageText(Image img, char* text, int len, uint alignment)
		{
			return (int) (ContextualItemImageText((Image) (img), text, (int) (len), (uint) (alignment)));
		}

		public int ComboItemImageLabel(Image img, char* text, uint alignment)
		{
			return (int) (ContextualItemImageLabel((Image) (img), text, (uint) (alignment)));
		}

		public int ComboItemSymbolText(int sym, char* text, int len, uint alignment)
		{
			return (int) (ContextualItemSymbolText((int) (sym), text, (int) (len), (uint) (alignment)));
		}

		public int ComboItemSymbolLabel(int sym, char* label, uint alignment)
		{
			return (int) (ContextualItemSymbolLabel((int) (sym), label, (uint) (alignment)));
		}

		public void ComboEnd()
		{
			ContextualEnd();
		}

		public void ComboClose()
		{
			ContextualClose();
		}

		public int Combo(char** items, int count, int selected, int item_height, Vec2 size)
		{
			int i = (int) (0);
			int max_height;
			Vec2 item_spacing = new Vec2();
			Vec2 window_padding = new Vec2();
			if (((items == null)) || (count == 0)) return (int) (selected);
			item_spacing = (Vec2) (this.style.window.spacing);
			window_padding = (Vec2) (this.style.PanelGetPadding((int) (this.current.layout.type)));
			max_height = (int) (count*item_height + count*(int) (item_spacing.y));
			max_height += (int) ((int) (item_spacing.y)*2 + (int) (window_padding.y)*2);
			size.y = (float) ((size.y) < ((float) (max_height)) ? (size.y) : ((float) (max_height)));
			if ((ComboBeginLabel(items[selected], (Vec2) (size))) != 0)
			{
				LayoutRowDynamic((float) (item_height), (int) (1));
				for (i = (int) (0); (i) < (count); ++i)
				{
					if ((ComboItemLabel(items[i], (uint) (Nuklear.NK_TEXT_LEFT))) != 0) selected = (int) (i);
				}
				ComboEnd();
			}

			return (int) (selected);
		}

		public int ComboSeparator(char* items_separated_by_separator, int separator, int selected, int count, int item_height,
			Vec2 size)
		{
			int i;
			int max_height;
			Vec2 item_spacing = new Vec2();
			Vec2 window_padding = new Vec2();
			char* current_item;
			char* iter;
			;
			int length = (int) (0);
			if ((items_separated_by_separator == null)) return (int) (selected);
			item_spacing = (Vec2) (this.style.window.spacing);
			window_padding = (Vec2) (this.style.PanelGetPadding((int) (this.current.layout.type)));
			max_height = (int) (count*item_height + count*(int) (item_spacing.y));
			max_height += (int) ((int) (item_spacing.y)*2 + (int) (window_padding.y)*2);
			size.y = (float) ((size.y) < ((float) (max_height)) ? (size.y) : ((float) (max_height)));
			current_item = items_separated_by_separator;
			for (i = (int) (0); (i) < (count); ++i)
			{
				iter = current_item;
				while (((*iter) != 0) && (*iter != separator))
				{
					iter++;
				}
				length = ((int) (iter - current_item));
				if ((i) == (selected)) break;
				current_item = iter + 1;
			}
			if ((ComboBeginText(current_item, (int) (length), (Vec2) (size))) != 0)
			{
				current_item = items_separated_by_separator;
				LayoutRowDynamic((float) (item_height), (int) (1));
				for (i = (int) (0); (i) < (count); ++i)
				{
					iter = current_item;
					while (((*iter) != 0) && (*iter != separator))
					{
						iter++;
					}
					length = ((int) (iter - current_item));
					if ((ComboItemText(current_item, (int) (length), (uint) (Nuklear.NK_TEXT_LEFT))) != 0) selected = (int) (i);
					current_item = current_item + length + 1;
				}
				ComboEnd();
			}

			return (int) (selected);
		}

		public int ComboString(char* items_separated_by_zeros, int selected, int count, int item_height, Vec2 size)
		{
			return
				(int)
					(ComboSeparator(items_separated_by_zeros, (int) ('\0'), (int) (selected), (int) (count), (int) (item_height),
						(Vec2) (size)));
		}

		public int ComboCallback(NkComboCallback item_getter, void* userdata, int selected, int count, int item_height,
			Vec2 size)
		{
			int i;
			int max_height;
			Vec2 item_spacing = new Vec2();
			Vec2 window_padding = new Vec2();
			char* item;
			if ((item_getter == null)) return (int) (selected);
			item_spacing = (Vec2) (this.style.window.spacing);
			window_padding = (Vec2) (this.style.PanelGetPadding((int) (this.current.layout.type)));
			max_height = (int) (count*item_height + count*(int) (item_spacing.y));
			max_height += (int) ((int) (item_spacing.y)*2 + (int) (window_padding.y)*2);
			size.y = (float) ((size.y) < ((float) (max_height)) ? (size.y) : ((float) (max_height)));
			item_getter(userdata, (int) (selected), &item);
			if ((ComboBeginLabel(item, (Vec2) (size))) != 0)
			{
				LayoutRowDynamic((float) (item_height), (int) (1));
				for (i = (int) (0); (i) < (count); ++i)
				{
					item_getter(userdata, (int) (i), &item);
					if ((ComboItemLabel(item, (uint) (Nuklear.NK_TEXT_LEFT))) != 0) selected = (int) (i);
				}
				ComboEnd();
			}

			return (int) (selected);
		}

		public void Combobox(char** items, int count, int* selected, int item_height, Vec2 size)
		{
			*selected = (int) (Combo(items, (int) (count), (int) (*selected), (int) (item_height), (Vec2) (size)));
		}

		public void ComboboxString(char* items_separated_by_zeros, int* selected, int count, int item_height, Vec2 size)
		{
			*selected =
				(int) (ComboString(items_separated_by_zeros, (int) (*selected), (int) (count), (int) (item_height), (Vec2) (size)));
		}

		public void ComboboxSeparator(char* items_separated_by_separator, int separator, int* selected, int count,
			int item_height, Vec2 size)
		{
			*selected =
				(int)
					(ComboSeparator(items_separated_by_separator, (int) (separator), (int) (*selected), (int) (count),
						(int) (item_height), (Vec2) (size)));
		}

		public void ComboboxCallback(NkComboCallback item_getter, void* userdata, int* selected, int count, int item_height,
			Vec2 size)
		{
			*selected =
				(int) (ComboCallback(item_getter, userdata, (int) (*selected), (int) (count), (int) (item_height), (Vec2) (size)));
		}

		public int MenuBegin(Window win, char* id, int is_clicked, Rect header, Vec2 size)
		{
			int is_open = (int) (0);
			int is_active = (int) (0);
			Rect body = new Rect();
			Window popup;
			uint hash = (uint) (Nuklear.MurmurHash(id, (int) (Nuklear.Strlen(id)), (uint) (Nuklear.NK_PANEL_MENU)));
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			body.x = (float) (header.x);
			body.w = (float) (size.x);
			body.y = (float) (header.y + header.h);
			body.h = (float) (size.y);
			popup = win.popup.win;
			is_open = (int) (popup != null ? Nuklear.nk_true : Nuklear.nk_false);
			is_active =
				(int) ((((popup) != null) && ((win.popup.name) == (hash))) && ((win.popup.type) == (Nuklear.NK_PANEL_MENU)) ? 1 : 0);
			if ((((((is_clicked) != 0) && ((is_open) != 0)) && (is_active == 0)) || (((is_open) != 0) && (is_active == 0))) ||
			    (((is_open == 0) && (is_active == 0)) && (is_clicked == 0))) return (int) (0);
			if (
				NonblockBegin((uint) (Nuklear.NK_WINDOW_NO_SCROLLBAR), (Rect) (body), (Rect) (header), (int) (Nuklear.NK_PANEL_MENU)) ==
				0) return (int) (0);
			win.popup.type = (int) (Nuklear.NK_PANEL_MENU);
			win.popup.name = (uint) (hash);
			return (int) (1);
		}

		public int MenuBeginText(char* title, int len, uint align, Vec2 size)
		{
			Window win;
			Input _in_;
			Rect header = new Rect();
			int is_clicked = (int) (Nuklear.nk_false);
			uint state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			state = (uint) (header.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((win.flags & Nuklear.NK_WINDOW_ROM) != 0)) ? null : this.input;
			if (
				(Nuklear.DoButtonText(ref this.last_widget_state, win.buffer, (Rect) (header), title, (int) (len), (uint) (align),
					(int) (Nuklear.NK_BUTTON_DEFAULT), this.style.menu_button, _in_, this.style.font)) != 0)
				is_clicked = (int) (Nuklear.nk_true);
			return (int) (MenuBegin(win, title, (int) (is_clicked), (Rect) (header), (Vec2) (size)));
		}

		public int MenuBeginLabel(char* text, uint align, Vec2 size)
		{
			return (int) (MenuBeginText(text, (int) (Nuklear.Strlen(text)), (uint) (align), (Vec2) (size)));
		}

		public int MenuBeginImage(char* id, Image img, Vec2 size)
		{
			Window win;
			Rect header = new Rect();
			Input _in_;
			int is_clicked = (int) (Nuklear.nk_false);
			uint state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			state = (uint) (header.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0))
				? null
				: this.input;
			if (
				(Nuklear.DoButtonImage(ref this.last_widget_state, win.buffer, (Rect) (header), (Image) (img),
					(int) (Nuklear.NK_BUTTON_DEFAULT), this.style.menu_button, _in_)) != 0) is_clicked = (int) (Nuklear.nk_true);
			return (int) (MenuBegin(win, id, (int) (is_clicked), (Rect) (header), (Vec2) (size)));
		}

		public int MenuBeginSymbol(char* id, int sym, Vec2 size)
		{
			Window win;
			Input _in_;
			Rect header = new Rect();
			int is_clicked = (int) (Nuklear.nk_false);
			uint state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			state = (uint) (header.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0))
				? null
				: this.input;
			if (
				(Nuklear.DoButtonSymbol(ref this.last_widget_state, win.buffer, (Rect) (header), (int) (sym),
					(int) (Nuklear.NK_BUTTON_DEFAULT), this.style.menu_button, _in_, this.style.font)) != 0)
				is_clicked = (int) (Nuklear.nk_true);
			return (int) (MenuBegin(win, id, (int) (is_clicked), (Rect) (header), (Vec2) (size)));
		}

		public int MenuBeginImageText(char* title, int len, uint align, Image img, Vec2 size)
		{
			Window win;
			Rect header = new Rect();
			Input _in_;
			int is_clicked = (int) (Nuklear.nk_false);
			uint state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			state = (uint) (header.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0))
				? null
				: this.input;
			if (
				(Nuklear.DoButtonTextImage(ref this.last_widget_state, win.buffer, (Rect) (header), (Image) (img), title,
					(int) (len), (uint) (align), (int) (Nuklear.NK_BUTTON_DEFAULT), this.style.menu_button, this.style.font, _in_)) !=
				0) is_clicked = (int) (Nuklear.nk_true);
			return (int) (MenuBegin(win, title, (int) (is_clicked), (Rect) (header), (Vec2) (size)));
		}

		public int MenuBeginImageLabel(char* title, uint align, Image img, Vec2 size)
		{
			return (int) (MenuBeginImageText(title, (int) (Nuklear.Strlen(title)), (uint) (align), (Image) (img), (Vec2) (size)));
		}

		public int MenuBeginSymbolText(char* title, int len, uint align, int sym, Vec2 size)
		{
			Window win;
			Rect header = new Rect();
			Input _in_;
			int is_clicked = (int) (Nuklear.nk_false);
			uint state;
			if (((this.current == null)) || (this.current.layout == null)) return (int) (0);
			win = this.current;
			state = (uint) (header.Widget(this));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (Nuklear.NK_WIDGET_ROM)) || ((win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0))
				? null
				: this.input;
			if (
				(Nuklear.DoButtonTextSymbol(ref this.last_widget_state, win.buffer, (Rect) (header), (int) (sym), title, (int) (len),
					(uint) (align), (int) (Nuklear.NK_BUTTON_DEFAULT), this.style.menu_button, this.style.font, _in_)) != 0)
				is_clicked = (int) (Nuklear.nk_true);
			return (int) (MenuBegin(win, title, (int) (is_clicked), (Rect) (header), (Vec2) (size)));
		}

		public int MenuBeginSymbolLabel(char* title, uint align, int sym, Vec2 size)
		{
			return (int) (MenuBeginSymbolText(title, (int) (Nuklear.Strlen(title)), (uint) (align), (int) (sym), (Vec2) (size)));
		}

		public int MenuItemText(char* title, int len, uint align)
		{
			return (int) (ContextualItemText(title, (int) (len), (uint) (align)));
		}

		public int MenuItemLabel(char* label, uint align)
		{
			return (int) (ContextualItemLabel(label, (uint) (align)));
		}

		public int MenuItemImageLabel(Image img, char* label, uint align)
		{
			return (int) (ContextualItemImageLabel((Image) (img), label, (uint) (align)));
		}

		public int MenuItemImageText(Image img, char* text, int len, uint align)
		{
			return (int) (ContextualItemImageText((Image) (img), text, (int) (len), (uint) (align)));
		}

		public int MenuItemSymbolText(int sym, char* text, int len, uint align)
		{
			return (int) (ContextualItemSymbolText((int) (sym), text, (int) (len), (uint) (align)));
		}

		public int MenuItemSymbolLabel(int sym, char* label, uint align)
		{
			return (int) (ContextualItemSymbolLabel((int) (sym), label, (uint) (align)));
		}

		public void MenuClose()
		{
			ContextualClose();
		}

		public void MenuEnd()
		{
			ContextualEnd();
		}
	}
}