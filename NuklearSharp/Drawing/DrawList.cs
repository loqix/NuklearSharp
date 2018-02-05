using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class DrawList
	{
		public void Init()
		{
			ulong i = (ulong)(0);
			if (this== null) return;
			
			for (i = (ulong)(0); (i) < (ulong)this.circle_vtx.Length; ++i) {
float a = (float)(((float)(i) / (float)(ulong)this.circle_vtx.Length) * 2 * 3.141592654f);this.circle_vtx[i].x = (float)(Nuklear.Cos((float)(a)));this.circle_vtx[i].y = (float)(Nuklear.Sin((float)(a)));}
		}

		public void Setup(ConvertConfig config, Buffer cmds, Buffer vertices, Buffer elements, int line_aa, int shape_aa)
		{
			if (((((config== null)) || (cmds== null)) || (vertices== null)) || (elements== null)) return;
			this.buffer = cmds;
			this.config = (ConvertConfig)(config);
			this.elements = elements;
			this.vertices = vertices;
			this.line_AA = (int)(line_aa);
			this.shape_AA = (int)(shape_aa);
			this.clip_rect = (Rect)(Nuklear.nk_null_rect);
		}

		public DrawCommand* Begin(Buffer buffer)
		{
			byte* memory;
			ulong offset;
			DrawCommand* cmd;
			if (((buffer== null) || (buffer.size== 0)) || (this.cmd_count== 0)) return null;
			memory = (byte*)(buffer.memory.ptr);
			offset = (ulong)(buffer.memory.size - this.cmd_offset);
			cmd = ((DrawCommand*)((void *)((memory) + (offset))));
			return cmd;
		}

		public DrawCommand* End(Buffer buffer)
		{
			ulong size;
			ulong offset;
			byte* memory;
			DrawCommand* end;
			if ((buffer== null) || (this== null)) return null;
			memory = (byte*)(buffer.memory.ptr);
			size = (ulong)(buffer.memory.size);
			offset = (ulong)(size - this.cmd_offset);
			end = ((DrawCommand*)((void *)((memory) + (offset))));
			end -= (this.cmd_count - 1);
			return end;
		}

		public void Clear()
		{
			if (this== null) return;
			if ((this.buffer) != null) this.buffer.Clear();
			if ((this.vertices) != null) this.vertices.Clear();
			if ((this.elements) != null) this.elements.Clear();
			this.element_count = (uint)(0);
			this.vertex_count = (uint)(0);
			this.cmd_offset = (ulong)(0);
			this.cmd_count = (uint)(0);
			this.path_count = (uint)(0);
			this.vertices = null;
			this.elements = null;
			this.clip_rect = (Rect)(Nuklear.nk_null_rect);
		}

		public Vec2* AllocPath(int count)
		{
			Vec2* points;
			ulong point_align = (ulong)(4);
			ulong point_size = (ulong)(sizeof(Vec2));
			points = (Vec2*)(this.buffer.Alloc((int)(Nuklear.NK_BUFFER_FRONT), (ulong)(point_size * (ulong)(count)), (ulong)(point_align)));
			if (points== null) return null;
			if (this.path_offset== 0) {
void * memory = this.buffer.Memory();this.path_offset = ((uint)((byte*)(points) - (byte*)(memory)));}

			this.path_count += ((uint)(count));
			return points;
		}

		public Vec2 PathLast()
		{
			void * memory;
			Vec2* point;
			memory = this.buffer.Memory();
			point = ((Vec2*)((void *)((byte*)(memory) + (this.path_offset))));
			point += (this.path_count - 1);
			return (Vec2)(*point);
		}

		public DrawCommand* PushCommand(Rect clip, Handle texture)
		{
			ulong cmd_align = (ulong)(4);
			ulong cmd_size = (ulong)(sizeof(DrawCommand));
			DrawCommand* cmd;
			cmd = (DrawCommand*)(this.buffer.Alloc((int)(Nuklear.NK_BUFFER_BACK), (ulong)(cmd_size), (ulong)(cmd_align)));
			if (cmd== null) return null;
			if (this.cmd_count== 0) {
byte* memory = (byte*)(this.buffer.Memory());ulong total = (ulong)(this.buffer.Total());memory = ((byte*)((void *)((memory) + (total))));this.cmd_offset = ((ulong)(memory - (byte*)(cmd)));}

			cmd->elem_count = (uint)(0);
			cmd->clip_rect = (Rect)(clip);
			cmd->texture = (Handle)(texture);
			cmd->userdata = (Handle)(this.userdata);
			this.cmd_count++;
			this.clip_rect = (Rect)(clip);
			return cmd;
		}

		public DrawCommand* CommandLast()
		{
			void * memory;
			ulong size;
			DrawCommand* cmd;
			memory = this.buffer.Memory();
			size = (ulong)(this.buffer.Total());
			cmd = ((DrawCommand*)((void *)((byte*)(memory) + (size - this.cmd_offset))));
			return (cmd - (this.cmd_count - 1));
		}

		public void AddClip(Rect rect)
		{
			if (this== null) return;
			if (this.cmd_count== 0) {
PushCommand((Rect)(rect), (Handle)(this.config._null_.texture));}
 else {
DrawCommand* prev = CommandLast();if ((prev->elem_count) == (0)) prev->clip_rect = (Rect)(rect);PushCommand((Rect)(rect), (Handle)(prev->texture));}

		}

		public void PushImage(Handle texture)
		{
			if (this== null) return;
			if (this.cmd_count== 0) {
PushCommand((Rect)(Nuklear.nk_null_rect), (Handle)(texture));}
 else {
DrawCommand* prev = CommandLast();if ((prev->elem_count) == (0)) {
prev->texture = (Handle)(texture);prev->userdata = (Handle)(this.userdata);}
 else if ((prev->texture.id != texture.id) || (prev->userdata.id != this.userdata.id)) PushCommand((Rect)(prev->clip_rect), (Handle)(texture));}

		}

		public void PushUserdata(Handle userdata)
		{
			this.userdata = (Handle)(userdata);
		}

		public void * AllocVertices(ulong count)
		{
			void * vtx;
			if (this== null) return null;
			vtx = this.vertices.Alloc((int)(Nuklear.NK_BUFFER_FRONT), (ulong)(this.config.vertex_size * count), (ulong)(this.config.vertex_alignment));
			if (vtx== null) return null;
			this.vertex_count += ((uint)(count));
			return vtx;
		}

		public ushort* AllocElements(ulong count)
		{
			ushort* ids;
			DrawCommand* cmd;
			ulong elem_align = (ulong)(4);
			ulong elem_size = (ulong)(sizeof(ushort));
			if (this== null) return null;
			ids = (ushort*)(this.elements.Alloc((int)(Nuklear.NK_BUFFER_FRONT), (ulong)(elem_size * count), (ulong)(elem_align)));
			if (ids== null) return null;
			cmd = CommandLast();
			this.element_count += ((uint)(count));
			cmd->elem_count += ((uint)(count));
			return ids;
		}

		public void StrokePolyLine(Vec2* points, uint points_count, Color color, int closed, float thickness, int aliasing)
		{
			ulong count;
			int thick_line;
			Colorf col =  new Colorf();
			Colorf col_trans =  new Colorf();
			if (((points_count) < (2))) return;
			color.a = ((byte)((float)(color.a) * this.config.global_alpha));
			count = (ulong)(points_count);
			if (closed== 0) count = (ulong)(points_count - 1);
			thick_line = (int)((thickness) > (1.0f)?1:0);
			PushUserdata((Handle)(this.userdata));
			color.a = ((byte)((float)(color.a) * this.config.global_alpha));
			Nuklear.ColorFv(&col.r, (Color)(color));
			col_trans = (Colorf)(col);
			col_trans.a = (float)(0);
			if ((aliasing) == (Nuklear.NK_ANTI_ALIASING_ON)) {
float AA_SIZE = (float)(1.0f);ulong pnt_align = (ulong)(4);ulong pnt_size = (ulong)(sizeof(Vec2));ulong i1 = (ulong)(0);ulong vertex_offset;ulong index = (ulong)(this.vertex_count);ulong idx_count = (ulong)((thick_line) != 0?(count * 18):(count * 12));ulong vtx_count = (ulong)((thick_line) != 0?(points_count * 4):(points_count * 3));void * vtx = AllocVertices((ulong)(vtx_count));ushort* ids = AllocElements((ulong)(idx_count));ulong size;Vec2* normals;Vec2* temp;if ((vtx== null) || (ids== null)) return;vertex_offset = ((ulong)((byte*)(vtx) - (byte*)(this.vertices.memory.ptr)));this.vertices.Mark((int)(Nuklear.NK_BUFFER_FRONT));size = (ulong)(pnt_size * (ulong)((thick_line) != 0?5:3) * points_count);normals = (Vec2*)(this.vertices.Alloc((int)(Nuklear.NK_BUFFER_FRONT), (ulong)(size), (ulong)(pnt_align)));if (normals== null) return;temp = normals + points_count;vtx = (void *)((byte*)(this.vertices.memory.ptr) + vertex_offset);for (i1 = (ulong)(0); (i1) < (count); ++i1) {
ulong i2 = (ulong)(((i1 + 1) == (points_count))?0:(i1 + 1));Vec2 diff = (Vec2)(Nuklear.Vec2z((float)((points[i2]).x - (points[i1]).x), (float)((points[i2]).y - (points[i1]).y)));float len;len = (float)((diff).x * (diff).x + (diff).y * (diff).y);if (len != 0.0f) len = (float)(Nuklear.InvSqrt((float)(len))); else len = (float)(1.0f);diff = (Vec2)(Nuklear.Vec2z((float)((diff).x * (len)), (float)((diff).y * (len))));normals[i1].x = (float)(diff.y);normals[i1].y = (float)(-diff.x);}if (closed== 0) normals[points_count - 1] = (Vec2)(normals[points_count - 2]);if (thick_line== 0) {
ulong idx1;ulong i;if (closed== 0) {
Vec2 d =  new Vec2();temp[0] = (Vec2)(Nuklear.Vec2z((float)((points[0]).x + (Nuklear.Vec2z((float)((normals[0]).x * (AA_SIZE)), (float)((normals[0]).y * (AA_SIZE)))).x), (float)((points[0]).y + (Nuklear.Vec2z((float)((normals[0]).x * (AA_SIZE)), (float)((normals[0]).y * (AA_SIZE)))).y)));temp[1] = (Vec2)(Nuklear.Vec2z((float)((points[0]).x - (Nuklear.Vec2z((float)((normals[0]).x * (AA_SIZE)), (float)((normals[0]).y * (AA_SIZE)))).x), (float)((points[0]).y - (Nuklear.Vec2z((float)((normals[0]).x * (AA_SIZE)), (float)((normals[0]).y * (AA_SIZE)))).y)));d = (Vec2)(Nuklear.Vec2z((float)((normals[points_count - 1]).x * (AA_SIZE)), (float)((normals[points_count - 1]).y * (AA_SIZE))));temp[(points_count - 1) * 2 + 0] = (Vec2)(Nuklear.Vec2z((float)((points[points_count - 1]).x + (d).x), (float)((points[points_count - 1]).y + (d).y)));temp[(points_count - 1) * 2 + 1] = (Vec2)(Nuklear.Vec2z((float)((points[points_count - 1]).x - (d).x), (float)((points[points_count - 1]).y - (d).y)));}
idx1 = (ulong)(index);for (i1 = (ulong)(0); (i1) < (count); i1++) {
Vec2 dm =  new Vec2();float dmr2;ulong i2 = (ulong)(((i1 + 1) == (points_count))?0:(i1 + 1));ulong idx2 = (ulong)(((i1 + 1) == (points_count))?index:(idx1 + 3));dm = (Vec2)(Nuklear.Vec2z((float)((Nuklear.Vec2z((float)((normals[i1]).x + (normals[i2]).x), (float)((normals[i1]).y + (normals[i2]).y))).x * (0.5f)), (float)((Nuklear.Vec2z((float)((normals[i1]).x + (normals[i2]).x), (float)((normals[i1]).y + (normals[i2]).y))).y * (0.5f))));dmr2 = (float)(dm.x * dm.x + dm.y * dm.y);if ((dmr2) > (0.000001f)) {
float scale = (float)(1.0f / dmr2);scale = (float)((100.0f) < (scale)?(100.0f):(scale));dm = (Vec2)(Nuklear.Vec2z((float)((dm).x * (scale)), (float)((dm).y * (scale))));}
dm = (Vec2)(Nuklear.Vec2z((float)((dm).x * (AA_SIZE)), (float)((dm).y * (AA_SIZE))));temp[i2 * 2 + 0] = (Vec2)(Nuklear.Vec2z((float)((points[i2]).x + (dm).x), (float)((points[i2]).y + (dm).y)));temp[i2 * 2 + 1] = (Vec2)(Nuklear.Vec2z((float)((points[i2]).x - (dm).x), (float)((points[i2]).y - (dm).y)));ids[0] = ((ushort)(idx2 + 0));ids[1] = ((ushort)(idx1 + 0));ids[2] = ((ushort)(idx1 + 2));ids[3] = ((ushort)(idx1 + 2));ids[4] = ((ushort)(idx2 + 2));ids[5] = ((ushort)(idx2 + 0));ids[6] = ((ushort)(idx2 + 1));ids[7] = ((ushort)(idx1 + 1));ids[8] = ((ushort)(idx1 + 0));ids[9] = ((ushort)(idx1 + 0));ids[10] = ((ushort)(idx2 + 0));ids[11] = ((ushort)(idx2 + 1));ids += 12;idx1 = (ulong)(idx2);}for (i = (ulong)(0); (i) < (points_count); ++i) {
Vec2 uv = (Vec2)(this.config._null_.uv);vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(points[i]), (Vec2)(uv), (Colorf)(col));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(temp[i * 2 + 0]), (Vec2)(uv), (Colorf)(col_trans));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(temp[i * 2 + 1]), (Vec2)(uv), (Colorf)(col_trans));}}
 else {
ulong idx1;ulong i;float half_inner_thickness = (float)((thickness - AA_SIZE) * 0.5f);if (closed== 0) {
Vec2 d1 = (Vec2)(Nuklear.Vec2z((float)((normals[0]).x * (half_inner_thickness + AA_SIZE)), (float)((normals[0]).y * (half_inner_thickness + AA_SIZE))));Vec2 d2 = (Vec2)(Nuklear.Vec2z((float)((normals[0]).x * (half_inner_thickness)), (float)((normals[0]).y * (half_inner_thickness))));temp[0] = (Vec2)(Nuklear.Vec2z((float)((points[0]).x + (d1).x), (float)((points[0]).y + (d1).y)));temp[1] = (Vec2)(Nuklear.Vec2z((float)((points[0]).x + (d2).x), (float)((points[0]).y + (d2).y)));temp[2] = (Vec2)(Nuklear.Vec2z((float)((points[0]).x - (d2).x), (float)((points[0]).y - (d2).y)));temp[3] = (Vec2)(Nuklear.Vec2z((float)((points[0]).x - (d1).x), (float)((points[0]).y - (d1).y)));d1 = (Vec2)(Nuklear.Vec2z((float)((normals[points_count - 1]).x * (half_inner_thickness + AA_SIZE)), (float)((normals[points_count - 1]).y * (half_inner_thickness + AA_SIZE))));d2 = (Vec2)(Nuklear.Vec2z((float)((normals[points_count - 1]).x * (half_inner_thickness)), (float)((normals[points_count - 1]).y * (half_inner_thickness))));temp[(points_count - 1) * 4 + 0] = (Vec2)(Nuklear.Vec2z((float)((points[points_count - 1]).x + (d1).x), (float)((points[points_count - 1]).y + (d1).y)));temp[(points_count - 1) * 4 + 1] = (Vec2)(Nuklear.Vec2z((float)((points[points_count - 1]).x + (d2).x), (float)((points[points_count - 1]).y + (d2).y)));temp[(points_count - 1) * 4 + 2] = (Vec2)(Nuklear.Vec2z((float)((points[points_count - 1]).x - (d2).x), (float)((points[points_count - 1]).y - (d2).y)));temp[(points_count - 1) * 4 + 3] = (Vec2)(Nuklear.Vec2z((float)((points[points_count - 1]).x - (d1).x), (float)((points[points_count - 1]).y - (d1).y)));}
idx1 = (ulong)(index);for (i1 = (ulong)(0); (i1) < (count); ++i1) {
Vec2 dm_out =  new Vec2();Vec2 dm_in =  new Vec2();ulong i2 = (ulong)(((i1 + 1) == (points_count))?0:(i1 + 1));ulong idx2 = (ulong)(((i1 + 1) == (points_count))?index:(idx1 + 4));Vec2 dm = (Vec2)(Nuklear.Vec2z((float)((Nuklear.Vec2z((float)((normals[i1]).x + (normals[i2]).x), (float)((normals[i1]).y + (normals[i2]).y))).x * (0.5f)), (float)((Nuklear.Vec2z((float)((normals[i1]).x + (normals[i2]).x), (float)((normals[i1]).y + (normals[i2]).y))).y * (0.5f))));float dmr2 = (float)(dm.x * dm.x + dm.y * dm.y);if ((dmr2) > (0.000001f)) {
float scale = (float)(1.0f / dmr2);scale = (float)((100.0f) < (scale)?(100.0f):(scale));dm = (Vec2)(Nuklear.Vec2z((float)((dm).x * (scale)), (float)((dm).y * (scale))));}
dm_out = (Vec2)(Nuklear.Vec2z((float)((dm).x * ((half_inner_thickness) + AA_SIZE)), (float)((dm).y * ((half_inner_thickness) + AA_SIZE))));dm_in = (Vec2)(Nuklear.Vec2z((float)((dm).x * (half_inner_thickness)), (float)((dm).y * (half_inner_thickness))));temp[i2 * 4 + 0] = (Vec2)(Nuklear.Vec2z((float)((points[i2]).x + (dm_out).x), (float)((points[i2]).y + (dm_out).y)));temp[i2 * 4 + 1] = (Vec2)(Nuklear.Vec2z((float)((points[i2]).x + (dm_in).x), (float)((points[i2]).y + (dm_in).y)));temp[i2 * 4 + 2] = (Vec2)(Nuklear.Vec2z((float)((points[i2]).x - (dm_in).x), (float)((points[i2]).y - (dm_in).y)));temp[i2 * 4 + 3] = (Vec2)(Nuklear.Vec2z((float)((points[i2]).x - (dm_out).x), (float)((points[i2]).y - (dm_out).y)));ids[0] = ((ushort)(idx2 + 1));ids[1] = ((ushort)(idx1 + 1));ids[2] = ((ushort)(idx1 + 2));ids[3] = ((ushort)(idx1 + 2));ids[4] = ((ushort)(idx2 + 2));ids[5] = ((ushort)(idx2 + 1));ids[6] = ((ushort)(idx2 + 1));ids[7] = ((ushort)(idx1 + 1));ids[8] = ((ushort)(idx1 + 0));ids[9] = ((ushort)(idx1 + 0));ids[10] = ((ushort)(idx2 + 0));ids[11] = ((ushort)(idx2 + 1));ids[12] = ((ushort)(idx2 + 2));ids[13] = ((ushort)(idx1 + 2));ids[14] = ((ushort)(idx1 + 3));ids[15] = ((ushort)(idx1 + 3));ids[16] = ((ushort)(idx2 + 3));ids[17] = ((ushort)(idx2 + 2));ids += 18;idx1 = (ulong)(idx2);}for (i = (ulong)(0); (i) < (points_count); ++i) {
Vec2 uv = (Vec2)(this.config._null_.uv);vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(temp[i * 4 + 0]), (Vec2)(uv), (Colorf)(col_trans));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(temp[i * 4 + 1]), (Vec2)(uv), (Colorf)(col));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(temp[i * 4 + 2]), (Vec2)(uv), (Colorf)(col));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(temp[i * 4 + 3]), (Vec2)(uv), (Colorf)(col_trans));}}
this.vertices.Reset((int)(Nuklear.NK_BUFFER_FRONT));}
 else {
ulong i1 = (ulong)(0);ulong idx = (ulong)(this.vertex_count);ulong idx_count = (ulong)(count * 6);ulong vtx_count = (ulong)(count * 4);void * vtx = AllocVertices((ulong)(vtx_count));ushort* ids = AllocElements((ulong)(idx_count));if ((vtx== null) || (ids== null)) return;for (i1 = (ulong)(0); (i1) < (count); ++i1) {
float dx;float dy;Vec2 uv = (Vec2)(this.config._null_.uv);ulong i2 = (ulong)(((i1 + 1) == (points_count))?0:i1 + 1);Vec2 p1 = (Vec2)(points[i1]);Vec2 p2 = (Vec2)(points[i2]);Vec2 diff = (Vec2)(Nuklear.Vec2z((float)((p2).x - (p1).x), (float)((p2).y - (p1).y)));float len;len = (float)((diff).x * (diff).x + (diff).y * (diff).y);if (len != 0.0f) len = (float)(Nuklear.InvSqrt((float)(len))); else len = (float)(1.0f);diff = (Vec2)(Nuklear.Vec2z((float)((diff).x * (len)), (float)((diff).y * (len))));dx = (float)(diff.x * (thickness * 0.5f));dy = (float)(diff.y * (thickness * 0.5f));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(Nuklear.Vec2z((float)(p1.x + dy), (float)(p1.y - dx))), (Vec2)(uv), (Colorf)(col));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(Nuklear.Vec2z((float)(p2.x + dy), (float)(p2.y - dx))), (Vec2)(uv), (Colorf)(col));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(Nuklear.Vec2z((float)(p2.x - dy), (float)(p2.y + dx))), (Vec2)(uv), (Colorf)(col));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(Nuklear.Vec2z((float)(p1.x - dy), (float)(p1.y + dx))), (Vec2)(uv), (Colorf)(col));ids[0] = ((ushort)(idx + 0));ids[1] = ((ushort)(idx + 1));ids[2] = ((ushort)(idx + 2));ids[3] = ((ushort)(idx + 0));ids[4] = ((ushort)(idx + 2));ids[5] = ((ushort)(idx + 3));ids += 6;idx += (ulong)(4);}}

		}

		public void FillPolyConvex(Vec2* points, uint points_count, Color color, int aliasing)
		{
			Colorf col =  new Colorf();
			Colorf col_trans =  new Colorf();
			ulong pnt_align = (ulong)(4);
			ulong pnt_size = (ulong)(sizeof(Vec2));
			if (((points_count) < (3))) return;
			PushUserdata((Handle)(this.userdata));
			color.a = ((byte)((float)(color.a) * this.config.global_alpha));
			Nuklear.ColorFv(&col.r, (Color)(color));
			col_trans = (Colorf)(col);
			col_trans.a = (float)(0);
			if ((aliasing) == (Nuklear.NK_ANTI_ALIASING_ON)) {
ulong i = (ulong)(0);ulong i0 = (ulong)(0);ulong i1 = (ulong)(0);float AA_SIZE = (float)(1.0f);ulong vertex_offset = (ulong)(0);ulong index = (ulong)(this.vertex_count);ulong idx_count = (ulong)((points_count - 2) * 3 + points_count * 6);ulong vtx_count = (ulong)(points_count * 2);void * vtx = AllocVertices((ulong)(vtx_count));ushort* ids = AllocElements((ulong)(idx_count));ulong size = (ulong)(0);Vec2* normals = null;uint vtx_inner_idx = (uint)(index + 0);uint vtx_outer_idx = (uint)(index + 1);if ((vtx== null) || (ids== null)) return;vertex_offset = ((ulong)((byte*)(vtx) - (byte*)(this.vertices.memory.ptr)));this.vertices.Mark((int)(Nuklear.NK_BUFFER_FRONT));size = (ulong)(pnt_size * points_count);normals = (Vec2*)(this.vertices.Alloc((int)(Nuklear.NK_BUFFER_FRONT), (ulong)(size), (ulong)(pnt_align)));if (normals== null) return;vtx = (void *)((byte*)(this.vertices.memory.ptr) + vertex_offset);for (i = (ulong)(2); (i) < (points_count); i++) {
ids[0] = ((ushort)(vtx_inner_idx));ids[1] = ((ushort)(vtx_inner_idx + ((i - 1) << 1)));ids[2] = ((ushort)(vtx_inner_idx + (i << 1)));ids += 3;}for (i0 = (ulong)(points_count - 1) , i1 = (ulong)(0); (i1) < (points_count); i0 = (ulong)(i1++)) {
Vec2 p0 = (Vec2)(points[i0]);Vec2 p1 = (Vec2)(points[i1]);Vec2 diff = (Vec2)(Nuklear.Vec2z((float)((p1).x - (p0).x), (float)((p1).y - (p0).y)));float len = (float)((diff).x * (diff).x + (diff).y * (diff).y);if (len != 0.0f) len = (float)(Nuklear.InvSqrt((float)(len))); else len = (float)(1.0f);diff = (Vec2)(Nuklear.Vec2z((float)((diff).x * (len)), (float)((diff).y * (len))));normals[i0].x = (float)(diff.y);normals[i0].y = (float)(-diff.x);}for (i0 = (ulong)(points_count - 1) , i1 = (ulong)(0); (i1) < (points_count); i0 = (ulong)(i1++)) {
Vec2 uv = (Vec2)(this.config._null_.uv);Vec2 n0 = (Vec2)(normals[i0]);Vec2 n1 = (Vec2)(normals[i1]);Vec2 dm = (Vec2)(Nuklear.Vec2z((float)((Nuklear.Vec2z((float)((n0).x + (n1).x), (float)((n0).y + (n1).y))).x * (0.5f)), (float)((Nuklear.Vec2z((float)((n0).x + (n1).x), (float)((n0).y + (n1).y))).y * (0.5f))));float dmr2 = (float)(dm.x * dm.x + dm.y * dm.y);if ((dmr2) > (0.000001f)) {
float scale = (float)(1.0f / dmr2);scale = (float)((scale) < (100.0f)?(scale):(100.0f));dm = (Vec2)(Nuklear.Vec2z((float)((dm).x * (scale)), (float)((dm).y * (scale))));}
dm = (Vec2)(Nuklear.Vec2z((float)((dm).x * (AA_SIZE * 0.5f)), (float)((dm).y * (AA_SIZE * 0.5f))));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(Nuklear.Vec2z((float)((points[i1]).x - (dm).x), (float)((points[i1]).y - (dm).y))), (Vec2)(uv), (Colorf)(col));vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(Nuklear.Vec2z((float)((points[i1]).x + (dm).x), (float)((points[i1]).y + (dm).y))), (Vec2)(uv), (Colorf)(col_trans));ids[0] = ((ushort)(vtx_inner_idx + (i1 << 1)));ids[1] = ((ushort)(vtx_inner_idx + (i0 << 1)));ids[2] = ((ushort)(vtx_outer_idx + (i0 << 1)));ids[3] = ((ushort)(vtx_outer_idx + (i0 << 1)));ids[4] = ((ushort)(vtx_outer_idx + (i1 << 1)));ids[5] = ((ushort)(vtx_inner_idx + (i1 << 1)));ids += 6;}this.vertices.Reset((int)(Nuklear.NK_BUFFER_FRONT));}
 else {
ulong i = (ulong)(0);ulong index = (ulong)(this.vertex_count);ulong idx_count = (ulong)((points_count - 2) * 3);ulong vtx_count = (ulong)(points_count);void * vtx = AllocVertices((ulong)(vtx_count));ushort* ids = AllocElements((ulong)(idx_count));if ((vtx== null) || (ids== null)) return;for (i = (ulong)(0); (i) < (vtx_count); ++i) {vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(points[i]), (Vec2)(this.config._null_.uv), (Colorf)(col));}for (i = (ulong)(2); (i) < (points_count); ++i) {
ids[0] = ((ushort)(index));ids[1] = ((ushort)(index + i - 1));ids[2] = ((ushort)(index + i));ids += 3;}}

		}

		public void PathClear()
		{
			if (this== null) return;
			this.buffer.Reset((int)(Nuklear.NK_BUFFER_FRONT));
			this.path_count = (uint)(0);
			this.path_offset = (uint)(0);
		}

		public void PathLineTo(Vec2 pos)
		{
			Vec2* points = null;
			DrawCommand* cmd = null;
			if (this== null) return;
			if (this.cmd_count== 0) AddClip((Rect)(Nuklear.nk_null_rect));
			cmd = CommandLast();
			if (((cmd) != null) && (cmd->texture.ptr != this.config._null_.texture.ptr)) PushImage((Handle)(this.config._null_.texture));
			points = AllocPath((int)(1));
			if (points== null) return;
			points[0] = (Vec2)(pos);
		}

		public void PathArcToFast(Vec2 center, float radius, int a_min, int a_max)
		{
			int a = (int)(0);
			if (this== null) return;
			if (a_min <= a_max) {
for (a = (int)(a_min); a <= a_max; a++) {
Vec2 c = (Vec2)(this.circle_vtx[(ulong)(a) % (ulong)this.circle_vtx.Length]);float x = (float)(center.x + c.x * radius);float y = (float)(center.y + c.y * radius);PathLineTo((Vec2)(Nuklear.Vec2z((float)(x), (float)(y))));}}

		}

		public void PathArcTo(Vec2 center, float radius, float a_min, float a_max, uint segments)
		{
			uint i = (uint)(0);
			if (this== null) return;
			if ((radius) == (0.0f)) return;
			{
float d_angle = (float)((a_max - a_min) / (float)(segments));float sin_d = (float)(Nuklear.Sin((float)(d_angle)));float cos_d = (float)(Nuklear.Cos((float)(d_angle)));float cx = (float)(Nuklear.Cos((float)(a_min)) * radius);float cy = (float)(Nuklear.Sin((float)(a_min)) * radius);for (i = (uint)(0); i <= segments; ++i) {
float new_cx;float new_cy;float x = (float)(center.x + cx);float y = (float)(center.y + cy);PathLineTo((Vec2)(Nuklear.Vec2z((float)(x), (float)(y))));new_cx = (float)(cx * cos_d - cy * sin_d);new_cy = (float)(cy * cos_d + cx * sin_d);cx = (float)(new_cx);cy = (float)(new_cy);}}

		}

		public void PathRectTo(Vec2 a, Vec2 b, float rounding)
		{
			float r;
			if (this== null) return;
			r = (float)(rounding);
			r = (float)((r) < (((b.x - a.x) < (0))?-(b.x - a.x):(b.x - a.x))?(r):(((b.x - a.x) < (0))?-(b.x - a.x):(b.x - a.x)));
			r = (float)((r) < (((b.y - a.y) < (0))?-(b.y - a.y):(b.y - a.y))?(r):(((b.y - a.y) < (0))?-(b.y - a.y):(b.y - a.y)));
			if ((r) == (0.0f)) {
PathLineTo((Vec2)(a));PathLineTo((Vec2)(Nuklear.Vec2z((float)(b.x), (float)(a.y))));PathLineTo((Vec2)(b));PathLineTo((Vec2)(Nuklear.Vec2z((float)(a.x), (float)(b.y))));}
 else {
PathArcToFast((Vec2)(Nuklear.Vec2z((float)(a.x + r), (float)(a.y + r))), (float)(r), (int)(6), (int)(9));PathArcToFast((Vec2)(Nuklear.Vec2z((float)(b.x - r), (float)(a.y + r))), (float)(r), (int)(9), (int)(12));PathArcToFast((Vec2)(Nuklear.Vec2z((float)(b.x - r), (float)(b.y - r))), (float)(r), (int)(0), (int)(3));PathArcToFast((Vec2)(Nuklear.Vec2z((float)(a.x + r), (float)(b.y - r))), (float)(r), (int)(3), (int)(6));}

		}

		public void PathCurveTo(Vec2 p2, Vec2 p3, Vec2 p4, uint num_segments)
		{
			float t_step;
			uint i_step;
			Vec2 p1 =  new Vec2();
			if ((this.path_count== 0)) return;
			num_segments = (uint)((num_segments) < (1)?(1):(num_segments));
			p1 = (Vec2)(PathLast());
			t_step = (float)(1.0f / (float)(num_segments));
			for (i_step = (uint)(1); i_step <= num_segments; ++i_step) {
float t = (float)(t_step * (float)(i_step));float u = (float)(1.0f - t);float w1 = (float)(u * u * u);float w2 = (float)(3 * u * u * t);float w3 = (float)(3 * u * t * t);float w4 = (float)(t * t * t);float x = (float)(w1 * p1.x + w2 * p2.x + w3 * p3.x + w4 * p4.x);float y = (float)(w1 * p1.y + w2 * p2.y + w3 * p3.y + w4 * p4.y);PathLineTo((Vec2)(Nuklear.Vec2z((float)(x), (float)(y))));}
		}

		public void PathFill(Color color)
		{
			Vec2* points;
			if (this== null) return;
			points = (Vec2*)(this.buffer.Memory());
			FillPolyConvex(points, (uint)(this.path_count), (Color)(color), (int)(this.config.shape_AA));
			PathClear();
		}

		public void PathStroke(Color color, int closed, float thickness)
		{
			Vec2* points;
			if (this== null) return;
			points = (Vec2*)(this.buffer.Memory());
			StrokePolyLine(points, (uint)(this.path_count), (Color)(color), (int)(closed), (float)(thickness), (int)(this.config.line_AA));
			PathClear();
		}

		public void StrokeLine(Vec2 a, Vec2 b, Color col, float thickness)
		{
			if ((col.a== 0)) return;
			if ((this.line_AA) == (Nuklear.NK_ANTI_ALIASING_ON)) {
PathLineTo((Vec2)(a));PathLineTo((Vec2)(b));}
 else {
PathLineTo((Vec2)(Nuklear.Vec2z((float)((a).x - (Nuklear.Vec2z((float)(0.5f), (float)(0.5f))).x), (float)((a).y - (Nuklear.Vec2z((float)(0.5f), (float)(0.5f))).y))));PathLineTo((Vec2)(Nuklear.Vec2z((float)((b).x - (Nuklear.Vec2z((float)(0.5f), (float)(0.5f))).x), (float)((b).y - (Nuklear.Vec2z((float)(0.5f), (float)(0.5f))).y))));}

			PathStroke((Color)(col), (int)(Nuklear.NK_STROKE_OPEN), (float)(thickness));
		}

		public void FillRect(Rect rect, Color col, float rounding)
		{
			if ((col.a== 0)) return;
			if ((this.line_AA) == (Nuklear.NK_ANTI_ALIASING_ON)) {
PathRectTo((Vec2)(Nuklear.Vec2z((float)(rect.x), (float)(rect.y))), (Vec2)(Nuklear.Vec2z((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (float)(rounding));}
 else {
PathRectTo((Vec2)(Nuklear.Vec2z((float)(rect.x - 0.5f), (float)(rect.y - 0.5f))), (Vec2)(Nuklear.Vec2z((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (float)(rounding));}

			PathFill((Color)(col));
		}

		public void StrokeRect(Rect rect, Color col, float rounding, float thickness)
		{
			if ((col.a== 0)) return;
			if ((this.line_AA) == (Nuklear.NK_ANTI_ALIASING_ON)) {
PathRectTo((Vec2)(Nuklear.Vec2z((float)(rect.x), (float)(rect.y))), (Vec2)(Nuklear.Vec2z((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (float)(rounding));}
 else {
PathRectTo((Vec2)(Nuklear.Vec2z((float)(rect.x - 0.5f), (float)(rect.y - 0.5f))), (Vec2)(Nuklear.Vec2z((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (float)(rounding));}

			PathStroke((Color)(col), (int)(Nuklear.NK_STROKE_CLOSED), (float)(thickness));
		}

		public void FillRectMultiColor(Rect rect, Color left, Color top, Color right, Color bottom)
		{
			void * vtx;
			Colorf col_left =  new Colorf();Colorf col_top =  new Colorf();
			Colorf col_right =  new Colorf();Colorf col_bottom =  new Colorf();
			ushort* idx;
			ushort index;
			Nuklear.ColorFv(&col_left.r, (Color)(left));
			Nuklear.ColorFv(&col_right.r, (Color)(right));
			Nuklear.ColorFv(&col_top.r, (Color)(top));
			Nuklear.ColorFv(&col_bottom.r, (Color)(bottom));
			if (this== null) return;
			PushImage((Handle)(this.config._null_.texture));
			index = ((ushort)(this.vertex_count));
			vtx = AllocVertices((ulong)(4));
			idx = AllocElements((ulong)(6));
			if ((vtx== null) || (idx== null)) return;
			idx[0] = ((ushort)(index + 0));
			idx[1] = ((ushort)(index + 1));
			idx[2] = ((ushort)(index + 2));
			idx[3] = ((ushort)(index + 0));
			idx[4] = ((ushort)(index + 2));
			idx[5] = ((ushort)(index + 3));
			vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(Nuklear.Vec2z((float)(rect.x), (float)(rect.y))), (Vec2)(this.config._null_.uv), (Colorf)(col_left));
			vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(Nuklear.Vec2z((float)(rect.x + rect.w), (float)(rect.y))), (Vec2)(this.config._null_.uv), (Colorf)(col_top));
			vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(Nuklear.Vec2z((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (Vec2)(this.config._null_.uv), (Colorf)(col_right));
			vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(Nuklear.Vec2z((float)(rect.x), (float)(rect.y + rect.h))), (Vec2)(this.config._null_.uv), (Colorf)(col_bottom));
		}

		public void FillTriangle(Vec2 a, Vec2 b, Vec2 c, Color col)
		{
			if ((col.a== 0)) return;
			PathLineTo((Vec2)(a));
			PathLineTo((Vec2)(b));
			PathLineTo((Vec2)(c));
			PathFill((Color)(col));
		}

		public void StrokeTriangle(Vec2 a, Vec2 b, Vec2 c, Color col, float thickness)
		{
			if ((col.a== 0)) return;
			PathLineTo((Vec2)(a));
			PathLineTo((Vec2)(b));
			PathLineTo((Vec2)(c));
			PathStroke((Color)(col), (int)(Nuklear.NK_STROKE_CLOSED), (float)(thickness));
		}

		public void FillCircle(Vec2 center, float radius, Color col, uint segs)
		{
			float a_max;
			if ((col.a== 0)) return;
			a_max = (float)(3.141592654f * 2.0f * ((float)(segs) - 1.0f) / (float)(segs));
			PathArcTo((Vec2)(center), (float)(radius), (float)(0.0f), (float)(a_max), (uint)(segs));
			PathFill((Color)(col));
		}

		public void StrokeCircle(Vec2 center, float radius, Color col, uint segs, float thickness)
		{
			float a_max;
			if ((col.a== 0)) return;
			a_max = (float)(3.141592654f * 2.0f * ((float)(segs) - 1.0f) / (float)(segs));
			PathArcTo((Vec2)(center), (float)(radius), (float)(0.0f), (float)(a_max), (uint)(segs));
			PathStroke((Color)(col), (int)(Nuklear.NK_STROKE_CLOSED), (float)(thickness));
		}

		public void StrokeCurve(Vec2 p0, Vec2 cp0, Vec2 cp1, Vec2 p1, Color col, uint segments, float thickness)
		{
			if ((col.a== 0)) return;
			PathLineTo((Vec2)(p0));
			PathCurveTo((Vec2)(cp0), (Vec2)(cp1), (Vec2)(p1), (uint)(segments));
			PathStroke((Color)(col), (int)(Nuklear.NK_STROKE_OPEN), (float)(thickness));
		}

		public void PushRectUv(Vec2 a, Vec2 c, Vec2 uva, Vec2 uvc, Color color)
		{
			void * vtx;
			Vec2 uvb =  new Vec2();
			Vec2 uvd =  new Vec2();
			Vec2 b =  new Vec2();
			Vec2 d =  new Vec2();
			Colorf col =  new Colorf();
			ushort* idx;
			ushort index;
			if (this== null) return;
			Nuklear.ColorFv(&col.r, (Color)(color));
			uvb = (Vec2)(Nuklear.Vec2z((float)(uvc.x), (float)(uva.y)));
			uvd = (Vec2)(Nuklear.Vec2z((float)(uva.x), (float)(uvc.y)));
			b = (Vec2)(Nuklear.Vec2z((float)(c.x), (float)(a.y)));
			d = (Vec2)(Nuklear.Vec2z((float)(a.x), (float)(c.y)));
			index = ((ushort)(this.vertex_count));
			vtx = AllocVertices((ulong)(4));
			idx = AllocElements((ulong)(6));
			if ((vtx== null) || (idx== null)) return;
			idx[0] = ((ushort)(index + 0));
			idx[1] = ((ushort)(index + 1));
			idx[2] = ((ushort)(index + 2));
			idx[3] = ((ushort)(index + 0));
			idx[4] = ((ushort)(index + 2));
			idx[5] = ((ushort)(index + 3));
			vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(a), (Vec2)(uva), (Colorf)(col));
			vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(b), (Vec2)(uvb), (Colorf)(col));
			vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(c), (Vec2)(uvc), (Colorf)(col));
			vtx = Nuklear.DrawVertex(vtx, this.config, (Vec2)(d), (Vec2)(uvd), (Colorf)(col));
		}

		public void AddImage(Image texture, Rect rect, Color color)
		{
			if (this== null) return;
			PushImage((Handle)(texture.handle));
			if ((texture.IsSubimage()) != 0) {
Vec2* uv = stackalloc Vec2[2];uv[0].x = (float)((float)(texture.region[0]) / (float)(texture.w));uv[0].y = (float)((float)(texture.region[1]) / (float)(texture.h));uv[1].x = (float)((float)(texture.region[0] + texture.region[2]) / (float)(texture.w));uv[1].y = (float)((float)(texture.region[1] + texture.region[3]) / (float)(texture.h));PushRectUv((Vec2)(Nuklear.Vec2z((float)(rect.x), (float)(rect.y))), (Vec2)(Nuklear.Vec2z((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (Vec2)(uv[0]), (Vec2)(uv[1]), (Color)(color));}
 else PushRectUv((Vec2)(Nuklear.Vec2z((float)(rect.x), (float)(rect.y))), (Vec2)(Nuklear.Vec2z((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (Vec2)(Nuklear.Vec2z((float)(0.0f), (float)(0.0f))), (Vec2)(Nuklear.Vec2z((float)(1.0f), (float)(1.0f))), (Color)(color));
		}

		public void AddText(UserFont font, Rect rect, char* text, int len, float font_height, Color fg)
		{
			float x = (float)(0);
			int text_len = (int)(0);
			char unicode = (char)0;
			char next = (char)(0);
			int glyph_len = (int)(0);
			int next_glyph_len = (int)(0);
			UserFontGlyph g =  new UserFontGlyph();
			if (((len== 0)) || (text== null)) return;
			if (!(!(((((this.clip_rect.x) > (rect.x + rect.w)) || ((this.clip_rect.x + this.clip_rect.w) < (rect.x))) || ((this.clip_rect.y) > (rect.y + rect.h))) || ((this.clip_rect.y + this.clip_rect.h) < (rect.y))))) return;
			PushImage((Handle)(font.texture));
			x = (float)(rect.x);
			glyph_len = (int)(Nuklear.UtfDecode(text, &unicode, (int)(len)));
			if (glyph_len== 0) return;
			fg.a = ((byte)((float)(fg.a) * this.config.global_alpha));
			while (((text_len) < (len)) && ((glyph_len) != 0)) {
float gx;float gy;float gh;float gw;float char_width = (float)(0);if ((unicode) == (0xFFFD)) break;next_glyph_len = (int)(Nuklear.UtfDecode(text + text_len + glyph_len, &next, (int)(len - text_len)));font.query((Handle)(font.userdata), (float)(font_height), &g, unicode,(next == 0xFFFD)?'\0':next);gx = (float)(x + g.offset.x);gy = (float)(rect.y + g.offset.y);gw = (float)(g.width);gh = (float)(g.height);char_width = (float)(g.xadvance);PushRectUv((Vec2)(Nuklear.Vec2z((float)(gx), (float)(gy))), (Vec2)(Nuklear.Vec2z((float)(gx + gw), (float)(gy + gh))), Nuklear.Vec2z(g.uv_x[0], g.uv_y[0]), Nuklear.Vec2z(g.uv_x[1], g.uv_y[1]), (Color)(fg));text_len += (int)(glyph_len);x += (float)(char_width);glyph_len = (int)(next_glyph_len);unicode = (char)(next);}
		}

	}
}