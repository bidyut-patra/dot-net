using System;
using System.Collections.Generic;
using System.Text;

namespace PES.SignalR.Core
{
    public class HubUrlRegistry
    {
        private static Dictionary<string, string> hubUrlsMap = new Dictionary<string, string>();

        /// <summary>
        /// Registers a hub class to generate its URL
        /// </summary>
        /// <param name="hubType"></param>
        /// <returns></returns>
        public static string RegisterHub(Type hubType)
        {
            string url = hubType.Name.Replace("hub", string.Empty, StringComparison.InvariantCultureIgnoreCase);
            url = @"/" + url.ToLower();
            hubUrlsMap.Add(hubType.Name, url);
            return url;
        }

        /// <summary>
        /// Gets the ULR for the registered hub class
        /// </summary>
        /// <param name="hubType"></param>
        /// <returns></returns>
        public static string GetUrl(Type hubType)
        {
            if(hubUrlsMap.ContainsKey(hubType.Name) == false)
            {
                RegisterHub(hubType);
            }
            return hubUrlsMap[hubType.Name];
        }
    }
}
