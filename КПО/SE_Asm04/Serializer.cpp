#include <fstream>

int main() {
	int signedValue = -12345;
	unsigned unsignedValue = 4294967295u;

	std::ofstream file("data.bin", std::ios::binary);

	file.write((char*)&signedValue, sizeof(int));
	file.write((char*)&unsignedValue, sizeof(unsigned));

	file.close();

	std::ofstream meta("plan.txt");
	meta << "int signedValue: " << signedValue << "\n";
	meta << "unsigned unsignedValue: " << unsignedValue;
	meta.close();

	return 0;
}