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
    public partial class TT_DKTC : Form
    {
        public TT_DKTC()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String q = "select * from PHIEUDKTIEM";
            if (textBox1.Text.Trim().Length > 0)
            {
                q= q + " WHERE MAPDKTIEM ='" + textBox1.Text+"'";
            }
            
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
            catch(Exception ex)
            {
                MessageBox.Show("Error TT_DKTC");
            }
            
        }
    }
}
