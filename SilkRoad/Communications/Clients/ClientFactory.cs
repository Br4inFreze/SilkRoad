using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SilkRoad.Communications;
using SilkRoad.Communications.Clients;
using SilkRoad.Communications.Impl;
using SilkRoad.Messages;

namespace SilkRoad.Clients
{
    public abstract class ClientFactory<TClient>
        where TClient : Client
    {
        public abstract TClient Connect(EndPoint endpoint);
    }

    public abstract class TcpBasedClientFactory<TClient> : ClientFactory<TClient>
        where TClient : Client
    {
        public override TClient Connect(EndPoint endpoint)
        {
            if (!(endpoint is IPEndPoint))
                throw new NotSupportedException("Endpoint not supported");
            var ipAddress = (IPEndPoint)endpoint;

            var tcpClient = new TcpClient(ipAddress.Address.ToString(), ipAddress.Port);
            var tcpTunnel = Communication.Tcp(tcpClient.Client);
            var acceptance = tcpTunnel.Receive<ServerAcceptanceMessage>();
            if (!acceptance.IsAccepted) throw new Exception("Connection denied for reason " + acceptance.DeniedReason);
            return OnConnectionAccepted(acceptance.ClientId, tcpTunnel);
        }

        protected abstract TClient OnConnectionAccepted(Guid id, SocketCommunicationTunnel tcpTunnel);
    }
}
