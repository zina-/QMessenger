using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QMessenger.Models
{
    public class LocalCache
    {
        public List<string> ConnectionIdCollection { get; set; }
        public User UserInfo { get; set; }

        public LocalCache()
        {
            ConnectionIdCollection = new List<string>();
        }
    }
}