using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD15Controls.Models
{
    public class Enums
    {
        public enum EnumDatePickerType
        {
            /// <summary>
            /// 单个日期
            /// </summary>
            SingleDate,
            /// <summary>
            /// 连续的多个日期
            /// </summary>
            SingleDateRange,
            /// <summary>
            /// 只显示年份
            /// </summary>
            Year,
            /// <summary>
            /// 只显示月份
            /// </summary>
            Month,
            /// <summary>
            /// 显示一个日期和时间
            /// </summary>
            DateTime,
            /// <summary>
            /// 显示连续的日期和时间
            /// </summary>
            DateTimeRange,
        }
    }
}
