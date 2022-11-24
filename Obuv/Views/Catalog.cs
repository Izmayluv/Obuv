using Obuv.Classes;
using Obuv.Properties;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Obuv.Views
{
    public partial class Catalog : Form
    {
        public Catalog()
        {
            InitializeComponent();
        }

        private void Catalog_Load(object sender, EventArgs e)
        {
            labelUserName.Text = Authorization.GetUserName + " ";
            labelUserName.Text += Authorization.GetUserPatronymic + " ";
            labelUserName.Text += Authorization.GetUserSurname + "\n";
            labelUserName.Text += Authorization.GetUserRole;

            comboBoxCategories.Items.Add("Все категории");
            var categories = Helper.DbContext.Categories.Select(x => x.categoryName).ToList();
            foreach (var category in categories)
            {
                comboBoxCategories.Items.Add(category);
            }

            comboBoxSort.Items.Add("По возрастанию");
            comboBoxSort.Items.Add("По убыванию");

            comboBoxDiscount.Items.Add("Не учитывается");
            comboBoxDiscount.Items.Add("От 0 до 10");
            comboBoxDiscount.Items.Add("От 10 до 15");
            comboBoxDiscount.Items.Add("От 15 до 100");

            LoadAllProductsToGrid();

        }

        private Bitmap bitmap;
        private string path = @"C:\Users\pixam\source\repos\Obuv\Obuv\Resources\";
        private string picName;

        private void LoadAllProductsToGrid()
        {
           var products = Helper.DbContext.Products;

            for (int i = 0; i < Helper.DbContext.Products.Count(); i++)
            {
                if (i != Helper.DbContext.Products.Count() - 1)
                {
                    dataGridView1.Rows.Add();
                }

                picName = products.Select(x => x.productPicture).ToArray()[i];

                if (String.IsNullOrEmpty(picName))
                {
                    bitmap = Resources.defPic;
                }

                if (!String.IsNullOrEmpty(picName))
                {
                    bitmap = new Bitmap(path + picName);
                    bitmap = new Bitmap(bitmap, 128, 128);
                }

                dataGridView1.Rows[i].Cells[0].Value = bitmap;

                dataGridView1.Rows[i].Cells[1].Value =
                    $"Наименование: { products.Select(x => x.productName).ToArray()[i] }\n" +
                    $"Описание: {products.Select(x => x.productDescription).ToArray()[i]}\n" +
                    $"Цена: { Round(products.Select(x => x.productCost).ToArray()[i], 0) }\n" +
                    $"Скидка: { Round((decimal)products.Select(x => x.productActiveDiscountAmount).ToArray()[i],0) }\n" +
                    $"Цена со скидкой: { Round((decimal)(products.Select(x => x.productCost).ToArray()[i] - products.Select(x => x.productCost).ToArray()[i] * products.Select(x => x.productActiveDiscountAmount).ToArray()[i] / 100), 0) }\n";
            }
            PrintCountOfRows();
        }

        private void PrintCountOfRows()
        {
            labelProdCount.Text = $"{dataGridView1.RowCount} из {Helper.DbContext.Products.Count()}";
        }

        private decimal Round(decimal d, int decimals)
        {
            var x = Math.Round(d, decimals);
            return x;
        }

        private void labelUserName_Click(object sender, EventArgs e)
        {

        }
    }
}
