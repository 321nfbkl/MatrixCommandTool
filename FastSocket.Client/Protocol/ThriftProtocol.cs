using System;
using FastSocket.Client.Messaging;
using FastSocket.SocketBase;

namespace FastSocket.Client.Protocol
{
    public sealed class ThriftProtocol : IProtocol<Messaging.ThriftMessage>
    {
        /// <summary>
        /// 此协议为异步协议
        /// </summary>
        public bool IsAsync
        {
            get => true;
        }

        public int DefaultSyncSeqID
        {
            get => throw new NotImplementedException();
        }
        
        /// <summary>
        /// find response
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="buffer"></param>
        /// <param name="readlength"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ThriftMessage Parse(IConnection connection, ArraySegment<byte> buffer, out int readlength)
        {
            if (buffer.Count < 4)
            {
                readlength = 0;
                return null;
            }
            //获取message length
            var messageLength = SocketBase.Utils.NetworkBitConverter.ToInt32(buffer.Array, buffer.Offset);
            if (messageLength < 14) throw new BadProtocolException("bad thrift protocol");
            readlength = messageLength + 4;
            if (buffer.Count < readlength)
            {
                readlength = 0;
                return null;
            }
            var cmdLen = SocketBase.Utils.NetworkBitConverter.ToInt32(buffer.Array, buffer.Offset + 8);
            if (messageLength < cmdLen + 13) throw new BadProtocolException("bad thrift protocol");
            int seqID = SocketBase.Utils.NetworkBitConverter.ToInt32(buffer.Array, buffer.Offset + 12 + cmdLen);
            var data = new byte[messageLength];
            Buffer.BlockCopy(buffer.Array, buffer.Offset + 4, data, 0, messageLength);
            return new Messaging.ThriftMessage(seqID, data);
        }
    }
}