using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TextEditRow
	{
		public float x0;
		public float x1;
		public float baseline_y_delta;
		public float ymin;
		public float ymax;
		public int num_chars;

		public void TexteditLayoutRow(TextEdit edit, int line_start_id, float row_height, UserFont font)
		{
			int l;
			int glyphs = (int)(0);
			char unicode;
			char* remaining;
			int len = (int)(edit._string_.LenChar());
			char* end = edit._string_.GetConst() + len;
			char* text = edit._string_.AtConst((int)(line_start_id), &unicode, ref l);
			Vec2 size = (Vec2)(font.TextCalculateTextBounds(text, (int)(end - text), (float)(row_height), &remaining, null, &glyphs, (int)(Nuklear.NK_STOP_ON_NEW_LINE)));
			this.x0 = (float)(0.0f);
			this.x1 = (float)(size.x);
			this.baseline_y_delta = (float)(size.y);
			this.ymin = (float)(0.0f);
			this.ymax = (float)(size.y);
			this.num_chars = (int)(glyphs);
		}

	}
}
