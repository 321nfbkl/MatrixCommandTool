
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixCommandTool.Net.TCP.Models;
using NLog;
using System.Net;
using System.Runtime.Remoting.Messaging;
using FastSocket.Client;
using MatrixCommandTool.Net.TCP.Client;
using MatrixCommandTool.Net.Args.MessageArgs;
using MatrixCommandTool.Net.Args;
using FastSocket.Client.Protocol;
using FastSocket.SocketBase;
using System.Threading;
using System.Timers;
using NLog.Time;
using Microsoft.SqlServer.Server;

namespace MatrixCommandTool.Net.TCP
{
    public class ClientSocket : SocketClient<ClientMessage>
    {
        private ILogger _logger;

        /// <summary>
        /// 连接状态
        /// </summary>
        public bool Status { get; private set; }
        public string ConnectedIP { get; private set; }
        public int ConnectedPort { get; private set; }
        /// <summary>
        /// socket唯一键值
        /// </summary>
        private readonly string mSocketUniqueID = Guid.NewGuid().ToString().Replace("-", string.Empty);
        private ClientProtocol mProtocol;

        /// <summary>
        /// 连接超时
        /// </summary>
        private System.Threading.ManualResetEvent mConnectionTimeoutEvent;



        private DelegateFactory _notifyFactory;

        /// <summary>
        /// 接收消息事件
        /// </summary>
        public event EventHandler<ReceiveMessageArgs> OnReceiveMessagesEvent;

        public DelegateFactory NotifyFactory => this._notifyFactory;

        public ClientSocket(DelegateFactory factory)
            : this(new CustomProtocol(), 1024, 1500)
        {
            this._notifyFactory = factory;
        }

        public ClientSocket(DelegateFactory factory, CustomProtocol protocol, int bufferSize, int timeout)
       : this(new ClientProtocol() { Protocols = protocol }, bufferSize, bufferSize, timeout, timeout)
        {
            this._notifyFactory = factory;
        }

        public ClientSocket(CustomProtocol protocol, int bufferSize, int timeout)
      : this(new ClientProtocol() { Protocols = protocol }, bufferSize, bufferSize, timeout, timeout)
        {

        }

        public ClientSocket(ClientProtocol protocol,
             int socketBufferSize,
             int messageBufferSize,
             int millisecondsSendTimeout,
             int millisecondsReceiveTimeout) :
             base(protocol,
                 socketBufferSize,
                 messageBufferSize,
                 millisecondsSendTimeout,
                 millisecondsReceiveTimeout)
        {
            this.mProtocol = protocol;
            this._recvTimer = new System.Threading.Timer(this.MessageRecvFinished, null, Timeout.Infinite, Timeout.Infinite);

        }


        /// <summary>
        /// 连接主机
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void Connection(string ip, int port)
        {
            if (this.Status)
                this.DisConnection();

            this.mConnectionTimeoutEvent = new System.Threading.ManualResetEvent(false);
            Action<string, int> func = (i, p) =>
            {
                if (this.TryRegisterEndPoint(mSocketUniqueID, new[] { new IPEndPoint(IPAddress.Parse(ip), port) }))
                {
                    this.ConnectedIP = ip;
                    this.ConnectedPort = port;
                }
                if (!this.mConnectionTimeoutEvent.WaitOne(5000))
                {
                    this.DisConnection();
                    this._notifyFactory.InvokeConnectionChanged(false);
                }
            };
            func.BeginInvoke(ip, port, new AsyncCallback(r =>
            {
                AsyncResult _result = (AsyncResult)r;
                Action<string, int> showMessage = (Action<string, int>)_result.AsyncDelegate;
                showMessage.EndInvoke(_result);
            }), null);


        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnection()
        {
            if (this.UnRegisterEndPoint(mSocketUniqueID))
            {
            }
        }

        /// <summary>
        /// 异步发送数据
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            if (!this.Status)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"连接已断开，消息发送失败 ： {message}");
                Console.ResetColor();
                this._notifyFactory.InvokeConnectionChanged(false);
                return;
            }
            var messageBt = Encoding.UTF8.GetBytes(message);
            Console.WriteLine(message);
            this.Send(new Packet(messageBt));

