using System;
using System.Security.AccessControl;

namespace Work.Api.Models
{
    public class BaseResponseModel
    {
        public bool IsError { get; set; } = false;
        public string ErrorDetail { get; set; }
        public string SuccessMessage { get; set; } = "Success";
        public object Data { get; set; }

    }
}
