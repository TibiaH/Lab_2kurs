#include <iostream>
#include <limits>
#include <vector>
using namespace std;
const int INF = numeric_limits<int>::max();
const int MAX_VERTICES = 9;

class Graph {
public:
    Graph(int n) : n(n) {
        graph.resize(n, vector<int>(n, INF));
        for (int i = 0; i < n; i++) {
            graph[i][i] = 0; 
        }
    }

    void addEdge(int from, int to, int weight) {
        graph[from][to] = weight; 
        graph[to][from] = weight; 
    }

    void Dijkstra(int start) {
        vector<int> distances(n, INF);
        vector<bool> visited(n, false);

        distances[start] = 0;

        for (int i = 0; i < n - 1; i++) {
            int u = -1;

            for (int j = 0; j < n; j++) {
                if (!visited[j] && (u == -1 || distances[j] < distances[u])) {
                    u = j; 
                }
            }

            visited[u] = true;

            for (int v = 0; v < n; v++) {
                if (graph[u][v] != INF && distances[u] + graph[u][v] < distances[v]) {
                    distances[v] = distances[u] + graph[u][v]; 
                }
            }
        }

        cout << "Расстояния от вершины " << (char)('A' + start) << ":" << endl;
        for (int i = 0; i < n; i++) {
            if (distances[i] == INF) {
                cout << "Вершина " << (char)('A' + i) << ": недоступна" << endl;
            }
            else {
                cout << (char)('A' + i) << ": " << distances[i] << endl;
            }
        }
    }

private:
    int n;
    vector<vector<int>> graph; 
};

int main() {
    Graph graph(MAX_VERTICES);
    setlocale(LC_ALL, "ru");
    graph.addEdge(0, 1, 7);
    graph.addEdge(0, 2, 10);
    graph.addEdge(1, 5, 9);
    graph.addEdge(1, 6, 27);
    graph.addEdge(5, 7, 11);
    graph.addEdge(5, 2, 8);
    graph.addEdge(7, 3, 17);
    graph.addEdge(7, 8, 15);
    graph.addEdge(3, 8, 21);
    graph.addEdge(4, 3, 32);
    graph.addEdge(4, 2, 31);
    graph.addEdge(6, 8, 15);

    int startVertex;
    cout << "Введите стартовую вершину (0-8): ";
    cin >> startVertex;

    if (startVertex < 0 || startVertex >= MAX_VERTICES) {
        cout << "Некорректный номер вершины. Пожалуйста, введите число от 0 до 8." << endl;
        return 1;
    }

    graph.Dijkstra(startVertex);

    return 0;
}