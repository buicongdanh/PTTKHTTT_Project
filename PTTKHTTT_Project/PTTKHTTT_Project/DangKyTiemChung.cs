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
            if (comboBox1.Text == "" || textBox_Date.Text == "")
            {
                MessageBox.Show("Thong tin dang ky khong hop le, vui long nhap lai!");
            }

            else if (ChonVaccine_Le.dgv_Le_Public.RowCount == 1 || ChonVaccine_Nhom.dgv_Nhom_Public.RowCount == 1)
            {
                MessageBox.Show("Ban chua chon vaccine!");
            }
            else
            {
                //Tao MaDonMua
                string q = "select top 1 MADON from DONMUAVC order by MADON desc";
                string DonMua = "";
                SqlCommand com = new SqlCommand(q, Menu_KH.con);
                using (SqlDataReader read = com.ExecuteReader())
                {
                    while (read.Read())
                    {
                        DonMua = NV_ThanhToan.increment((read["MADON"].ToString()), "DM");
                    }
                }

                //Them DonMuaVC
                q = "INSERT INTO DONMUAVC (MADON, MAKH, NGAYDATMUA) " +
                    "VALUES (@MADON, @MAKH, @NGAYDATMUA)";

                using (SqlCommand command = new SqlCommand(q, Menu_KH.con))
                {
                    command.Parameters.AddWithValue("@MADON", DonMua);
                    command.Parameters.AddWithValue("@MAKH", Menu_KH.MaKH);
                    command.Parameters.AddWithValue("@NGAYDATMUA", textBox_Date.Text);

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

                //Them chi tiet don mua
                if (comboBox1.Text == "Tiêm theo gói")
                {
                    for (int rows = 0; rows < ChonVaccine_Nhom.dgv_Nhom_Public.Rows.Count - 1; rows++)
                    {
                        try
                        {
                            string sql = "USP_Add_CT_DMVC";
                            SqlCommand cmd = new SqlCommand(sql, Menu_KH.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "@MADON",
                                SqlDbType = SqlDbType.Char,
                                Value = DonMua
                            });
                            cmd.Parameters.Add(new SqlParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "@MAVC",
                                SqlDbType = SqlDbType.Char,
                                Value = ChonVaccine_Nhom.dgv_Nhom_Public.Rows[rows].Cells[0].Value.ToString()
                            });
                            cmd.Parameters.Add(new SqlParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "@SOLUONG",
                                SqlDbType = SqlDbType.Int,
                                Value = 1
                            });
                            cmd.Parameters.Add(new SqlParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "@GIA",
                                SqlDbType = SqlDbType.Decimal,
                                Value = Convert.ToDouble(ChonVaccine_Nhom.dgv_Nhom_Public.Rows[rows].Cells[1].Value)
                            });

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }

                else if (comboBox1.Text == "Tiêm lẻ")
                {
                    for (int rows = 0; rows < ChonVaccine_Le.dgv_Le_Public.Rows.Count - 1; rows++)
                    {
                        try
                        {
                            string sql = "USP_Add_CT_DMVC";
                            SqlCommand cmd = new SqlCommand(sql, Menu_KH.con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "@MADON",
                                SqlDbType = SqlDbType.Char,
                                Value = DonMua
                            });
                            cmd.Parameters.Add(new SqlParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "@MAVC",
                                SqlDbType = SqlDbType.Char,
                                Value = ChonVaccine_Le.dgv_Le_Public.Rows[rows].Cells[0].Value.ToString()
                            });
                            cmd.Parameters.Add(new SqlParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "@SOLUONG",
                                SqlDbType = SqlDbType.Int,
                                Value = Convert.ToDouble(ChonVaccine_Le.dgv_Le_Public.Rows[rows].Cells[1].Value)
                            });
                            cmd.Parameters.Add(new SqlParameter()
                            {
                                Direction = ParameterDirection.Input,
                                ParameterName = "@GIA",
                                SqlDbType = SqlDbType.Decimal,
                                Value = Convert.ToDouble(ChonVaccine_Le.dgv_Le_Public.Rows[rows].Cells[2].Value)
                            });

                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }

                //Generate MaPhieuDK
                q = "select top 1 MAPDKTIEM from PHIEUDKTIEM order by MAPDKTIEM desc";
                string MaPDKTiem = "";
                com = new SqlCommand(q, Menu_KH.con);
                using (SqlDataReader read = com.ExecuteReader())
                {
                    while (read.Read())
                    {
                        MaPDKTiem = NV_ThanhToan.increment((read["MAPDKTIEM"].ToString()), "DK");
                    }
                }

                //Them PhieuDK
                q = "INSERT INTO PHIEUDKTIEM VALUES (@MAPDKTIEM, @MAKH, @DIACHI, @GIOITINH, @NGAYTIEM, " +
                    "@HOTEN, @SDT, @HOTEN_NGH, @SDT_NGH, @MQH, @MADON)";
                using (SqlCommand command = new SqlCommand(q, Menu_KH.con))
                {
                    command.Parameters.AddWithValue("@MAPDKTIEM", MaPDKTiem);
                    command.Parameters.AddWithValue("@MAKH", Menu_KH.MaKH);
                    command.Parameters.AddWithValue("@DIACHI", textBox4.Text);
                    command.Parameters.AddWithValue("@GIOITINH", "0");
                    command.Parameters.AddWithValue("@NGAYTIEM", textBox_Date.Text);
                    command.Parameters.AddWithValue("@HOTEN", textBox3.Text);
                    command.Parameters.AddWithValue("@SDT", textBox5.Text);
                    command.Parameters.AddWithValue("@HOTEN_NGH", textBox_Ten.Text);
                    command.Parameters.AddWithValue("@SDT_NGH", textBox_.Text);
                    command.Parameters.AddWithValue("@MQH", textBox1.Text);
                    command.Parameters.AddWithValue("@MADON", DonMua);

                    try
                    {
                        command.ExecuteNonQuery();
                        MessageBox.Show("Dang ky thanh cong");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                //Them CT_PhieuDK
                q = "INSERT INTO CT_PHIEUVC VALUES (@MAPDKTIEM, @MAVC, @NGAYTIEM)";

                if (comboBox1.Text == "Tiêm theo gói")
                {
                    for (int rows = 0; rows < ChonVaccine_Nhom.dgv_Nhom_Public.Rows.Count - 1; rows++)
                    {
                        using (SqlCommand command = new SqlCommand(q, Menu_KH.con))
                        {
                            command.Parameters.AddWithValue("@MAPDKTIEM", MaPDKTiem);
                            command.Parameters.AddWithValue("@MAVC", ChonVaccine_Nhom.dgv_Nhom_Public.Rows[rows].Cells[0].Value.ToString());
                            command.Parameters.AddWithValue("@NGAYTIEM", textBox_Date.Text);
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }

                if (comboBox1.Text == "Tiêm lẻ")
                {
                    for (int rows = 0; rows < ChonVaccine_Le.dgv_Le_Public.Rows.Count - 1; rows++)
                    {
                        using (SqlCommand command = new SqlCommand(q, Menu_KH.con))
                        {
                            command.Parameters.AddWithValue("@MAPDKTIEM", MaPDKTiem);
                            command.Parameters.AddWithValue("@MAVC", ChonVaccine_Le.dgv_Le_Public.Rows[rows].Cells[0].Value.ToString());
                            command.Parameters.AddWithValue("@NGAYTIEM", textBox_Date.Text);
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}
