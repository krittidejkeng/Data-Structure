#include <iostream>

using namespace std;
int main(){
    cout << "Number/multiply by 2      3      4      5      6      7      8      9      10     11     12\n";
    for(int i=2; i<=12; i++){
        cout << i << "\t\t   ";
        for(int j=2; j<=12; j++){
            if(to_string(i*j).size() == 1)
                cout << i*j << "      ";
            else if(to_string(i*j).size() == 2)
                cout << i*j <<"     ";
            else
                cout << i*j << "    ";
        }
        cout << endl;
    }
    return 0;
}