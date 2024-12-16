using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages;

public partial class AuthPage : Page
{
    public AuthPage()
    {
        InitializeComponent();
    }

    private async void AuthButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string login = LoginBox.Text;
            string password = PasswordBox.Password;

            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                User? user =
                    await Db.Context.Users.FirstOrDefaultAsync(c =>
                        c.Login == login && c.Password == password && c.IsActive);

                if (user != null)
                {
                    switch (user.RoleId)
                    {
                        case 1:
                        {
                            NavigationService.Navigate(new AdminMenuPage());
                            break;
                        }
                        case 2:
                        {
                            break;
                        }
                        case 3:
                        {
                            break;
                        }
                    }

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
            throw;
        }
    }
}