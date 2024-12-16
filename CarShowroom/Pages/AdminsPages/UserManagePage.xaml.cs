using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages.AdminsPages;

public partial class UserManagePage : Page
{
    private List<User>? _users;

    public UserManagePage()
    {
        InitializeComponent();
    }

    private void EditButton_OnClick(object sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new EditUserPage(((Button)sender).DataContext as User));
    }

    private void UserManagePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Role> roles = new() { new() { Name = "Все" } };
            roles.AddRange(Db.Context.Roles.ToList());
            FilterComboBox.ItemsSource = roles;
            _users = Db.Context.Users.Include(c => c.Passport).Include(c => c.Role).ToList();
            UserDataGrid.ItemsSource = _users;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show("Произошла ошибка");
        }
    }

    private void FilterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => LoadData();

    private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) => LoadData();

    private void LoadData()
    {
        try
        {
            _users = Db.Context.Users.Include(c => c.Passport).Include(c => c.Role).ToList();
            _users = _users.Where(c =>
                c.LastName.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                c.FirstName.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                c.MiddleName.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            if (FilterComboBox.SelectedIndex != 0)
                _users = _users.Where(c => c.RoleId == ((Role)FilterComboBox.SelectedItem).RoleId).ToList();

            UserDataGrid.ItemsSource = null;
            UserDataGrid.ItemsSource = _users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show("Произошла ошибка");
        }
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new EditUserPage());
}