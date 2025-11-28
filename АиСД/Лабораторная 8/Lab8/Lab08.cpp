#include <iostream>
using namespace std;

struct Item {
    char name[100];
    int weight;
    int price;
};

int main() {
    setlocale(LC_ALL, "rus");
    int capacity;
    int count;
    cout << "Введите вместимость рюкзака - "; cin >> capacity; cout << endl;
    cout << "Введите количество предметов - "; cin >> count; cout << endl;
    Item* items = new Item[count];
    Item* selectItems = new Item[count];

    for (int i = 0; i < count; i++) {
        cout << "Введите название - "; cin.ignore(); cin.getline(items[i].name, 100);
        cout << " Введите вес - "; cin >> items[i].weight;
        cout << " Введите стоимость - "; cin >> items[i].price;
    }

    int** dp = new int* [count + 1];
    for (int i = 0; i <= count; i++) {
        dp[i] = new int[capacity + 1];
        for (int w = 0; w <= capacity; w++) {
            dp[i][w] = 0;
        }
    }

    for (int i = 1; i <= count; i++) {
        for (int w = 1; w <= capacity; w++) {
            if (items[i - 1].weight <= w) {
                int take = dp[i - 1][w - items[i - 1].weight] + items[i - 1].price;
                int notTake = dp[i - 1][w];
                dp[i][w] = (take > notTake) ? take : notTake;
            }
            else {
                dp[i][w] = dp[i - 1][w];
            }
        }
    }

    int maxValueSaved = dp[count][capacity]; 
    int selectedCount = 0;
    int w = capacity;
    int maxValue = maxValueSaved; 

    for (int i = count; i > 0 && maxValue > 0; i--) {
        if (maxValue != dp[i - 1][w]) {
            selectItems[selectedCount] = items[i - 1];
            selectedCount++;
            maxValue -= items[i - 1].price;
            w -= items[i - 1].weight;
        }
    }

    for (int i = 0; i <= count; i++) {
        delete[] dp[i];
    }
    delete[] dp;

    cout << "Максимальная стоимость - " << maxValueSaved << endl;
    cout << "Предметы в рюкзаке - " << endl;
    int total = 0;
    for (int i = 0; i < selectedCount; i++) {
        cout << " Вес - " << selectItems[i].weight << " Цена - " << selectItems[i].price << endl;
        total += selectItems[i].weight;
    }
    cout << "Общий вес - " << total << endl;

    delete[] items;
    delete[] selectItems;
    return 0;
}