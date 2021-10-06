
namespace Core.Interfaces
{
    public interface ILoggerService
    {
        // Для отображения информации, которая может быть полезна при разработке и отладке приложения
        // path - Адрес действия
        // type - Тип действия
        // info - Информация в журнале
        // currentUserId - Идентификатор текущего пользователя
        void LogDebug(string path, string type, string info, string currentUserId);
        // Для отображения наиболее подробных сообщений
        void LogTrace(string path, string type, string info, string currentUserId);
        // Отслеживание потока выполнения приложения
        void LogInformation(string path, string type, string info, string currentUserId);
        // Для отображения сообщений о непредвиденных событиях
        void LogWarning(string path, string type, string info, string currentUserId);
        // Ошибки и исключения, которые произошли во время текущей операции и которые не могут быть обработаны
        void LogError(string path, string type, string info, string currentUserId);
        // Уровень критических ошибок, требующих немедленного реагирования
        void LogCritical(string path, string type, string info, string currentUserId);
    }
}
