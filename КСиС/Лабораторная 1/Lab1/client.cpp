#include <WinSock2.h>
#include<WS2tcpip.h>

#include <iostream>

#pragma comment(lib, "ws2_32.lib")
using namespace std;

int main() {
	setlocale(LC_ALL, "rus");
	WORD wVersionRequested;
	WSADATA wsaData;

	wVersionRequested = MAKEWORD(2, 2);
	if (WSAStartup(wVersionRequested, &wsaData) != 0) {
		cerr << "Ошибка инициализации Winsock." << endl;
		return 1;
	}

	struct sockaddr_in peer;
	peer.sin_family = AF_INET;
	peer.sin_port = htons(1280);
	inet_pton(AF_INET, "127.0.0.1", &peer.sin_addr);
	SOCKET s = socket(AF_INET, SOCK_STREAM, 0);
	if (s == INVALID_SOCKET) {
		cerr << "Ошибка создания сокета." << endl;
		WSACleanup();
		return 1;
	}

	//запрос на открытие соединения
	if (connect(s, (struct sockaddr*)&peer, sizeof(peer)) == SOCKET_ERROR) {
		cerr << "Ошибка подключения." << endl;
		closesocket(s);
		WSACleanup();
		return 1;
	}

	int m, n;
	cout << "Введите первое число (m): ";
	cin >> m;
	cout << "Введите второе число (n): ";
	cin >> n;

	int num[2] = { m,n };
	send(s, (char*)num, sizeof(num), 0);
	
	unsigned long long result;

	//прием данных через сокет потока
	int byteRecived = recv(s, (char*)&result, sizeof(result), 0);
	if (byteRecived == sizeof(result)) {
		cout << "Результат вычисления m! + n! = " << result << endl;
	}

	closesocket(s);
	WSACleanup();
	return 0;
}