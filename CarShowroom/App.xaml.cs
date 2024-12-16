using System.Configuration;
using System.Data;
using System.Windows;
using CarShowroom.Database;

namespace CarShowroom;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static User? AuthorizedUser;
}