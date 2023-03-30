using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RD15Controls
{
    /// <summary>
    /// LogsConsole.xaml 的交互逻辑
    /// </summary>
    public partial class LogsConsole : Window
    {
        public LogsConsole()
        {
            InitializeComponent();
        }
        public bool IsClosed { get; set; }
        public bool IsShow { get; set; }
        public bool IsDefault { get; set; }
        public void Console(string msg, ConsoleType consoleType)
        {
            if (consoleType == ConsoleType.SignalList)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    if (this.SignalListMsgRtb.LineCount > 5000)
                    {
                        this.SignalListMsgRtb.Clear();
                    }
                    this.SignalListMsgRtb.AppendText(DateTime.Now.ToString() + "\n" + msg + "\n");
                });
            }
            else
            if (consoleType == ConsoleType.SignalPollList)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    if (this.SignalPollListMsgRtb.LineCount > 5000)
                    {
                        this.SignalPollListMsgRtb.Clear();
                    }
                    this.SignalPollListMsgRtb.AppendText(DateTime.Now.ToString() + "\n" + msg + "\n");
                });
            }
            else if (consoleType == ConsoleType.WindowsList)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    if (this.WindowsListMsgRtb.LineCount > 5000)
                    {
                        this.WindowsListMsgRtb.Clear();
                    }
                    this.WindowsListMsgRtb.AppendText(DateTime.Now.ToString() + "\n" + msg + "\n");
                });
            }
            else if (consoleType == ConsoleType.SeatsList)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    if (this.SeatsListMsgRtb.LineCount > 5000)
                    {
                        this.SeatsListMsgRtb.Clear();
                    }
                    this.SeatsListMsgRtb.AppendText(DateTime.Now.ToString() + "\n" + msg + "\n");
                });
            }
            else
            if (consoleType == ConsoleType.Trace)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    if (this.TraceMsgRtb.LineCount > 5000)
                    {
                        this.TraceMsgRtb.Clear();
                    }
                    this.TraceMsgRtb.AppendText(DateTime.Now.ToString() + "\n" + msg + "\n");
                });
            }
            else if (consoleType == ConsoleType.Debug)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    if (this.DebugMsgRtb.LineCount > 5000)
                    {
                        this.DebugMsgRtb.Clear();
                    }
                    this.DebugMsgRtb.AppendText(DateTime.Now.ToString() + "\n" + msg + "\n");
                });
            }
            else if (consoleType == ConsoleType.Info)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    if(this.InfoMsgRtb.LineCount > 5000)
                    {
                        this.InfoMsgRtb.Clear();
                    }
                    this.InfoMsgRtb.AppendText(DateTime.Now.ToString() + "\n" + msg + "\n");
                });
            }
            else
            if (consoleType == ConsoleType.Error)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    if (this.ErrorMsgRtb.LineCount > 5000)
                    {
                        this.ErrorMsgRtb.Clear();
                    }
                    this.ErrorMsgRtb.AppendText(DateTime.Now.ToString() + "\n" + msg + "\n");
                });
            }
            else if (consoleType == ConsoleType.Warn)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    if (this.WarnMsgRtb.LineCount > 5000)
                    {
                        this.WarnMsgRtb.Clear();
                    }
                    this.WarnMsgRtb.AppendText(DateTime.Now.ToString() + "\n" + msg + "\n");
                });
            }
            else if (consoleType == ConsoleType.Fatal)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    if (this.FatalMsgRtb.LineCount > 5000)
                    {
                        this.FatalMsgRtb.Clear();
                    }
                    this.FatalMsgRtb.AppendText(DateTime.Now.ToString() + "\n" + msg + "\n");
                });
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsShow)
                this.Hide();
            IsShow = false;
            if (!IsClosed)
                e.Cancel = true;
            IsClosed = false;
        }
        public void Close(bool isClosed)
        {
            IsClosed = isClosed;
            this.Close();
        }
        public enum ConsoleType
        {
            Trace,
            Debug,
            Info,
            Warn,
            Error,
            Fatal,
            SignalList,
            SignalPollList,
            WindowsList,
            SeatsList
        }

        private void TbC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.F5 == e.Key)
            {
                if (TbC.SelectedItem != null)
                {
                    if ((TbC.SelectedItem as TabItem).Header.ToString() == "信号源")
                    {
                        this.SignalListMsgRtb.Clear();
                    }
                    else if((TbC.SelectedItem as TabItem).Header.ToString() == "信号轮询")
                    {
                        this.SignalPollListMsgRtb.Clear();
                    }
                    else if ((TbC.SelectedItem as TabItem).Header.ToString() == "窗口信息")
                    {
                        this.WindowsListMsgRtb.Clear();
                    }
                    else if ((TbC.SelectedItem as TabItem).Header.ToString() == "坐席信息")
                    {
                        SeatsListMsgRtb.Clear();
                    }
                    else if ((TbC.SelectedItem as TabItem).Header.ToString() == "Trace")
                    {
                        this.TraceMsgRtb.Clear();
                    }
                    else if ((TbC.SelectedItem as TabItem).Header.ToString() == "Debug")
                    {
                        this.DebugMsgRtb.Clear();
                    }
                    else if ((TbC.SelectedItem as TabItem).Header.ToString() == "Info")
                    {
                        this.InfoMsgRtb.Clear();
                    }
                    else if ((TbC.SelectedItem as TabItem).Header.ToString() == "Warn")
                    {
                        this.WarnMsgRtb.Clear();
                    }
                    else if ((TbC.SelectedItem as TabItem).Header.ToString() == "Error")
                    {
                        this.ErrorMsgRtb.Clear();
                    }
                    else if ((TbC.SelectedItem as TabItem).Header.ToString() == "Fatal")
                    {
                        this.FatalMsgRtb.Clear();
                    }
                }
            }
        }
    }
}
