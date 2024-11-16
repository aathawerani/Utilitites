// See https://aka.ms/new-console-template for more information
using UploadMasterData;
using System.Net;


BusinessLogic bl = new BusinessLogic();
bl.ReadExcel();
//bl.GetClientList();
//bl.filterData();
//bl.CreateCustomer();
//bl.UpdateExcel();
bl.CreateInvoice();