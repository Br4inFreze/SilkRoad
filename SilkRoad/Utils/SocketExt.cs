using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SilkRoad.Utils
{
    public static class SocketExt
    {
        public static IPAddress GetIPAddress(this Socket socket)
        {
            if (socket.RemoteEndPoint is IPEndPoint ipEndPoint)
                return ipEndPoint.Address;
            return IPAddress.Any;
        }

        public static int GetPort(this Socket socket)
        {
            if (socket.RemoteEndPoint is IPEndPoint ipEndPoint)
                return ipEndPoint.Port;
            return -1;
        }
    }
}
