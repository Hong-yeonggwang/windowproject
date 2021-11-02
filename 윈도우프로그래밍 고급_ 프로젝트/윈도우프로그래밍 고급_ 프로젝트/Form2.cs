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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            dataGridView1.DataSource = Stockmanger.Products;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Products product = dataGridView1.CurrentRow.DataBoundItem as Products;
                textBox1.Text = product.Name;
                textBox2.Text = product.Price.ToString();
                textBox3.Text = product.Life.ToString();
                textBox4.Text = product.Ea.ToString();
            }catch(Exception ex)
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Stockmanger.Products.Exists((x) => x.Name == textBox1.Text))
            {
                MessageBox.Show(" 이미 존재하는 상품입니다.");
            }
            else
            {
                Products products = new Products()
                {
                    Name = textBox1.Text,
                    Price = int.Parse(textBox2.Text),
                    Life = Convert.ToDateTime(textBox3.Text),
                    Ea = int.Parse(textBox4.Text)
                };

                Stockmanger.Products.Add(products);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Stockmanger.Products;
                Stockmanger.Save();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Products products = Stockmanger.Products.Single((x) => x.Name == textBox1.Text);
                Stockmanger.Products.Remove(products);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Stockmanger.Products;
                Stockmanger.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(" 존재하지 않는 상품입니다.상품목록에서 삭제할 상품을 선택해주세요.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Products products = Stockmanger.Products.Single((x) => x.Name == textBox1.Text);
                {
                    products.Price = int.Parse(textBox2.Text);
                    products.Life = Convert.ToDateTime(textBox3.Text);
                    products.Ea = int.Parse(textBox4.Text);
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Stockmanger.Products;
                Stockmanger.Save();
            }catch(Exception ex)
            {
                MessageBox.Show(" 존재하지 않는 상품입니다.");
            }
        }
    }
}
