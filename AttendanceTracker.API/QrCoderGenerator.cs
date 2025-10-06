/*using System;
using System.Drawing;
using System.IO;
using QRCoder;

namespace AttendanceTracker.API.Functions;

public static class QrCodeHelper
{
    public static Bitmap GenerateQrCodeImage(string conteudo)
    {
        using (var qrGenerator = new QRCodeGenerator())
        {
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(conteudo, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            qrCodeData.Dispose();
            string base64String = Convert.ToBase64String(qrCodeImage, 0, qrCodeImage.Length);
        }
    }

    public static string ConvertBitmapToBase64(Bitmap qrCodeImage)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            qrCodeImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] imageBytes = memoryStream.ToArray();
            return $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
        }
    }
}*/