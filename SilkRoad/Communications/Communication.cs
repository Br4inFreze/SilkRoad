using System.Net;
using System.Net.Sockets;
using SilkRoad.Communications.Impl;

namespace SilkRoad.Communications
{
    public static class Communication
    {
        public static SocketCommunicationTunnel Tcp(IPEndPoint remoteEndPoint, IPEndPoint localEndPoint)
        {
            return new TcpCommunicationTunnel(remoteEndPoint, localEndPoint);
        }

        public static SocketCommunicationTunnel Tcp(Socket socket)
        {
            return new TcpCommunicationTunnel(socket);
        }

        public static SocketCommunicationTunnel Udp(Socket socket)
        {
            return new UdpCommunicationTunnel(socket);
        }

        public static SocketCommunicationTunnel Udp(IPEndPoint remoteEndPoint, IPEndPoint localEndPoint)
        {
            return new UdpCommunicationTunnel(remoteEndPoint, localEndPoint);
        }
    }
}
