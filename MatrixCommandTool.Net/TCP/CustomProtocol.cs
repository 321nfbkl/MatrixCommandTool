using MatrixCommandTool.Net.TCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.TCP
{
    public class CustomProtocol
    {
        public CustomProtocolInfo ProtocolInfo { get; set; }

        public CustomProtocol()
        {

        }

        public byte[] GetSendData(byte[] sourcebt)
        {
            try
            {
                if (!this.ProtocolInfo.EnableProtocol)
                    return sourcebt;
                List<byte> result = new List<byte>();
                if (this.ProtocolInfo.HeadList.Count > 0)
                {
                    foreach (var item in this.ProtocolInfo.HeadList)
                    {
                        if (item.IsDtLen)
                        {
                            ///截取指定字节长度
                            var dtLenByte = BitConverter.GetBytes(sourcebt.Length);
                            for (int i = 0; i < this.ProtocolInfo.DtLenCount; i++)
                            {
                                result.Add(dtLenByte[i]);
                            }
                        }
                        else
                            result.Add(item.Byte);
                    }
                }

                result.AddRange(sourcebt);

                if (this.ProtocolInfo.TailList.Count > 0)
                {
                    foreach (var item in this.ProtocolInfo.TailList)
                    {
                        if (item.IsDtLen)
                        {
                            var dtLenByte = BitConverter.GetBytes(sourcebt.Length);
                            for (int i = 0; i < this.ProtocolInfo.DtLenCount; i++)
                            {
                                result.Add(dtLenByte[i]);
                            }
                        }
                        else
                            result.Add(item.Byte);
                    }
                }

                return result.ToArray();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 接收的数据转换协议
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="readlength"></param>
        /// <returns></returns>
        public byte[] Parse(ArraySegment<byte> buffer, out int readlength)
        {
            int dtLen = 0;
            int headLen = this.ProtocolInfo.HeadLen;
            int tailLen = this.ProtocolInfo.TailLen;
            int protocolLen = this.ProtocolInfo.ProtocolLen;
            if (!this.ProtocolInfo.EnableProtocol)
            {
                headLen = 0;
                dtLen = buffer.Count;
                tailLen = 0;
            }
            else
            {
                if (buffer.Count < protocolLen)
                {
                    readlength = 0;
                    return null;
                }

                if (this.ProtocolInfo.HeadList.Count > 0)
                {
                    var headBt = new byte[headLen];
                    Array.Copy(buffer.Array, 0, headBt, 0, headLen);
                    for (int i = 0; i < this.ProtocolInfo.HeadList.Count;)
                    {
                        if (this.ProtocolInfo.HeadList[i].IsDtLen)
                        {
                            //数据长度
                            byte[] bLength = new byte[4];
                            for (int headindex = 0; headindex < this.ProtocolInfo.DtLenCount; headindex++)
                            {
                                bLength[headindex] = headBt[i + headindex];
                            }
                            dtLen = BitConverter.ToInt32(bLength, 0);
                            i++;
                            continue;
                        }

                        if (headBt[i] != this.ProtocolInfo.HeadList[i].Byte)
                        {
                            readlength = buffer.Count;
                            return Encoding.UTF8.GetBytes($"Error : 接收数据异常，协议头错误，协议位置：{i}！");
                        }
                        i++;
                    }
                }

                if (this.ProtocolInfo.TailList.Count > 0)
                {
                    var tailBt = new byte[tailLen];
                    Array.Copy(buffer.Array, buffer.Count - tailBt.Length, tailBt, 0, tailLen);
                    for (int i = 0; i < this.ProtocolInfo.TailList.Count;)
                    {
                        if (this.ProtocolInfo.TailList[i].IsDtLen)
                        {
                            //数据长度
                            byte[] bLength = new byte[4];
                            for (int tailindex = 0; tailindex < this.ProtocolInfo.DtLenCount; tailindex++)
                            {
                                bLength[tailindex] = tailBt[i + tailindex];
                            }
                            dtLen = BitConverter.ToInt32(bLength, 0);
                            continue;
                        }

                        if (tailBt[i] != this.ProtocolInfo.TailList[i].Byte)
                        {
                            readlength = buffer.Count;
                            return Encoding.UTF8.GetBytes($"Error : 接收数据异常，协议尾错误，协议位置：{i}！");
                        }

                        i++;
                    }
                }

                if (dtLen == 0)
                    dtLen = buffer.Count - headLen - tailLen;
            }

            var data = new byte[dtLen];
            //Array.Copy(buffer.Array, headLen, data, 0, dtLen);
            Buffer.BlockCopy(buffer.Array, headLen, data, 0, dtLen);
            readlength = headLen + dtLen + tailLen;
            return data;
        }
    }
}
