using FastSocket.Client.Protocol;
using FastSocket.SocketBase;
using FastSocket.SocketBase.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.TCP.Client
{
    public class ClientProtocol : IProtocol<ClientMessage>
    {
        public bool IsAsync => true;

        public int DefaultSyncSeqID => 1;

        public CustomProtocol Protocols { get; set; }

        public ClientMessage Parse(IConnection connection, ArraySegment<byte> buffer, out int readlength)
        {

            //var bt = this.Protocols.Parse(buffer, out readlength);
            var data = new byte[buffer.Count];
            Buffer.BlockCopy(buffer.Array, 0, data, 0, data.Length);
            readlength = data.Length;
            return new ClientMessage(1, data);
        }
    }
}
