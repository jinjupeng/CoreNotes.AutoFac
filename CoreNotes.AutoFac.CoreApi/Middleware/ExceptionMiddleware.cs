using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreNotes.AutoFac.CoreApi.Middleware
{
    /// <summary>
    /// 在中间件中捕获全局异常
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment webHostEnvironment)
        {
            this._next = next;
            this._logger = logger;
            this._webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 在管道中使用try/catch进行捕获异常
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context).ConfigureAwait(false);
                var features = context.Features;
            }
            catch (Exception e)
            {
                await HandleException(context, e).ConfigureAwait(false);
            }
        }
        /// <summary>
        /// 处理全局异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private async Task HandleException(HttpContext context, Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/json;charset=utf-8;";
            string error = "";

            void ReadException(Exception ex)
            {
                error += $"{ex.Message} | {ex.StackTrace} | {ex.InnerException}";
                if (ex.InnerException != null)
                {
                    ReadException(ex.InnerException);
                }
            }

            ReadException(e);
            // 如果是开发环境模式，输出详细的错误信息
            if (_webHostEnvironment.IsDevelopment())
            {
                var json = new { message = e.Message, detail = error };
                error = JsonConvert.SerializeObject(json);
            }
            // 非开发环境下仅提示错误
            else
            {
                error = "抱歉，出错了";
            }

            await context.Response.WriteAsync(error).ConfigureAwait(false);
        }
    }
}
