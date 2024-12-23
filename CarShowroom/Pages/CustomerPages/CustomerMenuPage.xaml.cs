using System.Windows;
using System.Windows.Controls;

namespace CarShowroom.Pages.CustomerPages;

public partial class CustomerMenuPage : Page
{
    public CustomerMenuPage()
    {
        InitializeComponent();
    }

    private void CarButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new ViewCarPage());

    private void RequestButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewRequestPage());
}