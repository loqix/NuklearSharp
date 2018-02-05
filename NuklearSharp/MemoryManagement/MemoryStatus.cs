using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class MemoryStatus
	{
		public void * memory;
		public uint type;
		public ulong size;
		public ulong allocated;
		public ulong needed;
		public ulong calls;

		public void BufferInfo(Buffer b)
		{
			if ((b== null)) return;
			this.allocated = (ulong)(b.allocated);
			this.size = (ulong)(b.memory.size);
			this.needed = (ulong)(b.needed);
			this.memory = b.memory.ptr;
			this.calls = (ulong)(b.calls);
		}

	}
}
