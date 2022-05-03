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
    public partial class PheDuyetDonDat : Form
    {
        public PheDuyetDonDat()
        {
            InitializeComponent();
        }

        public string MaDS = "";

        private void PheDuyetDonDat_Load(object sender, EventArgs e)
        {
            String q = "select * from DSDATVACCINE";

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

        private void button1_Click(object sender, EventArgs e)
        {
            //phe duyet
            if(MaDS == "")
            {
                MessageBox.Show("Hay chon danh sach de duyet");
            }
            else
            {
                String q = "UPDATE DSDATVACCINE SET DUYET = 1, LyDo = 'Du so luong yeu cau'" +
                    " WHERE MADS = '" + MaDS + "'";

                using (SqlCommand cmd = new SqlCommand(q, Menu_QL.con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Updated");
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            String q2 = "select * from DSDATVACCINE";

                            try
                            {
                                SqlDataAdapter adp = new SqlDataAdapter(q2, Menu_NV.con);
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
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MaDS == "")
            {
                MessageBox.Show("Hay chon danh sach de duyet");
            }
            else
            {
                String q = "UPDATE DSDATVACCINE SET DUYET = 0, LyDo = 'Khong du so luong' " +
                            "WHERE MADS = '" + MaDS + "'";

                using (SqlCommand cmd = new SqlCommand(q, Menu_QL.con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Updated");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            String q2 = "select * from DSDATVACCINE";

                            try
                            {
                                SqlDataAdapter adp = new SqlDataAdapter(q2, Menu_NV.con);
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
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MaDS = dataGridView1.Rows[e.RowIndex].Cells["MaDS"].Value.ToString();
        }
    }
}
