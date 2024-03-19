namespace EyeAdvertisingDotNetTask.Core.ViewModels.General
{
    public class ApiResponseViewModel
    {
        public ApiResponseViewModel(object data, bool isSuccess, string message)
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }

        public ApiResponseViewModel(bool isSuccess, string message)
        {
            Data = null;
            IsSuccess = isSuccess;
            Message = message;
        }

        public ApiResponseViewModel(bool isSuccess)
        {
            Data = null;
            IsSuccess = isSuccess;
        }

        public object Data { get; set; }
        public bool IsSuccess { get; }
        public string? Message { get; set; }
        public string? ExtendedMessage { get; set; }
    }
}
