#include <iostream>
#include <WinSock2.h>
#include <ws2tcpip.h>

#pragma comment(lib, "ws2_32.lib") 
using namespace std;

int main() {
    setlocale(LC_ALL, "rus");
    WSADATA wsaData;
    SOCKET clientSocket;
    sockaddr_in serverAddr;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        cout << "Ошибка инициализации Winsock" << endl;
        return 1;
    }
    clientSocket = socket(AF_INET, SOCK_STREAM, 0);
    if (clientSocket == INVALID_SOCKET) {
        cout << "Ошибка создания сокета" << endl;
        WSACleanup();
        return 1;
    }
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(1280);

    if (inet_pton(AF_INET, "127.0.0.1", &serverAddr.sin_addr) <= 0) {
        cout << "Ошибка преобразования IP адреса" << endl;
        closesocket(clientSocket);
        WSACleanup();
        return 1;
    }

    if (connect(clientSocket, (sockaddr*)&serverAddr, sizeof(serverAddr)) == SOCKET_ERROR) {
        cout << "Ошибка подключения к серверу" << endl;
        closesocket(clientSocket);
        WSACleanup();
        return 1;
    }
    cout << "Подключение к серверу установлено" << endl;  
    char buffer[1024];
    int choice;

    while (true) {
        cout << "1 - Студенты без троек" << endl;
        cout << "2 - Добавить студента" << endl;
        cout << "3 - Редактировать студента" << endl;
        cout << "4 - Удалить студента" << endl;
        cout << "5 - Выход" << endl;
        cin >> choice;
        cin.ignore();
        send(clientSocket, (char*)&choice, sizeof(int), 0);
        if (choice == 5) break;

        int bytesReceived = recv(clientSocket, buffer, sizeof(buffer) - 1, 0);
        if (bytesReceived > 0) {
            buffer[bytesReceived] = '\0';
            cout << endl << buffer;

            if (choice == 2 || choice == 3 || choice == 4) {
                char input[256];

                if (choice == 2) {
                    cout << "Введите данные в формате: ФИО|Группа|Стипендия|Оценки" << endl;
                    cout << "Ввод: ";
                }
                else if (choice == 3) {
                    cout << "Введите данные в формате: номер|ФИО|Группа|Стипендия|Оценки" << endl;
                    cout << "Ввод: ";
                }
                else if (choice == 4) {
                    cout << "Введите номер студента: ";
                }
                cin.getline(input, sizeof(input));
                send(clientSocket, input, (int)strlen(input), 0);
                bytesReceived = recv(clientSocket, buffer, sizeof(buffer) - 1, 0);
                if (bytesReceived > 0) {
                    buffer[bytesReceived] = '\0';
                    cout << endl << buffer;
                }
            }
        }
    }
    closesocket(clientSocket);
    WSACleanup();
    return 0;
}