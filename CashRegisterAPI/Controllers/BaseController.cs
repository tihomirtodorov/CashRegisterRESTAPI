using CashRegisterAPI.Utilities.Models;
using CashRegisterAPI.Utilities.Wrappers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CashRegisterAPI.Controllers
{
    /// <summary>
    /// Base Controller for all API controllers
    /// </summary>
    public class BaseController : ControllerBase
    {
        protected async Task<CustomActionResult<T>> ExecuteAsync<T>(GeneralResponseModel<T> request) where T : class
           =>  new CustomActionResult<T>(request);

        protected async Task<CustomActionResult> ExecuteAsync(GeneralResponseModel request)
           => new CustomActionResult(request);
    }
}
