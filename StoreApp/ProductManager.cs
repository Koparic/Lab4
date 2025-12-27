using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Newtonsoft.Json;

namespace StoreApp
{
    public class ProductManager
    {
        public List<ProductClass> ProductsBase { get; private set; } = new List<ProductClass>();

        public ProductManager()
        {
            LoadProductsBase();
            //ProductsBase.Add(new ProductClass("Куст", "Это необычный куст. Он ночью светится.", 8, "a", 100));
            //ProductsBase.Add(new ProductClass("Куст2","А этот обычный", 8, "a", 200));
            //ProductsBase.Add(new ProductClass("Цветочек", "А это Цветочек", 8, "a", 400));
            //ProductsBase.Add(new ProductClass("Цветок", "А это Цветок", 8, "a", 110));
            //ProductsBase.Add(new ProductClass("Цветище", "А это Цветище", 8, "a", 400));
            //ProductsBase.Add(new ProductClass("Гладиолус", "Потому что Гладиолус", 8, "a", 100000));
            //ProductsBase.Add(new ProductClass("Фикус", "А это фикус", 8, "a", 100000));
        }

        public void AddProduct(ProductClass prod)
        {
            ProductsBase.Add(prod);
        }

        public void UpdateProduct(ProductClass prod)
        {
            ProductsBase[FindIndByName(prod.name)] = prod;
        }

        public int FindIndByName(string name)
        {
            for (int i = 0; i < ProductsBase.Count; i++)
            {
                if (ProductsBase[i].name == name) return i;
            }
            return -1;
        }

        public void SaveProductsBase()
        {
            string filePath = "productsBase.json";
            using (StreamWriter writer = File.CreateText(filePath))
            {
                string json = JsonConvert.SerializeObject(ProductsBase, Newtonsoft.Json.Formatting.Indented);
                writer.Write(json);
            }
        }

        public void LoadProductsBase()
        {
            string filePath = "productsBase.json";
            if (File.Exists(filePath))
            {
                using (StreamReader reader = File.OpenText(filePath))
                {
                    string json = reader.ReadToEnd();
                    ProductsBase.Clear();
                    try
                    {
                        ProductsBase = JsonConvert.DeserializeObject<List<ProductClass>>(json);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Заказы не смогли загрузиться " + ex.Message);
                    }
                }
            }
        }

        public Image GetImage(string name)
        {
            object obj = Properties.Resources.ResourceManager.GetObject(name);
            if (obj is Image)
            {
                return (Image)obj;
            }
            else
            {
                return Properties.Resources.Flower;
            }
        }
    }
}
