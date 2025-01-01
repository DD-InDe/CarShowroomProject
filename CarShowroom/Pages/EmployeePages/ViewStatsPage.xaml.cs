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

    /// <summary>
    /// Метод для обработки события загрузки страницы
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ViewStatsPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // загружаем данные для марок, вставляем базовое значение "все"
            List<Brand> brands = new()
            {
                new() { Name = "Все" }
            };

            // добавляем прочие марки из базы 
            brands.AddRange(Db.Context.Brands.ToList());
            // добавляем эти данные в BrandComboBox
            BrandComboBox.ItemsSource = brands;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }

    /// <summary>
    /// Метод для обработки события нажатия на кнопку "Показать диаграмму"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LoadButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            // переменная с контрактами
            List<Contract> contracts;
            // переменная для диаграммы
            List<PieSeries> pieChartsValues = new();

            // если марка выбрана не "все", то данные отображаться будут по моделям
            if (BrandComboBox.SelectedIndex > 0)
            {
                // ищем контракты с авто конкретной марки
                contracts = Db.Context.Contracts
                    .Include(c => c.ContractNavigation)
                    .Include(c => c.ContractNavigation.Car)
                    .Include(c => c.ContractNavigation.Car.Model)
                    .Include(c => c.ContractNavigation.Car.Model.Brand)
                    .Where(c => c.ContractNavigation.Car.Model.BrandId == ((Brand)BrandComboBox.SelectedItem).BrandId)
                    .ToList();

                // группируем по моделям
                var groupedList = contracts.GroupBy(c => c.ContractNavigation.Car.Model).ToList();

                /*
                 * идем по циклу сгруппированных данных.
                 * Создаем новые дольки (для круговой диаграммы) и добавляем туда
                 * название (модель) и значение (кол-во) 
                 */
                foreach (var contract in groupedList)
                {
                    pieChartsValues.Add(new PieSeries
                    {
                        Title = contract.Key.Name,
                        Values = new ChartValues<int> { contract.Count() }
                    });
                }
            }
            // если не выбрана конкретная марка
            else
            {
                // выгружаем все контракты
                contracts = Db.Context.Contracts
                    .Include(c => c.ContractNavigation)
                    .Include(c => c.ContractNavigation.Car)
                    .Include(c => c.ContractNavigation.Car.Model)
                    .Include(c => c.ContractNavigation.Car.Model.Brand)
                    .ToList();

                 // группируем данные по марке
                var groupedList = contracts.GroupBy(c => c.ContractNavigation.Car.Model.Brand).ToList();

                /*
                 * идем по циклу сгруппированных данных.
                 * Создаем новые дольки (для круговой диаграммы) и добавляем туда
                 * название (марка) и значение (кол-во)
                 */
                foreach (var contract in groupedList)
                {
                    pieChartsValues.Add(new PieSeries
                    {
                        Title = contract.Key.Name,
                        Values = new ChartValues<int> { contract.Count() }
                    });
                }
            }

            // очищаем диаграмму от старых данных и загружаем новые
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