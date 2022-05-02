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
        public ChonVaccine_Nhom()
        {
            InitializeComponent();
        }

        private void ChonVaccine_Nhom_Load(object sender, EventArgs e)
        {
            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();

            dataGridView3.ColumnCount = 2;
            dataGridView3.Columns[0].Name = "MaGoi";
            dataGridView3.Columns[1].Name = "TenGoi";
            
            col1.UseColumnTextForButtonValue = true;
            col1.Text = "HUy";
            col1.Name = "";

            dataGridView3.Columns.Add(col1);
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
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {

                try
                {
                    DataGridViewRow dgvR = dataGridView1.Rows[e.RowIndex];
                    dataGridView3.Rows.Add(dgvR.Cells[1].Value, dgvR.Cells[2].Value, dgvR.Cells[0].Value);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                MessageBox.Show("Chọn vaccine " + dataGridView1.Rows[e.RowIndex].Cells["magoi"].Value.ToString());
            }

            else
            {
                string MaGoi = dataGridView1.Rows[e.RowIndex].Cells["magoi"].Value.ToString();
                String q = "select vc.* from ct_goivc ctg, vaccine vc where ctg.magoi = '" + MaGoi + "' and ctg.mavc = vc.mavc";

                try
                {
                    SqlDataAdapter adp = new SqlDataAdapter(q, Menu_KH.con);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dataGridView2.DataSource = ds.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }
    }
}
