using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserList.Helpers.Filters
{
    public class RequestValidationFilterAttribute : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            if (!context.ModelState.IsValid)
            {
                if (context.Controller is Controller controller)
                {
                    var err = context.ModelState.Values
                        .SelectMany(state => state.Errors)
                        .Select(error => error.ErrorMessage);
                    var modelErrors = err.ToList();

                    controller.ViewData["error"] = modelErrors;
                }
            }
        }
    }
}
