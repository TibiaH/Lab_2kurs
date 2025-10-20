#include <iostream>
#include <WinSock2.h>

#pragma comment(lib, "ws2_32.lib")
using namespace std;
const int MAX_BUFFER_SIZE = 1024;
const int MAX_NAME_SIZE = 100;
const int MAX_GROUP_SIZE = 10;
const int MAX_GRADES_SIZE = 20;
struct Student {
    char name[MAX_NAME_SIZE];
    char group[MAX_GROUP_SIZE];
    double scholarship;
    char grades[MAX_GRADES_SIZE];
};
Student students[10];
int studentsCount = 5;
int numcl = 0;

void init() {
    strcpy_s(students[0].name, MAX_NAME_SIZE, "Homan Dmitry Pavlovich1");
    strcpy_s(students[0].group, MAX_GROUP_SIZE, "ПИ-7");
    students[0].scholarship = 200;
    strcpy_s(students[0].grades, MAX_GRADES_SIZE, "5 4 5 4");

    strcpy_s(students[1].name, MAX_NAME_SIZE, "Homan Dmitry Pavlovich2");
    strcpy_s(students[1].group, MAX_GROUP_SIZE, "ПИ-8");
    students[1].scholarship = 300;
    strcpy_s(students[1].grades, MAX_GRADES_SIZE, "3 4 3 5");

    strcpy_s(students[2].name, MAX_NAME_SIZE, "Homan Dmitry Pavlovich3");
    strcpy_s(students[2].group, MAX_GROUP_SIZE, "ПИ-9");
    students[2].scholarship = 250;
    strcpy_s(students[2].grades, MAX_GRADES_SIZE, "5 5 5 5");

    strcpy_s(students[3].name, MAX_NAME_SIZE, "Homan Dmitry Pavlovich4");
    strcpy_s(students[3].group, MAX_GROUP_SIZE, "ПИ-7");
    students[3].scholarship = 400;
    strcpy_s(students[3].grades, MAX_GRADES_SIZE, "4 3 4 4");

    strcpy_s(students[4].name, MAX_NAME_SIZE, "Homan Dmitry Pavlovich5");
    strcpy_s(students[4].group, MAX_GROUP_SIZE, "ПИ-8");
    students[4].scholarship = 350;
    strcpy_s(students[4].grades, MAX_GRADES_SIZE, "5 4 5 4");
}

void StudentsWithoutThree(char* result) {
    strcpy_s(result, MAX_BUFFER_SIZE, "Студенты без троек:\n");
    bool found = false;
    for (int i = 0; i < studentsCount; i++) {
        bool hasThree = false;
        for (int j = 0; students[i].grades[j] != '\0'; j++) {
            if (students[i].grades[j] == '3') {
                hasThree = true;
                break;
            }
        }

        if (!hasThree) {
            found = true;
            char temp[256];
            sprintf_s(temp, sizeof(temp), "ФИО: %s\nГруппа: %s\nСтипендия: %.2f\nОценки: %s\n---\n",
                students[i].name, students[i].group, students[i].scholarship, students[i].grades);
            strcat_s(result, MAX_BUFFER_SIZE, temp);
        }
    }
    if (!found) {
        strcpy_s(result, MAX_BUFFER_SIZE, "Нет студентов без троек\n");
    }
}

bool addStudent(char* data) {
    if (studentsCount >= 10) {
        return false;
    }
    char* parts[4];
    int partCount = 0;
    char temp[256];
    strcpy_s(temp, sizeof(temp), data);
    char* context = nullptr;
    char* token = strtok_s(temp, "|", &context);
    while (token != NULL && partCount < 4) {
        parts[partCount++] = token;
        token = strtok_s(NULL, "|", &context);
    }

    if (partCount != 4) {
        return false;
    }

    strcpy_s(students[studentsCount].name, MAX_NAME_SIZE, parts[0]);
    strcpy_s(students[studentsCount].group, MAX_GROUP_SIZE, parts[1]);
    students[studentsCount].scholarship = atof(parts[2]);
    strcpy_s(students[studentsCount].grades, MAX_GRADES_SIZE, parts[3]);
    studentsCount++;
    return true;
}

