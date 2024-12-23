using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;

namespace CarShowroom.Pages;

public partial class RequestInfoPage : Page
{
    private Request _request;

    public RequestInfoPage(Request request)
    {
        InitializeComponent();
    }

    private void RequestInfoPage_OnLoaded(object sender, RoutedEventArgs e)
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