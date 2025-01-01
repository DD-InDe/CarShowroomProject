using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages.AdminsPages;

public partial class UserManagePage : Page
{
    // глобальная переменная
    private List<User>? _users;

    public UserManagePage()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Метод для обработки события нажатия кнопки "Редактировать"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void EditButton_OnClick(object sender, RoutedEventArgs e)
    {
        // Открываем страницу для редактирования пользователя, данные пользователя берем из привязки
        NavigationService.Navigate(new EditUserPage(((Button)sender).DataContext as User));
    }

    /// <summary>
    /// Метод для обработки события загрузки страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UserManagePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // создаем лист с ролями для фильтрации по роли, задаем начальное значение "все"
            List<Role> roles = new() { new() { Name = "Все" } };
            // добавляем прочие роли из базы данных
            roles.AddRange(Db.Context.Roles.ToList());
            // задаем этот лист в ComboBox
            FilterComboBox.ItemsSource = roles;
            // загружаем пользователей из базы в переменную и задаем таблице UserDataGrid
            _users = Db.Context.Users.Include(c => c.Passport).Include(c => c.Role).ToList();
            UserDataGrid.ItemsSource = _users;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show("Произошла ошибка");
        }
    }

    /// <summary>
    /// Обработка события изменения выбранного фильтра
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void FilterComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) => LoadData();
    
    /// <summary>
    /// Обработка события изменения текст в строке поиска
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e) => LoadData();

    /// <summary>
    /// Метод для загрузки данных
    /// </summary>
    private void LoadData()
    {
        try
        {
            // загружаем данные из базы на основе выбранных параметров (поиск и фильтрация)
            _users = Db.Context.Users.Include(c => c.Passport).Include(c => c.Role).ToList();
            _users = _users.Where(c =>
                c.LastName.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                c.FirstName.ToLower().Contains(SearchTextBox.Text.ToLower()) ||
                c.MiddleName.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();
            if (FilterComboBox.SelectedIndex != 0)
                _users = _users.Where(c => c.RoleId == ((Role)FilterComboBox.SelectedItem).RoleId).ToList();

            // заново задаем UserDataGrid данные
            UserDataGrid.ItemsSource = null;
            UserDataGrid.ItemsSource = _users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageBox.Show("Произошла ошибка");
        }
    }
    
    /// <summary>
    /// Обработка события нажатия кнопки "Сохранить"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddButton_OnClick(object sender, RoutedEventArgs e) => NavigationService.Navigate(new EditUserPage());
}