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

    private void ViewRequestPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            List<RequestStatus> statusList = new() { new() { Name = "Все" } };
            statusList.AddRange(Db.Context.RequestStatuses.ToList());
            StatusComboBox.ItemsSource = statusList;
            
            LoadData();
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
            List<Request> requests = Db.Context.Requests
                .Include(c => c.Status)
                .Include(c => c.Car)
                .Include(c => c.Customer)
                .Include(c => c.Employee)
                .ToList();

            string search = SearchTextBox.Text.ToLower();

            requests = requests.Where(c =>
                c.Employee.FirstName.ToLower().Contains(search) ||
                c.Employee.LastName.ToLower().Contains(search) ||
                c.Customer.LastName.ToLower().Contains(search) ||
                c.Customer.LastName.ToLower().Contains(search)).ToList();

            if (StatusComboBox.SelectedIndex > 0)
                requests = requests
                    .Where(c => c.StatusId == ((RequestStatus)StatusComboBox.SelectedItem).RequestStatusId).ToList();

            if (App.AuthorizedUser.RoleId == 3)
                requests = requests.Where(c => c.Customer.Equals(App.AuthorizedUser)).ToList();

            RequestDataGrid.ItemsSource = null;
            RequestDataGrid.ItemsSource = requests;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show($"Произошла ошибка: {e.Message}");
        }
    }

    private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) => LoadData();

    private void StatusComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => LoadData();

    private void InfoButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new RequestInfoPage(((Button)sender).DataContext as Request));
}