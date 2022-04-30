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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            panel_Title.BackColor = System.Drawing.ColorTranslator.FromHtml("#2c6975");
            label_Exit.BackColor = System.Drawing.ColorTranslator.FromHtml("#E45046");
            label_Exit.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            label2.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            label1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#205072");
            button1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            button1.BackColor = System.Drawing.ColorTranslator.FromHtml("#329d9c");
            
        }

        private void label_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "Username")
                textBox1.Clear();
            textBox1.TextAlign = HorizontalAlignment.Left;
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Username";
                textBox1.ForeColor = Color.Gray;
            }
            textBox1.TextAlign = HorizontalAlignment.Center;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (username == "" || password == "")
            {
                MessageBox.Show("!");
            }
            else
            {
                string conString = "Data Source=FINN\\SQLEXPRESS;Initial Catalog=QLTC;Integrated Security=True";
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                try
                {
                    string sql = "USP_Login";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        Direction = ParameterDirection.Input,
                        ParameterName = "@USRNAME",
                        SqlDbType = SqlDbType.Char,
                        Value = username
                    });
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        Direction = ParameterDirection.Input,
                        ParameterName = "@PASSWRD",
                        SqlDbType = SqlDbType.Char,
                        Value = password
                    });
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        Direction = ParameterDirection.Output,
                        ParameterName = "@ACC_TYPE",
                        SqlDbType = SqlDbType.Int,
                    });

                    cmd.ExecuteNonQuery();

                    int acc_type = (int)cmd.Parameters["@ACC_TYPE"].Value;

                    switch (acc_type)
                    {
                        case 1:
                            {
                                MessageBox.Show("Khách hàng đăng nhập thành công");
                                this.Hide();
                                var form_KH = new Menu_KH();
                                form_KH.Closed += (s, args) => this.Close();
                                form_KH.Show();
                                break;
                            }
                        case 2:
                            {
                                MessageBox.Show("Nhân viên đăng nhập thành công");
                                this.Hide();
                                var form_KH = new Menu_NV();
                                form_KH.Closed += (s, args) => this.Close();
                                form_KH.Show();
                                break;
                            }
                        case 3:
                            {
                                MessageBox.Show("Quản lý đăng nhập thành công");
                                this.Hide();
                                var form_KH = new Menu_QL();
                                form_KH.Closed += (s, args) => this.Close();
                                form_KH.Show();
                                break;
                            }
                        case -1:
                            {
                                MessageBox.Show("Không hợp lệ, vui lòng nhập lại");
                                break;
                            }
                        default:
                            {

                                break;
                            }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }                
            }
        }
    }
}
