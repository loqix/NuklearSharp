using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtBitmap
	{
		public int w;
		public int h;
		public int stride;
		public byte* pixels;

		public void TtRasterizeSortedEdges(TtEdge* e, int n, int vsubsample, int off_x, int off_y)
		{
			TtHheap hh =  new TtHheap();
			TtActiveEdge* active = null;
			int y;int j = (int)(0);int i;
			float* scanline_data = stackalloc float[129];float* scanline;float* scanline2;
			Nuklear.Zero(&hh, (ulong)(sizeof(TtHheap)));
			
			if ((this.w) > (64)) scanline = (float*)(CRuntime.malloc((ulong)((ulong)(this.w * 2 + 1) * sizeof(float))))); else scanline = scanline_data;
			scanline2 = scanline + this.w;
			y = (int)(off_y);
			e[n].y0 = (float)((float)(off_y + this.h) + 1);
			while ((j) < (this.h)) {
float scan_y_top = (float)((float)(y) + 0.0f);float scan_y_bottom = (float)((float)(y) + 1.0f);TtActiveEdge** step = &active;Nuklear.Memset(scanline, (int)(0), (ulong)((ulong)(this.w) * sizeof(float)));Nuklear.Memset(scanline2, (int)(0), (ulong)((ulong)(this.w + 1) * sizeof(float)));while ((*step) != null) {
TtActiveEdge* z = *step;if (z->ey <= scan_y_top) {
*step = z->next;z->direction = (float)(0);hh.Free(z);}
 else {
step = &((*step)->next);}
}while (e->y0 <= scan_y_bottom) {
if (e->y0 != e->y1) {
TtActiveEdge* z = hh.TtNewActive(e, (int)(off_x), (float)(scan_y_top));if (z != null) {
z->next = active;active = z;}
}
++e;}if ((active) != null) Nuklear.TtFillActiveEdgesNew(scanline, scanline2 + 1, (int)(this.w), active, (float)(scan_y_top));{
float sum = (float)(0);for (i = (int)(0); (i) < (this.w); ++i) {
float k;int m;sum += (float)(scanline2[i]);k = (float)(scanline[i] + sum);k = (float)((((k) < (0))?-(k):(k)) * 255.0f + 0.5f);m = ((int)(k));if ((m) > (255)) m = (int)(255);this.pixels[j * this.stride + i] = ((byte)(m));}}
step = &active;while ((*step) != null) {
TtActiveEdge* z = *step;z->fx += (float)(z->fdx);step = &((*step)->next);}++y;++j;}
			hh.Cleanup();
			if (scanline != scanline_data) CRuntime.free(scanline);
		}

		public void TtRasterize(TtPoint* pts, int* wcount, int windings, float scale_x, float scale_y, float shift_x, float shift_y, int off_x, int off_y, int invert)
		{
			float y_scale_inv = (float)((invert) != 0?-scale_y:scale_y);
			TtEdge* e;
			int n;int i;int j;int k;int m;
			int vsubsample = (int)(1);
			n = (int)(0);
			for (i = (int)(0); (i) < (windings); ++i) {n += (int)(wcount[i]);}
			e = (TtEdge*)(CRuntime.malloc((ulong)((ulong)sizeof(Tt_Edge) * (ulong)(n + 1))));
			if ((e) == (null)) return;
			n = (int)(0);
			m = (int)(0);
			for (i = (int)(0); (i) < (windings); ++i) {
TtPoint* p = pts + m;m += (int)(wcount[i]);j = (int)(wcount[i] - 1);for (k = (int)(0); (k) < (wcount[i]); j = (int)(k++)) {
int a = (int)(k);int b = (int)(j);if ((p[j].y) == (p[k].y)) continue;e[n].invert = (int)(0);if (invert != 0?(p[j].y > p[k].y):(p[j].y < p[k].y)) {
e[n].invert = (int)(1);a = (int)(j);b = (int)(k);}
e[n].x0 = (float)(p[a].x * scale_x + shift_x);e[n].y0 = (float)((p[a].y * y_scale_inv + shift_y) * (float)(vsubsample));e[n].x1 = (float)(p[b].x * scale_x + shift_x);e[n].y1 = (float)((p[b].y * y_scale_inv + shift_y) * (float)(vsubsample));++n;}}
			e->TtSortEdges((int)(n));
			TtRasterizeSortedEdges(e, (int)(n), (int)(vsubsample), (int)(off_x), (int)(off_y));
			CRuntime.free(e);
		}

		public void TtRasterize(float flatness_in_pixels, TtVertex* vertices, int num_verts, float scale_x, float scale_y, float shift_x, float shift_y, int x_off, int y_off, int invert)
		{
			float scale = (float)((scale_x) > (scale_y)?scale_y:scale_x);
			int winding_count;int* winding_lengths;
			TtPoint* windings = vertices->TtFlattenCurves((int)(num_verts), (float)(flatness_in_pixels / scale), &winding_lengths, &winding_count);
			if ((windings) != null) {
TtRasterize(windings, winding_lengths, (int)(winding_count), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), (int)(x_off), (int)(y_off), (int)(invert));CRuntime.free(winding_lengths);CRuntime.free(windings);}

		}

	}
}