            //this._logger.Info($"Request  {message}");
        }

        /// <summary>
        /// 同步发送数据(此方法必须要用线程调用，否则会出现卡顿)
        /// </summary>
        /// <param name="message"></param>
        //public ResponseModel<RespModel> SendMessageAsync<RespModel>(
        //    RequestModel sendData,
        //    int timeOut = 5000)
        //    where RespModel : NetBaseModel
        //{
        //    string message = Newtonsoft.Json.JsonConvert.SerializeObject(sendData);
        //    try
        //    {
        //        if (!this.Status)
        //        {
        //            //this._logger.Error($"连接已断开，消息发送失败 ： {message}");
        //            return new ResponseModel<RespModel>() { Result = 500, ReturnMessage = "服务器未连接！" };
        //        }
        //        return new ResponseModel<RespModel>() { Result = -1, ReturnMessage = "消息发送失败！" };
        //    }
        //    catch (Exception ex)
        //    {
        //        //this._logger.Error($"消息发送失败： Response Type : {typeof(RespModel)} Request Message : {message}");
        //        //this._logger.Error(ex);
        //        return new ResponseModel<RespModel>() { Result = -1, ReturnMessage = $"消息发送失败:{ex.Message}" };
        //    }
        //}


        /// <summary>
        /// 连接成功
        /// </summary>
        /// <param name="connection"></param>
        protected override void OnConnected(IConnection connection)
        {
            base.OnConnected(connection);
            this.Status = true;
            this.mConnectionTimeoutEvent?.Set();
            this._notifyFactory.InvokeConnectionChanged(true);
        }

        /// <summary>
        /// 连接异常
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ex"></param>
        protected override void OnConnectionError(IConnection connection, Exception ex)
        {
            base.OnConnectionError(connection, ex);
            this.Status = false;
        }

        /// <summary>
        /// 连接断开
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ex"></param>
        protected override void OnDisconnected(IConnection connection, Exception ex)
        {
            base.OnDisconnected(connection, ex);
            this.Status = false;
            this.mConnectionTimeoutEvent?.Set();
            this._notifyFactory.InvokeConnectionChanged(false);
        }

        /// <summary>
        /// 接收消息定时器
        /// </summary>
        private System.Threading.Timer _recvTimer;

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="e"></param>
        protected override void OnMessageReceived(IConnection connection, MessageReceivedEventArgs e)
        {

            //设置计时器的间隔时间，单位为毫秒
            //timer.Interval = 10000;

            if (!this.Status)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"连接已断开，接收的消息作废！");
                Console.ResetColor();
                return;
            }
            int readlength;
            ClientMessage message = null;
            try
            {
                message = this.mProtocol.Parse(connection, e.Buffer, out readlength);
                //Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                base.OnConnectionError(connection, ex);
                connection.BeginDisconnect(ex);
                e.SetReadlength(e.Buffer.Count);
                return;
            }

            this._recvTimer.Change(500, Timeout.Infinite);

            if (message != null)
            {
                string msg = Encoding.ASCII.GetString(message.Payload);
                this._recvMessageCache += msg;
            }
            e.SetReadlength(readlength);
        }

        private string _recvMessageCache = string.Empty;

        private void MessageRecvFinished(object _)
        {
            if (string.IsNullOrEmpty(this._recvMessageCache))
                return;

            this._notifyFactory.InvokeMessageChanged(this._recvMessageCache);
            //_logger.Info(msg);
            NotifyMessageFactory.AddMessageInQueue(this._recvMessageCache);
            //timer.Start();

            this._recvMessageCache = string.Empty;
            this._recvTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

    }
}