bool editStudent(char* data) {
    char* parts[5];
    int partCount = 0;
    char temp[256];
    strcpy_s(temp, sizeof(temp), data);
    char* context = nullptr;
    char* token = strtok_s(temp, "|", &context);

    while (token != NULL && partCount < 5) {
        parts[partCount++] = token;
        token = strtok_s(NULL, "|", &context);
    }

    if (partCount != 5) {
        return false;
    }

    int studentIndex = atoi(parts[0]) - 1;
    if (studentIndex < 0 || studentIndex >= studentsCount) {
        return false;
    }

    strcpy_s(students[studentIndex].name, MAX_NAME_SIZE, parts[1]);
    strcpy_s(students[studentIndex].group, MAX_GROUP_SIZE, parts[2]);
    students[studentIndex].scholarship = atof(parts[3]);
    strcpy_s(students[studentIndex].grades, MAX_GRADES_SIZE, parts[4]);
    return true;
}

bool deleteStudent(int index) {
    if (index < 0 || index >= studentsCount) {
        return false;
    }
    for (int i = index; i < studentsCount - 1; i++) {
        students[i] = students[i + 1];
    }
    studentsCount--;
    return true;
}

void allStudent(char* result) {
    strcpy_s(result, MAX_BUFFER_SIZE, "Все студенты:\n");
    if (studentsCount == 0) {
        strcat_s(result, MAX_BUFFER_SIZE, "Нет студентов в базе данных\n");
        return;
    }
    for (int i = 0; i < studentsCount; i++) {
        char temp[256];
        sprintf_s(temp, sizeof(temp), "%d. ФИО: %s\n   Группа: %s\n   Стипендия: %.2f\n   Оценки: %s\n---\n",
            i + 1, students[i].name, students[i].group, students[i].scholarship, students[i].grades);
        strcat_s(result, MAX_BUFFER_SIZE, temp);
    }
}

