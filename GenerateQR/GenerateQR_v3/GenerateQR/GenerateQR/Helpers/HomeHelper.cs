using ExcelDataReader;
using GenerateQR.Models;
using NuGet.Packaging.Signing;
using QRCoder;
using System;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace GenerateQR.Helpers
{
    public class HomeHelper
    {
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
        private readonly QRCodeService _qrCodeService;

        public HomeHelper(IRazorViewToStringRenderer razorViewToStringRenderer, QRCodeService qrCodeService)
        {
            _razorViewToStringRenderer = razorViewToStringRenderer;
            _qrCodeService = qrCodeService;
        }
        public async Task<List<string>> SubmitUploadAttachFile(FileModel formData, string cat)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using var stream = new MemoryStream();
            await formData?.uploadExcelFile?.CopyToAsync(stream);


            //using var reader = ExcelReaderFactory.CreateReader(stream);
            //var result = reader.AsDataSet(new ExcelDataSetConfiguration()
            //{
            //ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
            //{
            //UseHeaderRow = true
            //}
            //});

            //var tables = result.Tables
            //.Cast<DataTable>()
            //.Select(t => new
            //{
            //TableName = t.TableName,
            //Columns = t.Columns
            //.Cast<DataColumn>()
            //.Select(x => x.ColumnName)
            //.ToList()
            //});

            //var dt = result.Tables[0];
            //if (dt.Rows != null && dt.Rows.Count > 0)
            //{
            //dt = dt.Rows.Cast<DataRow>()
            //.Where(row => !row.ItemArray.All(field => field is DBNull || string.IsNullOrWhiteSpace(field as string))).CopyToDataTable();
            //}

            //column validation
            //if (!(dt?.Rows?.Count > 0))
            //{
            //throw new ExcelException($"Excel File is Empty");
            //}

            Dictionary<string, Dictionary<string, string>> MainExcelColumnsName = ConfigurationReader.GetExcelColumnsName("ExcelConfig", "MainExcelColumnsName");

            ExcelReader XLreader = new ExcelReader(stream);
            //XLreader.OpenWorksheet("Invoices");
            XLreader.OpenWorksheet("Invoices");
            List<Dictionary<string, string>> data = XLreader.ReadAllLines(MainExcelColumnsName);

            List<string> errors = new();
            List<string> NoFiles = new();
            DateTime TimeStamp = DateTimeHelper.NphiesDateTime();

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //for (int i = 0; i < dt.Rows.Count; i++)

            //special customer handling
            int rowNum = 0;
                try
                {
                ReportModel reportModel = new ReportModel();
                string HealthProvider = "";
                string Location = "";
                string ContactPerson = "";
                string ContactNO = "";
                string EmailAddress = "";
                string ContractRefrence = "";
                string VATNO = ""; //XLrow["VATNO"];
                string InvoiceNO = "";
                string InvoiceDate = "";
                string BillingMonth = "";
                string DueDate = "";
                string SadadBILLID = "";
                string CCHINO = "";
                string DHSCode = "";
                string BatchMonth = "";
                string TotalExcVAT = "";
                string VAT = "";
                string TotalWithVAT = "";
                string ContractType = "";
                string ClaimRate = "";
                string SegmentPrice = "";
                string InsuranceCO = "";
                string CRMQuantity = "";
                string Invoicable = "";
                string GroupProvider = "";
                string item = "";
                string Description = "";
                string Quantity = "";
                string Rate = "";
                string Discount = "";
                string Total = "";
                string COMMERCIALREG = "";
                string CONTRACTDATE = "";
                foreach (Dictionary<string, string> XLrow in data)
                {
                    //special customer handling
                    if (rowNum == 0) { 
                        HealthProvider = XLrow["HealthProvider"];
                        Location = XLrow["Location"];
                        ContactPerson = XLrow["ContactPerson"];
                        ContactNO = XLrow["ContactNO"];
                        EmailAddress = XLrow["EmailAddress"];
                        ContractRefrence = XLrow["ContractRefrence"];
                        VATNO = XLrow["VATNo"]; //XLrow["VATNO"];
                        InvoiceNO = XLrow["InvoiceNO"];
                        InvoiceDate = XLrow["InvoiceDate"];
                        BillingMonth = XLrow["BillingMonth"];
                        DueDate = XLrow["DueDate"];
                        SadadBILLID = XLrow["SadadBILLID"];
                        CCHINO = XLrow["CCHINO"];
                        DHSCode = XLrow["DHSCode"];
                        TotalExcVAT = XLrow["TotalExcVAT"];
                        VAT = XLrow["VAT"];
                        TotalWithVAT = XLrow["TotalWithVAT"];
                        ContractType = XLrow["ContractType"];
                        ClaimRate = XLrow["ClaimRate"];
                        SegmentPrice = XLrow["SegmentPrice"];
                        CRMQuantity = XLrow["CRMQuantity"];
                        Invoicable = XLrow["Invoicable"];
                        GroupProvider = XLrow["GroupProvider"];
                        COMMERCIALREG = XLrow["COMMERCIALREG"];
                        CONTRACTDATE = XLrow["CONTRACTDATE"];
                        BatchMonth = XLrow["BatchMonth"];
                    }
                    else
                    {
                        item = XLrow["Item"]; //XLrow["item"];
                        Description = XLrow["Description"];
                        Quantity = XLrow["Quantity"];
                        Rate = XLrow["Rate"];
                        Discount = XLrow["Discount"];
                        Total = XLrow["Total"];
                        InsuranceCO = XLrow["InsuranceCO"];
                        BatchMonth = XLrow["BatchMonth"];
                    }
                    if (rowNum == 0)
                    {
                            reportModel.BillingMonth = !string.IsNullOrEmpty(BillingMonth) ? Convert.ToDateTime(BillingMonth) : null;
                        reportModel.CCHINO = CCHINO;
                        reportModel.ContactNO = ContactNO;
                        reportModel.ContactPerson = ContactPerson;
                        reportModel.ContractRefrence = ContractRefrence;
                        reportModel.DHSCode = DHSCode;
                        reportModel.DueDate = !string.IsNullOrEmpty(DueDate) ? Convert.ToDateTime(DueDate) : null;
                        reportModel.EmailAddress = EmailAddress;
                        reportModel.HealthProvider = HealthProvider;
                        reportModel.InvoiceNO = InvoiceNO;
                        reportModel.InvoiceDate = !string.IsNullOrEmpty(InvoiceDate) ? Convert.ToDateTime(InvoiceDate) : null;
                        reportModel.Location = Location;
                        reportModel.SadadBILLID = SadadBILLID;
                        reportModel.VATNO = VATNO;
                        reportModel.TotalExcVAT = !string.IsNullOrEmpty(TotalExcVAT) ? GetDecimalTwoDigits(TotalExcVAT) : null;
                        reportModel.ClaimRate = ClaimRate;
                        reportModel.ContractType = ContractType;
                        reportModel.SegmentPrice = !string.IsNullOrEmpty(SegmentPrice) ? GetDecimalTwoDigits(SegmentPrice) : null;
                        reportModel.TotalWithVAT = !string.IsNullOrEmpty(TotalWithVAT) ? GetDecimalTwoDigits(TotalWithVAT) : null;
                        reportModel.VAT = !string.IsNullOrEmpty(VAT) ? GetDecimalTwoDigits(VAT) : null;
                        reportModel.CRMQuantity = CRMQuantity;
                        reportModel.GroupProvider = GroupProvider;
                        reportModel.Invoicable = (!string.IsNullOrEmpty(Invoicable) ? ((Invoicable.ToLower() == "no") ? (false) : (true)) : true);
                        reportModel.TotalInWords = NumberToWords(TotalWithVAT);
                        reportModel.COMMERCIALREG = COMMERCIALREG;
                        reportModel.CONTRACTDATE = !string.IsNullOrEmpty(CONTRACTDATE) ? Convert.ToDateTime(CONTRACTDATE) : null;
                        reportModel.items = new LineItems[3];
                        reportModel.items[0] = new LineItems();
                        reportModel.items[1] = new LineItems();
                        reportModel.items[2] = new LineItems();
                        reportModel.BatchMonth = BatchMonth;
                    }
                    else if(rowNum <= 3)
                    {
                        reportModel.items[rowNum-1].item = item;
                        reportModel.items[rowNum-1].Description = Description;
                        reportModel.items[rowNum-1].Quantity = !string.IsNullOrEmpty(Quantity) ? GetDecimalTwoDigits(Quantity) : null;
                        reportModel.items[rowNum-1].Rate = !string.IsNullOrEmpty(Rate) ? GetDecimalTwoDigits(Rate) : null;
                        reportModel.items[rowNum-1].Discount = !string.IsNullOrEmpty(Discount) ? GetDecimalTwoDigits(Discount) : null;
                        reportModel.items[rowNum-1].Total = !string.IsNullOrEmpty(Total) ? GetDecimalTwoDigits(Total) : null;
                        reportModel.items[rowNum - 1].InsuranceCO = InsuranceCO;
                        reportModel.items[rowNum - 1].BatchMonth = BatchMonth;
                    }
                    rowNum++;
                }

                if (Convert.ToBoolean(reportModel?.Invoicable))
                        {
                            if (ValidateModel(cat, reportModel))
                            {
                                QRModel model = new()
                                {
                                    SellerName = "Digital Healthcare Solutions Arabia LLC",
                                    InvoiceAmount = GetDecimalTwoDigits(TotalWithVAT).ToString(),
                                    VatAmount = GetDecimalTwoDigits(VAT).ToString(),
                                    VatNumber = "300073375400003",
                                    TimeStamp = InvoiceDate,

                                };
                                var stringeBase64 = _qrCodeService.GenerateQRCode(model);
                                var qrCodeImageBase64 = GenerateQrCode(stringeBase64);
                                reportModel.QrCodeImageBase64 = qrCodeImageBase64;
                            
                                string TaxInvoiceString = "";
                                if (cat == "old")
                                {
                                    TaxInvoiceString = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Home/_TaxInvoiceOld.cshtml", reportModel);
                                }
                                else
                                {
                                    TaxInvoiceString = await _razorViewToStringRenderer.RenderViewToStringAsync("/Views/Home/_TaxInvoice.cshtml", reportModel);
                                }


                                var fileName = InvoiceNO;
                                string dateFormat = TimeStamp.ToString("ddMMMyy");
                                string path = "D:\\aaht14\\freelance\\Tanweer\\DHSArabia\\" + dateFormat + "-" + TimeStamp.ToString("HHmmss") + "\\" + fileName + ".pdf";
                                //D:\aaht14\freelance\Tanweer\DHSArabia\emailutility
                                try
                                {
                                    await PFDFileGeneratorHelper.ConvertHtmlToPdfForUFAC(CCHINO, path, TaxInvoiceString);
                                    /*EmailService emailService = new EmailService();
                                    string toEmail = "tansari@dhsarabia.com";
                                    string subject = "PROVIDER INVOICE";
                                    bool emailSent = emailService.SendEmail(toEmail, subject, path);*/
                                }
                                catch (Exception ex)
                                {
                                    string error = "";
                                    if (!string.IsNullOrEmpty(CCHINO))
                                    {
                                        error = CCHINO;
                                    }

                                    if (!string.IsNullOrEmpty(InvoiceNO))
                                    {
                                        error += ", " + InvoiceNO;
                                    }

                                    error += ":" + ex?.Message;

                                    errors.Add(error);
                                }
                            }
                            else
                            {
                                string NoFile = "";
                                if (!string.IsNullOrEmpty(CCHINO))
                                {
                                    NoFile = CCHINO;
                                }

                                if (!string.IsNullOrEmpty(InvoiceNO))
                                {
                                    NoFile += ", " + InvoiceNO; //COMMA SEPARATED
                                }

                                NoFiles.Add(NoFile);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex.Message);
                    }

                try
                {
                    if (NoFiles != null && NoFiles?.Count > 0)
                    {
                        string fileName = "NoTax"; //CONVERT TO LOG FILE ERROR.LOG
                        string dateFormat = TimeStamp.ToString("ddMMMyy");
                        string path = "C:\\Users\\tanwe\\OneDrive - DHS Arabia\\Shared\\Pro Art D365\\DHS Invoicing Utility\\FebMar - Batch Runs\\" + dateFormat + "-" + TimeStamp.ToString("HHmmss") + "\\" + fileName + ".pdf";
                        try
                        {
                            string str = "";
                            foreach (var item in NoFiles ?? [])
                            {
                                str += item + "<br/>";
                            }
                            await PFDFileGeneratorHelper.ConvertHtmlToPdfForUFAC("0000", path, str);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            //}

            if (errors != null && errors?.Count > 0)
            {
                string fileName = "errors";
                string dateFormat = TimeStamp.ToString("ddMMMyy");
                string path = "C:\\Users\\tanwe\\OneDrive - DHS Arabia\\Shared\\Pro Art D365\\DHS Invoicing Utility\\FebMar - Batch Runs\\" + dateFormat + "-" + TimeStamp.ToString("HHmmss") + "\\" + fileName + ".pdf";
                try
                {
                    string str = "";
                    foreach (var item in errors ?? [])
                    {
                        str += item + "<br/>";
                    }
                    await PFDFileGeneratorHelper.ConvertHtmlToPdfForUFAC("0000", path, str);
                }
                catch (Exception ex)
                {

                }
            }

            return errors;
        }
        private static bool ValidateModel(string cat, ReportModel reportModel)
        {
            var valid = (!string.IsNullOrEmpty(reportModel?.HealthProvider) &&
                    !string.IsNullOrEmpty(reportModel?.Location) &&
                    !string.IsNullOrEmpty(reportModel?.ContactPerson) &&
                    !string.IsNullOrEmpty(reportModel?.ContactNO) &&
                    !string.IsNullOrEmpty(reportModel?.EmailAddress) &&
                    !string.IsNullOrEmpty(reportModel?.ContractRefrence) &&
                    !string.IsNullOrEmpty(reportModel?.VATNO) &&
                    reportModel?.InvoiceDate != null &&
                    !string.IsNullOrEmpty(reportModel?.InvoiceNO) &&
                    reportModel?.BillingMonth != null &&
                    reportModel?.DueDate != null &&
                    !string.IsNullOrEmpty(reportModel?.CCHINO) &&
                    //!string.IsNullOrEmpty(reportModel?.item) &&
                    //!string.IsNullOrEmpty(reportModel?.Description) &&
                    //!string.IsNullOrEmpty(reportModel?.BatchMonth) &&
                    //reportModel?.Quantity != null &&
                    //reportModel?.Rate != null &&
                    //reportModel?.Discount != null &&
                    //reportModel?.Total != null &&
                    reportModel?.TotalExcVAT != null &&
                    !string.IsNullOrEmpty(reportModel?.ContractType) &&
                    !string.IsNullOrEmpty(reportModel?.ClaimRate) &&
                    reportModel?.SegmentPrice != null
                    );
            if (cat == "old")
            {
                valid &= (//!string.IsNullOrEmpty(reportModel?.InsuranceCO) &&
                    //!string.IsNullOrEmpty(reportModel?.SadadBILLID) &&
                    !string.IsNullOrEmpty(reportModel?.DHSCode));

            }

            return valid;
        }
        public static decimal GetDecimalTwoDigits(decimal? value)
        {
            if (value == null) value = 0;
            return Convert.ToDecimal(string.Format(CultureInfo.CurrentCulture, "{0:0.00}", value), CultureInfo.CurrentCulture);
        }
        public static decimal GetDecimalTwoDigits(string value)
        {
            if (value == null) value = "0";
            return Convert.ToDecimal(Convert.ToDecimal(string.Format("{0:0.00}", value)).ToString("F"));
        }
        #region Convert Number To Word
        public static string NumberToWords(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                decimal doubleNumber = Convert.ToDecimal(Convert.ToDecimal(string.Format("{0:0.00}", value)).ToString("F"));
                var beforeFloatingPoint = (int)Math.Floor(doubleNumber);
                var beforeFloatingPointWord = $"{NumberToWords(beforeFloatingPoint)}";
                var afterFloatingPointWord =
                    $"{SmallNumberToWord((int)((doubleNumber - beforeFloatingPoint) * 100), "")}";
                if (!string.IsNullOrEmpty(afterFloatingPointWord))
                {
                    return $"{beforeFloatingPointWord} and {afterFloatingPointWord}";
                }
                else
                {
                    return $"{beforeFloatingPointWord}";
                }
            }

            return "";
        }
        private static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            var words = "";

            if (number / 1000000000 > 0)
            {
                words += NumberToWords(number / 1000000000) + " billion ";
                number %= 1000000000;
            }

            if (number / 1000000 > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if (number / 1000 > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            words = SmallNumberToWord(number, words);

            return words;
        }
        private static string SmallNumberToWord(int number, string words)
        {
            if (number <= 0) return words;
            if (words != "")
                words += " ";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
            return words;
        }
        #endregion

        public class FileModel
        {
            public IFormFile uploadExcelFile { get; set; }
            public List<string> ResultMessage { get; set; }
            public bool IsFileUploaded { get; set; }
        }
        [Serializable]
        public class ExcelException : Exception
        {
            public ExcelException()
            {
            }
            public ExcelException(string message) : base(message)
            {
            }

            public ExcelException(string message, Exception innerException) : base(message, innerException)
            {
            }

            // Without this constructor, deserialization will fail
            protected ExcelException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        public static string GetValueFromTable(DataRow dr, string key, Dictionary<string, Dictionary<string, string>> ExcelColumnsName)
        {
            string value = "";
            if (dr != null && ExcelColumnsName?.Count > 0)
            {
                var selectedKey = ExcelColumnsName[key].Select(a => a.Value).FirstOrDefault();
                value = dr[selectedKey].ToString();
            }

            return value;
        }


        public static string GenerateQrCode(string qrCodeData)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeDataObj = qrGenerator.CreateQrCode(qrCodeData, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeDataObj);
                using (var qrCodeImage = qrCode.GetGraphic(20))
                {
                    using (var ms = new MemoryStream())
                    {
                        qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
    }

public class EmailService
    {
        public bool SendEmail(string toEmail, string subject, string pdfFilePath)
        {
            // Sample HTML content
            string htmlContent = @"
            <html>
            <body>
                <p>Dear Partner,</p>
                <p>Please find the attached invoice for batch 2024-04-30 for your reference. Kindly acknowledge receipt of our billing</p>
                <p>Additionally, please refer to the terms and conditions stipulated on the invoice for proper guidance and adherence.</p>
                <p>Thank you in advance for your cooperation.</p>
            </body>
            </html>";

            try
            {
                // Set up the SMTP client
                SmtpClient client = new SmtpClient("smtp.office365.com", 25)
                {
                    Credentials = new NetworkCredential("dev_test@dhsarabia.com", "L!104206117388ud"),
                    EnableSsl = true
                };

                // Create the email message
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("dev_test@dhsarabia.com"),
                    Subject = subject,
                    Body = htmlContent,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                // Attach the PDF file
                Attachment pdfAttachment = new Attachment(pdfFilePath);
                mailMessage.Attachments.Add(pdfAttachment);

                // Send the email
                client.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                // Log the exception (optional)
                return false;
            }
        }
    }
}
