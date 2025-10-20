#include <WinSock2.h>
#include <stdlib.h>
#include <string>
#include <cstring>
#include <iostream>

#pragma comment(lib, "ws2_32.lib")
using namespace std;

void count(const string& str, int counts[]) {
	string windows = "WINDOWS";
	for (int i = 0; i < 7; i++) {
		counts[i] = 0;
	}
	for (char c : str) {
		char upper = toupper(c);
		for (int i = 0; i < 7; i++) {
			if (upper == windows[i]) {
				counts[i]++;
				break;
			}
		}
	}
}

string results(const int counts[]) {
	string windows = "WINDOWS";
	string result = "Result of counting: \n";

	for (int i = 0; i < 7; i++) {
		result += windows[i]; result += ": "; result += std::to_string(counts[i]); result += "\n";
	}
	return result;
}

void main(void) {
	WORD wVersioRequested;
	WSADATA wsaData;
	int err;
	wVersioRequested = MAKEWORD(2, 2);
	err = WSAStartup(wVersioRequested, &wsaData);

	SOCKET s;
	s = socket(AF_INET, SOCK_DGRAM, 0);
	struct sockaddr_in ad;
	ad.sin_port = htons(1024);
	ad.sin_family = AF_INET;
	ad.sin_addr.s_addr = 0;
	bind(s, (struct sockaddr*)&ad, sizeof(ad));

	char b[200], tmp = '\0';
	int l;
	l = sizeof(ad);

	int rv = recvfrom(s, b, sizeof(b), 0, (struct sockaddr*) & ad, &l);

	int counts[7];
	count(b, counts);
	string result = results(counts);

	sendto(s, result.c_str(), result.length(), 0, (struct sockaddr*)&ad, l);
	
	closesocket(s);
	WSACleanup();
}