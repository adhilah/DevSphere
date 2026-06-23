using System.Text.Json;
using DevSphere.Application.DTOs.Common;
using DevSphere.Application.Exceptions;

namespace DevSphere.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(BadRequestException e){
            await HandleException( context, 400, e.Message);
        }
        catch(UnauthorizedException e){
            await HandleException(context, 401, e.Message);
        }
        catch(NotFoundException e){
            await HandleException(context, 404, e.Message);
        }
        catch(Exception){
            await HandleException(context , 500, "Internal server error");
        }
    }
    private static async Task HandleException(
        HttpContext context,
        int statusCode,
        string message){
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        
        var response = new ErrorResponse
        {
            Success = false,
            Message = message
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}