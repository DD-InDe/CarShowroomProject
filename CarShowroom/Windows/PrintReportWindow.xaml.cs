using System.Windows;
using CarShowroom.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace CarShowroom.Windows;

public partial class PrintReportWindow : Window
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool AllTime { get; set; }


    /// <summary>
    /// Конструктор окна
    /// </summary>
    public PrintReportWindow()
    {
        InitializeComponent();

        // указываем в качестве стартовой даты самую первую дату из базы
        StartDatePicker.DisplayDateStart =
            Db.Context.Contracts.OrderBy(c => c.DateOfTransaction).First().DateOfTransaction;
        EndDatePicker.DisplayDateStart = StartDatePicker.DisplayDateStart;

        // делаем привязку данных
        DataContext = this;
    }

    /// <summary>
    /// Обработка события нажатия кнопки "Печать"
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PrintButton_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string title = "Отчет по проданным автомобилям";
            List<Contract> contracts;

            // если стоит галочка на "За все время"
            if (!AllTime)
            {
                // проверка на пустоту
                if (StartDate != null)
                {
                    // проверка на пустоту
                    if (EndDate != null)
                    {
                        title +=
                            $" за промежуток {StartDate.Value.Date:d} - {EndDate.Value:d}.";

                        // в лист добавляем данные из базы на основе введенных данных
                        contracts = Db.Context.Contracts
                            .Include(c => c.ContractNavigation)
                            .Include(c => c.ContractNavigation.Car)
                            .Include(c => c.ContractNavigation.Customer)
                            .Include(c => c.ContractNavigation.Employee)
                            .Include(c => c.ContractNavigation.Car.Model)
                            .Include(c => c.ContractNavigation.Car.Model.Brand)
                            .Include(c => c.ContractNavigation.Car.Model.Class)
                            .Include(c => c.ContractNavigation.Car.Model.BodyType)
                            .Where(c => c.DateOfTransaction >= StartDate.Value || c.DateOfTransaction <= EndDate.Value)
                            .ToList();
                    }
                    else
                    {
                        title +=
                            $" за промежуток {StartDate.Value.Date:d} - {DateTime.Now:d}.";

                        // в лист добавляем данные из базы на основе введенных данных
                        contracts = Db.Context.Contracts
                            .Include(c => c.ContractNavigation)
                            .Include(c => c.ContractNavigation.Car)
                            .Include(c => c.ContractNavigation.Customer)
                            .Include(c => c.ContractNavigation.Employee)
                            .Include(c => c.ContractNavigation.Car.Model)
                            .Include(c => c.ContractNavigation.Car.Model.Brand)
                            .Include(c => c.ContractNavigation.Car.Model.Class)
                            .Include(c => c.ContractNavigation.Car.Model.BodyType)
                            .Where(c => c.DateOfTransaction >= StartDate.Value)
                            .ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Начальная дата не может быть пустой!");
                    return;
                }
            }
            else
            {
                title += " за всё время.";

                // в лист добавляем данные из базы данных
                contracts = Db.Context.Contracts
                    .Include(c => c.ContractNavigation)
                    .Include(c => c.ContractNavigation.Customer)
                    .Include(c => c.ContractNavigation.Employee)
                    .Include(c => c.ContractNavigation.Car)
                    .Include(c => c.ContractNavigation.Car.Model)
                    .Include(c => c.ContractNavigation.Car.Model.Brand)
                    .Include(c => c.ContractNavigation.Car.Model.Class)
                    .Include(c => c.ContractNavigation.Car.Model.BodyType)
                    .ToList();
            }

            // проверка на пустоту листа
            if (contracts.Count > 0)
            {
                // Просим пользователя выбрать место для хранения файла. Иначе будет сохраняться в bin/debug/net8.0/
                SaveFileDialog dialog = new();
                string filePath = "temp.docx";
                if (dialog.ShowDialog() == true)
                    filePath = dialog.FileName;

                // создаем новый документ
                DocX doc = DocX.Create(filePath);
                // добавляем в документ параграф
                Paragraph p1 = doc.InsertParagraph();
                // вставляем название отчета
                p1.Append(title).Font("Times New Roman").FontSize(18);

                // добавляем в документ параграф
                Paragraph p2 = doc.InsertParagraph();
                // добавляем в документ таблицу под количество элементов в листе
                Table table = p2.InsertTableAfterSelf(contracts.Count + 1, 5);

                // в таблицу прописываем заголовки к столбцам
                table.Rows[0].Cells[0].Paragraphs[0].Font("Times New Roman").FontSize(14).Append("№");
                table.Rows[0].Cells[1].Paragraphs[0].Font("Times New Roman").FontSize(14).Append("Клиент");
                table.Rows[0].Cells[2].Paragraphs[0].Font("Times New Roman").FontSize(14).Append("Сотрудник");
                table.Rows[0].Cells[3].Paragraphs[0].Font("Times New Roman").FontSize(14).Append("Автомобиль");
                table.Rows[0].Cells[4].Paragraphs[0].Font("Times New Roman").FontSize(14).Append("Дата сделки");

                // идем по всем элементам листа
                for (var i = 1; i < contracts.Count + 1; i++)
                {
                    // получаем текущий элемент
                    Contract contract = contracts[i - 1];
                    // вставляем номер записи
                    table.Rows[i].Cells[0].Paragraphs[0].Font("Times New Roman").Append(i.ToString());

                    // вставляем фио клиента
                    table.Rows[i].Cells[1].Paragraphs[0].Font("Times New Roman")
                        .Append(contract.ContractNavigation.Customer.FullName);
                    // вставляем фио сотрудника
                    table.Rows[i].Cells[2].Paragraphs[0].Font("Times New Roman")
                        .Append(contract.ContractNavigation.Employee.FullName);
                    string carName =
                        $"{contract.ContractNavigation.Car.Model.Brand.Name} {contract.ContractNavigation.Car.Model.Name}";
                    // вставляем название авто (марка + модель)
                    table.Rows[i].Cells[3].Paragraphs[0].Font("Times New Roman").Append(carName);
                    // вставляем дату сделки
                    table.Rows[i].Cells[4].Paragraphs[0].Font("Times New Roman")
                        .Append(contract.DateOfTransaction.Value.ToString("d"));
                }

                // добавляем в документ параграф
                Paragraph p3 = doc.InsertParagraph();
                // вставляем в параграф текст
                p3.Font("Times New Roman").AppendLine($"Сотрудник составивший отчет: {App.AuthorizedUser.FullName}");
                p3.Font("Times New Roman").AppendLine($"Дата составления отчета: {DateTime.Now:d}");
                doc.Save();

                MessageBox.Show("Файл сохранен");
            }
            else
                MessageBox.Show("За данный диапазон времени не было сделок");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            MessageBox.Show($"Произошла ошибка: {exception.Message}");
        }
    }
}