using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PTTKHTTT_Project
{
    public partial class DatMuaVaccine : Form
    {
        public string MaKH;
        public DatMuaVaccine()
        {
            InitializeComponent();
        }

        private void DatMuaVaccine_Load(object sender, EventArgs e)
        {
            Form childForm = new ChonVaccine_Le();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_Childform.Controls.Add(childForm);
            panel_Childform.Tag = childForm;
            childForm.Show();
        }
    }
}
