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
    public partial class ChonVaccine_Le : Form
    {
        public static DataGridView dgv_Le_Public = new DataGridView();
        public static double tongtien = 0;

        public ChonVaccine_Le()
        {
            InitializeComponent();
        }

        private void ChonVaccine_Le_Load(object sender, EventArgs e)
        {
            dgv_Le_Public.Rows.Clear();
            dgv_Le_Public.Refresh();

            dataGridView2.ColumnCount = 4;
            dataGridView2.Columns[0].Name = "MaVC";
            dataGridView2.Columns[1].Name = "TenVC";
            dataGridView2.Columns[2].Name = "SoLuong";
            dataGridView2.Columns[3].Name = "Gia";

            dgv_Le_Public.ColumnCount = 3;
            dgv_Le_Public.Columns[0].Name = "MaVC";
            dgv_Le_Public.Columns[1].Name = "SoLuong";
            dgv_Le_Public.Columns[2].Name = "Gia";

            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();
            col1.UseColumnTextForButtonValue = true;
            col1.Text = "Huy";
            col1.Name = "";
            dataGridView2.Columns.Add(col1);

            String q = "select * from vaccine";

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_KH.con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];

                    DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                    col.UseColumnTextForButtonValue = true;
                    col.Text = "Chọn";
                    col.Name = "";
                    dataGridView1.Columns.Add(col);
                }

                foreach (DataGridViewColumn dgvCol in dataGridView1.Columns)
                {
                    dgvCol.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                int soluong = 0;
                using(NhapSoLuong f_NhapSoLuong = new NhapSoLuong())
                {
                    if(f_NhapSoLuong.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        soluong = f_NhapSoLuong.soluong;
                        if (soluong <= 0 || soluong > int.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()))
                        {
                            MessageBox.Show("So luong khong hop le");
                        }
                        else
                        {
                            try
                            {
                                DataGridViewRow dgvR = dataGridView1.Rows[e.RowIndex];
                                dataGridView2.Rows.Add(dgvR.Cells[1].Value, dgvR.Cells[2].Value, soluong.ToString(), dgvR.Cells[5].Value);
                                dgv_Le_Public.Rows.Add(dgvR.Cells[1].Value, soluong.ToString(), dgvR.Cells[5].Value);

                                double dongia = Convert.ToDouble(dgvR.Cells[5].Value.ToString());
                                tongtien += dongia;
                                label4.Text = "Gia: " + tongtien.ToString();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                dataGridView2.Refresh();
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn) //&& e.RowIndex >= 0)
            {
                try
                {
                    double soluong, dongia;
                    soluong = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString());
                    dongia = Convert.ToDouble(dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString());

                    tongtien -= soluong * dongia;

                    label4.Text = "Gia: " + tongtien.ToString();
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                    dgv_Le_Public.Rows.RemoveAt(e.RowIndex);
                    //dataGridView2.Rows.RemoveAt(dataGridView2.SelectedRows[e.RowIndex].Index);

                    DataGridViewRow dgvR = dataGridView2.Rows[e.RowIndex];

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    dataGridView2.Refresh();
                }
            }
        }
    }
}
