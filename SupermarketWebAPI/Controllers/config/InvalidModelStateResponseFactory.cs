using Microsoft.AspNetCore.Mvc;
using SupermarketWebAPI.Extensions;
using SupermarketWebAPI.Resources;

namespace SupermarketWebAPI.Controllers.config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(messages: errors);

            return new BadRequestObjectResult(response);
        }
    }
}
