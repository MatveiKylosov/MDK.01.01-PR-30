using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Microsoft.Win32;
using OfficeOpenXml;

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

        Classes.Customers customer;
        Classes.Employees employee;

        bool admin = true;

        public MainWindow()
        {
            InitializeComponent();
            main = this;
        }

        public void CarBrandsClick(object sender, RoutedEventArgs e)
        {
            carBrands = Classes.CarBrands.GetAll;
            ElementsPanel.Children.Clear();

            if (carBrands == null || carBrands == null)
                MessageBox.Show("Не удалось подключиться к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if(admin) 
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
                if(admin) 
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
            {
                MessageBox.Show("Не удалось подключиться к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (admin)
            {
                ElementsPanel.Children.Add(new Elements.Customers(null));
                foreach (var x in customers)
                    ElementsPanel.Children.Add(new Elements.Customers(x, admin));  
            }
            else
                ElementsPanel.Children.Add(new Elements.Customers(customers.Find(x => x.CustomersID == customer.CustomersID)));
        }

        public void SalesClick(object sender, RoutedEventArgs e)
        {
            cars = Classes.Cars.GetAll;
            customers = Classes.Customers.GetAll;
            employees = Classes.Employees.GetAll;
            sales = Classes.Sales.GetAll;


            ElementsPanel.Children.Clear();
            if (cars == null ||  customers == null || employees == null || sales == null)
            {
                MessageBox.Show("Не удалось подключиться к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (admin)
            {
                ElementsPanel.Children.Add(new Elements.Sales(null, admin));
                foreach (var x in sales)
                    ElementsPanel.Children.Add(new Elements.Sales(x, admin));
            }
            else
            {
                var list = sales.FindAll(f => f.CustomersID == customer.CustomersID);

                foreach (var x in list)
                    ElementsPanel.Children.Add(new Elements.Sales(x));
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
                ElementsPanel.Children.Add(new Elements.Employees(null));
                foreach (var x in employees)
                    ElementsPanel.Children.Add(new Elements.Employees(x, x.EmployeeID == employee.EmployeeID));
            }
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            customers = Classes.Customers.GetAll;
            employees = Classes.Employees.GetAll;

            customer = customers.FirstOrDefault(x => x.FullName == FullName.Text && x.Password == Password.Password);
            employee = employees.FirstOrDefault(x => x.FullName == FullName.Text && x.Password == Password.Password);
            admin = employee != null;

            if(admin || customer != null)
            {
                Login.Visibility = Visibility.Hidden;
                if(customer != null)
                    EmployeesBTN.Height = EmployeesBTN.Width = ExportBTN.Height = ExportBTN.Width = 0;
                else 
                    EmployeesBTN.Height = EmployeesBTN.Width = ExportBTN.Height = ExportBTN.Width = 50;
            }
            else if (customers == null || employees == null)
                MessageBox.Show("Не удалось подключиться к базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else 
                MessageBox.Show("Такой пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            Login.Visibility = Visibility.Visible;
            ElementsPanel.Children.Clear();
        }

        private void export(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName == "")
                return;

            carBrands = Classes.CarBrands.GetAll;
            cars = Classes.Cars.GetAll;
            customers = Classes.Customers.GetAll;
            employees = Classes.Employees.GetAll;
            sales = Classes.Sales.GetAll;

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (var package = new ExcelPackage())
            {
                var carBrand = package.Workbook.Worksheets.Add("Бренды");
                var Car = package.Workbook.Worksheets.Add("Машины");
                var Customer = package.Workbook.Worksheets.Add("Клиенты");
                var Employee = package.Workbook.Worksheets.Add("Сотрудники");
                var Sale = package.Workbook.Worksheets.Add("Продажи");

                //carBrand
                {
                    carBrand.Cells[1, 1].Value = "Бренд";
                    carBrand.Cells[1, 2].Value = "Страна";
                    carBrand.Cells[1, 3].Value = "Завод";
                    carBrand.Cells[1, 4].Value = "Адрес";

                    for (int i = 0; i < carBrands.Count; i++)
                    {
                        carBrand.Cells[i + 2, 1].Value = carBrands[i].BrandName;
                        carBrand.Cells[i + 2, 2].Value = carBrands[i].CountryOrigin;
                        carBrand.Cells[i + 2, 3].Value = carBrands[i].ManufacturerFactory;
                        carBrand.Cells[i + 2, 4].Value = carBrands[i].Address;
                    }
                }

                //car
                {
                    Car.Cells[1, 1].Value = "ID";
                    Car.Cells[1, 2].Value = "Название";
                    Car.Cells[1, 3].Value = "Марка";
                    Car.Cells[1, 4].Value = "Год выпуска";
                    Car.Cells[1, 5].Value = "Цвет";
                    Car.Cells[1, 6].Value = "Категория";
                    Car.Cells[1, 7].Value = "Цена";

                    for (int i = 0; i < cars.Count; i++)
                    {
                        Car.Cells[i + 2, 1].Value = $"{cars[i].CarID}";
                        Car.Cells[i + 2, 2].Value = $"{cars[i].Name}";
                        Car.Cells[i + 2, 3].Value = $"{cars[i].Stamp}";
                        Car.Cells[i + 2, 4].Value = $"{cars[i].YearProduction}";
                        Car.Cells[i + 2, 5].Value = $"{cars[i].Colour}";
                        Car.Cells[i + 2, 6].Value = $"{cars[i].Category}";
                        Car.Cells[i + 2, 7].Value = $"{cars[i].Price}";
                    }
                }

                //Customer
                {
                    Customer.Cells[1, 1].Value = "ID";
                    Customer.Cells[1, 2].Value = "Полное имя";
                    Customer.Cells[1, 3].Value = "Данные паспорта";
                    Customer.Cells[1, 4].Value = "Адрес";
                    Customer.Cells[1, 5].Value = "Город";
                    Customer.Cells[1, 6].Value = "Дата рождения";
                    Customer.Cells[1, 7].Value = "Пол";

                    for (int i = 0; i < customers.Count; i++)
                    {
                        Customer.Cells[i + 2, 1].Value = $"{customers[i].CustomersID}";
                        Customer.Cells[i + 2, 2].Value = $"{customers[i].FullName}";
                        Customer.Cells[i + 2, 3].Value = $"{customers[i].PassportDetails}";
                        Customer.Cells[i + 2, 4].Value = $"{customers[i].Address}";
                        Customer.Cells[i + 2, 5].Value = $"{customers[i].city}";
                        Customer.Cells[i + 2, 6].Value = $"{customers[i].DateOfBirth.ToString("dd.MM.yyyy")}";
                        Customer.Cells[i + 2, 7].Value = $"{(customers[i].Gender ? "М" : "Ж")}";
                    }
                }

                //employees
                {
                    Employee.Cells[1, 1].Value = "ID";
                    Employee.Cells[1, 2].Value = "Полное имя";
                    Employee.Cells[1, 3].Value = "Опыт";
                    Employee.Cells[1, 4].Value = "Зарплата";

                    for (int i = 0; i < employees.Count; i++)
                    {
                        Employee.Cells[i + 2, 1].Value = $"{employees[i].EmployeeID}";
                        Employee.Cells[i + 2, 2].Value = $"{employees[i].FullName}";
                        Employee.Cells[i + 2, 3].Value = $"{employees[i].Experience}";
                        Employee.Cells[i + 2, 4].Value = $"{employees[i].Salary}";
                    }
                }

                //Sale
                {
                    Sale.Cells[1, 1].Value = "ID";
                    Sale.Cells[1, 2].Value = "Покупатель";
                    Sale.Cells[1, 3].Value = "Сотрудник";
                    Sale.Cells[1, 4].Value = "Машина";
                    Sale.Cells[1, 5].Value = "Дата";

                    for (int i = 0; i < sales.Count; i++)
                    {
                        Sale.Cells[i + 2, 1].Value = $"{sales[i].SaleID}";
                        Sale.Cells[i + 2, 2].Value = $"{customers.Find(x => x.CustomersID == sales[i].CustomersID).FullName}";
                        Sale.Cells[i + 2, 3].Value = $"{employees.Find(x => x.EmployeeID == sales[i].EmployeeID).FullName}";
                        Sale.Cells[i + 2, 4].Value = $"{cars.Find(x => x.CarID == sales[i].CarID).Name}";
                        Sale.Cells[i + 2, 5].Value = $"{sales[i].DateSale.ToString("dd.MM.yyyy")}";
                    }
                }

                // Сохранение файла
                var file = new FileInfo(saveFileDialog.FileName);
                package.SaveAs(file);
            }
        }
    }
}
