using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RD15Controls
{
    public class CardGroupBoxControl : GroupBox
    {
        static CardGroupBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CardGroupBoxControl), new FrameworkPropertyMetadata(typeof(CardGroupBoxControl)));
        }

        public static readonly DependencyProperty HeadMarginProperty = DependencyProperty.Register("HeadMargin",
            typeof(Thickness), typeof(CardGroupBoxControl));

        public static readonly DependencyProperty HeadHorAlignProperty = DependencyProperty.Register("HeadHorAlign",
            typeof(HorizontalAlignment), typeof(CardGroupBoxControl), new PropertyMetadata(HorizontalAlignment.Left));

        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.Register("ContentMargin",
            typeof(Thickness), typeof(CardGroupBoxControl));

        public static readonly DependencyProperty HeadFontSizeProperty = DependencyProperty.Register("HeadFontSize",
            typeof(double), typeof(CardGroupBoxControl), new PropertyMetadata(12.0));

        public static readonly DependencyProperty HeadFontBrushProperty = DependencyProperty.Register("HeadFontBrush",
            typeof(Brush), typeof(CardGroupBoxControl));

        public static readonly DependencyProperty CardBorderCornerRadiusProperty = DependencyProperty.Register("CardBorderCornerRadius",
            typeof(CornerRadius), typeof(CardGroupBoxControl));

        public Thickness HeadMargin
        {
            get => (Thickness)GetValue(HeadMarginProperty);
            set => SetValue(HeadMarginProperty, value);
        }

        public HorizontalAlignment HeadHorAlign
        {
            get => (HorizontalAlignment)GetValue(HeadHorAlignProperty);
            set => SetValue(HeadHorAlignProperty, value);
        }

        public Thickness ContentMargin
        {
            get => (Thickness)GetValue(ContentMarginProperty);
            set => SetValue(ContentMarginProperty, value);
        }

        public double HeadFontSize
        {
            get => (double)GetValue(HeadFontSizeProperty);
            set => SetValue(HeadFontSizeProperty, value);
        }

        public Brush HeadFontBrush
        {
            get => (Brush)GetValue(HeadFontBrushProperty);
            set => SetValue(HeadFontBrushProperty, value);
        }
        public CornerRadius CardBorderCornerRadius
        {
            get => (CornerRadius)GetValue(CardBorderCornerRadiusProperty);
            set => SetValue(CardBorderCornerRadiusProperty, value);
        
        }
    }
}
