using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
