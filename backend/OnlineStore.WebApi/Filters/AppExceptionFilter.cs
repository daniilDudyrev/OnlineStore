using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Domain.Exceptions;

namespace OnlineStore.WebApi.Filters;

public class AppExceptionFilter : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var message = TryGetUserMessageFromException(context);
        if (message == null) return;
        context.Result = new ObjectResult(new ErrorModel(message));
        context.ExceptionHandled = true;
    }

    private string? TryGetUserMessageFromException(ExceptionContext context)
    {
        return context.Exception switch
        {
            EmailNotFoundException => "There is no such account!",
            InvalidPasswordException => "Invalid password!",
            _ => null
        };
    }
}