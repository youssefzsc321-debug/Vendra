using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendra.Core.Common
{
    public class APIResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public APIResponse(int statusCode, bool isSuccess, T? data = default, string? errorMessage = null, List<string>? errors = null)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Data = data;
            Errors = errors;
            Message = errorMessage ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Success",
                201 => "Created Successfully",
                204 => "No Content",
                400 => "A bad request was made",
                401 => "You are not authorized",
                403 => "Forbidden access",
                404 => "Resource not found",
                500 => "An unexpected internal server error occurred",
                _ => null
            };
        }
    }
}
