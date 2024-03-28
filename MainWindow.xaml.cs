using Org.BouncyCastle.Pqc.Crypto.Falcon;
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

            UpdateDate();
        }

        void UpdateDate()
        {
            carBrands = Classes.CarBrands.GetAll;
            cars = Classes.Cars.GetAll;
            customers = Classes.Customers.GetAll;
            employees = Classes.Employees.GetAll;
            sales = Classes.Sales.GetAll;
        }

        private void CarBrandsOpenClick(object sender, RoutedEventArgs e)
        {
            carBrands = Classes.CarBrands.GetAll;
            ElementsPanel.Children.Clear();
            foreach(var x in carBrands)
                ElementsPanel.Children.Add(new Elements.CarBrands(x, admin));
        }

        private void CarsClick(object sender, RoutedEventArgs e)
        {

        }

        private void CustomersClick(object sender, RoutedEventArgs e)
        {

        }

        private void SalesClick(object sender, RoutedEventArgs e)
        {

        }

        private void StaffClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
