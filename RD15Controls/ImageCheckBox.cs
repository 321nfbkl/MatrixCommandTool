using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media;
namespace RD15Controls
{
    public class ImageCheckBox :CheckBox
    {
        static ImageCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageCheckBox), new FrameworkPropertyMetadata(typeof(ImageCheckBox)));
        }

        public static readonly DependencyProperty UnCheckedImageProperty =
            DependencyProperty.Register("UnCheckedImage", typeof(ImageBrush), typeof(ImageCheckBox));

        public static readonly DependencyProperty CheckedImageProperty =
            DependencyProperty.Register("CheckedImage", typeof(ImageBrush), typeof(ImageCheckBox));

        public static readonly DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(ImageCheckBox));

        public ImageBrush UnCheckedImage
        {
            get => (ImageBrush)GetValue(UnCheckedImageProperty);
            set => SetValue(UnCheckedImageProperty, value);
        }

        public ImageBrush CheckedImage
        {
            get => (ImageBrush)GetValue(CheckedImageProperty);
            set => SetValue(CheckedImageProperty, value);
        }

        public CornerRadius BorderCornerRadius
        {
            get => (CornerRadius)GetValue(BorderCornerRadiusProperty);
            set => SetValue(BorderCornerRadiusProperty, value);
        }
    }
}
