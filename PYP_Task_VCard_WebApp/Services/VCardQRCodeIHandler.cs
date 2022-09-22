using PYP_Task_VCard_WebApp.Models;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;
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

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] byteImage = ms.ToArray();
                return Task.FromResult("data:image/png;base64," + Convert.ToBase64String(byteImage));
            }
        }
    }
}
