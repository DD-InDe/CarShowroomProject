using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using CarShowroom.Windows;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages;

public partial class RequestInfoPage : Page
{
    private Request _request;

    public RequestInfoPage(Request request)
    {
        _request = request;
        InitializeComponent();
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            _request.StatusId = 5;
            Db.Context.SaveChanges();

            MessageBox.Show("Заявка отменена");
            NavigationService.GoBack();
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

            MessageBox.Show("Заявка отклонена");
            NavigationService.GoBack();
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
            _request.Car.StatusId = 2;
            Db.Context.SaveChanges();

            MessageBox.Show("Заявка одобрена");
            NavigationService.GoBack();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void RequestInfoPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            switch (App.AuthorizedUser.RoleId)
            {
                case 1:
                case 2:
                    switch (_request.StatusId)
                    {
                        case 1:
                            _request.StatusId = 2;
                            _request.EmployeeId = App.AuthorizedUser.UserId;
                            Db.Context.SaveChanges();
                            Case2();
                            break;
                        case 2:
                            Case2();
                            break;
                        case 4:
                            if (Db.Context.Contracts.Find(_request.RequestId) == null)
                                ContractButton.Visibility = Visibility.Visible;
                            else
                                ContractShowButton.Visibility = Visibility.Visible;
                            break;
                    }

                    break;
                case 3:
                    switch (_request.StatusId)
                    {
                        case 1:
                        case 2:
                            CancelButton.Visibility = Visibility.Visible;
                            break;
                        case 4:
                            if (Db.Context.Contracts.Find(_request.RequestId) != null)
                                ContractShowButton.Visibility = Visibility.Visible;
                            break;
                    }

                    break;
            }

            DataContext = _request;
            SecondFrame.Navigate(new CarInfoPage(_request.Car, true));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void Case2()
    {
        RejectButton.Visibility = Visibility.Visible;
        AcceptButton.Visibility = Visibility.Visible;
    }

    private void ContractButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (Db.Context.Contracts.Find(_request.RequestId) == null)
            {
                ContractWindow window = new(_request);
                window.ShowDialog();
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void ContractShowButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            Contract contract = Db.Context.Contracts.Include(c => c.PaymentType)
                .First(c => c.ContractId == _request.RequestId);
            if (contract != null)
            {
                ContractWindow window = new(contract);
                window.ShowDialog();
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }
}