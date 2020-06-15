using LIMSWebPortalAPIApp.Contracts;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILogger = NLog.ILogger;

namespace LIMSWebPortalAPIApp.Services
{
    public class LoggerService : ILoggerService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        void ILoggerService.LogDebug(string message)
        {
            logger.Debug(message);
        }

        void ILoggerService.LogError(string message)
        {
            logger.Error(message);
        }

        void ILoggerService.LogInfo(string message)
        {
            logger.Info(message);
        }

        void ILoggerService.LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
