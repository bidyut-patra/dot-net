using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class UserSession
    {
        public DateTime StartTime { get; internal set; }
        public DateTime EndTime { get; internal set; }
        public UserInfo UserInfo { get; internal set; }
        public string Token { get; internal set; }
    }
}
