using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            button2.Text = Resource1.Write;
            button3.Text = Resource1.Delete;

            //Ezeket is ki kell írni!!!!!
            //users listából hívja az adatokat
            listBox1.DataSource = users;
            //ha erre rámegyünk, akkor az id-ját választja ki
            listBox1.ValueMember = "ID";
            //ezt jeleníti meg
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                LastName = textBox1.Text,
                FirstName = textBox2.Text
            };
            users.Add(u);
            //MessageBox.Show(users[0].FirstName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var s in users)
                {
                    sw.Write(s.ID);
                    sw.Write(";");
                    sw.Write(s.FirstName);
                    sw.WriteLine();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var name = Convert.ToString(listBox1.SelectedValue);
            var u = from x in users
                    where x.ID.ToString() == name
                    select x;
            users.Remove(u.FirstOrDefault());
        }
    }
}
