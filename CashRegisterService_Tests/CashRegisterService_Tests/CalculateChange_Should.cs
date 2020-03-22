using CashRegisterAPI.Data;
using CashRegisterAPI.Services;
using CashRegisterAPI.Services.Models;
using CashRegisterAPI.Utilities.Models;
using CashRegisterService_Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CashRegisterService_Tests.CashRegisterService_Tests
{
    [TestClass]
    public class CalculateChange_Should
    {
        [TestMethod]
        public async Task Return_OK_Status_Code_When_Successful()
        {
            var databaseName = nameof(Return_OK_Status_Code_When_Successful);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            CashRegisterService_Utilities.FillContextWithUserData(options);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.CalculateChange(6, 8);
                Assert.IsTrue(response.StatusCode == 200);
            };
        }

        [TestMethod]
        public async Task Return_Not_Null_Model_When_Successful()
        {
            var databaseName = nameof(Return_Not_Null_Model_When_Successful);

            var options = CashRegisterService_Utilities.GetOptions(databaseName);

            CashRegisterService_Utilities.FillContextWithUserData(options);

            using (var actAndAssertContext = new CashRegisterContext(options))
            {
                var sut = new CashRegisterService(actAndAssertContext);

                var response = await sut.CalculateChange(6, 8);
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

                var response = await sut.CalculateChange(6, 8);
                Assert.IsInstanceOfType(response, typeof(GeneralResponseModel<BanknotesDTO>));
            };
        }
    }
}
