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
            String q = "select * from goivc";

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error TT_DKTC");
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
                    MessageBox.Show("Error");
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
