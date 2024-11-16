using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIResponse;
using APIRequest;

namespace UploadMasterData
{
    class BusinessLogic
    {
        List<Dictionary<string, string>> data;
        Dictionary<string, int> headerColumns;
        Configuration c = Configuration.getInstance();
        ApiResponse APIresposne = null;
        List<Dictionary<string, string>> filteredData = new List<Dictionary<string, string>>();
        public void ReadExcel()
        {
            ExcelReader reader = new ExcelReader(c.XLfileName);
            reader.OpenWorksheet(c.XLSheetName);
            data = reader.ReadAllLines();
            int row = c.XLHeaderRow, column = c.XLHeaderColumn;
            headerColumns = reader.ReadHeader(row, column);
        }

        public void GetClientList()
        {
            CallAPI api = new CallAPI();
            string responseSTR = api.GetClientList().Result;
            APIResponseClientListParser parser = new APIResponseClientListParser();
            APIresposne = parser.ParseResponse(responseSTR);
        }

        public void filterData()
        {
            ResponseMapper map = new ResponseMapper();
            foreach(Dictionary<string, string> item in data)
            { 
                if(!map.checkDuplicate(headerColumns, item, APIresposne))
                {
                    filteredData.Add(item);
                }
            }
        }

        public void CreateCustomer()
        {
            RequestMapper reqMAP = new RequestMapper();
            ResponseMapper resMAP = new ResponseMapper();
            CallAPI api = new CallAPI();
            foreach (Dictionary<string, string> item in filteredData)
            {
                ClientRequest client = reqMAP.APIClientData(item);
                APIRequestCreater apiRequest = new APIRequestCreater();
                string customerJSON = apiRequest.CreateClient(client);
                string response = api.CreateClient(customerJSON).Result;
                APIResponseCreateClientParser parser = new APIResponseCreateClientParser();
                APIresposne = parser.ParseResponse(response);
                item.Add(c.clientNumber, resMAP.GetClientNumber(APIresposne));
            }
        }

        public void UpdateExcel()
        {
            ExcelWriter writer = new ExcelWriter(c.XLfileName);
            writer.OpenWorksheet(c.XLSheetName);

            foreach (Dictionary<string, string> item in filteredData)
            {
                if(item[c.clientNumber] != "")
                {
                    int row = Convert.ToInt32(item[c.ROW]);
                    int column = headerColumns[c.clientID];
                    writer.Write(row, column, item[c.clientNumber]);
                }
            }
            writer.Save();
        }

        public void CreateInvoice()
        {
            RequestMapper reqMAP = new RequestMapper();
            ResponseMapper resMAP = new ResponseMapper();
            CallAPI api = new CallAPI();
            foreach (Dictionary<string, string> item in data)
            {
                InvoiceData invoice = reqMAP.APIInvoiceData(item);
                APIRequestCreater apiRequest = new APIRequestCreater();
                string invoiceJSON = apiRequest.CreateInvoice(invoice);
                string response = api.CreateInvoice(invoiceJSON).Result;
                APIResponseCreateInvoiceParser parser = new APIResponseCreateInvoiceParser();
                APIresposne = parser.ParseResponse(response);
            }
        }

    }
}
