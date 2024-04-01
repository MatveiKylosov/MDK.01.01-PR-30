using MDK._01._01_PR_30.Classes;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Cars.xaml
    /// </summary>
    public partial class Cars : UserControl
    {
        Classes.Cars car;
        bool edit = false;
        public Cars(Classes.Cars car, bool admin = false)
        {
            InitializeComponent();
            this.car = car;

            if (!admin)
                ButtonRow.Height = new GridLength(0);

            foreach (Classes.CarBrands i in MainWindow.main.carBrands)
                Stamp.Items.Add(i.BrandName);

            if (car != null)
            {
                Name.Text = car.Name;
                Stamp.SelectedItem = car.Stamp;
                YearProduction.Text = car.YearProduction.ToString();
                Colour.Text = car.Colour;
                Category.Text = car.Category;
                Price.Text = car.Price.ToString();
            }
            else
            {
                edit = true;
                Name.IsEnabled = Stamp.IsEnabled = YearProduction.IsEnabled = Colour.IsEnabled = Category.IsEnabled = Price.IsEnabled = true;
                CreateEditButton.Content = "Добавить";
                CancelRemoveButton.Content = "Отменить";
            }
        }

        private void CreateEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEdit())
                return;

            if (car != null)
            {
                if (car.Update(Name.Text, Stamp.SelectedItem.ToString(), int.Parse(YearProduction.Text), Colour.Text, Category.Text, decimal.Parse(Price.Text)))
                    MessageBox.Show("Данные были успешно обновлены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("В текущий момент база данных не доступна.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (Classes.Cars.Insert(Name.Text, Stamp.SelectedItem.ToString(), int.Parse(YearProduction.Text), Colour.Text, Category.Text, decimal.Parse(Price.Text)))
                {
                    MainWindow.main.CarsClick(null, null);
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
                if (string.IsNullOrEmpty(Name.Text) || Name.Text.Length > 250)
                {
                    MessageBox.Show("Название должно быть больше 0 и меньше 250 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (Stamp.SelectedIndex == -1)
                {
                    MessageBox.Show("Необходимо выбрать марку!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (!new Regex(@"^\d{4}$").IsMatch(YearProduction.Text))
                {
                    MessageBox.Show("Укажите корректный год производства. ", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(Colour.Text) || Colour.Text.Length > 250)
                {
                    MessageBox.Show("Цвет должен быть больше 0 и меньше 250 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(Category.Text) || Category.Text.Length > 250)
                {
                    MessageBox.Show("Категория должна быть больше 0 и меньше 250 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (!decimal.TryParse(Price.Text, out decimal price))
                {
                    MessageBox.Show("Укажите корректную цену!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                Name.IsEnabled = Stamp.IsEnabled = YearProduction.IsEnabled = Colour.IsEnabled = Category.IsEnabled = Price.IsEnabled = false;

                CancelRemoveButton.Content = (car != null ? "Удалить" : "Стереть");
                edit = false;

                return true;
            }
            else
            {
                Name.IsEnabled = Stamp.IsEnabled = YearProduction.IsEnabled = Colour.IsEnabled = Category.IsEnabled = Price.IsEnabled = true;
                CancelRemoveButton.Content = "Отменить";
                edit = true;

                return false;
            }
        }

        private void CancelRemove_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                if (car!= null)
                {
                    Name.Text = car.Name;
                    Stamp.SelectedItem = MainWindow.main.carBrands.FirstOrDefault(i => i.BrandName == car.Stamp);
                    YearProduction.Text = car.YearProduction.ToString();
                    Colour.Text = car.Colour;
                    Category.Text = car.Category;
                    Price.Text = car.Price.ToString();

                    CancelRemoveButton.Content = (car != null ? "Удалить" : "Стереть");

                    Name.IsEnabled = Stamp.IsEnabled = YearProduction.IsEnabled = Colour.IsEnabled = Category.IsEnabled = Price.IsEnabled = false;
                    edit = false;
                }
                else
                {
                    Stamp.SelectedIndex = -1;
                    Name.Text = YearProduction.Text = Colour.Text = Category.Text = Price.Text = "";
                }
            }
            else
            {
                if (MainWindow.main.sales.Any(x => x.CarID == this.car.CarID))
                {
                    MessageBox.Show("Эта запись используется в других таблицах!\nЧтобы удалить эту запись сначала удалите записи из других таблиц, где используется эта запись.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Внимание.", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    return;

                car.Delete();
                MainWindow.main.CarsClick(null, null);
            }
        }
    }
}
