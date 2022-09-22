using Microsoft.Extensions.DependencyInjection;
using PYP_Task_VCard_WebApp.Context;
using PYP_Task_VCard_WebApp.Interfaces.Services;
using PYP_Task_VCard_WebApp.Services;

namespace PYP_Task_VCard_WebApp
{
    public static class ServicesRegistration
    {
        public static void AddVCardServices(this IServiceCollection services)
        {
            services.AddScoped<IVCardQRCodeIHandler , VCardQRCodeIHandler>();
        }
    }
}