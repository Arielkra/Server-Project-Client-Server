using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using iTextSharp.text;
using iTextSharp.text.pdf;


using System.IO;
using System.Net; //
using System.Net.Sockets;//
using System.Data.OleDb;

namespace myserver
{
    class Program
    {
        public static string Bdika(string str)
        {
            string str_new = "";
            string s1 = str.Substring(2);
            if (str[0] == '1')
            {

                str_new = Findworker(s1);
            }
            if (str[0] == '2')
            {
                str_new = TableTeacher(s1);
            }
            if (str[0] == '3')
            {
                str_new = delworker(s1);
            }
            if (str[0] == '4')
            {
                str_new = upworker(s1);
            }
            if (str.Contains("#all") == true)
            {
                str_new = listworker();
            }
            if (str[0] == '5')
            {
                try
                {
                    str_new = FindworkerbyName(s1);
                }
                catch {
                    str_new = "Worker Not Found!";
                        }

            }
            return str_new;
            

        }
        public static string listworker()
        {
            
            string str = "#all";
            string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=project.accdb;" + "Persist Security Info=False";
            OleDbConnection conn = new OleDbConnection(strDb);
            conn.Open();
            OleDbDataReader dr;
            OleDbCommand cmd = new OleDbCommand("Select * from worker;", conn); //command sql
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                str+= dr["fname"].ToString();
                str += ';';
                

            }
            dr.Close();
            conn.Close();
            return str;

        }
        public static string upworker(string str)
        {
            try
            {
                string fname = str.Substring(1, str.IndexOf('!') - 1);
                string id = str.Substring(str.IndexOf('!') + 1, 9);
                string phone = str.Substring(str.IndexOf('@') + 1, 10);
                string strDb;
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Project.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update worker set phone='" + phone + "' where id='" + id + "'; ";

                int n = cmd.ExecuteNonQuery();

               
                conn.Close();
                return "Update";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

                public static string delworker(string str)
        {
            try
            {
                string id = str;
                string strDb;
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Project.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "delete from worker where id='" + id + "';";

                int n = cmd.ExecuteNonQuery();

                conn.Close();

                return id + "DELETE!";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            
        }
        public static string TableTeacher(string str)
        {
            try
            {
                string fname = str.Substring(1, str.IndexOf('!')-1);
                string id = str.Substring(str.IndexOf('!') + 1, 9);
                string phone = str.Substring(str.IndexOf('@') + 1, 10);
                string strDb;
                strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Project.accdb;" + "Persist Security Info=False";
                OleDbConnection conn = new OleDbConnection(strDb);
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into worker values('" + fname + "','" + id + "','" + phone + "'); ";

                int n = cmd.ExecuteNonQuery();



                conn.Close();

                return ("Good");
            }
            catch (Exception e)
            {
                return e.ToString();
            }

        }
        public static string Findworker(string str)
        {
            string str_new = "i";
            string fname = "";
            string id = "";
            string phone = "";
            string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=project.accdb;" + "Persist Security Info=False";
            OleDbConnection conn = new OleDbConnection(strDb);
            conn.Open();
            OleDbDataReader dr;
            OleDbCommand cmd = new OleDbCommand("Select * from worker where id='" + str + "';", conn); //command sql
            dr = cmd.ExecuteReader(); // pointer 

            while (dr.Read())
            {

                fname = dr["fname"].ToString();
                id = dr["id"].ToString();
                phone = dr["phone"].ToString();

            }
            dr.Close();
            conn.Close();
            if (id == "")
                str_new = " Worker not found!";
            else
                str_new = "Name is:" + fname + " \nId is:" + id+"\nPhone is: "+phone;


            return str_new;

        }

        public static string FindworkerbyName(string str)
        {
            string str_new = "i";
            string fname = "";
            string id = "";
            string phone = "";
            string strDb = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=project.accdb;" + "Persist Security Info=False";
            OleDbConnection conn = new OleDbConnection(strDb);
            conn.Open();
            OleDbDataReader dr;
            OleDbCommand cmd = new OleDbCommand("Select * from worker where fname='"+str+"' ;", conn); //command sql
            dr = cmd.ExecuteReader(); // pointer 
            int i = 0;

            while (dr.Read())
            {
               

                    fname = dr["fname"].ToString();
                    id = dr["id"].ToString();
                    phone = dr["phone"].ToString();
                
            }
            dr.Close();
            conn.Close();
            if (id == "")
                str_new = " Worker not found!";
            else
                str_new = "Name is:" + fname + " \nId is:" + id + "\nPhone is: " + phone;


            return str_new;

        }


        static void Main(string[] args)
        {
            try
            {
                Int32 port = 13000; // number port
                IPAddress localAddr = IPAddress.Parse("127.0.0.1"); //ip address
                TcpListener server = new TcpListener(localAddr, port);
                server.Start();

                byte[] bytes = new byte[256];
                string data = null;
                while (true)
                {
                    Console.WriteLine("waiting to connection...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("connected ....");
                    Console.WriteLine(client.Client.RemoteEndPoint.ToString());
                    Console.WriteLine(DateTime.Now.ToString());

                    data = null;
                    NetworkStream stream = client.GetStream();
                    int i;
                    i = stream.Read(bytes, 0, bytes.Length);
                    data = Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine(data);

                    string data1 = Bdika(data);
                    byte[] msg = Encoding.ASCII.GetBytes(data1);
                    stream.Write(msg, 0, msg.Length);
                    client.Close();


                }

            }
            catch (Exception arr)
            {
                Console.WriteLine(arr.Message);
            }

            




        }
    }
}
