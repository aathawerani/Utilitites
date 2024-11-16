using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emailutility
{
    class BusinessLogic
    {
        List<Dictionary<string, string>> data;
        Dictionary<string, int> headerColumns;
        Configuration c = Configuration.getInstance();
        public void ReadExcel()
        {
            ExcelReader reader = new ExcelReader(c.XLfileName);
            reader.OpenWorksheet(c.XLSheetName);
            data = reader.ReadAllLines();
            int row = c.XLHeaderRow, column = c.XLHeaderColumn;
            headerColumns = reader.ReadHeader(row, column);
        }

        public void SendEmail()
        {
            for (int i=0; i<data.Count; i++)
            {
                Dictionary<string, string> item = data[i];
                if (item[c.SendInvoice] == c.SendInvoiceYes && item[c.EmailSent] != c.EmailSentYes)
                {
                    string attachmentFilePath = c.PDFfilePath + item[c.InvoiceNO] + c.PDFext;

                    string smtpServer = c.smtpServer;
                    int smtpPort = c.smtpPort;
                    string senderEmail = c.senderEmail;
                    string senderPassword = c.senderPassword;
                    string recipientEmail = item[c.EmailAddress];
                    string subject = c.subject + item[c.InvoiceNO];
                    string body = c.body;

                    try
                    {
                        EmailSender emailSender = new EmailSender();
                        emailSender.Send(senderEmail, recipientEmail, subject, body, attachmentFilePath, smtpServer, smtpPort, senderPassword);
                        data[i][c.EmailSent] = c.EmailSentYes;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error sending email: " + ex.Message);
                    }
                }
            }
        }

        public void UpdateExcel()
        {
            ExcelWriter writer = new ExcelWriter(c.XLfileName);
            writer.OpenWorksheet(c.XLSheetName);

            for (int i = 0; i < data.Count; i++)
            {
                Dictionary<string, string> item = data[i];
                if (item[c.EmailSent] == c.EmailSentYes)
                {
                    int row = i + 2; //excel row starts from 1 and also skip header row
                    int column = headerColumns[c.EmailSent];
                    writer.Write(row, column, c.EmailSentYes);
                }
            }
            writer.Save();
        }
    }
}