DWORD WINAPI ThreadFunc(LPVOID client_socket) {
    SOCKET s2 = ((SOCKET*)client_socket)[0];
    char buf[1024];
    int bytesReceived;

    while (true) {
        int command;
        bytesReceived = recv(s2, (char*)&command, sizeof(int), 0);
        if (bytesReceived <= 0) {
            cout << "Клиент отключился" << endl;
            break;
        }
        cout << "Получена команда: " << command << endl;
        char response[1024];
        strcpy_s(response, sizeof(response), "");

        if (command == 1) {
            allStudent(response);
            send(s2, response, (int)strlen(response), 0);
        }
        else if (command == 2) {
            StudentsWithoutThree(response);
            send(s2, response, (int)strlen(response), 0);
        }
        else if (command == 3) {
            strcpy_s(response, sizeof(response), "Введите данные: ФИО|Группа|Стипендия|Оценки\nПример: Иванов Иван|ГР-01|1500|5 4 5 4\n");
            send(s2, response, (int)strlen(response), 0);

            bytesReceived = recv(s2, buf, sizeof(buf) - 1, 0);
            if (bytesReceived > 0) {
                buf[bytesReceived] = '\0';
                if (addStudent(buf)) {
                    strcpy_s(response, sizeof(response), "Студент добавлен\n");
                }
                else {
                    strcpy_s(response, sizeof(response), "Ошибка добавления\n");
                }
                send(s2, response, (int)strlen(response), 0);
            }
        }
        else if (command == 4) {
            strcpy_s(response, sizeof(response), "Введите: номер|ФИО|Группа|Стипендия|Оценки\nПример: 1|Иванов Иван|ГР-01|1600|5 5 5 5\n");
            send(s2, response, (int)strlen(response), 0);

            bytesReceived = recv(s2, buf, sizeof(buf) - 1, 0);
            if (bytesReceived > 0) {
                buf[bytesReceived] = '\0';
                if (editStudent(buf)) {
                    strcpy_s(response, sizeof(response), "Студент обновлен\n");
                }
                else {
                    strcpy_s(response, sizeof(response), "Ошибка обновления\n");
                }
                send(s2, response, (int)strlen(response), 0);
            }
        }
        else if (command == 5) {
            strcpy_s(response, sizeof(response), "Введите номер студента: ");
            send(s2, response, (int)strlen(response), 0);

            bytesReceived = recv(s2, buf, sizeof(buf) - 1, 0);
            if (bytesReceived > 0) {
                buf[bytesReceived] = '\0';
                int index = atoi(buf) - 1;
                if (deleteStudent(index)) {
                    strcpy_s(response, sizeof(response), "Студент удален\n");
                }
                else {
                    strcpy_s(response, sizeof(response), "Ошибка удаления\n");
                }
                send(s2, response, (int)strlen(response), 0);
            }
        }
        else if (command == 6) {
            strcpy_s(response, sizeof(response), "Выход из программы\n");
            send(s2, response, (int)strlen(response), 0);
            break;
        }
        else {
            strcat_s(response, sizeof(response), "1 - Все студенты\n");
            strcat_s(response, sizeof(response), "2 - Студенты без троек\n");
            strcat_s(response, sizeof(response), "3 - Добавить студента\n");
            strcat_s(response, sizeof(response), "4 - Редактировать студента\n");
            strcat_s(response, sizeof(response), "5 - Удалить студента\n");
            strcat_s(response, sizeof(response), "6 - Выход\n");
            send(s2, response, (int)strlen(response), 0);
        }
    }
    closesocket(s2);
    numcl--;
    if (numcl)
        cout << numcl << " Клиента подключено" << endl;
    else
        cout << "Нет подключенных клиентов" << endl;
    return 0;
}

int main() {
    setlocale(LC_ALL, "rus");
    WORD wVersionRequested;
    WSADATA wsaData;
    int err;
    wVersionRequested = MAKEWORD(2, 2);
    err = WSAStartup(wVersionRequested, &wsaData);
    if (err != 0) {
        cout << "Ошибка инициализации Winsock" << endl;
        return 1;
    }
    SOCKET s = socket(AF_INET, SOCK_STREAM, 0);
    if (s == INVALID_SOCKET) {
        cout << "Ошибка создания сокета" << endl;
        WSACleanup();
        return 1;
    }
    sockaddr_in local_addr;
    local_addr.sin_family = AF_INET;
    local_addr.sin_port = htons(1280);
    local_addr.sin_addr.s_addr = INADDR_ANY;

    if (bind(s, (sockaddr*)&local_addr, sizeof(local_addr)) == SOCKET_ERROR) {
        cout << "Ошибка привязки сокета" << endl;
        closesocket(s);
        WSACleanup();
        return 1;
    }

    if (listen(s, 5) == SOCKET_ERROR) {
        cout << "Ошибка прослушивания" << endl;
        closesocket(s);
        WSACleanup();
        return 1;
    }
    cout << "Ожидание подключений..." << endl;
    init();
    SOCKET client_socket;
    sockaddr_in client_addr;
    int client_addr_size = sizeof(client_addr);

    while ((client_socket = accept(s, (sockaddr*)&client_addr, &client_addr_size)) != INVALID_SOCKET) {
        numcl++;
        cout << "Новый клиент подключен. Всего клиентов: " << numcl << endl;
        DWORD thID;
        CreateThread(NULL, NULL, ThreadFunc, &client_socket, NULL, &thID);

        if (thID == NULL) {
            cout << "Ошибка создания потока" << endl;
            closesocket(client_socket);
            numcl--;
        }
    }

    closesocket(s);
    WSACleanup();
    return 0;
}