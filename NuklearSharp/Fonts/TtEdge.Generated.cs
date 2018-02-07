// Generated by Sichem at 2/7/2018 5:58:59 PM

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

		public static void TtSortEdgesInsSort(TtEdge* p, int n)
		{
			int i;
			int j;
			for (i = (int) (1); (i) < (n); ++i)
			{
				TtEdge t = (TtEdge) (p[i]);
				TtEdge* a = &t;
				j = (int) (i);
				while ((j) > (0))
				{
					TtEdge* b = &p[j - 1];
					int c = (int) (((a)->y0) < ((b)->y0) ? 1 : 0);
					if (c == 0) break;
					p[j] = (TtEdge) (p[j - 1]);
					--j;
				}
				if (i != j) p[j] = (TtEdge) (t);
			}
		}

		public static void TtSortEdgesQuicksort(TtEdge* p, int n)
		{
			while ((n) > (12))
			{
				TtEdge t = new TtEdge();
				int c01;
				int c12;
				int c;
				int m;
				int i;
				int j;
				m = (int) (n >> 1);
				c01 = (int) (((&p[0])->y0) < ((&p[m])->y0) ? 1 : 0);
				c12 = (int) (((&p[m])->y0) < ((&p[n - 1])->y0) ? 1 : 0);
				if (c01 != c12)
				{
					int z;
					c = (int) (((&p[0])->y0) < ((&p[n - 1])->y0) ? 1 : 0);
					z = (int) (((c) == (c12)) ? 0 : n - 1);
					t = (TtEdge) (p[z]);
					p[z] = (TtEdge) (p[m]);
					p[m] = (TtEdge) (t);
				}
				t = (TtEdge) (p[0]);
				p[0] = (TtEdge) (p[m]);
				p[m] = (TtEdge) (t);
				i = (int) (1);
				j = (int) (n - 1);
				for (;;)
				{
					for (;; ++i)
					{
						if (!(((&p[i])->y0) < ((&p[0])->y0))) break;
					}
					for (;; --j)
					{
						if (!(((&p[0])->y0) < ((&p[j])->y0))) break;
					}
					if ((i) >= (j)) break;
					t = (TtEdge) (p[i]);
					p[i] = (TtEdge) (p[j]);
					p[j] = (TtEdge) (t);
					++i;
					--j;
				}
				if ((j) < (n - i))
				{
					TtSortEdgesQuicksort(p, (int) (j));
					p = p + i;
					n = (int) (n - i);
				}
				else
				{
					TtSortEdgesQuicksort(p + i, (int) (n - i));
					n = (int) (j);
				}
			}
		}

		public static void TtSortEdges(TtEdge* p, int n)
		{
			TtSortEdgesQuicksort(p, (int) (n));
			TtSortEdgesInsSort(p, (int) (n));
		}
	}
}