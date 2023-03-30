using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.DelegateModel
{
    public class TCPConnectionChangedNotify : NotifyBaseModel
    {
        public TCPConnectionChangedNotify(bool status)
        {
            this.Status = status;
        }

        public bool Status { get; private set; }
    }
}
