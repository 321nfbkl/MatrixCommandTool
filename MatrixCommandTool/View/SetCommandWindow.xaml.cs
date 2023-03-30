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
using System.Windows.Shapes;

namespace MatrixCommandTool.View
{
    /// <summary>
    /// SetCommandWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SetCommandWindow : Window
    {
        public SetCommandWindow()
        {
            InitializeComponent();

        }

        private void DragWindowMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region 对比度
        private void sliderContrast_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendContrastDragDelta(this.slider_ScaleContrast.Value);
        }

        private void slider_DragDeltaContrast(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendContrastDragDelta(this.slider_ScaleContrast.Value);
        }
        #endregion

        #region 饱和度
        private void saturationslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendSaturationDragDelta(this.saturationslider_Scale.Value);
        }

        private void saturationslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendSaturationDragDelta(this.saturationslider_Scale.Value);
        }
        #endregion

        #region 锐度
        private void sharpnessslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendSharpnessDragDelta(this.sharpnessslider_Scale.Value);
        }

        private void sharpnessslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendSharpnessDragDelta(this.sharpnessslider_Scale.Value);
        }
        #endregion

        #region 绿色调
        private void greenslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendGreenToneDragDelta(this.greenslider_Scale.Value);
        }

        private void greenslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendGreenToneDragDelta(this.greenslider_Scale.Value);
        }
        #endregion

        #region 蓝色调
        private void blueslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendDragDelta(this.blueslider_Scale.Value);
        }

        private void blueslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendDragDelta(this.blueslider_Scale.Value);
        }
        #endregion

        #region 音量调节
        private void volumeslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendVolumeDragDelta(this.volumeslider_Scale.Value);
        }

        private void volumeslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendVolumeDragDelta(this.volumeslider_Scale.Value);
        }
        #endregion

        #region 亮度调节
        private void brightnessslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendBrightnessDragDelta(this.brightnessslider_Scale.Value);
        }

        private void brightnessslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendBrightnessDragDelta(this.brightnessslider_Scale.Value);
        }
        #endregion

        #region 红色调
        private void redslider_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendRedToneDragDelta(this.redslider_Scale.Value);
        }

        private void redslider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            GlobalContext.Current.CurrentVMLocator.SetCommandVM.SendRedToneDragDelta(this.redslider_Scale.Value);
        }
        #endregion

        private void WindowMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
