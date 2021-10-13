using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using api.Core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.IO;

namespace api.Middleware
{
    ///<summary> Middleware for logging requests and responses. </summary>
    public class RequestResponseLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggerMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        public async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogInfo($"Http Request Information:{Environment.NewLine}" +
                                $"Schema:{context.Request.Scheme} " +
                                $"Host: {context.Request.Host} " +
                                $"Path: {context.Request.Path} " +
                                $"QueryString: {context.Request.QueryString} " +
                                $"Request Body: {ReadStreamInChunks(requestStream)}");
            context.Request.Body.Position = 0;
        }

        public async Task LogResponse (HttpContext context)
        {
            if (context.Request.Path.ToString().Contains("/api"))
            {
                var originalBodyStream = context.Response.Body;
                await using var responseBody = _recyclableMemoryStreamManager.GetStream();
                context.Response.Body = responseBody;
                await _next(context);
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                _logger.LogInfo($"Http Response Information:{Environment.NewLine}" +
                                    $"Schema:{context.Request.Scheme} " +
                                    $"Host: {context.Request.Host} " +
                                    $"Path: {context.Request.Path} " +
                                    $"QueryString: {context.Request.QueryString} " +
                                    $"Response Body: {text}");
                await responseBody.CopyToAsync(originalBodyStream);
            } else {
                _logger.LogInfo($"Http Response Information:{Environment.NewLine}" +
                                    $"Schema:{context.Request.Scheme} " +
                                    $"Host: {context.Request.Host} " +
                                    $"Path: {context.Request.Path} " +
                                    $"QueryString: {context.Request.QueryString} " +
                                    $"Response Body: View for {context.Request.Path}");
                await _next(context);
            }
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 
                                                0, 
                                                readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
    }
}