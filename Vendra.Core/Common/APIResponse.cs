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
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }
        public List<String>? Errors { get; set; }
        public APIResponse(int StatusCode, bool IsSuccess, T? Data = default, string? ErrorMessage = null, List<String>? Errors = null)
        {
            this.StatusCode = StatusCode;
            this.ErrorMessage = ErrorMessage;
            this.Errors = Errors;
            this.IsSuccess = IsSuccess;
            this.Data = Data;
        }
    }
}
