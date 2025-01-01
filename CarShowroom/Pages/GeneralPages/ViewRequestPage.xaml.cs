using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages;

public partial class ViewRequestPage : Page
{
    public ViewRequestPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Обработка события загрузки страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ViewRequestPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // создаем список статусов с базовым значением "все"
            List<RequestStatus> statusList = new() { new() { Name = "Все" } };
            // добавляем данные из базы
            statusList.AddRange(Db.Context.RequestStatuses.ToList());
            // добавляем эти данные с StatusComboBox
            StatusComboBox.ItemsSource = statusList;

            // вызываем метод
            LoadData();
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
            // загружаем все запросы
            List<Request> requests = Db.Context.Requests
                .Include(c => c.Status)
                .Include(c => c.Car)
                .Include(c => c.Car.Model)
                .Include(c => c.Car.Model.Brand)
                .Include(c => c.Customer)
                .Include(c => c.Employee)
                .ToList();

            string search = SearchTextBox.Text.ToLower();

            // ищем запрос по поиску: фи клиента, фи сотрудника
            requests = requests.Where(c => (c.Employee != null &&
                                            (c.Employee.FirstName.ToLower().Contains(search) ||
                                             c.Employee.LastName.ToLower().Contains(search))) ||
                                           c.Customer.FirstName.ToLower().Contains(search) ||
                                           c.Customer.LastName.ToLower().Contains(search)).ToList();

            // если выбран статус не "все", то ищем заявки с выбранным статусом
            if (StatusComboBox.SelectedIndex > 0)
                requests = requests
                    .Where(c => c.StatusId == ((RequestStatus)StatusComboBox.SelectedItem).RequestStatusId).ToList();

            // если авторизирован клиент, то отображаем только его заявки
            if (App.AuthorizedUser.RoleId == 3)
                requests = requests.Where(c => c.Customer.Equals(App.AuthorizedUser)).ToList();

            // заново отображаем данные
            RequestDataGrid.ItemsSource = null;
            RequestDataGrid.ItemsSource = requests;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show($"Произошла ошибка: {e.Message}");
        }
    }

    /// <summary>
    /// Обработка события ввода в поисковую строку
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) => LoadData();

    /// <summary>
    /// Обработка события выбора статуса
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StatusComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => LoadData();

    /// <summary>
    /// Обработка события нажатия на кнопку "Подробнее"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void InfoButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new RequestInfoPage(((Button)sender).DataContext as Request));
}