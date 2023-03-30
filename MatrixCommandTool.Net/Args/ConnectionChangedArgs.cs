using FastSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.Args
{
    public class ConnectionChangedArgs : EventArgs
    {
        public IConnection Connection { get; private set; }
        public int SocketID { get; private set; }
        public string EndPoint { get; private set; }
        public ConnectionChangedArgs(int sourceid, IConnection connection = null, string endPoint = "")
        {
            this.SocketID = sourceid;
            this.Connection = connection;
            this.EndPoint = endPoint;
        }
    }
}
