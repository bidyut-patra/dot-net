using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class AuthenticationHelper
    {
        public static byte[] GetBytes(string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }

        public static string GetString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static string GetConfigurationValue(string parameter, string defaultValue)
        {
            var parameterValue = ConfigurationManager.AppSettings[parameter];
            return (parameterValue == null) ? defaultValue : parameterValue.ToString();
        }
    }
}
