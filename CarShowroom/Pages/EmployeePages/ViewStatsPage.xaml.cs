using System.Windows;
using System.Windows.Controls;
using CarShowroom.Database;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;

namespace CarShowroom.Pages.EmployeePages;

public partial class ViewStatsPage : Page
{
    public ViewStatsPage()
    {
        InitializeComponent();
    }

    private void ViewStatsPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Brand> brands = new()
            {
                new() { Name = "Все" }
            };

            brands.AddRange(Db.Context.Brands.ToList());
            BrandComboBox.ItemsSource = brands;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    private void LoadButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Contract> contracts;
            List<PieSeries> pieChartsValues = new();

            if (BrandComboBox.SelectedIndex > 0)
            {
                contracts = Db.Context.Contracts
                    .Include(c => c.ContractNavigation)
                    .Include(c => c.ContractNavigation.Car)
                    .Include(c => c.ContractNavigation.Car.Model)
                    .Include(c => c.ContractNavigation.Car.Model.Brand)
                    .Where(c => c.ContractNavigation.Car.Model.BrandId == ((Brand)BrandComboBox.SelectedItem).BrandId)
                    .ToList();
                
                var groupedList = contracts.GroupBy(c => c.ContractNavigation.Car.Model).ToList();

                foreach (var contract in groupedList)
                {
                    pieChartsValues.Add(new PieSeries
                    {
                        Title = contract.Key.Name,
                        Values = new ChartValues<int> { contract.Count() }
                    });
                }
            }
            else
            {
                contracts = Db.Context.Contracts
                    .Include(c => c.ContractNavigation)
                    .Include(c => c.ContractNavigation.Car)
                    .Include(c => c.ContractNavigation.Car.Model)
                    .Include(c => c.ContractNavigation.Car.Model.Brand)
                    .ToList();
                
                var groupedList = contracts.GroupBy(c => c.ContractNavigation.Car.Model.Brand).ToList();

                foreach (var contract in groupedList)
                {
                    pieChartsValues.Add(new PieSeries
                    {
                        Title = contract.Key.Name,
                        Values = new ChartValues<int> { contract.Count() }
                    });
                }
            }

            PieChart.Series.Clear();
            PieChart.Series.AddRange(pieChartsValues);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }
}