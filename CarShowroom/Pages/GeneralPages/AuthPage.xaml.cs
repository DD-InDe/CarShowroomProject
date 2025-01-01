using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using CarShowroom.Pages.CustomerPages;
using CarShowroom.Pages.EmployeePages;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages;

public partial class AuthPage : Page
{
    public AuthPage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Обработка нажатия кнопки "Войти"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void AuthButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Password;

            // проверка на пустоту
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                // ищем пользователя в базе
                User? user =
                    await Db.Context.Users.FirstOrDefaultAsync(c =>
                        c.Login == login && c.Password == password && c.IsActive);

                // если пользователь найден
                if (user != null)
                {
                    // распределение функционала по ролям
                    switch (user.RoleId)
                    {
                        // если админ, то открываем меню админа
                        case 1:
                        {
                            NavigationService.Navigate(new AdminMenuPage());
                            break;
                        }
                        // если сотрудник, то открываем меню сотрудника
                        case 2:
                        {
                            NavigationService.Navigate(new EmployeeMenuPage());
                            break;
                        }
                        // если клиент, то открываем меню клиента
                        case 3:
                        {
                            NavigationService.Navigate(new CustomerMenuPage());
                            break;
                        }
                    }

                    // сохраняем пользователя
                    App.AuthorizedUser = user;
                    MessageBox.Show("Вы вошли");
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show("Ошибка");
        }
    }
}