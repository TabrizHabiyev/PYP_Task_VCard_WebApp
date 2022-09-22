
using Microsoft.EntityFrameworkCore;
using PYP_Task_VCard_WebApp.Models;

namespace PYP_Task_VCard_WebApp.Context
{
    public class VCardContext : DbContext
    {
        public VCardContext(DbContextOptions<VCardContext> options)
            : base(options){}

        public DbSet<VCard> VCards { get; set; }
    }
}