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
    public class WaterMarkTextBox : TextBox
    {
        static WaterMarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WaterMarkTextBox), new FrameworkPropertyMetadata(typeof(WaterMarkTextBox)));
        }

        public ImageSource DeleteIcon
        {
            get { return (ImageSource)GetValue(DeleteIconProperty); }
            set { SetValue(DeleteIconProperty, value); }
        }
        public static readonly DependencyProperty DeleteIconProperty =
           DependencyProperty.Register("DeleteIcon", typeof(ImageSource), typeof(WaterMarkTextBox), new PropertyMetadata(null));
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
           DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(WaterMarkTextBox), new PropertyMetadata(new CornerRadius(0)));
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
           DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(WaterMarkTextBox), new PropertyMetadata(new Thickness(0)));
        public Brush WaterMarkColor
        {
            get { return (Brush)GetValue(WaterMarkColorProperty); }
            set { SetValue(WaterMarkColorProperty, value); }
        }

        public static readonly DependencyProperty WaterMarkColorProperty =
            DependencyProperty.Register("WaterMarkColor", typeof(Brush), typeof(WaterMarkTextBox), new PropertyMetadata());
        public string WaterMarkText
        {
            get { return (string)GetValue(WaterMarkTextProperty); }
            set { SetValue(WaterMarkTextProperty, value); }
        }

        public static readonly DependencyProperty WaterMarkTextProperty =
            DependencyProperty.Register("WaterMarkText", typeof(string), typeof(WaterMarkTextBox), new PropertyMetadata());

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(WaterMarkTextBox), new PropertyMetadata());

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(WaterMarkTextBox), new PropertyMetadata());

        public bool IsCanDelete
        {
            get { return (bool)GetValue(IsCanDeleteProperty); }
            set { SetValue(IsCanDeleteProperty, value); }
        }

        public static readonly DependencyProperty IsCanDeleteProperty =
            DependencyProperty.Register("IsCanDelete", typeof(bool), typeof(WaterMarkTextBox), new PropertyMetadata());

        public WaterMarkTextBox()
        {
            this.Loaded += WaterMarkTextBox_Loaded;
            this.Unloaded += WaterMarkTextBox_Unloaded;
        }

        private void WaterMarkTextBox_Unloaded(object sender, RoutedEventArgs e)
        {
            this.FocusableChanged -= WaterMarkTextBox_FocusableChanged;
            this.TextChanged -= _TextBox_TextChanged;
        }

        private void WaterMarkTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.FocusableChanged += WaterMarkTextBox_FocusableChanged;
            this.TextChanged += _TextBox_TextChanged;
        }

        private TextBlock _TextBlock;
        private ToggleButtonEx _ToggleButtonEx;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _TextBlock = Template.FindName("bgText", this) as TextBlock;
            _ToggleButtonEx = Template.FindName("DeleteTextBox", this) as ToggleButtonEx;
            _ToggleButtonEx.Click += _ToggleButtonEx_Click;
            this.TextChanged += _TextBox_TextChanged;
        }

        private void WaterMarkTextBox_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.SelectionStart = this.Text.Length;
            this.Focus();
        }

        private void WaterMarkTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.SelectionStart = this.Text.Length;
        }
        private void _ToggleButtonEx_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
        }
        private void _TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.SelectionStart = this.Text.Length;
            //SelectionStart = Text.Length;
            Focus();
        }
    }
}
