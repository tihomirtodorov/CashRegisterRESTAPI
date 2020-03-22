using CashRegisterAPI.Data;
using CashRegisterAPI.Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashRegisterService_Tests.Utilities
{
    public class CashRegisterService_Utilities
    {
        public static DbContextOptions<CashRegisterContext> GetOptions(string databaseName)
        {
            var serviceCollection = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            return new DbContextOptionsBuilder<CashRegisterContext>()
                .UseInMemoryDatabase(databaseName)
                .UseInternalServiceProvider(serviceCollection)
                .Options;
        }

        public static CashRegisterContext FillContextWithUserData(DbContextOptions<CashRegisterContext> options)
        {
            var context = new CashRegisterContext(options);

            var banknote = new Banknotes()
            {
                Fifty = 1,
                Twenty = 1,
                Ten = 1,
                Five = 1,
                Two = 1,
                One = 1
            };

            context.Banknotes.Add(banknote);

            context.SaveChanges();

            return context;
        }
    }
}
