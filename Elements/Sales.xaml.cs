using MDK._01._01_PR_30.Classes;
using Mysqlx.Datatypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для Sales.xaml
    /// </summary>
    public partial class Sales : UserControl
    {
        Classes.Sales sale;
        bool edit = false;
        public Sales(Classes.Sales sale, bool admin = false)
        {
            InitializeComponent();
            this.sale = sale;

            if (!admin)
                ButtonRow.Height = new GridLength(0);

            foreach (Classes.Customers i in MainWindow.main.customers)
                CustomersID.Items.Add(i.FullName);

            foreach (Classes.Employees i in MainWindow.main.employees)
                EmployeeID.Items.Add(i.FullName);

            foreach (Classes.Cars i in MainWindow.main.cars)
                CarID.Items.Add(i.Name);

            if (sale != null)
            {
                CustomersID.SelectedItem = MainWindow.main.customers.Find(x => x.CustomersID == sale.CustomersID).FullName;
                EmployeeID.SelectedItem = MainWindow.main.employees.Find(x => x.EmployeeID == sale.EmployeeID).FullName;
                CarID.SelectedItem = MainWindow.main.cars.Find(x => x.CarID == sale.CarID).Name;
                DateSale.Text = sale.DateSale.ToString("dd.MM.yyyy");
            }
            else
            {
                edit = true;
                CustomersID.IsEnabled = EmployeeID.IsEnabled = CarID.IsEnabled = DateSale.IsEnabled = true;
                CreateEditButton.Content = "Добавить";
                CancelRemoveButton.Content = "Отменить";
            }
        }

        private void CreateEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEdit())
                return;

            if (sale != null)
            {
                if (sale.Update(MainWindow.main.customers.Find(x => x.FullName == CustomersID.SelectedItem.ToString()).CustomersID, 
                                MainWindow.main.employees.Find(x => x.FullName == EmployeeID.SelectedItem.ToString()).EmployeeID,
                                MainWindow.main.cars.Find(x => x.Name == CarID.SelectedItem.ToString()).CarID, 
                                DateTime.Parse(DateSale.Text)))
                    MessageBox.Show("Данные были успешно обновлены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("В текущий момент база данных не доступна.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (Classes.Sales.Insert(MainWindow.main.customers.Find(x => x.FullName == CustomersID.SelectedItem.ToString()).CustomersID,
                                MainWindow.main.employees.Find(x => x.FullName == EmployeeID.SelectedItem.ToString()).EmployeeID,
                                MainWindow.main.cars.Find(x => x.Name == CarID.SelectedItem.ToString()).CarID,
                                DateTime.Parse(DateSale.Text)))
                {
                    MainWindow.main.SalesClick(null, null);
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
                if (CustomersID.SelectedIndex == -1)
                {
                    MessageBox.Show("Укажите клиента!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (EmployeeID.SelectedIndex == -1)
                {
                    MessageBox.Show("Укажите сотрудника!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (CarID.SelectedIndex == -1)
                {
                    MessageBox.Show("Укажите машину!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if(string.IsNullOrEmpty(DateSale.Text))
                {
                    MessageBox.Show("Укажите дату!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                CustomersID.IsEnabled = EmployeeID.IsEnabled = CarID.IsEnabled = DateSale.IsEnabled = false;

                CancelRemoveButton.Content = (sale != null ? "Удалить" : "Стереть");
                edit = false;

                return true;
            }
            else
            {
                CustomersID.IsEnabled = EmployeeID.IsEnabled = CarID.IsEnabled = DateSale.IsEnabled = true;
                CancelRemoveButton.Content = "Отменить";
                edit = true;

                return false;
            }
        }

        private void CancelRemove_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                if(sale != null)
                {
                    CustomersID.SelectedItem = MainWindow.main.customers.Find(x => x.CustomersID == sale.CustomersID).FullName;
                    EmployeeID.SelectedItem = MainWindow.main.employees.Find(x => x.EmployeeID == sale.EmployeeID).FullName;
                    CarID.SelectedItem = MainWindow.main.cars.Find(x => x.CarID == sale.CarID).Name;
                    DateSale.Text = sale.DateSale.ToString("dd.MM.yyyy");
                }
                else
                {
                    CustomersID.SelectedIndex = EmployeeID.SelectedIndex = CarID.SelectedIndex = 0;
                    DateSale.Text = "";
                }
            }
            else if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Внимание.", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                sale.Delete();
                MainWindow.main.SalesClick(null, null);
            }
        }
    }
}
