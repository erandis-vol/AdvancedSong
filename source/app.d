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
	int songTablePtr = rom.readInt32();

	// if it was actually a pointer, return offset
	if (songTablePtr >= 0x8000000 && songTablePtr <= 0x9FFFFFF)
		return songTablePtr & 0x1FFFFFF;

	// otherwise failure
	return -1;
}


void main()
{
	writeln("Opening \"bpre.gba\".");
	auto rom = new ROM("bpre.gba");
	int songTable = rom.findSongTable();
	if (songTable == -1)
		writeln("Song table not found!");
	else
		writefln("Song table at: 0x%07X", songTable);

	//writeln("Doing things to Po Pi Po.s...");
	//auto a = new Assembler(rom);
	//a.assemble("Po Pi Po.s", 0x900000, 0x800000, 0x48ABB0);
}
