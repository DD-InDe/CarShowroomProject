using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;

namespace CarShowroom.Pages;

public partial class RequestInfoPage : Page
{
    private Request _request;

    public RequestInfoPage(Request request)
    {
        _request = request;
        InitializeComponent();

        switch (App.AuthorizedUser.RoleId)
        {
            case 1:
            case 2:
                _request.StatusId = 2;
                _request.EmployeeId = App.AuthorizedUser.UserId;
                Db.Context.SaveChanges();
                if (request.StatusId == 1)
                {
                    RejectButton.Visibility = Visibility.Visible;
                    AcceptButton.Visibility = Visibility.Visible;
                }

                break;
            case 3:
                if (request.StatusId == 1)
                    CancelButton.Visibility = Visibility.Visible;
                break;
        }
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _request.StatusId = 5;
            Db.Context.SaveChanges();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void RejectButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _request.StatusId = 3;
            Db.Context.SaveChanges();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void AcceptButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _request.StatusId = 4;
            Db.Context.SaveChanges();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }
}