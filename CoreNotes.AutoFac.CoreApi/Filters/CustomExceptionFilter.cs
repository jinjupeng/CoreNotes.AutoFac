using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreNotes.AutoFac.CoreApi.Filters
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        // 记录异常日志
        private readonly ILogger _logger = null;
        // 获取程序运行环境变量
        private readonly IWebHostEnvironment _webHostEnvironment = null;
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger, IWebHostEnvironment webHostEnvironment)
        {
            this._logger = logger;
            this._webHostEnvironment = webHostEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            string error = string.Empty;

            void ReadException(Exception ex)
            {
                error += $"{ex.Message} | {ex.StackTrace} | {ex.InnerException}";
                if (ex.InnerException != null)
                {
                    ReadException(ex.InnerException);
                }
            }

            ReadException(context.Exception);
            _logger.LogError(error);

            ContentResult result = new ContentResult
            {
                StatusCode = 500,
                ContentType = "text/json;charset=utf-8;"
            };

            if (_webHostEnvironment.IsDevelopment())
            {
                var json = new { message = exception.Message, detail = error };
                result.Content = JsonConvert.SerializeObject(json);
            }
            else
            {
                result.Content = "内部异常";
            }
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}