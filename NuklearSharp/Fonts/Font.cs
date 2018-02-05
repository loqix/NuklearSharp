using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class Font
	{
		public float TextWidth(float height, char* text, int len)
		{
			char unicode;
			int text_len = (int)(0);
			float text_width = (float)(0);
			int glyph_len = (int)(0);
			float scale = (float)(0);
			if (((text== null)) || (len== 0)) return (float)(0);
			scale = (float)(height / this.info.height);
			glyph_len = (int)(text_len = (int)(Nuklear.UtfDecode(text, &unicode, (int)(len))));
			if (glyph_len== 0) return (float)(0);
			while ((text_len <= len) && ((glyph_len) != 0)) {
FontGlyph* g;if ((unicode) == (0xFFFD)) break;g = FindGlyph(unicode);text_width += (float)(g->xadvance * scale);glyph_len = (int)(Nuklear.UtfDecode(text + text_len, &unicode, (int)(len - text_len)));text_len += (int)(glyph_len);}
			return (float)(text_width);
		}

		public void QueryFontGlyph(float height, UserFontGlyph* glyph, char codepoint, char next_codepoint)
		{
			float scale;
			FontGlyph* g;
			if ((glyph== null)) return;
			scale = (float)(height / this.info.height);
			g = FindGlyph(codepoint);
			glyph->width = (float)((g->x1 - g->x0) * scale);
			glyph->height = (float)((g->y1 - g->y0) * scale);
			glyph->offset = (Vec2)(Nuklear.Vec2z((float)(g->x0 * scale), (float)(g->y0 * scale)));
			glyph->xadvance = (float)(g->xadvance * scale);
			glyph->uv_x[0] = g->u0; glyph->uv_y[0] = g->v0;
			glyph->uv_x[1] = g->u1; glyph->uv_y[1] = g->v1;
		}

		public FontGlyph* FindGlyph(char unicode)
		{
			int i = (int)(0);
			int count;
			int total_glyphs = (int)(0);
			FontGlyph* glyph = null;
			FontConfig iter = null;
			if ((this.glyphs== null)) return null;
			glyph = this.fallback;
			iter = this.config;
			do {
count = (int)(Nuklear.RangeCount(iter.range));for (i = (int)(0); (i) < (count); ++i) {
uint f = (uint)(iter.range[(i * 2) + 0]);uint t = (uint)(iter.range[(i * 2) + 1]);int diff = (int)((t - f) + 1);if (((unicode) >= (f)) && (unicode <= t)) return &this.glyphs[((uint)(total_glyphs) + (unicode - f))];total_glyphs += (int)(diff);}}
 while ((iter = iter.n) != this.config);
			return glyph;
		}

		public void Init(float pixel_height, char fallback_codepoint, FontGlyph* glyphs, BakedFont baked_font, Handle atlas)
		{
			BakedFont baked =  new BakedFont();
			if (((glyphs== null)) || (baked_font== null)) return;
			baked = (BakedFont)(baked_font);
			this.fallback = null;
			this.info = (BakedFont)(baked);
			this.scale = (float)(pixel_height / this.info.height);
			this.glyphs = &glyphs[baked_font.glyph_offset];
			this.texture = (Handle)(atlas);
			this.fallback_codepoint = fallback_codepoint;
			this.fallback = FindGlyph(fallback_codepoint);
			this.handle.height = (float)(this.info.height * this.scale);
			this.handle.width = this.TextWidth;
			
			this.handle.query = this.QueryFontGlyph;
			this.handle.texture = (Handle)(this.texture);
		}

	}
}
