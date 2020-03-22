using CashRegisterAPI.Services.Models;
using CashRegisterAPI.Utilities.Models;
using System.Threading.Tasks;

namespace CashRegisterAPI.Services.Interfaces
{
    public interface ICashRegisterService
    {
        Task<GeneralResponseModel> Deposit(BanknotesDTO banknotes);

        Task<GeneralResponseModel<BanknotesDTO>> TotalAmountAvailable();

        Task<GeneralResponseModel<BanknotesDTO>> Withdraw(double withdrawAmount);

        Task<GeneralResponseModel<BanknotesDTO>> CalculateChange(double price, double sum);

    }
}
