using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SilkRoad.Communications.Impl
{
    internal class UdpCommunicationTunnel : SocketCommunicationTunnel
    {
        public UdpCommunicationTunnel(IPEndPoint remoteEndPoint, IPEndPoint localEndPoint)
            : base(remoteEndPoint, localEndPoint)
        {
            Socket.Bind(new IPEndPoint(IPAddress.Any, remoteEndPoint.Port));
        }

        public UdpCommunicationTunnel(Socket socket)
            : base(socket)
        {
            Socket.Bind(new IPEndPoint(IPAddress.Any, RemoteEndPoint.Port));
        }

        public override ConnectionState ConnectionState => ConnectionState.Undetermine;

        public override void Connect()
        {
            throw new System.NotImplementedException();
        }

        public override Task ConnectAsync()
        {
            throw new System.NotImplementedException();
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
            //TODO Duplicated code
            var ipEndPoint = (EndPoint)new IPEndPoint(IPAddress.Any, RemoteEndPoint.Port);
            var buffer = new byte[1024];
            int bytesRead = Socket.ReceiveFrom(buffer, ref ipEndPoint);
            return buffer;
        }

        public override async Task<byte[]> ReceiveAsync()
        {
            //TODO Duplicated code
            var ipEndPoint = (EndPoint)new IPEndPoint(IPAddress.Any, RemoteEndPoint.Port);
            var buffer = new byte[1024];
            var result = await Socket.ReceiveFromAsync(buffer, SocketFlags.None, ipEndPoint);
            var intBytesRead = result.ReceivedBytes;
            return buffer;
        }

        public override void Send(byte[] data)
        {
            Socket.SendTo(data, RemoteEndPoint);
        }

        public override Task SendAsync(byte[] data)
        {
            return Socket.SendToAsync(data, SocketFlags.None, RemoteEndPoint);
        }

        public override SocketType SocketType => SocketType.Dgram;
        public override ProtocolType ProtocolType => ProtocolType.Udp;

        protected override Socket CreateSocket()
        {
            return new Socket(AddressFamily, SocketType, ProtocolType);
        }

        public override void Dispose()
        {
            base.Dispose();
            Socket.Dispose();
        }
    }
}