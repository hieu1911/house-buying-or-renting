using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Localization;
using HouseBuyingOrRenting.Domain;

namespace HouseBuyingOrRenting
{
    public class ExceptionMiddleware
    {
        public class ExceptionMiddleware
        {
            private readonly RequestDelegate _requestDelegate;
            private readonly ILogger<ExceptionMiddleware> _logger;
            private readonly IStringLocalizer<User> _stringLocalizer;

            public ExceptionMiddleware(RequestDelegate requestDelegate, 
                ILogger<ExceptionMiddleware> logger, IStringLocalizer<User> stringLocalizer)
            {
                _requestDelegate = requestDelegate;
                _logger = logger;
                _stringLocalizer = stringLocalizer;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    // Nếu chưa đăng nhập thì trả về mã 401
                    var path = context.Request.Path.Value;
                    if (path == "/api/v1/Auths/login")
                    {
                        await _requestDelegate(context);
                    }
                    else if (context.Request.Cookies.Keys.Contains("Authentication"))
                    {
                        var authenticateResult = await context.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        if (authenticateResult.Succeeded)
                        {
                            context.User = authenticateResult.Principal;
                            await _requestDelegate(context);
                        }
                        else
                        {
                            throw new UnAuthorizeException();
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(context, ex);
                }
            }

            private async Task HandleExceptionAsync(HttpContext context, Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";

                switch (ex)
                {
                    case NotFoundException notFoundException:
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.Response.WriteAsync(
                            text: new BaseException()
                            {
                                ErrorCode = notFoundException.ErrorCode,
                                UserMessage = notFoundException.Message ?? _stringLocalizer.GetString("NotFoundException"),
                                DevMessage = ex.Message,
                                TraceId = context.TraceIdentifier,
                                MoreInfo = ex.HelpLink
                            }.ToString() ?? ""
                        );
                        break;
                    case ConflictException conflictException:
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        await context.Response.WriteAsync(
                            text: new BaseException()
                            {
                                ErrorCode = conflictException.ErrorCode,
                                UserMessage = conflictException.Message ?? _stringLocalizer.GetString("ConflictException"),
                                DevMessage = ex.Message,
                                TraceId = context.TraceIdentifier,
                                MoreInfo = ex.HelpLink
                            }.ToString() ?? ""
                        );
                        break;
                    case BadRequestException badRequestException:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync(
                            text: new BaseException()
                            {
                                ErrorCode = badRequestException.ErrorCode,
                                UserMessage = badRequestException.UserMessage ?? _stringLocalizer.GetString("BadRequestException"),
                                DevMessage = ex.Message,
                                TraceId = context.TraceIdentifier,
                                MoreInfo = ex.HelpLink
                            }.ToString() ?? ""
                        );
                        break;
                    case UnAuthorizeException unauthorizedException:
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync(
                            text: new BaseException()
                            {
                                ErrorCode = unauthorizedException.ErrorCode,
                                UserMessage = unauthorizedException.Message ?? _stringLocalizer.GetString("UnauthorizedException"),
                                DevMessage = ex.Message,
                                TraceId = context.TraceIdentifier,
                                MoreInfo = ex.HelpLink
                            }.ToString() ?? ""
                        );
                        break;
                    case PreConditionException preConditionException:
                        context.Response.StatusCode = StatusCodes.Status412PreconditionFailed;
                        await context.Response.WriteAsync(
                            text: new BaseException()
                            {
                                ErrorCode = preConditionException.ErrorCode,
                                UserMessage = preConditionException.Message ?? "",
                                DevMessage = ex.Message,
                                TraceId = context.TraceIdentifier,
                                MoreInfo = ex.HelpLink,
                            }.ToString() ?? ""
                        );
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync(
                           text: new BaseException()
                           {
                               ErrorCode = StatusCodes.Status500InternalServerError,
                               UserMessage = _stringLocalizer.GetString("SeverError"),
                               DevMessage = ex.Message,
                               TraceId = context.TraceIdentifier,
                               MoreInfo = ex.HelpLink
                           }.ToString() ?? ""
                        );
                        break;
                }
            }
        }
    }
}
