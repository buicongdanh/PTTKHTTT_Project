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
    public partial class KH_ThanhToan : Form
    {
        public KH_ThanhToan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String query = "UPDATE HOADON SET tinhtrang = 'CHODUYET',HINHTHUC=@HINHTHUC WHERE MAHD=@MAHD AND SOTIEN=@SOTIEN AND HINHTHUC!='HOANTHANH'";
            using (SqlCommand command = new SqlCommand(query, Menu_KH.con))
            {
                string temp = textBox2.Text;
                command.Parameters.AddWithValue("@MAHD", temp);
                temp = textBox6.Text;
                command.Parameters.AddWithValue("@HINHTHUC", temp);
                temp = textBox3.Text;
                command.Parameters.AddWithValue("@SOTIEN", temp);
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Thanh Toan Hoa Don CHO DUYET");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void KH_ThanhToan_Load(object sender, EventArgs e)
        {
            //String q = "SELECT HD.MAHD,DM.MADON,DM.MAKH,DM.NGAYDATMUA,DM.TONGTIEN, HD.tinhtrang, HD.DoTT FROM HOADON HD JOIN DONMUAVC DM ON HD.madon = DM.MADON";
            string q = "SELECT HD.* " +
                "FROM HOADON HD JOIN DONMUAVC DMVC ON (HD.MADON = DMVC.MADON) " +
                "WHERE MAKH ='" + Menu_KH.MaKH + "'";

            if (textBox1.Text.Trim().Length > 0)
            {
                q = q + " AND DM.MADON ='" + textBox1.Text + "'";
            }
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(q, Menu_KH.con);
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
            string MaDon = dataGridView1.Rows[e.RowIndex].Cells["madon"].Value.ToString();
            String q = "select vaccine.tenvaccine, ct_dmvc.soluong, ct_dmvc.gia " +
                "from ct_dmvc, vaccine " +
                "where madon = '" + MaDon + "' and vaccine.mavc = ct_dmvc.mavc";

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
