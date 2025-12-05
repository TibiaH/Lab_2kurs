#include <iostream>
#include <algorithm>
#include <ctime>
#include <vector>
#include <iomanip>
#include <climits>

using namespace std;

vector<vector<int>> graph;
int numCities = 0;

void initTestGraph() {
	numCities = 8;
	graph.resize(numCities, vector<int>(numCities, 0));

    int distance[8][8] = {
        {0, 27, 22, 8, 40, 19, 5, 25},
        {27, 0, 6, 10, 31, 9, 24, 1},
        {22, 6, 0, 7, 3, 2, 15, 30},
        {8, 10, 7, 0, 18, 12, 4, 20},
        {40, 31, 3, 18, 0, 14, 35, 11},
        {19, 9, 2, 12, 14, 0, 28, 13},
        {5, 24, 15, 4, 35, 28, 0, 17},
        {25, 1, 30, 20, 11, 13, 17, 0}
    };

    for (int i = 0; i < numCities; i++) {
        for (int j = 0; j < numCities; j++) {
            graph[i][j] = distance[i][j];
        }
    }
}

int calculateRouteLength(const vector<int>& route) {
    int length = 0;
    for (int i = 0; i < route.size() - 1; i++) {
        length += graph[route[i]][route[i + 1]];
    }

    length += graph[route.back()][route[0]];
    return length;
}

vector<int> generateRandomRoute() {
    vector<int> route;
    for (int i = 0; i < numCities; i++) {
        route.push_back(i);
    }

    for (int i = 0; i < numCities; i++) {
        int j = rand() % numCities;
        swap(route[i], route[j]);
    }
    return route;
}

vector<int> crossover(const vector<int>& parent1, const vector<int>& parent2) {
    int size = parent1.size();
    vector<int> child(size);

    int start = rand() % size;
    int end = start + rand() % (size - start);

    for (int i = start; i <= end; i++) {
        child[i] = parent1[i];
    }

    int childPos = 0;
    for (int i = 0; i < size; i++) {
        if (childPos >= start && childPos <= end) {
            childPos = end + 1;
        }

        if (find(child.begin(), child.end(), parent2[i]) == child.end()) {
            if (childPos >= size) break;
            child[childPos] = parent2[i];
            childPos++;
        }
    }
    return child;
}

void mutate(vector<int>& route) {
    int i = rand() % route.size();
    int j = rand() % route.size();
    while (i == j) {
        j = rand() % route.size();
    }
    swap(route[i], route[j]);
}

void geneticAlgorithm(int populationSize, int childrenPerGeneration, int evolutions) {
    srand(time(0));

    vector<vector<int>> population;
    for (int i = 0; i < populationSize; i++) {
        population.push_back(generateRandomRoute());
    }

    for (int generation = 1; generation <= evolutions; generation++) {
        sort(population.begin(), population.end(),
            [](const vector<int>& a, const vector<int>& b) {
                return calculateRouteLength(a) < calculateRouteLength(b);
            });

        cout << "Популяция - " << generation << endl;
        cout << "Лучший маршрут - ";
        for (int city : population[0]) {
            cout << (city + 1) << " ";
        }
        cout << "-> " << (population[0][0] + 1);
        cout << "Длина маршрута - " << calculateRouteLength(population[0]) << endl;
        cout << "---------------------" << endl;

        if (generation == evolutions) break;

        vector<vector<int>> newPopulation;

        int eliteCount = populationSize / 4;
        for (int i = 0; i < eliteCount; i++) {
            newPopulation.push_back(population[i]);
        }

        while (newPopulation.size() < populationSize) {
            int parent1Idx = rand() % (populationSize / 2);
            int parent2Idx = rand() % (populationSize / 2);
            while (parent1Idx == parent2Idx) {
                parent2Idx = rand() % (populationSize / 2);
            }

            vector<int> child1 = crossover(population[parent1Idx], population[parent2Idx]);
            vector<int> child2 = crossover(population[parent2Idx], population[parent1Idx]);

            if (rand() % 100 < 10) {
                mutate(child1);
            }
            if (rand() % 100 < 10) {
                mutate(child2);
            }

            newPopulation.push_back(child1);
            if (newPopulation.size() < populationSize) {
                newPopulation.push_back(child2);
            }
        }

        population = newPopulation;
    }

    cout << "Лучший найденный маршрут - ";
    for (int city : population[0]) {
        cout << (city + 1) << " ";
    }
    cout << "-> " << (population[0][0] + 1);
    cout << "Длина маршрута - " << calculateRouteLength(population[0]) << endl;
}

int main() {
    setlocale(LC_ALL, "rus");
    initTestGraph();

    cout << "Граф городов (8 городов)" << endl;
    cout << "Матрица расстояний:" << endl;
    for (int i = 0; i < numCities; i++) {
        for (int j = 0; j < numCities; j++) {
            cout << setw(3) << graph[i][j] << " ";
        }
        cout << endl;
    }

    int populationSize, childrenPerGeneration, evolutions;
    cout << "Введите размер начальной популиции - ";
    cin >> populationSize;

    cout << "Введите количество потомоков - ";
    cin >> childrenPerGeneration;

    cout << "Введите количество эволюций - ";
    cin >> evolutions;

    cout << "------------------------" << endl;
    geneticAlgorithm(populationSize, childrenPerGeneration, evolutions);
}