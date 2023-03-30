using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RD15Controls
{
    public class PasswordBoxControl : TextBox
    {
        static PasswordBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBoxControl), new FrameworkPropertyMetadata(typeof(PasswordBoxControl)));
        }

        public static readonly DependencyProperty PlaceholderProperty =
              DependencyProperty.Register("Placeholder", typeof(string), typeof(PasswordBoxControl), new PropertyMetadata(""));

        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(PasswordBoxControl));

        public static readonly DependencyProperty PlaceholderBrushProperty =
            DependencyProperty.Register("PlaceholderBrush", typeof(Brush), typeof(PasswordBoxControl));

        public static readonly DependencyProperty IsPasswordBoxProperty =
            DependencyProperty.Register("IsPasswordBox", typeof(bool), typeof(PasswordBoxControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsPasswordBoxChnage)));

        public static readonly DependencyProperty PasswordCharProperty =
            DependencyProperty.Register("PasswordChar", typeof(char), typeof(PasswordBoxControl), new FrameworkPropertyMetadata('●'));

        public static readonly DependencyProperty PasswordStrProperty =
            DependencyProperty.Register("PasswordStr", typeof(string), typeof(PasswordBoxControl), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnPasswordStrChanged)));

        public static readonly DependencyProperty FocusedBorderBrushProperty =
            DependencyProperty.Register("FocusedBorderBrush", typeof(Brush), typeof(PasswordBoxControl));

        public static readonly DependencyProperty TextEmptyIconProperty =
            DependencyProperty.Register("TextEmptyIcon", typeof(ImageSource), typeof(PasswordBoxControl));

        public static readonly DependencyProperty TextExistIconProperty =
            DependencyProperty.Register("TextExistIcon", typeof(ImageSource), typeof(PasswordBoxControl));

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(PasswordBoxControl), new PropertyMetadata(48.0));

        public static readonly DependencyProperty ClearBtnMarginProperty =
            DependencyProperty.Register("ClearBtnMargin", typeof(Thickness), typeof(PasswordBoxControl), new PropertyMetadata(new Thickness(0, 0, 16, 0)));


        public ImageSource UnCheckEyeIcon
        {
            get { return (ImageSource)GetValue(UnCheckEyeIconProperty); }
            set { SetValue(UnCheckEyeIconProperty, value); }
        }

        public static readonly DependencyProperty UnCheckEyeIconProperty =
            DependencyProperty.Register("UnCheckEyeIcon", typeof(ImageSource), typeof(PasswordBoxControl));

        public ImageSource CheckEyeIcon
        {
            get { return (ImageSource)GetValue(CheckEyeIconProperty); }
            set { SetValue(CheckEyeIconProperty, value); }
        }

        public static readonly DependencyProperty CheckEyeIconProperty =
            DependencyProperty.Register("CheckEyeIcon", typeof(ImageSource), typeof(PasswordBoxControl));

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
        /// 是否为密码框
        /// </summary>
        public bool IsPasswordBox
        {
            get { return (bool)GetValue(IsPasswordBoxProperty); }
            set { SetValue(IsPasswordBoxProperty, value); }
        }

        /// <summary>
        /// 替换明文的密码字符
        /// </summary>
        public char PasswordChar
        {
            get { return (char)GetValue(PasswordCharProperty); }
            set { SetValue(PasswordCharProperty, value); }
        }

        /// <summary>
        /// 密码字符串
        /// </summary>
        public string PasswordStr
        {
            get
            {
                var value = GetValue(PasswordStrProperty);
                return value == null ? string.Empty : value.ToString();
            }
            set { SetValue(PasswordStrProperty, value); }
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
        /// 图标宽度
        /// </summary>
        public double IconWidth
        {
            get => (double)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }

        /// <summary>
        /// 清空按钮的边距
        /// </summary>
        public Thickness ClearBtnMargin
        {
            get => (Thickness)GetValue(ClearBtnMarginProperty);
            set => SetValue(ClearBtnMarginProperty, value);
        }

        private bool IsResponseChange;
        private StringBuilder PasswordBuilder;
        private ITCButton btnSee;
        private int lastOffset;
        public PasswordBoxControl()
        {
            IsResponseChange = true;
            PasswordBuilder = new StringBuilder();
            this.Loaded += BBBugTextBox_Loaded;
        }

        private void BBBugTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsPasswordBox && !string.IsNullOrEmpty(PasswordStr) && PasswordStr.Length > 0)
            {
                OnPasswordStrChanged();
            }
        }

        private static void OnPasswordStrChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            (d as PasswordBoxControl).OnPasswordStrChanged();
        }

        private void OnPasswordStrChanged()
        {
            if (!IsResponseChange)
                return;
            IsResponseChange = false;
            if (this.IsPasswordBox)
                this.Text = ConvertToPasswordChar(PasswordStr.Length);
            else
                this.Text = PasswordStr;
            IsResponseChange = true;
        }

        private static void OnIsPasswordBoxChnage(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as PasswordBoxControl).SetPwdEvent();
        }

        /// <summary>
        /// 定义TextChange事件
        /// </summary>
        private void SetPwdEvent()
        {
            if (IsPasswordBox)
                this.TextChanged += BBBugTextBox_TextChanged;
            else
                this.TextChanged -= BBBugTextBox_TextChanged;
        }

        private void BBBugTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsResponseChange)
                return;
            IsResponseChange = false;
            foreach (TextChange c in e.Changes)
            {
                PasswordStr = PasswordStr.Remove(c.Offset, c.RemovedLength);
                PasswordStr = PasswordStr.Insert(c.Offset, Text.Substring(c.Offset, c.AddedLength));
                lastOffset = c.Offset;
            }
            if (this.IsPasswordBox)
                /*将文本转换为密码字符*/
                this.Text = ConvertToPasswordChar(Text.Length);
            else
                this.Text = PasswordStr;
            IsResponseChange = true;
            this.SelectionStart = lastOffset + 1;
        }

        /// <summary>
        /// 按照指定的长度生成密码字符
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string ConvertToPasswordChar(int length)
        {
            if (PasswordBuilder != null)
                PasswordBuilder.Clear();
            else
                PasswordBuilder = new StringBuilder();
            for (var i = 0; i < length; i++)
                PasswordBuilder.Append(PasswordChar);
            return PasswordBuilder.ToString();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var btn = Template.FindName("btn_clear", this) as ITCButton;
            if (btn != null)
                btn.Click += Btn_Click;
            this.btnSee = Template.FindName("btn_eye", this) as ITCButton;
            if (btnSee != null)
            {
                btnSee.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(this.BtnSee_MouseLeftButtonDown), true);
                btnSee.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.BtnSee_MouseLeftButtonUp), true);
                btnSee.MouseDown += BtnSee_MouseLeftButtonDown;
                btnSee.MouseLeftButtonUp += BtnSee_MouseLeftButtonUp;
            }
        }

        private void BtnSee_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.IsPasswordBox = false;
            OnPasswordStrChanged();
        }

        private void BtnSee_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.IsPasswordBox = true;
            OnPasswordStrChanged();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            this.PasswordStr = string.Empty;
        }
    }
}
