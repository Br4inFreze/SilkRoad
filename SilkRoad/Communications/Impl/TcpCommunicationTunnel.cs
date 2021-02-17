using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SilkRoad.Utils;

namespace SilkRoad.Communications.Impl
{
    internal class TcpCommunicationTunnel : SocketCommunicationTunnel
    {
        public TcpCommunicationTunnel(IPEndPoint remoteEndPoint, IPEndPoint localEndPoint) 
            : base(remoteEndPoint,localEndPoint)
        {

        }

        public TcpCommunicationTunnel(Socket socket) 
            : base(socket)
        {

        }

        public override ConnectionState ConnectionState => Socket.Connected ? ConnectionState.Open : ConnectionState.Close;

        public override void Connect()
        {
            if (ConnectionState == ConnectionState.Open) throw new InvalidOperationException();
            Socket.Connect(RemoteEndPoint);
        }

        public override Task ConnectAsync()
        {
            if (ConnectionState == ConnectionState.Open) throw new InvalidOperationException();
            return Socket.ConnectAsync(RemoteEndPoint);
        }

        public override void Close()
        {
            Socket.Close();
        }

        public override Task CloseAsync()
        {
            Socket.Close();
            return Task.FromResult(true);
        }

        public override byte[] Receive()
        {
            var buffer = new byte[1024];
            var bytesRead = Socket.Receive(buffer);
            return buffer;
        }

        public override async Task<byte[]> ReceiveAsync()
        {
            var buffer = new byte[1024];
            var bytesRead = await Socket.ReceiveAsync(buffer, SocketFlags.None);
            return buffer;
        }

        public override void Send(byte[] data)
        {
            Socket.Send(data);
        }

        public override Task SendAsync(byte[] data)
        {
            return Socket.SendAsync(data, SocketFlags.None);
        }

        public override SocketType SocketType => SocketType.Stream;
        public override ProtocolType ProtocolType => ProtocolType.Tcp;
        protected override Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType, ProtocolType);
        }
    }
}
