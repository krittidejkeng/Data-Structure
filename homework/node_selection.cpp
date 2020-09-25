#include <iostream> 
using namespace std; 
  
struct Node  
{
    int data;  
    Node *next;  
};  

void insert(Node** head_ref, int new_data)  
{  
    Node* new_node = new Node(); 
    new_node->data = new_data;
    new_node->next = NULL;  
    Node *last = *head_ref; 
    // 
    if (*head_ref == NULL)  
    {  
        // นำค่าของ new_node มาเก็บใน memory ของ head_ref 
        *head_ref = new_node;  
        return;  
    }  
  
    while (last->next != NULL)  
        last = last->next;  

    last->next = new_node;  
    return;  
}  

void print(Node* n){
    if(n->next != NULL){
        cout << n->data << " ";
        print(n->next);
    }
    else
        cout << n->data;
} 

Node* selection(Node* val){
    Node* std = new Node();
    std = val;
    while(val != NULL){
        /*tmp สร้างไว้สำหรับ เปรียบเทียบลูปวงใน
        * min จะเก็บค่าแรกเหมือนกับ v ซึ่ง min จะเปรียบเทียบแล้สเอาค่าที่น้อยที่สุดมา
        * แต่ v จะเก็บค่าปัจจุบันเปรียเสมือ lock ค่าไว้ */
        Node* tmp = new Node();
        tmp = val;
        int min = val->data;
        int v = val->data;
        while(tmp != NULL){
            cout<<tmp->data<<" ";
            Node* check = new Node();
            /* จะทำการเช็คว่า ข้อลูปนอกมากว่าค่าลูปในไหม
            * ถ้ามากกมว่า จะนำค่าที่น้อยใส่ใน min และ ให้ Node check เท่ากับ tmp 
            * เปรียบเสมือนการเทียบอินเด็กเพื่อให้เรารู้ว่า ข้อมูลที่ลำดับเท่าไหร่ที่น้อยที่สุด */
            if(val->data > tmp->data){
                min = tmp->data;
                check = tmp;
            }
            // เช็ว่าถ้าตัวท้ายเมื่อไหร่ ให้สลับค่า v ที่ลอคไว้มาใส่ใน check ที่
            if(tmp->next == NULL)
                check->data = v;        
            tmp = tmp->next;
        }
        val->data = min;
        cout<<endl;
        val= val -> next;
    }
    return std;
}
int main()  
{  
    Node* val = NULL;  
    insert(&val,1);
    insert(&val,7);
    insert(&val,8);
    insert(&val,9);
    insert(&val,4);
    cout<<"Link list: ";  
    print(val);
    cout << endl;  
    val = selection(val);
    cout << "after sort: ";
    print(val);
    return 0;  
}  