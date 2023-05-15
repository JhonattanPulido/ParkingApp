// Libraries
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Utilitaries.DTO;

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

            context.Result = new JsonResult(error)
            {
                ContentType = "application/json",
                StatusCode = statusCode
            };
        }
    }
}
