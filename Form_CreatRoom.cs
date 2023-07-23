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
    public partial class Form_CreatRoom : Form
    {
        public Form_CreatRoom()
        {
            InitializeComponent();
        }

        public bool checkFloor
        {
            get { return check; }
            set { value = check; }
        }
        bool check;

        public List<string> listWallType { get; set; }

        string strItemWallType;
        public string ItemWallType
        {
            get { return strItemWallType; }
            set { strItemWallType = value; }
        }
        public List<string> listFloorType { get; set; }
        string StrFloorType;
        public string ItemFloorType
        {
            get { return StrFloorType; }
            set { StrFloorType = value; }
        }

        public List<string> listCelingType { get; set; }
        string strCeilingType;
        public string ItemCeilingType
        {
            get { return strCeilingType; }
            set { strCeilingType = value; }
        }


        double hightRoom1=2500;
        
        

        private void Form_CreatRoom_Load(object sender, EventArgs e)
        {
            check = cbUseFloorsModel.Checked;
            strItemWallType = CbWallType.Text;
            StrFloorType = CbFloorType.Text;
            strCeilingType = cbCeilingType.Text;

            CbWallType.DataSource = listWallType;
            CbFloorType.DataSource = listFloorType;
            cbCeilingType.DataSource = listCelingType;
            hightRoom1 = double.Parse(tbHighRoom.Text);
        }

        private void btnCreat_Click(object sender, EventArgs e)
        {
            check = cbUseFloorsModel.Checked;
            strItemWallType = CbWallType.Text;
            StrFloorType = CbFloorType.Text;
            strCeilingType = cbCeilingType.Text;

            CbWallType.DataSource = listWallType;
            CbFloorType.DataSource = listFloorType;
            cbCeilingType.DataSource = listCelingType;
            hightRoom1 = double.Parse(tbHighRoom.Text);
            DialogResult = DialogResult.OK;
        }

        public double hightRoom
        {
            get { return hightRoom1; }
            set { hightRoom1 = value; }
        }
    }
}
