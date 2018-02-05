using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Color
	{
		public byte r;
		public byte g;
		public byte b;
		public byte a;

		public uint U32()
		{
			uint _out_ = (uint)(this.r);
			_out_ |= (uint)((uint)(this.g) << 8);
			_out_ |= (uint)((uint)(this.b) << 16);
			_out_ |= (uint)((uint)(this.a) << 24);
			return (uint)(_out_);
		}

		public Colorf Cf()
		{
			Colorf o =  new Colorf();
			Nuklear.ColorF(&o.r, &o.g, &o.b, &o.a, (Color)(this));
			return (Colorf)(o);
		}

		public StyleItem StyleItemColor()
		{
			StyleItem i =  new StyleItem();
			i.type = (int)(Nuklear.NK_STYLE_ITEM_COLOR);
			i.data.color = (Color)(this);
			return (StyleItem)(i);
		}

	}
}