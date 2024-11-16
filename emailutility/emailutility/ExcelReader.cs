using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace emailutility
{
    class ExcelReader
    {
        XLWorkbook xlWorkBook;
        IXLWorksheet xlWorkSheet;
        Configuration c = Configuration.getInstance();
        public ExcelReader(string FileName)
        {
            xlWorkBook = new XLWorkbook(FileName);
        }

        public void OpenWorksheet(string SheetName)
        {
            if (!xlWorkBook.Worksheets.TryGetWorksheet(SheetName, out xlWorkSheet))
            {
                throw new Exception("Could not open worksheet");
            }
        }
        public string Read(int row, int column)
        {
            //return xlWorkSheet.Cell(row, column).Value.ToString();
            return xlWorkSheet.Cell(row, column).GetFormattedString();
        }

        public Dictionary<string, int> ReadHeader(int row, int column)
        {
            Dictionary<string, int> headerColumns = new Dictionary<string, int>();
            string value = Read(row, column);
            while (true)
            {
                if (string.IsNullOrEmpty(value))
                    break;

                if(value == c.InvoiceNO)
                    headerColumns.Add(c.InvoiceNO, column);
                else if (value == c.EmailAddress)
                    headerColumns.Add(c.EmailAddress, column);
                else if (value == c.SendInvoice)
                    headerColumns.Add(c.SendInvoice, column);
                else if (value == c.EmailSent)
                    headerColumns.Add(c.EmailSent, column);
                column++;
                value = Read(row, column);
            }
            return headerColumns;
        }

        public Dictionary<string, string> ReadLine(int row, Dictionary<string, int> headerColumns)
        {
            Dictionary<string, string> XLRow = new Dictionary<string, string>();

            bool nullRow = true;
            foreach(string key in headerColumns.Keys)
            {
                int column = headerColumns[key];
                string value = Read(row, column);
                if (!string.IsNullOrEmpty(value))
                {
                    nullRow = false;
                }
                XLRow.Add(key, value);
            }
            if (nullRow)
                return null;
            return XLRow;
        }

        public List<Dictionary<string, string>> ReadAllLines()
        {
            List<Dictionary<string, string>> XLRows = new List<Dictionary<string, string>>();
            int row = c.XLHeaderRow, column = c.XLHeaderColumn;
            Dictionary<string, int> headerColumns = ReadHeader(row, column);
            row++;
            while (true)
            {
                Dictionary<string, string> XLRow = ReadLine(row, headerColumns);
                if (XLRow is null)
                {
                    break;
                }
                XLRows.Add(XLRow);
                row++;
            }
            return XLRows;
        }

    }
}
