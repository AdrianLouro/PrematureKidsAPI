using System.Collections.Generic;
using System.Linq;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (getEntity(context) == null)
            {
                context.Result = new BadRequestObjectResult("Object is null");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        private object getEntity(ActionExecutingContext context)
        {
            return context.ActionArguments.SingleOrDefault(argument => argument.Value is IEntity).Value;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}