using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace RD15Controls
{
    public class IconAndTextComboBox : ComboBox
    {
        static IconAndTextComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconAndTextComboBox), new FrameworkPropertyMetadata(typeof(IconAndTextComboBox)));
        }
        public ImageSource TextEmptyIcon
        {
            get { return (ImageSource)GetValue(TextEmptyIconProperty); }
            set { SetValue(TextEmptyIconProperty, value); }
        }

        public static readonly DependencyProperty TextEmptyIconProperty =
            DependencyProperty.Register("TextEmptyIcon", typeof(ImageSource), typeof(IconAndTextComboBox), new PropertyMetadata(null));

        public ImageSource TextExistIcon
        {
            get { return (ImageSource)GetValue(TextExistIconProperty); }
            set { SetValue(TextExistIconProperty, value); }
        }
        public static readonly DependencyProperty TextExistIconProperty =
           DependencyProperty.Register("TextExistIcon", typeof(ImageSource), typeof(IconAndTextComboBox), new PropertyMetadata(null));

        public ImageSource DeleteIcon
        {
            get { return (ImageSource)GetValue(DeleteIconProperty); }
            set { SetValue(DeleteIconProperty, value); }
        }
        public static readonly DependencyProperty DeleteIconProperty =
           DependencyProperty.Register("DeleteIcon", typeof(ImageSource), typeof(IconAndTextComboBox), new PropertyMetadata(null));

        public ImageSource ComboBoxItemDeleteIcon
        {
            get { return (ImageSource)GetValue(ComboBoxItemDeleteIconProperty); }
            set { SetValue(ComboBoxItemDeleteIconProperty, value); }
        }

        public static readonly DependencyProperty ComboBoxItemDeleteIconProperty =
            DependencyProperty.Register("ComboBoxItemDeleteIcon", typeof(ImageSource), typeof(IconAndTextComboBox), new PropertyMetadata(null));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(IconAndTextComboBox), new PropertyMetadata(null));

        public Brush CloseBrush
        {
            get { return (Brush)GetValue(CloseBrushProperty); }
            set { SetValue(CloseBrushProperty, value); }
        }

        public static readonly DependencyProperty CloseBrushProperty =
            DependencyProperty.Register("CloseBrush", typeof(Brush), typeof(IconAndTextComboBox), new PropertyMetadata());

        public Brush WaterMarkColor
        {
            get { return (Brush)GetValue(WaterMarkColorProperty); }
            set { SetValue(WaterMarkColorProperty, value); }
        }

        public static readonly DependencyProperty WaterMarkColorProperty =
            DependencyProperty.Register("WaterMarkColor", typeof(Brush), typeof(IconAndTextComboBox), new PropertyMetadata());
        public string WaterMarkText
        {
            get { return (string)GetValue(WaterMarkTextProperty); }
            set { SetValue(WaterMarkTextProperty, value); }
        }

        public static readonly DependencyProperty WaterMarkTextProperty =
            DependencyProperty.Register("WaterMarkText", typeof(string), typeof(IconAndTextComboBox), new PropertyMetadata());
        public string CloseItemText
        {
            get { return (string)GetValue(CloseItemTextProperty); }
            set { SetValue(CloseItemTextProperty, value); }
        }

        public static readonly DependencyProperty CloseItemTextProperty =
            DependencyProperty.Register("CloseItemText", typeof(string), typeof(IconAndTextComboBox), new PropertyMetadata());

        /// <summary>
        /// 关闭item事件
        /// </summary>
        public event RoutedEventHandler CloseItem
        {
            add { AddHandler(CloseItemEvent, value); }
            remove { RemoveHandler(CloseItemEvent, value); }
        }
        public static readonly RoutedEvent CloseItemEvent =
            EventManager.RegisterRoutedEvent("CloseItem", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IconAndTextComboBox));


        /// <summary>
        /// TextChanged事件
        /// </summary>
        public event RoutedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }
        public static readonly RoutedEvent TextChangedEvent =
            EventManager.RegisterRoutedEvent("TextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IconAndTextComboBox));

        private TextBox _TextBox;
        private Image _Image;
        private TextBlock _TextBlock;
        private ToggleButtonEx _ToggleButtonEx;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.SelectionChanged += Text_SelectionChanged;
            this.PreviewMouseDown += Combox_MouseLeftButtonUp;
            this.GotFocus += Combox_GotFocus;
            this.LostFocus += Combox_LostFocus;
            _TextBox = Template.FindName("PART_EditableTextBox", this) as TextBox;
            _Image = Template.FindName("img", this) as Image;
            _TextBlock = Template.FindName("bgText", this) as TextBlock;
            _ToggleButtonEx = Template.FindName("DeleteTextBox", this) as ToggleButtonEx;
            _ToggleButtonEx.Click += _ToggleButtonEx_Click;
            _TextBox.TextChanged += _TextBox_TextChanged;

            if (_TextBox != null)
                Text_SelectionChanged(null, null);
            AddHandler(ToggleButtonEx.ClickEvent, new RoutedEventHandler(ButtonClicked));
        }

        private void Combox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_ToggleButtonEx != null)
                _ToggleButtonEx.Visibility = Visibility.Visible;
        }

        private void _ToggleButtonEx_Click(object sender, RoutedEventArgs e)
        {
            if (_TextBox != null)
                _TextBox.Text = string.Empty;
        }

        private void Combox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (_ToggleButtonEx != null)
                _ToggleButtonEx.Visibility = Visibility.Hidden;
        }

        private void Combox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.Text != string.Empty)
                _ToggleButtonEx.Visibility = Visibility.Visible;
            else
                _ToggleButtonEx.Visibility = Visibility.Hidden;
        }


        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            ToggleButtonEx toggleButtonEx = e.OriginalSource as ToggleButtonEx;
            if (toggleButtonEx != null)
            {
                if (!string.IsNullOrEmpty(toggleButtonEx.Name) && toggleButtonEx.Name == "Delete")
                {
                    ComboBoxItem comboBoxItem = FindVisualParent<ComboBoxItem>(toggleButtonEx);
                    RoutedEventArgs args = new RoutedEventArgs(IconAndTextComboBox.CloseItemEvent, comboBoxItem);
                    CloseItemText = comboBoxItem.Content.ToString();
                    this.RaiseEvent(args);
                }
            }
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


        private void Text_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (string.IsNullOrEmpty(this.Text))
            {
                _TextBlock.Visibility = Visibility.Visible;
                _ToggleButtonEx.Visibility = Visibility.Hidden;
                _Image.Source = TextEmptyIcon;
            }
            else
            {
                _TextBlock.Visibility = Visibility.Hidden;
                if (IsMouseCaptured)
                    _ToggleButtonEx.Visibility = Visibility.Visible;
                _Image.Source = TextExistIcon;
            }
        }

        private void _TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (string.IsNullOrEmpty(_TextBox.Text))
            {
                _TextBlock.Visibility = Visibility.Visible;
                _ToggleButtonEx.Visibility = Visibility.Hidden;
                _Image.Source = TextEmptyIcon;
            }
            else
            {
                _TextBlock.Visibility = Visibility.Hidden;
                if (IsMouseCaptured)
                    _ToggleButtonEx.Visibility = Visibility.Visible;
                _Image.Source = TextExistIcon;
                if (_TextBox.Text.Contains(" "))
                {
                    _TextBox.Text = _TextBox.Text.ToString().Replace(" ", ".");
                    _TextBox.SelectionStart = _TextBox.Text.ToString().Length;
                }
            }

            RoutedEventArgs args = new RoutedEventArgs(IconAndTextComboBox.TextChangedEvent, sender as TextBox);
            this.RaiseEvent(args);
        }
    }

    public class IconAndTextComboBoxItem:ComboBoxItem
    {
        static IconAndTextComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconAndTextComboBoxItem), new FrameworkPropertyMetadata(typeof(IconAndTextComboBoxItem)));
        }
        public ImageSource DeleteIcon
        {
            get { return (ImageSource)GetValue(DeleteIconProperty); }
            set { SetValue(DeleteIconProperty, value); }
        }
        public static readonly DependencyProperty DeleteIconProperty =
           DependencyProperty.Register("DeleteIcon", typeof(ImageSource), typeof(IconAndTextComboBoxItem), new PropertyMetadata(null));
    }
}
