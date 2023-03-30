using System;

namespace FastSocket.Client.Protocol
{
    /// <summary>
    /// 协议接口
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IProtocol<TMessage> where TMessage : class,Messaging.IMessage
    {
        /// <summary>
        /// 是否为异步协议
        /// </summary>
        bool IsAsync { get; }
        
        /// <summary>
        /// 
        /// </summary>
        int DefaultSyncSeqID { get; }

        /// <summary>
        /// parse
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="buffer"></param>
        /// <param name="readlength"></param>
        /// <returns></returns>
        TMessage Parse(SocketBase.IConnection connection, ArraySegment<byte> buffer, out int readlength);
    }
}