#include <iostream>
#include<WS2tcpip.h>
#include <WinSock2.h>

#pragma comment(lib,"ws2_32.lib")
using namespace std;

unsigned long long factorial(int n) {
	if (n < 0)
		return 0;
	if (n == 0 || n == 1)
		return 1;

	unsigned long long result = 1;
	for (int i = 2; i <= n; ++i) {
		result *= i;
	}
	return result;
}

int main() {
	setlocale(LC_ALL, "rus");
	WORD wVersionRequested;
	WSADATA wsaData;

	wVersionRequested = MAKEWORD(2, 2);
	if (WSAStartup(wVersionRequested, &wsaData) != 0) {
		cerr << "Ошибка инициализации." << endl;
		return 1;
	}

	struct sockaddr_in local;
	local.sin_family = AF_INET;
	local.sin_port = htons(1280);
	local.sin_addr.s_addr = INADDR_ANY;

	//создание сокета 
	SOCKET s = socket(AF_INET, SOCK_STREAM, 0);
	if (s == INVALID_SOCKET) {
		cerr << "Ошибка создания сокета" << endl;
		WSACleanup();
		return 1;
	}

	if (bind(s, (struct sockaddr*)&local, sizeof(local)) == SOCKET_ERROR) {
		cerr << "Ошибка привязки сокета." << endl;
		closesocket(s);
		WSACleanup();
		return 1;
	}

	listen(s, 5);
	cout << "Сервер запущен. Ожидание соединения..." << endl;

	while (true) {
		struct sockaddr_in remote;
		int size = sizeof(remote);
		//отркытие соединения
		SOCKET s2 = accept(s, (struct sockaddr*)&remote, &size);

		if (s2 == INVALID_SOCKET) {
			cerr << "Ошибка подключения: " << WSAGetLastError() << endl;
			continue;
		}

		int num[2];
		int bytesREceived = recv(s2, (char*)num, sizeof(num), 0);

		if (bytesREceived == sizeof(num)) {
			int m = num[0];
			int n = num[1];

			cout << "Получены числа m = " << m << " и число n = " << n << endl;
			unsigned long long result = factorial(m) + factorial(n);
			cout << "Результат вычислений = " << result << endl;

			//Пересылка данных через сокет потока 
			send(s2, (char*)&result, sizeof(result), 0);
		}
		else {
			cerr << "ошибка получения данных." << endl;
		}
		closesocket(s2);
	}
	closesocket(s);
	WSACleanup();
	return 0;
}