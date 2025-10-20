#include <iostream>

using namespace std;
void hanoi(int i, int k, int n) {
	if (n == 1) {
		cout << "Move disk 1 from pin " << i << " to pin " << k << endl;
	}
	else {
		int tmp = 6 - i - k;
		hanoi(i, tmp, n - 1);
		cout << "Move disk " << n << "from pin " << i << "to pin " << k << endl;
		hanoi(tmp, k, n - 1);
	}
}

int main() {
	int n, from, to;
	cout << "Enter number of disk: ";
	cin >> n;
	if (n <= 0) {
		cout << "Number of disk must be positive " << endl;
		return 1;
	}
	cout << "Enter from pin: ";
	cin >> from;
	cout << "Enter to pin: ";
	cin >> to;
	if (from > 3 || from < 1 || to > 3 || to < 1) {
		cout << "pins must be between 1 to 3" << endl;
		return 1;
	}
	if (from == to) {
		cout << "The value of the pins cannot be the same" << endl;
		return 1;
	}
	cout << "----- RESULT -----" << "\n";
	hanoi(from, to, n);
	cout << "----- RESULT -----" << "\n";
	return 0;
}