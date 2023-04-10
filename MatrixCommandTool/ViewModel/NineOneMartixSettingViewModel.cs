using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatrixCommandTool.Model.BindingModel;
using MatrixCommandTool.Net.TCP;
using MatrixCommandTool.Net.TCP.Request;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MatrixCommandTool.ViewModel
{
    public class NineOneMartixSettingViewModel : ViewModelBase
    {
        public ScanBoardListRequest _scanBoardListRequest;
        public ClientSocket _socket;
        #region Properities
        private bool mIsCheckedColor;
        /// <summary>
        /// 
        /// </summary>
        public bool IsCheckedColor
        {
            get => this.mIsCheckedColor;
            set => Set(ref this.mIsCheckedColor, value);
        }

        private bool mIsCheckedSwitch;
        /// <summary>
        /// 
        /// </summary>
        public bool IsCheckedSwitch
        {
            get => this.mIsCheckedSwitch;
            set => Set(ref this.mIsCheckedSwitch, value);
        }

        private bool mIsCheckedEDID;
        /// <summary>
        /// 
        /// </summary>
        public bool IsCheckedEDID
        {
            get => this.mIsCheckedEDID;
            set => Set(ref this.mIsCheckedEDID, value);
        }

        private bool mIsCheckedScene;
        /// <summary>
        /// 
        /// </summary>
        public bool IsCheckedScene
        {
            get => this.mIsCheckedScene;
            set => Set(ref this.mIsCheckedScene, value);
        }

        private bool mIsCheckedSignal;
        /// <summary>
        /// 
        /// </summary>
        public bool IsCheckedSignal
        {
            get => this.mIsCheckedSignal;
            set => Set(ref this.mIsCheckedSignal, value);
        }

        private bool mIsCheckedSetting;
        /// <summary>
        /// 
        /// </summary>
        public bool IsCheckedSetting
        {
            get => this.mIsCheckedSetting;
            set => Set(ref this.mIsCheckedSetting, value);
        }

        private int mSendIntevar = 1000;
        public int SendIntevar
        {
            get => this.mSendIntevar;
            set => Set(ref this.mSendIntevar, value);
        }

        private bool mIsMutilCheck;
        /// <summary>
        /// 是否批量选择
        /// </summary>
        public bool IsMutilCheck
        {
            get => this.mIsMutilCheck;
            set
            {
                if (Set(ref this.mIsMutilCheck, value))
                {

                }
            }
        }

        private bool mIsMutilCheckEdid;
        /// <summary>
        /// 是否批量选择
        /// </summary>
        public bool IsMutilCheckEdid
        {
            get => this.mIsMutilCheckEdid;
            set
            {
                if (Set(ref this.mIsMutilCheckEdid, value))
                {

                }
            }
        }

        private BoardBingModel mSelectedScenePlan;
        /// <summary>
        /// 选中的预案 
        /// </summary>
        public BoardBingModel SelectedScenePlan
        {
            get => this.mSelectedScenePlan;
            set
            {
                if (Set(ref this.mSelectedScenePlan, value))
                {
                    if (SelectedScenePlan == null)
                        return;
                }
            }
        }


        #region 输出类型
        private string mSelectedOutFourType;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutFourType
        {
            get => this.mSelectedOutFourType;
            set => Set(ref this.mSelectedOutFourType, value);
        }

        private string mSelectedOutThreeType;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutThreeType
        {
            get => this.mSelectedOutThreeType;
            set => Set(ref this.mSelectedOutThreeType, value);
        }

        private string mSelectedOutTwoType;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutTwoType
        {
            get => this.mSelectedOutTwoType;
            set => Set(ref this.mSelectedOutTwoType, value);
        }

        private string mSelectedOutOneType;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutOneType
        {
            get => this.mSelectedOutOneType;
            set => Set(ref this.mSelectedOutOneType, value);
        }
        #endregion

        #region 背景
        private string mSelectedOutFourBack;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutFourBack
        {
            get => this.mSelectedOutFourBack;
            set => Set(ref this.mSelectedOutFourBack, value);
        }

        private string mSelectedOutThreeBack;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutThreeBack
        {
            get => this.mSelectedOutThreeBack;
            set => Set(ref this.mSelectedOutThreeBack, value);
        }

        private string mSelectedOutTwoBack;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutTwoBack
        {
            get => this.mSelectedOutTwoBack;
            set => Set(ref this.mSelectedOutTwoBack, value);
        }

        private string mSelectedOutOneBack;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutOneBack
        {
            get => this.mSelectedOutOneBack;
            set => Set(ref this.mSelectedOutOneBack, value);
        }



        private string mSelectedBackFourName;

        public string SelectedBackFourName
        {
            get => mSelectedBackFourName;
            set => Set(ref mSelectedBackFourName, value);
        }

        private string mSelectedBackThreeName;

        public string SelectedBackThreeName
        {
            get => mSelectedBackThreeName;
            set => Set(ref mSelectedBackThreeName, value);
        }

        private string mSelectedBackTwoName;

        public string SelectedBackTwoName
        {
            get => mSelectedBackTwoName;
            set => Set(ref mSelectedBackTwoName, value);
        }

        private string mSelectedBackOneName;

        public string SelectedBackOneName
        {
            get => mSelectedBackOneName;
            set => Set(ref mSelectedBackOneName, value);
        }


        #endregion

        #region 音频
        private string mSelectedOutFourAudioName;

        public string SelectedOutFourAudioName
        {
            get => mSelectedOutFourAudioName;
            set => Set(ref mSelectedOutFourAudioName, value);
        }

        private string mSelectedOutThreeAudioName;

        public string SelectedOutThreeAudioName
        {
            get => mSelectedOutThreeAudioName;
            set => Set(ref mSelectedOutThreeAudioName, value);
        }

        private string mSelectedOutTwoAudioName;

        public string SelectedOutTwoAudioName
        {
            get => mSelectedOutTwoAudioName;
            set => Set(ref mSelectedOutTwoAudioName, value);
        }

        private string mSelectedOutOneAudioName;

        public string SelectedOutOneAudioName
        {
            get => mSelectedOutOneAudioName;
            set => Set(ref mSelectedOutOneAudioName, value);
        }

        private string mSelectedInFourAudioName;

        public string SelectedInFourAudioName
        {
            get => mSelectedInFourAudioName;
            set => Set(ref mSelectedInFourAudioName, value);
        }

        private string mSelectedInThreeAudioName;

        public string SelectedInThreeAudioName
        {
            get => mSelectedInThreeAudioName;
            set => Set(ref mSelectedInThreeAudioName, value);
        }

        private string mSelectedTwoInAudioName;

        public string SelectedTwoInAudioName
        {
            get => mSelectedTwoInAudioName;
            set => Set(ref mSelectedTwoInAudioName, value);
        }

        private string mSelectedOneInAudioName;
        public string SelectedOneInAudioName
        {
            get => mSelectedOneInAudioName;
            set => Set(ref mSelectedOneInAudioName, value);
        }
        #endregion

        /// <summary>
        /// 分页显示
        /// </summary>
        public IList<BoardBingModel> PagesSource { get; set; } = new ObservableCollection<BoardBingModel>();

        public IList<string> DeviceSwitch { get; set ; }= new ObservableCollection<string>();

        /// <summary>
        /// 色调调整指令
        /// </summary>
        public IList<BoardBingModel> NineOneColorAdj { get; set; } = new ObservableCollection<BoardBingModel>();

        /// <summary>
        /// EDID学习指令
        /// </summary>
        public IList<BoardBingModel> InputEdid { get; set; } = new ObservableCollection<BoardBingModel>();

        public IList<BoardBingModel> OutputEdid { get; set; } = new ObservableCollection<BoardBingModel>();

        /// <summary>
        /// 发送的色调调整指令
        /// </summary>
        public IList<BoardBingModel> SendColorAdjInstructionsList { get; set; } = new ObservableCollection<BoardBingModel>();

        /// <summary>
        /// 输出类型
        /// </summary>
        public IList<string> TypeList { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// 背景列表
        /// </summary>
        public IList<string> BackList { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// 音频列表
        /// </summary>
        public IList<string> AudioList { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// 获取到的EDID预案
        /// </summary>
        public IList<string> EDIDPlanList { get; set; }=new ObservableCollection<string>();

        /// <summary>
        /// 获取到的所有预案列表
        /// </summary>
        public IList<string> ScenePlanList { get; set; } = new ObservableCollection<string>();  
        #endregion

        #region Command

        public ICommand SendColorAdjInstructionsCommand { get; set; }

        public ICommand SwitchOneCommand { get; set; }

        public ICommand SwitchTwoCommand { get; set; }

        public ICommand SwitchThreeCommand { get; set; }

        public ICommand SwitchFourCommand { get; set; }
        #endregion

        public NineOneMartixSettingViewModel(ClientSocket clientSocket, ScanBoardListRequest scanBoardListRequest)
        {
            this._socket = clientSocket;
            this._scanBoardListRequest = scanBoardListRequest;
            this.SendColorAdjInstructionsCommand = new RelayCommand(SendColorAdjInstructions);

            this.SwitchOneCommand=new RelayCommand<string>(SwitchOne);
            this.SwitchTwoCommand = new RelayCommand<string>(SwitchTwo);
            this.SwitchThreeCommand = new RelayCommand<string>(SwitchThree);
            this.SwitchFourCommand = new RelayCommand<string>(SwitchFour);

            this.AudioList.Add("内部音频");
            this.AudioList.Add("外部音频");
            this.BackList.Add("设置蓝屏");
            this.BackList.Add("设置黑屏");
            this.TypeList.Add("HDMI");
            this.TypeList.Add("DVI");

            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出1" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出2" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出3" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出4" });
            #region 音频
            if (this.SelectedOneInAudioName == null)
                this.SelectedOneInAudioName = this.AudioList.FirstOrDefault();
            if (this.SelectedTwoInAudioName == null)
                this.SelectedTwoInAudioName = this.AudioList.FirstOrDefault();
            if (this.SelectedInThreeAudioName == null)
                this.SelectedInThreeAudioName = this.AudioList.FirstOrDefault();
            if (this.SelectedInFourAudioName == null)
                this.SelectedInFourAudioName = this.AudioList.FirstOrDefault();
            if (this.SelectedOutOneAudioName == null)
                this.SelectedOutOneAudioName = this.AudioList.FirstOrDefault();
            if (SelectedOutTwoAudioName == null)
                this.SelectedOutTwoAudioName = this.AudioList.FirstOrDefault();
            if (SelectedOutThreeAudioName == null)
                this.SelectedOutThreeAudioName = this.AudioList.FirstOrDefault();
            if (SelectedOutFourAudioName == null)
                this.SelectedOutFourAudioName = this.AudioList.FirstOrDefault();
            #endregion
            #region 背景
            if (this.SelectedOutOneBack == null)
                this.SelectedOutOneBack = this.BackList.FirstOrDefault();
            if (this.SelectedOutTwoBack == null)
                this.SelectedOutTwoBack = this.BackList.FirstOrDefault();
            if (this.SelectedOutThreeBack == null)
                this.SelectedOutThreeBack = this.BackList.FirstOrDefault();
            if (this.SelectedOutFourBack == null)
                this.SelectedOutFourBack = this.BackList.FirstOrDefault();
            if (this.SelectedBackOneName == null)
                this.SelectedBackOneName = this.BackList.FirstOrDefault();
            if (SelectedBackTwoName == null)
                this.SelectedBackTwoName = this.BackList.FirstOrDefault();
            if (SelectedBackThreeName == null)
                this.SelectedBackThreeName = this.BackList.FirstOrDefault();
            if (SelectedBackFourName == null)
                this.SelectedBackFourName = this.BackList.FirstOrDefault();
            #endregion
            #region 输出类型
            if (this.SelectedOutOneType == null)
                this.SelectedOutOneType = this.TypeList.FirstOrDefault();
            if (this.SelectedOutTwoType == null)
                this.SelectedOutTwoType = this.TypeList.FirstOrDefault();
            if (this.SelectedOutThreeType == null)
                this.SelectedOutThreeType = this.TypeList.FirstOrDefault();
            if (this.SelectedOutFourType == null)
                this.SelectedOutFourType = this.TypeList.FirstOrDefault();
            #endregion
            initCommandList();
        }

        #region 输入输出切换
        private void SwitchOne(string name)
        {
            if (name == "输出1")
            {

            }
            else if (name == "输出2")
            {

            }
            else if (name == "输出3")
            {

            }
            else if (name == "输出4")
            {

            }
        }

        private void SwitchTwo(string name)
        {
            if (name == "输出1")
            {

            }
            else if (name == "输出2")
            {

            }
            else if (name == "输出3")
            {

            }
            else if (name == "输出4")
            {

            }
        }


        private void SwitchThree(string name)
        {
            if (name == "输出1")
            {

            }
            else if (name == "输出2")
            {

            }
            else if (name == "输出3")
            {

            }
            else if (name == "输出4")
            {

            }
        }


        private void SwitchFour(string name)
        {
            if (name == "输出1")
            {

            }
            else if (name == "输出2")
            {

            }
            else if (name == "输出3")
            {

            }
            else if (name == "输出4")
            {

            }
        }

        #endregion


        /// <summary>
        /// 加载指令
        /// </summary>
        private void initCommandList()
        {
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "设置背景为蓝屏，重启有效", IsCheckInstructions = false, Code = "15C02." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "设置背景为黑屏，重启有效", IsCheckInstructions = false, Code = "16C02." });
            
            this.InputEdid.Add(new BoardBingModel() { Instructions = "1", IsCheckInstructions = false });
            this.InputEdid.Add(new BoardBingModel() { Instructions = "2", IsCheckInstructions = false });
            this.InputEdid.Add(new BoardBingModel() { Instructions = "3", IsCheckInstructions = false });
            this.InputEdid.Add(new BoardBingModel() { Instructions = "4", IsCheckInstructions = false });

            this.OutputEdid.Add(new BoardBingModel() { Instructions = "1", IsCheckInstructions = false });
            this.OutputEdid.Add(new BoardBingModel() { Instructions = "2", IsCheckInstructions = false });
            this.OutputEdid.Add(new BoardBingModel() { Instructions = "3", IsCheckInstructions = false });
            this.OutputEdid.Add(new BoardBingModel() { Instructions = "4", IsCheckInstructions = false });
            
        }

        /// <summary>
        /// 发送色调调整命令
        /// </summary>
        private void SendColorAdjInstructions()
        {
            string sendError = string.Empty;
            this.SendColorAdjInstructionsList.Clear();
            foreach (var item in this.NineOneColorAdj)
            {
                if (item.IsCheckInstructions == true)
                {
                    if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                    {
                        this.SendDataBySerialPort(item.Code);
                    }
                    else
                    {
                        this._scanBoardListRequest.SendIndtruction(item.Code, out sendError);
                    }
                    this.SendColorAdjInstructionsList.Add(new BoardBingModel() { Instructions = item.Instructions, Code = item.Code });
                    Thread.Sleep(SendIntevar);
                }
            }
        }

        public void SendDataBySerialPort(string message)
        {
            UTF8Encoding utf8 = new UTF8Encoding(false);
            Byte[] writeBytes = utf8.GetBytes(message);
            byte[] sendData = null;
            try
            {
                sendData = Encoding.UTF8.GetBytes(message.Trim());
                GlobalContext.Current.CurrentVMLocator.MainVM.Serial.Write(sendData, 0, sendData.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }






    }
}
