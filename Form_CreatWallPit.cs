using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoallVietnam
{
    public partial class Form_CreatWallPit : Form
    {
        public Form_CreatWallPit()
        {
            InitializeComponent();
        }

        public List<string> listWall { get; set; }
        string strWall;

        double unconectedHeight3 = 5140;

        


        public string ItemWallType
        {
            get { return strWall; }
            set { strWall = value; }
        }

        private void Form_CreatWallPit_Load(object sender, EventArgs e)
        {
            cbCreatWallWallType.DataSource = listWall;
            unconectedHeight3 = double.Parse(tbCreatWallUnconnectHeight.Text);
            //tbCreatWallUnconnectHeight.Text = unconectedHeight.ToString();
            //MessageBox.Show(unconectedHeight3.ToString());
        }

        private void btCreatWallOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            unconectedHeight3 = double.Parse(tbCreatWallUnconnectHeight.Text);
            

        }

        private void btCreatWallCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cbCreatWallWallType_SelectedIndexChanged(object sender, EventArgs e)
        {
            double unconectedHeight3 = double.Parse(tbCreatWallUnconnectHeight.Text);
            
        }

        private void tbCreatWallUnconnectHeight_TextChanged(object sender, EventArgs e)
        {
            double unconectedHeight3 = double.Parse(tbCreatWallUnconnectHeight.Text);
            
        }

        private void cbCreatWallWallType_SelectedValueChanged(object sender, EventArgs e)
        {
            strWall = cbCreatWallWallType.Text;
            double unconectedHeight3 = double.Parse(tbCreatWallUnconnectHeight.Text);
            
            
        }

        public double unHeight
        {
            get { return unconectedHeight3; }
            set { unconectedHeight3 = value; }
        }
    }
}
