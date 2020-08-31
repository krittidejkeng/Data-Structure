#include <iostream>
#include <fstream>
using namespace std;

double sum = 0;
double count_val = 0;
struct Node{
    int data;
    Node* next;
};
Node* insertList(Node* n, int data){
    if(n==NULL){
        Node* tmp = new Node();
        tmp -> data = data;
        tmp -> next = NULL;
        return tmp;
    }
    else{
        n->next = insertList(n->next,data);
        return n;
    }
    return n;
}
void printList(Node* n){
    if(n->next == NULL){
        cout << n->data << endl;
    }
    else{
        cout<< n->data << "->";
        printList(n->next);
    }
}
double finesum(Node* n){
    if(n->next != NULL){
        sum += n->data;
        finesum(n->next);
    }
    else{
        sum += n->data;
        return sum;
    }
    return sum;
}

double count(Node* n){
    if(n->next != NULL){
        count_val++;
        count(n->next);
    }
    else
        return count_val;
    return count_val;
}
Node* deleteDataInList(Node* n, int data){
    Node *newNode = new Node();
    if(n == NULL){
        return NULL;
    }
    else if(n->data == data){
        n->next = deleteDataInList(n->next,data);
        return n->next;
    }
    else{
        n->next = deleteDataInList(n->next,data);
        return n;
    }
    return n;
}

int main(){
    ifstream file;
	file.open("data.txt");
    string value_str[4];
    string line;
    int str_i = 0;
    while(getline(file,line)){
        value_str[str_i] = line;
        str_i++;
    }
    file.close();
    file.open("data.txt");
	int value_file[35];
	int index = 0;
	int x=0;
	while(file >> x){
		value_file[index] = x;
		index++;
	}
	index = 0;
	file.close();
	int value_group[12];
	float total = 0;
	for(int i=9; i<=20; i++){
		total += value_file[i];
		value_group[index] = value_file[i];
		index++;
	}

    Node* node = new Node();
    int n = sizeof(value_group)/sizeof(value_group[0]);
    for(int i=0; i<n; i++)
        node = insertList(node, value_group[i]);
    cout<<"value team: ";
    printList(node);
    deleteDataInList(node,29);
    cout<<"value update: ";
    printList(node);
    cout<<"value update: ";
    cout << "sum: "<<finesum(node) << endl;
    cout << "count: " <<count(node) << endl;
    cout << "average: "<<finesum(node)/count(node) << endl;
    return 0;
}