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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            label7.Text = DateTime.Now.ToString("yyyy/MM/dd");
            dataGridView1.DataSource = Stockmanger.Products;
            dataGridView2.DataSource = Stockmanger.Carts;

            int sum = 0;
            for (int i = 0; i < Stockmanger.Carts.Count; ++i)
            {
                sum += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value);
            }
            textBox4.Text = sum.ToString();
        }

        private void 상품관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form2().ShowDialog();
        }

        private void 매출관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form3().ShowDialog();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                Products product = dataGridView1.CurrentRow.DataBoundItem as Products;
                textBox1.Text = product.Name;
                textBox2.Text = product.Price.ToString();
                textBox3.Text = 1.ToString();
                textBox5.Text = product.Life.ToString();
            }
            catch (Exception ex) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Stockmanger.Carts.Exists((x) => x.Name == textBox1.Text))
                {
                    Carts carts = Stockmanger.Carts.Single((x) => x.Name == textBox1.Text);
                    Products products = Stockmanger.Products.Single((x) => x.Name == textBox1.Text);
                    {
                        if (products.Ea >= int.Parse(textBox3.Text))
                        {
                            products.Ea -= int.Parse(textBox3.Text);
                            carts.Ea += int.Parse(textBox3.Text);
                            carts.Total = carts.Price * carts.Ea;
                        }
                        else if (products.Ea <= int.Parse(textBox3.Text))
                            MessageBox.Show("재고가 모자릅니다.");
                    }
                   
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = Stockmanger.Products;
                    Stockmanger.Save();
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = Stockmanger.Carts;
                    Stockmanger.Save();

                    int sum = 0;
                    for (int i = 0; i < Stockmanger.Carts.Count; ++i)
                    {
                        sum += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value);
                    }
                    textBox4.Text = sum.ToString();
                }
                else
                {
                    Carts carts = new Carts();
                    Products products = Stockmanger.Products.Single((x) => x.Name == textBox1.Text);
                    if (products.Ea >= int.Parse(textBox3.Text))
                    {
                        carts.Name = textBox1.Text;
                        carts.Price = int.Parse(textBox2.Text);
                        carts.Ea = int.Parse(textBox3.Text);
                        carts.Life = Convert.ToDateTime(textBox5.Text);
                        carts.Total = carts.Price * carts.Ea;
                        products.Ea -= int.Parse(textBox3.Text);
                        Stockmanger.Carts.Add(carts);
                    }
                    else if (products.Ea <= int.Parse(textBox3.Text))
                    {
                        MessageBox.Show(" 해당 상품의 재고가 모자릅니다.");
                    }
                   
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = Stockmanger.Products;
                    Stockmanger.Save();
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = Stockmanger.Carts;
                    Stockmanger.Save();

                    int sum = 0;
                    for (int i = 0; i < Stockmanger.Carts.Count; ++i)
                    {
                        sum += Convert.ToInt32(dataGridView2.Rows[i].Cells[3].Value);
                    }
                    textBox4.Text = sum.ToString();
                }

            }catch (Exception ex)
            {
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("상품의 정보가 불충분합니다. 상품목록에서 물품을 선택해주세요.");
                }
                else if (textBox2.Text.Trim() == "")
                {
                    MessageBox.Show("상품의 정보가 불충분합니다. 상품목록에서 물품을 선택해주세요.");
                }
                else if (textBox3.Text.Trim() == "")
                {
                    MessageBox.Show(" 구매 개수를 입력해주세요.");
                }
                else if(textBox5.Text.Trim() == "")
                {
                    MessageBox.Show(" 상품의 정보가 불충분합니다. 상품목록에서 물품을 선택해주세요.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {                
                Carts carts = Stockmanger.Carts.Single((x) => x.Name == textBox1.Text );
                Products products = Stockmanger.Products.Single((x) => x.Name == textBox1.Text);
                {
                    products.Ea += carts.Ea;
                    textBox4.Text = Convert.ToString(Convert.ToInt32(textBox4.Text) - carts.Price * carts.Ea);
                }
                Stockmanger.Carts.Remove(carts);

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = Stockmanger.Products;
                Stockmanger.Save();
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = Stockmanger.Carts;
                Stockmanger.Save();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(" 존재하지 않는 상품입니다. 장바구니에서 삭제할 상품을 선택해주세요.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Stockmanger.Records.Exists((x) => x.Date == label7.Text))
                {
                    MessageBox.Show("오늘 정산은 이미 완료되었습니다.");
                }

                else if (Stockmanger.Carts.Exists((x) => x.Life < DateTime.Now))
                {
                    MessageBox.Show("유통기한이 지난 상품이 있습니다.");
                }
                else
                {
                    for (int j = 0; j < Stockmanger.Carts.Count; ++j)
                    {
                        if (Stockmanger.Sales.Exists((x) => x.Name == Convert.ToString(dataGridView2.Rows[j].Cells[0].Value)))
                        {
                            Sales sales = Stockmanger.Sales.Single((x) => x.Name == Convert.ToString(dataGridView2.Rows[j].Cells[0].Value));
                            {
                                sales.Ea += Convert.ToInt32(dataGridView2.Rows[j].Cells[2].Value);
                                sales.Cash += Convert.ToInt32(dataGridView2.Rows[j].Cells[3].Value);
                            }
                        }
                        else
                        {
                            Sales sales = new Sales();
                            {
                                sales.Name = Convert.ToString(dataGridView2.Rows[j].Cells[0].Value);
                                sales.Price = Convert.ToInt32(dataGridView2.Rows[j].Cells[1].Value);
                                sales.Ea = Convert.ToInt32(dataGridView2.Rows[j].Cells[2].Value);
                                sales.Cash = Convert.ToInt32(dataGridView2.Rows[j].Cells[3].Value);
                            }
                            Stockmanger.Sales.Add(sales);
                            Stockmanger.Save();
                        }
                    }
                    while (true)
                    {
                        Carts carts = Stockmanger.Carts.Single((x) => x.Name == Convert.ToString(dataGridView2.Rows[0].Cells[0].Value));
                        Stockmanger.Carts.Remove(carts);
                        Stockmanger.Save();
                        if (Stockmanger.Carts.Count == 0)
                            break;
                    }
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = Stockmanger.Carts;
                    Stockmanger.Save();
                    textBox4.Text = "0";
                    MessageBox.Show("현금결제가 완료되었습니다.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("장바구니가 비어있습니다.");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Trim() != "")
            {
                textBox7.Text = Convert.ToString(Convert.ToInt32(textBox6.Text) - Convert.ToInt32(textBox4.Text));

                if (int.Parse(textBox7.Text) < 0)
                {
                    textBox7.Text = " ...";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Stockmanger.Records.Exists((x) => x.Date == label7.Text))
                {
                    MessageBox.Show("오늘 정산은 이미 완료되었습니다.");
                }
                else if (Stockmanger.Carts.Exists((x) => x.Life < DateTime.Now))
                {
                    MessageBox.Show("유통기한이 지난 상품이 있습니다.");
                }
                else
                {
                    for (int j = 0; j < Stockmanger.Carts.Count; ++j)
                    {
                        if (Stockmanger.Sales.Exists((x) => x.Name == Convert.ToString(dataGridView2.Rows[j].Cells[0].Value)))
                        {
                            Sales sales = Stockmanger.Sales.Single((x) => x.Name == Convert.ToString(dataGridView2.Rows[j].Cells[0].Value));
                            {
                                sales.Ea += Convert.ToInt32(dataGridView2.Rows[j].Cells[2].Value);
                                sales.Card += Convert.ToInt32(dataGridView2.Rows[j].Cells[3].Value);
                            }
                        }
                        else
                        {
                            Sales sales = new Sales();
                            {
                                sales.Name = Convert.ToString(dataGridView2.Rows[j].Cells[0].Value);
                                sales.Price = Convert.ToInt32(dataGridView2.Rows[j].Cells[1].Value);
                                sales.Ea = Convert.ToInt32(dataGridView2.Rows[j].Cells[2].Value);
                                sales.Card = Convert.ToInt32(dataGridView2.Rows[j].Cells[3].Value);
                            }
                            Stockmanger.Sales.Add(sales);
                            Stockmanger.Save();
                        }
                    }
                    while (true)
                    {
                        Carts carts = Stockmanger.Carts.Single((x) => x.Name == Convert.ToString(dataGridView2.Rows[0].Cells[0].Value));
                        Stockmanger.Carts.Remove(carts);
                        Stockmanger.Save();
                        if (Stockmanger.Carts.Count == 0)
                            break;
                    }
                    dataGridView2.DataSource = null;
                    dataGridView2.DataSource = Stockmanger.Carts;
                    Stockmanger.Save();
                    textBox4.Text = "0";
                    MessageBox.Show("카드 결제가 완료되었습니다.");

                }
            }catch (Exception ex)
            {
                MessageBox.Show("장바구니가 비어있습니다.");
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Stockmanger.Products;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Carts carts = dataGridView2.CurrentRow.DataBoundItem as Carts;
                textBox1.Text = carts.Name;
                textBox2.Text = carts.Price.ToString();
                textBox5.Text = Convert.ToString(carts.Life);
            }
            catch (Exception ex) { }

        }
    }
}
