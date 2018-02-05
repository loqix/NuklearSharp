namespace NuklearSharp
{
	public unsafe partial class TextUndoState
	{
		public void TexteditFlushRedo()
		{
			this.redo_point = (short) (99);
			this.redo_char_point = (short) (999);
		}

		public void TexteditDiscardUndo()
		{
			if ((this.undo_point) > (0))
			{
				if ((this.undo_rec[0].char_storage) >= (0))
				{
					int n = (int) (this.undo_rec[0].insert_length);
					int i;
					this.undo_char_point = ((short) (this.undo_char_point - n));
					Nuklear.Memcopy(this.undo_char, this.undo_char + n, (ulong) ((ulong) (this.undo_char_point)*sizeof (uint)));
					for (i = (int) (0); (i) < (this.undo_point); ++i)
					{
						if ((this.undo_rec[i].char_storage) >= (0))
							this.undo_rec[i].char_storage = ((short) (this.undo_rec[i].char_storage - n));
					}
				}
				--this.undo_point;
				Nuklear.Memcopy(this.undo_rec, this.undo_rec + 1, (ulong) ((ulong) (this.undo_point)*sizeof ((this.undo_rec[0]))))
				;
			}

		}

		public void TexteditDiscardRedo()
		{
			ulong num;
			int k = (int) (99 - 1);
			if (this.redo_point <= k)
			{
				if ((this.undo_rec[k].char_storage) >= (0))
				{
					int n = (int) (this.undo_rec[k].insert_length);
					int i;
					this.redo_char_point = ((short) (this.redo_char_point + n));
					num = ((ulong) (999 - this.redo_char_point));
					Nuklear.Memcopy(this.undo_char + this.redo_char_point, this.undo_char + this.redo_char_point - n,
						(ulong) (num*sizeof (char)));
					for (i = (int) (this.redo_point); (i) < (k); ++i)
					{
						if ((this.undo_rec[i].char_storage) >= (0))
						{
							this.undo_rec[i].char_storage = ((short) (this.undo_rec[i].char_storage + n));
						}
					}
				}
				++this.redo_point;
				num = ((ulong) (99 - this.redo_point));
				if ((num) != 0)
					Nuklear.Memcopy(this.undo_rec + this.redo_point - 1, this.undo_rec + this.redo_point, (ulong) (num*sizeof ((
						this.undo_rec[0]))))
				;
			}

		}

		public TextUndoRecord* TexteditCreateUndoRecord(int numchars)
		{
			TexteditFlushRedo();
			if ((this.undo_point) == (99)) TexteditDiscardUndo();
			if ((numchars) > (999))
			{
				this.undo_point = (short) (0);
				this.undo_char_point = (short) (0);
				return null;
			}

			while ((this.undo_char_point + numchars) > (999))
			{
				TexteditDiscardUndo();
			}
			return &this.undo_rec[this.undo_point++];
		}

		public char* TexteditCreateundo(int pos, int insert_len, int delete_len)
		{
			TextUndoRecord* r = TexteditCreateUndoRecord((int) (insert_len));
			if ((r) == (null)) return null;
			r->where = (int) (pos);
			r->insert_length = ((short) (insert_len));
			r->delete_length = ((short) (delete_len));
			if ((insert_len) == (0))
			{
				r->char_storage = (short) (-1);
				return null;
			}
			else
			{
				r->char_storage = (short) (this.undo_char_point);
				this.undo_char_point = ((short) (this.undo_char_point + insert_len));
				return &this.undo_char[r->char_storage];
			}
		}
	}
}