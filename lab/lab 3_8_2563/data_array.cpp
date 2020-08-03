#include <iostream>
#include <fstream>
using namespace std;

int del(int value_g[],int n,int x){
    for(int i=0; i<n; i++){
        if(value_g[i] == x){
            n--;
            for(int j=i; j<n; j++)
            value_g[j] = value_g[j+1];
        }
    }
    return n;
}

int insert(int value_g[],int n,int k,int x){
     for (int i = n; i >= k; i--){
        value_g[i] = value_g[i - 1];
    }  
    value_g[k] = x;
    return n+1;
}

void insertion_sort(int value_g[],int n){
    for(int i=1; i<n; i++){
            int min = value_g[i];
            int j = i-1;
        while (j >= 0 && min < value_g[j]){  
            value_g[j + 1] = value_g[j];  
            j--;  
        }  
        value_g[j+1] = min;         
    }
    cout << "\nInsertionSort: ";
    for(int i; i< n; i++) cout << value_g[i] << " ";  
}


void selection_sort(int value_g[],int n){
    for(int i=0; i < n; i++){
         int min = value_g[i];
         int index = 0;
        for(int j=i; j < n; j++){
            if(value_g[j] < min){
                min = value_g[j];
                index = j;
            }
        }
        if(index == 0) continue;
        value_g[index] = value_g[i];
        value_g[i] = min;
    }
    cout << "\nSelectionSort: ";
    for(int i = 0; i < n; i++) cout << value_g[i] << " ";
}

void bubble_sort(int value_g[],int n){
    int max;
    for(int i=0; i<n; i++){
        for(int j=0; j<=n-i-1; j++){
            if(value_g[j] > value_g[j+1]){
                max = value_g[j];
                value_g[j] = value_g[j+1];
                value_g[j+1] = max;
            }
        }
    }
    cout << "\nBubbleSort: ";
    for(int i = 0; i < n; i++) cout << value_g[i] << " ";
}

int main(){
    ifstream myfile;
    string line;
    int value[35];
    int count = 0;
    int a;
    int n = sizeof(value)/sizeof(value[0]);
    myfile.open("data.txt");
    while(myfile >> a){
        value[count] = a;
        count++;
    }
    count = 0;
    int value_g[20];
	for(int i=9; i<=20; i++){
		value_g[count] = value[i];
		count++;
	}
    n = count;
    while(true){
        string choice;
        cout << "\n******************************\nValue: ";
        for(int i=0 ; i<n; i++) cout << value_g[i] << " ";
        cout<<"\nChoice:\n0. Exit\n1. Delete Element Array\n2. Insert Element Array\n3. insertion_sort\n4. selection_sort\n5. bubble_sort\n\nPlease Choose: ";
        cin >> choice;
        if(choice=="1"){
            int num;
            cout << "Delete Value: ";
            cin >> num;
            n = del(value_g,n,num);
            cout << endl;
            for(int i=0 ; i<n; i++) cout<<value_g[i]<<" ";
        }
        else if(choice=="2"){
            int num,index;
            cout << "index: ";
            cin >> index;
            cout << "Value: ";
            cin >> num;
            n = insert(value_g,n,index,num);
            cout << endl;
            for(int i=0 ; i<n; i++) cout<<value_g[i]<<" ";
        }
        else if(choice=="3")
            insertion_sort(value_g,n);
        else if(choice=="4")
            selection_sort(value_g,n);
        else if(choice=="5")
            bubble_sort(value_g,n);
        else if(choice=="0") 
            break;
    }
    
    return 0;
}