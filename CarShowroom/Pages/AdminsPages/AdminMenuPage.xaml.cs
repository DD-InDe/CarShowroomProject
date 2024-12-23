using System.Windows;
using System.Windows.Controls;
using CarShowroom.Pages.AdminsPages;

namespace CarShowroom.Pages;

public partial class AdminMenuPage : Page
{
    public AdminMenuPage()
    {
        InitializeComponent();
    }

    private void UserManageButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new UserManagePage());

    private void CarManageButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewCarPage());

    private void RequestManageButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewRequestPage());

    private void StatsButton_OnClick(object sender, RoutedEventArgs e) => throw new NotImplementedException();
}