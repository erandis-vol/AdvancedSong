module asong.assembler;

import std.file;
import std.stdio;
import std.string;

class Source
{
public:
	this(string filename)
	{
		// try to find the file
		assert(exists(filename), "Could not find " ~ filename ~ "!");
		path = filename;

		// read contents of file into lines
		lines = splitLines(readText(filename));

		// TODO: this is so shameful
		//		 a null string is an empty string
		for (int i = 0; i < lines.length; i++) {
			if (lines[i] == null) {
				lines[i] = "@";
			}
		}
	}

	string nextLine()
	{
		if (current >= lines.length)
			return null;

		return lines[current++];
	}

//private:
	string path;
	string[] lines;
	int current = 0;
}

class Assembler
{
public:
	this()
	{

	}

	bool assemble(string filename)
	{
		include(filename);
		parse();

		return true;
	}

private:
	void include(string filename)
	{
		Source src = new Source(filename);
		int i = 0;

		writeln("lines:");
		for (;;) {
			string line = src.nextLine();
			i += 1;

			if (line == null) {
				writeln(i, ": EOF");
				break;
			}

			writeln(i, ": ", line);
		}
	}

	void parse()
	{

	}
}
