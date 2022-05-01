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
    }
}
