using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarShowroom.Database;
using Microsoft.Win32;

namespace CarShowroom.Pages.EmployeePages;

public partial class EditCarPage : Page
{
    private Car _car;

    public EditCarPage()
    {
        _car = new() { Model = new() };
        InitializeComponent();

        DeleteButton.Visibility = Visibility.Collapsed;
    }

    public EditCarPage(Car car)
    {
        _car = car;
        InitializeComponent();

        VinTextBox.IsEnabled = false;
    }

    private void EditCarPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_car.Photo == null)
                _car.Photo = File.ReadAllBytes("../../../Resources/default-car.png");

            ModelComboBox.ItemsSource = Db.Context.Models.ToList();

            DataContext = _car;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void PhotoImage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        try
        {
            OpenFileDialog openFileDialog = new() { Filter = "images / .jpg, .jpeg, .png |*.jpg; *.jpeg; *.png" };
            openFileDialog.ShowDialog();
            if (!String.IsNullOrWhiteSpace(openFileDialog.FileName))
                _car.Photo = File.ReadAllBytes(openFileDialog.FileName);

            PhotoImage.DataContext = null;
            PhotoImage.DataContext = _car;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_car.Photo != null && !String.IsNullOrEmpty(_car.CarVin) &&
                _car.Color != null && _car.Price > 0 &&
                _car.YearOfManufacture > 1900 && _car.Model != null)
            {
                _car.StatusId = 1;
                if (Db.Context.Cars.Find(_car.CarVin) == null)
                    Db.Context.Cars.Add(_car);
                Db.Context.SaveChanges();

                MessageBox.Show("Данные сохранены");
            }
            else
            {
                MessageBox.Show("Заполните все поля. Год не может быть меньше 1900. Цена не может быть меньше 0");
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void DisableSpace(object sender, KeyEventArgs e) => e.Handled = e.Key == Key.Space;

    private void NumberTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e) =>
        e.Handled = !char.IsDigit(e.Text, 0);

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (Db.Context.Requests.FirstOrDefault(c => c.CarId == _car.CarVin && c.StatusId != 3) != null)
                MessageBox.Show("Эта машина забронирована в другой заявке");
            else
            {
                Db.Context.Cars.Remove(_car);
                Db.Context.SaveChanges();
                MessageBox.Show("Данные о машине удалены");
                NavigationService.GoBack();
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }
}