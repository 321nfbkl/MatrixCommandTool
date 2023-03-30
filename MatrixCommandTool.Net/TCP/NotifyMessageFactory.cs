using MatrixCommandTool.Net.TCP.Models;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace MatrixCommandTool.Net.TCP
{
    internal sealed class NotifyMessageFactory
    {
        private NotifyMessageFactory() { }

        static NotifyMessageFactory()
        {
            SyncRequestList = new ConcurrentDictionary<string, RequestBag>();
            mMsgQueue = new ConcurrentQueue<string>();

            mMsgProcessThread = new Thread(MessageProcess);
            mMsgProcessThread.IsBackground = true;
            mMsgProcessThread.Start();
        }

        private static DelegateFactory _delegateFactory;
        public static void SetDeleteFactory(DelegateFactory factory) => _delegateFactory = factory;

        private static ILogger _logger;
        public static void SetLogger(ILogger logger) => _logger = logger;

        /// <summary>
        /// 同步请求列表
        /// </summary>
        static ConcurrentDictionary<string, RequestBag> SyncRequestList = null;

        /// <summary>
        /// 缓存消息队列
        /// </summary>
        static ConcurrentQueue<string> mMsgQueue = null;

        /// <summary>
        /// 消息处理的线程
        /// </summary>
        static Thread mMsgProcessThread = null;

        /// <summary>
        /// 请求消息入列
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bag"></param>
        public static bool AddRequestQueue(string key, RequestBag bag)
        {
            return SyncRequestList.TryAdd(key, bag);
        }

        /// <summary>
        /// 请求消息出列
        /// </summary>
        /// <param name="key"></param>
        /// <param name="bag"></param>
        /// <returns></returns>
        public static bool RemoveRequestQueue(string key, out RequestBag bag)
        {
            return SyncRequestList.TryRemove(key, out bag);
        }

        /// <summary>
        /// 判断请求消息是否存在队列
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContinueRequestQueue(string key)
        {
            return SyncRequestList.ContainsKey(key);
        }

        /// <summary>
        /// 添加消息到队列内
        /// </summary>
        public static void AddMessageInQueue(string msg)
        {
            try
            {
                mMsgQueue.Enqueue(msg);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"消息插入队列异常 AddMessageInQueue ${msg}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// 判断命令码是否为通知
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static bool IsNotify(string code)
        {
            return code.ToLower().StartsWith("ntf") || code.ToLower().StartsWith("notify");
        }

        /// <summary>
        /// 消息处理
        /// </summary>
        static void MessageProcess()
        {
            while (true)
            {
                try
                {
                    if (mMsgQueue.Count <= 0)
                    {
                        Thread.Sleep(300);
                        continue;
                    }

                    string msg = string.Empty;
                    if (!mMsgQueue.TryDequeue(out msg) || string.IsNullOrEmpty(msg))
                    {
                        continue;
                    }
                    var respObj = Newtonsoft.Json.Linq.JObject.Parse(msg);
                    string session = respObj["session"]?.ToString();
                    string actionCode = respObj["actioncode"].ToString();

                    //if (actionCode == "Heartbeat")
                    //    _logger.Debug($"ReceiveMessage : {msg}");
                    //else
                    //    _logger.Info($"ReceiveMessage : {msg}");

                    #region 处理同步请求响应
                    if (!string.IsNullOrEmpty(session) && SyncRequestList.ContainsKey(session) && IsNotify(actionCode))
                        continue;

                    if (!string.IsNullOrEmpty(session) && SyncRequestList.ContainsKey(session) &&
                            SyncRequestList.TryGetValue(session, out var reqBag) && reqBag != null)
                    {

                        reqBag.ResponseData = msg;
                        reqBag.RequestResetEvent.Set();
                        continue;
                    }
                   
                    _logger.Warn($"未处理的命令:{msg}");
                    #endregion

                }
                catch (Exception ex)
                {
                    //_logger.Error(ex);
                }
            }
        }    
    }
}
