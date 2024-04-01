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
        Sales sale;
        bool edit = false;
        public Sales(Sales sale, bool admin = false)
        {
            InitializeComponent();
            this.sale = sale;

            if (!admin)
                ButtonRow.Height = new GridLength(0);

            foreach (Customers i in MainWindow.main.customers)
                CustomersID.Items.Add(i.CustomerID);

            foreach (Employees i in MainWindow.main.employees)
                EmployeeID.Items.Add(i.EmployeeID);

            foreach (Cars i in MainWindow.main.cars)
                CarID.Items.Add(i.CarID);

            if (sale != null)
            {
                CustomersID.SelectedItem = sale.CustomersID;
                EmployeeID.SelectedItem = sale.EmployeeID;
                CarID.SelectedItem = sale.CarID;
                DateSale.Text = sale.DateSale.ToString();
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
                if (sale.Update(int.Parse(CustomersID.SelectedItem.ToString()), int.Parse(EmployeeID.SelectedItem.ToString()), int.Parse(CarID.SelectedItem.ToString()), DateTime.Parse(DateSale.Text)))
                    MessageBox.Show("Данные были успешно обновлены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("В текущий момент база данных не доступна.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (Sales.Insert(int.Parse(CustomersID.SelectedItem.ToString()), int.Parse(EmployeeID.SelectedItem.ToString()), int.Parse(CarID.SelectedItem.ToString()), DateTime.Parse(DateSale.Text)))
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
            // Проверка ввода данных
            // ...
        }

        private void CancelRemove_Click(object sender, RoutedEventArgs e)
        {
            // Обработка нажатия кнопки "Отменить/Удалить"
            // ...
        }
    }
}
