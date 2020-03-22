using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CashRegisterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CashRegisterAPI.Services.Models;
using CashRegisterAPI.Utilities.Contracts;

namespace CashRegisterAPI.Controllers
{
    [ApiController]
    [Route("api/cashregister")]
    public class CashRegisterController : BaseController
    {
        private readonly ICashRegisterService cashRegisterService;

        public CashRegisterController(ICashRegisterService cashRegister)
        {
            this.cashRegisterService = cashRegister;
        }

        [HttpPost("deposit")]
        public async Task<ICustomActionResult> Deposit([FromBody][Required]BanknotesDTO banknotes)
        {
            var response = await this.cashRegisterService.Deposit(banknotes);

            return await ExecuteAsync(response);
        }

        [HttpPost("withdraw")]
        public async Task<ICustomActionResult> Withdraw([Required]double withdrawAmount)
        {
            var response = await this.cashRegisterService.Withdraw(withdrawAmount);

            return await ExecuteAsync(response);
        }

        [HttpGet("amountAvailable")]
        public async Task<ICustomActionResult> TotalAmountAvailable()
        {
            var response = await this.cashRegisterService.TotalAmountAvailable();

            return await ExecuteAsync(response);
        }

        [HttpPost("calculatechange")]
        public async Task<ICustomActionResult> CalculateChange([Required]double price, [Required]double sum)
        {
            var response = await this.cashRegisterService.CalculateChange(price, sum);

            return await ExecuteAsync(response);
        }
    }
}
