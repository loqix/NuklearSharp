using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtHheap
	{
		
		public TtHheapChunk* head;
		public void * first_free;
		public int num_remaining_in_head_chunk;

		public void * Alloc(ulong size)
		{
			if ((this.first_free) != null) {
void * p = this.first_free;this.first_free = *(void **)(p);return p;}
 else {
if ((this.num_remaining_in_head_chunk) == (0)) {
int count = (int)((size) < (32)?2000:(size) < (128)?800:100);TtHheapChunk* c = (TtHheapChunk*)(this.alloc.alloc((Handle)(this.alloc.userdata), null, (ulong)(sizeof(TtHheapChunk) + size * (ulong)(count))));if ((c) == (null)) return null;c->next = this.head;this.head = c;this.num_remaining_in_head_chunk = (int)(count);}
--this.num_remaining_in_head_chunk;return (sbyte*)(this.head) + size * (ulong)(this.num_remaining_in_head_chunk);}

		}

		public void Free(void * p)
		{
			*(void **)(p) = this.first_free;
			this.first_free = p;
		}

		public void Cleanup()
		{
			TtHheapChunk* c = this.head;
			while ((c) != null) {
TtHheapChunk* n = c->next;this.alloc.free((Handle)(this.alloc.userdata), c);c = n;}
		}

		public TtActiveEdge* TtNewActive(TtEdge* e, int off_x, float start_point)
		{
			TtActiveEdge* z = (TtActiveEdge*)(Alloc((ulong)(sizeof(TtActiveEdge))));
			float dxdy = (float)((e->x1 - e->x0) / (e->y1 - e->y0));
			if (z== null) return z;
			z->fdx = (float)(dxdy);
			z->fdy = (float)((dxdy != 0)?(1 / dxdy):0);
			z->fx = (float)(e->x0 + dxdy * (start_point - e->y0));
			z->fx -= ((float)(off_x));
			z->direction = (float)((e->invert) != 0?1.0f:-1.0f);
			z->sy = (float)(e->y0);
			z->ey = (float)(e->y1);
			z->next = null;
			return z;
		}

	}
}
