using CommonServiceLocator;
using FastSocket.Client;
using MatrixCommandTool.Net.TCP;
using MatrixCommandTool.ViewModel;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool
{
    public class GlobalContext
    {
        private GlobalContext() { }
        private static object _lockObj = new object();
        private static GlobalContext _current;
        public static GlobalContext Current
        {
            get
            {
                if (_current == null)
                {
                    lock (_lockObj)
                    {
                        if (_current == null)
                            _current = new GlobalContext();
                    }
                }

                return _current;
            }
        }

        #region 有些数据需要用DI获取

        private IServiceProvider _serviceProvicer;
        public void SetServiceProvider(IServiceProvider provicer)
        {
            this._serviceProvicer = provicer;
        }

        /// <summary>
        /// 配置类
        /// </summary>
        public Helper.JsonConfigurationHelper Config { get; set; }

        /// <summary>
        /// 全局日志管理
        /// </summary>
        public Logger Logger
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Logger>();
            }
        }

        /// <summary>
        /// 全局唯一的TCP连接客户端
        /// </summary>
        public ClientSocket SocketClient
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ClientSocket>();
            }
        }

        /// <summary>
        /// 所有主机下发通知的统一入口
        /// </summary>
        public DelegateFactory NotifyFactory
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DelegateFactory>();
            }
        }
        #endregion

        public ViewModel.ViewModelLocator CurrentVMLocator { get; private set; }

        public void SetVMLocator(ViewModel.ViewModelLocator locator)
        {
            this.CurrentVMLocator = locator;
        }
    }
}
