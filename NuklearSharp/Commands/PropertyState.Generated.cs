// Generated by Sichem at 2/7/2018 4:58:56 PM

using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class PropertyState
	{
		public int active;
		public int prev;
		public PinnedArray<sbyte> buffer = new PinnedArray<sbyte>(64);
		public int length;
		public int cursor;
		public int select_start;
		public int select_end;
		public uint name;
		public uint seq;
		public uint old;
		public int state;
	}
}