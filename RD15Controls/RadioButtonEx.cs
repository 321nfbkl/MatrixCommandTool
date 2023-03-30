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
    public class RadioButtonEx : RadioButton
    {
        static RadioButtonEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioButtonEx), new FrameworkPropertyMetadata(typeof(RadioButtonEx)));
        }

        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
           DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(RadioButtonEx), new PropertyMetadata(new Thickness(0)));
        public Thickness TextMargin
        {
            get { return (Thickness)GetValue(TextMarginProperty); }
            set { SetValue(TextMarginProperty, value); }
        }
        public static readonly DependencyProperty TextMarginProperty =
           DependencyProperty.Register("TextMargin", typeof(Thickness), typeof(RadioButtonEx), new PropertyMetadata(new Thickness(0)));

        public RadioButtonExType RadioButtonExType
        {
            get { return (RadioButtonExType)GetValue(RadioButtonExTypeProperty); }
            set { SetValue(RadioButtonExTypeProperty, value); }
        }

        public static readonly DependencyProperty RadioButtonExTypeProperty =
            DependencyProperty.Register("RadioButtonExType", typeof(RadioButtonExType), typeof(RadioButtonEx), new PropertyMetadata(RadioButtonExType.Normal));


        public ImageSource CheckIcon
        {
            get { return (ImageSource)GetValue(CheckIconProperty); }
            set { SetValue(CheckIconProperty, value); }
        }

        public static readonly DependencyProperty CheckIconProperty =
            DependencyProperty.Register("CheckIcon", typeof(ImageSource), typeof(RadioButtonEx), new PropertyMetadata(null));
        public ImageSource UnCheckIcon
        {
            get { return (ImageSource)GetValue(UnCheckIconProperty); }
            set { SetValue(UnCheckIconProperty, value); }
        }

        public static readonly DependencyProperty UnCheckIconProperty =
            DependencyProperty.Register("UnCheckIcon", typeof(ImageSource), typeof(RadioButtonEx), new PropertyMetadata(null));

        public ImageSource UnCheckOverIcon
        {
            get { return (ImageSource)GetValue(UnCheckOverIconProperty); }
            set { SetValue(UnCheckOverIconProperty, value); }
        }

        public static readonly DependencyProperty UnCheckOverIconProperty =
            DependencyProperty.Register("UnCheckOverIcon", typeof(ImageSource), typeof(RadioButtonEx), new PropertyMetadata(null));
        public ImageSource CheckOverIcon
        {
            get { return (ImageSource)GetValue(CheckOverIconProperty); }
            set { SetValue(CheckOverIconProperty, value); }
        }

        public static readonly DependencyProperty CheckOverIconProperty =
            DependencyProperty.Register("CheckOverIcon", typeof(ImageSource), typeof(RadioButtonEx), new PropertyMetadata(null));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(RadioButtonEx), new PropertyMetadata(new CornerRadius(0)));


        public Brush UnCheckMouseOverForeground
        {
            get { return (Brush)GetValue(UnCheckMouseOverForegroundProperty); }
            set { SetValue(UnCheckMouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty UnCheckMouseOverForegroundProperty =
            DependencyProperty.Register("UnCheckMouseOverForeground", typeof(Brush), typeof(RadioButtonEx), new PropertyMetadata(Brushes.Black));
        public Brush CheckMouseOverForeground
        {
            get { return (Brush)GetValue(CheckMouseOverForegroundProperty); }
            set { SetValue(CheckMouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty CheckMouseOverForegroundProperty =
            DependencyProperty.Register("CheckMouseOverForeground", typeof(Brush), typeof(RadioButtonEx), new PropertyMetadata(Brushes.Black));


        public Brush CheckMousePressedForeground
        {
            get { return (Brush)GetValue(CheckMousePressedForegroundProperty); }
            set { SetValue(CheckMousePressedForegroundProperty, value); }
        }

        public static readonly DependencyProperty CheckMousePressedForegroundProperty =
            DependencyProperty.Register("CheckMousePressedForeground", typeof(Brush), typeof(RadioButtonEx), new PropertyMetadata(Brushes.Black));
        public Brush UnCheckMousePressedForeground
        {
            get { return (Brush)GetValue(UnCheckMousePressedForegroundProperty); }
            set { SetValue(UnCheckMousePressedForegroundProperty, value); }
        }

        public static readonly DependencyProperty UnCheckMousePressedForegroundProperty =
            DependencyProperty.Register("UnCheckMousePressedForeground", typeof(Brush), typeof(RadioButtonEx), new PropertyMetadata(Brushes.Black));


        public Brush MouseOverBorderbrush
        {
            get { return (Brush)GetValue(MouseOverBorderbrushProperty); }
            set { SetValue(MouseOverBorderbrushProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBorderbrushProperty =
            DependencyProperty.Register("MouseOverBorderbrush", typeof(Brush), typeof(RadioButtonEx), new PropertyMetadata());


        public Brush CheckMouseOverBackground
        {
            get { return (Brush)GetValue(CheckMouseOverBackgroundProperty); }
            set { SetValue(CheckMouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CheckMouseOverBackgroundProperty =
            DependencyProperty.Register("CheckMouseOverBackground", typeof(Brush), typeof(RadioButtonEx), new PropertyMetadata());
        public Brush UnCheckMouseOverBackground
        {
            get { return (Brush)GetValue(UnCheckMouseOverBackgroundProperty); }
            set { SetValue(UnCheckMouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty UnCheckMouseOverBackgroundProperty =
            DependencyProperty.Register("UnCheckMouseOverBackground", typeof(Brush), typeof(RadioButtonEx), new PropertyMetadata());

        public Brush CheckMousePressedBackground
        {
            get { return (Brush)GetValue(CheckMousePressedBackgroundProperty); }
            set { SetValue(CheckMousePressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CheckMousePressedBackgroundProperty =
            DependencyProperty.Register("CheckMousePressedBackground", typeof(Brush), typeof(RadioButtonEx), new PropertyMetadata());

        public double CheckOpacity 
        { 
            get { return (double)GetValue(CheckOpacityProperty); }
            set { SetValue(CheckOpacityProperty, value); }
        }
        public static readonly DependencyProperty CheckOpacityProperty =
          DependencyProperty.Register("CheckOpacity", typeof(double), typeof(RadioButtonEx), new PropertyMetadata(100.0));

        public double UnCheckOpacity
        {
            get { return (double)GetValue(UnCheckOpacityProperty); }
            set { SetValue(UnCheckOpacityProperty, value); }
        }
        public static readonly DependencyProperty UnCheckOpacityProperty =
          DependencyProperty.Register("UnCheckOpacity", typeof(double), typeof(RadioButtonEx), new PropertyMetadata(100.0));

        public static T FindVisualParent<T>(DependencyObject obj) where T : class
        {
            while (obj != null)
            {
                if (obj is T)
                    return obj as T;

                obj = VisualTreeHelper.GetParent(obj);
            }

            return null;
        }
    }

    public enum RadioButtonExType
    {
        Custom,
        Normal,
        Icon,
        Text,
        IconText,
        TextIcon
    }
}
