using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetDataMysql
{
    public partial class Test : Form
    {
        int _id;
        int id;
        private int newId;
        public int NewId
        {
            set { newId = value; }
        }
        public Test(int id)
        {
            _id = id;
            this.id = id;
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            label1.Text = _id.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
