#include <iostream>
#include <fstream>

using namespace std;
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
    cout << "Value file text: ";
    for(string str:value_str) cout << endl << str;
    cout << endl;
	int value_group[12];
	float total = 0;
	for(int i=9; i<=20; i++){
		total += value_file[i];
		value_group[index] = value_file[i];
		index++;
	}
    cout << "Value group: ";
    for(int i:value_group) cout << i << " ";
	cout << "\nAverage is: " << total / 12 << endl;

    return 0;
}