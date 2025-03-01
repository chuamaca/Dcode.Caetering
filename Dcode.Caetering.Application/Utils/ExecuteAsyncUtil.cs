using Dcode.Caetering.Application.ResponseUtils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dcode.Caetering.Application.Utils
{
    public static class ExecuteAsyncUtil
    {
        public static async Task ExecuteAsync(Func<Task> func, /*ILogger logger,*/ string messageInitialLog = "") =>
            await ExecuteAsyncInternal(async () => await func(), /*logger,*/ messageInitialLog);

        public static async Task<Response<T>> ExecuteAsync<T>(Func<Task<Response<T>>> func, /*ILogger logger,*/ string messageInitialLog = "") =>
            await ExecuteAsyncInternal(async () => await func(),/* logger, */messageInitialLog);

        public static async Task<Response<bool>> ExecuteBoolAsync(Func<Task> func, /*ILogger logger,*/ string messageInitialLog = "") =>
            await ExecuteAsyncInternal(async () => { await func(); return new Response<bool> { Data = true, Success = true }; }, /*logger, */messageInitialLog);

        private static async Task ExecuteAsyncInternal(Func<Task> func, /*ILogger logger,*/ string messageInitialLog)
        {
            try
            {
                //logger.LogInformation("{messageInitialLog}", messageInitialLog);
                await func();
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "{LogMessage}", ex.Message);

            }
        }

        private static async Task<Response<T>> ExecuteAsyncInternal<T>(Func<Task<Response<T>>> func, /*ILogger logger,*/ string messageInitialLog)
        {
            try
            {
                //logger.LogInformation("{messageInitialLog}", messageInitialLog);
                return await func();
            }
            catch (Exception ex)
            {
                //logger.LogError(ex, "{LogMessage}", ex.Message);
                return new Response<T>
                {
                    Data = default,
                    Success = false,
                    IsWarning = true,
                    Message = ex.Message,
                    NoData = true
                };
            }
        }
    }
}
