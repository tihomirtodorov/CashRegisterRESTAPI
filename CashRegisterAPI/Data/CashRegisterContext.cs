using CashRegisterAPI.Data.DataModels;
using Microsoft.EntityFrameworkCore;

namespace CashRegisterAPI.Data
{
    public class CashRegisterContext : DbContext
    {
        public CashRegisterContext(DbContextOptions<CashRegisterContext> options)
            :base(options)
        {

        }

        public DbSet<Banknotes> Banknotes { get; set; }
    }
}
