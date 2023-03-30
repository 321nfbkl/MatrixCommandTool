using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RD15Controls
{
    /// <summary>
    ///     用以代替Grid  特别是参考HandyControl组件库之后，里面会大量使用SimplePanel控件，这里干脆吧这个组件拿过来
    /// </summary>
    /// <remarks>
    ///     当不需要Grid的行、列分隔等功能时建议用此轻量级类代替
    /// </remarks>
    public class SimplePanel : Panel
    {
        protected override Size MeasureOverride(Size constraint)
        {
            var maxSize = new Size();

            foreach (UIElement child in InternalChildren)
            {
                if (child != null)
                {
                    child.Measure(constraint);
                    maxSize.Width = Math.Max(maxSize.Width, child.DesiredSize.Width);
                    maxSize.Height = Math.Max(maxSize.Height, child.DesiredSize.Height);
                }
            }

            return maxSize;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                child?.Arrange(new Rect(arrangeSize));
            }

            return arrangeSize;
        }
    }
}
