using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace RD15Controls
{
    public class ErrorTipsControls : TextBox
    {
        /// <summary>
        /// 计数
        /// </summary>
        private int _tickCount;
        private StackPanel _panel;
        /// <summary>
        /// 关闭计时器
        /// </summary>
        private DispatcherTimer _timerClose;

        public static readonly DependencyProperty TimeTrickerProperty;

        public static readonly DependencyProperty TiipsIconProperty =
          DependencyProperty.Register("TiipsIcon", typeof(ImageSource), typeof(ErrorTipsControls));

        public static readonly DependencyProperty IconWidthProperty =
          DependencyProperty.Register("IconWidth", typeof(double), typeof(ErrorTipsControls), new PropertyMetadata(48.0));

        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(ErrorTipsControls), new PropertyMetadata(new Thickness(0, 0, 0, 0)));

        public static readonly DependencyProperty TipsTextProperty =
           DependencyProperty.Register("TipsText", typeof(string), typeof(ErrorTipsControls), new PropertyMetadata(""));

        public static readonly DependencyProperty TipsTextBrushProperty =
          DependencyProperty.Register("TipsTextBrush", typeof(Brush), typeof(ErrorTipsControls));

        public int TimeTricker
        {
            get => (int)GetValue(TimeTrickerProperty);
            set => SetValue(TimeTrickerProperty, value);
        }

        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource TiipsIcon
        {
            get => (ImageSource)GetValue(TiipsIconProperty);
            set => SetValue(TiipsIconProperty, value);
        }



        /// <summary>
        /// 图标边距
        /// </summary>
        public Thickness IconMargin
        {
            get => (Thickness)GetValue(IconMarginProperty);
            set => SetValue(IconMarginProperty, value);
        }


        /// <summary>
        /// 图标宽度
        /// </summary>
        public double IconWidth
        {
            get => (double)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }

        public string TipsText
        {
            get => GetValue(TipsTextProperty).ToString();
            set => SetValue(TipsTextProperty, value);
        }

        public Brush TipsTextBrush
        {
            get => (Brush)GetValue(TipsTextBrushProperty);
            set => SetValue(TipsTextBrushProperty, value);
        }

        static ErrorTipsControls()
        {
            TimeTrickerProperty = DependencyProperty.Register("TimeTricker", typeof(int), typeof(ErrorTipsControls), new PropertyMetadata(-1, (d, e) =>
            {
                var control = d as ErrorTipsControls;
                if ((int)e.NewValue > 0)
                {
                    control.StartTimer();
                }
                else
                    control.Close();
            }));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _panel = GetTemplateChild("panel") as StackPanel;
            _panel.Visibility = Visibility.Visible;
            //StartTimer();
        }

        private void StartTimer()
        {
            if (_timerClose == null)
            {
                _timerClose = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1)
                };
                _timerClose.Tick += delegate
                {
                    _tickCount++;
                    if (_tickCount >= TimeTricker) Close();
                };
            }
            _timerClose.Stop();
            _timerClose.Start();
        }

        private void Close()
        {
            _timerClose?.Stop();
            //_panel.Visibility = Visibility.Collapsed;
            this._tickCount = 0;
        }
    }
}
