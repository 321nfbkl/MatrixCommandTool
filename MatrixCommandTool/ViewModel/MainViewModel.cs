using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatrixCommandTool.Model.BindingModel;
using MatrixCommandTool.Net;
using MatrixCommandTool.Net.TCP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MatrixCommandTool.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ClientSocket _socket;

        #region properity

        private bool mIsCheckSerialPort;
        /// <summary>
        /// 是否选中串口连接
        /// </summary>
        public bool IsCheckSerialPort
        {
            get => this.mIsCheckSerialPort;
            set => Set(ref this.mIsCheckSerialPort, value);
        }

        private bool mIsCheckIp;
        /// <summary>
        /// 是否选中网络连接
        /// </summary>
        public bool IsCheckIp
        {
            get => this.mIsCheckIp;
            set => Set(ref this.mIsCheckIp, value);
        }

        private string mConnIP;
        /// <summary>
        /// ip
        /// </summary>
        public string ConnIP
        {
            get => this.mConnIP;
            set => Set(ref this.mConnIP, value);
        }

        private int mConnPort = 5000;
        /// <summary>
        /// 端口
        /// </summary>
        public int ConnPort
        {
            get => this.mConnPort;
            set => Set(ref this.mConnPort, value);
        }
        private int mBoundRate;
        public int BoundRate
        {
            get => this.mBoundRate;
            set => Set(ref this.mBoundRate, value);
        }

        private bool mIsOpen = false;
        /// <summary>
        /// 端口
        /// </summary>
        public bool IsOpen
        {
            get => this.mIsOpen;
            set => Set(ref this.mIsOpen, value);
        }

        private SerialPort mSerial=new SerialPort();
        public SerialPort Serial
        {
            get => this.mSerial;
            set => Set(ref this.mSerial, value);
        }

        private string mSelectedSerialPortName;
        public string SelectedSerialPortName
        {
            get => this.mSelectedSerialPortName;
            set => Set(ref this.mSelectedSerialPortName, value);
        }

        public IList<string> SerialPortList { get; set; } = new ObservableCollection<string>();
        #endregion

        #region Command
        public ICommand LoginByNetCommand { get; set; }

        public ICommand ScanSerialPortCommand { get; set; }

        public ICommand OpenSerialPortCommand { get; set; }

        #endregion
        public MainViewModel(ClientSocket clientSocket)
        {
            this._socket = clientSocket;
            this._socket.NotifyFactory.ConnectionChangedEvent += NotifyFactory_ConnectionChangedEvent;
            this.LoginByNetCommand = new RelayCommand(LoginByNet);
            this.ScanSerialPortCommand = new RelayCommand(ScanSerialPort);
            this.OpenSerialPortCommand=new RelayCommand(OpenSerialPort);
        }


        private void OpenSerialPort()
        {
            if (IsOpen == false)
            {
                try
                {
                    Serial.PortName=this.SelectedSerialPortName.ToString();
                    Serial.BaudRate = this.BoundRate;
                    Serial.Open();
                    IsOpen = true;
                    if (IsOpen)
                        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<object>(null, "ShowSetCommandView");
                    //this.Serial.DataReceived += (s, e) =>
                    //{
                    //    Console.WriteLine("接收到数据！");
                    //};
                }
                catch (Exception)
                {
                    IsOpen = false;
                    MessageBox.Show("串口无效或被占用", "错误提示");
                }
            }
        }


        /// <summary>
        /// 识别串口
        /// </summary>
        private void ScanSerialPort()
        {

            string[] port_names = SerialPort.GetPortNames();
            string last_name = "";

            this.SerialPortList.Clear();
            if (port_names == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                return;
            }
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {
                //获取有多少个COM口就添加进COMBOX项目列表  
                this.SerialPortList.Add(s);
                last_name = s;//保存最新的一个
            }
        


            //bool IsComExit = false;
            //this.SerialPortList.Clear();
            //for(int i = 0; i < 10; i++)
            //{
            //    try
            //    {
            //        SerialPort port = new SerialPort("Com" + (i + 1).ToString());
            //        port.Open();
            //        port.Close();
            //        this.SerialPortList.Add("Com" + (i + 1).ToString());
            //        IsComExit = true;
            //    }
            //    catch (Exception)
            //    {
            //        continue;
            //    }
            //}
            //if (IsComExit==false)
            //{
            //    MessageBox.Show("没有找到可用的串口", "错误提示");
            //}
        }

        /// <summary>
        /// 连接改变事件
        /// </summary>
        /// <param name="notify"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void NotifyFactory_ConnectionChangedEvent(Net.DelegateModel.TCPConnectionChangedNotify notify)
        {
            if (notify.Status == true)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<object>(null, "ShowSetCommandView");
                this._socket.NotifyFactory.ConnectionChangedEvent -= NotifyFactory_ConnectionChangedEvent;
            }
            else
                Console.WriteLine("连接失败!");
        }

        /// <summary>
        /// 网络连接登录
        /// </summary>
        private void LoginByNet()
        {
            this._socket.Connection(ConnIP, ConnPort);
        }
    }
}