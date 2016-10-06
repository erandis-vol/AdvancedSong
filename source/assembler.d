module asong.assembler;

import asong.stack;
import std.ascii;
import std.conv;
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
		definitions["voicegroup000"] = 0;

		// TODO: literal?/?
		operators['('] = 0;
		operators[')'] = 0;
		operators['+'] = 1;
		operators['-'] = 1;
		operators['*'] = 2;
		operators['/'] = 2;
		operators['%'] = 2;
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
	string[int] pointers;

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
					{
						string f = parts[1];

						// trim the "" characters
						if (f[0] == '"' && f[$-1] == '"') {
							f = f[1..$-1];
						}

						// include the file
						include(f);
					}
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
						definitions[name] = evaluateExpression(expression);
					}
					break;

				case ".byte":
					assert(parts.length > 1, ".byte expects at least 1 argument!");
					for (int i = 1; i < parts.length; i++) {
						// evaluate token
						uint value = evaluateExpression(splitExpression(parts[i]));

						// convert to byte range
						assert(value <= 0xFF, parts[i] ~ " is too large for a byte!");

						// TODO: write byte
						pc++;
					}
					break;

				case ".word":
					assert(parts.length > 1, ".word expects at least 1 argument!");
					for (int i = 1; i < parts.length; i++) {
						// NOTE: .word will always denote a pointer to a label
						string label = parts[i];
						pointers[pc] = label;

						// TODO: write temp pointer
						pc += 4;
					}
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

		// fix pointers
		foreach (offset; pointers.byKey()) {
			// assures that all pointers have been defined
			assert(pointers[offset] in labels ||
				pointers[offset] in definitions,
				"Label " ~ pointers[offset] ~ " was pointed to but not defined!");

			// TODO: seek and fix
		}
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

	int[char] operators;

	// split and convert an expression into reverse polish notation
	string[] splitExpression(string e)
	{
		string[] result;
		Stack!char ops = new Stack!char();

		int i = 0;
		while (i < e.length) {
			// ignore whitespace
			if (e[i].isWhite()) {
				while (i < e.length && e[i].isWhite())
					i++;
			}
			// gather number/definition
			else if (e[i].isAlphaNum() || e[i] == '_') {
				int s = i;
				while (i < e.length && (e[i].isAlphaNum() || e[i] == '_')) {
					i++;
				}
				//result ~= e[s..$];
				//writeln("n: ", e[s..i]);
				result ~= e[s..i];
			}
			// left parenthesis
			else if (e[i] == '(') {
				ops.push('(');
				i++;
			}
			// right parenthesis
			else if (e[i] == ')') {
				while (!ops.isEmpty && ops.peek() != '(')
					result ~= to!string(ops.pop());

				ops.pop();
				i++;
			}
			// operators
			else if (e[i] in operators) {
				char op = e[i++];
				while (!ops.isEmpty && operators[ops.peek()] >= operators[op])
					result ~= to!string(ops.pop());

				ops.push(op);
			}
			// invalid characters break the expression
			else {
				assert(false, "Unrecognized character " ~ e[i] ~ "!");
			}
		}

		while (!ops.isEmpty)
			result ~= to!string(ops.pop());

		return result;
	}

	// evaluate an expression
	uint evaluateExpression(string[] expr)
	{
		//writeln("expr: ", expr);
		Stack!uint stack = new Stack!uint();

		foreach (token; expr) {
			if (token[0] in operators) {
				if (stack.size == 1) {
					int a = stack.pop();

					switch (token[0]) {
						case '-':
							stack.push(-a);
							break;

						default:
							assert(false, "Invalid operator!");
					}
				}
				else {
					int a = stack.pop();
					int b = stack.pop();

					switch (token[0]) {
						case '+':
							stack.push(a + b);
							break;
						case '-':
							stack.push(b - a);
							break;
						case '*':
							stack.push(a * b);
							break;
						case '/':
							stack.push(b / a);
							break;
						case '%':
							stack.push(b % a);
							break;

						default:
							assert(false, "Invalid operator!");
					}
				}
			} else {
				stack.push(evaluateValue(token));
			}
		}

		if (stack.isEmpty)
			return 0;

		return stack.pop();
	}

	uint evaluateValue(string value)
	{
		// TODO: error catching

		// definition
		if (value in definitions)
			return definitions[value];

		// raw value
		if (value.startsWith("0x"))
			return to!uint(value[2..$], 16);

		return to!uint(value, 10);
	}
}
