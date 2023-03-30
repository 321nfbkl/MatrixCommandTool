using System;

namespace FastSocket.Client
{
    /// <summary>
    /// socket reqeust exception
    /// </summary>
    public sealed class RequestException : ApplicationException
    {
        /// <summary>
        /// error
        /// </summary>
        public readonly Errors Error;
        /// <summary>
        /// request name
        /// </summary>
        public readonly string RequestName;

        /// <summary>
        /// new
        /// </summary>
        /// <param name="error"></param>
        /// <param name="name"></param>
        public RequestException(Errors error, string name)
            : base(string.Concat("errorType:", error.ToString(), " name:", name ?? string.Empty))
        {
            this.Error = error;
            this.RequestName = name;
        }

        public enum Errors : byte
        {
            /// <summary>
            /// 
            /// </summary>
            Unknow = 0,
            /// <summary>
            /// 等待发送超时
            /// </summary>
            PendingSendTimeout = 1,
            /// <summary>
            /// 接收超时
            /// </summary>
            ReceiveTimeout = 2,
            /// <summary>
            /// 发送失败
            /// </summary>
            SendFaild = 3
        }
    }
}