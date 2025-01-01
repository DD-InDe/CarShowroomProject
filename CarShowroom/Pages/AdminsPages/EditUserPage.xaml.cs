using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarShowroom.Database;

namespace CarShowroom.Pages.AdminsPages;

public partial class EditUserPage : Page
{
    // глобальная переменная с пользователем
    private User _user;


    /// <summary>
    /// Конструктор для добавления нового пользователя
    /// </summary>
    public EditUserPage()
    {
        // создаем новую переменную, задаем базовые данные
        _user = new();
        _user.Passport = new()
        {
            BirthDate = DateTime.Today,
            IssueDate = DateTime.Today
        };
        InitializeComponent();
    }

    /// <summary>
    /// Конструктор для редактирования пользователя
    /// </summary>
    /// <param name="user"></param>
    public EditUserPage(User user)
    {
        // Переменную из конструктора передаем в глобальную переменную
        _user = user;
        InitializeComponent();
    }

    /// <summary>
    /// Обработка события загрузки страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EditUserPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            /*
             * настраиваем текст кнопки
             * ChangeStatusButton.Content = пользователь активен ? (да) "Отключить учетную запись" : (нет) "Активировать учетную запись";
             */
            ChangeStatusButton.Content = _user.IsActive ? "Отключить учетную запись" : "Активировать учетную запись";
            // указываем границы для выбора даты
            BirthDatePicker.DisplayDateEnd = DateTime.Now;
            IssueDatePicker.DisplayDateEnd = DateTime.Now;
            // задаем данные для ComboBox из базы
            RoleComboBox.ItemsSource = Db.Context.Roles.ToList();
            // указываем привязку данных
            DataContext = _user;
            
            // если пользователь уже создан, то нельзя менять роль и статус
            if (App.AuthorizedUser.UserId == _user.UserId)
            {
                ChangeStatusButton.IsEnabled = false;
                RoleComboBox.IsEnabled = false;
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    /// <summary>
    /// Обработка события нажатия кнопки "Сохранить"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // выключаем кнопку, чтоб нельзя было заспамить
            SaveButton.IsEnabled = !SaveButton.IsEnabled;

            // проверка введенных данных на пустоту и длину
            if (!String.IsNullOrEmpty(_user.LastName) && !String.IsNullOrEmpty(_user.FirstName) &&
                !String.IsNullOrEmpty(_user.Login) && !String.IsNullOrEmpty(_user.Password) &&
                _user.Passport.Serial.ToString().Length == 4 &&
                _user.Passport.Number.ToString().Length == 6 &&
                _user.Passport.BirthDate != null && _user.Passport.IssueDate != null &&
                _user.Role != null)
            {
                // если пользователь не создан, то создаем
                if (_user.UserId == 0)
                {
                    Db.Context.Passports.Add(_user.Passport);
                    Db.Context.SaveChanges();
                    Db.Context.Users.Add(_user);
                }

                // сохраняем данные в базе
                Db.Context.SaveChanges();
                MessageBox.Show("Данные сохранены!");
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }

            // включаем кнопку
            SaveButton.IsEnabled = !SaveButton.IsEnabled;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод для запрета ввода пробелов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DisableSpace(object sender, KeyEventArgs e) => e.Handled = e.Key == Key.Space;

    /// <summary>
    /// Метод для разрешения ввода только цифр
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumberTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e) =>
        e.Handled = !char.IsDigit(e.Text, 0);

    /// <summary>
    /// Метод для обработки нажатия кнопки "Изменить статус"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ChangeStatusButton_OnClick(object sender, RoutedEventArgs e)
    {
        _user.IsActive = !_user.IsActive;
        ChangeStatusButton.Content = _user.IsActive ? "Отключить учетную запись" : "Активировать учетную запись";
    }
}