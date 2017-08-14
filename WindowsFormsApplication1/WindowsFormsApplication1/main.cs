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

    public partial class main : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\HP\Documents\Visual Studio 2010\Projects\WindowsFormsApplication1\WindowsFormsApplication1\Database1.mdf;Integrated Security=True");
        
        
        int stat = 0, a ;
        int[] roomno = new int[100];
        //long cost = 0;
        string gender;

        public main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void buttonforsearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            SqlDataAdapter da = new SqlDataAdapter("select Roomnumber, Type, Floor,Price from Roominfo where Type ='" + comboBox1.Text + "' and Status = 0", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
            con.Close();

            if (comboBox1.Text == "Both" )
            {
                 //MessageBox.Show(comboBox1.Text);

                 con.Open();
                 SqlCommand cmd1 = con.CreateCommand();
                 SqlDataAdapter da1 = new SqlDataAdapter("select Roomnumber, Floor,Price, Type from Roominfo where Status = 0", con);
                 DataTable dt1 = new DataTable();
                 da1.Fill(dt1);
                 dataGridView1.DataSource = dt1;

                 con.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = comboBox1.Text;
        }



        


        private void button8_Click_1(object sender, EventArgs e)
        {
            
        }

        


        private void button6_Click(object sender, EventArgs e)
        {
            foreach(Control c in panel3.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    if (cb.Checked == true)
                    {
                        cb.Checked = false;
                    }
                }
            }
            panel3.Visible = true;
            panel4.Visible = false;
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
           long cost = 0;
           int b = a;
           //int carry = 0 ;
           while (b > 0)
           {

               con.Open();
               SqlCommand cmd3 = new SqlCommand(" SELECT Price FROM Roominfo WHERE RoomNumber = '" + roomno[b] + "' ", con);
               SqlDataReader mdr;
               mdr = cmd3.ExecuteReader();
               while (mdr.Read())
               {
                   long s = mdr.GetInt64(0);
                   //MessageBox.Show(s.ToString());
                   cost = cost + s;

               }
               if (cost > 0)
               {
                   textBoxcost.Text = cost.ToString();
                   mdr.Close();
                   con.Close();
                   //carry++;
                   b--;
                   //stat--;
               }
              
 
            }
             if(cost==0)
               {
                   textBoxcost.Text = "0";
               }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (stat > 0)
            {
                num_of_rooms.Text = stat.ToString();
            }
            else 
            {
                num_of_rooms.Text = "0";
            }
           
            

            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }



        private void checkBox201_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox201.Checked)
            {
                stat++;
                a++;
                roomno[stat] = 201;
                MessageBox.Show(roomno[stat].ToString());
            }
            else
            {
                stat--;
                a--;
            }
        }

        private void checkBox202_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox202.Checked)
            {
                stat++;
                a++;
                roomno[stat] = 202;
                MessageBox.Show(roomno[stat].ToString());
            }
            else
            {
                stat--;
                a--;
            }
        }
        private void checkBox203_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox203.Checked)
            {
                stat++;
                a++;
                roomno[stat] = 203;
                MessageBox.Show(roomno[stat].ToString());
            }
            else
            {
                stat--;
                a--;
            }
        }

        private void checkBox204_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox204.Checked)
            {
                stat++;
                a++;
                roomno[stat] = 204;
                MessageBox.Show(roomno[stat].ToString());
            }
            else
            {
                stat--;
                a--;
            }
        }

        private void checkBox205_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox205.Checked)
            {
                stat++;
                a++;
                roomno[stat] = 205;
                MessageBox.Show(roomno[stat].ToString());
            }
            else
            {
                stat--;
                a--;
            }
        }

        private void checkBox206_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox206.Checked)
            {
                stat++;
                a++;
                roomno[stat] = 206;
                MessageBox.Show(roomno[stat].ToString());
            }
            else
            {
                stat--;
                a--;
            }
        }
        //*************Code for check in and room booking*****************//

        private void Check_in_Click(object sender, EventArgs e)
        {
            if (stat > 0)
            {
                con.Open();
                SqlCommand checkin = new SqlCommand(@"INSERT INTO Customerinfo
                         (Name, Nationality, Address, NID, Passportno, Contactnumber, Gender, NumOfRooms, Checkin)
VALUES        ('" + textBox1_name.Text + "','" + textBox2_nation.Text + "','" + textBox3_address.Text + "','" + textBox4_passport.Text + "','" + textBox5_NID.Text + "','" + textBox6_cellno.Text + "','" + gender + "','" + num_of_rooms.Text + "','" + dateTimePicker1.Value.ToLocalTime().ToUniversalTime() + "')", con);


                checkin.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Checked In");
            }
            else
                MessageBox.Show("You need to book some rooms first");
            while (stat > 0)
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("UPDATE  Roominfo SET Status = 1  WHERE (RoomNumber = '" + roomno[stat] + "' AND Status = 0) ", con);
                SqlCommand checkin2 = new SqlCommand("INSERT INTO RoomBooking (Name, Roomnumber) VALUES        ('"+ textBox1_name.Text +"','"+ roomno[stat] +"' )", con );
                cmd2.ExecuteNonQuery();
                checkin2.ExecuteNonQuery();
                con.Close();
                stat--;
            }


        
        
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
             gender = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            checkout ss = new checkout();
            ss.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 ss = new Form1();
            ss.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            customerinformatin s = new customerinformatin();
            s.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphic = e.Graphics;
            string dash = "_ _  _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _";
            string fullname = "Name ".PadRight(23) +": "  + textBox1_name.Text;
            string nation = "Nationality ".PadRight(23) +": " + textBox2_nation.Text;
            string passport = "Passport No ".PadRight(20) +": " + textBox4_passport.Text;
            string dt = "Booking Time & Date ".PadRight(23) +": " + dateTimePicker1.Value;
            string rm = "Room Number ".PadRight(17) +": ";
            int startx = 215, p = 3;

            
            graphic.DrawString("Hotel Management", new Font("Arial", 17, FontStyle.Bold), Brushes.Black, 200, 10);
            graphic.DrawString(dash, new Font("Arial", 8, FontStyle.Regular), Brushes.Black, 80, 28);
            
            graphic.DrawString(fullname,new Font("Arial",11,FontStyle.Regular),Brushes.Black,89,50);
            graphic.DrawString(nation, new Font("Arial", 11, FontStyle.Regular), Brushes.Black,89,65);
            graphic.DrawString(passport, new Font("Arial", 11, FontStyle.Regular), Brushes.Black,89,80);
            graphic.DrawString(dt, new Font("Arial", 11, FontStyle.Regular), Brushes.Black, 89,95);
            graphic.DrawString(rm, new Font("Arial", 11, FontStyle.Regular), Brushes.Black, 89, 110);


            foreach (int c in roomno)
            {
                if (c != 0)
                {
                    graphic.DrawString(c.ToString(), new Font("Arial", 11, FontStyle.Regular), Brushes.Black, startx + p, 110);
                    p = p + 28;
                }
            }
        }

        private void checkBox301_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox203.Checked)
            {
                stat++;
                a++;
                roomno[stat] = 203;
                MessageBox.Show(roomno[stat].ToString());
            }
            else
            {
                stat--;
                a--;
            }
        }

        private void checkBox302_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox203.Checked)
            {
                stat++;
                a++;
                roomno[stat] = 203;
                MessageBox.Show(roomno[stat].ToString());
            }
            else
            {
                stat--;
                a--;
            }
        }

        private void checkBox304_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox304.Checked)
            {
                stat++;
                a++;
                roomno[stat] = 304;
                MessageBox.Show(roomno[stat].ToString());
            }
            else
            {
                stat--;
                a--;
            }
        }

        private void checkBox305_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox305.Checked)
            {
                stat++;
                a++;
                roomno[stat] = 305;
                MessageBox.Show(roomno[stat].ToString());
            }
            else
            {
                stat--;
                a--;
            }
        }



 


        




        

        
    }
}
