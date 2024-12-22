using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using CarShowroom.Pages.EmployeePages;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages;

public partial class ViewCarPage : Page
{
    public ViewCarPage()
    {
        InitializeComponent();
    }

    private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) => LoadData();

    private void BrandComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => LoadData();

    private void ViewCarPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            CarPresenter.ItemsSource = Db.Context.Cars.Include(c => c.Model).Include(c => c.Model.Brand)
                .Include(c => c.Status).ToList();

            List<Brand> brands = new() { new() { Name = "Все" } };
            brands.AddRange(Db.Context.Brands.ToList());
            BrandComboBox.ItemsSource = brands;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void LoadData()
    {
        try
        {
            List<Car> cars = Db.Context.Cars.Include(c => c.Model).Include(c => c.Model.Brand)
                .Include(c => c.Status).ToList();
            if (BrandComboBox.SelectedIndex != 0 && BrandComboBox.SelectedItem != null)
                cars = cars.Where(c => c.Model.BrandId == ((Brand)BrandComboBox.SelectedItem).BrandId).ToList();
            cars = cars.Where(c =>
                    c.Model.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                    c.Model.Brand.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                    c.YearOfManufacture.ToString().Contains(SearchTextBox.Text.ToLower()) ||
                    c.Price.ToString().Contains(SearchTextBox.Text.ToLower()))
                .ToList();

            CarPresenter.ItemsSource = null;
            CarPresenter.ItemsSource = cars;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show($"Произошла ошибка: {e.Message}");
        }
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new EditCarPage());

    private void InfoButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new CarInfoPage(
        ((Button)sender).DataContext as Car));
}