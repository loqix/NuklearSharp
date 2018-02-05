using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class FontAtlas
	{
		public void Begin()
		{
			
			if ((this.glyphs) != null) {
CRuntime.free(this.glyphs);this.glyphs = null;}

			if ((this.pixel) != null) {
CRuntime.free(this.pixel);this.pixel = null;}

		}

		public Font Add(FontConfig config)
		{
			Font font = null;
			FontConfig cfg;
			if (((((((((config== null)) || (config.ttf_blob== null)) || (config.ttf_size== 0)) || (config.size <= 0.0f)))))) return null;
			
			cfg = config;
			cfg.n = cfg;
			cfg.p = cfg;
			if (config.merge_mode== 0) {
if (this.config== null) {
this.config = cfg;cfg.next = null;}
 else {
FontConfig i = this.config;while ((i.next) != null) {i = i.next;}i.next = cfg;cfg.next = null;}
font = new Font();font.config = cfg;if (this.fonts== null) {
this.fonts = font;font.next = null;}
 else {
Font i = this.fonts;while ((i.next) != null) {i = i.next;}i.next = font;font.next = null;}
cfg.font = font.info;}
 else {
Font f = null;FontConfig c = null;f = this.fonts;c = f.config;cfg.font = f.info;cfg.n = c;cfg.p = c.p;c.p.n = cfg;c.p = cfg;}

			if (config.ttf_data_owned_by_atlas== 0) {
cfg.ttf_blob = CRuntime.malloc((ulong)(cfg.ttf_size));if (cfg.ttf_blob== null) {
this.font_num++;return null;}
Nuklear.Memcopy(cfg.ttf_blob, config.ttf_blob, (ulong)(cfg.ttf_size));cfg.ttf_data_owned_by_atlas = (byte)(1);}

			this.font_num++;
			return font;
		}

		public Font AddFromMemory(void * memory, ulong size, float height, FontConfig config)
		{
			FontConfig cfg =  new FontConfig();
			if (((((((this== null))) || (memory== null)) || (size== 0)))) return null;
			cfg = (FontConfig)((config != null)?config:Nuklear.FontConfigz((float)(height)));
			cfg.ttf_blob = memory;
			cfg.ttf_size = (ulong)(size);
			cfg.size = (float)(height);
			cfg.ttf_data_owned_by_atlas = (byte)(0);
			return Add(cfg);
		}

		public Font AddCompressed(void * compressed_data, ulong compressed_size, float height, FontConfig config)
		{
			uint decompressed_size;
			void * decompressed_data;
			FontConfig cfg =  new FontConfig();
			if ((((((compressed_data== null)))))) return null;
			decompressed_size = (uint)(Nuklear.DecompressLength((byte*)(compressed_data)));
			decompressed_data = CRuntime.malloc((ulong)(decompressed_size));
			if (decompressed_data== null) return null;
			Nuklear.Decompress((byte*)(decompressed_data), (byte*)(compressed_data), (uint)(compressed_size));
			cfg = (FontConfig)((config != null)?config:Nuklear.FontConfigz((float)(height)));
			cfg.ttf_blob = decompressed_data;
			cfg.ttf_size = (ulong)(decompressed_size);
			cfg.size = (float)(height);
			cfg.ttf_data_owned_by_atlas = (byte)(1);
			return Add(cfg);
		}

		public Font AddCompressedBase85(sbyte* data_base85, float height, FontConfig config)
		{
			int compressed_size;
			void * compressed_data;
			if ((((((data_base85== null)))))) return null;
			compressed_size = (int)(((Nuklear.Strlen(data_base85) + 4) / 5) * 4);
			compressed_data = CRuntime.malloc((ulong)(compressed_size));
			if (compressed_data== null) return null;
			Nuklear.Decode85((byte*)(compressed_data), (byte*)(data_base85));
			Font font = AddCompressed(compressed_data, (ulong)(compressed_size), (float)(height), config);
			CRuntime.free(compressed_data);
			return font;
		}

		public void * Bake(ref int width, ref int height, int fmt)
		{
			int i = (int)(0);
			void * tmp = null;
			ulong tmp_size;ulong img_size;
			Font font_iter;
			FontBaker* baker;
			if (((((((width== null)) || (height== null)))))) return null;
			if (this.font_num== 0) this.default_font = AddDefault((float)(13.0f), null);
			if (this.font_num== 0) return null;
			Nuklear.FontBakerMemory(&tmp_size, ref this.glyph_count, this.config, (int)(this.font_num));
			tmp = CRuntime.malloc((ulong)(tmp_size));
			if (tmp== null) goto failed;
			baker = Nuklear.FontBakerz(tmp, (int)(this.glyph_count), (int)(this.font_num));
			this.glyphs = (FontGlyph*)(CRuntime.malloc((ulong)((ulong)sizeof(FontGlyph) * (ulong)(this.glyph_count))));
			if (this.glyphs== null) goto failed;
			this.custom.w = (short)((90 * 2) + 1);
			this.custom.h = (short)(27 + 1);
			if (baker->FontBakePack(&img_size, ref width, ref height, ref this.custom, this.config, (int)(this.font_num))== 0) goto failed;
			this.pixel = CRuntime.malloc((ulong)(img_size));
			if (this.pixel== null) goto failed;
			baker->FontBake(this.pixel, (int)(width), (int)(height), this.glyphs, (int)(this.glyph_count), this.config, (int)(this.font_num));
			Nuklear.FontBakeCustomData(this.pixel, (int)(width), (int)(height), (Recti)(this.custom), Nuklear.nk_custom_cursor_data, (int)(90), (int)(27), ('.'), ('X'));
			if ((fmt) == (Nuklear.NK_FONT_ATLAS_RGBA32)) {
void * img_rgba = CRuntime.malloc((ulong)(width * height * 4));if (img_rgba== null) goto failed;Nuklear.FontBakeConvert(img_rgba, (int)(width), (int)(height), this.pixel);CRuntime.free(this.pixel);this.pixel = img_rgba;}

			this.tex_width = (int)(width);
			this.tex_height = (int)(height);
			for (font_iter = this.fonts; font_iter != null; font_iter = font_iter.next) {
Font font = font_iter;FontConfig config = font.config;font.Init((float)(config.size), (uint)(config.fallback_glyph), this.glyphs, config.font, (Handle)(Nuklear.HandlePtr(null)));}
			for (i = (int)(0); (i) < (Nuklear.NK_CURSOR_COUNT); ++i) {
Cursor cursor = this.cursors[i];cursor.img.w = ((ushort)(width));cursor.img.h = ((ushort)(height));cursor.img.region[0] = ((ushort)(this.custom.x + Nuklear.nk_cursor_data[i][0].x));cursor.img.region[1] = ((ushort)(this.custom.y + Nuklear.nk_cursor_data[i][0].y));cursor.img.region[2] = ((ushort)(Nuklear.nk_cursor_data[i][1].x));cursor.img.region[3] = ((ushort)(Nuklear.nk_cursor_data[i][1].y));cursor.size = (Vec2)(Nuklear.nk_cursor_data[i][1]);cursor.offset = (Vec2)(Nuklear.nk_cursor_data[i][2]);}
			CRuntime.free(tmp);
			return this.pixel;
			failed:;
if ((tmp) != null) CRuntime.free(tmp);
			if ((this.glyphs) != null) {
CRuntime.free(this.glyphs);this.glyphs = null;}

			if ((this.pixel) != null) {
CRuntime.free(this.pixel);this.pixel = null;}

			return null;
		}

		public void End(Handle texture, DrawNullTexture* _null_)
		{
			int i = (int)(0);
			Font font_iter;
			if (this== null) {
if (_null_== null) return;_null_->texture = (Handle)(texture);_null_->uv = (Vec2)(Nuklear.Vec2z((float)(0.5f), (float)(0.5f)));}

			if ((_null_) != null) {
_null_->texture = (Handle)(texture);_null_->uv.x = (float)((this.custom.x + 0.5f) / (float)(this.tex_width));_null_->uv.y = (float)((this.custom.y + 0.5f) / (float)(this.tex_height));}

			for (font_iter = this.fonts; font_iter != null; font_iter = font_iter.next) {
font_iter.texture = (Handle)(texture);font_iter.handle.texture = (Handle)(texture);}
			for (i = (int)(0); (i) < (Nuklear.NK_CURSOR_COUNT); ++i) {this.cursors[i].img.handle = (Handle)(texture);}
			CRuntime.free(this.pixel);
			this.pixel = null;
			this.tex_width = (int)(0);
			this.tex_height = (int)(0);
			this.custom.x = (short)(0);
			this.custom.y = (short)(0);
			this.custom.w = (short)(0);
			this.custom.h = (short)(0);
		}

		public void Cleanup()
		{
			if (((this== null))) return;
			if ((this.config) != null) {
FontConfig iter;for (iter = this.config; iter != null; iter = iter.next) {
FontConfig i;for (i = iter.n; i != iter; i = i.n) {
CRuntime.free(i.ttf_blob);i.ttf_blob = null;}CRuntime.free(iter.ttf_blob);iter.ttf_blob = null;}}

		}

		public void Clear()
		{
			if (((this== null))) return;
			if ((this.config) != null) {
FontConfig iter;FontConfig next;for (iter = this.config; iter != null; iter = next) {
FontConfig i;FontConfig n;for (i = iter.n; i != iter; i = n) {
n = i.n;if ((i.ttf_blob) != null) CRuntime.free(i.ttf_blob);}next = iter.next;if ((i.ttf_blob) != null) CRuntime.free(iter.ttf_blob);}this.config = null;}

			if ((this.fonts) != null) {
Font iter;Font next;for (iter = this.fonts; iter != null; iter = next) {
next = iter.next;}this.fonts = null;}

			if ((this.glyphs) != null) CRuntime.free(this.glyphs);
			
		}

	}
}