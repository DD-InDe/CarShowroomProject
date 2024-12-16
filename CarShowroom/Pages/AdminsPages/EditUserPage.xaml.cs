using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarShowroom.Database;

namespace CarShowroom.Pages.AdminsPages;

public partial class EditUserPage : Page
{
    private User _user;

    public EditUserPage()
    {
        _user = new();
        _user.Passport = new()
        {
            BirthDate = DateTime.Today,
            IssueDate = DateTime.Today
        };
        InitializeComponent();
    }

    public EditUserPage(User user)
    {
        _user = user;
        InitializeComponent();
    }

    private void EditUserPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            ChangeStatusButton.Content = _user.IsActive ? "Отключить учетную запись" : "Активировать учетную запись";
            BirthDatePicker.DisplayDateEnd = DateTime.Now;
            IssueDatePicker.DisplayDateEnd = DateTime.Now;
            RoleComboBox.ItemsSource = Db.Context.Roles.ToList();
            DataContext = _user;

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

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            SaveButton.IsEnabled = !SaveButton.IsEnabled;

            if (!String.IsNullOrEmpty(_user.LastName) && !String.IsNullOrEmpty(_user.FirstName) &&
                !String.IsNullOrEmpty(_user.Login) && !String.IsNullOrEmpty(_user.Password) &&
                _user.Passport.Serial.ToString().Length == 4 &&
                _user.Passport.Number.ToString().Length == 6 &&
                _user.Passport.BirthDate != null && _user.Passport.IssueDate != null &&
                _user.Role != null)
            {
                if (_user.UserId == 0)
                {
                    Db.Context.Passports.Add(_user.Passport);
                    Db.Context.SaveChanges();
                    Db.Context.Users.Add(_user);
                }

                Db.Context.SaveChanges();
                MessageBox.Show("Данные сохранены!");
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }

            SaveButton.IsEnabled = !SaveButton.IsEnabled;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void DisableSpace(object sender, KeyEventArgs e) => e.Handled = e.Key == Key.Space;

    private void NumberTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e) =>
        e.Handled = !char.IsDigit(e.Text, 0);

    private void ChangeStatusButton_OnClick(object sender, RoutedEventArgs e)
    {
        _user.IsActive = !_user.IsActive;
        ChangeStatusButton.Content = _user.IsActive ? "Отключить учетную запись" : "Активировать учетную запись";
    }
}