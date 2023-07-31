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

        private void rbtn_yuan_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbtn_signal_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbtn_EDID_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbtn_switch_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbtn_color_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbtn_seetingl_Checked(object sender, RoutedEventArgs e)
        {

        }

        #region 对比度
        private void sliderContrast_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendContrastDragDelta(this.slider_ScaleContrast.Value);
        }

        private void slider_DragDeltaContrast(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendContrastDragDelta(this.slider_ScaleContrast.Value);
        }
        #endregion

        #region 饱和度
        private void saturationslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendSaturationDragDelta(this.saturationslider_Scale.Value);
        }

        private void saturationslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendSaturationDragDelta(this.saturationslider_Scale.Value);
        }
        #endregion

        #region 亮度调节
        private void brightnessslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendBrightnessDragDelta(this.brightnessslider_Scale.Value);
        }

        private void brightnessslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendBrightnessDragDelta(this.brightnessslider_Scale.Value);
        }
        #endregion

        private void DragWindowMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        #region 设置色调
        private void colorslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendColorDragDelta(this.colorslider_Scale.Value);
        }

        private void colorslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendColorDragDelta(this.colorslider_Scale.Value);
        }

        #endregion

        #region 设置R值
        private void rvalueslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendRVauleDragDelta(this.rvalueslider_Scale.Value);
        }

        private void rvalueslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendRVauleDragDelta(this.rvalueslider_Scale.Value);
        }
        #endregion

        #region 设置G值
        private void gslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendGVauleDragDelta(this.gvalueslider_Scale.Value);
        }

        private void gslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendGVauleDragDelta(this.gvalueslider_Scale.Value);
        }
        #endregion

        #region 设置B值
        private void bslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendBVauleDragDelta(this.bslider_Scale.Value);
        }

        private void bslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.NineOneMartixSettingVM.SendBVauleDragDelta(this.bslider_Scale.Value);
        }
        #endregion

    }
}
