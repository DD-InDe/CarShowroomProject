using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using CarShowroom.Pages.EmployeePages;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages;

public partial class CarInfoPage : Page
{
    private Car _car;

    /// <summary>
    ///  Конструктор страницы
    /// </summary>
    /// <param name="car">наш автомобиль</param>
    /// <param name="isViewOnly">переменная указывающая только просмотр или возможно редактирование</param>
    public CarInfoPage(Car car, bool isViewOnly)
    {
        // 
        _car = car;
        InitializeComponent();

        // если не только просмотр, то разрешаем добавлять авто в заявку и редактировать его
        if (!isViewOnly)
        {
            if (App.AuthorizedUser.RoleId is 3)
                RequestButton.Visibility = Visibility.Visible;
            else
                EditButton.Visibility = Visibility.Visible;
        }
    }

    /// <summary>
    /// Метод для обработки события загрузки страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CarInfoPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            /*
             * ищем авто в базе, если не находим, то возвращаемся назад (на просмотр всех авто)
             * (это нам нужно для страницы EditCarPage, после удаление авто мы переходим назад на эту страницу, а авто уже не существует)
             */
            if (Db.Context.Cars.Find(_car.CarVin) == null)
            {
                NavigationService.GoBack();
                return;
            }

            // находим всю инфу по авто из базы и загружаем на страницу
            _car = Db.Context.Cars
                .Include(c => c.Model)
                .Include(c => c.Model.Brand)
                .Include(c => c.Model.EngineType)
                .Include(c => c.Model.BodyType)
                .Include(c => c.Model.Class)
                .Include(c => c.Model.Generation)
                .First(c => c.CarVin == _car.CarVin);
            DataContext = _car;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    /// <summary>
    /// Обработка события нажатия на кнопку "Оставить заявку"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RequestButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_car.StatusId != 1)
            {
                MessageBox.Show("Эта машина уже забронирована");
                return;
            }

            // создаем новую заявку
            Request request = new()
            {
                Car = _car,
                StatusId = 1,
                DateCreate = DateTime.Now,
                CustomerId = App.AuthorizedUser.UserId
            };

            // добавляем заявку в базу
            Db.Context.Requests.Add(request);
            Db.Context.SaveChanges();
            MessageBox.Show("Заявка оставлена");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    /// <summary>
    /// Обработка события нажатия на кнопку "Редактировать"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EditButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new EditCarPage(_car));
}