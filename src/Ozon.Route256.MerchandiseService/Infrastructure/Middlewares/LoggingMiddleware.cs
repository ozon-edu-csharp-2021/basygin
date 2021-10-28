using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Ozon.Route256.MerchandiseService.Infrastructure.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context.Request);
            await _next.Invoke(context);
            LogResponse(context.Response);
        }

        private async Task LogRequest(HttpRequest request)
        {
            try
            {
                var requestHeaders = request.Headers.Select(item => $"{item.Key}: {item.Value}").ToArray();

                _logger.LogInformation($"Service request: {request.Method} {request.Path}" );
                _logger.LogInformation($"Request headers: {string.Join(',', requestHeaders)}" );

                if (request.ContentLength > 0)
                {
                    request.EnableBuffering();
                    var buffer = new byte[request.ContentLength.Value];
                    await request.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);
                    request.Body.Position = 0;
                    
                    _logger.LogInformation($"Body: {bodyAsText}" );
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request body");
            }
        }

        private void LogResponse(HttpResponse response)
        {
            try
            {
                var responseHeaders = response.Headers.Select(item => $"{item.Key}: {item.Value}");
                
                _logger.LogInformation($"Service response: {response.HttpContext.Request.Method} {response.HttpContext.Request.Path} StatusCode: {response.StatusCode}" );
                _logger.LogInformation($"Response headers: {string.Join(',', responseHeaders)}" );
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log response body");
            }
        }
    }
}