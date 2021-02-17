using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SilkRoad.Communications.Clients;

namespace SilkRoad.Servers
{
    /// <summary>
    /// This class represent an end-node on the system, who provides connectivity and functionality for multiple <see cref="Client"/>
    /// </summary>
    /// <typeparam name="TClient">The type of clients the server will handle</typeparam>
    public abstract class Server<TClient> : IDisposable, IEnumerable<TClient>
        where TClient : Client
    {
        /// <summary>
        /// Gets a value indicating whether the server is up and running
        /// </summary>
        public abstract bool IsStarted { get; }

        /// <summary>
        /// Gets a value indicating whether the server is accepting incoming connections
        /// </summary>
        public abstract bool IsAccepting { get; }

        /// <summary>
        /// Starts the server
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws when the server is already started</exception>
        public abstract void Start();

        /// <summary>
        /// Stops the server
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws when the server isn't running</exception>
        public abstract void Stop();

        /// <summary>
        /// Accepting a incoming <typeparam name="TClient" />.
        /// </summary>
        /// <returns>
        /// Accepted client
        /// </returns>
        public abstract TClient AcceptClient();

        /// <summary>
        /// Asynchronously accepting a incoming <typeparam name="TClient" />.
        /// </summary>
        /// <returns>
        /// Accepted client
        /// </returns>
        public abstract Task<TClient> AcceptClientAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public abstract void DisconnectClient(TClient client);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public abstract Task DisconnectClientAsync(TClient client);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public abstract void Broadcast<T>(T message) where T : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract Task BroadcastAsync<T>(T message) where T : struct;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public virtual bool IsClientConnected(TClient client)
        {
            return IsClientConnected(client.Id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract bool IsClientConnected(Guid id);

        /// <inheritdoc/>
        public abstract IEnumerator<TClient> GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        protected virtual void OnClientConnected(TClient client)
        {

        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {

        }
    }
}
