using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zh_gyakorlas_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Kiírni, hogy hogyan kell Resource file-t hozzáadni
            //Ezeket is kiírni:
            label1.Text = Resource1.LastName;
            label2.Text = Resource1.FirstName;
            button1.Text = Resource1.Add;
        }
    }
}
