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

        private bool mIsCheckChannel;
        /// <summary>
        /// 是否选中通道号
        /// </summary>
        public bool IsCheckChannel
        {
            get => this.mIsCheckChannel;    
            set => Set(ref this.mIsCheckChannel, value);    
        }

        private int mOnputChanne;
        /// <summary>
        /// 输出通道
        /// </summary>
        public int OnputChannel
        {
            get => this.mOnputChanne;
            set => Set(ref this.mOnputChanne, value);
        }

        private int mInputChannel;
        /// <summary>
        /// 输入通道
        /// </summary>
        public int InputChannel
        {
            get => this.mInputChannel;
            set => Set(ref this.mInputChannel, value);
        }

        private string mEDIDInfo;
        /// <summary>
        /// EDID预案
        /// </summary>
        public string EDEDIDInfo
        {
            get => this.mEDIDInfo;
            set => Set(ref this.mEDIDInfo, value);  
        }

        private string mPlanInfo;
        /// <summary>
        /// 预案信息
        /// </summary>
        public string PlanInfo
        {
            get => this.mPlanInfo;
            set => Set(ref this.mPlanInfo, value);
        }

        private string mNetInfo;
        /// <summary>
        /// 网络信息
        /// </summary>
        public string NetInfo
        {
            get => this.mNetInfo;
            set => Set(ref this.mNetInfo, value);
        }

        private string mColorSettings;
        /// <summary>
        /// 色彩参数
        /// </summary>
        public string ColorSettings
        {
            get=>this.mColorSettings;
            set => Set(ref this.mColorSettings, value);
        }

    }
}
