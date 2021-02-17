using System;
using System.Collections.Generic;
using System.Text;

namespace SilkRoad.Servers.Impl.Tcp
{
    public class TcpServerOptions
    {
        public TcpServerOptions(ClientMultipleConnectionBehaviour clientMultipleConnectionBehaviour = ClientMultipleConnectionBehaviour.DenyIncoming)
        {
            ClientMultipleConnectionBehaviour = clientMultipleConnectionBehaviour;
        }

        public ClientMultipleConnectionBehaviour ClientMultipleConnectionBehaviour { get; }
    }

    public enum ClientMultipleConnectionBehaviour
    {
        DenyAll,
        DenyExisting,
        DenyIncoming,
        Throw,
    }
}

