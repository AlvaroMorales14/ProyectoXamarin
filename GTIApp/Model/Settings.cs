using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace GTIApp.Model
{
    public class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string UserNameKey = "username";
        private static readonly string UserNameDefault = string.Empty;

        private const string RememberMeKey = "rememberme";
        private static readonly string RememberMeDefault = string.Empty;

        private const string UserActiveKey = "useractive";
        private static readonly string UserActiveDefault = string.Empty;

        private const string LogOutKey = "logout";
        private static readonly int LogOutDefault = 0;

        #endregion


        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserNameKey, value);
            }
        }

        public static string RememberMe
        {
            get
            {
                return AppSettings.GetValueOrDefault(RememberMeKey, RememberMeDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(RememberMeKey, value);
            }
        }
        public static string UserActive
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserActiveKey, UserActiveDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserActiveKey, value);
            }
        }
        public static int LogOut
        {
            get
            {
                return AppSettings.GetValueOrDefault(LogOutKey, LogOutDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LogOutKey, value);
            }
        }
    }
}