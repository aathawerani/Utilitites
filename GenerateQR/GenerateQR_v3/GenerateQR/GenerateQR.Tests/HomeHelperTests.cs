using Xunit;
using Moq;
using GenerateQR.Helpers;
using GenerateQR.Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GenerateQR.Tests
{
    public class HomeHelperTests
    {
        private readonly HomeHelper _homeHelper;
        private readonly Mock<IRazorViewToStringRenderer> _razorViewMock;
        private readonly Mock<QRCodeService> _qrCodeServiceMock;

        public HomeHelperTests()
        {
            _razorViewMock = new Mock<IRazorViewToStringRenderer>();
            _qrCodeServiceMock = new Mock<QRCodeService>();
            _homeHelper = new HomeHelper(_razorViewMock.Object, _qrCodeServiceMock.Object);
        }

		/*[Fact]
		public async Task SubmitUploadAttachFile_WithEmptyFile_ReturnsError()
		{
			// Arrange: Create a valid Excel file with minimal data
			var stream = new MemoryStream();
			using (var workbook = new ClosedXML.Excel.XLWorkbook())
			{
				var worksheet = workbook.AddWorksheet("Sheet1");
				worksheet.Cell(1, 1).Value = "Header";
				workbook.SaveAs(stream);
			}
			stream.Position = 0; // Reset stream for reading

			var fileMock = new Mock<IFormFile>();
			fileMock.Setup(f => f.OpenReadStream()).Returns(stream); // Return the in-memory stream
			fileMock.Setup(f => f.FileName).Returns("valid_empty.xlsx");
			fileMock.Setup(f => f.Length).Returns(stream.Length);
			fileMock.Setup(f => f.ContentType).Returns("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

			var fileModel = new HomeHelper.FileModel
			{
				uploadExcelFile = fileMock.Object
			};

			// Act
			var result = await _homeHelper.SubmitUploadAttachFile(fileModel, "new");

			// Assert
			Assert.NotNull(result);
			Assert.Contains(result, error => error.Contains("No data found") || error.Contains("Excel File is Empty"));

			stream.Dispose(); // Dispose manually after the test
		}*/


        [Fact]
        public void GetDecimalTwoDigits_WithNullInput_ReturnsZero()
        {
            // Act
            var result = HomeHelper.GetDecimalTwoDigits((decimal?)null);

            // Assert
            Assert.Equal(0, result);
        }

		[Fact]
		public void NumberToWords_ConvertsCorrectly()
		{
			// Act
			var result = HomeHelper.NumberToWords("1234").Replace("  ", " "); // Remove extra spaces

			// Assert
			Assert.Equal("one thousand two hundred thirty-four", result);
		}
	}
}
