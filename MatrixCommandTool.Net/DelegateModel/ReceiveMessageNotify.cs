using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.DelegateModel
{
    public class ReceiveMessageNotify:NotifyBaseModel
    {
        public ReceiveMessageNotify(string message)
        {
            Message = message;
        }
        public string Message { get; set; }
    }
}
