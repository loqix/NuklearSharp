using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtFontinfo
	{
		public byte* data;
		public int fontstart;
		public int numGlyphs;
		public int loca;
		public int head;
		public int glyf;
		public int hhea;
		public int hmtx;
		public int kern;
		public int index_map;
		public int indexToLocFormat;

		public int TtInitFont(byte* data2, int fontstart)
		{
			uint cmap;uint t;
			int i;int numTables;
			byte* data = data2;
			this.data = data;
			this.fontstart = (int)(fontstart);
			cmap = (uint)(Nuklear.TtFindTable(data, (uint)(fontstart), "cmap"));
			this.loca = ((int)(Nuklear.TtFindTable(data, (uint)(fontstart), "loca")));
			this.head = ((int)(Nuklear.TtFindTable(data, (uint)(fontstart), "head")));
			this.glyf = ((int)(Nuklear.TtFindTable(data, (uint)(fontstart), "glyf")));
			this.hhea = ((int)(Nuklear.TtFindTable(data, (uint)(fontstart), "hhea")));
			this.hmtx = ((int)(Nuklear.TtFindTable(data, (uint)(fontstart), "hmtx")));
			this.kern = ((int)(Nuklear.TtFindTable(data, (uint)(fontstart), "kern")));
			if ((((((cmap== 0) || (this.loca== 0)) || (this.head== 0)) || (this.glyf== 0)) || (this.hhea== 0)) || (this.hmtx== 0)) return (int)(0);
			t = (uint)(Nuklear.TtFindTable(data, (uint)(fontstart), "maxp"));
			if ((t) != 0) this.numGlyphs = (int)(Nuklear.TtUSHORT(data + t + 4)); else this.numGlyphs = (int)(0xffff);
			numTables = (int)(Nuklear.TtUSHORT(data + cmap + 2));
			this.index_map = (int)(0);
			for (i = (int)(0); (i) < (numTables); ++i) {
uint encoding_record = (uint)(cmap + 4 + 8 * (uint)(i));switch (Nuklear.TtUSHORT(data + encoding_record)){
case Nuklear.NK_TT_PLATFORM_ID_MICROSOFT:switch (Nuklear.TtUSHORT(data + encoding_record + 2)){
case Nuklear.NK_TT_MS_EID_UNICODE_BMP:case Nuklear.NK_TT_MS_EID_UNICODE_FULL:this.index_map = ((int)(cmap + Nuklear.TtULONG(data + encoding_record + 4)));break;default: break;}
break;case Nuklear.NK_TT_PLATFORM_ID_UNICODE:this.index_map = ((int)(cmap + Nuklear.TtULONG(data + encoding_record + 4)));break;default: break;}
}
			if ((this.index_map) == (0)) return (int)(0);
			this.indexToLocFormat = (int)(Nuklear.TtUSHORT(data + this.head + 50));
			return (int)(1);
		}

		public int TtFindGlyphIndex(int unicode_codepoint)
		{
			byte* data = this.data;
			uint index_map = (uint)(this.index_map);
			ushort format = (ushort)(Nuklear.TtUSHORT(data + index_map + 0));
			if ((format) == (0)) {
int bytes = (int)(Nuklear.TtUSHORT(data + index_map + 2));if ((unicode_codepoint) < (bytes - 6)) return (int)(*(data + index_map + 6 + unicode_codepoint));return (int)(0);}
 else if ((format) == (6)) {
uint first = (uint)(Nuklear.TtUSHORT(data + index_map + 6));uint count = (uint)(Nuklear.TtUSHORT(data + index_map + 8));if ((((uint)(unicode_codepoint)) >= (first)) && (((uint)(unicode_codepoint)) < (first + count))) return (int)(Nuklear.TtUSHORT(data + index_map + 10 + (unicode_codepoint - (int)(first)) * 2));return (int)(0);}
 else if ((format) == (2)) {
return (int)(0);}
 else if ((format) == (4)) {
ushort segcount = (ushort)(Nuklear.TtUSHORT(data + index_map + 6) >> 1);ushort searchRange = (ushort)(Nuklear.TtUSHORT(data + index_map + 8) >> 1);ushort entrySelector = (ushort)(Nuklear.TtUSHORT(data + index_map + 10));ushort rangeShift = (ushort)(Nuklear.TtUSHORT(data + index_map + 12) >> 1);uint endCount = (uint)(index_map + 14);uint search = (uint)(endCount);if ((unicode_codepoint) > (0xffff)) return (int)(0);if ((unicode_codepoint) >= (Nuklear.TtUSHORT(data + search + rangeShift * 2))) search += ((uint)(rangeShift * 2));search -= (uint)(2);while ((entrySelector) != 0) {
ushort end;searchRange >>= 1;end = (ushort)(Nuklear.TtUSHORT(data + search + searchRange * 2));if ((unicode_codepoint) > (end)) search += ((uint)(searchRange * 2));--entrySelector;}search += (uint)(2);{
ushort offset;ushort start;ushort item = (ushort)((search - endCount) >> 1);start = (ushort)(Nuklear.TtUSHORT(data + index_map + 14 + segcount * 2 + 2 + 2 * item));if ((unicode_codepoint) < (start)) return (int)(0);offset = (ushort)(Nuklear.TtUSHORT(data + index_map + 14 + segcount * 6 + 2 + 2 * item));if ((offset) == (0)) return (int)((ushort)(unicode_codepoint + Nuklear.TtSHORT(data + index_map + 14 + segcount * 4 + 2 + 2 * item)));return (int)(Nuklear.TtUSHORT(data + offset + (unicode_codepoint - start) * 2 + index_map + 14 + segcount * 6 + 2 + 2 * item));}
}
 else if (((format) == (12)) || ((format) == (13))) {
uint ngroups = (uint)(Nuklear.TtULONG(data + index_map + 12));int low;int high;low = (int)(0);high = ((int)(ngroups));while ((low) < (high)) {
int mid = (int)(low + ((high - low) >> 1));uint start_char = (uint)(Nuklear.TtULONG(data + index_map + 16 + mid * 12));uint end_char = (uint)(Nuklear.TtULONG(data + index_map + 16 + mid * 12 + 4));if (((uint)(unicode_codepoint)) < (start_char)) high = (int)(mid); else if (((uint)(unicode_codepoint)) > (end_char)) low = (int)(mid + 1); else {
uint start_glyph = (uint)(Nuklear.TtULONG(data + index_map + 16 + mid * 12 + 8));if ((format) == (12)) return (int)((int)(start_glyph) + unicode_codepoint - (int)(start_char)); else return (int)(start_glyph);}
}return (int)(0);}

			return (int)(0);
		}

		public int TtGetGlyfOffset(int glyph_index)
		{
			int g1;int g2;
			if ((glyph_index) >= (this.numGlyphs)) return (int)(-1);
			if ((this.indexToLocFormat) >= (2)) return (int)(-1);
			if ((this.indexToLocFormat) == (0)) {
g1 = (int)(this.glyf + Nuklear.TtUSHORT(this.data + this.loca + glyph_index * 2) * 2);g2 = (int)(this.glyf + Nuklear.TtUSHORT(this.data + this.loca + glyph_index * 2 + 2) * 2);}
 else {
g1 = (int)(this.glyf + (int)(Nuklear.TtULONG(this.data + this.loca + glyph_index * 4)));g2 = (int)(this.glyf + (int)(Nuklear.TtULONG(this.data + this.loca + glyph_index * 4 + 4)));}

			return (int)((g1) == (g2)?-1:g1);
		}

		public int TtGetGlyphBox(int glyph_index, int* x0, int* y0, int* x1, int* y1)
		{
			int g = (int)(TtGetGlyfOffset((int)(glyph_index)));
			if ((g) < (0)) return (int)(0);
			if ((x0) != null) *x0 = (int)(Nuklear.TtSHORT(this.data + g + 2));
			if ((y0) != null) *y0 = (int)(Nuklear.TtSHORT(this.data + g + 4));
			if ((x1) != null) *x1 = (int)(Nuklear.TtSHORT(this.data + g + 6));
			if ((y1) != null) *y1 = (int)(Nuklear.TtSHORT(this.data + g + 8));
			return (int)(1);
		}

		public int TtGetGlyphShape( int glyph_index, TtVertex** pvertices)
		{
			short numberOfContours;
			byte* endPtsOfContours;
			byte* data = this.data;
			TtVertex* vertices = null;
			int num_vertices = (int)(0);
			int g = (int)(TtGetGlyfOffset((int)(glyph_index)));
			*pvertices = null;
			if ((g) < (0)) return (int)(0);
			numberOfContours = (short)(Nuklear.TtSHORT(data + g));
			if ((numberOfContours) > (0)) {
byte flags = (byte)(0);byte flagcount;int ins;int i;int j = (int)(0);int m;int n;int next_move;int was_off = (int)(0);int off;int start_off = (int)(0);int x;int y;int cx;int cy;int sx;int sy;int scx;int scy;byte* points;endPtsOfContours = (data + g + 10);ins = (int)(Nuklear.TtUSHORT(data + g + 10 + numberOfContours * 2));points = data + g + 10 + numberOfContours * 2 + 2 + ins;n = (int)(1 + Nuklear.TtUSHORT(endPtsOfContours + numberOfContours * 2 - 2));m = (int)(n + 2 * numberOfContours);vertices = (TtVertex*)(CRuntime.malloc((ulong)((ulong)(m) * (ulong)sizeof(Tt_vertex))));if ((vertices) == (null)) return (int)(0);next_move = (int)(0);flagcount = (byte)(0);off = (int)(m - n);for (i = (int)(0); (i) < (n); ++i) {
if ((flagcount) == (0)) {
flags = (byte)(*points++);if ((flags & 8) != 0) flagcount = (byte)(*points++);}
 else --flagcount;vertices[off + i].type = (byte)(flags);}x = (int)(0);for (i = (int)(0); (i) < (n); ++i) {
flags = (byte)(vertices[off + i].type);if ((flags & 2) != 0) {
short dx = (short)(*points++);x += (int)((flags & 16) != 0?dx:-dx);}
 else {
if ((flags & 16)== 0) {
x = (int)(x + (short)(points[0] * 256 + points[1]));points += 2;}
}
vertices[off + i].x = ((short)(x));}y = (int)(0);for (i = (int)(0); (i) < (n); ++i) {
flags = (byte)(vertices[off + i].type);if ((flags & 4) != 0) {
short dy = (short)(*points++);y += (int)((flags & 32) != 0?dy:-dy);}
 else {
if ((flags & 32)== 0) {
y = (int)(y + (short)(points[0] * 256 + points[1]));points += 2;}
}
vertices[off + i].y = ((short)(y));}num_vertices = (int)(0);sx = (int)(sy = (int)(cx = (int)(cy = (int)(scx = (int)(scy = (int)(0))))));for (i = (int)(0); (i) < (n); ++i) {
flags = (byte)(vertices[off + i].type);x = (int)(vertices[off + i].x);y = (int)(vertices[off + i].y);if ((next_move) == (i)) {
if (i != 0) num_vertices = (int)(vertices->StbttCloseShape((int)(num_vertices), (int)(was_off), (int)(start_off), (int)(sx), (int)(sy), (int)(scx), (int)(scy), (int)(cx), (int)(cy)));start_off = (int)((flags & 1)==0?1:0);if ((start_off) != 0) {
scx = (int)(x);scy = (int)(y);if ((vertices[off + i + 1].type & 1)== 0) {
sx = (int)((x + (int)(vertices[off + i + 1].x)) >> 1);sy = (int)((y + (int)(vertices[off + i + 1].y)) >> 1);}
 else {
sx = ((int)(vertices[off + i + 1].x));sy = ((int)(vertices[off + i + 1].y));++i;}
}
 else {
sx = (int)(x);sy = (int)(y);}
vertices[num_vertices++].TtSetvertex((byte)(Nuklear.NK_TT_vmove), (int)(sx), (int)(sy), (int)(0), (int)(0));was_off = (int)(0);next_move = (int)(1 + Nuklear.TtUSHORT(endPtsOfContours + j * 2));++j;}
 else {
if ((flags & 1)== 0) {
if ((was_off) != 0) vertices[num_vertices++].TtSetvertex((byte)(Nuklear.NK_TT_vcurve), (int)((cx + x) >> 1), (int)((cy + y) >> 1), (int)(cx), (int)(cy));cx = (int)(x);cy = (int)(y);was_off = (int)(1);}
 else {
if ((was_off) != 0) vertices[num_vertices++].TtSetvertex((byte)(Nuklear.NK_TT_vcurve), (int)(x), (int)(y), (int)(cx), (int)(cy)); else vertices[num_vertices++].TtSetvertex((byte)(Nuklear.NK_TT_vline), (int)(x), (int)(y), (int)(0), (int)(0));was_off = (int)(0);}
}
}num_vertices = (int)(vertices->StbttCloseShape((int)(num_vertices), (int)(was_off), (int)(start_off), (int)(sx), (int)(sy), (int)(scx), (int)(scy), (int)(cx), (int)(cy)));}
 else if ((numberOfContours) == (-1)) {
int more = (int)(1);byte* comp = data + g + 10;num_vertices = (int)(0);vertices = null;while ((more) != 0) {
ushort flags;ushort gidx;int comp_num_verts = (int)(0);int i;TtVertex* comp_verts = null;TtVertex* tmp = null;float* mtx = stackalloc float[6];
mtx[0] = (float)(1);
mtx[1] = (float)(0);
mtx[2] = (float)(0);
mtx[3] = (float)(1);
mtx[4] = (float)(0);
mtx[5] = (float)(0);
float m;float n;flags = ((ushort)(Nuklear.TtSHORT(comp)));comp += 2;gidx = ((ushort)(Nuklear.TtSHORT(comp)));comp += 2;if ((flags & 2) != 0) {
if ((flags & 1) != 0) {
mtx[4] = (float)(Nuklear.TtSHORT(comp));comp += 2;mtx[5] = (float)(Nuklear.TtSHORT(comp));comp += 2;}
 else {
mtx[4] = (float)(*(sbyte*)(comp));comp += 1;mtx[5] = (float)(*(sbyte*)(comp));comp += 1;}
}
 else {
}
if ((flags & (1 << 3)) != 0) {
mtx[0] = (float)(mtx[3] = (float)(Nuklear.TtSHORT(comp) / 16384.0f));comp += 2;mtx[1] = (float)(mtx[2] = (float)(0));}
 else if ((flags & (1 << 6)) != 0) {
mtx[0] = (float)(Nuklear.TtSHORT(comp) / 16384.0f);comp += 2;mtx[1] = (float)(mtx[2] = (float)(0));mtx[3] = (float)(Nuklear.TtSHORT(comp) / 16384.0f);comp += 2;}
 else if ((flags & (1 << 7)) != 0) {
mtx[0] = (float)(Nuklear.TtSHORT(comp) / 16384.0f);comp += 2;mtx[1] = (float)(Nuklear.TtSHORT(comp) / 16384.0f);comp += 2;mtx[2] = (float)(Nuklear.TtSHORT(comp) / 16384.0f);comp += 2;mtx[3] = (float)(Nuklear.TtSHORT(comp) / 16384.0f);comp += 2;}
m = (float)(Nuklear.Sqrt((float)(mtx[0] * mtx[0] + mtx[1] * mtx[1])));n = (float)(Nuklear.Sqrt((float)(mtx[2] * mtx[2] + mtx[3] * mtx[3])));comp_num_verts = (int)(TtGetGlyphShape(alloc, (int)(gidx), &comp_verts));if ((comp_num_verts) > (0)) {
for (i = (int)(0); (i) < (comp_num_verts); ++i) {
TtVertex* v = &comp_verts[i];short x;short y;x = (short)(v->x);y = (short)(v->y);v->x = ((short)(m * (mtx[0] * x + mtx[2] * y + mtx[4])));v->y = ((short)(n * (mtx[1] * x + mtx[3] * y + mtx[5])));x = (short)(v->cx);y = (short)(v->cy);v->cx = ((short)(m * (mtx[0] * x + mtx[2] * y + mtx[4])));v->cy = ((short)(n * (mtx[1] * x + mtx[3] * y + mtx[5])));}tmp = (TtVertex*)(CRuntime.malloc((ulong)((ulong)(num_vertices + comp_num_verts) * sizeof(TtVertex))));if (tmp== null) {
if ((vertices) != null) CRuntime.free(vertices);if ((comp_verts) != null) CRuntime.free(comp_verts);return (int)(0);}
if ((num_vertices) > (0)) Nuklear.Memcopy(tmp, vertices, (ulong)((ulong)(num_vertices) * sizeof(TtVertex)));Nuklear.Memcopy(tmp + num_vertices, comp_verts, (ulong)((ulong)(comp_num_verts) * sizeof(TtVertex)));if ((vertices) != null) CRuntime.free(vertices);vertices = tmp;CRuntime.free(comp_verts);num_vertices += (int)(comp_num_verts);}
more = (int)(flags & (1 << 5));}}
 else if ((numberOfContours) < (0)) {
}
 else {
}

			*pvertices = vertices;
			return (int)(num_vertices);
		}

		public void TtGetGlyphHMetrics(int glyph_index, int* advanceWidth, int* leftSideBearing)
		{
			ushort numOfLongHorMetrics = (ushort)(Nuklear.TtUSHORT(this.data + this.hhea + 34));
			if ((glyph_index) < (numOfLongHorMetrics)) {
if ((advanceWidth) != null) *advanceWidth = (int)(Nuklear.TtSHORT(this.data + this.hmtx + 4 * glyph_index));if ((leftSideBearing) != null) *leftSideBearing = (int)(Nuklear.TtSHORT(this.data + this.hmtx + 4 * glyph_index + 2));}
 else {
if ((advanceWidth) != null) *advanceWidth = (int)(Nuklear.TtSHORT(this.data + this.hmtx + 4 * (numOfLongHorMetrics - 1)));if ((leftSideBearing) != null) *leftSideBearing = (int)(Nuklear.TtSHORT(this.data + this.hmtx + 4 * numOfLongHorMetrics + 2 * (glyph_index - numOfLongHorMetrics)));}

		}

		public void TtGetFontVMetrics(int* ascent, int* descent, int* lineGap)
		{
			if ((ascent) != null) *ascent = (int)(Nuklear.TtSHORT(this.data + this.hhea + 4));
			if ((descent) != null) *descent = (int)(Nuklear.TtSHORT(this.data + this.hhea + 6));
			if ((lineGap) != null) *lineGap = (int)(Nuklear.TtSHORT(this.data + this.hhea + 8));
		}

		public float TtScaleForPixelHeight(float height)
		{
			int fheight = (int)(Nuklear.TtSHORT(this.data + this.hhea + 4) - Nuklear.TtSHORT(this.data + this.hhea + 6));
			return (float)(height / (float)(fheight));
		}

		public float TtScaleForMappingEmToPixels(float pixels)
		{
			int unitsPerEm = (int)(Nuklear.TtUSHORT(this.data + this.head + 18));
			return (float)(pixels / (float)(unitsPerEm));
		}

		public void TtGetGlyphBitmapBoxSubpixel(int glyph, float scale_x, float scale_y, float shift_x, float shift_y, int* ix0, int* iy0, int* ix1, int* iy1)
		{
			int x0;int y0;int x1;int y1;
			if (TtGetGlyphBox((int)(glyph), &x0, &y0, &x1, &y1)== 0) {
if ((ix0) != null) *ix0 = (int)(0);if ((iy0) != null) *iy0 = (int)(0);if ((ix1) != null) *ix1 = (int)(0);if ((iy1) != null) *iy1 = (int)(0);}
 else {
if ((ix0) != null) *ix0 = (int)(Nuklear.Ifloorf((float)((float)(x0) * scale_x + shift_x)));if ((iy0) != null) *iy0 = (int)(Nuklear.Ifloorf((float)((float)(-y1) * scale_y + shift_y)));if ((ix1) != null) *ix1 = (int)(Nuklear.Iceilf((float)((float)(x1) * scale_x + shift_x)));if ((iy1) != null) *iy1 = (int)(Nuklear.Iceilf((float)((float)(-y0) * scale_y + shift_y)));}

		}

		public void TtGetGlyphBitmapBox(int glyph, float scale_x, float scale_y, int* ix0, int* iy0, int* ix1, int* iy1)
		{
			TtGetGlyphBitmapBoxSubpixel((int)(glyph), (float)(scale_x), (float)(scale_y), (float)(0.0f), (float)(0.0f), ix0, iy0, ix1, iy1);
		}

		public void TtMakeGlyphBitmapSubpixel(byte* output, int out_w, int out_h, int out_stride, float scale_x, float scale_y, float shift_x, float shift_y, int glyph)
		{
			int ix0;int iy0;
			TtVertex* vertices;
			int num_verts = (int)(TtGetGlyphShape(alloc, (int)(glyph), &vertices));
			TtBitmap gbm =  new TtBitmap();
			TtGetGlyphBitmapBoxSubpixel((int)(glyph), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), &ix0, &iy0, null, null);
			gbm.pixels = output;
			gbm.w = (int)(out_w);
			gbm.h = (int)(out_h);
			gbm.stride = (int)(out_stride);
			if (((gbm.w) != 0) && ((gbm.h) != 0)) gbm.TtRasterize((float)(0.35f), vertices, (int)(num_verts), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), (int)(ix0), (int)(iy0), (int)(1));
			CRuntime.free(vertices);
		}

	}
}
