using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TextUndoRecord
	{
		public int where;
		public short insert_length;
		public short delete_length;
		public short char_storage;
	}
}