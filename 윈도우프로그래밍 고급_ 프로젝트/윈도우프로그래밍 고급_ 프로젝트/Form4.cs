using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 윈도우프로그래밍_고급__프로젝트
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            dataGridView1.DataSource = Stockmanger.Records;
        }
    }
}
