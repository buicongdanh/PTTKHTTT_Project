﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PTTKHTTT_Project
{
    public partial class NV_ThanhToan : Form
    {
        public NV_ThanhToan()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String q = "select * from HOADON";
            if (textBox1.Text.Trim().Length > 0)
            {
                q = q + " WHERE MAHD ='" + textBox1.Text + "'";
            }

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_NV.con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String query = "UPDATE HOADON SET tinhtrang = 'HOANTHANH' WHERE MAHD=@MAHD AND HINHTHUC=@HINHTHUC AND SOTIEN=@SOTIEN AND dott=@dott";

            using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
            {
                string temp = textBox2.Text;
                command.Parameters.AddWithValue("@MAHD", temp);
                temp = textBox6.Text;
                command.Parameters.AddWithValue("@HINHTHUC", temp);
                temp = textBox3.Text;
                command.Parameters.AddWithValue("@SOTIEN", temp);
                temp = textBox5.Text;
                command.Parameters.AddWithValue("@dott", temp);
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thanh Toan Hoa Don Thanh Cong");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }    
        }

        private void NV_ThanhToan_Load(object sender, EventArgs e)
        {
            String q = "select * from HOADON";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_NV.con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string q = "select top 1 mahd from hoadon order by mahd desc";
            SqlCommand com = new SqlCommand(q, Menu_NV.con);
            using (SqlDataReader read = com.ExecuteReader())
            {
                while (read.Read())
                {
                    string new_mahd = increment((read["mahd"].ToString()), "HD");

                    textBox2.Text = new_mahd;
                }
            }
        }

        public static string increment(string before, string head)
        {
            string result = head;
            string num = before.Substring(2, before.Length - 2);
            int next_val = Convert.ToInt32(num);
            next_val++;
            //Get number digit
            int temp = next_val;
            int number_of_digit = 0;
            while (temp != 0)
            {
                temp /= 10;
                number_of_digit++;
            }
            //Making new MaHD
            for (int i = 0; i < 6 - number_of_digit; i++)
            {
                result = result + "0";
            }
            result += next_val.ToString();
            return result;
        }
    }
}
