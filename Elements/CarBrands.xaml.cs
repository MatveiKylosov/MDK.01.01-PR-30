﻿using System;
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
    /// Логика взаимодействия для CarBrands.xaml
    /// </summary>
    public partial class CarBrands : UserControl
    {
        Classes.CarBrands carBrands;
        bool edit = false;

        public CarBrands(Classes.CarBrands carBrands, bool admin = false)
        {
            InitializeComponent();
            this.carBrands = carBrands;

            if(!admin) 
                ButtonRow.Height = new GridLength(0);

            if(carBrands != null)
            {
                BrandName.Text = carBrands.BrandName;
                CountryOrigin.Text = carBrands.CountryOrigin;
                ManufacturerFactory.Text = carBrands.ManufacturerFactory;
                Address.Text = carBrands.Address;
            }
            else
            {
                CreateEditButton.Content = "Добавить";
                CancelRemoveButton.Content = "Отменить";
            }
        }

        private void CreateEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEdit())
                return;

            if (carBrands != null)
            {
                MessageBox.Show(BrandName.Text);
                if (carBrands.Update(BrandName.Text, CountryOrigin.Text, ManufacturerFactory.Text, Address.Text))
                    MessageBox.Show("Данные были успешно обновлены!", "Внимание!",MessageBoxButton.OK, MessageBoxImage.Information);
                else 
                    MessageBox.Show("В текущий момент база данных не доступна.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (carBrands.Insert(BrandName.Text, CountryOrigin.Text, ManufacturerFactory.Text, Address.Text))
                    MessageBox.Show("Данные были успешно добавлены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("В текущий момент база данных не доступна.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        bool CheckEdit()
        {
            if (edit)
            {
                if(MainWindow.main.cars.Any(x => x.Stamp == this.carBrands.BrandName))
                {
                    MessageBox.Show("Эта запись используется в других таблицах!\nЧтобы изменить название бреда удалите записи из других таблиц, где используется эта запись.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if(string.IsNullOrEmpty(BrandName.Text) || BrandName.Text.Length > 50)
                {
                    MessageBox.Show("Название бренда должно быть больше 0 и меньше 50 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(CountryOrigin.Text) || BrandName.Text.Length > 250)
                {
                    MessageBox.Show("Название страны должно быть больше 0 и меньше 250 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(ManufacturerFactory.Text) || BrandName.Text.Length > 250)
                {
                    MessageBox.Show("Название завода должно быть больше 0 и меньше 250 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                if (string.IsNullOrEmpty(Address.Text) || BrandName.Text.Length > 250)
                {
                    MessageBox.Show("Адрес должен быть больше 0 и меньше 250 символов!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                BrandName.IsEnabled = CountryOrigin.IsEnabled = ManufacturerFactory.IsEnabled = Address.IsEnabled = false;

                CancelRemoveButton.Content = (carBrands != null ? "Удалить" : "Стереть");
                edit = false;

                return true;
            }
            else
            {
                BrandName.IsEnabled = CountryOrigin.IsEnabled = ManufacturerFactory.IsEnabled = Address.IsEnabled = true;
                CancelRemoveButton.Content = "Отменить";
                edit = true;

                return false;
            } 
        }

        private void CancelRemove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}