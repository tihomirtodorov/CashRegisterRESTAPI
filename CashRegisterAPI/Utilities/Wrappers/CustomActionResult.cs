using CashRegisterAPI.Utilities.Contracts;
using CashRegisterAPI.Utilities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CashRegisterAPI.Utilities.Wrappers
{
    public class CustomActionResult<T> : ICustomActionResult, IActionResult where T : class
    {
        private readonly GeneralResponseModel<T> generalResponseModel;

        public CustomActionResult(GeneralResponseModel<T> generalResponseModel)
        {
            this.generalResponseModel = generalResponseModel;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult objectResult;

            switch (generalResponseModel.StatusCode)
            {
                case StatusCodes.Status200OK:
                    objectResult = new ObjectResult(generalResponseModel.Model)
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                    break;
                default:
                    objectResult = new ObjectResult(generalResponseModel.Message)
                    {
                        StatusCode = generalResponseModel.StatusCode
                    };
                    break;
            }

            await objectResult.ExecuteResultAsync(context);
        }
    }

    public class CustomActionResult : ICustomActionResult, IActionResult
    {
        private readonly GeneralResponseModel generalResponseModel;

        public CustomActionResult(GeneralResponseModel generalResponseModel)
        {
            this.generalResponseModel = generalResponseModel;
        }
         
        public async Task ExecuteResultAsync(ActionContext context)
        {
            ObjectResult objectResult = new ObjectResult(generalResponseModel.Message)
            {
                StatusCode = generalResponseModel.StatusCode
            };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
