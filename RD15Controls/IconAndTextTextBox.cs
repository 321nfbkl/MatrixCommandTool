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
    public class IconAndTextTextBox:TextBox
    {
        static IconAndTextTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconAndTextTextBox), new FrameworkPropertyMetadata(typeof(IconAndTextTextBox)));
        }
        public new string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static new readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IconAndTextTextBox), new PropertyMetadata(string.Empty,new PropertyChangedCallback(Textchange)));

        private static void Textchange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IconAndTextTextBox iconAndTextTextBox = d as IconAndTextTextBox;
            if (iconAndTextTextBox._TextBlock != null)
            {
                if (iconAndTextTextBox.Text != string.Empty)
                {

                    iconAndTextTextBox._TextBlock.Visibility = Visibility.Hidden;
                    iconAndTextTextBox._Image.Source = iconAndTextTextBox.TextExistIcon;
                }
                else
                {
                    iconAndTextTextBox._TextBlock.Visibility = Visibility.Visible;
                    iconAndTextTextBox._Image.Source = iconAndTextTextBox.TextEmptyIcon;
                }
            }
        }

        public ImageSource TextEmptyIcon
        {
            get { return (ImageSource)GetValue(TextEmptyIconProperty); }
            set { SetValue(TextEmptyIconProperty, value); }
        }

        public static readonly DependencyProperty TextEmptyIconProperty =
            DependencyProperty.Register("TextEmptyIcon", typeof(ImageSource), typeof(IconAndTextTextBox), new PropertyMetadata(null));

        public ImageSource TextExistIcon
        {
            get { return (ImageSource)GetValue(TextExistIconProperty); }
            set { SetValue(TextExistIconProperty, value); }
        }
        public static readonly DependencyProperty TextExistIconProperty =
           DependencyProperty.Register("TextExistIcon", typeof(ImageSource), typeof(IconAndTextTextBox), new PropertyMetadata(null));

        public ImageSource DeleteIcon
        {
            get { return (ImageSource)GetValue(DeleteIconProperty); }
            set { SetValue(DeleteIconProperty, value); }
        }
        public static readonly DependencyProperty DeleteIconProperty =
           DependencyProperty.Register("DeleteIcon", typeof(ImageSource), typeof(IconAndTextTextBox), new PropertyMetadata(null));
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
           DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(IconAndTextTextBox), new PropertyMetadata(new CornerRadius(0)));
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
           DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(IconAndTextTextBox), new PropertyMetadata(new Thickness(0)));
        public Brush WaterMarkColor
        {
            get { return (Brush)GetValue(WaterMarkColorProperty); }
            set { SetValue(WaterMarkColorProperty, value); }
        }

        public static readonly DependencyProperty WaterMarkColorProperty =
            DependencyProperty.Register("WaterMarkColor", typeof(Brush), typeof(IconAndTextTextBox), new PropertyMetadata());
        public string WaterMarkText
        {
            get { return (string)GetValue(WaterMarkTextProperty); }
            set { SetValue(WaterMarkTextProperty, value); }
        }

        public static readonly DependencyProperty WaterMarkTextProperty =
            DependencyProperty.Register("WaterMarkText", typeof(string), typeof(IconAndTextTextBox), new PropertyMetadata());
        private TextBox _TextBox;
        private Image _Image;
        private TextBlock _TextBlock;
        private ToggleButtonEx _ToggleButtonEx;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.GotFocus += TextBox_GotFocus;
            this.LostFocus += TextBox_LostFocus;
            _TextBox = Template.FindName("PART_EditableTextBox", this) as TextBox;
            _Image = Template.FindName("img", this) as Image;
            _TextBlock = Template.FindName("bgText", this) as TextBlock;
            _ToggleButtonEx = Template.FindName("DeleteTextBox", this) as ToggleButtonEx;
            _ToggleButtonEx.Click += _ToggleButtonEx_Click;
            _TextBox.TextChanged += _TextBox_TextChanged;
            this.TextChanged += _TextBox_TextChanged;
            TextBox_Visibility(false);
            _TextBox.IsKeyboardFocusedChanged += _IsKeyboardFocusedChanged;
        }

        private void _IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _ToggleButtonEx.Visibility = Visibility.Visible;
            _TextBox.SelectionStart = _TextBox.Text.Length;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_ToggleButtonEx != null)
                _ToggleButtonEx.Visibility = Visibility.Hidden;
            
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.Text != string.Empty)
                _ToggleButtonEx.Visibility = Visibility.Visible;
            else
                _ToggleButtonEx.Visibility = Visibility.Hidden;
        }
        private void _ToggleButtonEx_Click(object sender, RoutedEventArgs e)
        {
            if (_TextBox != null)
                _TextBox.Text = string.Empty;
        }
        public static T FindVisualParent<T>(DependencyObject obj) where T : class
        {
            while (obj != null)
            {
                System.Threading.Thread.Sleep(10);
                if (obj is T)
                    return obj as T;

                obj = VisualTreeHelper.GetParent(obj);
            }

            return null;
        }
        private void _TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = _TextBox.Text;
            TextBox_Visibility(true);
        }
        private void TextBox_Visibility(bool VisibilityBool)
        {
            if (string.IsNullOrEmpty(Text))
            {
                _TextBlock.Visibility = Visibility.Visible;
                _ToggleButtonEx.Visibility = Visibility.Hidden;
                _Image.Source = TextEmptyIcon;
            }
            else
            {
                _TextBlock.Visibility = Visibility.Hidden;
                if(VisibilityBool)
                    _ToggleButtonEx.Visibility = Visibility.Visible;
                _Image.Source = TextExistIcon;
            }
        }
    }
}

