using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using System.Data;

namespace JENCORECORP
{
    /// <summary>
    /// Interaction logic for MutantBulkImport.xaml
    /// </summary>
    public partial class MutantBulkImport : UserControlClass
    {
        static string ExcelPath;
        public MutantBulkImport()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog odlg = new OpenFileDialog();
            if (odlg.ShowDialog() == true)
            {
                ExcelPath = odlg.FileName;

            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

        }


        public List<Mutants> excelread(string fileName)
        {
            List<Mutants> MutantList;
            Workbook workBook;
            SharedStringTable sharedStrings;
            IEnumerable<Sheet> workSheets;
            WorksheetPart custSheet;
            WorksheetPart orderSheet;

            //Declare helper variables.
            string custID;
            string orderID;

            MutantList = new List<Mutants>();
            //Open the Excel workbook.
            using (SpreadsheetDocument document =
              SpreadsheetDocument.Open(fileName, true))
            {
                //References to the workbook and Shared String Table.
                workBook = document.WorkbookPart.Workbook;
                workSheets = workBook.Descendants<Sheet>();
                sharedStrings =
                  document.WorkbookPart.SharedStringTablePart.SharedStringTable;

                //Reference to Excel Worksheet with Customer data.
                custID =
                  workSheets.First(s => s.Name == @"root").Id;
                custSheet =
                  (WorksheetPart)document.WorkbookPart.GetPartById(custID);

                //        IEnumerable<Row> dataRows =
                //from row in custSheet.Worksheet.Descendants<Row>()
                //where row.RowIndex > 0
                //select row;

                DataTable dataTable = new DataTable();
                IEnumerable<Row> dataRows = custSheet.Worksheet.Descendants<Row>();
                //foreach (Cell cell in dataRows.ElementAt(0))
                //{
                //    dataTable.Columns.Add(GetCellValue(document, cell));
                //}

                foreach (Row row in dataRows)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                    {
                        dataRow[i] = GetCellValue(document, row.Descendants<Cell>().ElementAt(i));
                    }

                    dataTable.Rows.Add(dataRow);
                }
                foreach (Row row in dataRows)
                {
                    //LINQ query to return the row's cell values.
                    //Where clause filters out any cells that do not contain a value.
                    //Select returns the value of a cell unless the cell contains
                    //  a Shared String.
                    //If the cell contains a Shared String, its value will be a 
                    //  reference id which will be used to look up the value in the 
                    //  Shared String table.
                    IEnumerable<String> textValues =
                      from cell in row.Descendants<Cell>()
                      where cell.CellValue != null
                      select
                        (cell.DataType != null
                          && cell.DataType.HasValue
                          && cell.DataType == CellValues.SharedString
                        ? sharedStrings.ChildElements[
                          int.Parse(cell.CellValue.InnerText)].InnerText
                        : cell.CellValue.InnerText)
                      ;

                    //Check to verify the row contained data.
                    if (textValues.Count() > 0)
                    {
                        //Create a customer and add it to the list.
                        var textArray = textValues.ToArray();

                    }
                    else
                    {
                        //If no cells, then you have reached the end of the table.
                        break;
                    }
                }
            }

            return MutantList;
        }

        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string value = cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }
    }
}
