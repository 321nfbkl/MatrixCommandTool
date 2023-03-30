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
    public class TextAndEditRadioButton : RadioButton
    {
        static TextAndEditRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextAndEditRadioButton), new FrameworkPropertyMetadata(typeof(TextAndEditRadioButton)));
        }
        public ImageSource EditSelectedIcon
        {
            get { return (ImageSource)GetValue(EditSelectedIconProperty); }
            set { SetValue(EditSelectedIconProperty, value); }
        }

        public static readonly DependencyProperty EditSelectedIconProperty =
            DependencyProperty.Register("EditSelectedIcon", typeof(ImageSource), typeof(TextAndEditRadioButton), new PropertyMetadata(null));

        public ImageSource EditUnSelectedIcon
        {
            get { return (ImageSource)GetValue(EditUnSelectedIconProperty); }
            set { SetValue(EditUnSelectedIconProperty, value); }
        }
        public static readonly DependencyProperty EditUnSelectedIconProperty =
            DependencyProperty.Register("EditUnSelectedIcon", typeof(ImageSource), typeof(TextAndEditRadioButton), new PropertyMetadata(null));

        public ImageSource EditOverIcon
        {
            get { return (ImageSource)GetValue(EditOverIconProperty); }
            set { SetValue(EditOverIconProperty, value); }
        }
        public static readonly DependencyProperty EditOverIconProperty =
            DependencyProperty.Register("EditOverIcon", typeof(ImageSource), typeof(TextAndEditRadioButton), new PropertyMetadata(null));


        public Brush SelectedForeground
        {
            get { return (Brush)GetValue(SelectedForegroundProperty); }
            set { SetValue(SelectedForegroundProperty, value); }
        }
        public static readonly DependencyProperty SelectedForegroundProperty =
            DependencyProperty.Register("SelectedForeground", typeof(Brush), typeof(TextAndEditRadioButton), new PropertyMetadata());
        public Brush SelectedBackground
        {
            get { return (Brush)GetValue(SelectedBackgroundProperty); }
            set { SetValue(SelectedBackgroundProperty, value); }
        }
        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.Register("SelectedBackground", typeof(Brush), typeof(TextAndEditRadioButton), new PropertyMetadata());

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(TextAndEditRadioButton), new PropertyMetadata());

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextAndEditRadioButton), new PropertyMetadata(string.Empty, new PropertyChangedCallback(ContentChanged)));

        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(TextAndEditRadioButton), new PropertyMetadata(0,new PropertyChangedCallback(ContentChanged)));

        private static void ContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextAndEditRadioButton textAndEditRadioButton = d as TextAndEditRadioButton;
            if (textAndEditRadioButton._TextBlock != null)
            {
                textAndEditRadioButton._TextBlock.Text = $"{textAndEditRadioButton.Text}";
            }/*{textAndEditRadioButton.Id.ToString().PadLeft(2,'0')}*/
        }

        public string EditButtonTooltip
        {
            get { return (string)GetValue(EditButtonTooltipProperty); }
            set { SetValue(EditButtonTooltipProperty, value); }
        }
        public static readonly DependencyProperty EditButtonTooltipProperty =
            DependencyProperty.Register("EditButtonTooltip", typeof(string), typeof(TextAndEditRadioButton), new PropertyMetadata());

        public event RoutedEventHandler EditClick
        {
            add { AddHandler(EditClickEvent, value); }
            remove { RemoveHandler(EditClickEvent, value); }
        }
        public static readonly RoutedEvent EditClickEvent =
            EventManager.RegisterRoutedEvent("EditClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextAndEditRadioButton));

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
        TextBlock _TextBlock;
        ToggleButtonEx _ToggleButtonEx;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _TextBlock = Template.FindName("tb", this) as TextBlock;
            _ToggleButtonEx = Template.FindName("PART_Close_Button", this) as ToggleButtonEx;
            _TextBlock.Text = $"{Text}";
            _ToggleButtonEx.Click += ToggleButtonEx_Click;

        }

        private void ToggleButtonEx_Click(object sender, RoutedEventArgs e)
        {
            
            RoutedEventArgs args = new RoutedEventArgs(TextAndEditRadioButton.EditClickEvent, this);
            this.RaiseEvent(args);
            _ToggleButtonEx.IsChecked = false;
        }
    }
}
