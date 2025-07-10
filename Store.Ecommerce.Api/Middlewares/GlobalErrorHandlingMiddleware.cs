using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Expressions;
using Shared.ErrorModels;

namespace Store.Ecommerce.Api.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next , ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                // If the request was successful, do nothing and let the response continue
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    // Handle 404 Not Found specifically if needed
                    await HandlinNotFoundEndPointAsync(context);
                }

            }
            catch (Exception ex)
            {
                // Log the exception 
                _logger.LogError(ex, ex.Message);

                await HandlingErrorAsync(context, ex);

            }
        }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            // context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            // Create a response object with error details
            var response = new ErrorDetails()
            {
                // StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = ex.Message // "An unexpected error occurred. Please try again later."
            };
            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ValidationException => HandlValidationExceptionAsync((ValidationException)ex,response),
                _ => StatusCodes.Status500InternalServerError// Default to 500 for other exceptions
            };
            context.Response.StatusCode = response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlinNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"The requested resource{context.Request.Path} was not found."
            };
            await context.Response.WriteAsJsonAsync(response);
        }
        private static int HandlValidationExceptionAsync(ValidationException ex , ErrorDetails response)
        {
            response.Errors = ex.Errors;
            return StatusCodes.Status400BadRequest;

        }






















        }
}   
