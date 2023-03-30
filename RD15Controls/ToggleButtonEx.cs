using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RD15Controls
{
    public class ToggleButtonEx : ToggleButton
    {
        static ToggleButtonEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToggleButtonEx), new FrameworkPropertyMetadata(typeof(ToggleButtonEx)));
        }
        public string CheckContent
        {
            get { return (string)GetValue(CheckContentProperty); }
            set { SetValue(CheckContentProperty, value); }
        }
        public static readonly DependencyProperty CheckContentProperty =
            DependencyProperty.Register("CheckContent", typeof(string), typeof(ToggleButtonEx), new PropertyMetadata(string.Empty));
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
           DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(ToggleButtonEx), new PropertyMetadata(new Thickness(0)));
        
        public Stretch IconStretch
        {
            get { return (Stretch)GetValue(IconStretchProperty); }
            set { SetValue(IconStretchProperty, value); }
        }
        public static readonly DependencyProperty IconStretchProperty =
        DependencyProperty.Register("IconStretch", typeof(Stretch), typeof(ToggleButtonEx), new PropertyMetadata(Stretch.None));

        public ToggleButtonExType ToggleButtonExType
        {
            get { return (ToggleButtonExType)GetValue(ToggleButtonExTypeProperty); }
            set { SetValue(ToggleButtonExTypeProperty, value); }
        }

        public static readonly DependencyProperty ToggleButtonExTypeProperty =
            DependencyProperty.Register("ToggleButtonExType", typeof(ToggleButtonExType), typeof(ToggleButtonEx), new PropertyMetadata(ToggleButtonExType.Normal));

        public object CheckedToolTip
        {
            get { return (object)GetValue(CheckedToolTipProperty); }
            set { SetValue(CheckedToolTipProperty, value); }
        }

        public static readonly DependencyProperty CheckedToolTipProperty =
            DependencyProperty.Register("CheckedToolTip", typeof(object), typeof(ToggleButtonEx), new PropertyMetadata(null));
        public ImageSource CheckIcon
        {
            get { return (ImageSource)GetValue(CheckIconProperty); }
            set { SetValue(CheckIconProperty, value); }
        }

        public static readonly DependencyProperty CheckIconProperty =
            DependencyProperty.Register("CheckIcon", typeof(ImageSource), typeof(ToggleButtonEx), new PropertyMetadata(null));
        public ImageSource UnCheckIcon
        {
            get { return (ImageSource)GetValue(UnCheckIconProperty); }
            set { SetValue(UnCheckIconProperty, value); }
        }

        public static readonly DependencyProperty UnCheckIconProperty =
            DependencyProperty.Register("UnCheckIcon", typeof(ImageSource), typeof(ToggleButtonEx), new PropertyMetadata(null));

        public ImageSource UnCheckOverIcon
        {
            get { return (ImageSource)GetValue(UnCheckOverIconProperty); }
            set { SetValue(UnCheckOverIconProperty, value); }
        }

        public static readonly DependencyProperty UnCheckOverIconProperty =
            DependencyProperty.Register("UnCheckOverIcon", typeof(ImageSource), typeof(ToggleButtonEx), new PropertyMetadata(null));
        public ImageSource CheckOverIcon
        {
            get { return (ImageSource)GetValue(CheckOverIconProperty); }
            set { SetValue(CheckOverIconProperty, value); }
        }

        public static readonly DependencyProperty CheckOverIconProperty =
            DependencyProperty.Register("CheckOverIcon", typeof(ImageSource), typeof(ToggleButtonEx), new PropertyMetadata(null));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ToggleButtonEx), new PropertyMetadata(new CornerRadius(0)));


        public Brush UnCheckMouseOverForeground
        {
            get { return (Brush)GetValue(UnCheckMouseOverForegroundProperty); }
            set { SetValue(UnCheckMouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty UnCheckMouseOverForegroundProperty =
            DependencyProperty.Register("UnCheckMouseOverForeground", typeof(Brush), typeof(ToggleButtonEx), new PropertyMetadata());
        public Brush CheckMouseOverForeground
        {
            get { return (Brush)GetValue(CheckMouseOverForegroundProperty); }
            set { SetValue(CheckMouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty CheckMouseOverForegroundProperty =
            DependencyProperty.Register("CheckMouseOverForeground", typeof(Brush), typeof(ToggleButtonEx), new PropertyMetadata());


        public Brush CheckMousePressedForeground
        {
            get { return (Brush)GetValue(CheckMousePressedForegroundProperty); }
            set { SetValue(CheckMousePressedForegroundProperty, value); }
        }

        public static readonly DependencyProperty CheckMousePressedForegroundProperty =
            DependencyProperty.Register("CheckMousePressedForeground", typeof(Brush), typeof(ToggleButtonEx), new PropertyMetadata());
        public Brush UnCheckMousePressedForeground
        {
            get { return (Brush)GetValue(UnCheckMousePressedForegroundProperty); }
            set { SetValue(UnCheckMousePressedForegroundProperty, value); }
        }

        public static readonly DependencyProperty UnCheckMousePressedForegroundProperty =
            DependencyProperty.Register("UnCheckMousePressedForeground", typeof(Brush), typeof(ToggleButtonEx), new PropertyMetadata());


        public Brush MouseOverBorderbrush
        {
            get { return (Brush)GetValue(MouseOverBorderbrushProperty); }
            set { SetValue(MouseOverBorderbrushProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBorderbrushProperty =
            DependencyProperty.Register("MouseOverBorderbrush", typeof(Brush), typeof(ToggleButtonEx), new PropertyMetadata());


        public Brush CheckMouseOverBackground
        {
            get { return (Brush)GetValue(CheckMouseOverBackgroundProperty); }
            set { SetValue(CheckMouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CheckMouseOverBackgroundProperty =
            DependencyProperty.Register("CheckMouseOverBackground", typeof(Brush), typeof(ToggleButtonEx), new PropertyMetadata());
        public Brush UnCheckMouseOverBackground
        {
            get { return (Brush)GetValue(UnCheckMouseOverBackgroundProperty); }
            set { SetValue(UnCheckMouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty UnCheckMouseOverBackgroundProperty =
            DependencyProperty.Register("UnCheckMouseOverBackground", typeof(Brush), typeof(ToggleButtonEx), new PropertyMetadata());

        public Brush CheckMousePressedBackground
        {
            get { return (Brush)GetValue(CheckMousePressedBackgroundProperty); }
            set { SetValue(CheckMousePressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty CheckMousePressedBackgroundProperty =
            DependencyProperty.Register("CheckMousePressedBackground", typeof(Brush), typeof(ToggleButtonEx), new PropertyMetadata());

        public Brush UnCheckMousePressedBackground
        {
            get { return (Brush)GetValue(UnCheckMousePressedBackgroundProperty); }
            set { SetValue(UnCheckMousePressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty UnCheckMousePressedBackgroundProperty =
            DependencyProperty.Register("UnCheckMousePressedBackground", typeof(Brush), typeof(ToggleButtonEx), new PropertyMetadata());


        protected override void OnClick()
        {
            base.OnClick();
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
    }

    public enum ToggleButtonExType
    {
        Normal,
        Icon,
        Text,
        IconText,
        TextIcon
    }
}
