using MatrixCommandTool.Net.TCP.Models;
using MatrixCommandTool.Net.TCP.Response;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.TCP.Request
{
    public class ScanBoardListRequest:IRequestBase
    {
        private ClientSocket _client;

        public ScanBoardListRequest(ClientSocket client)
        {
            this._client = client;
        }

        /// <summary>
        /// 获取所有板卡信息
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public void GetAllBoardList(out string error)
        {
            try
            {
                error = String.Empty;
                string sendMsg = RequestActionCodes.GetAllBoardName;
                this._client.SendMessage(sendMsg);
            }
            catch (Exception ex)
            {
                error = "获取所有板卡信息数据异常";
            }
        }

        /// <summary>
        /// 激活[{channelId}]输入通道为预设置状态
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="error"></param>
        public void SetActivationIn(int channelId,out string error)
        {
            try
            {
                error = string.Empty;
                this._client.SendMessage("["+channelId+ "]SETIN.");
            }
            catch (Exception ex)
            {
                error = $"激活[{channelId}]输入通道为预设置状态异常";
            }
        }

        /// <summary>
        /// 激活[{channelId}]输出通道为预设置状态
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="error"></param>
        public void SetActivationOut(int channelId,out string error)
        {
            try
            {
                error = string.Empty;
                this._client.SendMessage("[" + channelId + "]SETOUT.");
            }
            catch (Exception ex)
            {
                error = $"激活[{channelId}]输出通道为预设置状态异常";
            }
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="code"></param>
        /// <param name="error"></param>
        public void SendIndtruction(string code,out string error)
        {
            try
            {
                error = string.Empty;
                this._client.SendMessage(code);
            }
            catch (Exception ex)
            {
                error = $"发送指令{code}异常";
            }
        }


    }
}
