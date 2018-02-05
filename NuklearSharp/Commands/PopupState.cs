using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class PopupState
	{
		public Window win;
		public int type;
		public PopupBuffer buf = new PopupBuffer();
		public uint name;
		public int active;
		public uint combo_count;
		public uint con_count;
		public uint con_old;
		public uint active_con;
		public Rect header = new Rect();

	}
}
