using System;
using UnityEngine;
using NotTouch;

#if UNITY_ANDROID && !UNITY_EDITOR

using System.Linq;

#endif

namespace Managers
{
    public static class NotificationManager
    {
#if UNITY_ANDROID && !UNITY_EDITOR

        private const string FullClassName = "com.hippogames.simpleandroidnotifications.Controller";
        private const string MainActivityClassName = "com.unity3d.player.UnityPlayerActivity";

#endif

        public static int Send(int id, DateTime date, string title, string message, Color smallIconColor, NotificationIcon smallIcon = 0)
        {
            return Send(id, (date - DateTime.Now), title, message, smallIconColor, smallIcon);
        }
        /// <summary>
        /// Schedule simple notification without app icon.
        /// </summary>
        /// <param name="smallIcon">List of build-in small icons: notification_icon_bell (default), notification_icon_clock, notification_icon_heart, notification_icon_message, notification_icon_nut, notification_icon_star, notification_icon_warning.</param>

        public static int Send(int id, TimeSpan delay, string title, string message, Color smallIconColor, NotificationIcon smallIcon = 0)
        {
            return SendCustom(new NotificationParams
            {
                Id = id,
                Delay = delay,
                Title = title,
                Message = message,
                Ticker = message,
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = smallIcon,
                SmallIconColor = smallIconColor,
                LargeIcon = ""
            });
        }

        /// <summary>
        /// Schedule notification with app icon.
        /// </summary>
        /// <param name="smallIcon">List of build-in small icons: notification_icon_bell (default), notification_icon_clock, notification_icon_heart, notification_icon_message, notification_icon_nut, notification_icon_star, notification_icon_warning.</param>
        public static int SendWithAppIcon(TimeSpan delay, string title, string message, Color smallIconColor, NotificationIcon smallIcon = 0)
        {
            return SendCustom(new NotificationParams
            {
                Id = UnityEngine.Random.Range(0, int.MaxValue),
                Delay = delay,
                Title = title,
                Message = message,
                Ticker = message,
                Sound = true,
                Vibrate = true,
                Light = true,
                SmallIcon = smallIcon,
                SmallIconColor = smallIconColor,
                LargeIcon = "app_icon"
            });
        }

        /// <summary>
        /// Schedule customizable notification.
        /// </summary>
        public static int SendCustom(NotificationParams notificationParams)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

            var p = notificationParams;
            var delay = (long) p.Delay.TotalMilliseconds;

            new AndroidJavaClass(FullClassName).CallStatic("SetNotification", p.Id, delay, p.Title, p.Message, p.Ticker,
                p.Sound ? 1 : 0, p.Vibrate ? 1 : 0, p.Light ? 1 : 0, p.LargeIcon, GetSmallIconName(p.SmallIcon), ColotToInt(p.SmallIconColor), MainActivityClassName);
            #endif

            return notificationParams.Id;
        }

        /// <summary>
        /// Cancel notification by id.
        /// </summary>
        public static void Cancel(int id)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

            new AndroidJavaClass(FullClassName).CallStatic("CancelScheduledNotification", id);

            #endif
        }

        /// <summary>
        /// Cancel all notifications.
        /// </summary>
        public static void CancelAll()
        {
            #if UNITY_ANDROID && !UNITY_EDITOR

            new AndroidJavaClass(FullClassName).CallStatic("CancelAllScheduledNotifications");

            #endif
        }

        private static int ColotToInt(Color color)
        {
            var smallIconColor = (Color32) color;
            
            return smallIconColor.r * 65536 + smallIconColor.g * 256 + smallIconColor.b;
        }

        private static string GetSmallIconName(NotificationIcon icon)
        {
            return "anp_" + icon.ToString().ToLower();
        }
    }
}

namespace NotTouch
{
    public class NotificationParams
    {
        /// <summary>
        /// Use random id for each new notification.
        /// </summary>
        public int Id;
        public TimeSpan Delay;
        public string Title;
        public string Message;
        public string Ticker;
        public bool Sound = true;
        public bool Vibrate = true;
        public bool Light = true;
        public NotificationIcon SmallIcon;
        public Color SmallIconColor;
        /// <summary>
        /// Use "" for simple notification. Use "app_icon" to use the app icon. Use custom value but first place image to "simple-android-notifications.aar/res/". To modify "aar" file just rename it to "zip" and back.
        /// </summary>
        public string LargeIcon;
    }

    public enum NotificationIcon
    {
        Bell,
        Clock,
        Event,
        Heart,
        Message,
        Star
    }
}
