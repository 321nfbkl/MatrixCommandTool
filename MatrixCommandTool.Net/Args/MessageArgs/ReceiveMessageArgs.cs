using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.Args.MessageArgs
{
    public class ReceiveMessageArgs : EventArgs
    {
        public string Msg { get; private set; }
        public ReceiveMessageArgs(string msg)
        {
            Msg = msg;
        }
    }
}
