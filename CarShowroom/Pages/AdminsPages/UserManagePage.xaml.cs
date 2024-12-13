using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;

namespace CarShowroom.Pages.AdminsPages;

public partial class UserManagePage : Page
{
    // private List<User> _users;
    
    public UserManagePage()
    {
        InitializeComponent();
    }

    private void EditButton_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void UserManagePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // _users = new();
            //
            // UserDataGrid.ItemsSource = 
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}