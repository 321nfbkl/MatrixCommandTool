using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace MatrixCommandTool.Controls
{
    public class TitleSwitchButtonControl : ToggleButton
    {
        static TitleSwitchButtonControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleSwitchButtonControl), new FrameworkPropertyMetadata(typeof(TitleSwitchButtonControl)));
        }
        public static readonly DependencyProperty TextVisibilityProperty = DependencyProperty.Register("TextVisibility",
            typeof(Visibility), typeof(TitleSwitchButtonControl), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty UnCheckedTextProperty = DependencyProperty.Register("UnCheckedText",
            typeof(string), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty ImageOverProperty = DependencyProperty.Register("ImageOver",
        typeof(Image), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty ImagePressProperty = DependencyProperty.Register("ImagePress",
            typeof(Image), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty CheckedTextProperty = DependencyProperty.Register("CheckedText",
            typeof(string), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty UnCheckedColorProperty = DependencyProperty.Register("UnCheckedColor",
            typeof(Brush), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty CheckedColorProperty = DependencyProperty.Register("CheckedColor",
            typeof(Brush), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty EllipseWidthProperty = DependencyProperty.Register("EllipseWidth",
            typeof(double), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty EllipseHeigthProperty = DependencyProperty.Register("EllipseHeigth",
            typeof(double), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty CircleHeigthProperty = DependencyProperty.Register("CircleHeigth",
            typeof(double), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty RectangleMarginProperty = DependencyProperty.Register("RectangleMargin",
            typeof(Thickness), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty CircleColorProperty = DependencyProperty.Register("CircleColor",
            typeof(Brush), typeof(TitleSwitchButtonControl));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation",
            typeof(Orientation), typeof(TitleSwitchButtonControl), new FrameworkPropertyMetadata(Orientation.Vertical));

        public static readonly DependencyProperty TextMarginProperty = DependencyProperty.Register("TextMargin",
            typeof(Thickness), typeof(TitleSwitchButtonControl));



        public Visibility TextVisibility
        {
            get
            {
                return (Visibility)base.GetValue(TextVisibilityProperty);
            }
            set
            {
                base.SetValue(TextVisibilityProperty, value);
            }
        }
        public string UnCheckedText
        {
            get
            {
                return (string)base.GetValue(UnCheckedTextProperty);
            }
            set
            {
                base.SetValue(UnCheckedTextProperty, value);
            }
        }

        public string CheckedText
        {
            get
            {
                return (string)base.GetValue(CheckedTextProperty);
            }
            set
            {
                base.SetValue(CheckedTextProperty, value);
            }
        }

        public Brush UnCheckedColor
        {
            get
            {
                return (Brush)base.GetValue(UnCheckedColorProperty);
            }
            set
            {
                base.SetValue(UnCheckedColorProperty, value);
            }
        }

        public Brush CheckedColor
        {
            get
            {
                return (Brush)base.GetValue(CheckedColorProperty);
            }
            set
            {
                base.SetValue(CheckedColorProperty, value);
            }
        }

        public double EllipseWidth
        {
            get
            {
                return (double)base.GetValue(EllipseWidthProperty);
            }
            set
            {
                base.SetValue(EllipseWidthProperty, value);
            }
        }

        public double EllipseHeigth
        {
            get
            {
                return (double)base.GetValue(EllipseHeigthProperty);
            }
            set
            {
                base.SetValue(EllipseHeigthProperty, value);
            }
        }

        public double CircleHeigth
        {
            get
            {
                return (double)base.GetValue(CircleHeigthProperty);
            }
            set
            {
                base.SetValue(CircleHeigthProperty, value);
            }
        }

        public Thickness RectangleMargin
        {
            get
            {
                base.SetValue(RectangleMarginProperty, new Thickness(base.Height / 2.0, 0.0, base.Height / 2.0, 0.0));
                return (Thickness)base.GetValue(RectangleMarginProperty);
            }
        }

        public Brush CircleColor
        {
            get
            {
                return (Brush)base.GetValue(CircleColorProperty);
            }
            set
            {
                base.SetValue(CircleColorProperty, value);
            }
        }

        public Orientation Orientation
        {
            get
            {
                return (Orientation)base.GetValue(OrientationProperty);
            }
            set
            {
                base.SetValue(OrientationProperty, value);
            }
        }

        public Thickness TextMargin
        {
            get
            {
                return (Thickness)base.GetValue(TextMarginProperty);
            }
            set
            {
                base.SetValue(TextMarginProperty, value);
            }
        }

        public Image ImageOver
        {
            get
            {
                return (Image)GetValue(ImageOverProperty);
            }
            set
            {
                SetValue(ImageOverProperty, value);
            }
        }

        public Image ImagePress
        {
            get
            {
                return (Image)base.GetValue(ImagePressProperty);
            }
            set
            {
                base.SetValue(ImagePressProperty, value);
            }
        }
    }
}
