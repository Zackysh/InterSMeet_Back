using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using InterSMeet.BL.Exception;
using Newtonsoft.Json;

namespace InterSMeet.API
{
    /// <summary>
    /// Class used internally to produce uniform error responses.
    /// </summary>
    internal class ErrorResponse
    {
        public string Error { get; set; }
        public ErrorResponse(string message)
        {
            Error = message;
        }

        public static string ToJson(string message)
        {
            return JsonConvert.SerializeObject(new ErrorResponse(message)) ?? "";
        }
    }

    /// <summary>
    /// InterSMeet.ApiRest Exception Middleware.
    /// It will catch any exception, verify its origin and produce user-friendly responses.
    /// </summary>
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (BLNotFoundException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(ErrorResponse.ToJson(e.Message));
            }
            catch (BLUnauthorizedException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(ErrorResponse.ToJson(e.Message));
            }
            catch (BLForbiddenException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(ErrorResponse.ToJson(e.Message));
            }
            catch (BLConflictException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(ErrorResponse.ToJson(e.Message));
            }
            catch (BLBadRequestException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(ErrorResponse.ToJson(e.Message));
            }
            catch (Exception e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(ErrorResponse.ToJson(e.Message));
            }
        }
    }
}
