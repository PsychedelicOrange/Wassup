using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wassup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            maskedTextBox1.KeyDown += new KeyEventHandler(maskedTextBox1_KeyDown);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string command = "SELECT COUNT(*) FROM USERS WHERE UUID = " + maskedTextBox1.Text;
            object result = dHelper.ExecuteCommand(command);
            Console.WriteLine(result);
            if(result == null)
            {
                MessageBox.Show("User does not exist !");
            }

            else
            {
                Form2 form = new Form2(maskedTextBox1.Text);
                form.Show();
                this.Hide();
            }
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();

            }
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
