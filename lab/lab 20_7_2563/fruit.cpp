#include <iostream>
#include <fstream>
using namespace std;

int main(){
    ifstream myfile;
    string line;
    string value_file[10];
    string choice[5];
    int index = 0;

    myfile.open("d:/fruit.txt");
    while(getline(myfile, line)){
        value_file[index] = line;
        choice[index] = line;
        index++;
    }
    myfile.close();

    string temp_int[5];
    string temp_str[5];
    for (int i = 0; i < 5; i++) { 
        char p[value_file[i].length()];
        string str = value_file[i];
        for(int j=0; j<str.length(); j++){
             p[j] = str[j];
            if(p[j] >= '0' && p[j] <= '9')
                temp_int[i] += p[j];
            else
                temp_str[i] += p[j];
        } 
    } 

    int i = 0;
    while(i < sizeof(value_file)/sizeof(value_file[0])){
        if(i%2 == 0) value_file[i] = temp_str[i];
        else value_file[i] = temp_int[i];
        i++;
    }

    int weight,choose,total;
    string name;
    cout << "<< what do you want (choose = quit)>>" << endl;
    for(int i=0;i<sizeof(choice)/sizeof(choice[0]); i++)
        cout << i+1 << ". " << choice[i] << endl;
    int no=1;
    while(true){
        cout << "choose fruit: ";
        cin >> choose;
        if(choose == 0) break;
        cout << "weights(kg): ";
        cin >> weight;
        total += weight*stoi(temp_int[choose-1]);
        name += to_string(no)+ ". " + temp_str[choose-1] + "\n";
        no++;
    }
    
    cout << "\n***list fruit***\n" << name << "***Price: " << total << " Baht***" << endl;

    return 0;   
}