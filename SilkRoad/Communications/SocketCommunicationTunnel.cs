using System;
using System.Net;
using System.Net.Sockets;

namespace SilkRoad.Communications
{
    public abstract class SocketCommunicationTunnel : CommunicationTunnel
    {
        protected readonly Socket Socket;

        protected SocketCommunicationTunnel(IPEndPoint remoteEndPoint, IPEndPoint localEndPoint)
            : base(remoteEndPoint, localEndPoint)
        {
            RemoteEndPoint = remoteEndPoint;
            LocalEndPoint = localEndPoint;

            Socket = CreateSocket();
            ValidateSocket(Socket);
            InitSocket(Socket);
        }

        protected SocketCommunicationTunnel(Socket socket)
            : base((IPEndPoint)socket.RemoteEndPoint, (IPEndPoint)socket.LocalEndPoint)
        {
            RemoteEndPoint = (IPEndPoint) socket.RemoteEndPoint;
            LocalEndPoint = (IPEndPoint)socket.LocalEndPoint;

            Socket = socket;
            ValidateSocket(Socket);
            InitSocket(Socket);
        }

        private void ValidateSocket(Socket socket)
        {
            if (socket == null) throw new ArgumentNullException(nameof(socket));
            if (socket.SocketType != SocketType)
                throw new ArgumentException("Socket type must be of value " + SocketType);
            if (socket.ProtocolType != ProtocolType)
                throw new ArgumentException("Protocol type must be of value " + ProtocolType);
            if (socket.AddressFamily != AddressFamily)
                throw new ArgumentException("Address family must be of value " + AddressFamily);
        }

        private void InitSocket(Socket socket)
        {
            Socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
        }


        public abstract SocketType SocketType { get; }
        public abstract ProtocolType ProtocolType { get; }
        public virtual AddressFamily AddressFamily => AddressFamily.InterNetwork;

        public new IPEndPoint RemoteEndPoint { get; }
        public new IPEndPoint LocalEndPoint { get; }

        protected abstract Socket CreateSocket();

        public override void Dispose()
        {
            base.Dispose();
            Socket.Dispose();
        }
    }
}