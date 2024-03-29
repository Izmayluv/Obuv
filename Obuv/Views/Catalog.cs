﻿using Obuv.Classes;
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
        public Catalog(string userName, string userPatronymic, string userSurname, string userRole)
        {
            InitializeComponent();

            this.userName = userName;
            this.userPatronymic = userPatronymic;
            this.userSurname = userSurname;
            this.userRole = userRole;
        }

        private void Catalog_Load(object sender, EventArgs e)
        {
            labelUserName.Text = userName + " ";
            labelUserName.Text += userPatronymic + " ";
            labelUserName.Text += userSurname + "\n";
            labelUserName.Text += userRole;

            comboBoxCategories.Items.Add("Не учитывать категорию");
            var categories = Helper.DbContext.Categories.Select(x => x.categoryName).ToList();
            foreach (var category in categories)
            {
                comboBoxCategories.Items.Add(category);
            }

            comboBoxSort.Items.Add("Не учитывать сортировку");
            comboBoxSort.Items.Add("По возрастанию");
            comboBoxSort.Items.Add("По убыванию");

            comboBoxDiscount.Items.Add("Не учитывать скидку");
            comboBoxDiscount.Items.Add("От 0 до 10");
            comboBoxDiscount.Items.Add("От 10 до 15");
            comboBoxDiscount.Items.Add("От 15 до 100");

            LoadProductsToGrid();

        }

        private static Bitmap bitmap;
        public static string path = @"C:\Users\ones\source\repos\gitfolder\Obuv\Resources\";
        private static string picName;
        private string userName;
        private string userPatronymic;
        private string userSurname;
        private string userRole;

        private void LoadProductsToGrid()
        {
            var products = Helper.DbContext.Products.ToList();

            if (textBoxSearch.Text != null)
                products = products.Where(x => x.productName.Contains(textBoxSearch.Text)).ToList();

            switch (comboBoxCategories.SelectedIndex)
            {
                default:
                    break;

                case 1:
                    products = products.Where(x => x.productCategory == 1).ToList();
                    break;

                case 2:
                    products = products.Where(x => x.productCategory == 2).ToList();
                    break;
            }

            switch (comboBoxDiscount.SelectedIndex)
            {
                default:
                    break;

                case 1:
                    products = products.Where(x => x.productActiveDiscountAmount > 0 && x.productActiveDiscountAmount < 3).ToList();
                    break;

                case 2:
                    products = products.Where(x => x.productActiveDiscountAmount > 0 && x.productActiveDiscountAmount < 4).ToList();
                    break;

                case 3:
                    products = products.Where(x => x.productActiveDiscountAmount > 3 && x.productActiveDiscountAmount < 6).ToList();
                    break;
            }

            switch (comboBoxSort.SelectedIndex)
            {
                default:
                    break;

                case 1:
                    products = products.OrderBy(x => x.productCost).ToList();
                    break;

                case 2:
                    products = products.OrderByDescending(x => x.productCost).ToList();
                    break;
            }

            for (int i = 0; i < products.Count(); i++)
            {
                if (i != products.Count() - 1)
                    dataGridView1.Rows.Add();

                picName = products.Select(x => x.productPicture).ToArray()[i];

                if (picName == "" || String.IsNullOrEmpty(picName))
                    picName = "defPic";

                if (!picName.Contains(".jpg"))
                    picName += ".jpg";

                try
                {
                    if (String.IsNullOrEmpty(picName))
                        bitmap = Resources.defPic;

                    if (!String.IsNullOrEmpty(picName))
                    {
                        bitmap = new Bitmap(path + picName);
                        bitmap = new Bitmap(bitmap, 128, 128);
                    }
                }
                catch (Exception)
                {
                    bitmap = Resources.defPic;
                }

                dataGridView1.Rows[i].Cells[0].Value = bitmap;

                dataGridView1.Rows[i].Cells[1].Value =
                    $"Наименование: {products.Select(x => x.productName).ToArray()[i]}\n" +
                    $"Описание: {products.Select(x => x.productDescription).ToArray()[i]}\n" +
                    $"Цена: {Round(products.Select(x => x.productCost).ToArray()[i], 0)}\n" +
                    $"Скидка: {Round((decimal)products.Select(x => x.productActiveDiscountAmount).ToArray()[i], 0)}\n" +
                    $"Цена со скидкой: {Round((decimal)(products.Select(x => x.productCost).ToArray()[i] - products.Select(x => x.productCost).ToArray()[i] * products.Select(x => x.productActiveDiscountAmount).ToArray()[i] / 100), 0)}\n";
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

        private void comboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            LoadProductsToGrid();
        }

        private void comboBoxDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            LoadProductsToGrid();
        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            LoadProductsToGrid();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Authorization authorization = new Authorization();

            this.Hide();
            authorization.Show();
        }

        private void textBoxSearch_TextChanged_1(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            LoadProductsToGrid();
        }
    }
}
