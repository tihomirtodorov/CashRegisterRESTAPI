using CashRegisterAPI.Data;
using CashRegisterAPI.Services;
using CashRegisterAPI.Services.Models;
using CashRegisterAPI.Utilities;
using CashRegisterAPI.Utilities.Models;
using CashRegisterService_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CashRegisterService_Tests.CashRegisterService_Tests
{
    [TestClass]
    public class TotalAmountAvailable_Should
    {
        [TestMethod]
        public async Task Return_Forbidden_Status_Code_When_RegisterEmpty()
        {
            var databaseName = nameof(Return_Forbidden_Status_Code_When_RegisterEmpty);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.TotalAmountAvailable();
                Assert.IsTrue(response.StatusCode == 403);
            };
        }

        [TestMethod]
        public async Task Return_Fail_Message_When_RegisterEmpty()
        {
            var databaseName = nameof(Return_Fail_Message_When_RegisterEmpty);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.TotalAmountAvailable();
                Assert.IsTrue(response.Message == Messages.CashRegisterEmpty);
            };
        }

        [TestMethod]
        public async Task Return_Null_Model_When_RegisterEmpty()
        {
            var databaseName = nameof(Return_Null_Model_When_RegisterEmpty);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.TotalAmountAvailable();
                Assert.IsNull(response.Model);
            };
        }

        [TestMethod]
        public async Task Return_OK_StatusCode_When_Cash_Found()
        {
            var databaseName = nameof(Return_OK_StatusCode_When_Cash_Found);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            CashRegisterService_Utilities.FillContextWithUserData(options);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.TotalAmountAvailable();
                Assert.IsTrue(response.StatusCode == 200);
            };
        }

        [TestMethod]
        public async Task Return_NotNull_Model_When_Cash_Found()
        {
            var databaseName = nameof(Return_NotNull_Model_When_Cash_Found);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            CashRegisterService_Utilities.FillContextWithUserData(options);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.TotalAmountAvailable();
                Assert.IsNotNull(response.Model);
            };
        }

        [TestMethod]
        public async Task Return_Correct_Instance_Of_Model_When_Successful()
        {
            var databaseName = nameof(Return_Correct_Instance_Of_Model_When_Successful);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            CashRegisterService_Utilities.FillContextWithUserData(options);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.TotalAmountAvailable();
                Assert.IsInstanceOfType(response, typeof(GeneralResponseModel<BanknotesDTO>));
            };
        }
    }
}
