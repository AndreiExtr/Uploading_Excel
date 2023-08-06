using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.OpenGL.Egl;
using Microsoft.VisualBasic;
using Programming;
//using Programming.Model;
using Excel = Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Office.Interop.Excel;
using Window = Avalonia.Controls.Window;
using Range = Microsoft.Office.Interop.Excel.Range;
using static System.Net.WebRequestMethods;
using System.Net.Http;

namespace Programming
{

    public partial class MainWindow : Window
    {

        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync("https://andreiextr.github.io/Uploading_Excel/");

            Console.WriteLine(response);
        }

        private ObservableCollection<MyClass> dataItems;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            dataItems = new ObservableCollection<MyClass>();
            MyDataGrid.Items = dataItems;
        }


        //ОСНОВНОЙ КОД
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (dataItems == null)
            {
                dataItems = new ObservableCollection<MyClass>();
            }
            dataItems.Add(
                new MyClass()
                {
                    First = "Введите текст",
                    Second = "Введите текст",
                    Third = "Введите текст",
                    Fourth = "Введите дату и время"
                }
            );
            MyDataGrid.Items = dataItems;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //ОСНОВНОЙ КОД
        
        private void ToExcelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            var dataGrid = MyDataGrid;

            //создание нового экземпляра
            Excel.Application excel = new Excel.Application();

            //создание новой рабочей книги Excel
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);

            //получение текущего листа Excel
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];


            //заполнение заголовков столбцов
            for (int i = 0; i < MyDataGrid.Columns.Count; i++)
            {
                //sheet1.Cells.Font.Bold = true;
                sheet1.Cells.Columns.ColumnWidth = 15;
                var columnHeaderText = dataGrid.Columns[i].Header.ToString();
                sheet1.Cells[1, i + 1].Value = columnHeaderText;
            }


            for (int j = 0; j < dataItems.Count; j++)
            {//для каждой строки данной таблицы...
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    //для каждого столбца данной таблицы... Записываю данные в ячейку начиная со 2 строки, 1 столбца
                    TextBlock b = dataGrid.Columns[i].GetCellContent(dataItems[j]) as TextBlock;
                    Range myRange = (Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;

                }
            }

            excel.Visible = true;
            excel.Quit();

        }

    }


    public class MyClass
    {
        public string First { get; set; }
        public string Second { get; set; }
        public string Third { get; set; }
        public string Fourth { get; set; }

    }
   

}
