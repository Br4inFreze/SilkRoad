using System;
using System.Net;
using System.Threading.Tasks;
using SilkRoad.Utils;

namespace SilkRoad.Communications
{
    public abstract class CommunicationTunnel : IDisposable
    {
        protected CommunicationTunnel(EndPoint remoteEndPoint, EndPoint localEndPoint)
        {
            RemoteEndPoint = remoteEndPoint;
            LocalEndPoint = localEndPoint;
        }

        ~CommunicationTunnel()
        {
            Dispose();
        }

        public EndPoint RemoteEndPoint { get; }
        public EndPoint LocalEndPoint { get; }
        public abstract ConnectionState ConnectionState { get; }

        public abstract void Connect();
        public abstract Task ConnectAsync();

        public abstract void Close();
        public abstract Task CloseAsync();

        public abstract byte[] Receive();
        public abstract Task<byte[]> ReceiveAsync();

        public T Receive<T>() where T : struct
        {
            //TODO Validate received size to structure size
            var bytes = Receive();
            return bytes.ToStructure<T>();
        }

        public async Task<T> ReceiveAsync<T>() where T : struct
        {
            //TODO Validate received size to structure size
            var bytes = await ReceiveAsync();
            return bytes.ToStructure<T>();
        }

        public abstract void Send(byte[] data);
        public abstract Task SendAsync(byte[] data);

        public void Send<T>(T data) where T : struct
        {
            Send(data.ToBytes());
        }

        public Task SendAsync<T>(T data) where T : struct
        {
            return SendAsync(data.ToBytes());
        }

        public virtual void Dispose()
        {
        }
    }
}
