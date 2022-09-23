using PYP_Task_VCard_WebApp.Models;
using QRCoder;
using System.Text;
using PYP_Task_VCard_WebApp.Interfaces.Services;

namespace PYP_Task_VCard_WebApp.Services
{
    public class VCardQRCodeIHandler : IVCardQRCodeIHandler
    {

         public Task<string> CreateQrCode(VCard vCard)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCARD");
            sb.AppendLine("VERSION:2.1");
            sb.AppendLine("N:;" + vCard.Firtname + ";" + vCard.Surname + ";;;;");
            sb.AppendLine("FN:" + vCard.Firtname + " " + vCard.Surname);
            sb.AppendLine("TEL;CELL:" + vCard.Phone);
            sb.AppendLine("EMAIL;PREF;INTERNET:" + vCard.Email);
            sb.AppendLine("ADR;HOME:;;" + vCard.City + ";" + vCard.Country);
            sb.AppendLine("END:VCARD");

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(sb.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            
            using (var bitmap = qrCode.GetGraphic(20))
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    var bytes = stream.ToArray();
                    return Task.FromResult("data:image/png;base64," + Convert.ToBase64String(bytes));
                }
            }

            
        }
    }
}
