using System;

namespace FastSocket.Client.Protocol
{
    /// <summary>
    /// bad protocol exception
    /// </summary>
    public sealed class BadProtocolException : ApplicationException
    {
        public BadProtocolException() 
            : base("bad protocol.")
        {
            
        }

        public BadProtocolException(string message) 
            :base(message)
        {
        }
    }
}