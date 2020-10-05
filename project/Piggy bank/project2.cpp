#include<iostream>
using namespace std;

int M_month[12] = {0,0,0,0,0,0,0,0,0,0,0,0};
struct Node {
	int data;
	Node *left;
	Node *right;
};

Node* findmin(Node* node)
{
	while(node->left != NULL) 
        node = node->left;
	return node;
}

struct Node* del(Node *node, int data) {
	if(node == NULL) return node; 
	else if(data < node->data) 
        node->left = del(node->left,data);
	else if (data > node->data) 
        node->right = del(node->right,data);
	// กณีที่เจอข้อมูลที่จะลบ	
	else {
        // กรณีที่ 2 children
        if(node->left != NULL && node->right != NULL){
            struct Node *temp = findmin(node->right);
			node->data = temp->data;
			node->right = del(node->right,temp->data);
        }
        // กรณีที่มี 1 child
        else if(node->right == NULL) {
			struct Node *temp = node;
			node = node->left;
			delete temp;
		}
        	else if(node->left == NULL) {
			struct Node *temp = node;
			node = node->right;
			delete temp;
		}
        // กรณีที่ไม่มี child
        else{
           delete node;
			node = NULL; 
        }
	}
	return node;
}
 
void Inorder(Node *node) {
	if(node == NULL) 
        return;
	Inorder(node->left);     
	cout << node->data << " ";
	Inorder(node->right);    
}
 
Node* Insert(Node *node,char data) {
	if(node == NULL) {
		node = new Node();
		node->data = data;
		node->left = node->right = NULL;
	}
	else if(data >= node->data)
		node->right = Insert(node->right,data);
	else
        node->left = Insert(node->left,data);
	return node;
}

void printMonth(){
    string month_mini[] = {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul","Aug", "Sep", "Oct", "Nov", "Dec"};
    cout << "---------------------------------" << endl;
    cout << "| 1.jan\t 2.Feb\t 3.Mar\t 4.Apr  |\n| 5.May\t 6.Jun\t 7.Jul\t 8.Auf  |\n| 9.Sep\t 10.Oct\t 11.Nov\t 12.Dec |\n";
    cout << "---------------------------------" << endl;
}
string Ch_Month(int m){
    string month[] = {"January","February","March","April","May","June","July",
    "August","September","October","November","December"};
    return month[m-1];
}
int main(){
    printMonth();
    cout << "Please Choose Month:";
    int m;
    cin >> m;
    Ch_Month(m);
	Node* node = NULL;
    while(true){
        int choice;
        cout << "0.exit\n1.insert\n2.delete\n3.inorder\n4.money of month\n5.change month\n6.Show All month\nPlease Choose: ";
        cin >> choice;
        cout << endl;
        if(choice == 0)
            break;
        if(choice == 1){
            cout << "Please enter data: ";
            int num = 0;
            cin >> num;
            node = Insert(node,num);    
            M_month[m-1] += num;  
        }
        if(choice == 2){
            cout << "Please enter value delete: ";
            int num = 0;
            cin >> num;
            node = del(node,num);
            M_month[m-1] -= num;
        }
        if(choice == 3){
            cout<<"Inorder: ";
	        Inorder(node);
            cout<<endl;
        }
        if(choice == 4){
            cout << "Money of " << Ch_Month(m) << ": " << M_month[m-1] << endl;
        }
        if(choice == 5){
            printMonth();
            cin >> m;
            Ch_Month(m);
        }
        if(choice == 6){
            for(int i=0; i<sizeof(M_month)/sizeof(M_month[0]); i++){
                cout << "Money of " << Ch_Month(i+1) << ": " << M_month[i] << endl;
            }
        }
    }
    return 0;
}