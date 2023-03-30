using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.TCP.Models
{
    /// <summary>
    /// TCP同步请求使用的线程同步类
    /// </summary>
    internal class RequestBag
    {
        public RequestBag()
        {
            this.RequestResetEvent = new ManualResetEvent(false);
        }

        public ManualResetEvent RequestResetEvent { get; set; }

        public string ResponseData { get; set; }
    }
}
