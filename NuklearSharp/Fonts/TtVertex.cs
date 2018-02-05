using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtVertex
	{
		public short x;
		public short y;
		public short cx;
		public short cy;
		public byte type;
		public byte padding;

		public void TtSetvertex(byte type, int x, int y, int cx, int cy)
		{
			this.type = (byte)(type);
			this.x = ((short)(x));
			this.y = ((short)(y));
			this.cx = ((short)(cx));
			this.cy = ((short)(cy));
		}

		public int StbttCloseShape(int num_vertices, int was_off, int start_off, int sx, int sy, int scx, int scy, int cx, int cy)
		{
			if ((start_off) != 0) {
if ((was_off) != 0) this[num_vertices++].TtSetvertex((byte)(Nuklear.NK_TT_vcurve), (int)((cx + scx) >> 1), (int)((cy + scy) >> 1), (int)(cx), (int)(cy));this[num_vertices++].TtSetvertex((byte)(Nuklear.NK_TT_vcurve), (int)(sx), (int)(sy), (int)(scx), (int)(scy));}
 else {
if ((was_off) != 0) this[num_vertices++].TtSetvertex((byte)(Nuklear.NK_TT_vcurve), (int)(sx), (int)(sy), (int)(cx), (int)(cy)); else this[num_vertices++].TtSetvertex((byte)(Nuklear.NK_TT_vline), (int)(sx), (int)(sy), (int)(0), (int)(0));}

			return (int)(num_vertices);
		}

		public TtPoint* TtFlattenCurves(int num_verts, float objspace_flatness, int** contour_lengths, int* num_contours)
		{
			TtPoint* points = null;
			int num_points = (int)(0);
			float objspace_flatness_squared = (float)(objspace_flatness * objspace_flatness);
			int i;
			int n = (int)(0);
			int start = (int)(0);
			int pass;
			for (i = (int)(0); (i) < (num_verts); ++i) {if ((this[i].type) == (Nuklear.NK_TT_vmove)) ++n;}
			*num_contours = (int)(n);
			if ((n) == (0)) return null;
			*contour_lengths = (int*)(CRuntime.malloc((ulong)((ulong)sizeof(int) * (ulong)(n))));
			if ((*contour_lengths) == (null)) {
*num_contours = (int)(0);return null;}

			for (pass = (int)(0); (pass) < (2); ++pass) {
float x = (float)(0);float y = (float)(0);if ((pass) == (1)) {
points = (TtPoint*)(CRuntime.malloc((ulong)((ulong)(num_points) * (ulong)sizeof(Tt__point))));if ((points) == (null)) goto error;}
num_points = (int)(0);n = (int)(-1);for (i = (int)(0); (i) < (num_verts); ++i) {
switch (this[i].type){
case Nuklear.NK_TT_vmove:if ((n) >= (0)) (*contour_lengths)[n] = (int)(num_points - start);++n;start = (int)(num_points);x = (float)(this[i].x);y = (float)(this[i].y);points->TtAddPoint((int)(num_points++), (float)(x), (float)(y));break;case Nuklear.NK_TT_vline:x = (float)(this[i].x);y = (float)(this[i].y);points->TtAddPoint((int)(num_points++), (float)(x), (float)(y));break;case Nuklear.NK_TT_vcurve:points->TtTesselateCurve(&num_points, (float)(x), (float)(y), (float)(this[i].cx), (float)(this[i].cy), (float)(this[i].x), (float)(this[i].y), (float)(objspace_flatness_squared), (int)(0));x = (float)(this[i].x);y = (float)(this[i].y);break;default: break;}
}(*contour_lengths)[n] = (int)(num_points - start);}
			return points;
			error:;
CRuntime.free(points);
			CRuntime.free(*contour_lengths);
			*contour_lengths = null;
			*num_contours = (int)(0);
			return null;
		}

	}
}
