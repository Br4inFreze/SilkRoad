using System;
using System.Collections.Generic;
using System.Text;

namespace SilkRoad.Messages
{
    public struct ServerAcceptanceMessage
    {
        public const int ReasonOk = 0;

        public static ServerAcceptanceMessage Deny(Guid clientId, int reason)
        {
            return new ServerAcceptanceMessage(clientId, false, reason);
        }

        public static ServerAcceptanceMessage Approve(Guid clientId)
        {
            return new ServerAcceptanceMessage(clientId, true, ReasonOk);
        }

        public ServerAcceptanceMessage(Guid clientId, bool isAccepted, int deniedReason)
        {
            ClientId = clientId;
            IsAccepted = isAccepted;
            DeniedReason = deniedReason;
        }

        public Guid ClientId;
        public readonly bool IsAccepted;
        public readonly int DeniedReason;
    }
}
