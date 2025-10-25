using Domain.Exceptions;
using Shared.ErrorModels;

namespace OnionAPI.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                    await HandleNotFoundApiAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong ==> : {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = $"The endpoint with url {context.Request.Path} not found"
            }.ToString();
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
            }.ToString();

            await context.Response.WriteAsync(response);
        }
    }
}
