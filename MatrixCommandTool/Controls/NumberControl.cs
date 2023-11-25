using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace MatrixCommandTool.Controls
{
    [TemplatePart(Name = ElementReduceButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementAddButton, Type = typeof(Button))]
    [TemplatePart(Name = ElementNumberTextBox, Type = typeof(TextBox))]

    public class NumberControl : Control
    {
        private const string ElementReduceButton = "PART_ButtonReduce";
        private const string ElementAddButton = "PART_ButtonAdd";
        private const string ElementNumberTextBox = "PART_TextBoxNumber";

        public static readonly DependencyProperty NumberProperty;
        public static readonly DependencyProperty ShowModifyButtonProperty;
        public static readonly DependencyProperty MinNumberProperty;
        public static readonly DependencyProperty MaxNumberProperty;
        public static readonly DependencyProperty IsReadonlyProperty;
        public static readonly DependencyProperty IsMinNumberProperty;
        public static readonly DependencyProperty IsMaxNumberProperty;

        /// <summary>
        /// 数值
        /// </summary>
        public int Number
        {
            get => (int)GetValue(NumberProperty);
            set => SetValue(NumberProperty, value);
        }

        /// <summary>
        /// 最小数值
        /// </summary>
        public int MinNumber
        {
            get => (int)GetValue(MinNumberProperty);
            set => SetValue(MinNumberProperty, value);
        }

        /// <summary>
        /// 最大数值
        /// </summary>
        public int MaxNumber
        {
            get => (int)GetValue(MaxNumberProperty);
            set => SetValue(MaxNumberProperty, value);
        }

        /// <summary>
        /// 是否只能通过编辑按钮调整数值
        /// </summary>
        public bool IsReadonly
        {
            get => (bool)GetValue(IsReadonlyProperty);
            set => SetValue(IsReadonlyProperty, value);
        }

        /// <summary>
        /// 是否是最小数值
        /// </summary>
        public bool IsMinNumber
        {
            get => (bool)GetValue(IsMinNumberProperty);
            private set => SetValue(IsMinNumberProperty, value);
        }

        /// <summary>
        /// 是否是最大数值
        /// </summary>
        public bool IsMaxNumber
        {
            get => (bool)GetValue(IsMaxNumberProperty);
            private set => SetValue(IsMaxNumberProperty, value);
        }

        static NumberControl()
        {
            NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(NumberControl), new PropertyMetadata(0, NumberChanged));
            MinNumberProperty = DependencyProperty.Register("MinNumber", typeof(int), typeof(NumberControl), new PropertyMetadata(int.MinValue));
            MaxNumberProperty = DependencyProperty.Register("MaxNumber", typeof(int), typeof(NumberControl), new PropertyMetadata(int.MaxValue));
            IsReadonlyProperty = DependencyProperty.Register("IsReadonly", typeof(bool), typeof(NumberControl), new PropertyMetadata(false));
            IsMinNumberProperty = DependencyProperty.Register("IsMinNumber", typeof(bool), typeof(NumberControl), new PropertyMetadata(true));
            IsMaxNumberProperty = DependencyProperty.Register("IsMaxNumber", typeof(bool), typeof(NumberControl), new PropertyMetadata(true));
        }

        /// <summary>
        /// number改变事件
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void NumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NumberControl;
            int newVal = (int)e.NewValue;
            int oldVal = (int)e.OldValue;
            if (newVal == oldVal) return;
            if (newVal <= control.MinNumber)
            {
                control.Number = control.MinNumber;
                control.IsMinNumber = false;
            }
            else
                control.IsMinNumber = true;

            if (newVal >= control.MaxNumber)
            {
                control.IsMaxNumber = false;
                control.Number = control.MaxNumber;
            }
            else
                control.IsMaxNumber = true;
        }

        private Button _reduceBtn;
        private Button _addBtn;
        private TextBox _numberTbx;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._reduceBtn = this.GetTemplateChild(ElementReduceButton) as Button;
            this._addBtn = this.GetTemplateChild(ElementAddButton) as Button;
            this._numberTbx = this.GetTemplateChild(ElementNumberTextBox) as TextBox;
            this.CheckNull();
            this.AppendEvent();
        }

        private void CheckNull()
        {
            if (_reduceBtn == null || _addBtn == null || _numberTbx == null) throw new Exception();
        }

        private void AppendEvent()
        {
            this._reduceBtn.Click += _reduceBtn_Click;
            this._addBtn.Click += _addBtn_Click;
            this._numberTbx.PreviewKeyDown += _numberTbx_KeyDown;
        }

        private void _numberTbx_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ///如果是只读状态，直接返回
            if (this.IsReadonly)
            {
                e.Handled = true;
                return;
            }
            ///如果按下Alt或者Shift或者Ctl键，直接返回，因为有可能会输入特殊字符
            if (e.KeyboardDevice.Modifiers != System.Windows.Input.ModifierKeys.None)
            {
                e.Handled = true;
                return;
            }
            ///退出、删除、左右移动键可以正常使用
            if (e.Key == System.Windows.Input.Key.Escape || e.Key == System.Windows.Input.Key.Back || e.Key == System.Windows.Input.Key.Left || e.Key == System.Windows.Input.Key.Right)
            {
                var tbx = sender as TextBox;
                ///如果是删除，判断是否是最后一位，如果是最后一位默认为0
                if (e.Key == System.Windows.Input.Key.Back && (this.Number.ToString().Length == 1 || tbx.SelectionLength == this.Number.ToString().Length))
                {
                    this.Number = 0;
                    e.Handled = true;
                }
                else
                    e.Handled = false;
                return;
            }
            int keyCode = (int)e.Key;
            ///只输出数字
            if (keyCode >= 34 && keyCode <= 43 || keyCode >= 74 && keyCode <= 83)
            {
                e.Handled = false;
                return;
            }

            e.Handled = true;
        }

        private void _addBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Number++;
        }

        private void _reduceBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Number--;
        }
    }
}
