using System;

namespace FastSocket.Client.Messaging
{
    /// <summary>
    /// command line message 命令行协议
    /// </summary>
    public class CommandLineMessage : IMessage
    {
        public int SeqID { get; }

        /// <summary>
        /// get the current command name 
        /// </summary>
        public readonly string CmdName;

        /// <summary>
        /// params
        /// </summary>
        public readonly string[] Parameters;


        /// <summary>
        /// new
        /// </summary>
        public CommandLineMessage(int seqID,string cmdName,params  string[] parameters)
        {
            if (cmdName == null) throw new ArgumentNullException("cmdName");

            this.SeqID = seqID;
            this.CmdName = cmdName;
            this.Parameters = parameters;
        }
    }
}