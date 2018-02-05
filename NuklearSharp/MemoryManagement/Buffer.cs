using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class Buffer
	{
		public void Init(ulong initial_size)
		{
			if (((this== null) ) || (initial_size== 0)) return;
			
			this.type = (int)(Nuklear.NK_BUFFER_DYNAMIC);
			this.memory.ptr = CRuntime.malloc((ulong)(initial_size));
			this.memory.size = (ulong)(initial_size);
			this.size = (ulong)(initial_size);
			this.grow_factor = (float)(2.0f);
			this.pool = (Allocator)(*a);
		}

		public void InitFixed(void * m, ulong size)
		{
			if (((m== null)) || (size== 0)) return;
			
			this.type = (int)(Nuklear.NK_BUFFER_FIXED);
			this.memory.ptr = m;
			this.memory.size = (ulong)(size);
			this.size = (ulong)(size);
		}

		public void * Realloc(ulong capacity, ref ulong size)
		{
			void * temp;
			ulong buffer_size;
			if ((((size== null)) || (this.pool.alloc== null)) || (this.pool.free== null)) return null;
			buffer_size = (ulong)(this.memory.size);
			temp = this.pool.alloc((Handle)(this.pool.userdata), this.memory.ptr, (ulong)(capacity));
			if (temp== null) return null;
			size = (ulong)(capacity);
			if (temp != this.memory.ptr) {
Nuklear.Memcopy(temp, this.memory.ptr, (ulong)(buffer_size));this.pool.free((Handle)(this.pool.userdata), this.memory.ptr);}

			if ((this.size) == (buffer_size)) {
this.size = (ulong)(capacity);return temp;}
 else {
void * dst;void * src;ulong back_size;back_size = (ulong)(buffer_size - this.size);dst = ((void *)((byte*)(temp) + (capacity - back_size)));src = ((void *)((byte*)(temp) + (this.size)));Nuklear.Memcopy(dst, src, (ulong)(back_size));this.size = (ulong)(capacity - back_size);}

			return temp;
		}

		public void * Alloc(int type, ulong size, ulong align)
		{
			int full;
			ulong alignment;
			void * unaligned;
			void * memory;
			if ((size== 0)) return null;
			this.needed += (ulong)(size);
			if ((type) == (Nuklear.NK_BUFFER_FRONT)) unaligned = ((void *)((byte*)(this.memory.ptr) + (this.allocated))); else unaligned = ((void *)((byte*)(this.memory.ptr) + (this.size - size)));
			memory = Nuklear.BufferAlign(unaligned, (ulong)(align), &alignment, (int)(type));
			if ((type) == (Nuklear.NK_BUFFER_FRONT)) full = (int)((this.allocated + size + alignment) > (this.size)?1:0); else full = (int)((this.size - ((this.size) < (size + alignment)?(this.size):(size + alignment))) <= this.allocated);
			if ((full) != 0) {
ulong capacity;if (this.type != Nuklear.NK_BUFFER_DYNAMIC) return null;if (((this.type != Nuklear.NK_BUFFER_DYNAMIC) || (this.pool.alloc== null)) || (this.pool.free== null)) return null;capacity = ((ulong)((float)(this.memory.size) * this.grow_factor));capacity = (ulong)((capacity) < (Nuklear.RoundUpPow2((uint)(this.allocated + size)))?(Nuklear.RoundUpPow2((uint)(this.allocated + size))):(capacity));this.memory.ptr = Realloc((ulong)(capacity), ref this.memory.size);if (this.memory.ptr== null) return null;if ((type) == (Nuklear.NK_BUFFER_FRONT)) unaligned = ((void *)((byte*)(this.memory.ptr) + (this.allocated))); else unaligned = ((void *)((byte*)(this.memory.ptr) + (this.size - size)));memory = Nuklear.BufferAlign(unaligned, (ulong)(align), &alignment, (int)(type));}

			if ((type) == (Nuklear.NK_BUFFER_FRONT)) this.allocated += (ulong)(size + alignment); else this.size -= (ulong)(size + alignment);
			this.needed += (ulong)(alignment);
			this.calls++;
			return memory;
		}

		public void Push(int type, void * memory, ulong size, ulong align)
		{
			void * mem = Alloc((int)(type), (ulong)(size), (ulong)(align));
			if (mem== null) return;
			Nuklear.Memcopy(mem, (ulong)(size));
		}

		public void Mark(int type)
		{
			if (this== null) return;
			this.marker[type].active = (int)(Nuklear.nk_true);
			if ((type) == (Nuklear.NK_BUFFER_BACK)) this.marker[type].offset = (ulong)(this.size); else this.marker[type].offset = (ulong)(this.allocated);
		}

		public void Reset(int type)
		{
			if (this== null) return;
			if ((type) == (Nuklear.NK_BUFFER_BACK)) {
this.needed -= (ulong)(this.memory.size - this.marker[type].offset);if ((this.marker[type].active) != 0) this.size = (ulong)(this.marker[type].offset); else this.size = (ulong)(this.memory.size);this.marker[type].active = (int)(Nuklear.nk_false);}
 else {
this.needed -= (ulong)(this.allocated - this.marker[type].offset);if ((this.marker[type].active) != 0) this.allocated = (ulong)(this.marker[type].offset); else this.allocated = (ulong)(0);this.marker[type].active = (int)(Nuklear.nk_false);}

		}

		public void Clear()
		{
			if (this== null) return;
			this.allocated = (ulong)(0);
			this.size = (ulong)(this.memory.size);
			this.calls = (ulong)(0);
			this.needed = (ulong)(0);
		}

		public void Free()
		{
			if ((this.memory.ptr== null)) return;
			if ((this.type) == (Nuklear.NK_BUFFER_FIXED)) return;
			if (this.pool.free== null) return;
			this.pool.free((Handle)(this.pool.userdata), this.memory.ptr);
		}

		public void * Memory()
		{
			if (this== null) return null;
			return this.memory.ptr;
		}

		public void * MemoryConst()
		{
			if (this== null) return null;
			return this.memory.ptr;
		}

		public ulong Total()
		{
			if (this== null) return (ulong)(0);
			return (ulong)(this.memory.size);
		}

	}
}