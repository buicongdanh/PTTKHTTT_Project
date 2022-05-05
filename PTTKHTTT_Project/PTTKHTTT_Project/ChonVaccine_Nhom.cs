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
    public partial class ChonVaccine_Nhom : Form
    {
        public static DataGridView dgv_Nhom_Public = new DataGridView();
        public static double tongtien;

        public ChonVaccine_Nhom()
        {
            InitializeComponent();
        }

        private void ChonVaccine_Nhom_Load(object sender, EventArgs e)
        {
            tongtien = 0;

            dgv_Nhom_Public.Rows.Clear();
            dgv_Nhom_Public.Refresh();

            dataGridView3.ColumnCount = 2;
            dataGridView3.Columns[0].Name = "MaGoi";
            dataGridView3.Columns[1].Name = "Thanh Tien";
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv_Nhom_Public.ColumnCount = 3;
            dgv_Nhom_Public.Columns[0].Name = "MaVC";
            dgv_Nhom_Public.Columns[1].Name = "SoLuong";
            dgv_Nhom_Public.Columns[2].Name = "Gia";

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            String q = "select * from goivc";

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_KH.con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                    DataGridViewButtonColumn col2 = new DataGridViewButtonColumn();
                    col2.UseColumnTextForButtonValue = true;
                    col2.Text = "Chọn";
                    col2.Name = "";
                    dataGridView1.Columns.Add(col2);

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaGoi = dataGridView1.Rows[e.RowIndex].Cells["magoi"].Value.ToString();
            String q = "select vc.* from ct_goivc ctg, vaccine vc " +
                "where ctg.magoi = '" + MaGoi + "' and ctg.mavc = vc.mavc";

            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_KH.con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dataGridView2.DataSource = ds.Tables[0];
                    dataGridView2.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {

                try
                {
                    DataGridViewRow dgvR = dataGridView1.Rows[e.RowIndex];
                    dataGridView3.Rows.Add(dgvR.Cells[1].Value, dgvR.Cells[2].Value, dgvR.Cells[0].Value);

                    for (int rows = 0; rows < dataGridView2.Rows.Count - 1; rows++)
                    {
                        dgv_Nhom_Public.Rows.Add(dataGridView2.Rows[rows].Cells[0].Value, "1", 
                            dataGridView2.Rows[rows].Cells[4].Value);
                    }

                    double dongia = Convert.ToDouble(dgvR.Cells[2].Value.ToString());
                    tongtien += dongia;
                    label4.Text = "Gia: " + tongtien.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                MessageBox.Show("Chọn vaccine " + dataGridView1.Rows[e.RowIndex].Cells["magoi"].Value.ToString());
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn) //&& e.RowIndex >= 0)
            {

            }
        }
    }
}
