using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RD15Controls
{
    public class IconTextBox : TextBox
    {
        public static readonly DependencyProperty PlaceholderProperty =
             DependencyProperty.Register("Placeholder", typeof(string), typeof(IconTextBox), new PropertyMetadata(""));

        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(IconTextBox));

        public static readonly DependencyProperty PlaceholderBrushProperty =
            DependencyProperty.Register("PlaceholderBrush", typeof(Brush), typeof(IconTextBox));

        public static readonly DependencyProperty TextEmptyIconProperty =
            DependencyProperty.Register("TextEmptyIcon", typeof(ImageSource), typeof(IconTextBox));

        public static readonly DependencyProperty TextExistIconProperty =
            DependencyProperty.Register("TextExistIcon", typeof(ImageSource), typeof(IconTextBox));

        public static readonly DependencyProperty IconWidthProperty =
          DependencyProperty.Register("IconWidth", typeof(double), typeof(IconTextBox), new PropertyMetadata(48.0));

        public static readonly DependencyProperty FocusedBorderBrushProperty =
           DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(IconTextBox));

        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(IconTextBox), new PropertyMetadata(new Thickness(0, 0, 0, 0)));

        public static readonly DependencyProperty ClearBtnMarginProperty =
            DependencyProperty.Register("ClearBtnMargin", typeof(Thickness), typeof(IconTextBox), new PropertyMetadata(new Thickness(0, 0, 0, 0)));

        public static readonly DependencyProperty ShowClearBtnProperty =
            DependencyProperty.Register("ShowClearBtn", typeof(Visibility), typeof(IconTextBox), new PropertyMetadata(Visibility.Visible));

        public string Placeholder
        {
            get => GetValue(PlaceholderProperty).ToString();
            set => SetValue(PlaceholderProperty, value);
        }

        public CornerRadius BorderCornerRadius
        {
            get => (CornerRadius)GetValue(BorderCornerRadiusProperty);
            set => SetValue(BorderCornerRadiusProperty, value);
        }

        public Brush PlaceholderBrush
        {
            get => (Brush)GetValue(PlaceholderBrushProperty);
            set => SetValue(PlaceholderBrushProperty, value);
        }

        /// <summary>
        /// 空文本下的图标
        /// </summary>
        public ImageSource TextEmptyIcon
        {
            get => (ImageSource)GetValue(TextEmptyIconProperty);
            set => SetValue(TextEmptyIconProperty, value);
        }

        /// <summary>
        /// 有文本的图标
        /// </summary>
        public ImageSource TextExistIcon
        {
            get => (ImageSource)GetValue(TextExistIconProperty);
            set => SetValue(TextExistIconProperty, value);
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
        /// 获取鼠标焦点后的边框颜色
        /// </summary>
        public Brush FocusedBorderBrush
        {
            get => (Brush)GetValue(FocusedBorderBrushProperty);
            set => SetValue(FocusedBorderBrushProperty, value);
        }

        /// <summary>
        /// 清空按钮的边距
        /// </summary>
        public Thickness ClearBtnMargin
        {
            get => (Thickness)GetValue(ClearBtnMarginProperty);
            set => SetValue(ClearBtnMarginProperty, value);
        }

        /// <summary>
        /// 图标宽度
        /// </summary>
        public double IconWidth
        {
            get => (double)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }


        public Visibility ShowClearBtn
        {
            get => (Visibility)GetValue(ShowClearBtnProperty);
            set => SetValue(ShowClearBtnProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var btn = Template.FindName("btn_clear", this) as ITCButton;
            if (btn != null)
                btn.Click += Btn_Click;
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Text = string.Empty;
        }
    }
}
