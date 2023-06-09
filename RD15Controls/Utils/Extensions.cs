﻿using RD15Controls.DateControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RD15Controls.Utils
{
    public static class Extensions
    {
        /// <summary>
        /// 平滑滚动至指定元素
        /// </summary>
        /// <param name="itemsControl"></param>
        /// <param name="item"></param>
        public static void AnimateScrollIntoView(this ItemsControl itemsControl, object item)
        {
            ScrollViewer scrollViewer = Utils.VisualHelper.FindVisualChild<ScrollViewer>(itemsControl);
            if (scrollViewer == null)
            {
                return;
            }

            UIElement container = itemsControl.ItemContainerGenerator.ContainerFromItem(item) as UIElement;

            if (container == null)
            {
                return;
            }

            int index = itemsControl.ItemContainerGenerator.IndexFromContainer(container);

            //平滑滚动到元素所在位置
            //double toValue = scrollViewer.ScrollableHeight * ((double)index / itemsControl.Items.Count);
            //平滑滚动，将选中的元素置顶
            double toValue = VisualTreeHelper.GetOffset(container).Y;

            DoubleAnimation verticalAnimation = new DoubleAnimation();
            verticalAnimation.From = scrollViewer.VerticalOffset;
            verticalAnimation.To = toValue;
            verticalAnimation.DecelerationRatio = .2;
            verticalAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(200));
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(verticalAnimation);
            Storyboard.SetTarget(verticalAnimation, scrollViewer);
            Storyboard.SetTargetProperty(verticalAnimation, new PropertyPath(ScrollViewerBehavior.VerticalOffsetProperty));
            storyboard.Begin();
        }
    }


    public class ScrollViewerBehavior
    {
        public static DependencyProperty VerticalOffsetProperty =
            DependencyProperty.RegisterAttached("VerticalOffset",
                                                typeof(double),
                                                typeof(ScrollViewerBehavior),
                                                new UIPropertyMetadata(0.0, OnVerticalOffsetChanged));

        public static void SetVerticalOffset(FrameworkElement target, double value)
        {
            target.SetValue(VerticalOffsetProperty, value);
        }
        public static double GetVerticalOffset(FrameworkElement target)
        {
            return (double)target.GetValue(VerticalOffsetProperty);
        }
        private static void OnVerticalOffsetChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer scrollViewer = target as ScrollViewer;
            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset((double)e.NewValue);
            }
        }
    }
}
