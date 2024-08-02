using Intl.Realty.Firm.Models.Models;
using Intl.Realty.Firm.Models.Models.Auxiliary;
using Intl.Realty.Firm.Repository.IRepository;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Intl.Realty.Firm.Helper
{
    public static class Activity
    {
        private static IActivityLogRepository _activityService;
        private static IActivityLogRepository _logService;
        private static IModuleRepository _moduleService;
        private static IMemoryCache _memoryCache;
        public static void Configure(IServiceProvider serviceProvider)
        {
            _logService = serviceProvider.GetService<IActivityLogRepository>()!;
            _activityService = serviceProvider.GetService<IActivityLogRepository>()!;
            _moduleService = serviceProvider.GetService<IModuleRepository>()!;
            _memoryCache = serviceProvider.GetService<IMemoryCache>()!;
        }

        public static void Log<T>(ActivityType activityType, T oldValue)
        {
            if (activityType == ActivityType.BEFORE_UPDATE
                || activityType == ActivityType.BEFORE_DELETE)
            {
                if (oldValue is not null)
                {
                    string oValue = JsonConvert.SerializeObject(oldValue, Formatting.Indented);
                    _memoryCache.Set(Session.UserId, oValue, TimeSpan.FromMinutes(2));
                }
            }
        }

        public static void Log<T>(ActivityType activityType, Type tpe, T newValue)
        {
            var module = GetModule(tpe);
            string oValue = GetOldValue();
            string nValue = JsonConvert.SerializeObject(newValue, Formatting.Indented);
            var log = new ActivityLog(module.Id, activityType, oValue, nValue, Session.GetInt(SessionKey.UserId));
            _logService.AddAsync(log);
        }

        public static void Log<T>(ActivityType activityType, Type tpe, T newValue, T oldValue)
        {
            var module = GetModule(tpe);
            string oValue = JsonConvert.SerializeObject(oldValue, Formatting.Indented);
            string nValue = JsonConvert.SerializeObject(newValue, Formatting.Indented);
            var log = new ActivityLog(module.Id, activityType, oValue, nValue, Session.GetInt(SessionKey.UserId));
            _logService.AddAsync(log);
        }

        private static string GetOldValue()
        {
            string oValue = string.Empty;
            if (_memoryCache.TryGetValue(Session.UserId, out oValue))
                _memoryCache.Remove(Session.UserId);
            return oValue;
        }

        private static Module? GetModule(Type tpe)
        {
            var currentModules = Attribute.GetCustomAttributes(tpe);
            var currentModule = currentModules.OfType<Module>().FirstOrDefault();
            var moduleRecord = _moduleService.GetAllAsync().Result;
            return moduleRecord.FirstOrDefault(f => f.Name.Equals(currentModule.Name));
        }
    }
}
