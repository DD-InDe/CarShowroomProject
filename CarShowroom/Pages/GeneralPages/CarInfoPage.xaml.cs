using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using CarShowroom.Pages.EmployeePages;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages;

public partial class CarInfoPage : Page
{
    private Car _car;

    public CarInfoPage(Car car, bool isViewOnly)
    {
        _car = car;
        InitializeComponent();

        if (!isViewOnly)
        {
            if (App.AuthorizedUser.RoleId is 3)
                RequestButton.Visibility = Visibility.Visible;
            else
                EditButton.Visibility = Visibility.Visible;
        }
    }

    private void CarInfoPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            if (Db.Context.Cars.Find(_car.CarVin) == null)
            {
                NavigationService.GoBack();
                return;
            }

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

    private void RequestButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (_car.StatusId != 1)
            {
                MessageBox.Show("Эта машина уже забронирована");
                return;
            }

            Request request = new()
            {
                Car = _car,
                StatusId = 1,
                DateCreate = DateTime.Now,
                CustomerId = App.AuthorizedUser.UserId
            };

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

    private void EditButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new EditCarPage(_car));
}