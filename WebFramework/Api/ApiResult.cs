using Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Api;

public class ApiResult
{
    public bool isSuccess { get; set; }
    public ApiStatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public ApiResult(bool isSuccess, ApiStatusCode statusCode, string message = null)
    {
        this.isSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
    }

    public static implicit operator ApiResult(OkResult result)
    {
        return new ApiResult(true, ApiStatusCode.Success);
    }
    public static implicit operator ApiResult(BadRequestResult result)
    {
        return new ApiResult(false, ApiStatusCode.BadRequest);
    }
    public static implicit operator ApiResult(BadRequestObjectResult result)
    {
        var message = result.Value.ToString();
        if (result.Value is SerializableError errors)
        {
            var errorMessage = errors.SelectMany(p => (string[])p.Value).Distinct();
            message = String.Join('|', errorMessage);
        }
        return new ApiResult(false, ApiStatusCode.BadRequest, message);
    }
    public static implicit operator ApiResult(ContentResult result)
    {
        return new ApiResult(true, ApiStatusCode.Success, result.Content);
    }
    public static implicit operator ApiResult(NotFoundResult result)
    {
        return new ApiResult(false, ApiStatusCode.NotFound);
    }
}

public class ApiResult<TData> : ApiResult
    where TData : class
{
    public TData Data { get; set; }

    public ApiResult(bool isSuccess, ApiStatusCode statusCode, TData data, string message = null) : base(isSuccess, statusCode, message)
    {
        Data = data;
    }

    public static implicit operator ApiResult<TData>(TData data)
    {
        return new ApiResult<TData>(true, ApiStatusCode.Success, data);
    }
    public static implicit operator ApiResult<TData>(OkResult result)
    {
        return new ApiResult<TData>(true, ApiStatusCode.Success, null);
    }
    public static implicit operator ApiResult<TData>(OkObjectResult result)
    {
        return new ApiResult<TData>(true, ApiStatusCode.Success, (TData)result.Value);
    }
    public static implicit operator ApiResult<TData>(BadRequestResult result)
    {
        return new ApiResult<TData>(false, ApiStatusCode.BadRequest, null);
    }
    public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
    {
        var message = result.Value.ToString();
        if (result.Value is SerializableError errors)
        {
            var errorMessage = errors.SelectMany(p => (string[])p.Value).Distinct();
            message = String.Join('|', errorMessage);
        }
        return new ApiResult<TData>(false, ApiStatusCode.BadRequest, null, message);
    }
    public static implicit operator ApiResult<TData>(ContentResult result)
    {
        return new ApiResult<TData>(false, ApiStatusCode.Success, null, result.Content);
    }
    public static implicit operator ApiResult<TData>(NotFoundResult result)
    {
        return new ApiResult<TData>(false, ApiStatusCode.NotFound, null);
    }
    public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
    {
        return new ApiResult<TData>(false, ApiStatusCode.NotFound, (TData)result.Value);
    }

}