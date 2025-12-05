#include <iostream>
#include <vector>
#include <cmath>
#include <cstdlib>
#include <ctime>
#include <algorithm>
#include <iomanip>
#include <limits>

using namespace std;

class AntColony {
private:
    int N; 
    vector<vector<double>> distance;  
    vector<vector<double>> pheromone;  
    vector<vector<double>> visibility;  

    double alpha; 
    double beta;  
    double initialPheromone;
    int iterations;

    vector<int> bestTour;
    double bestTourLength;

    void initialize() {
        distance.resize(N, vector<double>(N, 0.0));
        pheromone.resize(N, vector<double>(N, initialPheromone));
        visibility.resize(N, vector<double>(N, 0.0));

        srand(time(NULL));
        for (int i = 0; i < N; i++) {
            for (int j = i + 1; j < N; j++) {
                double dist = 1.0 + rand() % 100;
                distance[i][j] = dist;
                distance[j][i] = dist;
                visibility[i][j] = 1.0 / dist;
                visibility[j][i] = 1.0 / dist;
            }
        }
    }

    int selectNextCity(int currentCity, const vector<bool>& visited, const vector<double>& probabilities) {
        double random = (double)rand() / RAND_MAX;
        double cumulative = 0.0;

        for (int i = 0; i < N; i++) {
            if (!visited[i]) {
                cumulative += probabilities[i];
                if (random <= cumulative) {
                    return i;
                }
            }
        }

        for (int i = 0; i < N; i++) {
            if (!visited[i]) return i;
        }
        return -1;
    }

    vector<double> calculateProbabilities(int currentCity, const vector<bool>& visited) {
        vector<double> probabilities(N, 0.0);
        double sum = 0.0;

        for (int i = 0; i < N; i++) {
            if (!visited[i]) {
                double pheromoneLevel = pow(pheromone[currentCity][i], alpha);
                double visibilityLevel = pow(visibility[currentCity][i], beta);
                probabilities[i] = pheromoneLevel * visibilityLevel;
                sum += probabilities[i];
            }
        }

        if (sum > 0) {
            for (int i = 0; i < N; i++) {
                if (!visited[i]) {
                    probabilities[i] /= sum;
                }
            }
        }

        return probabilities;
    }

    void updatePheromones(const vector<vector<int>>& tours, const vector<double>& tourLengths) {
        double evaporation = 0.5;
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                pheromone[i][j] *= (1.0 - evaporation);
            }
        }

        for (size_t ant = 0; ant < tours.size(); ant++) {
            double pheromoneToAdd = 1.0 / tourLengths[ant];
            for (size_t i = 0; i < tours[ant].size() - 1; i++) {
                int from = tours[ant][i];
                int to = tours[ant][i + 1];
                pheromone[from][to] += pheromoneToAdd;
                pheromone[to][from] += pheromoneToAdd;
            }
        }
    }

    double calculateTourLength(const vector<int>& tour) {
        double length = 0.0;
        for (size_t i = 0; i < tour.size() - 1; i++) {
            length += distance[tour[i]][tour[i + 1]];
        }
        length += distance[tour.back()][tour[0]];
        return length;
    }

public:
    AntColony(int n, double initPheromone, double a, double b, int iter)
        : N(n), initialPheromone(initPheromone), alpha(a), beta(b), iterations(iter) {
        bestTourLength = numeric_limits<double>::max();
        initialize();
    }

    void run() {
        int numAnts = N;  

        for (int iter = 0; iter < iterations; iter++) {
            vector<vector<int>> antTours(numAnts);
            vector<double> antTourLengths(numAnts, 0.0);

            for (int ant = 0; ant < numAnts; ant++) {
                vector<bool> visited(N, false);
                vector<int> tour;

                int startCity = ant % N;
                int currentCity = startCity;
                tour.push_back(currentCity);
                visited[currentCity] = true;

                for (int step = 0; step < N - 1; step++) {
                    vector<double> probabilities = calculateProbabilities(currentCity, visited);
                    int nextCity = selectNextCity(currentCity, visited, probabilities);

                    tour.push_back(nextCity);
                    visited[nextCity] = true;
                    currentCity = nextCity;
                }

                antTours[ant] = tour;
                antTourLengths[ant] = calculateTourLength(tour);

                if (antTourLengths[ant] < bestTourLength) {
                    bestTourLength = antTourLengths[ant];
                    bestTour = tour;
                }
            }

            updatePheromones(antTours, antTourLengths);

            displayIterationInfo(iter + 1);
        }

        displayFinalResult();
    }

    void displayIterationInfo(int iteration) {
        cout << "\n--- Итерация " << iteration << " ---" << endl;
        cout << "Лучший маршрут: ";
        for (int city : bestTour) {
            cout << city << " ";
        }
        cout << bestTour[0];
        cout << "\nДлина маршрута: " << fixed << setprecision(2) << bestTourLength << endl;
    }

    void displayFinalResult() {
        cout << "Лучший найденный маршрут: ";
        for (int city : bestTour) {
            cout << city << " ";
        }
        cout << bestTour[0];
        cout << "\nДлина лучшего маршрута: " << fixed << setprecision(2) << bestTourLength << endl;
    }

    void displayDistanceMatrix() {
        cout << "\nМатрица расстояний:" << endl;
        for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++) {
                cout << setw(6) << fixed << setprecision(1) << distance[i][j];
            }
            cout << endl;
        }
    }
};

int main() {
    setlocale(LC_ALL, "rus");
    srand(time(NULL));

    int N;
    double initialPheromone, alpha, beta;
    int iterations;

    cout << "Муравьиный алгоритм для задачи коммивояжера" << endl;

    cout << "Введите количество городов N: ";
    cin >> N;

    cout << "Введите начальное значение феромонов: ";
    cin >> initialPheromone;

    cout << "Введите коэффициент alpha (влияние феромонов): ";
    cin >> alpha;

    cout << "Введите коэффициент beta (влияние расстояния): ";
    cin >> beta;

    cout << "Введите количество итераций: ";
    cin >> iterations;

    AntColony colony(N, initialPheromone, alpha, beta, iterations);

    colony.displayDistanceMatrix();

    colony.run();

    return 0;
}