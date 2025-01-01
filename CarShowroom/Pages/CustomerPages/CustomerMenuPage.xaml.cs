using System.Windows;
using System.Windows.Controls;

namespace CarShowroom.Pages.CustomerPages;

public partial class CustomerMenuPage : Page
{
    public CustomerMenuPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Обработка события нажатия кнопки "Список автомобилей"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CarButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new ViewCarPage());

    /// <summary>
    /// Обработка события нажатия кнопки "Мои заявки"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RequestButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewRequestPage());
}