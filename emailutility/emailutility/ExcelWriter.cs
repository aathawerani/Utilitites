using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emailutility
{
    class ExcelWriter
    {
        XLWorkbook xlWorkBook;
        IXLWorksheet xlWorkSheet;
        string FileName;
        public ExcelWriter(string _FileName)
        {
            FileName = _FileName;
        }
        public void OpenWorksheet(string SheetName)
        {
            xlWorkBook = new XLWorkbook(FileName);
            if (xlWorkBook == null)
            {
                throw new Exception("Could not open excel work book");
            }
            xlWorkSheet = xlWorkBook.Worksheet(SheetName);
            if (xlWorkSheet == null)
            {
                throw new Exception("Could not open excel sheet");
            }
        }
        public void Write(int row, int column, string value)
        {
            xlWorkSheet.Cell(row, column).Value = value;
        }
        public void Save()
        {
            xlWorkBook.Save();
        }
    }
}
