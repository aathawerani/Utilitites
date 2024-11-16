using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadMasterData
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

        public string PROVIDERNAME { get { return "PROVIDERNAME"; } }
        public string CONTACTPERSON{ get { return "CONTACTPERSON"; } }
        public string EmailAddress { get { return "EMAILADDRESS"; } }
        public string Street { get { return "Street"; } }
        public string CITY { get { return "CITY"; } }
        public string REGION { get { return "REGION"; } }
        public string ZipCode { get { return "ZipCode"; } }
        public string CONTACTNO { get { return "CONTACTNO."; } }
        public string Country { get { return "Country"; } }
        public string Currency { get { return "Currency"; } }
        public string ContractType { get { return "Contract Type"; } }
        public string CREDITTERM { get { return "CREDITTERM"; } }
        public string CCHI{ get { return "CCHI"; } }
        public string DHSCODE { get { return "DHSCODE"; } }
        public string PROVTYPE{ get { return "PROV.TYPE"; } }
        public string NPHIESID{ get { return "NPHIESID"; } }
        public string CONTRACT{ get { return "CONTRACT"; } }
        public string buildingNo { get { return "building no"; } }
        public string district { get { return "district"; } }
        public string ROW { get { return "ROW"; } }
        public string clientNumber { get { return "client_number"; } }
        public string invoiceNumber { get { return "invoice_number"; } }
        public string clientID { get { return "client id"; } }
        public string baseAPI { get { return "https://aliahmedmanaging.daftra.com"; } }
        public string GetClients { get { return "/api2/clients"; } }
        public string CreateClient { get { return "/api2/clients.json"; } }
        public string CreateInvoice { get { return "/api2/invoices.json"; } }
        public string APIKEY { get { return "fc7942fc6cf997eafe59059f1b87d2cf5cabc818"; } }

        public string XLfileName { get { return "D:\\aaht14\\freelance\\Tanweer\\DHSArabia\\provider_master_data_240724.xlsx"; } }
        public string PDFfilePath { get { return "D:\\aaht14\\freelance\\Tanweer\\DHSArabia\\emailutility\\31Jul24-223520\\"; } }
        public string PDFext { get { return ".pdf"; } }
        public string XLSheetName { get { return "Provider"; } }
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
