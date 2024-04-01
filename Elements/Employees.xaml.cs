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
    /// Логика взаимодействия для Employees.xaml
    /// </summary>
    public partial class Employees : UserControl
    {
        Classes.Employees employee;
        bool edit = false;
        public Employees(Classes.Employees employee, bool admin = false)
        {
            InitializeComponent();
            this.employee = employee;

            if (!admin)
                ButtonRow.Height = new GridLength(0);

            if (employee != null)
            {
                FullName.Text = employee.FullName;
                Experience.Text = employee.Experience.ToString();
                Salary.Text = employee.Salary.ToString();
            }
            else
            {
                edit = true;
                FullName.IsEnabled = Experience.IsEnabled = Salary.IsEnabled = true;
                CreateEditButton.Content = "Добавить";
                CancelRemoveButton.Content = "Отменить";
            }
        }

        private void CreateEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEdit())
                return;

            if (employee != null)
            {
                if (employee.Update(FullName.Text, int.Parse(Experience.Text), decimal.Parse(Salary.Text)))
                    MessageBox.Show("Данные были успешно обновлены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("В текущий момент база данных не доступна.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (Classes.Employees.Insert(FullName.Text, int.Parse(Experience.Text), decimal.Parse(Salary.Text)))
                {
                    MainWindow.main.EmployeesClick(null, null);
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
                if (string.IsNullOrEmpty(FullName.Text) || FullName.Text.Length > 250)
                {
                    MessageBox.Show("Полное имя должно быть больше 0 и меньше 250 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (!int.TryParse(Experience.Text, out int experience))
                {
                    MessageBox.Show("Укажите корректный опыт работы!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (!decimal.TryParse(Salary.Text, out decimal salary))
                {
                    MessageBox.Show("Укажите корректную зарплату!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                FullName.IsEnabled = Experience.IsEnabled = Salary.IsEnabled = false;

                CancelRemoveButton.Content = (employee != null ? "Удалить" : "Стереть");
                edit = false;

                return true;
            }
            else
            {
                FullName.IsEnabled = Experience.IsEnabled = Salary.IsEnabled = true;
                CancelRemoveButton.Content = "Отменить";
                edit = true;

                return false;
            }
        }

        private void CancelRemove_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                if (employee != null)
                {
                    FullName.Text = employee.FullName;
                    Experience.Text = employee.Experience.ToString();
                    Salary.Text = employee.Salary.ToString();

                    CancelRemoveButton.Content = (employee != null ? "Удалить" : "Стереть");

                    FullName.IsEnabled = Experience.IsEnabled = Salary.IsEnabled = false;
                    edit = false;
                }
                else
                {
                    FullName.Text = Experience.Text = Salary.Text = "";
                }
            }
            else
            {
                if (MainWindow.main.sales.Any(x => x.EmployeeID == this.employee.EmployeeID))
                {
                    MessageBox.Show("Эта запись используется в других таблицах!\nЧтобы удалить эту запись сначала удалите записи из других таблиц, где используется эта запись.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                employee.Delete();
                MainWindow.main.EmployeesClick(null, null);
            }
        }
    }

}
