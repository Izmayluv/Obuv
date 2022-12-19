using Obuv.Classes;
using Obuv.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Obuv.Views
{
    public partial class EditProduct : Form
    {
        private string article;

        public EditProduct(string article)
        {
            InitializeComponent();
            this.article = article;
        }

        public static Bitmap bitmap;

        private void EditProduct_Load(object sender, EventArgs e)
        {
            // if editing
            if (!String.IsNullOrEmpty(article))
            {
                textBoxArticle.Text = article;
                textBoxArticle.Enabled = false;
                textBoxArticle.BackColor = Color.LightGray;

                var products = Helper.DbContext.Products.Where(x => x.productID == article).ToList();

                textBoxName.Text = Convert.ToString(products.Select(x => x.productName).First());

                textBoxDescription.Text = Convert.ToString(products.Select(x => x.productDescription).First());

                comboBoxManufacturer.Text = Convert.ToString(products.Select(x => x.Manufacturer.manufacturerName).First());
                comboBoxCategory.Text = Convert.ToString(products.Select(x => x.Category.categoryName).First());
                comboBoxProvider.Text = Convert.ToString(products.Select(x => x.Provider.providerName).First());

                textBoxCost.Text = Convert.ToString(products.Select(x => x.productCost).First());

                textBoxMaxDiscount.Text = Convert.ToString(products.Select(x => x.productMaxDiscountAmount).First());

                textBoxCurrentDiscount.Text = Convert.ToString(products.Select(x => x.productActiveDiscountAmount).First());

                textBoxAmount.Text = Convert.ToString(products.Select(x => x.productQuantityInStock).First());

                string picName = Convert.ToString(products.Select(x => x.productPicture).First());

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
                        bitmap = new Bitmap(Catalog.path + picName);
                        bitmap = new Bitmap(bitmap, 128, 128);
                    }
                }
                catch (Exception)
                {
                    bitmap = Resources.defPic;
                }

            pictureBox1.Image = bitmap;
            }
            else
            {
                var manufacturers = Helper.DbContext.Manufacturers.ToList();
                comboBoxManufacturer.DataSource = manufacturers;
                comboBoxManufacturer.DisplayMember = "manufacturerName";
                comboBoxManufacturer.ValueMember = "manufacturerID";

                var categories = Helper.DbContext.Categories.ToList();
                comboBoxCategory.DataSource = categories;
                comboBoxCategory.DisplayMember = "categoryName";
                comboBoxCategory.ValueMember = "categoryID";

                var providers = Helper.DbContext.Providers.ToList();
                comboBoxProvider.DataSource = providers;
                comboBoxProvider.DisplayMember = "providerName";
                comboBoxProvider.ValueMember = "providerID";
            }


        }

    }
}
