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
            DataGridViewButtonColumn col1 = new DataGridViewButtonColumn();

            dataGridView2.ColumnCount = 3;
            dataGridView2.Columns[0].Name = "MaVC";
            dataGridView2.Columns[1].Name = "TenVC";
            dataGridView2.Columns[2].Name = "SoLuong";

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
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                if (textBox1.Text == "" || int.Parse(textBox1.Text) <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng Vaccine");

                }
                else
                {
                    try
                    {
                        DataGridViewRow dgvR = dataGridView1.Rows[e.RowIndex];
                        dataGridView2.Rows.Add(dgvR.Cells[1].Value, dgvR.Cells[2].Value, textBox1.Text);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        textBox1.Text = "";
                    }
                } 
            }
        }
    }
}
