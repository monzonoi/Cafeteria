// En Cafeteria.Api.Middleware
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;


public class ErrorResponse
{
    public string Message { get; set; }
    public int StatusCode { get; set; }
}

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Registra la excepción (puedes usar un sistema de registro como Serilog o log4net)
            Console.WriteLine($"Error: {ex.Message}");

            // Crea un objeto ErrorResponse con el mensaje de error y el código de estado
            var errorResponse = new ErrorResponse
            {
                Message = "Ocurrió un error interno en el servidor.",
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            // Convierte el objeto ErrorResponse en una respuesta JSON
            var jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(errorResponse);

            // Configura la respuesta HTTP con el mensaje de error y el código de estado
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Escribe la respuesta JSON en el cuerpo de la respuesta
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
