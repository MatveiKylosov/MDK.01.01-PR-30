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
        public MainWindow()
        {
            InitializeComponent();
            
            List<Classes.CarBrands> carBrands = Classes.CarBrands.GetAll;
            List<Classes.Cars> cars = Classes.Cars.GetAll;
            List<Classes.Customers> customers = Classes.Customers.GetAll;
            List<Classes.Employees> employees = Classes.Employees.GetAll;
            List<Classes.Sales> sales = Classes.Sales.GetAll;

            MessageBox.Show($"{carBrands.Count}\n{cars.Count}\n{customers.Count}\n{employees.Count}\n{sales.Count}");
        }
    }
}
