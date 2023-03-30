using System;

namespace FastSocket.Client
{
    public class Request<TMessage> : SocketBase.Packet where TMessage : Messaging.IMessage
    {
        #region Members

        /// <summary>
        /// default is don't allow retry send.
        /// </summary>
        internal bool AllowRetry = false;

        /// <summary>
        /// get or set send connection
        /// </summary>
        internal SocketBase.IConnection SendConnection = null;

        /// <summary>
        /// get or set sent time, default is DateTime.MaxValue.
        /// </summary>
        internal DateTime SentTime = DateTime.MaxValue;

        /// <summary>
        /// seqID
        /// </summary>
        public readonly int SeqID;

        /// <summary>
        /// get request name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// get or set receive time out
        /// </summary>
        public readonly int MillisecondsReceiveTimeout;

        /// <summary>
        /// 异常回调
        /// </summary>
        private readonly Action<Exception> _onException = null;

        /// <summary>
        /// 结果回调
        /// </summary>
        private readonly Action<TMessage> _onResult = null;

        #endregion

        /// <summary>
        /// new
        /// </summary>
        /// <param name="seqID"></param>
        /// <param name="name"></param>
        /// <param name="payload"></param>
        /// <param name="millisecondsReceiveTimeout"></param>
        /// <param name="onException"></param>
        /// <param name="onResult"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Request(int seqID, string name, byte[] payload, int millisecondsReceiveTimeout,
            Action<Exception> onException, Action<TMessage> onResult)
            : base(payload)
        {
            if (onException == null) throw new ArgumentNullException("onException");
            if (onResult == null) throw new ArgumentNullException("onResult");

            this.SeqID = seqID;
            this.Name = name;
            this.MillisecondsReceiveTimeout = millisecondsReceiveTimeout;
            this._onException = onException;
            this._onResult = onResult;
        }

        public bool SetException(Exception ex)
        {
            this._onException(ex);
            return true;
        }

        public bool SetResult(TMessage message)
        {
            this._onResult(message);
            return true;
        }
    }
}