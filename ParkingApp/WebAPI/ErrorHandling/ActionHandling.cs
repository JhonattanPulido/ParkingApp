// Libraries
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Utilitaries.DTO;

namespace WebAPI.ErrorHandling
{
    /// <summary>
    /// Action handling
    /// </summary>
    public class ActionHandling : IActionFilter
    {
        /// <summary>
        /// On action executed handler
        /// </summary>
        /// <param name="context">Request context</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        /// <summary>
        /// On action executing handler
        /// </summary>
        /// <param name="context">Request context</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                BadRequestObjectResult badRequest = new(!context.ModelState.IsValid ? new ModelValidationDTO(context.ModelState.Where(x => x.Value?.ValidationState == ModelValidationState.Invalid)) : "");
                context.Result = badRequest;
            }
        }
    }
}
