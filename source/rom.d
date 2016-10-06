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
		assert(offset > 0 && offset < buffer.length, "Cannot seek beyond file.!");
		p = offset;
	}

	ubyte readUByte()
	{
		return buffer[p++];
	}

	int readInt32()
	{
		int i;
		for (int j = 0; j < 4; j++)
			i |= readUByte() << (j * 8);

		return i;
	}

	int readPointer()
	{
		int ptr = readInt32();

		if (ptr == 0)
			return 0;

		return ptr & 0x1FFFFFF;
	}

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

	@property int position() const
	{
		return p;
	}
}
