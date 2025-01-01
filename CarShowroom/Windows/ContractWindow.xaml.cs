using System.ComponentModel;
using System.Windows;
using CarShowroom.Database;

namespace CarShowroom.Windows;

/// <summary>
/// Окно для добавления нового контракта
/// к одобренной заявке
/// </summary>
public partial class ContractWindow : Window
{
    private Request _request;
    private Contract _contract;

  
    public ContractWindow(Request request)
    {
        // передаем в глобальную переменную значение из конструктора
        _request = request;
        InitializeComponent();

        // отображаем панель для ввода данных 
        NewContractPanel.Visibility = Visibility.Visible;

        // создаем новый контракт
        Contract contract = new()
        {
            ContractId = request.RequestId,
        };

        // передаем в глобальную переменную значение
        _contract = contract;

        // в ComboBox передаем данные из базы (тип оплаты)
        PaymentTypeComboBox.ItemsSource =
            Db.Context.PaymentTypes.ToList();
        // задаем разметке привязку данных
        DataContext = _contract;
    }

    /// <summary>
    /// Конструктор окна при просмотре контракта
    /// </summary>
    /// <param name="contract"></param>
    public ContractWindow(Contract contract)
    {
        InitializeComponent();

        // отображаем панель для просмотра контракта
        ViewContractPanel.Visibility = Visibility.Visible;
        // задаем разметке привязку данных
        DataContext = contract;
    }

    /// <summary>
    /// Обработка события нажатия кнопки "Добавить"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // проверяем на пустоту
            if (PaymentTypeComboBox.SelectedItem != null && TransactionDatePicker.SelectedDate != null)
            {
                // задаем контракту дата создания сегодняшнюю
                _contract.DateCreate = DateTime.Now;

                // задаем автомобилю статус "продан"
                _request.Car.StatusId = 3;
                // добавляем в базу новый контракт
                Db.Context.Contracts.Add(_contract);
                Db.Context.SaveChanges();

                MessageBox.Show("Контракт добавлен");
                // закрываем окно
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

    /// <summary>
    /// Обработка события нажатия кнопки "Отмена" 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelButton_OnClick(object sender, RoutedEventArgs e) => this.Close();
}