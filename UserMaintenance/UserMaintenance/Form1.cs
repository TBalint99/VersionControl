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
using UserMaintenance.Entities;

namespace UserMaintenance
{
    public partial class Form1 : Form
    {
        BindingList<User> users = new BindingList<User>();
        public Form1()
        {
            InitializeComponent();
            lblLastName.Text = Resource_hu_en.LastName; // label1
            lblFirstName.Text = Resource_hu_en.FirstName; // label2
            btnAdd.Text = Resource_hu_en.Add; // button1
            btnWrite.Text = Resource_hu_en.Write; //button2
            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                LastName = txtLastName.Text,
                FirstName = txtFirstName.Text
            };
            users.Add(u);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw = new StreamWriter(sfd.FileName,false,Encoding.UTF8))
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
    }
}
