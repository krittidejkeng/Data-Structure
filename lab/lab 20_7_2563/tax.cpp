#include <iostream>

using namespace std;
double method_tax(double income){
    double tax;
    if(income < 2000){
        tax = income * 0.07;  
    }
    else if(20000 >= income && income < 50000){
        tax = income * 0.12;
    }
    else if(50000 >= income && income < 70000){
        tax = income * 0.16;
    }
    else{
        tax = income * 0.28;
    }
    return tax;
}

int main(){
    string name;
    int age = 0;
    double income,tax;
    cout << "#####################\nUsername: ";
    cin >> name;
    cout << "Age: ";
    cin >> age;
    cout << "Income(Baht): " ;
    cin >> income;
    double kk = method_tax(income);
    if(age < 21 || age > 60)
        tax = 0.6 * method_tax(income);
    else
        tax = method_tax(income);

    cout << "Have to pay tax = " << tax << " Baht" << endl <<"#####################\n";
    return 0;
}

