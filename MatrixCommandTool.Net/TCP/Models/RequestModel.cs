using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.TCP.Models
{
    /// <summary>
    /// 消息请求类
    /// </summary>
    public class RequestModel
    {
        public RequestModel()
        {
           
        }

        [Newtonsoft.Json.JsonProperty("actioncode")]
        public string ActionCode { get; set; }
    }
}
