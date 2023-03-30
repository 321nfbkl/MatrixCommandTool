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
    [StyleTypedProperty(Property = "Style", StyleTargetType = typeof(ITCButton))]
    public class ITCButton : Button
    {
        static ITCButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ITCButton), new FrameworkPropertyMetadata(typeof(ITCButton)));
        }

        public static readonly DependencyProperty BorderCornerRadiusProperty =
           DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(ITCButton));
        public static readonly DependencyProperty MouseOverBgColorProperty =
            DependencyProperty.Register("MouseOverBgColor", typeof(Brush), typeof(ITCButton));
        public static readonly DependencyProperty MouseOverFontColorProperty =
            DependencyProperty.Register("MouseOverFontColor", typeof(Brush), typeof(ITCButton));
        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.Register("MouseOverBorderBrush", typeof(Brush), typeof(ITCButton));
        public static readonly DependencyProperty PressedBgColorProperty =
            DependencyProperty.Register("PressedBgColor", typeof(Brush), typeof(ITCButton));
        public static readonly DependencyProperty PressedFontColorProperty =
            DependencyProperty.Register("PressedFontColor", typeof(Brush), typeof(ITCButton));
        public static readonly DependencyProperty PressedScaleProperty =
            DependencyProperty.Register("PressedScale", typeof(bool), typeof(ITCButton));
        public static readonly DependencyProperty DisableBgColorProperty =
            DependencyProperty.Register("DisableBgColor", typeof(Brush), typeof(ITCButton));
        public static readonly DependencyProperty DisableForeColorProperty =
            DependencyProperty.Register("DisableForeColor", typeof(Brush), typeof(ITCButton));
        public static readonly DependencyProperty DisableTextProperty =
            DependencyProperty.Register("DisableText", typeof(string), typeof(ITCButton));
        public static readonly DependencyProperty HasDisableContentProperty =
            DependencyProperty.Register("HasDisableContent", typeof(bool), typeof(ITCButton));
        public static readonly DependencyProperty BackImageHorAlignProperty =
            DependencyProperty.Register("BackImageHorAlign", typeof(HorizontalAlignment), typeof(ITCButton),new PropertyMetadata(HorizontalAlignment.Left));


        /// <summary>
        /// 按钮边框圆角
        /// </summary>
        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }

        /// <summary>
        /// 获取鼠标焦点背景色
        /// </summary>
        public Brush MouseOverBgColor
        {
            get { return GetValue(MouseOverBgColorProperty) as Brush; }
            set { SetValue(MouseOverBgColorProperty, value); }
        }

        /// <summary>
        /// 获取鼠标焦点前景色
        /// </summary>
        public Brush MouseOverFontColor
        {
            get { return GetValue(MouseOverFontColorProperty) as Brush; }
            set { SetValue(MouseOverFontColorProperty, value); }
        }

        /// <summary>
        /// 获取鼠标焦点边框颜色
        /// </summary>
        public Brush MouseOverBorderBrush
        {
            get { return GetValue(MouseOverBorderBrushProperty) as Brush; }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }

        /// <summary>
        /// 鼠标按下背景色
        /// </summary>
        public Brush PressedBgColor
        {
            get { return GetValue(PressedBgColorProperty) as Brush; }
            set { SetValue(PressedBgColorProperty, value); }
        }

        /// <summary>
        /// 鼠标按下前景色
        /// </summary>
        public Brush PressedFontColor
        {
            get { return GetValue(PressedFontColorProperty) as Brush; }
            set { SetValue(PressedFontColorProperty, value); }
        }

        /// <summary>
        /// 是否显示按下缩放特效
        /// </summary>
        public bool PressedScale
        {
            get { return (bool)GetValue(PressedScaleProperty); }
            set { SetValue(PressedScaleProperty, value); }
        }

        /// <summary>
        /// 按钮禁用后的背景色
        /// </summary>
        public Brush DisableBgColor
        {
            get { return GetValue(DisableBgColorProperty) as Brush; }
            set { SetValue(DisableBgColorProperty, value); }
        }

        /// <summary>
        /// 禁用后的前景色
        /// </summary>
        public Brush DisableForeColor
        {
            get { return GetValue(DisableForeColorProperty) as Brush; }
            set { SetValue(DisableForeColorProperty, value); }
        }

        /// <summary>
        /// 按钮禁用后的文本
        /// </summary>
        public string DisableText
        {
            get { return GetValue(DisableTextProperty).ToString(); }
            set { SetValue(DisableTextProperty, value); }
        }

        /// <summary>
        /// 是否包含禁用内容
        /// </summary>
        public bool HasDisableContent
        {
            get { return (bool)GetValue(HasDisableContentProperty); }
            set { SetValue(HasDisableContentProperty, value); }
        }

        /// <summary>
        /// 按钮有背景图时背景图所在位置
        /// </summary>
        public HorizontalAlignment BackImageHorAlign
        {
            get => (HorizontalAlignment)GetValue(BackImageHorAlignProperty);
            set => SetValue(BackImageHorAlignProperty, value);
        }
    }
}
