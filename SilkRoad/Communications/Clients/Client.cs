using System;

namespace SilkRoad.Communications.Clients
{
    public abstract class Client
    {
        protected Client(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}