using PYP_Task_VCard_WebApp.Models;

namespace PYP_Task_VCard_WebApp.Interfaces.Services
{
    public interface IVCardQRCodeIHandler
    {
        Task<string> CreateQrCode(VCard vCard);
    }
}

