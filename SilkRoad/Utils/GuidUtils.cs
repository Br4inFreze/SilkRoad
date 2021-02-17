using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SilkRoad.Utils
{
    public static class GuidUtils
    {
        public static Guid CreateUniqueId(IPAddress ip)
        {
            var bytes = new List<byte>(16);
            bytes.AddRange(ip.GetAddressBytes());
            bytes.AddRange(Enumerable.Repeat<byte>(5, bytes.Capacity - bytes.Count));
            return new Guid(bytes.ToArray());
        }
    }
}
