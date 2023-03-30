using System;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Media;

namespace RD15Controls.DateTimePicker
{
    [System.ComponentModel.DesignTimeVisible(false)]//在工具箱中 隐藏该窗体
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class DateTimePicker : UserControl
    {
        public DateTimePicker()
        {
            InitializeComponent();
        }
        public ICommand TimeChangeCommand
        {
            get { return (ICommand)GetValue(TimeChangeCommandProperty); }
            set { SetValue(TimeChangeCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeChangeCommandProperty =
            DependencyProperty.Register("TimeChangeCommand", typeof(ICommand), typeof(DateTimePicker), new PropertyMetadata(null));

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txt"></param>
        public DateTimePicker(string txt)
            : this()
        {
            // this.textBox1.Text = txt;

        }

        #region 事件

        /// <summary>
        /// 日历图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconButton1_Click(object sender, RoutedEventArgs e)
        {

            TDateTimeView dtView = new TDateTimeView(DateTimeStr);// TDateTimeView  构造函数传入日期时间
            dtView.DateTimeOK += (dateTimeStr) => //TDateTimeView 日期时间确定事件
            {

                textBlock1.Text = dateTimeStr;
                DateTimeStr = dateTimeStr;
                popChioce.IsOpen = false;//TDateTimeView 所在pop  关闭

            };

            popChioce.Child = dtView;
            popChioce.IsOpen = true;
        }
        /// <summary>
        /// The delete event
        /// </summary>
        public static readonly RoutedEvent TimeTextChangedEvent =
             EventManager.RegisterRoutedEvent("TimeTextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(DateTimePicker));

        /// <summary>
        /// 时间改变的操作.
        /// </summary>
        public event RoutedEventHandler TimeTextChanged
        {
            add
            {
                AddHandler(TimeTextChangedEvent, value);
            }

            remove
            {
                RemoveHandler(TimeTextChangedEvent, value);
            }
        }
        /// <summary>
        /// DateTimePicker 窗体登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //DateTime dt = DateTime.Now;
            //textBlock1.Text = dt.ToString("yyyy-MM-dd HH:mm:ss");//"yyyyMMddHHmmss"
            //DateTimeStr = dt;            
            //  DateTime = Convert.ToDateTime(textBlock1.Text);
        }
        public void TimeTextChanged_Click(object sender, TextChangedEventArgs e)
        {
            ButtonAutomationPeer bam = new ButtonAutomationPeer(TimeChangeBtn);
            IInvokeProvider iip = bam.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
            iip.Invoke();
        }
        #endregion

        #region 属性

        /// <summary>
        /// 日期时间
        /// </summary>
        public string DateTimeStr
        {
            get { return (string)GetValue(DateTimeProperty); }
            set
            {
                SetValue(DateTimeProperty, value);
            }
        }
        // Using a DependencyProperty as the backing store for DateTimeText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeProperty =
            DependencyProperty.Register("DateTimeStr", typeof(string), typeof(DateTimePicker));
        #endregion

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(DateTimePicker), new PropertyMetadata());

        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
           DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(DateTimePicker), new PropertyMetadata(new Thickness(0)));
        public Stretch IconStretch
        {
            get { return (Stretch)GetValue(IconStretchProperty); }
            set { SetValue(IconStretchProperty, value); }
        }
        public static readonly DependencyProperty IconStretchProperty =
        DependencyProperty.Register("IconStretch", typeof(Stretch), typeof(DateTimePicker), new PropertyMetadata(Stretch.None));
        public Thickness ButtonIconMargin
        {
            get { return (Thickness)GetValue(ButtonIconMarginProperty); }
            set { SetValue(ButtonIconMarginProperty, value); }
        }
        public static readonly DependencyProperty ButtonIconMarginProperty =
           DependencyProperty.Register("ButtonIconMargin", typeof(Thickness), typeof(DateTimePicker), new PropertyMetadata(new Thickness(0)));
        public Stretch ButtonIconStretch
        {
            get { return (Stretch)GetValue(ButtonIconStretchProperty); }
            set { SetValue(ButtonIconStretchProperty, value); }
        }
        public static readonly DependencyProperty ButtonIconStretchProperty =
        DependencyProperty.Register("ButtonIconStretch", typeof(Stretch), typeof(DateTimePicker), new PropertyMetadata(Stretch.None));
        public ImageSource ButtonIcon
        {
            get { return (ImageSource)GetValue(ButtonIconProperty); }
            set { SetValue(ButtonIconProperty, value); }
        }

        public static readonly DependencyProperty ButtonIconProperty =
            DependencyProperty.Register("ButtonIcon", typeof(ImageSource), typeof(DateTimePicker), new PropertyMetadata());
        public ImageSource ButtonOverIcon
        {
            get { return (ImageSource)GetValue(ButtonOverIconProperty); }
            set { SetValue(ButtonOverIconProperty, value); }
        }
        public static readonly DependencyProperty ButtonOverIconProperty =
            DependencyProperty.Register("ButtonOverIcon", typeof(ImageSource), typeof(DateTimePicker), new PropertyMetadata());
        public ImageSource ButtonPressIcon
        {
            get { return (ImageSource)GetValue(ButtonPressIconProperty); }
            set { SetValue(ButtonPressIconProperty, value); }
        }

        public static readonly DependencyProperty ButtonPressIconProperty =
            DependencyProperty.Register("ButtonPressIcon", typeof(ImageSource), typeof(DateTimePicker), new PropertyMetadata());

        public Brush TextBackground
        {
            get { return (Brush)GetValue(TextBackgroundProperty); }
            set { SetValue(TextBackgroundProperty, value); }
        }

        public static readonly DependencyProperty TextBackgroundProperty =
            DependencyProperty.Register("TextBackground", typeof(Brush), typeof(DateTimePicker), new PropertyMetadata());

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public static readonly DependencyProperty CornerRadiusProperty =
           DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(DateTimePicker), new PropertyMetadata(new CornerRadius(0)));

        private void popChioce_Opened(object sender, EventArgs e)
        {
            iconButton1_Click(null, null);
        }

        private void mydp_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
