using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatrixCommandTool.Model.BindingModel;
using MatrixCommandTool.Net.DelegateModel;
using MatrixCommandTool.Net.TCP;
using MatrixCommandTool.Net.TCP.Request;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

        private int mPlanIndex;
        public int PlanIndex
        {
            get => this.mPlanIndex;
            set => Set(ref this.mPlanIndex, value);
        }

        private int mSavePlanToIndex;
        /// <summary>
        /// 保存到序号n
        /// </summary>
        public int SavePlanToIndex
        {
            get => this.mSavePlanToIndex;
            set => Set(ref this.mSavePlanToIndex, value);
        }

        private string mPlanName;
        public string PlanName
        {
            get => this.mPlanName;
            set => Set(ref this.mPlanName, value);
        }

        private int mModifyPlanIndex;
        public int ModifyPlanIndex
        {
            get => this.mModifyPlanIndex;
            set => Set(ref this.mModifyPlanIndex, value);
        }

        private string mNewPlanName;
        public string NewPlanName
        {
            get => this.mNewPlanName;
            set => Set(ref this.mNewPlanName, value);
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

        private bool mIsOpenBuzzer;
        /// <summary>
        /// 蜂鸣器开关
        /// </summary>
        public bool IsOpenBuzzer
        {
            get => this.mIsOpenBuzzer;
            set => Set(ref this.mIsOpenBuzzer, value);
        }

        private string mSetIpAddr;
        public string SetIpAddr
        {
            get => this.mSetIpAddr;
            set => Set(ref this.mSetIpAddr, value);
        }
        private string mSetPort;
        public string SetPort
        {
            get => this.mSetPort;
            set => Set(ref this.mSetPort, value);
        }
        private string mSetNetMask;
        public string SetNetMask
        {
            get => this.mSetNetMask;
            set => Set(ref this.mSetNetMask, value);
        }
        private string mSetGateway;
        public string SetGateway
        {
            get => this.mSetGateway;
            set => Set(ref this.mSetGateway, value);
        }
        #region 通道

        #endregion

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
                        SetHDMI(4);
                    }
                    else if (value == "DVI")
                    {
                        SetDvi(4);
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
                        SetHDMI(3);
                    }
                    else if (value == "DVI")
                    {
                        SetDvi(3);
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
                        SetHDMI(2);
                    }
                    else if (value == "DVI")
                    {
                        SetDvi(2);
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
                        SetHDMI(1);
                    }
                    else if (value == "DVI")
                    {
                        SetDvi(1);
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
                        SetOutBgBlueScreen(4);
                    }
                    else if (value == "设为黑屏")
                    {
                        SetOutBgBlackScreen(4);
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
                        SetOutBgBlueScreen(3);
                    }
                    else if (value == "设为黑屏")
                    {
                        SetOutBgBlackScreen(3);
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
                        SetOutBgBlueScreen(2);
                    }
                    else if (value == "设为黑屏")
                    {
                        SetOutBgBlackScreen(2);
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
                        SetOutBgBlueScreen(1);
                    }
                    else if (value == "设为黑屏")
                    {
                        SetOutBgBlackScreen(1);
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
                        SetBgBlueScreen(4);
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen(4);
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
                        SetBgBlueScreen(3);
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen(3);
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
                        SetBgBlueScreen(2);
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen(2);
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
                        SetBgBlueScreen(1);
                    }
                    else if (value == "设为黑屏")
                    {
                        SetBgBlackScreen(1);
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
                        SetMoniOutAudioMode(4);
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruOutAudioMode(4);
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
                        SetMoniOutAudioMode(3);
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruOutAudioMode(3);
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
                        SetMoniOutAudioMode(2);
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruOutAudioMode(2);
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
                        SetMoniOutAudioMode(1);
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruOutAudioMode(1);
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
                        SetMoniAudioMode(4);
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode(4);
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
                        SetMoniAudioMode(3);
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode(3);
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
                        SetMoniAudioMode(2);
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode(2);
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
                        SetMoniAudioMode(1);
                    }
                    else if (value == "嵌入音频")
                    {
                        SetQianruAudioMode(1);
                    }
                }
            }
        }

        private string mTips;
        /// <summary>
        /// 提示
        /// </summary>
        public string Tips
        {
            get => this.mTips;
            set => Set(ref this.mTips, value);
        }
        #endregion

        /// <summary>
        /// 分页显示
        /// </summary>
        public IList<BoardBingModel> PagesSource { get; set; } = new ObservableCollection<BoardBingModel>();

        public IList<string> DeviceSwitch { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// 色调调整指令
        /// </summary>
        public IList<BoardBingModel> NineOneColorAdj { get; set; } = new ObservableCollection<BoardBingModel>();

        /// <summary>
        /// 输出通道列表
        /// </summary>
        public IList<BoardBingModel> OutPutChannels { get; set; } = new ObservableCollection<BoardBingModel>();

        /// <summary>
        /// 色调调整输出列表
        /// </summary>
        public IList<BoardBingModel> OutPutColorChannels { get; set; } = new ObservableCollection<BoardBingModel>();

        /// <summary>
        /// 输入通道列表
        /// </summary>
        public IList<BoardBingModel> InPutChannels { get; set; } = new ObservableCollection<BoardBingModel>();

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
        public IList<BoardBingModel> EDIDPlanList { get; set; } = new ObservableCollection<BoardBingModel>();

        public IList<BoardBingModel> NetInfoList { get; set; } = new ObservableCollection<BoardBingModel>();
        /// <summary>
        /// 获取到的所有预案列表
        /// </summary>
        public IList<BoardBingModel> ScenePlanList { get; set; } = new ObservableCollection<BoardBingModel>();

        public IList<BoardBingModel> ColorSettingsList { get; set; } = new ObservableCollection<BoardBingModel>();

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

        public ICommand SwitchBuzzerCommand { get; set; }

        public ICommand ModifyPlanNameCommand { get; set; }

        public ICommand ChoosePlanCommand { get; set; }

        public ICommand SavePlanAsIndexCommand { get; set; }

        public ICommand ModifyIPCommand { get; set; }

        public ICommand ModifyPortCommand { get; set; }

        public ICommand ModifyNetmaskCommand { get; set; }

        public ICommand ModifyGatewayCommand { get; set; }

        public ICommand ResetAllSetingCommand { get; set; }

        public ICommand ResetNetSetCommand { get; set; }

        public ICommand GetColorInfoCommand { get; set; }

        public ICommand ResetColorInfoCommand { get; set; }
        #endregion

        public NineOneMartixSettingViewModel(ClientSocket clientSocket, ScanBoardListRequest scanBoardListRequest)
        {
            this._socket = clientSocket;
            this._scanBoardListRequest = scanBoardListRequest;
            this.SendColorAdjInstructionsCommand = new RelayCommand(SendColorAdjInstructions);
            this.mInstructionsFilterDelay = new Helper.DelayTrigger();
            this.mInstructionsFilterDelay.OnExecute = filterInstruction;
            this.SwitchOneCommand = new RelayCommand<string>(SwitchOne);
            this.SwitchTwoCommand = new RelayCommand<string>(SwitchTwo);
            this.SwitchThreeCommand = new RelayCommand<string>(SwitchThree);
            this.SwitchFourCommand = new RelayCommand<string>(SwitchFour);
            this.FindInstructionListCommand = new RelayCommand(FindInstructionList);
            this.SendEdidInstructionsCommand = new RelayCommand(SendEdidInstructions);
            this.ScanAllEdidPlanCommand = new RelayCommand(ScanAllEdidPlan);
            this.AllPresetScanCommand = new RelayCommand(AllPresetScan);
            this.QueryNetSetCommand = new RelayCommand(QueryNetSet);

            this.SwitchBuzzerCommand = new RelayCommand(SwitchBuzzer);
            this.ModifyPlanNameCommand = new RelayCommand(ModifyPlanName);
            this.ChoosePlanCommand = new RelayCommand(ChoosePlan);
            this.SavePlanAsIndexCommand = new RelayCommand(SavePlanAsIndex);

            this.ModifyIPCommand = new RelayCommand(ModifyIP);
            this.ModifyPortCommand = new RelayCommand(ModifyPort);
            this.ModifyNetmaskCommand = new RelayCommand(ModifyNetmask);
            this.ModifyGatewayCommand = new RelayCommand(ModifyGateway);
            this.ResetAllSetingCommand = new RelayCommand(ResetAllSeting);
            this.ResetNetSetCommand = new RelayCommand(ResetNetSet);

            this.GetColorInfoCommand = new RelayCommand(GetColorInfo);
            this.ResetColorInfoCommand = new RelayCommand(ResetColorInfo);

            this.AudioList.Add("模拟音频");
            this.AudioList.Add("嵌入音频");
            this.BackList.Add("设为蓝屏");
            this.BackList.Add("设为黑屏");
            this.TypeList.Add("HDMI");
            this.TypeList.Add("DVI");

            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出1" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出2" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出3" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输出4" });

            #region MyRegion
            //#region 音频
            //if (this.SelectedOneInAudioName == null)
            this.SelectedOneInAudioName = null;
            //if (this.SelectedTwoInAudioName == null)
            this.SelectedTwoInAudioName = null;
            //if (this.SelectedInThreeAudioName == null)
            this.SelectedInThreeAudioName = null;
            //if (this.SelectedInFourAudioName == null)
            this.SelectedInFourAudioName = null;
            //if (this.SelectedOutOneAudioName == null)
            this.SelectedOutOneAudioName = null;
            //if (SelectedOutTwoAudioName == null)
            this.SelectedOutTwoAudioName = null;
            //if (SelectedOutThreeAudioName == null)
            this.SelectedOutThreeAudioName = null;
            //if (SelectedOutFourAudioName == null)
            this.SelectedOutFourAudioName = null;
            //#endregion
            //#region 背景
            //if (this.SelectedOutOneBack == null)
            this.SelectedOutOneBack = null;
            //if (this.SelectedOutTwoBack == null)
            this.SelectedOutTwoBack = null;
            //if (this.SelectedOutThreeBack == null)
            this.SelectedOutThreeBack = null;
            //if (this.SelectedOutFourBack == null)
            this.SelectedOutFourBack = null;
            //if (this.SelectedBackOneName == null)
            this.SelectedBackOneName = null;
            //if (SelectedBackTwoName == null)
            this.SelectedBackTwoName = null;
            //if (SelectedBackThreeName == null)
            this.SelectedBackThreeName = null;
            //if (SelectedBackFourName == null)
            this.SelectedBackFourName = null;
            //#endregion
            //#region 输出类型
            //if (this.SelectedOutOneType == null)
            this.SelectedOutOneType = null;
            //if (this.SelectedOutTwoType == null)
            this.SelectedOutTwoType = null;
            //if (this.SelectedOutThreeType == null)
            this.SelectedOutThreeType = null;
            //if (this.SelectedOutFourType == null)
            this.SelectedOutFourType = null;
            //#endregion
            #endregion

            initCommandList();
            GlobalContext.Current.CurrentVMLocator.MainVM.Serial.DataReceived += Serial_DataReceived;
            this._socket.NotifyFactory.ReceiveMessageChangedEvent += NotifyFactory_ReceiveMessageChangedEvent;
        }
        string mes = string.Empty;
        string edidmes = string.Empty;
        string colormes = string.Empty;
        /// <summary>
        /// 网口消息接收
        /// </summary>
        /// <param name="notify"></param>
        private void NotifyFactory_ReceiveMessageChangedEvent(ReceiveMessageNotify notify)
        {
            if (notify.Message == null)
                return;
            Console.WriteLine(notify.Message);
            if (notify.Message.IndexOf("EDID Scan") > -1)
            {
                edidmes += notify.Message;
                App.RunInUIThread(() =>
                {
                    this.EDIDPlanList.Clear();
                    this.EDIDPlanList.Add(new BoardBingModel() { EDEDIDInfo = notify.Message });
                }, true);
            }
            if (notify.Message.IndexOf("01-") > -1)
            {
                App.RunInUIThread(() =>
                {
                    this.ScenePlanList.Clear();
                    this.ScenePlanList.Add(new BoardBingModel() { PlanInfo = notify.Message });
                }, true);
            }
            if (notify.Message.IndexOf("Network Information") > -1)
            {
                mes+= notify.Message;
                    App.RunInUIThread(() =>
                    {
                        this.NetInfoList.Clear();
                        this.NetInfoList.Add(new BoardBingModel() { NetInfo= notify.Message });
                        mes = string.Empty;
                    }, true);
                
          
            }
            if (notify.Message.IndexOf(" CHN  | Brightness | Contrast | Saturability |  HUE") > -1)
            {
                colormes += notify.Message;
                App.RunInUIThread(() =>
                {
                    this.ColorSettingsList.Clear();
                    this.ColorSettingsList.Add(new BoardBingModel() { ColorSettings = notify.Message });
                    colormes = string.Empty;
                }, true);
            }
        }

        #region 色彩参数
        private void GetColorInfo()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("<Color>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("<Color>", out sendError);
            }
        }

        private void ResetColorInfo()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("<ColorReset>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("<ColorReset>", out sendError);
            }
        }
        #endregion


        #region 系统设置
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

        private void ModifyGateway()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"<SetGW-{SetGateway}>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"<SetGW-{SetGateway}>", out sendError);
            }
        }

        private void ModifyNetmask()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"<SetSUB-{SetNetMask}>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"<SetSUB-{SetNetMask}>", out sendError);
            }
        }

        private void ModifyPort()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"<SetPort-{SetPort}>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"<SetPort-{SetPort}>", out sendError);
            }
        }

        private void ModifyIP()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"<SetIP-{SetIpAddr}>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"<SetIP-{SetIpAddr}>", out sendError);
            }
        }

        private void ResetNetSet()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("<NetReset>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("<NetReset>", out sendError);
            }
        }

        private void ResetAllSeting()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("<Reset>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("<Reset>", out sendError);
            }
        }
        #endregion

        #region 预案场景
        /// <summary>
        /// 修改预案名称
        /// </summary>
        private void ModifyPlanName()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"<PresetName-{ModifyPlanIndex}-{NewPlanName}>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"<PresetName-{ModifyPlanIndex}-{NewPlanName}>", out sendError);
            }
        }

        /// <summary>
        /// 选择场景
        /// </summary>
        private void ChoosePlan()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"<PresetSet-{PlanIndex}>");

            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"<PresetSet-{PlanIndex}>", out sendError);
            }
        }

        /// <summary>
        /// 保存场景到序号n
        /// </summary>
        private void SavePlanAsIndex()
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"<PresetSave-{SavePlanToIndex}>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"<PresetSave-{SavePlanToIndex}>", out sendError);
            }
        }
        #endregion

        string oldStr = string.Empty;
        private void Serial_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            App.RunInUIThread(() =>
            {
                string str = GlobalContext.Current.CurrentVMLocator.MainVM.Serial.ReadExisting(); //以字符串方式读
                if (String.IsNullOrEmpty(str) || str.Equals("<Set Succeed!>\r\n"))
                    return;
                Console.Write(str);//"/* EDID Scan */\n1-EDID_1080P\n2-EDID_4K\n3-Default1\n4-Default2\n5-LEARN_CH1\n6-LEARN_CH2\n7-LEARN_CH3\n8-LEARN_CH4\n"
                if (str.IndexOf("EDID Scan") > -1)
                {
                    this.EDIDPlanList.Clear();
                    this.EDIDPlanList.Add(new BoardBingModel() { EDEDIDInfo = str });
                }
                if (str.IndexOf("01-") > -1)
                {
                    this.ScenePlanList.Clear();
                    this.ScenePlanList.Add(new BoardBingModel() { PlanInfo = str });
                }
                if (str.IndexOf("Network Information") > -1)
                {
                    this.NetInfoList.Clear();
                    this.NetInfoList.Add(new BoardBingModel() { NetInfo = str });
                }
                if (str.IndexOf(" CHN  | Brightness | Contrast | Saturability |  HUE") > -1)
                {
                    this.ColorSettingsList.Clear();
                    this.ColorSettingsList.Add(new BoardBingModel() { ColorSettings = str });
                }
            }, true);
        }

        /// <summary>
        /// 开启关闭蜂鸣器
        /// </summary>
        private void SwitchBuzzer()
        {
            string sendError = string.Empty;
            if (this.IsOpenBuzzer == true)
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("<BellOn>");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("<BellOn>", out sendError);
                }
            }
            else if (this.IsOpenBuzzer == false)
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("<BellOff>");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("<BellOff>", out sendError);
                }
            }
        }

        #region 信号设置
        /// <summary>
        /// 设置DVI
        /// </summary>
        private void SetDvi(int channelNum)
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{channelNum}SETOUT.07C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{channelNum}SETOUT.07C02.", out sendError);
            }
        }

        /// <summary>
        /// 设置HDMI
        /// </summary>
        private void SetHDMI(int channelNum)
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{channelNum}SETOUT.08C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{channelNum}SETOUT.08C02.", out sendError);
            }
        }
        /// <summary>
        /// 设置黑屏
        /// </summary>
        private void SetBgBlackScreen(int channelNum)
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{channelNum}SETIN.16C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{channelNum}SETIN.16C02.", out sendError);
            }
        }

        /// <summary>
        /// 设置蓝屏
        /// </summary>
        private void SetBgBlueScreen(int channelNum)
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{channelNum}SETIN.15C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{channelNum}SETIN.15C02.", out sendError);
            }
        }

        /// <summary>
        /// 设置输出黑屏
        /// </summary>
        private void SetOutBgBlackScreen(int channelNum)
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{channelNum}SETOUT.16C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{channelNum}SETOUT.16C02.", out sendError);
            }
        }

        /// <summary>
        /// 设置输出蓝屏
        /// </summary>
        private void SetOutBgBlueScreen(int channelNum)
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{channelNum}SETOUT.15C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{channelNum}SETOUT.15C02.", out sendError);
            }
        }

        /// <summary>
        /// 输入嵌入
        /// </summary>
        private void SetQianruAudioMode(int channelNum)
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{channelNum}SETIN.03C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{channelNum}SETIN.03C02.", out sendError);
            }
        }

        /// <summary>
        ///输入 模拟
        /// </summary>
        private void SetMoniAudioMode(int channelNum)
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{channelNum}SETIN.01C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{channelNum}SETIN.01C02.", out sendError);
            }
        }

        /// <summary>
        /// 输出嵌入
        /// </summary>
        private void SetQianruOutAudioMode(int channelNum)
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{channelNum}SETOUT.03C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{channelNum}SETIN.03C02.", out sendError);
            }
        }

        /// <summary>
        /// 输出模拟
        /// </summary>
        private void SetMoniOutAudioMode(int channelNum)
        {
            string sendError = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{channelNum}SETOUT.01C02.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{channelNum}SETIN.01C02.", out sendError);
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
            foreach (var item in InputEdid)
            {
                if (item.IsCheckInstructions == true)
                {
                    this.SelectedEdidPlan.Add(new BoardBingModel() { Instructions = item.Instructions, IsCheckInstructions = item.IsCheckInstructions });
                }
            }
            foreach (var outedid in OutputEdid)
            {
                if (outedid.IsCheckInstructions == true)
                {
                    outname = outedid.Instructions;
                }
            }
            foreach (var indeid in SelectedEdidPlan)
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
            this.OutPutChannels.Clear();
            this.OutPutChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 1 });
            this.OutPutChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 2 });
            this.OutPutChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 3 });
            this.OutPutChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 4 });

            this.InPutChannels.Clear();
            this.InPutChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 1 });
            this.InPutChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 2 });
            this.InPutChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 3 });
            this.InPutChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 4 });

            this.OutPutColorChannels.Clear();
            this.OutPutColorChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 1 });
            this.OutPutColorChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 2 });
            this.OutPutColorChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 3 });
            this.OutPutColorChannels.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = 4 });

            this.NineOneColorAdj.Clear();
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "设置背景为蓝屏，重启有效", IsCheckInstructions = false, Code = "15C02." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "设置背景为黑屏，重启有效", IsCheckInstructions = false, Code = "16C02." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "关闭背景颜色，无信号输入时关闭输出，重启有效", IsCheckInstructions = false, Code = "17C02." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1024 X 768@60Hz ", IsCheckInstructions = false, Code = "01C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@60Hz ", IsCheckInstructions = false, Code = "02C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 1024@60Hz ", IsCheckInstructions = false, Code = "03C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1366 X 768@60Hz ", IsCheckInstructions = false, Code = "04C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1400 X 1050@60Hz ", IsCheckInstructions = false, Code = "05C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1440 X 900@60Hz", IsCheckInstructions = false, Code = "07C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@60Hz", IsCheckInstructions = false, Code = "08C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1680 X 1050@60Hz", IsCheckInstructions = false, Code = "09C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@50Hz", IsCheckInstructions = false, Code = "17C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@60Hz", IsCheckInstructions = false, Code = "11C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1200@60Hz", IsCheckInstructions = false, Code = "12C08." });

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
            string sendcode = string.Empty;
            this.SendColorAdjInstructionsList.Clear();
            foreach (var item in this.NineOneColorAdj)
            {
                if (item.IsCheckInstructions == true)
                {
                    if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                    {
                        foreach (var channels in this.OutPutChannels)
                        {
                            if (channels.IsCheckChannel == true)
                            {
                                sendcode = $"{channels.OnputChannel}SETOUT.";
                                this.SendDataBySerialPort($"{sendcode}{item.Code}");
                            }
                        }
                    }
                    else
                    {
                        foreach (var channels in this.OutPutChannels)
                        {
                            if (channels.IsCheckChannel == true)
                            {
                                sendcode = $"{channels.OnputChannel}SETOUT.";
                                this._scanBoardListRequest.SendIndtruction($"{sendcode}{item.Code}", out sendError);
                            }
                        }
                       
                    }
                    this.SendColorAdjInstructionsList.Add(new BoardBingModel() { Instructions = item.Instructions, Code = $"{sendcode}{item.Code}" });
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
                Console.WriteLine(message);
                GlobalContext.Current.CurrentVMLocator.MainVM.Serial.Write(sendData, 0, sendData.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// 设置色调
        /// </summary>
        /// <param name="value"></param>
        public void SendColorDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this.SendDataBySerialPort($"{item.OnputChannel}SETOUT.{code}C24.");
                    }
                }
                //this.SendDataBySerialPort($"{code}C24.");
            }
            else
            {
                string sendError = string.Empty;
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this._scanBoardListRequest.SendIndtruction($"{item.OnputChannel}SETOUT.{code}C24.", out sendError);
                    }
                }
            }
        }

        /// <summary>
        /// 设置对比度
        /// </summary>
        /// <param name="value"></param>
        public void SendContrastDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this.SendDataBySerialPort($"{item.OnputChannel}SETOUT.{code}C22.");
                    }
                }
                // this.SendDataBySerialPort($"{code}C22.");
            }
            else
            {

                string sendError = string.Empty;
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this._scanBoardListRequest.SendIndtruction($"{item.OnputChannel}SETOUT.{code}C22.", out sendError);
                    }
                }


            }
        }

        /// <summary>
        /// 设置饱和度
        /// </summary>
        /// <param name="value"></param>
        public void SendSaturationDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this.SendDataBySerialPort($"{item.OnputChannel}SETOUT.{code}C23.");
                    }
                }
            }
            else
            {

                string sendError = string.Empty;
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this._scanBoardListRequest.SendIndtruction($"{item.OnputChannel}SETOUT.{code}C23.", out sendError);
                    }
                }

            }
        }

        /// <summary>
        /// 设置R值
        /// </summary>
        /// <param name="value"></param>
        public void SendRVauleDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this.SendDataBySerialPort($"{item.OnputChannel}SETOUT.{code}C26.");
                    }
                }
            }
            else
            {

                string sendError = string.Empty;
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this._scanBoardListRequest.SendIndtruction($"{item.OnputChannel}SETOUT.{code}C26.", out sendError);
                    }
                }

            }
        }

        /// <summary>
        /// 设置G值
        /// </summary>
        /// <param name="value"></param>
        public void SendGVauleDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this.SendDataBySerialPort($"{item.OnputChannel}SETOUT.{code}C27.");
                    }
                }
            }
            else
            {

                string sendError = string.Empty;
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this._scanBoardListRequest.SendIndtruction($"{item.OnputChannel}SETOUT.{code}C27.", out sendError);
                    }
                }

            }
        }

        /// <summary>
        /// 设置B值
        /// </summary>
        /// <param name="value"></param>
        public void SendBVauleDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this.SendDataBySerialPort($"{item.OnputChannel}SETOUT.{code}C28.");
                    }
                }
            }
            else
            {

                string sendError = string.Empty;
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this._scanBoardListRequest.SendIndtruction($"{item.OnputChannel}SETOUT.{code}C28.", out sendError);
                    }
                }

            }
        }

        /// <summary>
        /// 设置亮度
        /// </summary>
        /// <param name="value"></param>
        public void SendBrightnessDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this.SendDataBySerialPort($"{item.OnputChannel}SETOUT.{code}C21.");
                    }
                }
            }
            else
            {
                string sendError = string.Empty;
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this._scanBoardListRequest.SendIndtruction($"{item.OnputChannel}SETOUT.{code}C21.", out sendError);
                    }
                }
            }
        }
    }
}
