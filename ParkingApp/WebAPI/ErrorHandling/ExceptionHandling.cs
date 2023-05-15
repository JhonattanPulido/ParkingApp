// Libraries
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Utilitaries.DTO;
using Utilitaries.Exceptions;

namespace WebAPI.ErrorHandling
{
    /// <summary>
    /// Custom exception handling
    /// </summary>
    public class ExceptionHandling : ExceptionFilterAttribute
    {
        // Vars
        private ILogger<ExceptionHandling> Logger { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ExceptionHandling(ILogger<ExceptionHandling> logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// On exception handler
        /// </summary>
        /// <param name="context">Exception context</param>
        public override void OnException(ExceptionContext context)
        {
            Logger.LogError(context.Exception.ToString());

            int statusCode = StatusCodes.Status500InternalServerError;
            ErrorDTO error = new();

            if (context.Exception is CustomNotFoundException)
            {
                error.Message = context.Exception.Message;
                statusCode = StatusCodes.Status404NotFound;
            }
            else if (context.Exception is CustomBadRequestException)
            {
                error.Message = context.Exception.Message;
                statusCode = StatusCodes.Status400BadRequest;
            }

            context.Result = new JsonResult(error)
            {
                ContentType = "application/json",
                StatusCode = statusCode
            };
        }
    }
}
