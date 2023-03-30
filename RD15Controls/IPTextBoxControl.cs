using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RD15Controls
{
    public class IPTextBoxControl : Control
    {
        static IPTextBoxControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IPTextBoxControl), new FrameworkPropertyMetadata(typeof(IPTextBoxControl)));
        }
        private TextBox txt_One;
        private TextBox txt_Two;
        private TextBox txt_Three;
        private TextBox txt_Four;

        public readonly static DependencyProperty IPAddressProperty =
            DependencyProperty.Register("IPAddress", typeof(string), typeof(IPTextBoxControl), new PropertyMetadata(null));

        public readonly static DependencyProperty IPAddsOneProperty =
            DependencyProperty.Register("IPAddsOne", typeof(string), typeof(IPTextBoxControl), new PropertyMetadata(null));

        public readonly static DependencyProperty IPAddsTwoProperty =
             DependencyProperty.Register("IPAddsTwo", typeof(string), typeof(IPTextBoxControl), new PropertyMetadata(null));

        public readonly static DependencyProperty IPAddsThreeProperty =
            DependencyProperty.Register("IPAddsThree", typeof(string), typeof(IPTextBoxControl), new PropertyMetadata(null));

        public readonly static DependencyProperty IPAddsFourProperty =
            DependencyProperty.Register("IPAddsFour", typeof(string), typeof(IPTextBoxControl), new PropertyMetadata(null));

        public readonly static DependencyProperty BorderCornerRadiusProperty =
            DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(IPTextBoxControl), new PropertyMetadata(new CornerRadius(0, 0, 0, 0)));

        public string IPAddress
        {
            get
            {
                string changesIp = getChangedIpAddress();
                string beforIp = GetValue(IPAddressProperty).ToString();
                if (changesIp != beforIp)
                    return changesIp;
                return GetValue(IPAddressProperty).ToString();
            }
            set
            {
                SetValue(IPAddressProperty, value);
                var ips = value.Split('.');
                if (ips.Length < 4)
                    return;
                IPAddsOne = ips[0];
                IPAddsTwo = ips[1];
                IPAddsThree = ips[2];
                IPAddsFour = ips[3];
            }
        }
        private static void IPChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                return;

        }

        public string IPAddsOne
        {
            get { return GetValue(IPAddsOneProperty).ToString(); }
            set { SetValue(IPAddsOneProperty, value); }
        }

        public string IPAddsTwo
        {
            get { return GetValue(IPAddsTwoProperty).ToString(); }
            set { SetValue(IPAddsTwoProperty, value); }
        }
        public string IPAddsThree
        {
            get { return GetValue(IPAddsThreeProperty).ToString(); }
            set { SetValue(IPAddsThreeProperty, value); }
        }

        public string IPAddsFour
        {
            get { return GetValue(IPAddsFourProperty).ToString(); }
            set { SetValue(IPAddsFourProperty, value); }
        }

        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }
        private string getChangedIpAddress()
        {
            string one = string.IsNullOrEmpty(this.IPAddsOne) ? "0" : this.IPAddsOne;
            string two = string.IsNullOrEmpty(this.IPAddsTwo) ? "0" : this.IPAddsTwo;
            string three = string.IsNullOrEmpty(this.IPAddsThree) ? "0" : this.IPAddsThree;
            string four = string.IsNullOrEmpty(this.IPAddsFour) ? "0" : this.IPAddsFour;
            return $"{one}.{two}.{three}.{four}";
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.txt_One = GetTemplateChild("txt_One") as TextBox;
            this.txt_Two = GetTemplateChild("txt_Two") as TextBox;
            this.txt_Three = GetTemplateChild("txt_Three") as TextBox;
            this.txt_Four = GetTemplateChild("txt_Four") as TextBox;

            if (txt_One != null)
            {
                this.txt_One.PreviewKeyDown += TxtBox_PreviewKeyDown;
                this.txt_One.TextChanged += Txt_One_TextChanged;
                this.txt_One.LostFocus += Txt_One_LostFocus;
            }

            if (txt_Two != null)
            {
                this.txt_Two.PreviewKeyDown += TxtBox_PreviewKeyDown;
                this.txt_Two.TextChanged += Txt_One_TextChanged;
                this.txt_Two.LostFocus += Txt_One_LostFocus;
            }

            if (txt_Three != null)
            {
                this.txt_Three.PreviewKeyDown += TxtBox_PreviewKeyDown;
                this.txt_Three.TextChanged += Txt_One_TextChanged;
                this.txt_Three.LostFocus += Txt_One_LostFocus;
            }

            if (txt_Four != null)
            {
                this.txt_Four.PreviewKeyDown += TxtBox_PreviewKeyDown;
                this.txt_Four.TextChanged += Txt_One_TextChanged;
                this.txt_Four.LostFocus += Txt_One_LostFocus;
            }
        }

        private void Txt_One_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Txt_One_TextChanged(object sender, TextChangedEventArgs e)
        {

            var textbox = sender as TextBox;
            if (int.TryParse(textbox.Text, out int value) && textbox.IsKeyboardFocused)
            {

                if (textbox.Text.Length >= 3)
                {
                    switch (textbox.Name)
                    {
                        case "txt_One":
                            this.txt_Two.Focus();
                            if (!string.IsNullOrEmpty(this.txt_Two.Text))
                            {
                                this.txt_Two.SelectionStart = 0;
                                this.txt_Two.SelectionLength = this.txt_Two.Text.Length;
                            }
                            break;
                        case "txt_Two":
                            this.txt_Three.Focus();
                            if (!string.IsNullOrEmpty(this.txt_Three.Text))
                            {
                                this.txt_Three.SelectionStart = 0;
                                this.txt_Three.SelectionLength = this.txt_Three.Text.Length;
                            }
                            break;
                        case "txt_Three":
                            this.txt_Four.Focus();
                            if (!string.IsNullOrEmpty(this.txt_Four.Text))
                            {
                                this.txt_Four.SelectionStart = 0;
                                this.txt_Four.SelectionLength = this.txt_Four.Text.Length;
                            }
                            break;
                        case "txt_Four":
                            break;
                    }
                }
                if (value > 255)
                    textbox.Text = "255";
            }
        }

        private void TxtBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            var textbox = sender as TextBox;

            if (e.Key == System.Windows.Input.Key.Left || e.Key == System.Windows.Input.Key.Right)
            {
                if (textbox.SelectionStart == textbox.Text.Length && e.Key == System.Windows.Input.Key.Right)
                {
                    switch (textbox.Name)
                    {
                        case "txt_One":
                            this.txt_Two.Focus();
                            this.txt_Two.SelectionStart = 0;
                            break;
                        case "txt_Two":
                            this.txt_Three.Focus();
                            this.txt_Three.SelectionStart = 0;
                            break;
                        case "txt_Three":
                            this.txt_Four.Focus();
                            this.txt_Four.SelectionStart = 0;
                            break;
                        case "txt_Four":
                            break;
                    }
                }
                else if (textbox.SelectionStart == 0 && e.Key == System.Windows.Input.Key.Left)
                {
                    switch (textbox.Name)
                    {
                        case "txt_Two":
                            this.txt_One.Focus();
                            this.txt_One.SelectionStart = this.txt_One.Text.Length;
                            break;
                        case "txt_Three":
                            this.txt_Two.Focus();
                            this.txt_Two.SelectionStart = this.txt_Two.Text.Length;
                            break;
                        case "txt_Four":
                            this.txt_Three.Focus();
                            this.txt_Three.SelectionStart = this.txt_Three.Text.Length;
                            break;
                    }
                }
                e.Handled = false;
                return;
            }
            if (e.Key == System.Windows.Input.Key.Space || e.Key == System.Windows.Input.Key.OemPeriod)
            {
                switch (textbox.Name)
                {
                    case "txt_One":
                        this.txt_Two.Focus();
                        if (!string.IsNullOrEmpty(this.txt_Two.Text))
                        {
                            this.txt_Two.SelectionStart = 0;
                            this.txt_Two.SelectionLength = this.txt_Two.Text.Length;
                        }
                        break;
                    case "txt_Two":
                        this.txt_Three.Focus();
                        if (!string.IsNullOrEmpty(this.txt_Three.Text))
                        {
                            this.txt_Three.SelectionStart = 0;
                            this.txt_Three.SelectionLength = this.txt_Three.Text.Length;
                        }
                        break;
                    case "txt_Three":
                        this.txt_Four.Focus();
                        if (!string.IsNullOrEmpty(this.txt_Four.Text))
                        {
                            this.txt_Four.SelectionStart = 0;
                            this.txt_Four.SelectionLength = this.txt_Four.Text.Length;
                        }
                        break;
                    case "txt_Four":
                        break;
                }
            }
            if (e.Key != System.Windows.Input.Key.Back && textbox.SelectionLength <= 0)
            {
                int keyCode = (int)e.Key;
                string txt = (sender as TextBox).Text;
                string selectedTxt = (sender as TextBox).SelectedText;

                if (keyCode < 34 || (keyCode > 43 && keyCode < 74) || keyCode > 83 || txt.Length >= 3)
                {
                    e.Handled = true;
                    return;
                }
            }
        }
    }
}
