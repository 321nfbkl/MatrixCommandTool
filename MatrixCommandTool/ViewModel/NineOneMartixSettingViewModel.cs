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
using System.IO.Packaging;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Input;

namespace MatrixCommandTool.ViewModel
{
    public class NineOneMartixSettingViewModel : ViewModelBase
    {
        public ScanBoardListRequest _scanBoardListRequest;
        public ClientSocket _socket;
        /// <summary>
        /// System.Threading.Timer 实例，
        /// 最推荐使用的定时器
        /// </summary>
        private System.Threading.Timer mThreadingTimer;


        #region Properities
        private Visibility mIsOverAllMsg=Visibility.Collapsed;
        public Visibility IsOverAllMsg
        {
            get => this.mIsOverAllMsg;
            set => Set(ref this.mIsOverAllMsg, value);
        }

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

        private string mSendInstructionStr;
        /// <summary>
        /// 发送的指令
        /// </summary>
        public string SendInstructionStr
        {
            get => this.mSendInstructionStr;
            set => Set(ref this.mSendInstructionStr, value);
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

        private bool mIsAlreadySetOuputFour;
        public bool IsAlreadySetOuputFour
        {
            get => this.mIsAlreadySetOuputFour;
            set => Set(ref this.mIsAlreadySetOuputFour, value);
        }

        private bool mIsOpenBuzzer=false;
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
                    //if (value == "HDMI")
                    //{
                    //    SetHDMI(4);
                    //}
                    //else if (value == "DVI")
                    //{
                    //    SetDvi(4);
                    //}
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
                    //if (value == "HDMI")
                    //{
                    //    SetHDMI(3);
                    //}
                    //else if (value == "DVI")
                    //{
                    //    SetDvi(3);
                    //}
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
                    //if (value == "HDMI")
                    //{
                    //    SetHDMI(2);
                    //}
                    //else if (value == "DVI")
                    //{
                    //    SetDvi(2);
                    //}
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
                    //if (value == "HDMI")
                    //{
                    //    SetHDMI(1);
                    //}
                    //else if (value == "DVI")
                    //{
                    //    SetDvi(1);
                    //}
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
                    //if (value == "设为蓝屏")
                    //{
                    //    SetOutBgBlueScreen(4);
                    //}
                    //else if (value == "设为黑屏")
                    //{
                    //    SetOutBgBlackScreen(4);
                    //}
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
                    //if (value == "设为蓝屏")
                    //{
                    //    SetOutBgBlueScreen(3);
                    //}
                    //else if (value == "设为黑屏")
                    //{
                    //    SetOutBgBlackScreen(3);
                    //}
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
                    //if (value == "设为蓝屏")
                    //{
                    //    SetOutBgBlueScreen(2);
                    //}
                    //else if (value == "设为黑屏")
                    //{
                    //    SetOutBgBlackScreen(2);
                    //}
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
                    //if (value == "设为蓝屏")
                    //{
                    //    SetOutBgBlueScreen(1);
                    //}
                    //else if (value == "设为黑屏")
                    //{
                    //    SetOutBgBlackScreen(1);
                    //}
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
                    //if (value == "设为蓝屏")
                    //{
                    //    SetBgBlueScreen(4);
                    //}
                    //else if (value == "设为黑屏")
                    //{
                    //    SetBgBlackScreen(4);
                    //}
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
                    //if (value == "设为蓝屏")
                    //{
                    //    SetBgBlueScreen(3);
                    //}
                    //else if (value == "设为黑屏")
                    //{
                    //    SetBgBlackScreen(3);
                    //}
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
                    //if (value == "设为蓝屏")
                    //{
                    //    SetBgBlueScreen(2);
                    //}
                    //else if (value == "设为黑屏")
                    //{
                    //    SetBgBlackScreen(2);
                    //}
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
                    //if (value == "模拟音频")
                    //{
                    //    SetMoniOutAudioMode(4);
                    //}
                    //else if (value == "嵌入音频")
                    //{
                    //    SetQianruOutAudioMode(4);
                    //}
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
                    //if (value == "模拟音频")
                    //{
                    //    SetMoniOutAudioMode(3);
                    //}
                    //else if (value == "嵌入音频")
                    //{
                    //    SetQianruOutAudioMode(3);
                    //}
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
                    //if (value == "模拟音频")
                    //{
                    //    SetMoniOutAudioMode(2);
                    //}
                    //else if (value == "嵌入音频")
                    //{
                    //    SetQianruOutAudioMode(2);
                    //}
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
                    //if (value == "模拟音频")
                    //{
                    //    SetMoniOutAudioMode(1);
                    //}
                    //else if (value == "嵌入音频")
                    //{
                    //    SetQianruOutAudioMode(1);
                    //}
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
                    //if (value == "模拟音频")
                    //{
                    //    SetMoniAudioMode(4);
                    //}
                    //else if (value == "嵌入音频")
                    //{
                    //    SetQianruAudioMode(4);
                    //}
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
                    //if (value == "模拟音频")
                    //{
                    //    SetMoniAudioMode(3);
                    //}
                    //else if (value == "嵌入音频")
                    //{
                    //    SetQianruAudioMode(3);
                    //}
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
                    //if (value == "模拟音频")
                    //{
                    //    SetMoniAudioMode(2);
                    //}
                    //else if (value == "嵌入音频")
                    //{
                    //    SetQianruAudioMode(2);
                    //}
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
                    //if (value == "模拟音频")
                    //{
                    //    SetMoniAudioMode(1);
                    //}
                    //else if (value == "嵌入音频")
                    //{
                    //    SetQianruAudioMode(1);
                    //}
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

        #region 分辨率
        private string mSelectedOutOneResolution;
        public string SelectedOutOneResolution
        {
            get => this.mSelectedOutOneResolution;
            set
            {
                if (Set(ref this.mSelectedOutOneResolution, value))
                {
                    //SendResolution(1, value);
                }
            }
        }



        private string mSelectedOutTwoResolution;
        public string SelectedOutTwoResolution
        {
            get => this.mSelectedOutTwoResolution;
            set
            {
                if (Set(ref this.mSelectedOutTwoResolution, value))
                {
                    //SendResolution(2, value);
                }
            }
        }

        private string mSelectedOutThreeResolution;
        public string SelectedOutThreeResolution
        {
            get => this.mSelectedOutThreeResolution;
            set
            {
                if (Set(ref this.mSelectedOutThreeResolution, value))
                {
                   // SendResolution(3, value);
                }
            }
        }

        private string mSelectedOutFourResolution;
        public string SelectedOutFourResolution
        {
            get => this.mSelectedOutFourResolution;
            set
            {
                if (Set(ref this.mSelectedOutFourResolution, value))
                {
                    //SendResolution(4, value);
                }
            }
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

        public IList<BoardBingModel> ChannelsList { get; set; } = new ObservableCollection<BoardBingModel>();   

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
        /// 分辨率列表
        /// </summary>
        public IList<string> ResolutionList { get; set; } = new ObservableCollection<string>();

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

        public IList<BoardBingModel> ChannelStatus { get; set; }=new ObservableCollection<BoardBingModel>();
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

        public ICommand QuerySwitchChannelCommand { get; set; }

        public ICommand QueryPortScanCommand { get; set; }

        public ICommand QueryAudioCommand { get; set; }
        #endregion

        public NineOneMartixSettingViewModel(ClientSocket clientSocket, ScanBoardListRequest scanBoardListRequest)
        {
            this._socket = clientSocket;
            this._scanBoardListRequest = scanBoardListRequest;
            this.SendColorAdjInstructionsCommand = new RelayCommand(SendColorAdjInstructions);
            this.mInstructionsFilterDelay = new Helper.DelayTrigger();
            this.mInstructionsFilterDelay.OnExecute = filterInstruction;
            this.SwitchOneCommand = new RelayCommand<BoardBingModel>(SwitchOne);
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

            this.QuerySwitchChannelCommand = new RelayCommand(QuerySwitchChannel);
            this.QueryPortScanCommand = new RelayCommand(QueryPortScan);
            this.QueryAudioCommand = new RelayCommand(QueryAudio);

            this.AudioList.Add("模拟音频");
            this.AudioList.Add("嵌入音频");
            this.BackList.Add("设为蓝屏");
            this.BackList.Add("设为黑屏");
            this.TypeList.Add("HDMI");
            this.TypeList.Add("DVI");

            this.ResolutionList.Add("1024x768p60");
            this.ResolutionList.Add("1280x720p50");
            this.ResolutionList.Add("1280x720p60");
            this.ResolutionList.Add("1280x1024p60");
            this.ResolutionList.Add("1366x768p60");
            this.ResolutionList.Add("1400x1050p60");
            this.ResolutionList.Add("1600x1200p60");
            this.ResolutionList.Add("1680x1050p60");
            this.ResolutionList.Add("1920x1080p30");
            this.ResolutionList.Add("1920x1080p50");
            this.ResolutionList.Add("1920x1080p60");
            this.ResolutionList.Add("1920x1200p60");
            this.ResolutionList.Add("3840x2160p30");
            this.ResolutionList.Add("3840x2160p60");

            this.PagesSource.Add(new BoardBingModel() { OutputName = "输入1" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输入2" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输入3" });
            this.PagesSource.Add(new BoardBingModel() { OutputName = "输入4" });


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
            mThreadingTimer = new System.Threading.Timer(MessageRecvFinished, null, Timeout.Infinite, Timeout.Infinite);
            initCommandList();
            initData();
            GlobalContext.Current.CurrentVMLocator.MainVM.Serial.DataReceived += Serial_DataReceived;
            this._socket.NotifyFactory.ReceiveMessageChangedEvent += NotifyFactory_ReceiveMessageChangedEvent;
            // 初始化一个定时器：具有回调函数，无状态的，不启动，不终止定时器
           
        }

        private void QuerySwitchChannel()
        {
            QueryChannelStatus();
        }

        /// <summary>
        /// 初始化数据 打开蜂鸣器
        /// </summary>
        private void initData()
        {
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                SwitchBuzzer();
            }
            else
            {
                SwitchBuzzer();
            }
        }

        /// <summary>
        /// 查询通道的音频状态。
        /// </summary>
        public void QueryAudio()
        {
            string queryerror = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("<Audio>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("<Audio>", out queryerror);
            }
        }

        /// <summary>
        /// 查询接口分辨率、输出模式、背景颜色
        /// </summary>
        public void QueryPortScan()
        {
            string queryerror = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("<PortScan>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("<PortScan>", out queryerror);
            }
        }

        /// <summary>
        /// 查询通道映射关系状态
        /// </summary>
        public void QueryChannelStatus()
        {
            string queryerror = string.Empty;
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("<Status>");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction("<Status>", out queryerror);
            }
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
            this.IsOverAllMsg = Visibility.Visible;
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
                    this.IsOverAllMsg = Visibility.Collapsed;
                }, true);
            }
            if (notify.Message.IndexOf("01-") > -1)
            {
                App.RunInUIThread(() =>
                {
                    this.ScenePlanList.Clear();
                    this.ScenePlanList.Add(new BoardBingModel() { PlanInfo = notify.Message });
                    this.IsOverAllMsg = Visibility.Collapsed;
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
                        this.IsOverAllMsg = Visibility.Collapsed;
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
                    this.IsOverAllMsg = Visibility.Collapsed;
                }, true);
            }
            if (notify.Message.IndexOf("Channel Status") > -1)
            {
                App.RunInUIThread(() =>
                {
                    for (int m = 0; m < 4; m++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            this.ChannelsList.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = m, InputChannel = j });
                        }
                    }
                    string[] getAry = notify.Message.Split(new char[] { '\n' });
                    List<int> temp_index = getAry.Select((item, index) => new { item, index }).Where(t => t.item.IndexOf("->") > -1).Select(t => t.index).ToList();
                    int i = 1;
                    int j4 = 0;
                    int j1 = 0;
                    int j2 = 0;
                    int j3 = 0;
                    if (temp_index.Count() < 1)
                        return;
                    foreach (var item in temp_index)
                    {
                        int len = getAry[item].Count();
                        Console.WriteLine(len);
                        if (len == 8)
                        {
                            //没有给到其他输出
                            continue;
                        }
                        else if (len == 9)
                        {
                            //给到1个输出
                            j1 = int.Parse(getAry[item].Substring(8, 1));
                        }
                        else if (len == 14)
                        {
                            //给了两个输出
                            j1 = int.Parse(getAry[item].Substring(8, 1));
                            j2 = int.Parse(getAry[item].Substring(11, 1));
                        }
                        else if (len == 17)
                        {
                            //给了3个输出
                            j1 = int.Parse(getAry[item].Substring(8, 1));
                            j2 = int.Parse(getAry[item].Substring(11, 1));
                            j3 = int.Parse(getAry[item].Substring(14, 1));
                        }
                        else if (len == 20)
                        {
                            //给了4个输出
                            j1 = int.Parse(getAry[item].Substring(8, 1));
                            j2 = int.Parse(getAry[item].Substring(11, 1));
                            j3 = int.Parse(getAry[item].Substring(14, 1));
                            j4 = int.Parse(getAry[item].Substring(17, 1));

                        }
                        var findchannel1 = this.ChannelsList.FirstOrDefault(x => (x.OnputChannel + 1 == j1) && (x.InputChannel + 1) == i);
                        if (findchannel1 != null)
                            findchannel1.IsCheckChannel = true;
                        var findchannel2 = this.ChannelsList.FirstOrDefault(x => (x.OnputChannel + 1 == j2) && (x.InputChannel + 1) == i);
                        if (findchannel2 != null)
                            findchannel2.IsCheckChannel = true;
                        var findchannel3 = this.ChannelsList.FirstOrDefault(x => (x.OnputChannel + 1 == j3) && (x.InputChannel + 1) == i);
                        if (findchannel3 != null)
                            findchannel3.IsCheckChannel = true;
                        var findchannel4 = this.ChannelsList.FirstOrDefault(x => (x.OnputChannel + 1 == j4) && (x.InputChannel + 1) == i);
                        if (findchannel4 != null)
                            findchannel4.IsCheckChannel = true;
                        i++;
                    }
                    this.IsOverAllMsg = Visibility.Collapsed;
                }, true);
            }

            if (notify.Message.IndexOf("Audio Type ") > -1)
            {
                App.RunInUIThread(() =>
                {
                    string[] getAudio = notify.Message.Split(new string[] { "\n" }, StringSplitOptions.None);
                    Console.WriteLine(getAudio);

                    if (getAudio[2].IndexOf("Ext.") > -1)
                    {
                        this.SelectedOneInAudioName = "模拟音频";
                    }
                    else
                    {
                        this.SelectedOneInAudioName = "嵌入音频";
                    }
                    if (getAudio[3].IndexOf("Ext.") > -1)
                    {
                        this.SelectedTwoInAudioName = "模拟音频";
                    }
                    else
                    {
                        this.SelectedTwoInAudioName = "嵌入音频";
                    }
                    if (getAudio[4].IndexOf("Ext.") > -1)
                    {
                        this.SelectedInThreeAudioName = "模拟音频";
                    }
                    else
                    {
                        this.SelectedInThreeAudioName = "嵌入音频";
                    }
                    if (getAudio[5].IndexOf("Ext.") > -1)
                    {
                        this.SelectedInFourAudioName = "模拟音频";
                    }
                    else
                    {
                        this.SelectedInFourAudioName = "嵌入音频";
                    }
                    this.IsOverAllMsg = Visibility.Collapsed;
                }, true);
            }

            if(notify.Message.IndexOf("Out Channel |  Resolution  | Mode | BackColor ") > -1)
            {
                App.RunInUIThread(() => {
                string[] getPort = notify.Message.Split(new string[] { "*/\n/*" }, StringSplitOptions.None);
                //Console.WriteLine(notify.Message);
                for (int k = 1; k <= 4; k++)
                {
                    var split = getPort[k].Split(new char[] { '|' });
                    Console.WriteLine(split);
                    if (split[0].IndexOf(" CH-2 ") > -1)
                    {
                        this.SelectedOutTwoResolution = split[1].Trim();
                        if (split[3].Trim() == "Blue")
                            this.SelectedOutTwoBack = "设为蓝屏";
                        else
                            this.SelectedOutTwoBack = "设为黑屏";
                        this.SelectedOutTwoType = split[2].Trim();
                    }
                    if (split[0].IndexOf(" CH-1 ") > -1)
                    {
                        this.SelectedOutOneResolution = split[1].Trim();
                        if (split[3].Trim() == "Blue")
                            this.SelectedOutOneBack = "设为蓝屏";
                        else
                            this.SelectedOutOneBack = "设为黑屏";
                        this.SelectedOutOneType = split[2].Trim();
                    }
                    if (split[0].IndexOf(" CH-3 ") > -1)
                    {
                        this.SelectedOutThreeResolution = split[1].Trim();
                        if (split[3].Trim() == "Blue")
                            this.SelectedOutThreeBack = "设为蓝屏";
                        else
                            this.SelectedOutThreeBack = "设为黑屏";
                        this.SelectedOutThreeType = split[2].Trim();
                    }
                    if (split[0].IndexOf(" CH-4 ") > -1)
                    {
                        this.SelectedOutFourResolution = split[1].Trim();
                        if (split[3].Trim() == "Blue")
                            this.SelectedOutFourBack = "设为蓝屏";
                        else
                            this.SelectedOutFourBack = "设为黑屏";
                        this.SelectedOutFourType = split[2].Trim();
                    }
                }
                    this.IsOverAllMsg = Visibility.Collapsed;
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
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this.SendDataBySerialPort($"{item.OnputChannel}SETOUT.<ColorReset>");
                    }
                }
            }
            else
            {
                foreach (var item in this.OutPutColorChannels)
                {
                    if (item.IsCheckChannel == true)
                    {
                        this._scanBoardListRequest.SendIndtruction($"{item.OnputChannel}SETOUT.<ColorReset>", out sendError);
                    }
                }
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
            this.IsOverAllMsg = Visibility.Visible;
            string str = GlobalContext.Current.CurrentVMLocator.MainVM.Serial.ReadExisting(); //以字符串方式读
            if (String.IsNullOrEmpty(str) || str.Equals("<Set Succeed!>\r\n"))
                return;

            this.mThreadingTimer.Change(500, Timeout.Infinite);

            if (str != null)
            {
                this._recvMessageCache += str;
            }
        }

        private string _recvMessageCache = string.Empty;

        private void MessageRecvFinished(object _)
        {
            if (string.IsNullOrEmpty(this._recvMessageCache))
                return;

            App.RunInUIThread(() =>
            {
                if (_recvMessageCache.IndexOf("Channel Status") > -1)
                {
                    this.ChannelsList.Clear();
                    for (int m = 0; m < 4; m++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            this.ChannelsList.Add(new BoardBingModel() { IsCheckChannel = false, OnputChannel = m, InputChannel = j });
                        }
                    }
                    string[] getAry = _recvMessageCache.Split(new char[] { '\n' }, 7);
                    List<int> temp_index = getAry.Select((item, index) => new { item, index }).Where(t => t.item.IndexOf("->") > -1).Select(t => t.index).ToList();
                    int i = 1;
                    int j4 = 0;
                    int j1 = 0;
                    int j2 = 0;
                    int j3 = 0;
                    if (temp_index.Count() < 1)
                        return;
                    foreach (var item in temp_index)
                    {
                        int len = getAry[item].Count();
                        Console.WriteLine(len);
                        if (len == 8)
                        {
                            //没有给到其他输出
                            continue;
                        }
                        else if (len == 11)
                        {
                            //给到1个输出
                            j1 = int.Parse(getAry[item].Substring(8, 1));
                        }
                        else if (len == 14)
                        {
                            //给了两个输出
                            j1 = int.Parse(getAry[item].Substring(8, 1));
                            j2 = int.Parse(getAry[item].Substring(11, 1));
                        }
                        else if (len == 17)
                        {
                            //给了3个输出
                            j1 = int.Parse(getAry[item].Substring(8, 1));
                            j2 = int.Parse(getAry[item].Substring(11, 1));
                            j3 = int.Parse(getAry[item].Substring(14, 1));
                        }
                        else if (len == 20)
                        {
                            //给了4个输出
                            j1 = int.Parse(getAry[item].Substring(8, 1));
                            j2 = int.Parse(getAry[item].Substring(11, 1));
                            j3 = int.Parse(getAry[item].Substring(14, 1));
                            j4 = int.Parse(getAry[item].Substring(17, 1));

                        }
                        var findchannel1 = this.ChannelsList.FirstOrDefault(x => (x.OnputChannel + 1 == j1) && (x.InputChannel + 1) == i);
                        if (findchannel1 != null)
                            findchannel1.IsCheckChannel = true;
                        var findchannel2 = this.ChannelsList.FirstOrDefault(x => (x.OnputChannel + 1 == j2) && (x.InputChannel + 1) == i);
                        if (findchannel2 != null)
                            findchannel2.IsCheckChannel = true;
                        var findchannel3 = this.ChannelsList.FirstOrDefault(x => (x.OnputChannel + 1 == j3) && (x.InputChannel + 1) == i);
                        if (findchannel3 != null)
                            findchannel3.IsCheckChannel = true;
                        var findchannel4 = this.ChannelsList.FirstOrDefault(x => (x.OnputChannel + 1 == j4) && (x.InputChannel + 1) == i);
                        if (findchannel4 != null)
                            findchannel4.IsCheckChannel = true;
                        i++;
                    }
                }

                if(_recvMessageCache.IndexOf("Out Channel |  Resolution  | Mode | BackColor ") > -1)
                {
                    string[] getPort = _recvMessageCache.Split(new string[] { "*/\n/*" }, StringSplitOptions.None);
                    Console.WriteLine(_recvMessageCache);
                    List<int> temp_indexport = getPort.Select((item, index) => new { item, index }).Where(t => t.item.IndexOf("CH-") > -1).Select(t => t.index).ToList();
                    for (int k = 1; k <= 4; k++)
                    {
                        var split = getPort[k].Split(new char[] { '|' });
                        Console.WriteLine(split);
                        if (split[0].IndexOf(" CH-2 ") > -1)
                        {
                            this.SelectedOutTwoResolution = split[1].Trim();
                            if (split[3].Trim() == "Blue")
                                this.SelectedOutTwoBack = "设为蓝屏";
                            else
                                this.SelectedOutTwoBack = "设为黑屏";
                            this.SelectedOutTwoType = split[2].Trim();
                        }
                        if (split[0].IndexOf(" CH-1 ") > -1)
                        {
                            this.SelectedOutOneResolution = split[1].Trim();
                            if (split[3].Trim() == "Blue")
                                this.SelectedOutOneBack = "设为蓝屏";
                            else
                                this.SelectedOutOneBack = "设为黑屏";
                            this.SelectedOutOneType = split[2].Trim();
                        }
                        if (split[0].IndexOf(" CH-3 ") > -1)
                        {
                            this.SelectedOutThreeResolution = split[1].Trim();
                            if (split[3].Trim() == "Blue")
                                this.SelectedOutThreeBack = "设为蓝屏";
                            else
                                this.SelectedOutThreeBack = "设为黑屏";
                            this.SelectedOutThreeType = split[2].Trim();
                        }
                        if (split[0].IndexOf(" CH-4 ") > -1)
                        {
                            this.SelectedOutFourResolution = split[1].Trim();
                            if (split[3].Trim() == "Blue")
                                this.SelectedOutFourBack = "设为蓝屏";
                            else
                                this.SelectedOutFourBack = "设为黑屏";
                            this.SelectedOutFourType = split[2].Trim();
                        }
                    }
                }

                if (_recvMessageCache.IndexOf("Audio Type ") > -1)
                {
                    string[] getAudio = _recvMessageCache.Split(new string[] { "\n" }, StringSplitOptions.None);
                    Console.WriteLine(getAudio);
                    List<int> temp_indexaudio = getAudio.Select((item, index) => new { item, index }).Where(t => t.item.IndexOf(":") > -1).Select(t => t.index).ToList();
                    if (temp_indexaudio.Count() < 4)
                        return;
                    if (getAudio[temp_indexaudio[0]].IndexOf("Ext.") > -1)
                    {
                        this.SelectedOneInAudioName = "模拟音频";
                    }
                    else
                    {
                        this.SelectedOneInAudioName = "嵌入音频";
                    }
                    if (getAudio[temp_indexaudio[1]].IndexOf("Ext.") > -1)
                    {
                        this.SelectedTwoInAudioName = "模拟音频";
                    }
                    else
                    {
                        this.SelectedTwoInAudioName = "嵌入音频";
                    }
                    if (getAudio[temp_indexaudio[2]].IndexOf("Ext.") > -1)
                    {
                        this.SelectedInThreeAudioName = "模拟音频";
                    }
                    else
                    {
                        this.SelectedInThreeAudioName = "嵌入音频";
                    }
                    if (getAudio[temp_indexaudio[3]].IndexOf("Ext.") > -1)
                    {
                        this.SelectedInFourAudioName = "模拟音频";
                    }
                    else
                    {
                        this.SelectedInFourAudioName = "嵌入音频";
                    }

                }

                if (_recvMessageCache.IndexOf("EDID Scan") > -1)
                {
                    this.EDIDPlanList.Clear();
                    this.EDIDPlanList.Add(new BoardBingModel() { EDEDIDInfo = _recvMessageCache });
                }
                if (_recvMessageCache.IndexOf("01-") > -1)
                {
                    this.ScenePlanList.Clear();
                    this.ScenePlanList.Add(new BoardBingModel() { PlanInfo = _recvMessageCache });
                }
                if (_recvMessageCache.IndexOf("Network Information") > -1)
                {
                    this.NetInfoList.Clear();
                    this.NetInfoList.Add(new BoardBingModel() { NetInfo = _recvMessageCache });
                }
                if (_recvMessageCache.IndexOf(" CHN  | Brightness | Contrast | Saturability |  HUE") > -1)
                {
                    this.ColorSettingsList.Clear();
                    this.ColorSettingsList.Add(new BoardBingModel() { ColorSettings = _recvMessageCache });
                }
                this.IsOverAllMsg = Visibility.Collapsed;
                this._recvMessageCache = string.Empty;
                this.mThreadingTimer.Change(Timeout.Infinite, Timeout.Infinite);
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
        public void SetDvi(int channelNum)
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
        public void SetHDMI(int channelNum)
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
        public void SetBgBlackScreen(int channelNum)
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
        public void SetBgBlueScreen(int channelNum)
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
        public void SetOutBgBlackScreen(int channelNum)
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
        public void SetOutBgBlueScreen(int channelNum)
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
        public void SetQianruAudioMode(int channelNum)
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
        public void SetMoniAudioMode(int channelNum)
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
                    this.SendDataBySerialPort($"{outname}L{indeid.Instructions}.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction($"{outname}L{indeid.Instructions}.", out sendError);
                }
            }

        }

        #endregion

        #region 输入输出切换


        private void SwitchOne(BoardBingModel boardBing)
        {
            foreach (var item in this.ChannelsList)
            {
                if (item.IsCheckChannel == true && item.OnputChannel == boardBing.OnputChannel)
                    item.IsCheckChannel = false;
            }
            string sendError = string.Empty;
            int outchannel = 0;
            outchannel=boardBing.OnputChannel + 1;
            int intputchannel = 0;
            intputchannel = boardBing.InputChannel + 1;   
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{intputchannel}V{outchannel}.");
            }
            else
            {
                this._scanBoardListRequest.SendIndtruction($"{intputchannel}V{outchannel}.", out sendError);
            }
            boardBing.IsCheckChannel = true;

            //if (name == "输出1")
            //{
            //    //xVy.
            //    if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            //    {
            //        this.SendDataBySerialPort("1V1.");
            //    }
            //    else
            //    {
            //        this._scanBoardListRequest.SendIndtruction("1V1.", out sendError);
            //    }
            //}
            //else if (name == "输出2")
            //{
            //    if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            //    {
            //        this.SendDataBySerialPort("1V2.");
            //    }
            //    else
            //    {
            //        this._scanBoardListRequest.SendIndtruction("1V2.", out sendError);
            //    }
            //}
            //else if (name == "输出3")
            //{
            //    if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            //    {
            //        this.SendDataBySerialPort("1V3.");
            //    }
            //    else
            //    {
            //        this._scanBoardListRequest.SendIndtruction("1V3.", out sendError);
            //    }
            //}
            //else if (name == "输出4")
            //{
            //    if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            //    {
            //        this.SendDataBySerialPort("1V4.");
            //    }
            //    else
            //    {
            //        this._scanBoardListRequest.SendIndtruction("1V4.", out sendError);
            //    }
            //}
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
            if (name == "输入1")
            {
                if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                {
                    this.SendDataBySerialPort("1V4.");
                }
                else
                {
                    this._scanBoardListRequest.SendIndtruction("1V4.", out sendError);
                }
                this.IsAlreadySetOuputFour = false;
            }
            else if (name == "输入2")
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
            else if (name == "输入3")
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
            else if (name == "输入4")
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
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1024x768p60", IsCheckInstructions = false, Code = "01C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1280x720p50", IsCheckInstructions = false, Code = "02C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1280x720p60", IsCheckInstructions = false, Code = "03C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1280x1024p60", IsCheckInstructions = false, Code = "04C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1400x1050p60", IsCheckInstructions = false, Code = "05C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1440x900p60", IsCheckInstructions = false, Code = "07C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1600x1200p60", IsCheckInstructions = false, Code = "08C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1680x1050p60", IsCheckInstructions = false, Code = "09C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1920x1080p30", IsCheckInstructions = false, Code = "18C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1920x1080p50", IsCheckInstructions = false, Code = "17C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1920x1080p60", IsCheckInstructions = false, Code = "11C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "1920x1200p60", IsCheckInstructions = false, Code = "12C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "3840x2160p30", IsCheckInstructions = false, Code = "24C08." });
            this.NineOneColorAdj.Add(new BoardBingModel() { Instructions = "3840x2160p60", IsCheckInstructions = false, Code = "30C08." });

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

        #region 分辨率
        public void SendResolution(int channelNum,string selesctedInstruction)
        {
            string sendError = string.Empty;
            foreach(var item in this.NineOneColorAdj)
            {
                if (item.Instructions == selesctedInstruction)
                {
                    if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                    {
                        this.SendDataBySerialPort($"{channelNum}SETOUT.{item.Code}");
                    }
                    else
                    {
                        this._scanBoardListRequest.SendIndtruction($"{channelNum}SETIN.{item.Code}", out sendError);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 发送色调调整命令
        /// </summary>
        private void SendColorAdjInstructions()
        {
            string sendError = string.Empty;
            string sendcode = string.Empty;
            //this.SendColorAdjInstructionsList.Clear();
            //foreach (var item in this.NineOneColorAdj)
            //{
            //    if (item.IsCheckInstructions == true)
            //    {
                    if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                    {
                        //foreach (var channels in this.OutPutChannels)
                        //{
                        //    if (channels.IsCheckChannel == true)
                        //    {
                                //sendcode = $"{channels.OnputChannel}SETOUT.";
                                this.SendDataBySerialPort(SendInstructionStr);
                        //    }
                        //}
                    }
                    else
                    {
                        //foreach (var channels in this.OutPutChannels)
                        //{
                        //    if (channels.IsCheckChannel == true)
                        //    {
                        //        sendcode = $"{channels.OnputChannel}SETOUT.";
                                this._scanBoardListRequest.SendIndtruction(SendInstructionStr, out sendError);
                        //    }
                        //}
                       
                    }
                    this.SendColorAdjInstructionsList.Add(new BoardBingModel() { Code = SendInstructionStr });
                    //Thread.Sleep(SendIntevar);
                //}
            //}
        }

        public void SendDataBySerialPort(string message)
        {
            UTF8Encoding utf8 = new UTF8Encoding(false);
            Byte[] writeBytes = utf8.GetBytes(message);
            byte[] sendData = null;
            try
            {
                // 马上启动定时器
                mThreadingTimer.Change(0, Timeout.Infinite);
                this._recvMessageCache = String.Empty;
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
