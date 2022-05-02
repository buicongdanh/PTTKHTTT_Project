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
        //public string MAPDKTIEM;
        
        
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
                foreach (DataGridViewColumn dgvCol in dataGridView1.Columns)
                {
                    dgvCol.ReadOnly = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MAPDKTIEM = dataGridView1.Rows[e.RowIndex].Cells["MAPDKTIEM"].FormattedValue.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                String query = "Update PHIEUDKTIEM set NGAYTIEM=CONVERT(datetime,'";
                for (int item = 0; item < dataGridView1.Rows.Count - 1; item++)
                {
                    String temp = dataGridView1.Rows[item].Cells[4].Value.ToString();
                    String temp2 = dataGridView1.Rows[item].Cells[0].Value.ToString();
                    query = query + temp + "') where MAPDKTIEM = '" + temp2 + "'";
                    using (SqlCommand command = new SqlCommand(query, Menu_NV.con))
                    {
                        command.ExecuteNonQuery();
                    }

                    if (item == dataGridView1.Rows.Count - 2)
                    {
                        MessageBox.Show("Cap nhat thanh cong");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void TT_DKTC_Load(object sender, EventArgs e)
        {
            String q = "select * from PHIEUDKTIEM";
            if (textBox1.Text.Trim().Length > 0)
            {
                q = q + " WHERE MAPDKTIEM ='" + textBox1.Text + "'";
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
    }
}
