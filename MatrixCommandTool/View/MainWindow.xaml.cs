using MaterialDesignThemes.Wpf;
using MatrixCommandTool.View;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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

namespace MatrixCommandTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //串口实例
        SerialPort serialPort = new SerialPort();
        public bool IsOpen = false;
        public MainWindow()
        {
            InitializeComponent();
            App.MainWindow = this;

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<object>(this, "ShowSetCommandView", o =>
            {
                App.RunInUIThread(() =>
                {
                    SetCommandWindow set = new SetCommandWindow();
                    set.Show();
                    this.Close();
                });
            });
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ip_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void SerialPort_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DragWindowMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void WinMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 夏日初恋
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAero_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resource = (ResourceDictionary)Application.LoadComponent(new Uri("/Resource/Generic.xaml", UriKind.Relative));
            Application.Current.Resources.MergedDictionaries[0] = resource;
        }

        /// <summary>
        /// 梦醒时分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuRoyale_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary resource = (ResourceDictionary)Application.LoadComponent(new Uri("/Resource/Default/RedSkin.xaml", UriKind.Relative));
            Application.Current.Resources.MergedDictionaries[0] = resource;
        }

        /// <summary>
        /// 换肤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeskin_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)e.OriginalSource;
            button.ContextMenu.IsOpen = true;
        }
    }
}
