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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

            label4.Text = DateTime.Now.ToString("yyyy/MM/dd");

            dataGridView1.DataSource = Stockmanger.Sales;
            int sum = 0;
            int sum_1 = 0;


            for(int i = 0; i< Stockmanger.Sales.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
            }

            for (int i = 0; i < Stockmanger.Sales.Count; ++i)
            {
                sum_1 += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }
            textBox1.Text = sum.ToString();
            textBox2.Text = sum_1.ToString();
            textBox3.Text = (sum + sum_1).ToString(); 
        }

        private void 이전매출ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form4().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Stockmanger.Records.Exists((x) => x.Date == label4.Text))
                {
                    MessageBox.Show("오늘 정산은 이미 완료되었습니다.");
                }
                else if(textBox3.Text == 0.ToString())
                {
                    if (MessageBox.Show("매출이 0원입니다. 마감하겠습니까?", "YesOrNo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Records records = new Records();
                        {
                            records.Date = label4.Text;
                            records.Card = int.Parse(textBox1.Text);
                            records.Cash = int.Parse(textBox2.Text);
                            records.Total = int.Parse(textBox3.Text);
                        }
                        Stockmanger.Records.Add(records);
                        Stockmanger.Save();

                        while (true)
                        {
                            Sales sales = Stockmanger.Sales.First();
                            Stockmanger.Sales.Remove(sales);
                            Stockmanger.Save();
                            if (Stockmanger.Sales.Count == 0)
                                break;
                        }
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = Stockmanger.Sales;
                        Stockmanger.Save();
                        MessageBox.Show("정산이 완료되었습니다.");
                    }
                }
                else
                {
                    Records records = new Records();
                    {
                        records.Date = label4.Text;
                        records.Card = int.Parse(textBox1.Text);
                        records.Cash = int.Parse(textBox2.Text);
                        records.Total = int.Parse(textBox3.Text);
                    }
                    Stockmanger.Records.Add(records);
                    Stockmanger.Save();

                    while (true)
                    {
                        Sales sales = Stockmanger.Sales.First();
                        Stockmanger.Sales.Remove(sales);
                        Stockmanger.Save();
                        if (Stockmanger.Sales.Count == 0)
                            break;
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = Stockmanger.Sales;
                    Stockmanger.Save();
                    textBox1.Text = "0";
                    textBox2.Text = "0";
                    textBox3.Text = "0";
                    MessageBox.Show("정산이 완료되었습니다.");
                    
                }
        }catch(Exception ex) {}
}
    }
}
