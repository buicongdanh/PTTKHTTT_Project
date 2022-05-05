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
    public partial class TT_DatVaccine : Form
    {
        public TT_DatVaccine()
        {
            InitializeComponent();
        }

        private void TT_DatVaccine_Load(object sender, EventArgs e)
        {
            String q = "select * from DONMUAVC WHERE MAKH = '" + Menu_KH.MaKH + "'";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaDon = dataGridView1.Rows[e.RowIndex].Cells["MaDon"].Value.ToString();
            String q = "select CT_DVC.MaVC, VC.TenVaccine, VC.XuatXu, CT_DVC.SoLuong " +
                        "from DONMUAVC DVC, CT_DMVC CT_DVC, Vaccine VC " +
                        "where DVC.MaDon = CT_DVC.MaDon AND CT_DVC.MaVC = VC.MaVC AND DVC.MaDon = '" + MaDon + "'";

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
}
