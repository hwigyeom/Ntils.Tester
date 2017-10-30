using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Ntils.Hubs;

namespace Ntils.Middlewares
{
    public class WebRequestTraceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<WebRequestTraceMiddleware> _logger;
        private readonly IHubContext<WebRequestTracerHub> _hub;

        public WebRequestTraceMiddleware(RequestDelegate next, ILogger<WebRequestTraceMiddleware> logger, IHubContext<WebRequestTracerHub> hub)
        {
            _next = next;
            _logger = logger;
            _hub = hub;
        }

        public async Task Invoke(HttpContext context)
        {
            // Request 및 Response 객체에 포함된 원본 Body Stream을 저장해 둠
            var originalRequestBodyStream = context.Request.Body;
            var originalResponseBodyStream = context.Response.Body;

            // Request 및 Response 객체의 Body Stream을 대체할 MemoryStream 을 생성
            var requestBodyStream = new MemoryStream();
            var responseBodyStream = new MemoryStream();

            // 요청 정보를 나타내는 메시징 객체를 생성
            var message = new WebRequestTracerHubModel();

            // Request Body 스트림의 내용을 MemoryStream 으로 복사
            await originalRequestBodyStream.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            // Request 및 Response 객체의 Body 의 Stream을 MemoryStream 으로 교체
            context.Request.Body = requestBodyStream;
            context.Response.Body = responseBodyStream;

            // Request Body 스트림의 내용을 읽어 Trace 객체를 작성하고 로그 추가
            var requestBody = new StreamReader(requestBodyStream).ReadToEnd();
            message.SetRequest(context.Request, requestBody);
            _logger.LogInformation(message.Request.ToString());
            
            // Request 스트림의 위치를 다시 시작지점으로 이동
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            // 요청 파이프라인의 실행을 계속
            await _next(context);

            // Response Body의 내용을 MemoryStream 으로 복사
            await responseBodyStream.CopyToAsync(originalResponseBodyStream);
            responseBodyStream.Seek(0, SeekOrigin.Begin);
            
            // Response Body 스트림의 내용을 읽고 Trace 객체를 작성하고 로그 추가
            var responseBody = new StreamReader(responseBodyStream).ReadToEnd();
            message.SetResponse(context.Response, responseBody);
            _logger.LogInformation(message.Response.ToString());
            
            // Response 스트림의 위치를 다시 시작지점으로 이동
            responseBodyStream.Seek(0, SeekOrigin.Begin);

            // Response Body MemoryStream의 내용을 원본 Body Stream 으로 복사
            await responseBodyStream.CopyToAsync(originalResponseBodyStream);

            // Request 및 Response 객체의 Body Stream을 원본 Body Stream 으로 복구
            context.Request.Body = originalRequestBodyStream;
            context.Response.Body = originalResponseBodyStream;
        }
    }
}