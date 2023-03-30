using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.TCP.Models
{
    /// <summary>
    /// 自定义协议数据类
    /// </summary>
    public class ProtocolModel
    {
        public ProtocolModel()
        {

        }

        public ProtocolModel(byte bt, bool isDtlen)
        {
            this.Byte = bt;
            this.IsDtLen = isDtlen;
        }
        public byte Byte { get; set; }
        public bool IsDtLen { get; set; }
    }

    public class CustomProtocolInfo
    {
        public CustomProtocolInfo()
        {
            this.HeadList = new List<ProtocolModel>();
            this.TailList = new List<ProtocolModel>();
        }
        public CustomProtocolInfo(int dtlencount, bool enableparotocol, IList<ProtocolModel> head, IList<ProtocolModel> tail)
        {
            if (head == null || tail == null)
                throw new ArgumentNullException("head or tail is null");
            this.DtLenCount = dtlencount;
            this.EnableProtocol = enableparotocol;
            this.HeadList = head;
            this.TailList = tail;
        }

        /// <summary>
        /// 数据长度所占字节数
        /// </summary>
        public int DtLenCount { get; set; }

        /// <summary>
        /// 数据头长度
        /// </summary>
        [JsonIgnore]
        public int HeadLen
        {
            get => this.HeadList.Any(a => a.IsDtLen) ? this.HeadList.Count + this.DtLenCount - 1 : this.HeadList.Count;
        }

        /// <summary>
        /// 数据尾长度
        /// </summary>
        [JsonIgnore]
        public int TailLen
        {
            get => this.TailList.Any(a => a.IsDtLen) ? this.TailList.Count + this.DtLenCount - 1 : this.TailList.Count;
        }

        /// <summary>
        /// 协议头和协议尾加起来的长度
        /// </summary>
        [JsonIgnore]
        public int ProtocolLen
        {
            get
            {
                int result = 0;
                foreach (var item in HeadList)
                {
                    if (item.IsDtLen)
                        result += DtLenCount;
                    else
                        result += 1;
                }
                foreach (var item in TailList)
                {
                    if (item.IsDtLen)
                        result += DtLenCount;
                    else
                        result += 1;
                }
                return result;
            }
        }

        /// <summary>
        /// 是否启用协议
        /// </summary>
        [JsonIgnore]
        public bool EnableProtocol { get; set; }
        public IList<ProtocolModel> HeadList { get; private set; }
        public IList<ProtocolModel> TailList { get; private set; }

    }
}
