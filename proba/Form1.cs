using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proba
{
    public partial class Form1 : Form
    {
        UserContext db;
        public Form1()
        {
            InitializeComponent();
            db = new UserContext();
            db.Users.Load();
            dgwChat.DataSource = db.Users.Local.ToBindingList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            DialogResult result =  userForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            User user = new User();
            user.Name = userForm.Controls["txtbxName"].Text;
            db.Users.Add(user);
            db.SaveChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
