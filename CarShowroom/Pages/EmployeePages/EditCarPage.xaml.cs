using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarShowroom.Database;
using Microsoft.Win32;

namespace CarShowroom.Pages.EmployeePages;

public partial class EditCarPage : Page
{
    // глобальная переменная
    private Car _car;

    /// <summary>
    /// Конструктор для нового авто
    /// </summary>
    public EditCarPage()
    {
        _car = new() { Model = new() };
        InitializeComponent();

        DeleteButton.Visibility = Visibility.Collapsed;
    }

    /// <summary>
    /// Конструктор уже для созданного авто
    /// </summary>
    /// <param name="car"></param>
    public EditCarPage(Car car)
    {
        _car = car;
        InitializeComponent();

        VinTextBox.IsEnabled = false;
    }

    /// <summary>
    /// Метод для обработки события загрузки страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EditCarPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // если у авто нет фото, то загружаем дефолтное
            if (_car.Photo == null)
                _car.Photo = File.ReadAllBytes("../../../Resources/default-car.png");

            // в список моделей загружаем данные из базы
            ModelComboBox.ItemsSource = Db.Context.Models.ToList();

            // делаем привязку данных
            DataContext = _car;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод для обработки события нажатия по фотографии
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PhotoImage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        try
        {
            // используем OpenFileDialog для того чтоб запросить у пользователя путь к файлу фото
            OpenFileDialog openFileDialog = new() { Filter = "images / .jpg, .jpeg, .png |*.jpg; *.jpeg; *.png" };
            openFileDialog.ShowDialog();
            if (!String.IsNullOrWhiteSpace(openFileDialog.FileName))
                _car.Photo = File.ReadAllBytes(openFileDialog.FileName);

            // заново отображаем фото
            PhotoImage.DataContext = null;
            PhotoImage.DataContext = _car;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }
    
    /// <summary>
    /// Метод для обработки события нажатия на кнопку "Сохранить"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // проверка на пустоту и выход за ограничения
            if (_car.Photo != null && !String.IsNullOrEmpty(_car.CarVin) &&
                _car.Color != null && _car.Price > 0 &&
                _car.YearOfManufacture > 1900 && _car.Model != null)
            {
                // задаем авто новый статус
                _car.StatusId = 1;
                // если авто нет в базе, то добавляем
                if (Db.Context.Cars.Find(_car.CarVin) == null)
                    Db.Context.Cars.Add(_car);
                // сохраняем данные в базе
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

    /// <summary>
    /// Метод для блокировки ввода пробела в поле
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DisableSpace(object sender, KeyEventArgs e) => e.Handled = e.Key == Key.Space;

    /// <summary>
    /// Метод для блокировки символов, кроме цифр
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumberTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e) =>
        e.Handled = !char.IsDigit(e.Text, 0);

    /// <summary>
    /// Метод для обработки события нажатия на кнопку "Удалить"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // если авто есть в заявках, то удалить нельзя
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