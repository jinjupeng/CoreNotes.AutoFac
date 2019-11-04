using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreNotes.AutoFac.CoreApi.Filters
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        // 记录异常日志
        private readonly ILogger logger = null;
        // 获取程序运行环境变量
        private readonly IHostingEnvironment environment = null;
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger, IHostingEnvironment environment)
        {
            this.logger = logger;
            this.environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            string error = string.Empty;

            void ReadException(Exception ex)
            {
                error += string.Format("{0} | {1} | {2}", ex.Message, ex.StackTrace, ex.InnerException);
                if (ex.InnerException != null)
                {
                    ReadException(ex.InnerException);
                }
            }

            ReadException(context.Exception);
            logger.LogError(error);

            ContentResult result = new ContentResult
            {
                StatusCode = 500,
                ContentType = "text/json;charset=utf-8;"
            };

            if (environment.IsDevelopment())
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