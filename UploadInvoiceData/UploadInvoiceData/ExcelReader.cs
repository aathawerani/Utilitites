using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace UploadMasterData
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

                /*if(value == c.PROVIDERNAME)
                    headerColumns.Add(c.PROVIDERNAME, column);
                else if (value == c.EmailAddress)
                    headerColumns.Add(c.EmailAddress, column);
                else if (value == c.CONTACTPERSON)
                    headerColumns.Add(c.CONTACTPERSON, column);
                else if (value == c.Street)
                    headerColumns.Add(c.Street, column);
                else if (value == c.CITY)
                    headerColumns.Add(c.CITY, column);
                else if (value == c.REGION)
                    headerColumns.Add(c.REGION, column);
                else if (value == c.ZipCode)
                    headerColumns.Add(c.ZipCode, column);
                else if (value == c.CONTACTNO)
                    headerColumns.Add(c.CONTACTNO, column);
                else if (value == c.Country)
                    headerColumns.Add(c.Country, column);
                else if (value == c.Currency)
                    headerColumns.Add(c.Currency, column);
                else if (value == c.ContractType)
                    headerColumns.Add(c.ContractType, column);
                else if (value == c.CREDITTERM)
                    headerColumns.Add(c.CREDITTERM, column);*/
                headerColumns.Add(value, column);
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
                XLRow.Add(c.ROW, row.ToString());
                XLRows.Add(XLRow);
                row++;
            }
            return XLRows;
        }

    }
}
