using Mono.TextTemplating;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GenerateQR.Models
{
    public class LineItems
    {
        public string item = "";
        public string Description = "";
        public decimal? Quantity = 0;
        public decimal? Rate = 0;
        public decimal? Discount = 0;
        public decimal? Total = 0;
        public string InsuranceCO = "";
        public string BatchMonth = "";
    }
    public class ReportModel
    {
        public string HealthProvider { get; set; }
        public string Location { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNO { get; set; }
        public string EmailAddress { get; set; }
        public string ContractRefrence { get; set; }
        public string VATNO { get; set; }
        public string InvoiceNO { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? BillingMonth { get; set; }
        public DateTime? DueDate { get; set; }
        public string SadadBILLID { get; set; }
        public string CCHINO { get; set; }
        public string DHSCode { get; set; }
        public string item { get; set; }
        public string Description { get; set; }
        public string BatchMonth { get; set; }
        public string InsuranceCO { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Total { get; set; }
        public decimal? TotalExcVAT { get; set; }
        public decimal? VAT { get; set; }
        public decimal? TotalWithVAT { get; set; }
        public decimal? SegmentPrice { get; set; }
        public string ContractType { get; set; }
        public string ClaimRate { get; set; }
        public string CRMQuantity { get; set; }
        public bool? Invoicable { get; set; }
        public string GroupProvider { get; set; }
        public string TotalInWords { get; set; }
        public string QrCodeImageBase64 { get; set; }
        public string COMMERCIALREG { get; set; }
        public DateTime? CONTRACTDATE { get; set; }

        public LineItems[] items;
    }
}
