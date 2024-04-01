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

namespace MDK._01._01_PR_30
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public MainWindow main;
        public List<Classes.CarBrands> carBrands;
        public List<Classes.Cars> cars;
        public List<Classes.Customers> customers;
        public List<Classes.Employees> employees;
        public List<Classes.Sales> sales;
        bool admin = true;

        public MainWindow()
        {
            InitializeComponent();
            main = this;
        }

        public void CarBrandsOpenClick(object sender, RoutedEventArgs e)
        {
            carBrands = Classes.CarBrands.GetAll;
            ElementsPanel.Children.Clear();

            if (carBrands == null || carBrands == null)
                MessageBox.Show("Не удалось подключиться к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                ElementsPanel.Children.Add(new Elements.CarBrands(null, admin));
                foreach (var x in carBrands)
                    ElementsPanel.Children.Add(new Elements.CarBrands(x, admin));
            }
        }

        public void CarsClick(object sender, RoutedEventArgs e)
        {
            cars = Classes.Cars.GetAll;
            carBrands = Classes.CarBrands.GetAll;
            sales = Classes.Sales.GetAll;

            ElementsPanel.Children.Clear();

            if (cars == null || sales == null || carBrands == null)
                MessageBox.Show("Не удалось подключиться к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                ElementsPanel.Children.Add(new Elements.Cars(null, admin));
                foreach (var x in cars)
                    ElementsPanel.Children.Add(new Elements.Cars(x, admin));
            }
        }

        public void CustomersClick(object sender, RoutedEventArgs e)
        {
            customers = Classes.Customers.GetAll;
            sales = Classes.Sales.GetAll;

            ElementsPanel.Children.Clear();

            if (customers == null || sales == null)
                MessageBox.Show("Не удалось подключиться к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                ElementsPanel.Children.Add(new Elements.Customers(null, admin));
                foreach (var x in customers)
                    ElementsPanel.Children.Add(new Elements.Customers(x, admin));
            }
        }

        public void SalesClick(object sender, RoutedEventArgs e)
        {
            cars = Classes.Cars.GetAll;
            customers = Classes.Customers.GetAll;
            employees = Classes.Employees.GetAll;
            sales = Classes.Sales.GetAll;


            ElementsPanel.Children.Clear();
            if (cars == null ||  customers == null || employees == null || sales == null)
                MessageBox.Show("Не удалось подключиться к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                ElementsPanel.Children.Add(new Elements.Sales(null, admin));
                foreach (var x in sales)
                    ElementsPanel.Children.Add(new Elements.Sales(x, admin));
            }
        }

        public void EmployeesClick(object sender, RoutedEventArgs e)
        {
            employees = Classes.Employees.GetAll;
            sales = Classes.Sales.GetAll;
            ElementsPanel.Children.Clear();

            if (sales == null || employees == null)
                MessageBox.Show("Не удалось подключиться к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                ElementsPanel.Children.Add(new Elements.Employees(null, admin));
                foreach (var x in employees)
                    ElementsPanel.Children.Add(new Elements.Employees(x, admin));
            }
        }
    }
}
