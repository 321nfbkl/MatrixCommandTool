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
    public class DescriptionTextBox : TextBox
    {

        static DescriptionTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DescriptionTextBox), new FrameworkPropertyMetadata(typeof(DescriptionTextBox)));
        }
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
           DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(DescriptionTextBox), new PropertyMetadata(new CornerRadius(0)));

        public string DescriptionMark
        {
            get { return (string)GetValue(DescriptionMarkProperty); }
            set { SetValue(DescriptionMarkProperty, value); }
        }
        public static readonly DependencyProperty DescriptionMarkProperty =
           DependencyProperty.Register("DescriptionMark", typeof(string), typeof(DescriptionTextBox));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        public static readonly DependencyProperty DescriptionProperty =
           DependencyProperty.Register("Description", typeof(string), typeof(DescriptionTextBox));

        public double DescriptionFontSize
        {
            get { return (double)GetValue(DescriptionFontSizeProperty); }
            set { SetValue(DescriptionFontSizeProperty, value); }
        }
        public static readonly DependencyProperty DescriptionFontSizeProperty =
           DependencyProperty.Register("DescriptionFontSize", typeof(double), typeof(DescriptionTextBox));
        public double DescriptionWidth
        {
            get { return (double)GetValue(DescriptionWidthProperty); }
            set { SetValue(DescriptionWidthProperty, value); }
        }
        public static readonly DependencyProperty DescriptionWidthProperty =
           DependencyProperty.Register("DescriptionWidth", typeof(double), typeof(DescriptionTextBox));

        public Brush DescriptionForeground
        {
            get { return (Brush)GetValue(DescriptionForegroundProperty); }
            set { SetValue(DescriptionForegroundProperty, value); }
        }

        public static readonly DependencyProperty DescriptionForegroundProperty =
            DependencyProperty.Register("DescriptionForeground", typeof(Brush), typeof(DescriptionTextBox), new PropertyMetadata());

        public double TextWidth
        {
            get { return (double)GetValue(TextWidthProperty); }
            set { SetValue(TextWidthProperty, value); }
        }
        public static readonly DependencyProperty TextWidthProperty =
           DependencyProperty.Register("TextWidth", typeof(double), typeof(DescriptionTextBox));

        public ImageSource DeleteIcon
        {
            get { return (ImageSource)GetValue(DeleteIconProperty); }
            set { SetValue(DeleteIconProperty, value); }
        }
        public static readonly DependencyProperty DeleteIconProperty =
           DependencyProperty.Register("DeleteIcon", typeof(ImageSource), typeof(DescriptionTextBox), new PropertyMetadata(null));

        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(DescriptionTextBox), new PropertyMetadata());

        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(DescriptionTextBox), new PropertyMetadata());

        public bool IsCanDelete
        {
            get { return (bool)GetValue(IsCanDeleteProperty); }
            set { SetValue(IsCanDeleteProperty, value); }
        }

        public static readonly DependencyProperty IsCanDeleteProperty =
            DependencyProperty.Register("IsCanDelete", typeof(bool), typeof(DescriptionTextBox), new PropertyMetadata());
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
           DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(DescriptionTextBox), new PropertyMetadata(new Thickness(0)));
        public Brush WaterMarkColor
        {
            get { return (Brush)GetValue(WaterMarkColorProperty); }
            set { SetValue(WaterMarkColorProperty, value); }
        }

        public static readonly DependencyProperty WaterMarkColorProperty =
            DependencyProperty.Register("WaterMarkColor", typeof(Brush), typeof(DescriptionTextBox), new PropertyMetadata());
        public string WaterMarkText
        {
            get { return (string)GetValue(WaterMarkTextProperty); }
            set { SetValue(WaterMarkTextProperty, value); }
        }

        public static readonly DependencyProperty WaterMarkTextProperty =
            DependencyProperty.Register("WaterMarkText", typeof(string), typeof(DescriptionTextBox), new PropertyMetadata());



        private ToggleButtonEx _ToggleButtonEx;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _ToggleButtonEx = Template.FindName("DeleteTextBox", this) as ToggleButtonEx;
            _ToggleButtonEx.Click += _ToggleButtonEx_Click;
        }

        private void _ToggleButtonEx_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
        }
    }
}
