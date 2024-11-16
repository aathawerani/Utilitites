using GenerateQR.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace GenerateQR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRGeneratorController : ControllerBase
    {
        private readonly QRCodeService _qrCodeService;
        public QRGeneratorController(QRCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;

        }

        [HttpPost]
        [Route("generate")]
        public IActionResult GenerateCode([FromBody] QRModel model)
        {
            var stringeBase64 = _qrCodeService.GenerateQRCode(model);
            var qrCodeImageBase64 = GenerateQrCode(stringeBase64);
            return Ok(qrCodeImageBase64);
        }
        private string GenerateQrCode(string qrCodeData)
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
}
