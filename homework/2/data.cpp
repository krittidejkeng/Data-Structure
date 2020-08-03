#include <iostream>
using namespace std;

/**********************Insertion_Sort******************************/
void Insertion_Sort(){
    int value[] = {10,34,24,3,66,47,18,8,26,55,82};
    int n = sizeof(value)/sizeof(value[0]);
    cout << "Origin: ";
    for(int i:value) cout << i << " ";
    for(int i=1; i<n; i++){
            int min = value[i];
            int j = i-1;
        while (j >= 0 && min < value[j]){  
            value[j + 1] = value[j];  
            j--;  
        }  
        value[j+1] = min;         
    }
    cout << "\nInsertionSort: ";
    for(int i; i< n; i++) cout << value[i] << " ";  
}

/**********************Selection_Sort******************************/
void Selection_Sort(){
    int value[] = {10,34,24,3,66,47,18,8,26,55,82};
    int n = sizeof(value)/sizeof(value[0]);
    for(int i=0; i < n; i++){
         int min = value[i];
         int index = 0;
        for(int j=i; j < n; j++){
            if(value[j] < min){
                min = value[j];
                index = j;
            }
        }
        if(index == 0) continue;
        value[index] = value[i];
        value[i] = min;
    }
    cout << "\nSelectionSort: ";
    for(int i = 0; i < n; i++) cout << value[i] << " ";
}

/**********************Bubble_Sort******************************/
void Bubble_Sort(){
    int value[] = {10,34,24,3,66,47,18,8,26,55,82};
    int n = sizeof(value)/sizeof(value[0]);
    int max;
    for(int i=0; i<n; i++){
        for(int j=0; j<=n-i-1; j++){
            if(value[j] > value[j+1]){
                max = value[j];
                value[j] = value[j+1];
                value[j+1] = max;
            }
        }
    }
    cout << "\nBubbleSort: ";
    for(int i = 0; i < n; i++) cout << value[i] << " ";
}

int main(){
    Insertion_Sort();
    Selection_Sort();
    Bubble_Sort();
    return 0;
}

