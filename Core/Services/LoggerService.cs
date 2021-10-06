using Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        private readonly IUserHelper _userHelper;
        public LoggerService(ILogger<LoggerService> logger,
            IUserHelper userHelper)
        {
            _logger = logger;
            _userHelper = userHelper;
        }

        // Уровень критических ошибок, требующих немедленного реагирования
        public void LogCritical(string path, string type, string info, string currentUserId)
        {
            _logger.LogCritical($"[{DateTime.Now.ToString()}]:[{path}]:[type:{type}]:[critical:{info}]:[user:{_userHelper.GetIdUserById(currentUserId)}]");
        }

        // Для отображения информации, которая может быть полезна при разработке и отладке приложения
        public void LogDebug(string path, string type, string info, string currentUserId)
        {
            _logger.LogDebug($"[{DateTime.Now.ToString()}]:[{path}]:[type:{type}]:[debug:{info}]:[user:{_userHelper.GetIdUserById(currentUserId)}]");
        }

        // Ошибки и исключения, которые произошли во время текущей операции и которые не могут быть обработаны
        public void LogError(string path, string type, string info, string currentUserId)
        {
            _logger.LogError($"[{DateTime.Now.ToString()}]:[{path}]:[type:{type}]:[error:{info}]:[user:{_userHelper.GetIdUserById(currentUserId)}]");
        }

        // Отслеживание потока выполнения приложения
        public void LogInformation(string path, string type, string info, string currentUserId)
        {
            _logger.LogInformation($"[{DateTime.Now.ToString()}]:[{path}]:[type:{type}]:[info:{info}]:[user:{_userHelper.GetIdUserById(currentUserId)}]");
        }

        // Для отображения наиболее подробных сообщений
        public void LogTrace(string path, string type, string info, string currentUserId)
        {
            _logger.LogTrace($"[{DateTime.Now.ToString()}]:[{path}]:[type:{type}]:[trace:{info}]:[user:{_userHelper.GetIdUserById(currentUserId)}]");
        }

        // Для отображения сообщений о непредвиденных событиях
        public void LogWarning(string path, string type, string info, string currentUserId)
        {
            _logger.LogWarning($"[{DateTime.Now.ToString()}]:[{path}]:[type:{type}]:[warning:{info}]:[user:{_userHelper.GetIdUserById(currentUserId)}]");
        }
    }
}
