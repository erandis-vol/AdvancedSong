module asong.rom;

import std.file;
import std.stdio;

class ROM
{
private:
	string filename;
	ubyte[] buffer;
	int p;

public:
	this(string filename)
	{
		auto f = File(filename);
		foreach (ubyte[] chunk; f.byChunk(4096))
			buffer ~= chunk;
		f.close();

		this.filename = filename;
	}

	void save()
	{
		auto f = File(filename, "wb");
		f.rawWrite(buffer);
		f.close();
	}

	void seek(int offset)
	{
		p = offset;
		assert(p > 0 && p < buffer.length, "Cannot seek beyond file.!");
	}

	void skip(int bytes)
	{
		p += bytes;
		assert(p > 0 && p < buffer.length, "Cannot seek beyond file.!");
	}

	ubyte readUByte()
	{
		return buffer[p++];
	}

	ubyte peekUByte()
	{
		return buffer[p];
	}

	int readInt32()
	{
		int i;
		for (int j = 0; j < 4; j++) {
			i |= buffer[p++] << (j * 8);
		}
		return i;
	}

	int peekInt32()
	{
		int i;
		for (int j = 0; j < 4; j++) {
			i |= buffer[p + j] << (j * 8);
		}
		return i;
	}

	ubyte[] readUBytes(int count)
	{
		ubyte[] r = new ubyte[count];
		for (int i = 0; i < count; i++)
			r[i] = buffer[p++];
		return r;
	}

	int readPointer()
	{
		int ptr = readInt32();

		if (ptr == 0)
			return 0;

		return ptr & 0x1FFFFFF;
	}

	/*int peekPointer()
	{
		int ptr = peekInt32();

		if (ptr == 0)
			return 0;

		if (ptr >= 0x8000000 && ptr <= 0x9FFFFFF)
			return ptr & 0x1FFFFFF;

		return -1;
	}*/

	void writeUByte(ubyte value)
	{
		buffer[p++] = value;
	}

	void writeInt32(int value)
	{
		buffer[p++] = cast(ubyte)value;
		buffer[p++] = cast(ubyte)(value >> 8);
		buffer[p++] = cast(ubyte)(value >> 16);
		buffer[p++] = cast(ubyte)(value >> 24);
	}

	void writePointer(int offset)
	{
		if (offset)
			writeInt32(offset | 0x8000000);
		else
			writeInt32(0);
	}

	int findLoose(ubyte[] search, int maxDiff)
	{
		int p = 0;

		int compareLoose()
		{
			if (maxDiff <= 0)
				return 0;

			int d = 0;
			for (int i = 0; i < search.length; i++) {
				if (buffer[i + p] != search[i]) {
					if (++d >= maxDiff)
						return d;
				}
			}
			return d;
		}

		for (; p < buffer.length - search.length; p += 4) {
			if (compareLoose() < 8)
				return p;
		}

		return -1;
	}

	@property int position() const
	{
		return p;
	}
}
