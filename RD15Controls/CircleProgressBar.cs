using Microsoft.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace RD15Controls
{
    public class CircleProgressBar : RangeBase
    {
        static CircleProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CircleProgressBar), new FrameworkPropertyMetadata(typeof(CircleProgressBar)));
        }

        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register("StartAngle", typeof(double), typeof(CircleProgressBar), new PropertyMetadata(0.0));

        public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register("EndAngle", typeof(double), typeof(CircleProgressBar), new PropertyMetadata(360.0));

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(double), typeof(CircleProgressBar), new PropertyMetadata(3.0));

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(double), typeof(CircleProgressBar), new PropertyMetadata(0.0));

        public static readonly DependencyProperty DurtionProperty = DependencyProperty.Register("Durtion", typeof(double), typeof(CircleProgressBar), new PropertyMetadata(500.0));

        public double StartAngle
        {
            get
            {
                return (double)base.GetValue(StartAngleProperty);
            }
            set
            {
                base.SetValue(StartAngleProperty, value);
            }
        }
        public double EndAngle
        {
            get
            {
                return (double)base.GetValue(EndAngleProperty);
            }
            set
            {
                base.SetValue(EndAngleProperty, value);
            }
        }

        public double StrokeThickness
        {
            get => (double)GetValue(StrokeThicknessProperty);
            set => SetValue(StrokeThicknessProperty, value);
        }
        public double Angle
        {
            get
            {
                return (double)base.GetValue(CircleProgressBar.AngleProperty);
            }
            private set
            {
                base.SetValue(CircleProgressBar.AngleProperty, value);
            }
        }

        public double Durtion
        {
            get
            {
                return (double)base.GetValue(CircleProgressBar.DurtionProperty);
            }
            set
            {
                base.SetValue(CircleProgressBar.DurtionProperty, value);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Indicator = (base.GetTemplateChild("Indicator") as Arc);
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            this.oldAngle = this.Angle;
            double num = base.Value - base.Minimum;
            this.Angle = StartAngle + Math.Abs(EndAngle - StartAngle) / (base.Maximum - base.Minimum) * num;
            this.TransformAngle(this.oldAngle, this.Angle, this.Durtion);
        }

        private void SetAngle()
        {
            bool flag = base.Value < base.Minimum;
            if (flag)
            {
                this.Angle = StartAngle;
            }
            else
            {
                bool flag2 = base.Value > base.Maximum;
                if (flag2)
                {
                    this.Angle = EndAngle;
                }
            }
        }

        private void TransformAngle(double From, double To, double durtion)
        {
            bool flag = this.Indicator != null;
            if (flag)
            {
                DoubleAnimation animation = new DoubleAnimation(From, this.Angle, new Duration(TimeSpan.FromMilliseconds(durtion)));
                this.Indicator.BeginAnimation(Arc.EndAngleProperty, animation);
            }
        }

        private Arc Indicator;

        private double oldAngle;


    }
}
