using GenerateQR.Models;
using System.Text;

public class QRCodeService
{
    public string GenerateQRCode(QRModel model)
    {
        var byteBuilder = new ByteBuilder();
        byteBuilder.AddByte(1);

        byte[] seleerNameBytes = Encoding.UTF8.GetBytes(model.SellerName);
        byteBuilder.AddByte((byte)seleerNameBytes.Length);
        byteBuilder.AddBytes(seleerNameBytes);
        byteBuilder.AddByte(2);

        byte[] vatNumberBytes = Encoding.UTF8.GetBytes(model.VatNumber);
        byteBuilder.AddByte((byte)vatNumberBytes.Length);
        byteBuilder.AddBytes(vatNumberBytes);
        byteBuilder.AddByte(3);

        byte[] timeStampBytes = Encoding.UTF8.GetBytes(model.TimeStamp);
        byteBuilder.AddByte((byte)timeStampBytes.Length);
        byteBuilder.AddBytes(timeStampBytes);
        byteBuilder.AddByte(4);
       
        byte[] invoiceAmountBytes = Encoding.UTF8.GetBytes(model.InvoiceAmount);
        byteBuilder.AddByte((byte)invoiceAmountBytes.Length);
        byteBuilder.AddBytes(invoiceAmountBytes);
        byteBuilder.AddByte(5);
        
        byte[] vatAmountBytes = Encoding.UTF8.GetBytes(model.VatAmount);
        byteBuilder.AddByte((byte)vatAmountBytes.Length);
        byteBuilder.AddBytes(vatAmountBytes);              

        byte[] qrCodeBytes = byteBuilder.GetBytes();
        return Convert.ToBase64String(qrCodeBytes);
    }

}
