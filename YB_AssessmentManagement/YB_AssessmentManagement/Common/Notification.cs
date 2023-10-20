using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace YB_AssessmentManagement.Common
{
    public static class Notification
    {
        private static readonly IDictionary<string, string> NotificationKey = new Dictionary<string, string>
        {
            { "Error",      "App.Notifications.Error" },
            { "Warning",    "App.Notifications.Warning" },
            { "Success",    "App.Notifications.Success" },
            { "Info",       "App.Notifications.Info" }
        };

        public static void AddNotification(this Controller controller, string message, string notificationType)
        {
            string NotificationKey = GetNotificationKeyByType(notificationType);
            ICollection<string> messages = controller.ViewData[NotificationKey] as ICollection<string>;
            if (messages == null)
            {
                controller.TempData[NotificationKey] = (messages = new HashSet<string>());
            }
            messages.Add(message);
        }
        public static IEnumerable<string> GetNotifications(this IHtmlHelper htmlHelper, string notificationType)
        {
            string NotificationKey = GetNotificationKeyByType(notificationType);
            return htmlHelper.ViewContext.ViewData[NotificationKey] as ICollection<string> ?? null;
        }

        private static string GetNotificationKeyByType(string notificationType)
        {
            try
            {
                return NotificationKey[notificationType];
            }
            catch (IndexOutOfRangeException e)
            {
                ArgumentException exception = new ArgumentException("Key is invalid", "notificationType", e);
                throw exception;
            }
        }
    }

    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }

    public static class NotificationType
    {
        public const string ERROR = "Error";
        public const string WARNING = "Warning";
        public const string SUCCESS = "Success";
        public const string INFO = "Info";
    }
}
