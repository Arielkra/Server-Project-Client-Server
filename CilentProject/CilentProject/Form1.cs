using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.OleDb;

using System.IO;
using System.Net.Sockets;
using System.Net;
namespace CilentProject
{

    public partial class Form1 : Form
    {
        string messagefromserver = "";
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "13000";
        }

        public bool SendId(string strId)
        {
            
            try
            {
                
                


                Int32 port = int.Parse(textBox2.Text); // 13000
                IPAddress addressserver = IPAddress.Parse(textBox1.Text); //33.88.44.81
                TcpClient client = new TcpClient(addressserver.ToString(), port);
                listBox1.Items.Clear();
                listBox1.Items.Add("Connection to server :");
                byte[] data = System.Text.Encoding.ASCII.GetBytes(strId);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                 
                
                data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                messagefromserver = Encoding.ASCII.GetString(data, 0, bytes);
                listBox1.Items.Add("Answer from Server :" + messagefromserver);
                if (messagefromserver.Contains("#all"))
                {
                    string item="";
                    comboBox1.Items.Clear();
                    for(int i = 4; i < messagefromserver.Length; i++)
                    {
                        if (messagefromserver[i] == ';')
                        {
                            comboBox1.Items.Add(item);
                            item = "";
                        }
                        else {
                            item += messagefromserver[i];
                        }
                        

                    }
                }
                // MessageBox.Show(messagefromserver);
                stream.Close();
                client.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Please Connected To Server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            
                return true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            

        }

        private void button4_Click(object sender, EventArgs e)
        {

            string str = textBox6.Text;
            string str1 = label9.Text;

            try
            {

                SendId("1#" + str);
                label11.Text = messagefromserver.Substring(messagefromserver.LastIndexOf("\n") + 10);//phone
                label10.Text = messagefromserver.Substring(messagefromserver.IndexOf(":") + 1, messagefromserver.IndexOf(' ') + 1);//name
                label9.Text = messagefromserver.Substring(messagefromserver.IndexOf("\n") + 7, 9);


                if (textBox6.Text == label9.Text)
                {


                }
                else
                {
                    MessageBox.Show("Id Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("The Server Not Connected...\n Please Connect To the srever and Try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "";


            SendId("2#" + str + " " + textBox8.Text + "!" + textBox7.Text + "@" + textBox3.Text);
            if (SendId("#all"))
            {
                MessageBox.Show("!עובד נוסף בהצלחה", "הוספת עובד ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Text = "";
                label10.Text = "";
                label9.Text = "";
                label11.Text = "";

            }
        }
             

        

        private void button5_Click(object sender, EventArgs e)
        {
            
           
                string str = "";
                SendId("3#" + str + textBox7.Text);
            if (SendId("#all"))
            {
                MessageBox.Show("!עובד הוסר בהצלחה", "הסרת עובד", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Text = "";
                label10.Text = "";
                label9.Text = "";
                label11.Text = "";
                textBox3.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "";
            SendId("4#" + str + " " + textBox8.Text + "!" + textBox7.Text + "@" + textBox3.Text);
            if (SendId("#all"))
            {
                MessageBox.Show("!עודכן מספר הטלפון", "עדכון", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Text = "";
                label10.Text = "";
                label9.Text = "";
                label11.Text = "";

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת", "! יציאה מהמערכת ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (dr == DialogResult.OK)
            {
                Application.ExitThread();
            }
            else
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
            

                
                SendId("5#" +comboBox1.Text);
                label11.Text = messagefromserver.Substring(messagefromserver.LastIndexOf("\n") + 10);//phone
                label10.Text = messagefromserver.Substring(messagefromserver.IndexOf(":") + 1, messagefromserver.IndexOf(' ') + 1);//name
                label9.Text = messagefromserver.Substring(messagefromserver.IndexOf("\n") + 7, 9);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SendId("#all"))
            {
                MessageBox.Show("!שרת התחבר בהצלחה");
            }
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
