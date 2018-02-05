using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtPackedchar
	{
		public ushort x0;
		public ushort y0;
		public ushort x1;
		public ushort y1;
		public float xoff;
		public float yoff;
		public float xadvance;
		public float xoff2;
		public float yoff2;

		public void TtGetPackedQuad(int pw, int ph, int char_index, float* xpos, float* ypos, TtAlignedQuad* q, int align_to_integer)
		{
			float ipw = (float)(1.0f / (float)(pw));float iph = (float)(1.0f / (float)(ph));
			TtPackedchar* b = (this + char_index);
			if ((align_to_integer) != 0) {
int tx = (int)(Nuklear.Ifloorf((float)((*xpos + b->xoff) + 0.5f)));int ty = (int)(Nuklear.Ifloorf((float)((*ypos + b->yoff) + 0.5f)));float x = (float)(tx);float y = (float)(ty);q->x0 = (float)(x);q->y0 = (float)(y);q->x1 = (float)(x + b->xoff2 - b->xoff);q->y1 = (float)(y + b->yoff2 - b->yoff);}
 else {
q->x0 = (float)(*xpos + b->xoff);q->y0 = (float)(*ypos + b->yoff);q->x1 = (float)(*xpos + b->xoff2);q->y1 = (float)(*ypos + b->yoff2);}

			q->s0 = (float)(b->x0 * ipw);
			q->t0 = (float)(b->y0 * iph);
			q->s1 = (float)(b->x1 * ipw);
			q->t1 = (float)(b->y1 * iph);
			*xpos += (float)(b->xadvance);
		}

	}
}
