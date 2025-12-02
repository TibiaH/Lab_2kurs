#include <fstream>

int main() {
	std::ifstream input("data.bin", std::ios::binary);

	int signedValue;
	unsigned unsignedValue;

	input.read((char*)&signedValue, sizeof(int));
	input.read((char*)&unsignedValue, sizeof(unsigned));
	input.close();

	std::ofstream asmFile("data.asm");

	asmFile << ".586\n";
	asmFile << ".MODEL flat, stdcall\n\n";
	asmFile << ".DATA\n";
	asmFile << "myInt SDWORD " << signedValue << "\n";
	asmFile << "myUnsigned DWORD " << unsignedValue << "\n";
	asmFile << "\n.CODE\nmain PROC\nmain ENDP\nend main";

	asmFile.close();

	return 0;
}