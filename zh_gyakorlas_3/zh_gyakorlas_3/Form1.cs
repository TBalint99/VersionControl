using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zh_gyakorlas_3.Entities;

namespace zh_gyakorlas_3
{
    public partial class Form1 : Form
    {
        //Kiírni, hogy ez fogja majd tartalmazni a Users objektumokat
        //User-t majd meg kell hívni
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            //Kiírni, hogy hogyan kell Resource file-t hozzáadni
            //Ezeket is kiírni:
            label1.Text = Resource1.LastName;
            label2.Text = Resource1.FirstName;
            button1.Text = Resource1.Add;

            //Ezeket is ki kell írni!!!!!
            //users listából hívja az adatokat
            listBox1.DataSource = users;
            //ha erre rámegyünk, akkor az id-ját választja ki
            listBox1.ValueMember = "ID";
            //ezt jeleníti meg
            listBox1.DisplayMember = "FullName";
        }
    }
}
