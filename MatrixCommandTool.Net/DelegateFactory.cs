using MatrixCommandTool.Net.DelegateModel;
using System;

namespace MatrixCommandTool.Net.TCP
{
    public class DelegateFactory
    {
        /// <summary>
        /// 接收消息的回调方法
        /// </summary>
        /// <param name="param"></param>
        public delegate void NotifyMessageDelegate();
        public delegate void NotifyMessageDelegate<T>(T notify) where T : NotifyBaseModel;

        #region 主机连接状态改变回调方法
        /// <summary>
        /// 连接改变事件
        /// </summary>
        public event NotifyMessageDelegate<TCPConnectionChangedNotify> ConnectionChangedEvent;
        /// <summary>
        /// 提供对外调用方法
        /// </summary>
        /// <param name="isConnected"></param>
        internal void InvokeConnectionChanged(bool isConnected)
        {
            ConnectionChangedEvent?.Invoke(new TCPConnectionChangedNotify(isConnected));
        }


        public event NotifyMessageDelegate<ReceiveMessageNotify> ReceiveMessageChangedEvent;
        internal void InvokeMessageChanged(string msg)
        {
            ReceiveMessageChangedEvent?.Invoke(new ReceiveMessageNotify(msg));
        }
        #endregion
    }
}