module asong.assembler;

import asong.stack;
import std.ascii;
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
		/*for (int i = 0; i < lines.length; i++) {
			if (lines[i] == null) {
				lines[i] = "@";
			}
		}*/
	}

	string nextLine()
	{
		if (current >= lines.length)
			return null;

		return lines[current++];
	}

	@property bool endOfFile() const
	{
		return current >= lines.length;
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
	Stack!Source sources = new Stack!Source();

	int[string] labels;
	uint[string] definitions;

	int pc = 0;

	void include(string filename)
	{
		// TODO: search for file to include

		// add a new source to the top of the stack
		sources.push(new Source(filename));
	}

	void parse()
	{
		// single pass assembler
		while (!sources.isEmpty()) {
			// get current source
			Source source = sources.peek();

			// pop source if EOF
			if (source.endOfFile) {
				sources.pop();
				continue;
			}

			// get next line from the source
			string line = source.nextLine();

			// clean the line of unneeded characters
			stripz(line);

			if (line == null)
				continue;

			// tokenize the line
			// mid2agb produces very clean output so this is quite simple
			string[] parts = splitLine(line);
			if (parts.length == 0)
				continue;

			// parse line
			parse_line:
			if (parts[0][$-1] == ':') {
				// first grab a label
				labels[parts[0][0..$-1]] = pc;
				writeln("label: ", parts[0][0..$-1], ": ", labels[parts[0][0..$-1]]);

				// if finished, move on
				if (parts.length == 1)
					continue;

				// otherwise parse again
				parts = parts[1..$];
				goto parse_line;
			}

			// grab a command (directive)
			// anything else is invalid
			switch (parts[0]) {
				case ".include":
					assert(parts.length == 2, ".include expects at 1 argument!");
					writeln("include file: ", parts[1]);
					break;

				case ".equ":
					assert(parts.length == 3, ".equ expects 2 arguments!");
					{
						// parse name
						string name = parts[1];
						assert(name !in definitions, name ~ " has already been defined!");

						// parse argument
						string[] expression = splitExpression(parts[2]);

						// evaluate
						definitions[name] = 42;
					}
					break;

				case ".byte":
					assert(parts.length > 1, ".byte expects at least 1 argument!");
					break;

				case ".word":
					assert(parts.length > 1, ".word expects at least 1 argument!");
					break;

				case ".end":
					writeln("wow kill the parsing");
					goto kill_parse;

				case ".align":
					// TODO: start a new section
					//		 in a valid song there will always be two of these
					break;

				case ".global":
				case ".section":
					// NOTE: ignore these directives, they're not needed
					break;

				default:
					assert(false, "Invalid command " ~ parts[0] ~ "!");
			}
		}

		kill_parse:

		// TODO: fix labels
	}

	void stripz(ref string s)
	{
		// strip a comment
		auto index = s.indexOf('@');
		if (index >= 0) {
			s = s[0..index];
		}

		// strip whitespace
		s = strip(s);
	}

	// split a string the way it should be
	// label: command arg1, arg2, ..., argN
	string[] splitLine(string line)
	{
		string[] result;
		if (line.length == 0)
			return result;

		// grabs the front of the string
		grab_front:
		int i = 0;

		while (i < line.length) {
			if (line[i].isWhite()) {
				break;
			}

			i++;
		}
		result ~= line[0..i];
		line = strip(line[i..$]);

		if (line == null) {
			return result;
		}

		// if the line had a label, it could also have a command
		if (result[$-1].endsWith(":")) {
			goto grab_front;
		}

		// collect and clean arguments
		foreach (argument; split(line, ",")) {
			result ~= strip(argument);
		}

		return result;
	}

	// split and convert an expression into reverse polish notation
	string[] splitExpression(string e)
	{
		string[] result;
		return result;
	}


}
