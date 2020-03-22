namespace CashRegisterAPI.Utilities.Models
{
    public class GeneralResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class GeneralResponseModel<T> where T : class
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Model { get; set; }
    }
}
