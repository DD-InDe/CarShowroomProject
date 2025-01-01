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

        // делаем кнопку "добавить" доступной только для сотрудников
        // AddButton.Visibility = роль пользователя клиент ? (да) кнопку не видно : (нет) кнопку видно;
        AddButton.Visibility = App.AuthorizedUser.RoleId is 3 ? Visibility.Collapsed : Visibility.Visible;
    }

    /// <summary>
    /// Обработка события ввода в поисковую строку
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) => LoadData();

    /// <summary>
    /// Обработка события выбора марки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BrandComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => LoadData();

    /// <summary>
    /// Обработка события загрузки страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ViewCarPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // добавляем в объект для отображения авто данные из базы
            CarPresenter.ItemsSource = Db.Context.Cars.Include(c => c.Model).Include(c => c.Model.Brand)
                .Include(c => c.Status).ToList();

            // создаем переменную для хранения марок авто с базовым значением "все"
            List<Brand> brands = new() { new() { Name = "Все" } };
            // добавляем прочие марки
            brands.AddRange(Db.Context.Brands.ToList());
            // задаем этот список в элемент BrandComboBox
            BrandComboBox.ItemsSource = brands;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод для загрузки данных из базы
    /// </summary>
    private void LoadData()
    {
        try
        {
            // берем все авто
            List<Car> cars = Db.Context.Cars.Include(c => c.Model).Include(c => c.Model.Brand)
                .Include(c => c.Status).ToList();
            // если марка авто выбрана и она не "все", то загружаем только автомобиле марки, которую выбрали 
            if (BrandComboBox.SelectedIndex != 0 && BrandComboBox.SelectedItem != null)
                cars = cars.Where(c => c.Model.BrandId == ((Brand)BrandComboBox.SelectedItem).BrandId).ToList();
            // добавляем еще поисковую строку, которая действует на название модели, марки, дату производства и цену
            cars = cars.Where(c =>
                    c.Model.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                    c.Model.Brand.Name.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                    c.YearOfManufacture.ToString().Contains(SearchTextBox.Text.ToLower()) ||
                    c.Price.ToString().Contains(SearchTextBox.Text.ToLower()))
                .ToList();

            // заново отображаем данные
            CarPresenter.ItemsSource = null;
            CarPresenter.ItemsSource = cars;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show($"Произошла ошибка: {e.Message}");
        }
    }

    /// <summary>
    /// Обработка события нажатия на кнопку "Добавить"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new EditCarPage());

    /// <summary>
    /// Обработка события нажатия на кнопку "Информация"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void InfoButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new CarInfoPage(((Button)sender).DataContext as Car,false));
}