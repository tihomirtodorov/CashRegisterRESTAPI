using CashRegisterAPI.Data;
using CashRegisterAPI.Services;
using CashRegisterAPI.Services.Models;
using CashRegisterAPI.Utilities;
using CashRegisterService_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace CashRegisterService_Tests.CashRegisterService_Tests
{
    [TestClass]
    public class Deposit_Should
    {
        [TestMethod]
        public async Task Return_Forbidden_Status_Code_When_NoCash_Is_Provided()
        {
            var databaseName = nameof(Return_Forbidden_Status_Code_When_NoCash_Is_Provided);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.Deposit(null);
                Assert.IsTrue(response.StatusCode == 403);
            };
        }

        [TestMethod]
        public async Task Return_FailureMessage_When_NoCash_Is_Provided()
        {
            var databaseName = nameof(Return_FailureMessage_When_NoCash_Is_Provided);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.Deposit(null);
                Assert.IsTrue(response.Message == Messages.NoCashIsProvidedToBeDeposited);
            };
        }

        [TestMethod]
        public async Task Initiliaze_Banknotes_Object_In_Database_With_Zero_Values()
        {
            var databaseName = nameof(Initiliaze_Banknotes_Object_In_Database_With_Zero_Values);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            var banknotesDTO = new Mock<BanknotesDTO>();

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.Deposit(banknotesDTO.Object);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().Fifty, 0);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().Twenty, 0);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().Ten, 0);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().Five, 0);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().Two, 0);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().One, 0);
            };
        }

        [TestMethod]
        public async Task Add_Banknotes_Cout_To_Database_Object_Values()
        {
            var databaseName = nameof(Add_Banknotes_Cout_To_Database_Object_Values);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            var banknotesDTO = new BanknotesDTO() { Fifty = 5, Twenty = 2, Ten = 3, Five = 6, Two = 4, One = 9 };

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.Deposit(banknotesDTO);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().Fifty, 5);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().Twenty, 2);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().Ten, 3);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().Five, 6);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().Two, 4);
                Assert.AreEqual(actAndAssertContext.Banknotes.FirstOrDefault().One, 9);
            };
        }

        [TestMethod]
        public async Task Return_OK_Status_Code_When_Successful()
        {
            var databaseName = nameof(Return_OK_Status_Code_When_Successful);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            var banknotesDTO = new BanknotesDTO() { Fifty = 5, Twenty = 2, Ten = 3, Five = 6, Two = 4, One = 9 };

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.Deposit(banknotesDTO);
                Assert.IsTrue(response.StatusCode == 200);
            };
        }

        [TestMethod]
        public async Task Return_Success_Message_When_Successful()
        {
            var databaseName = nameof(Return_Success_Message_When_Successful);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            var banknotesDTO = new BanknotesDTO() { Fifty = 5, Twenty = 2, Ten = 3, Five = 6, Two = 4, One = 9 };

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.Deposit(banknotesDTO);
                Assert.IsTrue(response.Message == Messages.DepositPassed);
            };
        }
    }
}
