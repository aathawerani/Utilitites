using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emailutility
{
    class Configuration
    {
        private static Configuration _instance;
        private Configuration() { }
        public static Configuration getInstance() { 
            if(_instance is null)
            {
                _instance = new Configuration();
            }
            return _instance; 
        }

        public string InvoiceNO { get { return "InvoiceNO"; } }
        public string EmailAddress { get { return "EmailAddress"; } }
        public string SendInvoice { get { return "SendInvoice"; } }
        public string EmailSent { get { return "EmailSent"; } }
        public string XLfileName { get { return "D:\\aaht14\\freelance\\Tanweer\\DHSArabia\\0524_cloud_invoice_300724A.xlsx"; } }
        public string PDFfilePath { get { return "D:\\aaht14\\freelance\\Tanweer\\DHSArabia\\emailutility\\31Jul24-223520\\"; } }
        public string PDFext { get { return ".pdf"; } }
        public string XLSheetName { get { return "0524"; } }
        public string SendInvoiceYes { get { return "Yes"; } }
        public string EmailSentYes { get { return "Yes"; } }
        public string smtpServer { get { return "smtp.office365.com"; } }
        public int smtpPort { get { return 587; } }
        public string senderEmail { get { return "dev_test@dhsarabia.com"; } }
        public string senderPassword { get { return "L!104206117388ud"; } }
        public string subject { get { return "Provider Invoice "; } }
        public string body { get { return "This is a test email with an attachment."; } }
        public int XLHeaderRow { get { return 1; } }
        public int XLHeaderColumn { get { return 1; } }
    }
}
