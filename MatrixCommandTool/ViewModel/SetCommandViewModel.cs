using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MatrixCommandTool.Model.BindingModel;
using MatrixCommandTool.Net.DelegateModel;
using MatrixCommandTool.Net.TCP;
using MatrixCommandTool.Net.TCP.Request;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using static MatrixCommandTool.Net.TCP.DelegateFactory;

namespace MatrixCommandTool.ViewModel
{
    public class SetCommandViewModel : ViewModelBase
    {
        public ScanBoardListRequest _scanBoardListRequest;
        public ClientSocket _socket;
        #region Properity
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
                        if (this.InstructionsList != null)
                        {
                            foreach (var item in InstructionsList)
                            {
                                item.IsCheckInstructions = true;
                            }
                        }
                    }
                    else if (value == false)
                    {
                        if (this.InstructionsList != null)
                        {
                            foreach (var item in InstructionsList)
                            {
                                item.IsCheckInstructions = false;
                            }
                        }
                    }
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

        private BoardBingModel mSelectedBoard;
        public BoardBingModel SelectedBoard
        {
            get => this.mSelectedBoard;
            set
            {
                if (Set(ref this.mSelectedBoard, value))
                {
                    if (SelectedBoard == null)
                        return;
                    //激活板卡 <In/003/VGA   /1920x1080x60Hz!/V1.4->
                    if (SelectedBoard.BoardName == "")
                        return;
                    string[] boardInfo = this.SelectedBoard.BoardName.Split('/');
                    int channelId = int.Parse(boardInfo[1]);
                    string cardType = boardInfo[0].Replace("<", "");
                    string setError = string.Empty;
                    this.InitCardType(boardInfo);
                    if (cardType == "In")
                    {
                        if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                        {
                            this.SendDataBySerialPort("[" + channelId + "]SETIN.");
                        }
                        else
                        {
                            this._scanBoardListRequest.SetActivationIn(channelId, out setError);
                        }
                       
                        this.InitInstructionByInputType(boardInfo);
                    }
                    else if (cardType == "Out")
                    {
                        if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
                        {
                            this.SendDataBySerialPort("[" + channelId + "]SETOUT.");
                        }
                        else
                        {
                            this._scanBoardListRequest.SetActivationOut(channelId, out setError);
                        }
                        this.InitInstructionByOutputType(boardInfo);
                    }
                }
            }
        }

        private int mSendIntevar = 1000;
        public int SendIntevar
        {
            get => this.mSendIntevar;
            set => Set(ref this.mSendIntevar, value);
        }

        private BoardBingModel mSelectedInstruction;
        /// <summary>
        /// 选中的命令
        /// </summary>
        public BoardBingModel SelectedInstruction
        {
            get => this.mSelectedInstruction;
            set => Set(ref this.mSelectedInstruction, value);

        }

        private string mSelectedModel;
        /// <summary>
        /// 选中的板卡型号
        /// </summary>
        public string SelectedModel
        {
            get => this.mSelectedModel;
            set => Set(ref this.mSelectedModel, value);
        }

        private string mMode;
        /// <summary>
        /// 选中的板卡型号
        /// </summary>
        public string Mode
        {
            get => this.mMode;
            set => Set(ref this.mMode, value);
        }

        private string mInputInstruction;
        /// <summary>
        /// 选中的板卡型号
        /// </summary>
        public string InputInstruction
        {
            get => this.mInputInstruction;
            set => Set(ref this.mInputInstruction, value);
        }

        private string mInputCode;
        /// <summary>
        /// 选中的板卡型号
        /// </summary>
        public string InputCode
        {
            get => this.mInputCode;
            set => Set(ref this.mInputCode, value);
        }

