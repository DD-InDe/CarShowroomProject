using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;

namespace CarShowroom.Pages.EmployeePages;

public partial class EditCarPage : Page
{
    public EditCarPage()
    {
        InitializeComponent();
    }
    public EditCarPage(Car car)
    {
        InitializeComponent();
    }

    private void EditCarPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }
}