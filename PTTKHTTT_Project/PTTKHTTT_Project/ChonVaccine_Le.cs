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
        public ChonVaccine_Le()
        {
            InitializeComponent();
        }

        private void ChonVaccine_Le_Load(object sender, EventArgs e)
        {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error TT_DKTC");
            }
        }
    }
}
