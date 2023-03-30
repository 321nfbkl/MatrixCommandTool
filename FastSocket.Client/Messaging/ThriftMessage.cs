namespace FastSocket.Client.Messaging
{
    public class ThriftMessage : IMessage
    {
        public readonly int _seqID;

        /// <summary>
        /// seqID
        /// </summary>
        public int SeqID
        {
            get => this._seqID;
        }


        /// <summary>
        /// payload
        /// </summary>
        public readonly byte[] Payload = null;

        /// <summary>
        /// new
        /// </summary>
        /// <param name="seqID"></param>
        /// <param name="buffer"></param>
        public ThriftMessage(int seqID,byte[] buffer)
        {
            this._seqID = seqID;
            this.Payload = buffer;
        }
        
    }
}