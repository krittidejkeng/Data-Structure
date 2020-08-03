#include <iostream>
#include <fstream>

using namespace std;

/************************ เมธอด สำหรับ หาค่า mix max average *******************/
void threevalue(int value_file[], int count){
    int max = 0, min = 999;
    float total = 0;
    for(int i=0; i<count; i++){
        total += value_file[i];
        if(value_file[i] <= min) min = value_file[i];
        else if(value_file[i] >= max) max = value_file[i];
    }
    cout << "min is: " << min << endl;
    cout << "max is: " << max << endl;
    cout << "average is: " << total/count << endl;
}


/***************** เมธอดหาค่า median *****************/
void median(int value_file[], int count){
    float index_med = (float)(count+1)/2;
    string str1 = to_string(index_med);
    if(index_med/(int)index_med == 1) 
        cout << "median is: " << value_file[(int)index_med -1] << endl;
    else
        cout << "median is: " << (float)(value_file[(int)index_med -1] + value_file[(int)index_med])/2 << endl;
}

/******************* เมธอดหาค่า mode *******************/
void mode(int count,string file_name){
    ifstream file;
    string line;
    file.open(file_name+".txt");
    int value_file[count];
    int n_file = 0;
    while(getline(file, line)){
        value_file[n_file] = stoi(line);
        n_file++;
    }
    file.close();

    /*เรียงข้อมูลจากน้อยไปมาก*/
    int temp;
    for(int i=0; i<count; i++){
        for(int j=i+1; j<count; j++){
            if(value_file[j] < value_file[i]){
                temp = value_file[i];
                value_file[i] = value_file[j];
                value_file[j] = temp;
            }
        }
    }

    /*ทำการนับว่ามีตัวเลขที่ซ้ำกันหรือไม่ ซึ่งตัวเลขที่ซ้ำกันมากที่สุดจะมีค่าตามการซ้ำของข้อมูลนั้นๆ และจะเก็บข้อมูลอยู่ใน mode[]*/
    int mode[count];
    for(int i=0; i<count; i++)
        mode[i] = 0;
    for(int i=0; i<sizeof(value_file)/sizeof(value_file[0]); i++){
        for(int j=0; j<=i; j++){
            if(value_file[i] == value_file[j])
                mode[j]++;
        }
    }

    /* นี้จะทำการเช็คข้อมูล mode[] ว่าข้อมูลไหนซ้ำกันมากที่สุด และ นำข้อมูลนั้นมาเก็บในตัวแปร max_mode */
    int max_mode = 0;
    for(int i=0; i<sizeof(mode)/sizeof(mode[0]); i++)
            if(mode[i] >= max_mode) max_mode = mode[i];           
    

    /* ตัวแปร max_mode นั้นจะทำการเช็คกับข้อมูลของ mode[] ว่ามีข้อมูลซ้ำกันหรือไม่ 
    หากมี ตัวแปร index_mode นั้นจะบวก 1 เพื่อนำค่าของตัวมันเองไปเป็น index 
    ในการสร้าง array ของ point_mode[index_mode] */
    int index_mode = 0;
    for(int i=0; i<sizeof(mode)/sizeof(mode[0]); i++){
         if(max_mode == mode[i])
            index_mode++;
    }

    /* ถ้าหาก maxmode > 1 นั้นบ่งบอกว่ามีฐานนิยม */
    if(max_mode > 1){

        /* point_mode[index_mode] จะเก็บ index ของ mode[] ที่เท่ากับ max_mode(ค่าที่ซ้ำกันมากที่สุด) */
        int point_mode[index_mode],j=0;
        for(int i=0; i<sizeof(point_mode)/sizeof(point_mode[0]); i++){
            while(true){
                if(max_mode == mode[j]){
                    point_mode[i] = j;
                    j++;
                    break;
                }
                else if(j == sizeof(mode)/sizeof(mode[0]))  break;
                else    j++;
            }
        }   
    /* จะเข้า if ก็ต่อเมื่อมีข้อมูลที่ซ้ำกันเท่ากัน 2 ข้อมูลขึ้นไป
    และ เป็น else ก็ต่อเมื่อ มีข้อมูลซ้ำกันแค่ตัวเดียว */
        if(sizeof(point_mode)/sizeof(point_mode[0]) > 1){
            for(int i=0; i<sizeof(point_mode)/sizeof(point_mode[0]); i++)
                cout << "mode"<< i+1 <<" : " << value_file[point_mode[i]] << endl;
        }
        else    cout << "mode: " << value_file[point_mode[0]] << endl;
    }

    else    cout << "have no mode in this file" << endl;
}


/********************* method สำหรับเช็คข้อมูลว่าเป็นตัวเลขหรือไม่ *******************************/
bool check_number(string value_file[],int count){ 
    bool count_true = true, count_false = true;
    int line_error = 0;
    string value_error;
    for (int i = 0; i < count; i++) { 
        char p[value_file[i].length()];
        string str = value_file[i];
        for(int j=0; j<str.length(); j++){
             p[j] = str[j];

            if(p[j] >= '0' && p[j] <= '9')
                count_true = true;
            else{
                count_false = false;
                line_error = i+1;
                value_error = value_file[i];
                break;
            }
        } 
    } 
    if(count_true == true && count_false == true){
        cout << "this file number" << endl;
        return count_true;
    }   
    else{
        cout << "this file " << endl << "value not a number : " << value_error 
        << endl << "error line: " << line_error <<endl;
        return count_false;
    }  
}

int main(){   
     /************ อ่านไฟล์สำหรับนับจำนวนข้อมู๔ลทั้งหมดในไฟล์ ************/
    ifstream file;
    string line = "";
    string file_name = "";
    cout << "Please Enter Name: ";
    cin >> file_name;
    file.open(file_name+".txt");
    string value[7];
    int count=0;
    while(getline(file,line)) 
        count++;
    file.close();

    /****************** อ่านไฟล์สำหรับใช้หาข้อมูล *********************/
    ifstream file_2;
    string line_2 = "";
    string value_file_str[count];
    int value_file[count];
    file_2.open(file_name+".txt");
    // n_file สำหรับหมุน index
    int n_file = 0; 
    while(getline(file_2,line_2)){ 
        value_file_str[n_file] = line_2;
        n_file++;     
    }
    file_2.close();

    if(check_number(value_file_str,count)){
        for(int i=0; i<count; i++)
            value_file[i] = stoi(value_file_str[i]);
         int temp;
    for(int i=0; i<count; i++){
        for(int j=i+1; j<count; j++){
            if(value_file[j] < value_file[i]){
                temp = value_file[i];
                value_file[i] = value_file[j];
                value_file[j] = temp;
            }
        }
    }

    threevalue(value_file,count);
   
    median(value_file,count);

    mode(count,file_name);   
    } 
    else
    cout << "file not success!" << endl;
    return 0;
}