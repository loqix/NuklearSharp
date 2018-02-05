using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtPoint
	{
		public float x;
		public float y;

		public void TtAddPoint(int n, float x, float y)
		{
			if (this== null) return;
			this[n].x = (float)(x);
			this[n].y = (float)(y);
		}

		public int TtTesselateCurve(int* num_points, float x0, float y0, float x1, float y1, float x2, float y2, float objspace_flatness_squared, int n)
		{
			float mx = (float)((x0 + 2 * x1 + x2) / 4);
			float my = (float)((y0 + 2 * y1 + y2) / 4);
			float dx = (float)((x0 + x2) / 2 - mx);
			float dy = (float)((y0 + y2) / 2 - my);
			if ((n) > (16)) return (int)(1);
			if ((dx * dx + dy * dy) > (objspace_flatness_squared)) {
TtTesselateCurve(num_points, (float)(x0), (float)(y0), (float)((x0 + x1) / 2.0f), (float)((y0 + y1) / 2.0f), (float)(mx), (float)(my), (float)(objspace_flatness_squared), (int)(n + 1));TtTesselateCurve(num_points, (float)(mx), (float)(my), (float)((x1 + x2) / 2.0f), (float)((y1 + y2) / 2.0f), (float)(x2), (float)(y2), (float)(objspace_flatness_squared), (int)(n + 1));}
 else {
TtAddPoint((int)(*num_points), (float)(x2), (float)(y2));*num_points = (int)(*num_points + 1);}

			return (int)(1);
		}

	}
}
