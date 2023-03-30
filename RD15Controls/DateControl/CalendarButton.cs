using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RD15Controls.DateControl
{
    public class CalendarButton : Button
    {
        #region Private属性

        #endregion

        #region Fields
        public Calendar Owner { get; set; }
        #endregion

        #region 依赖属性set get

        #region HasSelectedDates
        public bool HasSelectedDates
        {
            get { return (bool)GetValue(HasSelectedDatesProperty); }
            set { SetValue(HasSelectedDatesProperty, value); }
        }

        public static readonly DependencyProperty HasSelectedDatesProperty =
            DependencyProperty.Register("HasSelectedDates", typeof(bool), typeof(CalendarButton), new PropertyMetadata(false));
        #endregion

        #endregion

        #region Constructors
        static CalendarButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalendarButton), new FrameworkPropertyMetadata(typeof(CalendarButton)));
        }
        #endregion

        #region Override方法

        #endregion

        #region Private方法

        #endregion
    }
}
