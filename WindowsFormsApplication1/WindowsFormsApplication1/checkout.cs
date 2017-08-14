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
    public partial class checkout : Form
    {

        SqlConnection con2 = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\HP\Documents\Data.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True;");

        DateTime d1;
        long cost = 0 , total= 0 ;
        int a = 0;
        string msglb4;


        public checkout()
        {
            InitializeComponent();
        }

        private void button1_Checkout_Click(object sender, EventArgs e)
        {
            //int a =0 ;
            con2.Open();
            SqlCommand cmd9 = new SqlCommand("SELECT        Roominfo.Status FROM           RoomBooking CROSS JOIN Roominfo WHERE (RoomBooking.Name = '" + textBox1_customername.Text + "' AND (RoomBooking.RoomNumber = Roominfo.RoomNumber)) ", con2);
            SqlDataReader mdr;
            mdr = cmd9.ExecuteReader();
            while (mdr.Read())
            {
                 a = mdr.GetInt16(0);
            }
            mdr.Close();
            if ( a == 1)
            {
                SqlCommand cmd = new SqlCommand("UPDATE      Roominfo SET    Status =0  FROM          Roominfo CROSS JOIN RoomBooking  WHERE  (Name = '" + textBox1_customername.Text + "' AND (RoomBooking.RoomNumber = Roominfo.RoomNumber ))  ", con2);
                cmd.ExecuteNonQuery();
                con2.Close();
                string message = textBox1_customername.Text + " is successfully checked out. ";
                MessageBox.Show(message);
                textBox1_customername.Text = "";
            }
            else
                MessageBox.Show("Already Checked out");
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            main ss = new main();
            ss.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss = new Form1();
            ss.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Show();
            con2.Open();
            SqlCommand cmd8 = new SqlCommand("SELECT   Checkin FROM Customerinfo WHERE (Name = '" + textBox1_customername.Text + "') ", con2);
            SqlDataReader mdr2, mdr3;
            mdr2 = cmd8.ExecuteReader();
            mdr2.Read();
            d1 = mdr2.GetDateTime(0);
            mdr2.Close();
            DateTime d2 = DateTime.Now;
            //TimeSpan ts = (d2 - d1);
            var v = (d2 - d1).TotalHours;
            //MessageBox.Show(v.ToString());
            int days, x, y;
             x = Convert.ToInt32(v);
             y = x / 12;
             if ((y % 2) == 0)
             {
                 days = y / 2;
             }
             else
             {
                 days = y - 1;
             }
            
            //int days = ts.Days;
            
            if (days == 0)
            {
                days = 1;
                msglb4 = "Reservation days : " + days.ToString();
                label4.Text = msglb4.ToString();
            }
            else
                 msglb4 = "Reservation days : " + days.ToString();
                label4.Text = msglb4.ToString();
            //con2.Close();

            label2.Text = textBox1_customername.Text;

                /*..........Code for calculating total room reservation cost..........*/


                SqlCommand cmd7 = new SqlCommand("SELECT  Roominfo.Price     FROM RoomBooking CROSS JOIN Roominfo   WHERE(RoomBooking.Name = '" + textBox1_customername.Text + "' AND   RoomBooking.RoomNumber = Roominfo.RoomNumber)  ", con2);
                mdr3 = cmd7.ExecuteReader();
                while (mdr3.Read())
                {
                    cost = mdr3.GetInt64(0);
                    cost = cost * days;
                    total = total + cost;
                }
                string msglabel3 = " Total Cost : " + total.ToString();
                label3.Text = msglabel3.ToString();
                con2.Close();
                total = 0;
            }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
      

        
    }
}
