using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using CarShowroom.Windows;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages;

public partial class RequestInfoPage : Page
{
    // глобальная переменная
    private Request _request;

    /// <summary>
    ///  конструктор страницы
    /// </summary>
    /// <param name="request"></param>
    public RequestInfoPage(Request request)
    {
        // передаем значение переменной из конструктора в глобальную переменную
        _request = request;
        InitializeComponent();
    }

    /// <summary>
    /// Обработка события нажатия кнопки "Отмена"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // задаем статус "Отменена"
            _request.StatusId = 5;
            Db.Context.SaveChanges();

            MessageBox.Show("Заявка отменена");
            // переходим назад
            NavigationService.GoBack();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    /// <summary>
    /// Обработка события нажатия кнопки "Отклонить"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RejectButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // задаем статус "Отклонена"
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

    /// <summary>
    /// Обработка события нажатия кнопки "Одобрить"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AcceptButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // задаем заявке статус "Одобрена"
            _request.StatusId = 4;
            // машине задаем статус "В заявке"
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

    /// <summary>
    /// Обработка события загрузки страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void RequestInfoPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // в зависимости от роли авторизированного пользователя, распределяем функционал
            switch (App.AuthorizedUser.RoleId)
            {
                /*
                 * если админ или сотрдуник, то:
                 * (1) если статус заявки "новая", то можно одобрить и отклонить заявку,
                 * также заявке присваиваться текущий авторизированный пользователь в качестве сотрудника
                 * (2) если статус заявки "на рассмотрении", то тоже самое что и (1), но без присваивания сотрудника
                 * (4) если статус заявки "одобрена" и есть договор, то договор можно посмотреть, иначе сделать его
                 */
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
                /*
                 * если клиент, то:
                 * (1,2) если статус заявки "новая" или "на рассмотрении", то можно отменить заявку
                 * (4) если статус заявки "одобрена" и есть договор, то можно посмотреть договор
                 */
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

            // прописываем привязку данных
            DataContext = _request;
            // показываем информацию по автомобилю
            SecondFrame.Navigate(new CarInfoPage(_request.Car, true));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    /// <summary>
    ///  просто отображаем кнопочки
    /// </summary>
    private void Case2()
    {
        RejectButton.Visibility = Visibility.Visible;
        AcceptButton.Visibility = Visibility.Visible;
    }

    /// <summary>
    /// Обработка события нажатия на кнопку "Добавить контракт"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ContractButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // проверяем, что контракта нет
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

    /// <summary>
    /// Обработка события нажатия на кнопку "Посмотреть контракт"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ContractShowButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            Contract contract = Db.Context.Contracts.Include(c => c.PaymentType)
                .First(c => c.ContractId == _request.RequestId);
            // проверяем, что контракт есть
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