        private IList<string> mCardMode;
        /// <summary>
        /// 板卡型号
        /// </summary>
        public IList<string> CardMode
        {
            get => this.mCardMode;
            set => Set(ref this.mCardMode, value);
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

        #region 弃用
        private int mCurrentSharpness;
        /// <summary>
        /// 当前锐度数值
        /// </summary>
        public int CurrentSharpness
        {
            get => this.mCurrentSharpness;
            set => Set(ref this.mCurrentSharpness, value);
        }

        private int mCurrentContrast;
        /// <summary>
        /// 当前对比度数值
        /// </summary>
        public int CurrentContrast
        {
            get => this.mCurrentContrast;
            set => Set(ref this.mCurrentContrast, value);
        }

        private int mCurrentSaturation;
        /// <summary>
        /// 当前饱和度数值
        /// </summary>
        public int CurrentSaturation
        {
            get => this.mCurrentSaturation;
            set => Set(ref this.mCurrentSaturation, value);
        }

        private int mCurrentVolume;
        /// <summary>
        /// 当前音量调节数值
        /// </summary>
        public int CurrentVolume
        {
            get => this.mCurrentVolume;
            set => Set(ref this.mCurrentVolume, value);
        }

        private int mCurrentBrightness;
        /// <summary>
        /// 当前亮度数值
        /// </summary>
        public int CurrentBrightness
        {
            get => this.mCurrentBrightness;
            set => Set(ref this.mCurrentBrightness, value);
        }

        private int mCurrentRedTone;
        /// <summary>
        /// 当前红色调数值
        /// </summary>
        public int CurrentRedTone
        {
            get => this.mCurrentRedTone;
            set => Set(ref this.mCurrentRedTone, value);
        }

        private int mCurrentGreenTone;
        /// <summary>
        /// 当前绿色调数值
        /// </summary>
        public int CurrentGreenTone
        {
            get => this.mCurrentGreenTone;
            set => Set(ref this.mCurrentGreenTone, value);
        }

        private int mCurrentBlueTone;
        /// <summary>
        /// 当前蓝色调数值
        /// </summary>
        public int CurrentBlueTone
        {
            get => this.mCurrentBlueTone;
            set => Set(ref this.mCurrentBlueTone, value);
        }
        #endregion

        public delegate void OnMessageReceiveDelegate(string message);
        public event OnMessageReceiveDelegate OnMessageReceive; 

        /// <summary>
        /// 所有板卡
        /// </summary>
        public IList<BoardBingModel> BoardCardList { get; set; } = new ObservableCollection<BoardBingModel>();

        /// <summary>
        /// 所有指令
        /// </summary>
        public IList<BoardBingModel> InstructionsList { get; set; } = new ObservableCollection<BoardBingModel>();

        public IList<BoardBingModel> FilterInstructions { get; set; } = new ObservableCollection<BoardBingModel>();
        /// <summary>
        /// 已发送指令
        /// </summary>
        public IList<BoardBingModel> SendInstructionsList { get; set; } = new ObservableCollection<BoardBingModel>();
        #endregion

        #region Command
        public ICommand ScanBoardInstructions { get; set; }

        public ICommand SendInstructionsCommand { get; set; }

        public ICommand EditInstructionsCommand { get; set; }

        public ICommand AddInstructionsCommand { get; set; }

        public ICommand FindInstructionListCommand { get; set; }

        public ICommand SwitchBuzzerCommand { get; set; }
        #endregion

        public SetCommandViewModel(ScanBoardListRequest scanBoardListRequest, ClientSocket clientSocket)
        {
            this._scanBoardListRequest = scanBoardListRequest;
            this._socket = clientSocket;
            this.ScanBoardInstructions = new RelayCommand(ScanBoardList);
            this.SendInstructionsCommand = new RelayCommand(SendInstructions);
            this.EditInstructionsCommand = new RelayCommand(EditInstructions);
            this.AddInstructionsCommand = new RelayCommand(AddInstructions);
            this.SwitchBuzzerCommand = new RelayCommand(SwitchBuzzer);

            this.mInstructionsFilterDelay = new Helper.DelayTrigger();
            this.mInstructionsFilterDelay.OnExecute = filterInstruction;
            this.CardMode = new ObservableCollection<string>();
            this._socket.NotifyFactory.ReceiveMessageChangedEvent += NotifyFactory_ReceiveMessageChangedEvent;
            GlobalContext.Current.CurrentVMLocator.MainVM.Serial.DataReceived += Serial_DataReceived;
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

        string oldStr = string.Empty;
        private void Serial_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            App.RunInUIThread(() =>
            {
                string str = GlobalContext.Current.CurrentVMLocator.MainVM.Serial.ReadExisting(); //以字符串方式读
                if (String.IsNullOrEmpty(str) || str.Equals("<Set Succeed!>\r\n"))
                    return;
                if (str.Last().ToString().Equals("n") == false)

                oldStr = oldStr + str;
                if (oldStr.Length < 18)
                    return;

                string msg = oldStr.Substring(1, 18);
                //判断是否含有扫描结束
                if (msg == "Port/StartScanning"&&oldStr.Contains("<Port/StopScanning>"))
                {
                    this.BoardCardList.Clear();
                    List<string> boardCardList = new List<string>(str.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToArray());

                    foreach (var item in boardCardList)
                    {
                        if (item == "")
                            continue;
                        string subItem = item.Substring(1, 2);
                        if (subItem.Equals("In") || subItem.Equals("Ou"))
                        {
                            this.BoardCardList.Add(new BoardBingModel() { BoardName = item });

                        }
                    }
                    oldStr = string.Empty;
                    if (this.SelectedBoard == null)
                        this.SelectedBoard = this.BoardCardList.FirstOrDefault();
                }
            }, true);
        }

        /// <summary>
        /// 加入自定义的命令
        /// </summary>
        private void AddInstructions()
        {
            this.InstructionsList.Add(new BoardBingModel() { Instructions = InputInstruction, IsCheckInstructions = false, Code = InputCode });
        }

        /// <summary>
        /// 发送自定义的命令
        /// </summary>
        private void EditInstructions()
        {
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort(InputCode);
            }
            else
            {
                string editError = string.Empty;
                this._scanBoardListRequest.SendIndtruction(InputCode, out editError);
            }
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        private void SendInstructions()
        {
            string sendError = string.Empty;
            this.SendInstructionsList.Clear();
            foreach (var item in this.InstructionsList)
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
                    this.SendInstructionsList.Add(new BoardBingModel() { Instructions = item.Instructions, Code = item.Code });
                    Thread.Sleep(SendIntevar);
                }
            }
        }

        private void NotifyFactory_ReceiveMessageChangedEvent(Net.DelegateModel.ReceiveMessageNotify notify)
        {
            if (notify.Message == null)
                return;
            if (notify.Message.Equals("<Set Succeed!>\r\n"))
            {
                Console.WriteLine(notify.Message);
                return;
            }
            string msg = notify.Message.Substring(1, 18);
            if (msg == "Port/StartScanning")
            {
                App.RunInUIThread(() =>
                {
                    this.InitBoardCardList(notify.Message);
                }, true);
            }
        }

        private void InitBoardCardList(string message)
        {
            this.BoardCardList.Clear();
            List<string> boardCardList = new List<string>(message.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToArray());
            App.RunInUIThread(() =>
            {
                foreach (var item in boardCardList)
                {
                    if (item.Equals("<Port/StartScanning>") || item.Equals("<Port/StopScanning>") || item.Equals("\n"))
                        continue;
                    this.BoardCardList.Add(new BoardBingModel() { BoardName = item });
                }
                if (this.SelectedBoard == null)
                    this.SelectedBoard = this.BoardCardList.FirstOrDefault();
            }, true);
        }

        /// <summary>
        ///扫描版卡
        /// </summary>
        private void ScanBoardList()
        {
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort("/:ScanPortType;");
            }
            else
            {
                string scanError = string.Empty;
                this._scanBoardListRequest.GetAllBoardList(out scanError);
            }

        }

        private void filterInstruction(string obj)
        {
            App.RunInUIThread(() => this.FindInstructionList());
        }

        private void FindInstructionList()
        {
            if (string.IsNullOrEmpty(this.FindInstructionsStr))
            {
                //激活板卡 <In/003/VGA   /1920x1080x60Hz!/V1.4->
                string[] boardInfo = this.SelectedBoard.BoardName.Split('/');
                int channelId = int.Parse(boardInfo[1]);
                string cardType = boardInfo[0].Replace("<", "");
                string setError = string.Empty;
                this.InitCardType(boardInfo);
                if (cardType == "In")
                {
                    this.InitInstructionByInputType(boardInfo);
                }
                else if (cardType == "Out")
                {
                    this.InitInstructionByOutputType(boardInfo);
                }
            }
            if (!string.IsNullOrEmpty(this.FindInstructionsStr))
            {
                foreach (var item in this.InstructionsList)
                {
                    if (item.Instructions.Contains(this.FindInstructionsStr))
                        this.FilterInstructions.Add(new BoardBingModel() { Instructions = item.Instructions, IsCheckInstructions = item.IsCheckInstructions, Code = item.Instructions });
                }
                this.InstructionsList.Clear();
                foreach (var filter in this.FilterInstructions)
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = filter.Instructions, IsCheckInstructions = filter.IsCheckInstructions, Code = filter.Instructions });
                }
            }
        }

        /// <summary>
        /// 初始化卡类型
        /// </summary>
        /// <param name="boardInfo"></param>
        private void InitCardType(string[] boardInfo)
        {
            this.CardMode.Clear();
            if (boardInfo[0].Replace("<", "") == "In")
            {

                if (boardInfo[2].Trim() == "HDMI")
                {
                    //HDMI
                    this.CardMode.Add("TS-9404HI");
                    this.CardMode.Add("TS-9404UHI");
                    this.CardMode.Add("TS-9604HI");
                    this.CardMode.Add("TS-9604HI-4K");
                }
                else if (boardInfo[2].Trim() == "VGA")
                {
                    //VGA
                    this.CardMode.Add("TS-9404CI");
                }
                else if (boardInfo[2].Trim() == "DVI")
                {
                    //DVI
                    this.CardMode.Add("TS-9404DI");
                }
                else if (boardInfo[2].Trim() == "HDBasT")
                {
                    //HDBasT
                    this.CardMode.Add("TS-9404HBI");
                }
                else if (boardInfo[2].Trim() == "FIBER")
                {
                    //FIBER
                    this.CardMode.Add("TS-9404FI");
                    this.CardMode.Add("TS-9404HBI");
                }
                else if (boardInfo[2].Trim() == "SDI")
                {
                    //SDI
                    this.CardMode.Add("TS-9404SI");
                }
                else if (boardInfo[2].Trim() == "IP")
                {
                    //IP
                    this.CardMode.Add("TS-9404IPI");
                }

            }
            else if (boardInfo[0].Replace("<", "") == "Out")
            {
                if (boardInfo[2].Trim() == "HDMI")
                {
                    //HDMI
                    this.CardMode.Add("TS-9404HO");
                    this.CardMode.Add("TS-940UHO");
                    this.CardMode.Add("TS-9604HO");
                    this.CardMode.Add("TS-9604HO-4K");
                }
                else if (boardInfo[2].Trim() == "VGA")
                {
                    this.CardMode.Add("TS-9404CO");
                }
                else if (boardInfo[2].Trim() == "DVI")
                {
                    this.CardMode.Add("TS-9404DO");
                }
                else if (boardInfo[2].Trim() == "HDBasT")
                {
                    this.CardMode.Add("TS-9404HBO");
                }
                else if (boardInfo[2].Trim() == "FIBER")
                {
                    this.CardMode.Add("TS-940FO");
                    this.CardMode.Add("TS-9404HBO");
                }
                else if (boardInfo[2].Trim() == "SDI")
                {
                    this.CardMode.Add("TS-940SO");
                }
                else if (boardInfo[2].Trim() == "IP")
                {
                }
            }
            this.SelectedModel = CardMode.FirstOrDefault();
        }

        /// <summary>
        /// 输出卡指令
        /// </summary>
        /// <param name="boardInfo"></param>
        private void InitInstructionByOutputType(string[] boardInfo)
        {
            this.InstructionsList.Clear();
            if (boardInfo[2].Trim() == "HDMI")
            {
                if (SelectedModel == "TS-9404HO")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1024 X 768@60Hz ", IsCheckInstructions = false, Code = "01C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@60Hz ", IsCheckInstructions = false, Code = "02C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 1024@60Hz ", IsCheckInstructions = false, Code = "03C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1366 X 768@60Hz ", IsCheckInstructions = false, Code = "04C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1400 X 1050@60Hz ", IsCheckInstructions = false, Code = "05C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1440 X 900@60Hz", IsCheckInstructions = false, Code = "07C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@60Hz", IsCheckInstructions = false, Code = "08C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1680 X 1050@60Hz", IsCheckInstructions = false, Code = "09C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@50Hz", IsCheckInstructions = false, Code = "17C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@60Hz", IsCheckInstructions = false, Code = "11C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1200@60Hz", IsCheckInstructions = false, Code = "12C08." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "复位RGB色调参数", IsCheckInstructions = false, Code = "00C25." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置默认信号为蓝屏", IsCheckInstructions = false, Code = "15C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置默认信号为黑屏", IsCheckInstructions = false, Code = "16C02." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@50Hz", IsCheckInstructions = false, Code = "10C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1200@50Hz", IsCheckInstructions = false, Code = "13C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720 @ 50Hz", IsCheckInstructions = false, Code = "20C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置DVI输出模式", IsCheckInstructions = false, Code = "07C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置HDMI输出模式", IsCheckInstructions = false, Code = "08C02." });
                }
                if (SelectedModel == "TS-940UHO")
                {

                }
                if (SelectedModel == "TV-9604HO")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1024 X 768@60Hz ", IsCheckInstructions = false, Code = "01C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@60Hz ", IsCheckInstructions = false, Code = "02C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 1024@60Hz ", IsCheckInstructions = false, Code = "03C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1366 X 768@60Hz ", IsCheckInstructions = false, Code = "04C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1400 X 1050@60Hz ", IsCheckInstructions = false, Code = "05C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1440 X 900@60Hz", IsCheckInstructions = false, Code = "07C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@60Hz", IsCheckInstructions = false, Code = "08C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1680 X 1050@60Hz", IsCheckInstructions = false, Code = "09C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1360 X 176@60Hz", IsCheckInstructions = false, Code = "26C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@60Hz", IsCheckInstructions = false, Code = "11C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1200@60Hz", IsCheckInstructions = false, Code = "12C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置DVI输出模式", IsCheckInstructions = false, Code = "07C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置HDMI输出模式", IsCheckInstructions = false, Code = "08C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为外部音频输出", IsCheckInstructions = false, Code = "11C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为内部音频输出", IsCheckInstructions = false, Code = "12C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "音频正常", IsCheckInstructions = false, Code = "13C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "音频静音", IsCheckInstructions = false, Code = "14C02." });
                }
                if (SelectedModel == "TV-9604HO-4K")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1024 X 768@60Hz ", IsCheckInstructions = false, Code = "01C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@60Hz ", IsCheckInstructions = false, Code = "02C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 1024@60Hz  ", IsCheckInstructions = false, Code = "03C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1366 X 768@60Hz ", IsCheckInstructions = false, Code = "04C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1400 X 1050@60Hz ", IsCheckInstructions = false, Code = "05C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1440 X 900@60Hz", IsCheckInstructions = false, Code = "07C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@60Hz", IsCheckInstructions = false, Code = "08C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1680 X 1050@60Hz", IsCheckInstructions = false, Code = "09C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1360 X 760@60Hz", IsCheckInstructions = false, Code = "26C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@60Hz", IsCheckInstructions = false, Code = "11C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1200@60Hz", IsCheckInstructions = false, Code = "12C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 3840 X 2160@30Hz", IsCheckInstructions = false });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置DVI输出模式", IsCheckInstructions = false, Code = "07C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置HDMI输出模式", IsCheckInstructions = false, Code = "08C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为外部音频输出", IsCheckInstructions = false, Code = "11C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为内部音频输出", IsCheckInstructions = false, Code = "12C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "音频正常", IsCheckInstructions = false, Code = "13C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "音频静音", IsCheckInstructions = false, Code = "14C02." });
                }
            }
            else if (boardInfo[2].Trim() == "VGA")
            {
                if (SelectedModel == "TS-9404CO")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1024 X 768@60Hz ", IsCheckInstructions = false, Code = "01C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@60Hz ", IsCheckInstructions = false, Code = "02C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 1024@60Hz ", IsCheckInstructions = false, Code = "03C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1366 X 768@60Hz ", IsCheckInstructions = false, Code = "04C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1400 X 1050@60Hz ", IsCheckInstructions = false, Code = "05C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1440 X 900@60Hz", IsCheckInstructions = false, Code = "07C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@60Hz", IsCheckInstructions = false, Code = "08C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1680 X 1050@60Hz", IsCheckInstructions = false, Code = "09C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@60Hz", IsCheckInstructions = false, Code = "11C08." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为CVBS模式", IsCheckInstructions = false, Code = "01C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为Ypbpr模式", IsCheckInstructions = false, Code = "03C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为VGA模式", IsCheckInstructions = false, Code = "05C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "复位RGB色调参数", IsCheckInstructions = false, Code = "00C25." });

                }
            }
            else if (boardInfo[2].Trim() == "DVI")
            {
                if (SelectedModel == "TS-9404DO")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1024 X 768@60Hz ", IsCheckInstructions = false, Code = "01C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@60Hz ", IsCheckInstructions = false, Code = "02C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@50Hz ", IsCheckInstructions = false });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 1024@60Hz ", IsCheckInstructions = false, Code = "03C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1366 X 768@60Hz ", IsCheckInstructions = false, Code = "04C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1400 X 1050@60Hz ", IsCheckInstructions = false, Code = "05C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1440 X 900@60Hz", IsCheckInstructions = false, Code = "07C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@60Hz", IsCheckInstructions = false, Code = "08C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@50Hz", IsCheckInstructions = false, Code = "10C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1680 X 1050@60Hz", IsCheckInstructions = false, Code = "09C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@60Hz", IsCheckInstructions = false, Code = "11C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@50Hz", IsCheckInstructions = false, Code = "17C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1200@60Hz", IsCheckInstructions = false, Code = "12C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1200@50Hz", IsCheckInstructions = false, Code = "13C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置DVI输出模式", IsCheckInstructions = false, Code = "07C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置HDMI输出模式", IsCheckInstructions = false, Code = "08C02." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为CVBS模式", IsCheckInstructions = false, Code = "01C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为Ypbpr模式", IsCheckInstructions = false, Code = "03C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为VGA模式", IsCheckInstructions = false, Code = "05C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "复位RGB色调参数", IsCheckInstructions = false, Code = "00C25." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置背景为蓝屏", IsCheckInstructions = false, Code = "15C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置背景为黑屏", IsCheckInstructions = false, Code = "16C02." });
                }
            }
            else if (boardInfo[2].Trim() == "HDBasT")
            {
                if (SelectedModel == "TS-9404HBO")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@50Hz ", IsCheckInstructions = false, Code = "20C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1024 X 768@60Hz ", IsCheckInstructions = false, Code = "01C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@60Hz ", IsCheckInstructions = false, Code = "02C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 1024@60Hz ", IsCheckInstructions = false, Code = "03C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1366 X 768@60Hz ", IsCheckInstructions = false, Code = "04C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1400 X 1050@60Hz ", IsCheckInstructions = false, Code = "05C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1440 X 900@60Hz", IsCheckInstructions = false, Code = "07C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@60Hz", IsCheckInstructions = false, Code = "08C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@50Hz", IsCheckInstructions = false, Code = "10C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1680 X 1050@60Hz", IsCheckInstructions = false, Code = "09C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@60Hz", IsCheckInstructions = false, Code = "11C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@50Hz", IsCheckInstructions = false, Code = "17C08." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "复位RGB色调参数", IsCheckInstructions = false, Code = "00C25." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置背景为蓝屏", IsCheckInstructions = false, Code = "15C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置背景为黑屏", IsCheckInstructions = false, Code = "16C02." });
                }
            }
            else if (boardInfo[2].Trim() == "FIBER")
            {
                if (SelectedModel == "TS-9404HBO")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1024 X 768@60Hz ", IsCheckInstructions = false, Code = "01C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@60Hz ", IsCheckInstructions = false, Code = "02C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@50Hz ", IsCheckInstructions = false, Code = "20C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 1024@60Hz ", IsCheckInstructions = false, Code = "03C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1366 X 768@60Hz ", IsCheckInstructions = false, Code = "04C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1400 X 1050@60Hz ", IsCheckInstructions = false, Code = "05C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1440 X 900@60Hz", IsCheckInstructions = false, Code = "07C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1200@50Hz", IsCheckInstructions = false, Code = "13C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1200@60Hz", IsCheckInstructions = false, Code = "12C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1680 X 1050@60Hz", IsCheckInstructions = false, Code = "09C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@60Hz", IsCheckInstructions = false, Code = "11C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@50Hz", IsCheckInstructions = false, Code = "17C08." });


                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "复位RGB色调参数", IsCheckInstructions = false, Code = "00C25." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置背景为蓝屏", IsCheckInstructions = false, Code = "15C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置背景为黑屏", IsCheckInstructions = false, Code = "16C02." });
                }

                if (SelectedModel == "TS-940FO")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1024 X 768@60Hz ", IsCheckInstructions = false, Code = "01C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@60Hz ", IsCheckInstructions = false, Code = "02C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 1024@60Hz ", IsCheckInstructions = false, Code = "03C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1366 X 768@60Hz ", IsCheckInstructions = false, Code = "04C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1400 X 1050@60Hz ", IsCheckInstructions = false, Code = "05C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1440 X 900@60Hz", IsCheckInstructions = false, Code = "07C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1600 X 1200@60Hz", IsCheckInstructions = false, Code = "08C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1680 X 1050@60Hz", IsCheckInstructions = false, Code = "09C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920 X 1080@60Hz", IsCheckInstructions = false, Code = "11C08." });
                }
            }
            else if (boardInfo[2].Trim() == "SDI")
            {
                if (SelectedModel == "TS-940SO")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280 X 720@60Hz ", IsCheckInstructions = false, Code = "02C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920x1080px50HZ", IsCheckInstructions = false, Code = "17C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920x1080px30HZ", IsCheckInstructions = false, Code = "18C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920x1080px25HZ", IsCheckInstructions = false, Code = "19C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280x720px50hz", IsCheckInstructions = false, Code = "20C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1280x720x30hz", IsCheckInstructions = false, Code = "21C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920x1080ix60hz", IsCheckInstructions = false, Code = "22C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920x1080ix50hz", IsCheckInstructions = false, Code = "23C08." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "分辨率设置为 1920x1080px60HZ", IsCheckInstructions = false, Code = "11C08." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "复位RGB色调参数", IsCheckInstructions = false, Code = "00C25." });

                }
            }
            else if (boardInfo[2].Trim() == "IP")
            {

            }
        }

        /// <summary>
        /// 输入卡指令
        /// </summary>
        /// <param name="boardInfo"></param>
        private void InitInstructionByInputType(string[] boardInfo)
        {
            this.InstructionsList.Clear();
            if (boardInfo[2].Trim() == "HDMI")
            {
                if (SelectedModel == "TS-9404HI")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置外部音频输入", IsCheckInstructions = false, Code = "01C02." });//01C02.
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置内部音频输入", IsCheckInstructions = false, Code = "03C02." });//03C02
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "RGB恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });

                }
                if (SelectedModel == "TS-9404UHI")
                {

                }
                if (SelectedModel == "TV-9604HI")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置外部音频输入", IsCheckInstructions = false, Code = "01C02." });//01C02.
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置内部音频输入", IsCheckInstructions = false, Code = "03C02." });//03C02

                }
                if (SelectedModel == "TV-9604HI-4K")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置外部音频输入", IsCheckInstructions = false, Code = "01C02." });//01C02.
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置内部音频输入", IsCheckInstructions = false, Code = "03C02." });//03C02
                }
            }
            else if (boardInfo[2].Trim() == "VGA")
            {
                if (SelectedModel == "TS-9404CI")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为CVBS模式", IsCheckInstructions = false, Code = "01C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为Ypbpr模式", IsCheckInstructions = false, Code = "03C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为VGA模式", IsCheckInstructions = false, Code = "05C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "复位RGB色调参数", IsCheckInstructions = false, Code = "00C25." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "自动调整VGA输入", IsCheckInstructions = false, Code = "08C15." });
                }
            }
            else if (boardInfo[2].Trim() == "DVI")
            {
                if (SelectedModel == "TS-9404DI")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为CVBS模式", IsCheckInstructions = false, Code = "01C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为Ypbpr模式", IsCheckInstructions = false, Code = "03C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置为VGA模式", IsCheckInstructions = false, Code = "05C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "复位RGB色调参数", IsCheckInstructions = false, Code = "00C25." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置DVI输入模式", IsCheckInstructions = false, Code = "06C02." });
                }
            }
            else if (boardInfo[2].Trim() == "HDBasT")
            {
                if (SelectedModel == "TS-9404HBI")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "复位RGB色调参数", IsCheckInstructions = false, Code = "00C25." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置背景为蓝屏", IsCheckInstructions = false, Code = "15C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置背景为黑屏", IsCheckInstructions = false, Code = "16C02." });
                }
            }
            else if (boardInfo[2].Trim() == "FIBER")
            {
                if (SelectedModel == "TS-9404HBI")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "恢复默认色调设置", IsCheckInstructions = false, Code = "50C20." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "复位RGB色调参数", IsCheckInstructions = false, Code = "00C25." });

                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置背景为蓝屏", IsCheckInstructions = false, Code = "15C02." });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置背景为黑屏", IsCheckInstructions = false, Code = "16C02." });
                }
                if (SelectedModel == "TS-9404FI")
                {

                }
            }
            else if (boardInfo[2].Trim() == "SDI")
            {
                if (SelectedModel == "TS-9404SI")
                {
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置外部音频输入", IsCheckInstructions = false, Code = "01C02." });//01C02.
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "设置内部音频输入", IsCheckInstructions = false, Code = "03C02." });//03C02
                }
            }
            else if (boardInfo[2].Trim() == "IP")
            {
                if (SelectedModel == "TS-9404IPI")
                {
                    int ChannelID = int.Parse(boardInfo[1]);
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "查询通道x的ip", IsCheckInstructions = false, Code = $"<^ip:{ChannelID}>" });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "查询通道x的DNS", IsCheckInstructions = false, Code = $"<^DNS:{ChannelID}>" });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "查询通道x的网光", IsCheckInstructions = false, Code = $"<^gateway:{ChannelID}>" });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "查询通道x的rtsp", IsCheckInstructions = false, Code = $"<^rtsp:{ChannelID}>" });
                    this.InstructionsList.Add(new BoardBingModel() { Instructions = "查询通道x的掩码", IsCheckInstructions = false, Code = $"<^netmask:{ChannelID}>" });
                }
            }
        }

        /// <summary>
        /// 发送蓝调拖动条命令
        /// </summary>
        public void SendDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{code}C28.");
            }
            else
            {
                string sendError = string.Empty;
                this._scanBoardListRequest.SendIndtruction($"{code}C28.", out sendError);
            }
        }

        /// <summary>
        /// 音量调节
        /// </summary>
        /// <param name="value"></param>
        public void SendVolumeDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{code}C29.");
            }
            else
            {

                string sendError = string.Empty;
                this._scanBoardListRequest.SendIndtruction($"{code}C29.", out sendError);
            }

        }

        /// <summary>
        /// 亮度调节
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
                this.SendDataBySerialPort($"{code}C21.");
            }
            else
            {

                string sendError = string.Empty;
                this._scanBoardListRequest.SendIndtruction($"{code}C21.", out sendError);
            }

        }

        /// <summary>
        /// 红色调
        /// </summary>
        /// <param name="value"></param>
        public void SendRedToneDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{code}C26.");
            }
            else
            {

                string sendError = string.Empty;
                this._scanBoardListRequest.SendIndtruction($"{code}C26.", out sendError);
            }

        }

        /// <summary>
        /// 绿色调
        /// </summary>
        /// <param name="value"></param>
        public void SendGreenToneDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{code}C27.");
            }
            else
            {

                string sendError = string.Empty;
                this._scanBoardListRequest.SendIndtruction($"{code}C27.", out sendError);
            }

        }

        /// <summary>
        /// 锐度
        /// </summary>
        /// <param name="value"></param>
        public void SendSharpnessDragDelta(double value)
        {
            string code = string.Empty;//xxC24.
            if (value < 10)
                code = ("0" + value).ToString();
            else
                code = value.ToString();
            if (GlobalContext.Current.CurrentVMLocator.MainVM.IsOpen == true && GlobalContext.Current.CurrentVMLocator.MainVM.Serial != null)
            {
                this.SendDataBySerialPort($"{code}C24.");
            }
            else
            {

                string sendError = string.Empty;
                this._scanBoardListRequest.SendIndtruction($"{code}C24.", out sendError);
            }

        }

        /// <summary>
        /// 饱和度
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
                this.SendDataBySerialPort($"{code}C23.");
            }
            else
            {

                string sendError = string.Empty;
                this._scanBoardListRequest.SendIndtruction($"{code}C23.", out sendError);
            }

        }

        /// <summary>
        /// 对比度
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
                this.SendDataBySerialPort($"{code}C22.");
            }
            else
            {

                string sendError = string.Empty;
                this._scanBoardListRequest.SendIndtruction($"{code}C22.", out sendError);

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
