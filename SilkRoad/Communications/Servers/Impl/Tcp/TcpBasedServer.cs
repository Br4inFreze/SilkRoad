using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SilkRoad.Communications.Clients;
using SilkRoad.Messages;
using SilkRoad.Utils;

namespace SilkRoad.Servers.Impl.Tcp
{
    public abstract class TcpBasedServer<TClient> : Server<TClient>
        where TClient : Client
    {
        private TcpListener _tcpListener;
        private bool _isStarted;

        protected TcpBasedServer(TcpServerOptions options, int port)
        {
            Options = options;
            Port = port;
        }

        public override bool IsStarted => _isStarted;
        public TcpServerOptions Options { get; }
        public int Port { get; }

        public override void Start()
        {
            if (_isStarted) throw new Exception("Already started..");
            _tcpListener = new TcpListener(IPAddress.Any, Port);
            _tcpListener.Start();
            _isStarted = true;
        }

        public override void Stop()
        {
            if (!_isStarted) throw new Exception("Server not running..");
            _tcpListener.Stop();
            _isStarted = false;
        }

        public override TClient AcceptClient()
        {
            if (!_isStarted) throw new InvalidOperationException("Please start first");
            var acceptedTcpClient = _tcpListener.AcceptTcpClient();
            return InternalTcpClientConnected(acceptedTcpClient);
        }

        /// <inheritdoc />
        public override async Task<TClient> AcceptClientAsync()
        {
            if (!_isStarted) throw new InvalidOperationException("Please start first");
            var acceptedTcpClient = await _tcpListener.AcceptTcpClientAsync();
            return InternalTcpClientConnected(acceptedTcpClient);
        }

        private TClient InternalTcpClientConnected(TcpClient acceptedTcpClient)
        {
            var endPoint = ((IPEndPoint)acceptedTcpClient.Client.RemoteEndPoint);
            var id = GuidUtils.CreateUniqueId(endPoint.Address);

            var denyReason = OnClientAcceptanceQuestioned(id, endPoint, acceptedTcpClient);
            if (denyReason != ServerAcceptanceMessage.ReasonOk)
            {
                acceptedTcpClient.Client.Send(ServerAcceptanceMessage.Deny(id, denyReason).ToBytes());
                return null;
            }

            acceptedTcpClient.Client.Send(ServerAcceptanceMessage.Approve(id).ToBytes());
            var client = CreateClient(id, endPoint, acceptedTcpClient);
            OnClientConnected(client);
            return client;
        }

        protected abstract TClient CreateClient(Guid id, IPEndPoint ip, TcpClient tcpClient);

        protected virtual int OnClientAcceptanceQuestioned(Guid id, IPEndPoint ip, TcpClient tcpClient)
        {
            return ServerAcceptanceMessage.ReasonOk;
        }
    }
}