using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Api;

namespace WebFramework.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {

            if (context.Result is OkResult okResult)
            {
                var apiResult = new ApiResult(true, ApiStatusCode.Success);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is OkObjectResult okObjectResult)
            {
                var apiResult = new ApiResult<Object>(true, ApiStatusCode.Success, okObjectResult.Value);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is BadRequestResult badRequestResult)
            {
                var apiResult = new ApiResult(false, ApiStatusCode.BadRequest);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is ObjectResult badRequestObjectResult && badRequestObjectResult.StatusCode == 400)
            {
                string message = null;
                switch (badRequestObjectResult.Value)
                {
                    case ValidationProblemDetails validationProblemDetails:
                        var errorMessages = validationProblemDetails.Errors.SelectMany(p => p.Value).Distinct();
                        message = string.Join(" | ", errorMessages);
                        break;
                    case SerializableError errors:
                        var errorMessages2 = errors.SelectMany(p => (string[])p.Value).Distinct();
                        message = string.Join(" | ", errorMessages2);
                        break;
                    case var value when value != null && !(value is ProblemDetails):
                        message = badRequestObjectResult.Value.ToString();
                        break;
                }

                var apiResult = new ApiResult(false, ApiStatusCode.BadRequest, message);
                context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
            }
            else if (context.Result is ContentResult contentResult)
            {
                var apiResult = new ApiResult(true, ApiStatusCode.Success, contentResult.Content);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is NotFoundResult notFoundResult)
            {
                var apiResult = new ApiResult(false, ApiStatusCode.NotFound);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                var apiResult = new ApiResult<Object>(false, ApiStatusCode.NotFound, notFoundObjectResult.Value);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null)
            {
                var apiResult = new ApiResult<Object>(true, ApiStatusCode.Success, objectResult.Value);
                context.Result = new JsonResult(apiResult);
            }
            base.OnResultExecuting(context);
        }
    }
}
