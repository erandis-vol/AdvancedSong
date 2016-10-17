import asong.assembler;
import asong.rom;
import std.stdio;

int findSongTable(ROM rom)
{
	// bluh
	ubyte[] code = [
		0x00, 0xB5, 0x00, 0x04, 0x07, 0x4A, 0x08, 0x49,
		0x40, 0x0B, 0x40, 0x18, 0x83, 0x88, 0x59, 0x00,
		0xC9, 0x18, 0x89, 0x00, 0x89, 0x18, 0x0A, 0x68,
		0x01, 0x68, 0x10, 0x1C, 0x00, 0xF0
	];

	// find select song function
	int selectSongOffset = rom.findLoose(code, 8);
	if (selectSongOffset == -1)
		return -1;

	// read pointer to song table
	rom.seek(selectSongOffset + 0x28);
	return rom.peekPointer();
}

int getSongTableLength(ROM rom)
{
	int count = 0;

	// basically, while there is a valid pointer
	// we have a valid entry, so continue
	while (rom.peekPointer() >= 0 && rom.position < rom.length - 8) {
		rom.skip(8);
		count++;
	}

	return count;
}

void main()
{
	writeln("Opening \"bpre.gba\".");
	auto rom = new ROM("bpre.gba");

	int songTable = rom.findSongTable();
	if (songTable == -1) {
		writeln("Song table not found!");
		return;
	}

	writefln("Song table at: 0x%07X", songTable);

	rom.seek(songTable);
	writeln("Song table length: ", rom.getSongTableLength(), " songs");

	// assemble po pi po
	auto a = new Assembler(rom);
	if (a.assemble("Po Pi Po.s", 0x900000, 0x800000, 0x48ABB0))
		writeln("Assembled successfully!");
	else
		writeln("Assembling aborted.");

	// adjust song table
	rom.seek(songTable + 0x12C * 8);
	rom.writePointer(0x800000);

	// save ROM
	rom.save();
}
