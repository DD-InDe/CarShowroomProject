using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using CarShowroom.Pages.EmployeePages;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages;

public partial class CarInfoPage : Page
{
    private Car _car;

    public CarInfoPage(Car car)
    {
        _car = car;
        InitializeComponent();

        if (App.AuthorizedUser.RoleId is 3)
            EditButton.Visibility = Visibility.Collapsed;
        else
            RequestButton.Visibility = Visibility.Collapsed;
    }

    private void CarInfoPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
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

    private void RequestButton_OnClick(object sender, RoutedEventArgs e) => throw new NotImplementedException();

    private void EditButton_OnClick(object sender, RoutedEventArgs e)=>
        NavigationService.Navigate(new EditCarPage(_car));
}