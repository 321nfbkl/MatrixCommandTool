using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatrixCommandTool.Model.BindingModel;
using MatrixCommandTool.Net.TCP;
using MatrixCommandTool.Net.TCP.Request;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
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
                    if (value == true)
                    {
                        if (this.NineOneColorAdj != null)
                        {
                            foreach (var item in NineOneColorAdj)
                            {
                                item.IsCheckInstructions = true;
                            }
                        }
                    }
                    else if (value == false)
                    {
                        if (this.NineOneColorAdj != null)
                        {
                            foreach (var item in NineOneColorAdj)
                            {
                                item.IsCheckInstructions = false;
                            }
                        }
                    }
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

        /// <summary>
        /// 指令选延迟方法
        /// </summary>
        private Helper.DelayTrigger mInstructionsFilterDelay;

        private string mFindInstructionsStr;
        /// <summary>
        /// 搜索命令字符
        /// </summary>
        public string FindInstructionsStr
        {
            get => this.mFindInstructionsStr;
            set
            {
                if (Set(ref this.mFindInstructionsStr, value))
                    this.mInstructionsFilterDelay.SetKey(value);
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
            set
            {
                if (Set(ref this.mSelectedOutFourType, value))
                {
                    if (value == "HDMI")
                    {
                        SetHDMI();
                    }
                    else if (value == "DVI")
                    {
                        SetDvi();
                    }
                }
            }
        }

        private string mSelectedOutThreeType;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutThreeType
        {
            get => this.mSelectedOutThreeType;
            set
            {
                if (Set(ref this.mSelectedOutThreeType, value))
                {
                    if (value == "HDMI")
                    {
                        SetHDMI();
                    }
                    else if (value == "DVI")
                    {
                        SetDvi();
                    }
                }
            }
        }

        private string mSelectedOutTwoType;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutTwoType
        {
            get => this.mSelectedOutTwoType;
            set
            {
                if (Set(ref this.mSelectedOutTwoType, value))
                {
                    if (value == "HDMI")
                    {
                        SetHDMI();
                    }
                    else if (value == "DVI")
                    {
                        SetDvi();
                    }
                }
            }
        }

        private string mSelectedOutOneType;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutOneType
        {
            get => this.mSelectedOutOneType;
            set
            {
                if (Set(ref this.mSelectedOutOneType, value))
                {
                    if (value == "HDMI")
                    {
                        SetHDMI();
                    }
                    else if (value == "DVI")
                    {
                        SetDvi();
                    }
                }
            }
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
            set
            {
                if (Set(ref this.mSelectedOutFourBack, value))
                {
                    if (value == "设为蓝屏")
                    {
                        SetBgBlueScreen();
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen();
                    }
                }
            }
        }


        private string mSelectedOutThreeBack;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutThreeBack
        {
            get => this.mSelectedOutThreeBack;
            set
            {
                if (Set(ref this.mSelectedOutThreeBack, value))
                {
                    if (value == "设为蓝屏")
                    {
                        SetBgBlueScreen();
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen();
                    }
                }
            }
        }

        private string mSelectedOutTwoBack;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutTwoBack
        {
            get => this.mSelectedOutTwoBack;
            set
            {
                if (Set(ref this.mSelectedOutTwoBack, value))
                {
                    if (value == "设为蓝屏")
                    {
                        SetBgBlueScreen();
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen();
                    }
                }
            }
        }

        private string mSelectedOutOneBack;
        /// <summary>
        /// 
        /// </summary>
        public string SelectedOutOneBack
        {
            get => this.mSelectedOutOneBack;
            set
            {
                if (Set(ref this.mSelectedOutOneBack, value))
                {
                    if (value == "设为蓝屏")
                    {
                        SetBgBlueScreen();
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen();
                    }
                }
            }
        }



        private string mSelectedBackFourName;

        public string SelectedBackFourName
        {
            get => mSelectedBackFourName;
            set
            {
                if (Set(ref this.mSelectedBackFourName, value))
                {
                    if (value == "设为蓝屏")
                    {
                        SetBgBlueScreen();
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen();
                    }
                }
            }
        }

        private string mSelectedBackThreeName;

        public string SelectedBackThreeName
        {
            get => mSelectedBackThreeName;
            set
            {
                if (Set(ref this.mSelectedBackThreeName, value))
                {
                    if (value == "设为蓝屏")
                    {
                        SetBgBlueScreen();
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen();
                    }
                }
            }
        }

        private string mSelectedBackTwoName;

        public string SelectedBackTwoName
        {
            get => mSelectedBackTwoName;
            set
            {
                if (Set(ref this.mSelectedBackTwoName, value))
                {
                    if (value == "设为蓝屏")
                    {
                        SetBgBlueScreen();
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen();
                    }
                }
            }
        }

        private string mSelectedBackOneName;

        public string SelectedBackOneName
        {
            get => mSelectedBackOneName;
            set
            {
                if (Set(ref this.mSelectedBackOneName, value))
                {
                    if (value == "设为蓝屏")
                    {
                        SetBgBlueScreen();
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen();
                    }
                }
            }
        }


        #endregion

        #region 音频
        private string mSelectedOutFourAudioName;

        public string SelectedOutFourAudioName
        {
            get => mSelectedOutFourAudioName;
            set
            {
                if (Set(ref mSelectedOutFourAudioName, value))
                {
                    if (value == "模拟音频")
                    {
                        SetMoniAudioMode();
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode();
                    }
                }
            }
        }

        private string mSelectedOutThreeAudioName;

        public string SelectedOutThreeAudioName
        {
            get => mSelectedOutThreeAudioName;
            set
            {
                if (Set(ref mSelectedOutThreeAudioName, value))
                {
                    if (value == "模拟音频")
                    {
                        SetMoniAudioMode();
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode();
                    }
                }
            }
        }

        private string mSelectedOutTwoAudioName;

        public string SelectedOutTwoAudioName
        {
            get => mSelectedOutTwoAudioName;
            set
            {
                if (Set(ref mSelectedOutTwoAudioName, value))
                {
                    if (value == "模拟音频")
                    {
                        SetMoniAudioMode();
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode();
                    }
                }
            }
        }

        private string mSelectedOutOneAudioName;

        public string SelectedOutOneAudioName
        {
            get => mSelectedOutOneAudioName;
            set
            {
                if (Set(ref mSelectedOutOneAudioName, value))
                {
                    if (value == "模拟音频")
                    {
                        SetMoniAudioMode();
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode();
                    }
                }
            }
        }

        private string mSelectedInFourAudioName;

        public string SelectedInFourAudioName
        {
            get => mSelectedInFourAudioName;
            set
            {
                if (Set(ref mSelectedInFourAudioName, value))
                {
                    if (value == "模拟音频")
                    {
                        SetMoniAudioMode();
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode();
                    }
                }
            }
        }

        private string mSelectedInThreeAudioName;

        public string SelectedInThreeAudioName
        {
            get => mSelectedInThreeAudioName;
            set
            {
                if (Set(ref mSelectedInThreeAudioName, value))
                {
                    if (value == "模拟音频")
                    {
                        SetMoniAudioMode();
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode();
                    }
                }
            }
        }

        private string mSelectedTwoInAudioName;

        public string SelectedTwoInAudioName
        {
            get => mSelectedTwoInAudioName;
            set
            {
                if (Set(ref mSelectedTwoInAudioName, value))
                {
                    if (value == "模拟音频")
                    {
                        SetMoniAudioMode();
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode();
                    }
                }
            }
        }

        private string mSelectedOneInAudioName;
        public string SelectedOneInAudioName
        {
            get => mSelectedOneInAudioName;
            set
            {
                if (Set(ref mSelectedOneInAudioName, value))
                {
                    if (value == "模拟音频")
                    {
                        SetMoniAudioMode();
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode();
                    }
                }
            }
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

        public IList<BoardBingModel> SelectedEdidPlan { get; set; } = new ObservableCollection<BoardBingModel>();

        public IList<BoardBingModel> FilterInstructions { get; set; } = new ObservableCollection<BoardBingModel>();
        #endregion

        #region Command

        public ICommand SendColorAdjInstructionsCommand { get; set; }

        public ICommand SwitchOneCommand { get; set; }

        public ICommand SwitchTwoCommand { get; set; }

        public ICommand SwitchThreeCommand { get; set; }

        public ICommand SwitchFourCommand { get; set; }

        public ICommand FindInstructionListCommand { get; set; }

        public ICommand SendEdidInstructionsCommand { get; set; }

        public ICommand ScanAllEdidPlanCommand { get; set; }

        public ICommand AllPresetScanCommand { get; set; }

        public ICommand QueryNetSetCommand { get; set; }
        #endregion

        public NineOneMartixSettingViewModel(ClientSocket clientSocket, ScanBoardListRequest scanBoardListRequest)
        {
            this._socket = clientSocket;
            this._scanBoardListRequest = scanBoardListRequest;
            this.SendColorAdjInstructionsCommand = new RelayCommand(SendColorAdjInstructions);
            this.mInstructionsFilterDelay = new Helper.DelayTrigger();
            this.mInstructionsFilterDelay.OnExecute = filterInstruction;
            this.SwitchOneCommand=new RelayCommand<string>(SwitchOne);
            this.SwitchTwoCommand = new RelayCommand<string>(SwitchTwo);
            this.SwitchThreeCommand = new RelayCommand<string>(SwitchThree);
            this.SwitchFourCommand = new RelayCommand<string>(SwitchFour);
            this.FindInstructionListCommand = new RelayCommand(FindInstructionList);
            this.SendEdidInstructionsCommand = new RelayCommand(SendEdidInstructions);
            this.ScanAllEdidPlanCommand = new RelayCommand(ScanAllEdidPlan);
            this.AllPresetScanCommand = new RelayCommand(AllPresetScan);
            this.QueryNetSetCommand = new RelayCommand(QueryNetSet);

            this.AudioList.Add("模拟音频");
            this.AudioList.Add("嵌入音频");
            this.BackList.Add("设置蓝屏");
            this.BackList.Add("设置黑屏");
            this.TypeList.Add("HDMI");
            this.TypeList.Add("DVI");

            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出1" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出2" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出3" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出4" });

            #region MyRegion
            //#region 音频
            //if (this.SelectedOneInAudioName == null)
            //    this.SelectedOneInAudioName = this.AudioList.FirstOrDefault();
            //if (this.SelectedTwoInAudioName == null)
            //    this.SelectedTwoInAudioName = this.AudioList.FirstOrDefault();
            //if (this.SelectedInThreeAudioName == null)
            //    this.SelectedInThreeAudioName = this.AudioList.FirstOrDefault();
            //if (this.SelectedInFourAudioName == null)
            //    this.SelectedInFourAudioName = this.AudioList.FirstOrDefault();
            //if (this.SelectedOutOneAudioName == null)
            //    this.SelectedOutOneAudioName = this.AudioList.FirstOrDefault();
            //if (SelectedOutTwoAudioName == null)
            //    this.SelectedOutTwoAudioName = this.AudioList.FirstOrDefault();
            //if (SelectedOutThreeAudioName == null)
            //    this.SelectedOutThreeAudioName = this.AudioList.FirstOrDefault();
            //if (SelectedOutFourAudioName == null)
            //    this.SelectedOutFourAudioName = this.AudioList.FirstOrDefault();
            //#endregion
            //#region 背景
            //if (this.SelectedOutOneBack == null)
            //    this.SelectedOutOneBack = this.BackList.FirstOrDefault();
            //if (this.SelectedOutTwoBack == null)
            //    this.SelectedOutTwoBack = this.BackList.FirstOrDefault();
            //if (this.SelectedOutThreeBack == null)
            //    this.SelectedOutThreeBack = this.BackList.FirstOrDefault();
            //if (this.SelectedOutFourBack == null)
            //    this.SelectedOutFourBack = this.BackList.FirstOrDefault();
            //if (this.SelectedBackOneName == null)
            //    this.SelectedBackOneName = this.BackList.FirstOrDefault();
            //if (SelectedBackTwoName == null)
            //    this.SelectedBackTwoName = this.BackList.FirstOrDefault();
            //if (SelectedBackThreeName == null)
            //    this.SelectedBackThreeName = this.BackList.FirstOrDefault();
            //if (SelectedBackFourName == null)
            //    this.SelectedBackFourName = this.BackList.FirstOrDefault();
            //#endregion
            //#region 输出类型
            //if (this.SelectedOutOneType == null)
            //    this.SelectedOutOneType = this.TypeList.FirstOrDefault();
            //if (this.SelectedOutTwoType == null)
            //    this.SelectedOutTwoType = this.TypeList.FirstOrDefault();
            //if (this.SelectedOutThreeType == null)
            //    this.SelectedOutThreeType = this.TypeList.FirstOrDefault();
            //if (this.SelectedOutFourType == null)
            //    this.SelectedOutFourType = this.TypeList.FirstOrDefault();
            //#endregion
            #endregion

            initCommandList();
        }

        /// <summary>
        /// 获取网口信息
        /// </summary>
        private void QueryNetSet()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("<NetInfo>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("<NetInfo>", out sendError);
            }
        }

        #region 信号设置
        /// <summary>
        /// 设置DVI
        /// </summary>
        private void SetDvi()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("07C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("07C02.", out sendError);
            }
        }

        /// <summary>
        /// 设置HDMI
        /// </summary>
        private void SetHDMI()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("08C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("08C02.", out sendError);
            }
        }
        /// <summary>
        /// 设置黑屏
        /// </summary>
        private void SetBgBlackScreen()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("16C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("16C02.", out sendError);
            }
        }

        /// <summary>
        /// 设置蓝屏
        /// </summary>
        private void SetBgBlueScreen()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("15C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("15C02.", out sendError);
            }
        }


        /// <summary>
        /// 嵌入
        /// </summary>
        private void SetQianruAudioMode()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("03C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("03C02.", out sendError);
            }
        }

        /// <summary>
        /// 模拟
        /// </summary>
        private void SetMoniAudioMode()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("01C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("01C02.", out sendError);
            }
        }

        #endregion



        /// <summary>
        /// 查询所有预案
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void AllPresetScan()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("<PresetScan>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("<PresetScan>", out sendError);
            }
        }

        /// <summary>
        /// 查询所有EDID预案
        /// </summary>
        private void ScanAllEdidPlan()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("<EDIDScan>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("<EDIDScan>", out sendError);
            }
        }

        #region EDID
        private void SendEdidInstructions()
        {
            this.SelectedEdidPlan.Clear();
            var outname = string.Empty;
            string sendError = string.Empty;
            foreach(var item in InputEdid)
            {
                if (item.IsCheckInstructions == true)
                {
                    this.SelectedEdidPlan.Add(new BoardBingModel() {Instructions=item.Instructions,IsCheckInstructions=item.IsCheckInstructions });
                }
            }
            foreach(var outedid in OutputEdid)
            {
                if (outedid.IsCheckInstructions == true)
                {
                    outname = outedid.Instructions;
                }
            }
            foreach(var indeid in SelectedEdidPlan)
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort($"{outname}E{indeid.Instructions}.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction($"{outname}E{indeid.Instructions}.", out sendError);
                }
            }
 
        }

        #endregion

        #region 输入输出切换
        private void SwitchOne(string name)
        {
            string sendError = string.Empty;
            if (name == "输出1")
            {
                //xVy.
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("1V1.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("1V1.", out sendError);
                }
            }
            else if (name == "输出2")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("1V2.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("1V2.", out sendError);
                }
            }
            else if (name == "输出3")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("1V3.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("1V3.", out sendError);
                }
            }
            else if (name == "输出4")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("1V4.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("1V4.", out sendError);
                }
            }
        }

        private void SwitchTwo(string name)
        {
            string sendError = string.Empty;
            if (name == "输出1")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("2V1.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("2V1.", out sendError);
                }
            }
            else if (name == "输出2")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("2V2.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("2V2.", out sendError);
                }
            }
            else if (name == "输出3")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("2V3.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("2V3.", out sendError);
                }
            }
            else if (name == "输出4")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("2V4.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("2V4.", out sendError);
                }
            }
        }


        private void SwitchThree(string name)
        {
            string sendError = string.Empty;
            if (name == "输出1")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("3V1.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("3V1.", out sendError);
                }
            }
            else if (name == "输出2")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("3V2.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("3V2.", out sendError);
                }
            }
            else if (name == "输出3")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("3V3.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("3V3.", out sendError);
                }
            }
            else if (name == "输出4")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("3V4.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("3V4.", out sendError);
                }
            }
        }


        private void SwitchFour(string name)
        {
            string sendError = string.Empty;
            if (name == "输出1")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("4V1.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("4V1.", out sendError);
                }
            }
            else if (name == "输出2")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("4V2.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("4V2.", out sendError);
                }
            }
            else if (name == "输出3")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("4V3.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("4V3.", out sendError);
                }
            }
            else if (name == "输出4")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("4V4.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("4V4.", out sendError);
                }
            }
        }

        #endregion


        private void filterInstruction(string obj)
        {
            App.RunInUIThread(() => this.FindInstructionList());
        }

        private void FindInstructionList()
        {
            if (string.IsNullOrEmpty(this.FindInstructionsStr))
            {
                this.initCommandList();
            }
            if (!string.IsNullOrEmpty(this.FindInstructionsStr))
            {
                foreach (var item in this.NineOneColorAdj)
                {
                    if (item.Instructions.Contains(this.FindInstructionsStr))
                        this.FilterInstructions.Add(new BoardBingModel() { Instructions = item.Instructions, IsCheckInstructions = item.IsCheckInstructions, Code = item.Instructions });
                }
                this.NineOneColorAdj.Clear();
                foreach (var filter in this.FilterInstructions)
                {
                    this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = filter.Instructions, IsCheckInstructions = filter.IsCheckInstructions, Code = filter.Instructions });
                }
            }
        }

        /// <summary>
        /// 加载指令
        /// </summary>
        private void initCommandList()
        {
            this.NineOneColorAdj.Clear();
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "设置背景为蓝屏，重启有效", IsCheckInstructions = false, Code = "15C02." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "设置背景为黑屏，重启有效", IsCheckInstructions = false, Code = "16C02." });

            this.InputEdid.Clear();
            this.InputEdid.Add(new BoardBingModel() { Instructions = "1", IsCheckInstructions = false });
            this.InputEdid.Add(new BoardBingModel() { Instructions = "2", IsCheckInstructions = false });
            this.InputEdid.Add(new BoardBingModel() { Instructions = "3", IsCheckInstructions = false });
            this.InputEdid.Add(new BoardBingModel() { Instructions = "4", IsCheckInstructions = false });

            this.OutputEdid.Clear();
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
