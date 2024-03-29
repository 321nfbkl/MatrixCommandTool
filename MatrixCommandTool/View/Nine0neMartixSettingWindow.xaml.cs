﻿using Newtonsoft.Json.Linq;
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
using System.Windows.Shapes;

namespace MatrixCommandTool.View
{
    /// <summary>
    /// Nine0neMartixSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Nine0neMartixSettingWindow : Window
    {
        public Nine0neMartixSettingWindow()
        {
            InitializeComponent();
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
            //ImageBrush imageBrush = new ImageBrush();
            //imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/MatrixCommandTool;component/Resource/Image/3.bmp"));
            //this.nineonegrid.Background = imageBrush;
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
            //ImageBrush imageBrush = new ImageBrush();
            //imageBrush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/MatrixCommandTool;component/Resource/Image/4.bmp"));
            //this.nineonegrid.Background = imageBrush;
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

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void DragWindowMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void WindowMax_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
        }


        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            var combox = sender as ComboBox;
            if (combox != null)
            {
                if (combox.SelectedItem != null)
                {
                    if (combox.SelectedItem.ToString() == "模拟音频")
                    {
                        GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SetMoniAudioMode(int.Parse(combox.Tag.ToString()));
                    }
                    else if (combox.SelectedItem.ToString() == "嵌入音频")
                    {
                        GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SetQianruAudioMode(int.Parse(combox.Tag.ToString()));
                    }
                }
            }
        }

        private void ComboBoxBack_DropDownClosed(object sender, EventArgs e)
        {
            var combox = sender as ComboBox;
            if (combox != null)
            {
                if (combox.SelectedItem != null)
                {
                    if (combox.SelectedItem.ToString() == "设为蓝屏")
                    {
                        GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SetOutBgBlueScreen(int.Parse(combox.Tag.ToString()));
                    }
                    else if (combox.SelectedItem.ToString() == "设为黑屏")
                    {
                        GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SetOutBgBlackScreen(int.Parse(combox.Tag.ToString()));
                    }
                    else if (combox.SelectedItem.ToString() == "关闭背景颜色")
                    {
                        GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.CloseBackGround(int.Parse(combox.Tag.ToString()));
                    }
                }
            }
        }

        private void ComboBoxType_DropDownClosed(object sender, EventArgs e)
        {
            var combox = sender as ComboBox;
            if (combox != null)
            {
                if (combox.SelectedItem != null)
                {
                    if (combox.SelectedItem.ToString() == "HDMI")
                    {
                        GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SetHDMI(int.Parse(combox.Tag.ToString()));
                    }
                    else if (combox.SelectedItem.ToString() == "DVI")
                    {
                        GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SetDvi(int.Parse(combox.Tag.ToString()));
                    }
                }
            }
        }

        private void ComboBoxResolution_DropDownClosed(object sender, EventArgs e)
        {
            var combox = sender as ComboBox;
            if (combox != null)
            {
                if (combox.SelectedItem != null)
                {
                    GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendResolution(int.Parse(combox.Tag.ToString()), combox.SelectedItem.ToString());
                }
            }
        }
    }
}
