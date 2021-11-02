using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace 윈도우프로그래밍_고급__프로젝트
{
    class Stockmanger
    {
        public static List<Products> Products = new List<Products>();
        public static List<Sales> Sales = new List<Sales>();
        public static List<Carts> Carts = new List<Carts>();
        public static List<Records> Records = new List<Records>();

        static Stockmanger()
        {
            Load();
        }
        public static void Load()
        {
            try
            {
                string Productoutput = File.ReadAllText(@"./Products.xml");
                XElement ProductXElement = XElement.Parse(Productoutput);

                Products = (from item in ProductXElement.Descendants("product")
                            select new Products()
                            {
                                Name = item.Element("name").Value,
                                Life = DateTime.Parse(item.Element("life").Value),
                                Price = int.Parse(item.Element("price").Value),
                                Ea = int.Parse(item.Element("ea").Value),


                            }).ToList<Products>(); 
                string SalesOutPut = File.ReadAllText(@"./Sales.xml");
                XElement SalesXElement = XElement.Parse(SalesOutPut);

                Sales = (from item in SalesXElement.Descendants("sale")
                            select new Sales()
                            {
                                Name = item.Element("name").Value,
                                Price = int.Parse(item.Element("price").Value),
                                Ea = int.Parse(item.Element("ea").Value),
                                Cash = int.Parse(item.Element("cash").Value),
                                Card = int.Parse(item.Element("card").Value),
                            }).ToList<Sales>();

                string CartsOutPut = File.ReadAllText(@"./Carts.xml");
                XElement CartsXElement = XElement.Parse(CartsOutPut);

                Carts = (from item in CartsXElement.Descendants("cart")
                         select new Carts()
                         {
                             Name = item.Element("name").Value,
                             Price = int.Parse(item.Element("price").Value),
                             Ea = int.Parse(item.Element("ea").Value),
                             Life = DateTime.Parse(item.Element("life").Value),
                             Total = int.Parse(item.Element("total").Value),
                         }).ToList<Carts>();

                string Recordsoutput = File.ReadAllText(@"./Records.xml");
                XElement RecordsXElement = XElement.Parse(Recordsoutput);

                Records = (from item in RecordsXElement.Descendants("record")
                            select new Records()
                            {
                                Date = item.Element("date").Value,
                                Card = int.Parse(item.Element("card").Value),
                                Cash = int.Parse(item.Element("cash").Value),
                                Total = int.Parse(item.Element("total").Value),
                            }).ToList<Records>();


            }
            catch(Exception ex)
            {
                Save();
            }           
        }
        
        public static void Save()
        {

                string productoutput = "";
                productoutput += "<Products>\n";
                foreach (var item in Products)
                {
                    productoutput += "<product>\n";
                    productoutput += "  <name>" + item.Name + "</name>\n";
                    productoutput += "<life>" + item.Life + "</life>\n";
                    productoutput += "<price>" + item.Price + "</price>\n";
                    productoutput += "<ea>" + item.Ea + "</ea>\n";
                    productoutput += "</product>\n";
                }
                productoutput += "</Products>\n";


                string saleoutput = "";
                saleoutput += "<Sales>\n";
                foreach (var item in Sales)
                {
                    saleoutput += "<sale>\n";
                    saleoutput += "  <name>" + item.Name + "</name>\n";
                    saleoutput += "  <price>" + item.Price + "</price>\n";
                saleoutput += "  <ea>" + item.Ea + "</ea>\n";
                saleoutput += "  <cash>" + item.Cash + "</cash>\n";
                    saleoutput += "  <card>" + item.Card + "</card>\n";
                    saleoutput += "</sale>\n";
                }
                saleoutput += "</Sales>\n";

                string cartoutput = "";
                cartoutput += "<Carts>\n";
                foreach (var item in Carts)
                {
                    cartoutput += "<cart>\n";
                    cartoutput += "  <name>" + item.Name + "</name>\n";
                    cartoutput += "  <price>" + item.Price + "</price>\n";
                    cartoutput += "  <ea>" + item.Ea + "</ea>\n";
                    cartoutput += "  <life>" + item.Life + "</life>\n";
                    cartoutput += "  <total>" + item.Total + "</total>\n";

                cartoutput += "</cart>\n";
                }
                cartoutput += "</Carts>\n";

                string recordsoutput = "";
                recordsoutput += "<Records>\n";
                foreach (var item in Records)
                {
                    recordsoutput += "<record>\n";
                    recordsoutput += "  <date>" + item.Date + "</date>\n";
                    recordsoutput += "  <cash>" + item.Cash + "</cash>\n";
                    recordsoutput += "  <card>" + item.Card + "</card>\n";
                    recordsoutput += "  <total>" + item.Total + "</total>\n";
                    recordsoutput += "</record>\n";
                }
                recordsoutput += "</Records>\n";

                File.WriteAllText(@"./Products.xml", productoutput);
                File.WriteAllText(@"./Sales.xml", saleoutput);
                File.WriteAllText(@"./Carts.xml", cartoutput);
                File.WriteAllText(@"./Records.xml", recordsoutput);

        }
    }
}
