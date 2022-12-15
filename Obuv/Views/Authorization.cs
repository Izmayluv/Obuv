using Obuv.Classes;
using Obuv.Views;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Obuv
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private static string userSurname, userPatronymic, userName, userRole;

        public static string GetUserSurname => userSurname;
        public static string GetUserPatronymic => userPatronymic;
        public static string GetUserName => userName;
        public static string GetUserRole => userRole;

        private void Auth(string login, string password)
        {
            var customer = Helper.DbContext.Customers
                .Where(x => x.userLogin == login && x.userPassword == password)
                .FirstOrDefault();

            if (customer != null)
            {
                userSurname = customer.userSurname;
                userPatronymic = customer.userPatronymic;
                userName = customer.userName;

                var staffRole = Helper.DbContext.StaffRoles
                    .Where(x => x.roleID == customer.userRole)
                    .FirstOrDefault();

                userRole = staffRole.roleName;

                _ = MessageBox.Show($"Здравствуйте, {customer.userSurname} {customer.userPatronymic}!\nВаша роль - {staffRole.roleName}");

                switch (customer.userRole)
                {
                    case 1:                                 //роль: клиент
                        //Catalog catalogView = new Catalog();

                        //this.Hide();
                        //catalogView.Show();

                        EditCatalog editCatalog = new EditCatalog();

                        this.Hide();
                        editCatalog.Show();
                        break;

                    case 2:                                 //роль: менеджер
                        //EditCatalog editCatalog = new EditCatalog();

                        //this.Hide();
                        //editCatalog.Show();
                        break;

                    case 3:                                 //роль: администратор

                        break;
                }

            }

            if (customer == null)
            {
               _ = MessageBox.Show("Пользователь с такими данными не найден");
            }
        }

        private void buttonAuthorization_Click(object sender, EventArgs e)
        {
            if (textBoxLogin.Text == String.Empty || textBoxPassword.Text == String.Empty)
            {
                _ = MessageBox.Show("Введите данные");
                return;
            }

            try
            {
                Auth(textBoxLogin.Text, textBoxPassword.Text);
            }
            catch (Exception)
            {
                _ = MessageBox.Show("Ошибка");
            }
        }
    }
}
