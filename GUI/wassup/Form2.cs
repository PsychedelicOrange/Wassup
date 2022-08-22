using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
namespace wassup
{
    public partial class Form2 : Form
    {
        string activeUser;
        string activeChat;
        DataTable activeUser_rooms;
        public Form2(string user)
        {
            activeUser = user;
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // select chat
            if(listView1.SelectedItems.Count == 0)
                return;
            activeChat = (string)listView1.SelectedItems[0].Tag;
            updateMessages();

        }
        

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            
            msgInput.KeyDown += new KeyEventHandler(msgInput_KeyDown);
            activeUser_rooms = dHelper.GetDataSet("select * from ROOMS where RUID in ( select RUID from USER_ROOM where UUID = " + activeUser + ")").Tables[0];

            foreach (DataRow dr in activeUser_rooms.Rows)
            {
                ListViewItem listViewItem = new ListViewItem(dr["NAME"].ToString());
                listViewItem.Tag = dr["RUID"].ToString();
                listView1.Items.Add(listViewItem);
            }
            activeChat = (string)listView1.Items[0].Tag;

        }
        private string getRoomName(string name1, string name2)
        {
            if (name1.CompareTo(name2)>0)
            {
                return name1 + '-' + name2;
            }
            else
            {
                return name2 + '-' + name1;
            }
        }
        private void LoadData()
        {
           
        }
        private void msgInput_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void msgInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                // send message
                string command = "insert into MESSAGES values(" + activeUser + "," + activeChat + 
                    ","+ getIsGroup(activeChat) +",sysdate,'" + msgInput.Text + "')";
                Console.WriteLine(command);
                dHelper.ExecuteCommand(command);
                msgInput.Clear();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            updateMessages();
        }
        private string getUserName(string UUID)
        {
            return (string)dHelper.ExecuteCommand("select NAME from USERS where UUID =" + UUID);
        }
        private string getIsGroup(string RUID)
        {
            return dHelper.ExecuteCommand("select ISGROUP from ROOMS where RUID =" + RUID).ToString();
        }

        private void updateMessages()
        {
            chat.Clear();
            DataSet ds = dHelper.GetDataSet("select * from MESSAGES where RUID = " + activeChat);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                chat.Text += getUserName(dr["UUID"].ToString()) + " : " + dr["CONTENT"].ToString() + "\n";
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
