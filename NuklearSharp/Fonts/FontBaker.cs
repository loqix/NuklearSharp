using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct FontBaker
	{

		public TtPackContext spc;
		public FontBakeData* build;
		public TtPackedchar* packed_chars;
		public RpRect* rects;
		public TtPackRange* ranges;

		public int FontBakePack(ulong* image_memory, ref int width, ref int height, ref Recti custom, FontConfig config_list,
			int count)
		{
			ulong max_height = (ulong) (1024*32);
			FontConfig config_iter;
			FontConfig it;
			int total_glyph_count = (int) (0);
			int total_range_count = (int) (0);
			int range_count = (int) (0);
			int i = (int) (0);
			if (((((image_memory == null) || (width == null)) || (height == null)) || (config_list == null)) || (count == 0))
				return (int) (Nuklear.nk_false);
			for (config_iter = config_list; config_iter != null; config_iter = config_iter.next)
			{
				it = config_iter;
				do
				{
					range_count = (int) (Nuklear.RangeCount(it.range));
					total_range_count += (int) (range_count);
					total_glyph_count += (int) (Nuklear.RangeGlyphCount(it.range, (int) (range_count)));
				} while ((it = it.n) != config_iter);
			}
			for (config_iter = config_list; config_iter != null; config_iter = config_iter.next)
			{
				it = config_iter;
				do
				{
					if (this.build[i++].info.TtInitFont((byte*) (it.ttf_blob), (int) (0)) == 0) return (int) (Nuklear.nk_false);
				} while ((it = it.n) != config_iter);
			}
			height = (int) (0);
			width = (int) (((total_glyph_count) > (1000)) ? 1024 : 512);
			this.spc.TtPackBegin(null, (int) (width), (int) (max_height), (int) (0), (int) (1));
			{
				int input_i = (int) (0);
				int range_n = (int) (0);
				int rect_n = (int) (0);
				int char_n = (int) (0);
				{
					RpRect custom_space = new RpRect();
					Nuklear.Zero(&custom_space, (ulong) (sizeof (RpRect)));
					custom_space.w = ((ushort) ((custom.w*2) + 1));
					custom_space.h = ((ushort) (custom.h + 1));
					this.spc.TtPackSetOversampling((uint) (1), (uint) (1));
					(RpContext*) (this.spc.pack_info)->RpPackRects(&custom_space, (int) (1));
					height = (int) ((height) < (custom_space.y + custom_space.h) ? (custom_space.y + custom_space.h) : (height));
					custom.x = ((short) (custom_space.x));
					custom.y = ((short) (custom_space.y));
					custom.w = ((short) (custom_space.w));
					custom.h = ((short) (custom_space.h));
				}
				for (input_i = (int) (0) , config_iter = config_list;
					((input_i) < (count)) && ((config_iter) != null);
					config_iter = config_iter.next)
				{
					it = config_iter;
					do
					{
						int n = (int) (0);
						int glyph_count;
						uint* in_range;
						FontConfig cfg = it;
						FontBakeData* tmp = &this.build[input_i++];
						glyph_count = (int) (0);
						range_count = (int) (0);
						for (in_range = cfg.range; ((in_range[0]) != 0) && ((in_range[1]) != 0); in_range += 2)
						{
							glyph_count += (int) ((int) (in_range[1] - in_range[0]) + 1);
							range_count++;
						}
						tmp->ranges = this.ranges + range_n;
						tmp->range_count = ((uint) (range_count));
						range_n += (int) (range_count);
						for (i = (int) (0); (i) < (range_count); ++i)
						{
							in_range = &cfg.range[i*2];
							tmp->ranges[i].font_size = (float) (cfg.size);
							tmp->ranges[i].first_unicode_codepoint_in_range = ((int) (in_range[0]));
							tmp->ranges[i].num_chars = (int) ((int) (in_range[1] - in_range[0]) + 1);
							tmp->ranges[i].chardata_for_range = this.packed_chars + char_n;
							char_n += (int) (tmp->ranges[i].num_chars);
						}
						tmp->rects = this.rects + rect_n;
						rect_n += (int) (glyph_count);
						this.spc.TtPackSetOversampling((uint) (cfg.oversample_h), (uint) (cfg.oversample_v));
						n = (int) (this.spc.TtPackFontRangesGatherRects(&tmp->info, tmp->ranges, (int) (tmp->range_count), tmp->rects));
						(RpContext*) (this.spc.pack_info)->RpPackRects(tmp->rects, (int) (n));
						for (i = (int) (0); (i) < (n); ++i)
						{
							if ((tmp->rects[i].was_packed) != 0)
								height = (int) ((height) < (tmp->rects[i].y + tmp->rects[i].h) ? (tmp->rects[i].y + tmp->rects[i].h) : (height));
						}
					} while ((it = it.n) != config_iter);
				}
			}

			height = ((int) (Nuklear.RoundUpPow2((uint) (height))));
			*image_memory = (ulong) ((ulong) (width)*(ulong) (height));
			return (int) (Nuklear.nk_true);
		}

		public void FontBake(void* image_memory, int width, int height, FontGlyph* glyphs, int glyphs_count,
			FontConfig config_list, int font_count)
		{
			int input_i = (int) (0);
			uint glyph_n = (uint) (0);
			FontConfig config_iter;
			FontConfig it;
			if (((((((image_memory == null) || (width == 0)) || (height == 0)) || (config_list == null)) || (font_count == 0)) ||
			     (glyphs == null)) || (glyphs_count == 0)) return;
			Nuklear.Zero(image_memory, (ulong) ((ulong) (width)*(ulong) (height)));
			this.spc.pixels = (byte*) (image_memory);
			this.spc.height = (int) (height);
			for (input_i = (int) (0) , config_iter = config_list;
				((input_i) < (font_count)) && ((config_iter) != null);
				config_iter = config_iter.next)
			{
				it = config_iter;
				do
				{
					FontConfig cfg = it;
					FontBakeData* tmp = &this.build[input_i++];
					this.spc.TtPackSetOversampling((uint) (cfg.oversample_h), (uint) (cfg.oversample_v));
					this.spc.TtPackFontRangesRenderIntoRects(&tmp->info, tmp->ranges, (int) (tmp->range_count), tmp->rects, &this.alloc);
				} while ((it = it.n) != config_iter);
			}
			this.spc.TtPackEnd(&this.alloc);
			for (input_i = (int) (0) , config_iter = config_list;
				((input_i) < (font_count)) && ((config_iter) != null);
				config_iter = config_iter.next)
			{
				it = config_iter;
				do
				{
					ulong i = (ulong) (0);
					int char_idx = (int) (0);
					uint glyph_count = (uint) (0);
					FontConfig cfg = it;
					FontBakeData* tmp = &this.build[input_i++];
					BakedFont dst_font = cfg.font;
					float font_scale = (float) (tmp->info.TtScaleForPixelHeight((float) (cfg.size)));
					int unscaled_ascent;
					int unscaled_descent;
					int unscaled_line_gap;
					tmp->info.TtGetFontVMetrics(&unscaled_ascent, &unscaled_descent, &unscaled_line_gap);
					if (cfg.merge_mode == 0)
					{
						dst_font.ranges = cfg.range;
						dst_font.height = (float) (cfg.size);
						dst_font.ascent = (float) ((float) (unscaled_ascent)*font_scale);
						dst_font.descent = (float) ((float) (unscaled_descent)*font_scale);
						dst_font.glyph_offset = (uint) (glyph_n);
					}
					for (i = (ulong) (0); (i) < (tmp->range_count); ++i)
					{
						TtPackRange* range = &tmp->ranges[i];
						for (char_idx = (int) (0); (char_idx) < (range->num_chars); char_idx++)
						{
							char codepoint = (char) 0;
							float dummy_x = (float) (0);
							float dummy_y = (float) (0);
							TtAlignedQuad q = new TtAlignedQuad();
							FontGlyph* glyph;
							TtPackedchar* pc = &range->chardata_for_range[char_idx];
							if ((((pc->x0 == 0) && (pc->x1 == 0)) && (pc->y0 == 0)) && (pc->y1 == 0)) continue;
							codepoint = ((char) (range->first_unicode_codepoint_in_range + char_idx));
							range->chardata_for_range->TtGetPackedQuad((int) (width), (int) (height), (int) (char_idx), &dummy_x, &dummy_y,
								&q, (int) (0));
							glyph = &glyphs[dst_font.glyph_offset + dst_font.glyph_count + glyph_count];
							glyph->codepoint = codepoint;
							glyph->x0 = (float) (q.x0);
							glyph->y0 = (float) (q.y0);
							glyph->x1 = (float) (q.x1);
							glyph->y1 = (float) (q.y1);
							glyph->y0 += (float) (dst_font.ascent + 0.5f);
							glyph->y1 += (float) (dst_font.ascent + 0.5f);
							glyph->w = (float) (glyph->x1 - glyph->x0 + 0.5f);
							glyph->h = (float) (glyph->y1 - glyph->y0);
							if ((cfg.coord_type) == (Nuklear.NK_COORD_PIXEL))
							{
								glyph->u0 = (float) (q.s0*(float) (width));
								glyph->v0 = (float) (q.t0*(float) (height));
								glyph->u1 = (float) (q.s1*(float) (width));
								glyph->v1 = (float) (q.t1*(float) (height));
							}
							else
							{
								glyph->u0 = (float) (q.s0);
								glyph->v0 = (float) (q.t0);
								glyph->u1 = (float) (q.s1);
								glyph->v1 = (float) (q.t1);
							}
							glyph->xadvance = (float) (pc->xadvance + cfg.spacing.x);
							if ((cfg.pixel_snap) != 0) glyph->xadvance = ((float) ((int) (glyph->xadvance + 0.5f)));
							glyph_count++;
						}
					}
					dst_font.glyph_count += (uint) (glyph_count);
					glyph_n += (uint) (glyph_count);
				} while ((it = it.n) != config_iter);
			}
		}

	}
}