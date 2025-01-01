using System.Windows;
using System.Windows.Controls;
using CarShowroom.Pages.AdminsPages;
using CarShowroom.Pages.EmployeePages;
using CarShowroom.Windows;

namespace CarShowroom.Pages;

public partial class AdminMenuPage : Page
{
    public AdminMenuPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Обработка события нажатия кнопки "Управление пользователями"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UserManageButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new UserManagePage());

    /// <summary>
    /// Обработка события нажатия кнопки "Управление автомобилями"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CarManageButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewCarPage());

    /// <summary>
    /// Обработка события нажатия кнопки "Управление заявками"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RequestManageButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewRequestPage());

    /// <summary>
    /// Обработка события нажатия кнопки "Статистика"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StatsButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewStatsPage());

    /// <summary>
    /// Обработка события нажатия кнопки "Отчетность"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ReportButton_OnClick(object sender, RoutedEventArgs e)
    {
        PrintReportWindow window = new();
        window.ShowDialog();
    }
}