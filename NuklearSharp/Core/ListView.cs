using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class ListView
	{
		public int begin;
		public int end;
		public int count;
		public int total_height;
		public Context ctx;
		public uint* scroll_pointer;
		public uint scroll_value;

		public void End()
		{
			Context ctx;
			Window win;
			Panel layout;
			if ((this.ctx== null)) return;
			ctx = this.ctx;
			win = ctx.current;
			layout = win.layout;
			layout.at_y = (float)(layout.bounds.y + (float)(this.total_height));
			*this.scroll_pointer = (uint)(*this.scroll_pointer + this.scroll_value);
			this.ctx.GroupEnd();
		}

	}
}
