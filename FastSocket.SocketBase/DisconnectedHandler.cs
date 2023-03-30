using System;

namespace FastSocket.SocketBase
{
    /// <summary>
    /// connection disconnected delegate
    /// </summary>
    public delegate void DisconnectedHandler(IConnection connection, Exception ex);
}