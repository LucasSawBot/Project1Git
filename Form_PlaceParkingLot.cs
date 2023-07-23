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
    public partial class Form_PlaceParkingLot : Form
    {
        public Form_PlaceParkingLot()
        {
            InitializeComponent();
        }

        public List<string> listtileblock { get; set; }
        public List<string> listBlock { get; set; }
        string strItemBlock;
        string strFamily;

        public string ItemBlock
        {
            get { return strItemBlock; }
            set { strItemBlock = value; }
        }

        public string ItemFamilyInstance
        {
            get { return strFamily; }
            set { strFamily = value; }
        }

        private void Form_PlaceParkingLot_Load(object sender, EventArgs e)
        {
            cbFamily.DataSource = listtileblock;
            cbBlockCad.DataSource = listBlock;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        //private void cbFamily_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    strFamily = cbFamily.Text;
        //}

        //private void cbBlockCad_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    strItemBlock = cbBlockCad.Text;
        //}

        private void cbBlockCad_SelectedValueChanged(object sender, EventArgs e)
        {
            strItemBlock = cbBlockCad.Text;

        }

        private void cbFamily_SelectedValueChanged(object sender, EventArgs e)
        {
            strFamily = cbFamily.Text;
        }
    }
}
