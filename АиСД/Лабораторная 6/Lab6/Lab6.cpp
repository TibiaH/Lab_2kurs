#include <iostream>
#include <string>
#include <queue>
#include <vector>

using namespace std;

const int ASCII_SIZE = 256;

struct HuffmanNode {
    char character;
    int frequency;
    HuffmanNode* left;
    HuffmanNode* right;
    HuffmanNode(char ch, int freq) : character(ch), frequency(freq), left(nullptr), right(nullptr) {}
    HuffmanNode(int freq, HuffmanNode* l, HuffmanNode* r) : character('\0'), frequency(freq), left(l), right(r) {}
};

struct CompareNodes {
    bool operator()(HuffmanNode* a, HuffmanNode* b) { return a->frequency > b->frequency; }
};

unsigned char safeCharToIndex(char ch) {
    return static_cast<unsigned char>(ch);
}

void buildFrequencyTable(const string& text, vector<int>& freqTable) {
    freqTable.assign(ASCII_SIZE, 0);
    for (char ch : text) {
        freqTable[safeCharToIndex(ch)]++;
    }
}

vector<pair<char, int>> getUsedCharacters(const vector<int>& freqTable) {
    vector<pair<char, int>> usedChars;
    for (int i = 0; i < ASCII_SIZE; i++) {
        if (freqTable[i] > 0) {
            usedChars.push_back({ static_cast<char>(i), freqTable[i] });
        }
    }
    return usedChars;
}

void buildCodeTableRecursive(HuffmanNode* node, const string& code, vector<string>& codeTable) {
    if (!node) return;
    if (!node->left && !node->right) {
        unsigned char index = safeCharToIndex(node->character);
        codeTable[index] = code;
        return;
    }
    buildCodeTableRecursive(node->left, code + "0", codeTable);
    buildCodeTableRecursive(node->right, code + "1", codeTable);
}

HuffmanNode* buildHuffmanTree(const vector<pair<char, int>>& usedChars) {
    priority_queue<HuffmanNode*, vector<HuffmanNode*>, CompareNodes> pq;
    for (auto& pair : usedChars) {
        pq.push(new HuffmanNode(pair.first, pair.second));
    }
    while (pq.size() > 1) {
        HuffmanNode* left = pq.top(); pq.pop();
        HuffmanNode* right = pq.top(); pq.pop();

        HuffmanNode* parent = new HuffmanNode(left->frequency + right->frequency, left, right);
        pq.push(parent);
    }
    return pq.empty() ? nullptr : pq.top();
}

string encodeText(const string& text, const vector<string>& codeTable) {
    string encodedText;
    for (char ch : text) {
        unsigned char index = safeCharToIndex(ch);
        encodedText += codeTable[index];
    }
    return encodedText;
}

void deleteTree(HuffmanNode* node) {
    if (!node) return;
    deleteTree(node->left);
    deleteTree(node->right);
    delete node;
}

int main() {
    setlocale(LC_ALL, "rus");
    string text;
    cout << "Введите текст: ";
    getline(cin, text);
    vector<int> frequencyTable;
    buildFrequencyTable(text, frequencyTable);
    cout << "\nТаблица встречаемости символов:\n";
    for (int i = 0; i < ASCII_SIZE; i++) {
        if (frequencyTable[i] > 0) {
            char ch = static_cast<char>(i);
            if (ch == ' ') cout << "[пробел]";
            else if (ch == '\n') cout << "[\\n]";
            else if (ch == '\t') cout << "[\\t]";
            else cout << ch;
            cout << ": " << frequencyTable[i] << endl;
        }
    }
    vector<pair<char, int>> usedChars = getUsedCharacters(frequencyTable);
    HuffmanNode* root = buildHuffmanTree(usedChars);
    vector<string> codeTable(ASCII_SIZE);
    buildCodeTableRecursive(root, "", codeTable);
    cout << "\nТаблица соответствия символа и кодовой последовательности:\n";
    for (int i = 0; i < ASCII_SIZE; i++) {
        if (!codeTable[i].empty()) {
            char ch = static_cast<char>(i);
            if (ch == ' ') cout << "[пробел]";
            else if (ch == '\n') cout << "[\\n]";
            else if (ch == '\t') cout << "[\\t]";
            else cout << ch;
            cout << ": " << codeTable[i] << endl;
        }
    }
    string encodedText = encodeText(text, codeTable);
    cout << "\nВыходная последовательность:\n" << encodedText << endl;
    deleteTree(root);
    return 0;
}