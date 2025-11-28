#include <iostream>
using namespace std;

int main() {
	setlocale(LC_ALL, "rus");
	int N;
	cout << "¬ведите количество элементов: "; cin >> N; cout << endl;
	int* arr = new int[N];
	cout << "¬ведите элементы последовательности: ";
	for (int i = 0; i < N; i++) {
		cin >> arr[i];
	}

	int* dp = new int[N];
	int* prev = new int[N];

	for (int i = 0; i < N; i++) {
		dp[i] = 1;
		prev[i] = -1;
	}

	int max = 1;
	int last = 0;

	for (int i = 1; i < N; i++) {
		for (int j = 0; j < i; j++) {
			if (arr[j] < arr[i] && dp[j] + 1 > dp[i]) {
				dp[i] = dp[j] + 1;
				prev[i] = j;
			}
		}
		if (dp[i] > max) {
			max = dp[i];
			last = i;
		}
	}

	int* sequence = new int[max];
	int pos = last;
	int idx = max - 1;

	while (pos != -1) {
		sequence[idx] = arr[pos];
		pos = prev[pos];
		idx--;
	}

	cout << max << endl;
	for (int i = 0; i < max; i++) {
		cout << sequence[i];
		if (i < max - 1) {
			cout << ", ";
		}
	}
	cout << endl;
	delete[] arr;
	delete[] dp;
	delete[] prev;
	delete[] sequence;
	return 0;
}