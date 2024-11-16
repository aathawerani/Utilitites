using SelectPdf;

namespace GenerateQR.Helpers
{
    public static class PFDFileGeneratorHelper
    {
        public static async Task ConvertHtmlToPdfForUFAC(string cchino, string path, string html)
        {
            // Create the PDF converter
            HtmlToPdf converter = new();
            // Set the converter options
            converter.Options.MaxPageLoadTime = 300;
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            
            // Convert HTML to PDF
            PdfDocument doc = converter.ConvertHtmlString(html);

            // Save the PDF to a byte array
            byte[] pdfBytes = doc.Save();
            // Close the PDF document
            doc.Close();

            // Create the directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            // Write the byte array to the file
            File.WriteAllBytes(path, pdfBytes); Console.WriteLine(cchino + " - File saved successfully.");
        }
    }
}
