using System.Windows;
using System.Windows.Controls;

namespace CarShowroom.Pages.EmployeePages;

public partial class EmployeeMenuPage : Page
{
    public EmployeeMenuPage()
    {
        InitializeComponent();
    }

    private void CarManageButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewCarPage());

    private void RequestManageButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewRequestPage());

    private void StatsButton_OnClick(object sender, RoutedEventArgs e) => throw new NotImplementedException();
}