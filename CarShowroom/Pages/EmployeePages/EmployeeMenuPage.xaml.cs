using System.Windows;
using System.Windows.Controls;
using CarShowroom.Windows;

namespace CarShowroom.Pages.EmployeePages;

public partial class EmployeeMenuPage : Page
{
    public EmployeeMenuPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Метод для обработки события нажатия на кнопку "Управление автомобилями"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CarManageButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewCarPage());

    /// <summary>
    /// Метод для обработки события нажатия на кнопку "Управление автомобилями"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RequestManageButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewRequestPage());

    /// <summary>
    /// Метод для обработки события нажатия на кнопку "Статистика"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StatsButton_OnClick(object sender, RoutedEventArgs e) =>
        NavigationService.Navigate(new ViewStatsPage());

    /// <summary>
    /// Метод для обработки события нажатия на кнопку "Отчетность"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ReportButton_OnClick(object sender, RoutedEventArgs e)
    {
        PrintReportWindow window = new();
        window.ShowDialog();
    }
}