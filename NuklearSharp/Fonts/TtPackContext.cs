using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtPackContext
	{
		public void * pack_info;
		public int width;
		public int height;
		public int stride_in_bytes;
		public int padding;
		public uint h_oversample;
		public uint v_oversample;
		public byte* pixels;
		public void * nodes;

		public int TtPackBegin(byte* pixels, int pw, int ph, int stride_in_bytes, int padding)
		{
			int num_nodes = (int)(pw - padding);
			RpContext* context = (RpContext*)(CRuntime.malloc((ulong)(sizeof(Rp_context))));
			RpNode* nodes = (RpNode*)(CRuntime.malloc((ulong)((ulong)sizeof(Rp_node) * (ulong)(num_nodes))));
			if (((context) == (null)) || ((nodes) == (null))) {
if (context != null) CRuntime.free(context);if (nodes != null) CRuntime.free(nodes);return (int)(0);}

			this.width = (int)(pw);
			this.height = (int)(ph);
			this.pixels = pixels;
			this.pack_info = context;
			this.nodes = nodes;
			this.padding = (int)(padding);
			this.stride_in_bytes = (int)((stride_in_bytes != 0)?stride_in_bytes:pw);
			this.h_oversample = (uint)(1);
			this.v_oversample = (uint)(1);
			context->RpInitTarget((int)(pw - padding), (int)(ph - padding), nodes, (int)(num_nodes));
			if ((pixels) != null) Nuklear.Memset(pixels, (int)(0), (ulong)(pw * ph));
			return (int)(1);
		}

		public void TtPackEnd(Allocator* alloc)
		{
			CRuntime.free(this.nodes);
			CRuntime.free(this.pack_info);
		}

		public void TtPackSetOversampling(uint h_oversample, uint v_oversample)
		{
			if (h_oversample <= 8) this.h_oversample = (uint)(h_oversample);
			if (v_oversample <= 8) this.v_oversample = (uint)(v_oversample);
		}

		public int TtPackFontRangesGatherRects(TtFontinfo* info, TtPackRange* ranges, int num_ranges, RpRect* rects)
		{
			int i;int j;int k;
			k = (int)(0);
			for (i = (int)(0); (i) < (num_ranges); ++i) {
float fh = (float)(ranges[i].font_size);float scale = (float)(((fh) > (0))?info->TtScaleForPixelHeight((float)(fh)):info->TtScaleForMappingEmToPixels((float)(-fh)));ranges[i].h_oversample = ((byte)(this.h_oversample));ranges[i].v_oversample = ((byte)(this.v_oversample));for (j = (int)(0); (j) < (ranges[i].num_chars); ++j) {
int x0;int y0;int x1;int y1;int codepoint = (int)((ranges[i].first_unicode_codepoint_in_range) != 0?ranges[i].first_unicode_codepoint_in_range + j:ranges[i].array_of_unicode_codepoints[j]);int glyph = (int)(info->TtFindGlyphIndex((int)(codepoint)));info->TtGetGlyphBitmapBoxSubpixel((int)(glyph), (float)(scale * (float)(this.h_oversample)), (float)(scale * (float)(this.v_oversample)), (float)(0), (float)(0), &x0, &y0, &x1, &y1);rects[k].w = ((ushort)(x1 - x0 + this.padding + (int)(this.h_oversample) - 1));rects[k].h = ((ushort)(y1 - y0 + this.padding + (int)(this.v_oversample) - 1));++k;}}
			return (int)(k);
		}

		public int TtPackFontRangesRenderIntoRects(TtFontinfo* info, TtPackRange* ranges, int num_ranges, RpRect* rects)
		{
			int i;int j;int k;int return_value = (int)(1);
			int old_h_over = (int)(this.h_oversample);
			int old_v_over = (int)(this.v_oversample);
			k = (int)(0);
			for (i = (int)(0); (i) < (num_ranges); ++i) {
float fh = (float)(ranges[i].font_size);float recip_h;float recip_v;float sub_x;float sub_y;float scale = (float)((fh) > (0)?info->TtScaleForPixelHeight((float)(fh)):info->TtScaleForMappingEmToPixels((float)(-fh)));this.h_oversample = (uint)(ranges[i].h_oversample);this.v_oversample = (uint)(ranges[i].v_oversample);recip_h = (float)(1.0f / (float)(this.h_oversample));recip_v = (float)(1.0f / (float)(this.v_oversample));sub_x = (float)(Nuklear.TtOversampleShift((int)(this.h_oversample)));sub_y = (float)(Nuklear.TtOversampleShift((int)(this.v_oversample)));for (j = (int)(0); (j) < (ranges[i].num_chars); ++j) {
RpRect* r = &rects[k];if ((r->was_packed) != 0) {
TtPackedchar* bc = &ranges[i].chardata_for_range[j];int advance;int lsb;int x0;int y0;int x1;int y1;int codepoint = (int)((ranges[i].first_unicode_codepoint_in_range) != 0?ranges[i].first_unicode_codepoint_in_range + j:ranges[i].array_of_unicode_codepoints[j]);int glyph = (int)(info->TtFindGlyphIndex((int)(codepoint)));ushort pad = (ushort)(this.padding);r->x = ((ushort)((int)(r->x) + (int)(pad)));r->y = ((ushort)((int)(r->y) + (int)(pad)));r->w = ((ushort)((int)(r->w) - (int)(pad)));r->h = ((ushort)((int)(r->h) - (int)(pad)));info->TtGetGlyphHMetrics((int)(glyph), &advance, &lsb);info->TtGetGlyphBitmapBox((int)(glyph), (float)(scale * (float)(this.h_oversample)), (float)(scale * (float)(this.v_oversample)), &x0, &y0, &x1, &y1);info->TtMakeGlyphBitmapSubpixel(this.pixels + r->x + r->y * this.stride_in_bytes, (int)(r->w - this.h_oversample + 1), (int)(r->h - this.v_oversample + 1), (int)(this.stride_in_bytes), (float)(scale * (float)(this.h_oversample)), (float)(scale * (float)(this.v_oversample)), (float)(0), (float)(0), (int)(glyph));if ((this.h_oversample) > (1)) Nuklear.TtHPrefilter(this.pixels + r->x + r->y * this.stride_in_bytes, (int)(r->w), (int)(r->h), (int)(this.stride_in_bytes), (int)(this.h_oversample));if ((this.v_oversample) > (1)) Nuklear.TtVPrefilter(this.pixels + r->x + r->y * this.stride_in_bytes, (int)(r->w), (int)(r->h), (int)(this.stride_in_bytes), (int)(this.v_oversample));bc->x0 = (ushort)(r->x);bc->y0 = (ushort)(r->y);bc->x1 = ((ushort)(r->x + r->w));bc->y1 = ((ushort)(r->y + r->h));bc->xadvance = (float)(scale * (float)(advance));bc->xoff = (float)((float)(x0) * recip_h + sub_x);bc->yoff = (float)((float)(y0) * recip_v + sub_y);bc->xoff2 = (float)(((float)(x0) + r->w) * recip_h + sub_x);bc->yoff2 = (float)(((float)(y0) + r->h) * recip_v + sub_y);}
 else {
return_value = (int)(0);}
++k;}}
			this.h_oversample = ((uint)(old_h_over));
			this.v_oversample = ((uint)(old_v_over));
			return (int)(return_value);
		}

	}
}
