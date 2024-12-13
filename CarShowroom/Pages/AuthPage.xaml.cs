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
            // string login = LoginBox.Text;
            // string password = PasswordBox.Password;
            //
            // if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            // {
            //     Employee? employee =
            //         await Db.Context.Employees.Include(c => c.Role)
            //             .FirstOrDefaultAsync(c => c.Login == login && c.Password == password);
            //     Client? client =
            //         await Db.Context.Clients.FirstOrDefaultAsync(c => c.Login == login && c.Password == password);
            //
            //     if (employee != null || client != null)
            //     {
            //         SessionUser user = new();
            //         if (employee != null)
            //         {
            //             user.FirstName = employee.FirstName;
            //             user.LastName = employee.LastName;
            //             user.MiddleName = employee.MiddleName;
            //             user.Role = employee.Role.Name;
            //             user.Id = employee.EmployeeId;
            //         }
            //
            //         if (client != null)
            //         {
            //             user.FirstName = client.FirstName;
            //             user.LastName = client.LastName;
            //             user.MiddleName = client.MiddleName;
            //             user.Role = "Клиент";
            //             user.Id = client.ClientId;
            //         }
            //
            //         switch (user.Role)
            //         {
            //             case "Администратор":
            //             {
            //                 NavigationService.Navigate(new AdminMenuPage());
            //                 break;
            //             }
            //         }
            //
            //         App.User = user;
            //         MessageBox.Show("Вы вошли");
            //     }
            //     else
            //     {
            //         MessageBox.Show("Неверный логин или пароль");
            //     }
            // }
            // else
            // {
            //     MessageBox.Show("Заполните все поля");
            // }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}