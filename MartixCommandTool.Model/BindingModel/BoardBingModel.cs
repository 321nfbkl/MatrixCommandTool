using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;

namespace MatrixCommandTool.Model.BindingModel
{
    public  class BoardBingModel:NotifyObject
    {
        private string mBoardName;
        /// <summary>
        /// 板卡名称
        /// </summary>
        public string BoardName
        {
            get => this.mBoardName;
            set => Set(ref this.mBoardName, value);
        }

        private bool mIsCheckInstructions;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsCheckInstructions
        {
            get => this.mIsCheckInstructions;
            set => Set(ref this.mIsCheckInstructions, value);
        }

        private string mInstructions;
        /// <summary>
        /// 命令
        /// </summary>
        public string Instructions
        {
            get => this.mInstructions;
            set => Set(ref this.mInstructions, value);
        }

        private string mCode;
        /// <summary>
        /// 已发送指令
        /// </summary>
        public string Code
        {
            get => this.mCode;
            set => Set(ref this.mCode, value);
        }

        private int mRate;
        /// <summary>
        /// 已发送指令
        /// </summary>
        public int Rate
        {
            get => this.mRate;
            set => Set(ref this.mRate, value);
        }

        private int mIndex;
        /// <summary>
        /// 预案的编号
        /// </summary>
        public int Index
        {
            get => this.mIndex;
            set=>Set(ref this.mIndex, value);   
        }

        private string mPlanName;
        /// <summary>
        /// 预案的名称
        /// </summary>
        public string PlanName
        {
            get => this.mPlanName;
            set => Set(ref this.mPlanName, value);
        }

        private string mOutputName;
        /// <summary>
        /// 输出卡名称
        /// </summary>
        public string OutputName
        {
            get => this.mOutputName;
            set => Set(ref this.mOutputName, value);
        }

        private SolidColorBrush mInput1 =Brushes.White;
        /// <summary>
        /// 输出卡名称
        /// </summary>
        public SolidColorBrush Input1
        {
            get => this.mInput1;
            set => Set(ref this.mInput1, value);
        }


        private SolidColorBrush mInput2 = Brushes.White;
        /// <summary>
        /// 输出卡名称
        /// </summary>
        public SolidColorBrush Input2
        {
            get => this.mInput2;
            set => Set(ref this.mInput2, value);
        }

        private SolidColorBrush mInput3 = Brushes.White;
        /// <summary>
        /// 输出卡名称
        /// </summary>
        public SolidColorBrush Input3
        {
            get => this.mInput3;
            set => Set(ref this.mInput3, value);
        }

        private SolidColorBrush mInput4 = Brushes.White;
        /// <summary>
        /// 输出卡名称
        /// </summary>
        public SolidColorBrush Input4
        {
            get => this.mInput4;
            set => Set(ref this.mInput4, value);
        }
    }
}
