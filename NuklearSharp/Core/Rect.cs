using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Rect
	{
		public float x;
		public float y;
		public float w;
		public float h;

		public Vec2 Pos()
		{
			Vec2 ret =  new Vec2();
			ret.x = (float)(this.x);
			ret.y = (float)(this.y);
			return (Vec2)(ret);
		}

		public Vec2 Size()
		{
			Vec2 ret =  new Vec2();
			ret.x = (float)(this.w);
			ret.y = (float)(this.h);
			return (Vec2)(ret);
		}

		public Rect ShrinkRectz(float amount)
		{
			Rect res =  new Rect();
			this.w = (float)((this.w) < (2 * amount)?(2 * amount):(this.w));
			this.h = (float)((this.h) < (2 * amount)?(2 * amount):(this.h));
			res.x = (float)(this.x + amount);
			res.y = (float)(this.y + amount);
			res.w = (float)(this.w - 2 * amount);
			res.h = (float)(this.h - 2 * amount);
			return (Rect)(res);
		}

		public Rect PadRect(Vec2 pad)
		{
			this.w = (float)((this.w) < (2 * pad.x)?(2 * pad.x):(this.w));
			this.h = (float)((this.h) < (2 * pad.y)?(2 * pad.y):(this.h));
			this.x += (float)(pad.x);
			this.y += (float)(pad.y);
			this.w -= (float)(2 * pad.x);
			this.h -= (float)(2 * pad.y);
			return (Rect)(this);
		}

		public void Unify(ref Rect a, float x0, float y0, float x1, float y1)
		{
			this.x = (float)((a.x) < (x0)?(x0):(a.x));
			this.y = (float)((a.y) < (y0)?(y0):(a.y));
			this.w = (float)(((a.x + a.w) < (x1)?(a.x + a.w):(x1)) - this.x);
			this.h = (float)(((a.y + a.h) < (y1)?(a.y + a.h):(y1)) - this.y);
			this.w = (float)((0) < (this.w)?(this.w):(0));
			this.h = (float)((0) < (this.h)?(this.h):(0));
		}

		public void LayoutWidgetSpace(Context ctx, Window win, int modify)
		{
			Panel layout;
			Style style;
			Vec2 spacing =  new Vec2();
			Vec2 padding =  new Vec2();
			float item_offset = (float)(0);
			float item_width = (float)(0);
			float item_spacing = (float)(0);
			float panel_space = (float)(0);
			if (((ctx== null) || (ctx.current== null)) || (ctx.current.layout== null)) return;
			win = ctx.current;
			layout = win.layout;
			style = ctx.style;
			spacing = (Vec2)(style.window.spacing);
			padding = (Vec2)(style.PanelGetPadding((int)(layout.type)));
			panel_space = (float)(ctx.style.LayoutRowCalculateUsableSpace((int)(layout.type), (float)(layout.bounds.w), (int)(layout.row.columns)));
			switch (layout.row.type){
case Nuklear.NK_LAYOUT_DYNAMIC_FIXED:{
item_width = (float)(((1.0f) < (panel_space - 1.0f)?(panel_space - 1.0f):(1.0f)) / (float)(layout.row.columns));item_offset = (float)((float)(layout.row.index) * item_width);item_spacing = (float)((float)(layout.row.index) * spacing.x);}
break;case Nuklear.NK_LAYOUT_DYNAMIC_ROW:{
item_width = (float)(layout.row.item_width * panel_space);item_offset = (float)(layout.row.item_offset);item_spacing = (float)(0);if ((modify) != 0) {
layout.row.item_offset += (float)(item_width + spacing.x);layout.row.filled += (float)(layout.row.item_width);layout.row.index = (int)(0);}
}
break;case Nuklear.NK_LAYOUT_DYNAMIC_FREE:{
this.x = (float)(layout.at_x + (layout.bounds.w * layout.row.item.x));this.x -= ((float)(layout.offset.x));this.y = (float)(layout.at_y + (layout.row.height * layout.row.item.y));this.y -= ((float)(layout.offset.y));this.w = (float)(layout.bounds.w * layout.row.item.w);this.h = (float)(layout.row.height * layout.row.item.h);return;}
break;case Nuklear.NK_LAYOUT_DYNAMIC:{
float ratio;ratio = (float)(((layout.row.ratio[layout.row.index]) < (0))?layout.row.item_width:layout.row.ratio[layout.row.index]);item_spacing = (float)((float)(layout.row.index) * spacing.x);item_width = (float)(ratio * panel_space);item_offset = (float)(layout.row.item_offset);if ((modify) != 0) {
layout.row.item_offset += (float)(item_width);layout.row.filled += (float)(ratio);}
}
break;case Nuklear.NK_LAYOUT_STATIC_FIXED:{
item_width = (float)(layout.row.item_width);item_offset = (float)((float)(layout.row.index) * item_width);item_spacing = (float)((float)(layout.row.index) * spacing.x);}
break;case Nuklear.NK_LAYOUT_STATIC_ROW:{
item_width = (float)(layout.row.item_width);item_offset = (float)(layout.row.item_offset);item_spacing = (float)((float)(layout.row.index) * spacing.x);if ((modify) != 0) layout.row.item_offset += (float)(item_width);}
break;case Nuklear.NK_LAYOUT_STATIC_FREE:{
this.x = (float)(layout.at_x + layout.row.item.x);this.w = (float)(layout.row.item.w);if (((this.x + this.w) > (layout.max_x)) && ((modify) != 0)) layout.max_x = (float)(this.x + this.w);this.x -= ((float)(layout.offset.x));this.y = (float)(layout.at_y + layout.row.item.y);this.y -= ((float)(layout.offset.y));this.h = (float)(layout.row.item.h);return;}
break;case Nuklear.NK_LAYOUT_STATIC:{
item_spacing = (float)((float)(layout.row.index) * spacing.x);item_width = (float)(layout.row.ratio[layout.row.index]);item_offset = (float)(layout.row.item_offset);if ((modify) != 0) layout.row.item_offset += (float)(item_width);}
break;case Nuklear.NK_LAYOUT_TEMPLATE:{
item_width = (float)(layout.row.templates[layout.row.index]);item_offset = (float)(layout.row.item_offset);item_spacing = (float)((float)(layout.row.index) * spacing.x);if ((modify) != 0) layout.row.item_offset += (float)(item_width);}
break;default: ;break;}

			this.w = (float)(item_width);
			this.h = (float)(layout.row.height - spacing.y);
			this.y = (float)(layout.at_y - (float)(layout.offset.y));
			this.x = (float)(layout.at_x + item_offset + item_spacing + padding.x);
			if (((this.x + this.w) > (layout.max_x)) && ((modify) != 0)) layout.max_x = (float)(this.x + this.w);
			this.x -= ((float)(layout.offset.x));
		}

		public void PanelAllocSpace(Context ctx)
		{
			Window win;
			Panel layout;
			if (((ctx== null) || (ctx.current== null)) || (ctx.current.layout== null)) return;
			win = ctx.current;
			layout = win.layout;
			if ((layout.row.index) >= (layout.row.columns)) ctx.PanelAllocRow(win);
			LayoutWidgetSpace(ctx, win, (int)(Nuklear.nk_true));
			layout.row.index++;
		}

		public void LayoutPeek(Context ctx)
		{
			float y;
			int index;
			Window win;
			Panel layout;
			if (((ctx== null) || (ctx.current== null)) || (ctx.current.layout== null)) return;
			win = ctx.current;
			layout = win.layout;
			y = (float)(layout.at_y);
			index = (int)(layout.row.index);
			if ((layout.row.index) >= (layout.row.columns)) {
layout.at_y += (float)(layout.row.height);layout.row.index = (int)(0);}

			LayoutWidgetSpace(ctx, win, (int)(Nuklear.nk_false));
			if (layout.row.index== 0) {
this.x -= (float)(layout.row.item_offset);}

			layout.at_y = (float)(y);
			layout.row.index = (int)(index);
		}

		public int Widget(Context ctx)
		{
			Rect c =  new Rect();Rect v =  new Rect();
			Window win;
			Panel layout;
			Input _in_;
			if (((ctx== null) || (ctx.current== null)) || (ctx.current.layout== null)) return (int)(Nuklear.NK_WIDGET_INVALID);
			PanelAllocSpace(ctx);
			win = ctx.current;
			layout = win.layout;
			_in_ = ctx.input;
			c = (Rect)(layout.clip);
			this.x = ((float)((int)(this.x)));
			this.y = ((float)((int)(this.y)));
			this.w = ((float)((int)(this.w)));
			this.h = ((float)((int)(this.h)));
			c.x = ((float)((int)(c.x)));
			c.y = ((float)((int)(c.y)));
			c.w = ((float)((int)(c.w)));
			c.h = ((float)((int)(c.h)));
			v.Unify(ref c, (float)(this.x), (float)(this.y), (float)(this.x + this.w), (float)(this.y + this.h));
			if (!(!(((((this.x) > (c.x + c.w)) || ((this.x + this.w) < (c.x))) || ((this.y) > (c.y + c.h))) || ((this.y + this.h) < (c.y))))) return (int)(Nuklear.NK_WIDGET_INVALID);
			if (!((((v.x) <= (_in_.mouse.pos.x)) && ((_in_.mouse.pos.x) < (v.x + v.w))) && (((v.y) <= (_in_.mouse.pos.y)) && ((_in_.mouse.pos.y) < (v.y + v.h))))) return (int)(Nuklear.NK_WIDGET_ROM);
			return (int)(Nuklear.NK_WIDGET_VALID);
		}

		public int WidgetFitting(Context ctx, Vec2 item_padding)
		{
			Window win;
			Style style;
			Panel layout;
			int state;
			Vec2 panel_padding =  new Vec2();
			if (((ctx== null) || (ctx.current== null)) || (ctx.current.layout== null)) return (int)(Nuklear.NK_WIDGET_INVALID);
			win = ctx.current;
			style = ctx.style;
			layout = win.layout;
			state = (int)(Widget(ctx));
			panel_padding = (Vec2)(style.PanelGetPadding((int)(layout.type)));
			if ((layout.row.index) == (1)) {
this.w += (float)(panel_padding.x);this.x -= (float)(panel_padding.x);}
 else this.x -= (float)(item_padding.x);
			if ((layout.row.index) == (layout.row.columns)) this.w += (float)(panel_padding.x); else this.w += (float)(item_padding.x);
			return (int)(state);
		}

	}
}