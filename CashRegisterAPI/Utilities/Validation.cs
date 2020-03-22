using CashRegisterAPI.Utilities.Models;
using System.Net;

namespace CashRegisterAPI.Utilities
{
    internal static class Validation
    {
        internal static GeneralResponseModel ValidateReponse(bool TransactionPassed, int FailureStatusCode, string SuccessfulMessage = "", string FailureMessage = "")
        {
            if (TransactionPassed)
            {
                return new GeneralResponseModel()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = SuccessfulMessage
                };
            }
            else
            {
                return new GeneralResponseModel()
                {
                    StatusCode = FailureStatusCode,
                    Message = FailureMessage
                };
            }
        }

        internal static GeneralResponseModel<T> ValidateResponse<T>(bool IsTransactionPassed, int StatusCode, T Model, string SuccessfulMessage = "", string FailureMessage = "") 
            where T : class
        {
            if (IsTransactionPassed)
            {
                return new GeneralResponseModel<T>()
                {
                    StatusCode = StatusCode,
                    Message = SuccessfulMessage,
                    Model = Model
                };
            }
            else
            {
                return new GeneralResponseModel<T>()
                {
                    StatusCode = StatusCode,
                    Message = FailureMessage
                };
            }
        }
    }
}
