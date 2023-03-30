using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FastSocket.Client.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.TCP.Client
{
    public class ClientMessage : IMessage
    {
        private int mSeqID;
        public int SeqID
        {
            get => mSeqID;
            private set => mSeqID = value;
        }

        public IDictionary Properties => throw new NotImplementedException();

        public readonly byte[] Payload = null;

        public ClientMessage(int seqID, byte[] payload)
        {
            this.SeqID = seqID;
            this.Payload = payload;
        }
    }
}
