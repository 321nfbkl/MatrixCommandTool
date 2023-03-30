using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RD15Controls
{
    public class CornerThumb : Thumb
    {
        public DragDirection DragDirection
        {
            get { return (DragDirection)GetValue(DragDirectionProperty); }
            set { SetValue(DragDirectionProperty, value); }
        }
        public static readonly DependencyProperty DragDirectionProperty =
            DependencyProperty.Register("DragDirection", typeof(DragDirection), typeof(CornerThumb), new PropertyMetadata(DragDirection.MiddleCenter));

        public new Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }
        public static new readonly DependencyProperty BorderBrushProperty =
            DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(CornerThumb), new PropertyMetadata(null));
    }
    public enum DragDirection
    {
        TopLeft = 1,
        TopCenter = 2,
        TopRight = 4,
        MiddleLeft = 16,
        MiddleCenter = 32,
        MiddleRight = 64,
        BottomLeft = 256,
        BottomCenter = 512,
        BottomRight = 1024,
    }
}
