using ClosedXML.Excel;
using System.Configuration;

namespace GenerateQR.Helpers
{
    public class ExcelReader
    {
        XLWorkbook xlWorkBook;
        IXLWorksheet xlWorkSheet;
        public ExcelReader(string FileName)
        {
            xlWorkBook = new XLWorkbook(FileName);
        }

        public ExcelReader(Stream stream)
        {
            xlWorkBook = new XLWorkbook(stream);
        }


        public void OpenWorksheet(string SheetName)
        {
            if (!xlWorkBook.Worksheets.TryGetWorksheet(SheetName, out xlWorkSheet))
            {
                throw new Exception("Could not open worksheet");
            }
        }
        public string Read(int row, int column, bool DTtype)
        {
            string value = "";
            //if (!DTtype)
                //value = xlWorkSheet.Cell(row, column).Value.ToString();
            //else
                value = xlWorkSheet.Cell(row, column).GetFormattedString();
            return value;
        }

        public Dictionary<string, string> ReadLine(int row, Dictionary<string, int> headerColumns, Dictionary<string, Dictionary<string, string>> MainExcelColumnsName)
        {
            Dictionary<string, string> XLRow = new Dictionary<string, string>();

            bool nullRow = true;
            foreach (string key in headerColumns.Keys)
            {
                int column = headerColumns[key];
                bool DTtype = false;
                if (MainExcelColumnsName.Keys.Contains(key) && MainExcelColumnsName[key]["Type"] == "DateTime")
                    DTtype = true;
                string value = Read(row, column, DTtype);
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

        public List<Dictionary<string, string>> ReadAllLines(Dictionary<string, Dictionary<string, string>> MainExcelColumnsName)
        {
            List<Dictionary<string, string>> XLRows = new List<Dictionary<string, string>>();
            int row = 1, column = 1;
            Dictionary<string, int> headerColumns = ReadHeader(row, column);
            row++;
            while (true)
            {
                Dictionary<string, string> XLRow = ReadLine(row, headerColumns, MainExcelColumnsName);
                if (XLRow is null)
                {
                    break;
                }
                XLRows.Add(XLRow);
                row++;
            }
            return XLRows;
        }

        public Dictionary<string, int> ReadHeader(int row, int column)
        {
            Dictionary<string, int> headerColumns = new Dictionary<string, int>();
            string value = Read(row, column, false);
            while (true)
            {
                if (string.IsNullOrEmpty(value))
                    break;
                headerColumns.Add(value, column);
                column++;
                value = Read(row, column, false);
            }
            return headerColumns;
        }


    }
}
