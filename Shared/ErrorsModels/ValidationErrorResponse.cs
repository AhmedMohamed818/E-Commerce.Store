using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorsModels
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; } = StatusCodes.Status400BadRequest; // Default to Bad Request 
        public string ErrorMessage { get; set; } = "Validation failed"; // Default error message
        public IEnumerable<ValidationError>Errors { get; set; }
    }
}
