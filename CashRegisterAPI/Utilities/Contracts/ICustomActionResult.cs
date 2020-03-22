using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CashRegisterAPI.Utilities.Contracts
{
    public interface ICustomActionResult
    {
        Task ExecuteResultAsync(ActionContext context);
    }
}
