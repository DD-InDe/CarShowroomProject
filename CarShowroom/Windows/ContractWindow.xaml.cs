using System.ComponentModel;
using System.Windows;
using CarShowroom.Database;

namespace CarShowroom.Windows;

public partial class ContractWindow : Window
{
    private Request _request;
    private Contract _contract;

    public ContractWindow(Request request)
    {
        _request = request;
        InitializeComponent();

        NewContractPanel.Visibility = Visibility.Visible;

        Contract contract = new()
        {
            ContractId = request.RequestId,
        };
        _contract = contract;

        PaymentTypeComboBox.ItemsSource = Db.Context.PaymentTypes.ToList();
        DataContext = _contract;
    }

    public ContractWindow(Contract contract)
    {
        InitializeComponent();

        ViewContractPanel.Visibility = Visibility.Visible;
        DataContext = contract;
    }

    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (PaymentTypeComboBox.SelectedItem != null)
            {
                _contract.DateCreate = DateTime.Now;

                _request.Car.StatusId = 3;
                Db.Context.Contracts.Add(_contract);
                Db.Context.SaveChanges();

                MessageBox.Show("Контракт добавлен");
                Close();
            }
            else
                MessageBox.Show("Поля не могут быть пустыми");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void CancelButton_OnClick(object sender, RoutedEventArgs e) => this.Close();
}