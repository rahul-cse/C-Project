using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApplication1
{
    
    public partial class customerinformatin : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True;");


        public customerinformatin()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            main ss = new main();
            ss.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            {
                this.Hide();
                Form1 ss = new Form1();
                ss.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand com = con.CreateCommand();
            SqlDataAdapter search = new SqlDataAdapter("SELECT       Customerinfo.Name, Customerinfo.Contactnumber, Customerinfo.Checkin, Customerinfo.NumOfRooms, RoomBooking.RoomNumber,  Customerinfo.Checkout  FROM            Customerinfo CROSS JOIN   RoomBooking WHERE (Customerinfo.Name = '" + textBox1.Text + "' AND ( Customerinfo.Name = RoomBooking.Name ))", con);
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) From RoomBooking where Name='" + textBox1.Text + "' ", con);
            DataTable dts = new DataTable();
            DataTable dt = new DataTable();

            sda.Fill(dts);
            search.Fill(dt);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
            string a = dts.Rows[0][0].ToString();
            dataGridView1.Columns.Clear();
           // MessageBox.Show(a);

            /*Adding text in Column head */


            if (a != "0")
            {
                DataGridViewColumn col1, col2, col3, col4, col5, col6;
                col1 = new DataGridViewTextBoxColumn();
                col1.DataPropertyName = "Name";
                col1.HeaderText = "Full Name";
                col1.Name = "cname";
                dataGridView1.Columns.Add(col1);
                col2 = new DataGridViewTextBoxColumn();
                col2.DataPropertyName = "Contactnumber";
                col2.HeaderText = "Contact Number";
                dataGridView1.Columns.Add(col2);

                col3 = new DataGridViewTextBoxColumn();
                col3.DataPropertyName = "Checkin";
                col3.HeaderText = "Check In Time";
                dataGridView1.Columns.Add(col3);

                col4 = new DataGridViewTextBoxColumn();
                col4.DataPropertyName = "NumOfRooms";
                col4.HeaderText = "Number Of Rooms";
                dataGridView1.Columns.Add(col4);

                col5 = new DataGridViewTextBoxColumn();
                col5.DataPropertyName = "Roomnumber";
                col5.HeaderText = "Room Number";
                dataGridView1.Columns.Add(col5);

                col6 = new DataGridViewTextBoxColumn();
                col6.DataPropertyName = "Checkout";
                col6.HeaderText = "Check Out Time";
                dataGridView1.Columns.Add(col6);
    
            }
            textBox1.Text = "";

            con.Close();
        }

        
       

        

        
    }
}
