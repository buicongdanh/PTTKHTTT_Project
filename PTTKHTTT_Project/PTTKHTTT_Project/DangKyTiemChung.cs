using System;
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
    public partial class DangKyTiemChung : Form
    {
        public DangKyTiemChung()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                panel_NGH.Visible = true;
            else
                panel_NGH.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Tiêm theo gói")
            {
                Form ChonVaccine_Nhom = new ChonVaccine_Nhom();
                openChildForm(ChonVaccine_Nhom);
            }

            if (comboBox1.Text == "Tiêm lẻ")
            {
                Form ChonVaccine_Le = new ChonVaccine_Le();
                openChildForm(ChonVaccine_Le);
            }
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_ChonVC.Controls.Add(childForm);
            panel_ChonVC.Tag = childForm;
            childForm.Show();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime start = monthCalendar1.SelectionRange.Start;
            string formattedStart = start.ToString("yyyy-MM-dd");
            textBox_Date.Text = formattedStart;
        }

        private void textBox_Date_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }

        private void textBox_Date_Leave(object sender, EventArgs e)
        {
            monthCalendar1.Visible = false;
        }

        private void DangKyTiemChung_Load(object sender, EventArgs e)
        {
            String q = "select HOTEN, DIACHI, SDT from KHACHHANG WHERE MAKH ='" + Menu_KH.MaKH + "'";

            SqlCommand cmd = new SqlCommand(q);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection = Menu_KH.con;
            try
            {
                DataSet dataset = new DataSet();
                da.Fill(dataset);

                DataTable table = dataset.Tables[0]; // Get the data table.
                textBox2.Text = Menu_KH.MaKH;
                textBox3.Text = table.Rows[0].ItemArray[0].ToString();
                textBox4.Text = table.Rows[0].ItemArray[1].ToString();
                textBox5.Text = table.Rows[0].ItemArray[2].ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Xu ly thong tin
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Thong tin dang ky khong hop le, vui long nhap lai!");
            }
            else if (ChonVaccine_Le.dgvPublic.RowCount == 1 && comboBox1.Text == "Tiêm lẻ")
            {
                MessageBox.Show("Ban chua chon vaccine!");
            }
            else if (ChonVaccine_Nhom.dgvPublic.RowCount == 1 && comboBox1.Text == "Tiêm theo gói")
            {
                MessageBox.Show("Ban chua chon vaccine!");
            }

            //Dieu kien hop le
            else
            {
                //Generate MaPhieuDK
                string q = "select top 1 MAPDKTIEM from PHIEUDKTIEM order by MAPDKTIEM desc";
                string MaPDKTiem = "";
                SqlCommand com = new SqlCommand(q, Menu_KH.con);
                using (SqlDataReader read = com.ExecuteReader())
                {
                    while (read.Read())
                    {
                        MaPDKTiem = NV_ThanhToan.increment((read["MAPDKTIEM"].ToString()), "DK");
                    }
                }

                MessageBox.Show(MaPDKTiem);
/*
                //PHIEUDK
                String query = "INSERT INTO PHIEUDKTIEM VALUES" +
                    "(@MAPDKTIEM, @MAKH, @DIACHI, @GIOITINH, @NGAYTIEM, " +
                    "@HOTEN, @SDT, @HOTEN_NGH, @SDT_NGH, @MQH)";

                using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
                {
                    command.Parameters.AddWithValue("@MAPDKTIEM", textBox2.Text);
                    command.Parameters.AddWithValue("@MAKH", textBox2.Text);
                    command.Parameters.AddWithValue("@DIACHI", textBox3.Text);
                    command.Parameters.AddWithValue("@GIOITINH", textBox4.Text);
                    command.Parameters.AddWithValue("@NGAYTIEM", textBox5.Text);
                    command.Parameters.AddWithValue("@HOTEN", textBox5.Text);
                    command.Parameters.AddWithValue("@SDT", textBox2.Text);
                    command.Parameters.AddWithValue("@HOTEN_NGH", textBox2.Text);
                    command.Parameters.AddWithValue("@SDT_NGH", textBox3.Text);
                    command.Parameters.AddWithValue("@MQH", textBox4.Text);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Lap Hoa Don Thanh Cong");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                //foreach
                {
                    query = "";
                    query = "INSERT INTO CT_PDKTIEM VALUES (@MAPDKTIEM, @MAVC, @NGAYTIEM";
                    using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
                    {
                        command.Parameters.AddWithValue("@MAPDKTIEM", textBox2.Text);
                        command.Parameters.AddWithValue("@MAKH", textBox2.Text);
                        command.Parameters.AddWithValue("@DIACHI", textBox3.Text);

                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Lap Hoa Don Thanh Cong");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }

                query = "INSERT INTO HOADON VALUES" +
                    "(@MAHD, @HINHTHUC, @SOTIEN, @MADON, @DOTT, @TINHTRANG)";

                using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
                {
                    //Them PHIEUDK
                    command.Parameters.AddWithValue("@MAHD", textBox2.Text);
                    command.Parameters.AddWithValue("@HINHTHUC", textBox2.Text);
                    command.Parameters.AddWithValue("@SOTIEN", textBox3.Text);
                    command.Parameters.AddWithValue("@MADON", textBox4.Text);
                    command.Parameters.AddWithValue("@DOTT", textBox5.Text);
                    command.Parameters.AddWithValue("@TINHTRANG", textBox5.Text);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Lap Hoa Don Thanh Cong");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                query = "INSERT INTO DONMUAVC VALUES" +
                    "(@MADON, @MAKH, @NGAYDATMUA, @TONGTIEN)";

                using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
                {
                    //Them PHIEUDK
                    command.Parameters.AddWithValue("@MAHD", textBox2.Text);
                    command.Parameters.AddWithValue("@HINHTHUC", textBox2.Text);
                    command.Parameters.AddWithValue("@SOTIEN", textBox3.Text);
                    command.Parameters.AddWithValue("@MADON", textBox4.Text);
                    command.Parameters.AddWithValue("@DOTT", textBox5.Text);
                    command.Parameters.AddWithValue("@TINHTRANG", textBox5.Text);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Lap Hoa Don Thanh Cong");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                //foreach
                {
                    query = "";
                    query = "INSERT INTO CT_DMVC VALUES (@MADON, @MAVC, @SOLUONG, @GIA";
                    using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
                    {
                        command.Parameters.AddWithValue("@MADON", textBox2.Text);
                        command.Parameters.AddWithValue("@MAVC", textBox2.Text);
                        command.Parameters.AddWithValue("@SOLUONG", textBox3.Text);
                        command.Parameters.AddWithValue("@GIA", textBox3.Text);

                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Lap Hoa Don Thanh Cong");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }

                //foreach
                {
                    query = "";
                    query = "INSERT INTO CT_PDKTIEM VALUES (@MAPDKTIEM, @MAVC, @NGAYTIEM";
                    using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
                    {
                        command.Parameters.AddWithValue("@MAPDKTIEM", textBox2.Text);
                        command.Parameters.AddWithValue("@MAKH", textBox2.Text);
                        command.Parameters.AddWithValue("@DIACHI", textBox3.Text);

                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Lap Hoa Don Thanh Cong");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }*/
            }
        }
    }
}
