// See https://aka.ms/new-console-template for more information
using emailutility;
using System.Net;


BusinessLogic bl = new BusinessLogic();
bl.ReadExcel();
//bl.SendEmail();
bl.UpdateExcel();