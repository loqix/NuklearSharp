using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtEdge
	{
		public float x0;
		public float y0;
		public float x1;
		public float y1;
		public int invert;

		public void TtSortEdgesInsSort(int n)
		{
			int i;int j;
			for (i = (int)(1); (i) < (n); ++i) {
TtEdge t = (TtEdge)(this[i]);TtEdge* a = &t;j = (int)(i);while ((j) > (0)) {
TtEdge* b = &this[j - 1];int c = (int)(((a)->y0) < ((b)->y0)?1:0);if (c== 0) break;this[j] = (TtEdge)(this[j - 1]);--j;}if (i != j) this[j] = (TtEdge)(t);}
		}

		public void TtSortEdgesQuicksort(int n)
		{
			while ((n) > (12)) {
TtEdge t =  new TtEdge();int c01;int c12;int c;int m;int i;int j;m = (int)(n >> 1);c01 = (int)(((&this[0])->y0) < ((&this[m])->y0)?1:0);c12 = (int)(((&this[m])->y0) < ((&this[n - 1])->y0)?1:0);if (c01 != c12) {
int z;c = (int)(((&this[0])->y0) < ((&this[n - 1])->y0)?1:0);z = (int)(((c) == (c12))?0:n - 1);t = (TtEdge)(this[z]);this[z] = (TtEdge)(this[m]);this[m] = (TtEdge)(t);}
t = (TtEdge)(this[0]);this[0] = (TtEdge)(this[m]);this[m] = (TtEdge)(t);i = (int)(1);j = (int)(n - 1);for (; ; ) {
for (;;++i) {
if (!(((&this[i])->y0) < ((&this[0])->y0))) break;}
for (;;--j) {
if (!(((&this[0])->y0) < ((&this[j])->y0))) break;}
if ((i) >= (j)) break;t = (TtEdge)(this[i]);this[i] = (TtEdge)(this[j]);this[j] = (TtEdge)(t);++i;--j;}if ((j) < (n - i)) {
TtSortEdgesQuicksort((int)(j));this = this + i;n = (int)(n - i);}
 else {
this + i->TtSortEdgesQuicksort((int)(n - i));n = (int)(j);}
}
		}

		public void TtSortEdges(int n)
		{
			TtSortEdgesQuicksort((int)(n));
			TtSortEdgesInsSort((int)(n));
		}

	}
}
