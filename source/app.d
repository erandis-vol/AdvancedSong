import asong.assembler;
import asong.rom;
import std.stdio;

void main()
{
	writeln("Doing things to Po Pi Po.s...");
	auto rom = new ROM("bpre.gba");
	auto a = new Assembler(rom);
	a.assemble("Po Pi Po.s", 0x900000, 0x800000, 0x123456);
}
