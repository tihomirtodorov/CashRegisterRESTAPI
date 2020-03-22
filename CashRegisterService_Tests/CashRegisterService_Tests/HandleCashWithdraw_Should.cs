using CashRegisterAPI.Data;
using CashRegisterAPI.Data.DataModels;
using CashRegisterAPI.Services;
using CashRegisterAPI.Utilities;
using CashRegisterService_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace CashRegisterService_Tests.CashRegisterService_Tests
{
    [TestClass]
    public class HandleCashWithdraw_Should
    {
        [TestMethod]
        public async Task Return_Forbidden_Status_Code_When_Withdraw_Amount_IsLess_Than_Zero()
        {
            var databaseName = nameof(Return_Forbidden_Status_Code_When_Withdraw_Amount_IsLess_Than_Zero);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var withdrawAmount = -5;
                var response = await sut.HandleCashWithdraw(withdrawAmount);
                Assert.IsTrue(response.StatusCode == 403);
            };
        }

        [TestMethod]
        public async Task Return_Failure_Message_When_Withdraw_Amount_IsLess_Than_Zero()
        {
            var databaseName = nameof(Return_Failure_Message_When_Withdraw_Amount_IsLess_Than_Zero);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var withdrawAmount = -5;
                var response = await sut.HandleCashWithdraw(withdrawAmount);
                Assert.IsTrue(response.Message == Messages.NegativeAmountCannotBeWithdrawn);
            };
        }

        [TestMethod]
        public async Task Return_Forbidden_Status_Code_When_RegisterEmpty()
        {
            var databaseName = nameof(Return_Forbidden_Status_Code_When_RegisterEmpty);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var withdrawAmount = 50;
                var response = await sut.HandleCashWithdraw(withdrawAmount);
                Assert.IsTrue(response.StatusCode == 403);
            };
        }

        [TestMethod]
        public async Task Return_Forbidden_Status_Code_When_Withdraw_Amount_Bigger_Than_Banknotes_Sum()
        {
            var databaseName = nameof(Return_Forbidden_Status_Code_When_Withdraw_Amount_Bigger_Than_Banknotes_Sum);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            CashRegisterService_Utilities.FillContextWithUserData(options);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var withdrawAmount = 99999;
                var response = await sut.HandleCashWithdraw(withdrawAmount);
                Assert.IsTrue(response.StatusCode == 403);
            };
        }

        [TestMethod]
        public async Task Return_Failure_Message_When_Withdraw_Amount_Bigger_Than_Banknotes_Sum()
        {
            var databaseName = nameof(Return_Failure_Message_When_Withdraw_Amount_Bigger_Than_Banknotes_Sum);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            CashRegisterService_Utilities.FillContextWithUserData(options);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var withdrawAmount = 99999;
                var response = await sut.HandleCashWithdraw(withdrawAmount);
                Assert.IsTrue(response.Message == Messages.CashNotEnoughToWithdraw);
            };
        }

        [TestMethod]
        public async Task Return_Forbidden_Status_Code_When_Banknotes_Cannot_Fulfil_Withdraw()
        {
            var databaseName = nameof(Return_Forbidden_Status_Code_When_Banknotes_Cannot_Fulfil_Withdraw);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            // Arrange
            var context = new CashRegisterContext(options);

            var banknote = new Banknotes()
            {
                Fifty = 1,
                One = 1,
            };

            context.Banknotes.Add(banknote);

            context.SaveChanges();


            var sut = new CashRegisterService(context);

            var withdrawAmount = 41;
            var response = await sut.HandleCashWithdraw(withdrawAmount);
            Assert.IsTrue(response.StatusCode == 403);
        }

        [TestMethod]
        public async Task Return_Failure_Message_When_Banknotes_Cannot_Fulfil_Withdraw()
        {
            var databaseName = nameof(Return_Failure_Message_When_Banknotes_Cannot_Fulfil_Withdraw);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            // Arrange
            var context = new CashRegisterContext(options);

            var banknote = new Banknotes()
            {
                Fifty = 1,
                One = 1,
            };

            context.Banknotes.Add(banknote);

            context.SaveChanges();


            var sut = new CashRegisterService(context);

            var withdrawAmount = 41;
            var response = await sut.HandleCashWithdraw(withdrawAmount);
            Assert.IsTrue(response.Message == Messages.BanknotesCannotFulfilPayment);
        }

        [TestMethod]
        public async Task Return_Model_With_Withdrawn_Banknotes()
        {
            var databaseName = nameof(Return_Model_With_Withdrawn_Banknotes);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            // Arrange
            var context = new CashRegisterContext(options);

            var banknote = new Banknotes()
            {
                Fifty = 1,
                One = 1,
            };

            context.Banknotes.Add(banknote);

            context.SaveChanges();


            var sut = new CashRegisterService(context);

            var withdrawAmount = 51;
            var response = await sut.HandleCashWithdraw(withdrawAmount);
            Assert.IsNotNull(response.Model);
        }

        [TestMethod]
        public async Task Update_Database_With_Withdrawn_Banknotes()
        {
            var databaseName = nameof(Update_Database_With_Withdrawn_Banknotes);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            // Arrange
            var context = new CashRegisterContext(options);

            var banknote = new Banknotes()
            {
                Fifty = 1,
                One = 1,
            };

            context.Banknotes.Add(banknote);

            context.SaveChanges();


            // Act
            var sut = new CashRegisterService(context);

            var withdrawAmount = 51;
            var response = await sut.HandleCashWithdraw(withdrawAmount);

            // Assert
            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                Assert.IsTrue(actAndAssertContext.Banknotes.FirstOrDefault().Fifty == 0);
                Assert.IsTrue(actAndAssertContext.Banknotes.FirstOrDefault().One == 0);
            };
        }

        [TestMethod]
        public async Task Return_OK_Status_Code_When_Successful()
        {
            var databaseName = nameof(Return_OK_Status_Code_When_Successful);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            // Arrange
            var context = new CashRegisterContext(options);

            var banknote = new Banknotes()
            {
                Fifty = 1,
                One = 1,
            };

            context.Banknotes.Add(banknote);

            context.SaveChanges();


            var sut = new CashRegisterService(context);

            var withdrawAmount = 51;
            var response = await sut.HandleCashWithdraw(withdrawAmount);
            Assert.IsTrue(response.StatusCode == 200);
        }
    }
}
