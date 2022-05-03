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
    public partial class TT_DKTiemChung : Form
    {
        public TT_DKTiemChung()
        {
            InitializeComponent();
        }

        private void TT_DKTiemChung_Load(object sender, EventArgs e)
        {
            String q = "select * from PHIEUDKTIEM WHERE MAKH = '" + Menu_KH.MaKH + "'";

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaPDK = dataGridView1.Rows[e.RowIndex].Cells["MaPDKTIEM"].Value.ToString();
            String q = "select ctp.MAPDKTIEM, CTP.MAVC, VC.TENVACCINE, CTP.NGAYTIEM " +
                "from CT_PHIEUVC ctp, vaccine vc where ctp.MAPDKTIEM = '" + MaPDK + "' and ctp.mavc = vc.mavc";

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
