using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MDK._01._01_PR_30.Elements
{
    /// <summary>
    /// Логика взаимодействия для Customers.xaml
    /// </summary>
    public partial class Customers : UserControl
    {
        Classes.Customers customers;
        bool edit = false;

        public Customers(Classes.Customers customers, bool admin = false)
        {
            InitializeComponent();
            this.customers = customers;

            if (!admin)
                ButtonRow.Height = new GridLength(0);

            if (customers != null)
            {
                FullName.Text = customers.FullName;
                PassportDetails.Text = customers.PassportDetails;
                Address.Text = customers.Address;
                City.Text = customers.city;
                DateOfBirth.Text = customers.DateOfBirth.ToString("dd.MM.yyyy");

                Man.IsChecked = customers.Gender;
                Woman.IsChecked = !customers.Gender;
            }
            else
            {
                edit = true;
                FullName.IsEnabled = PassportDetails.IsEnabled = Address.IsEnabled = City.IsEnabled = DateOfBirth.IsEnabled = Gender.IsEnabled = true;
                CreateEditButton.Content = "Добавить";
                CancelRemoveButton.Content = "Отменить";

                Man.IsChecked = false;
                Woman.IsChecked = false;
            }
        }

        private void CreateEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEdit())
                return;

            if (customers != null)
            {
                if (customers.Update(FullName.Text, PassportDetails.Text, Address.Text, City.Text, DateTime.Parse(DateOfBirth.Text), (bool)Man.IsChecked))
                    MessageBox.Show("Данные были успешно обновлены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("В текущий момент база данных не доступна.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (Classes.Customers.Insert(FullName.Text, PassportDetails.Text, Address.Text, City.Text, DateTime.Parse(DateOfBirth.Text), (bool)Man.IsChecked))
                {
                    MainWindow.main.CustomersClick(null, null);
                    MessageBox.Show("Данные были успешно добавлены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("В текущий момент база данных не доступна.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool CheckEdit()
        {

            if (edit)
            {
                if (string.IsNullOrEmpty(FullName.Text) || FullName.Text.Length > 50)
                {
                    MessageBox.Show("Полное имя должно быть больше 0 и меньше 250 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (!new Regex(@"^\d{4} \d{6}$").IsMatch(PassportDetails.Text))
                {
                    MessageBox.Show("Паспортные данные должны быть в формате \"xxxx xxxxxx\"!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(Address.Text) || Address.Text.Length > 250)
                {
                    MessageBox.Show("Адрес должен быть больше 0 и меньше 250 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(City.Text) || City.Text.Length > 250)
                {
                    MessageBox.Show("Город должен быть больше 0 и меньше 250 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                
                if(!DateTime.TryParseExact(DateOfBirth.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
                {
                    MessageBox.Show("Дата рождения указана не правильно!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if(!(bool)Man.IsChecked && !(bool)Woman.IsChecked)
                {
                    MessageBox.Show("Укажите пол!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                FullName.IsEnabled = PassportDetails.IsEnabled = Address.IsEnabled = City.IsEnabled = DateOfBirth.IsEnabled = Gender.IsEnabled = false;

                CancelRemoveButton.Content = (customers != null ? "Удалить" : "Стереть");
                edit = false;

                return true;
            }
            else
            {
                FullName.IsEnabled = PassportDetails.IsEnabled = Address.IsEnabled = City.IsEnabled = DateOfBirth.IsEnabled = Gender.IsEnabled = true;
                CancelRemoveButton.Content = "Отменить";
                edit = true;

                return false;
            }
        }

        private void CancelRemove_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                if (customers != null)
                {
                    FullName.Text = customers.FullName;
                    PassportDetails.Text = customers.PassportDetails;
                    Address.Text = customers.Address;
                    City.Text = customers.city;
                    DateOfBirth.Text = customers.DateOfBirth.ToString();

                    Man.IsChecked = customers.Gender;
                    Woman.IsChecked = !customers.Gender;

                    CancelRemoveButton.Content = (customers != null ? "Удалить" : "Стереть");

                    FullName.IsEnabled = PassportDetails.IsEnabled = Address.IsEnabled = City.IsEnabled = DateOfBirth.IsEnabled = Gender.IsEnabled = false;
                    edit = false;
                }
                else
                {
                    FullName.Text = PassportDetails.Text = Address.Text = City.Text = DateOfBirth.Text = "";

                    Man.IsChecked = false;
                    Woman.IsChecked = false;
                }
            }
            else
            {
                if (MainWindow.main.sales.Any(x => x.CustomersID == this.customers.CustomersID ))
                {
                    MessageBox.Show("Эта запись используется в других таблицах!\nЧтобы удалить эту запись сначала удалите записи из других таблиц, где используется эта запись.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Внимание.", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    return;

                customers.Delete();
                MainWindow.main.CustomersClick(null, null);
            }
        }
    }

}
