using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixCommandTool.Net.TCP
{
    internal sealed class RequestActionCodes
    {
        /// <summary>
        /// 扫描卡槽（查询各通道信息）
        /// </summary>
        public const string GetAllBoardName = "/:ScanPortType;";

        #region <In/005/HDMI  /1920x1080x60Hz!/V1.4-> <Out/009/HDMI  /1920x1080x60Hz!/V1.0->

        /// <summary>
        /// 激活[x1],[x2]输入通道为预设置状态
        /// </summary>
        public const string ActivationIn = "[x1]SETIN.";

        /// <summary>
        /// 激活[x1],[x2]输出通道为预设置状态
        /// </summary>
        public const string ActivationOut = "[x1]SETOUT.";

        #region 输出板卡参数设置设置
        /// <summary>
        /// 分辨率设置为 1024 X 768@60Hz 
        /// </summary>
        public const string Set1024768 = "01C08.";

        public const string Set128072060 = "02C08.";

        public const string Set12801024 = "03C08.";

        public const string Set1366768 = "04C08.";

        public const string Set14001050 = "05C08.";

        public const string Set1440900 = "07C08.";

        public const string Set1600120060 = "08C08.";

        public const string Set16801050 = "09C08.";

        public const string Set1920108050 = "13C08.";

        public const string Set1920108060 = "11C08.";

        public const string Set19201200 = "12C08";

        /// <summary>
        /// 设置DVI输出模式
        /// </summary>
        public const string SetDVIOutput = "07C02.";

        /// <summary>
        /// 设置HDMI输出模式
        /// </summary>
        public const string SetHDMIOutput = "08C02.";

        #endregion

        #region 输入输出都可以调节
        /// <summary>
        /// RGB恢复默认色调设置
        /// </summary>
        public const string SetRGBDefault = "50C20.";

        /// <summary>
        /// 亮度设置
        /// </summary>
        public const string SetBrightness = "xxC21.";

        /// <summary>
        /// 对比度设置
        /// </summary>
        public const string SetContrastRatio = "xxC22..";

        /// <summary>
        /// 饱和度设置
        /// </summary>
        public const string SetSaturation = "xxC23..";

        /// <summary>
        /// 锐度设置
        /// </summary>
        public const string SetSharpness = "xxC24.";

        /// <summary>
        /// 红色调
        /// </summary>
        public const string SetRedTone = "xxC26.";

        /// <summary>
        /// 绿色调
        /// </summary>
        public const string SetGreenTone = "xxC27.";

        /// <summary>
        /// 蓝色调
        /// </summary>
        public const string SetBlueTone = "xxC28.";

        /// <summary>
        /// 音量调节
        /// </summary>
        public const string SetModulation = "xxC29.";
        #endregion

        #region 输入板卡设置
        /// <summary>
        /// 设置外部音频输入
        /// </summary>
        public const string SetOuterAudioInput = "01C02.";

        /// <summary>
        /// 设置内部音频输入
        /// </summary>
        public const string SetInnerAudioInput = "03C02.";
        #endregion
        #endregion

        #region <In/005/HDMI  /1920x1080x60Hz!/V0.3-HDMI   > <Out/009/HDMI  /1920x1080x60Hz!/V0.1-HDMI   >

        public const string Set1600120050 = "10C08.";


        #endregion

        #region <In/009/VGA   /1920x1080x60Hz!/V1.0-> <Out/005/VGA   /1920x1080x60Hz!/V1.3->

        public const string SetCVBS = "01C02.";

        public const string SetYpbpr = "03C02.";

        public const string SetVGA = "05C02.";

        /// <summary>
        /// 复位RGB色调参数
        /// </summary>
        public const string RestRGB = "00C25.";

        /// <summary>
        /// 自动调整VGA输入
        /// </summary>
        public const string AutoVGAInput = "08C15.";
        #endregion

        #region <In/011/DVI   /1024x768x60Hz! /V1.0-> <Out/013/DVI   /1920x1080x60Hz!/V1.4-DVI    >

        /// <summary>
        /// 设置DVI输入模式
        /// </summary>
        public const string SetDVIInput = "06C02.";

        #endregion

        #region <Out/005/DVI   /1920x1080x60Hz!/V0.1-DVI    >

        public const string SetBlueScreen = "15C02.";

        public const string SetBlackScreen = "16C02.";
        #endregion

        #region <In/005/HDBasT/1920x1080x60Hz!/V1.3-> <Out/011/HDBasT/1920x1080x60Hz!/V0.3-HDB    >


        #endregion

        #region <In/013/HDBasT/1920x1080x60Hz!/V0.1-HDB    > <Out/013/HDBasT/1366x768x60Hz! /V0.3-HDB    >


        #endregion

        #region <In/009/SDI   /1920x1080x60Hz!/V0.1-SDI    >

        /// <summary>
        /// 分辨率设置为 1920x1080px50HZ
        /// </summary>
        public const string Set19201080px50 = "17C08.";

        /// <summary>
        /// 分辨率设置为 1920x1080px30HZ
        /// </summary>
        public const string Set19201080px30 = "18C08.";

        /// <summary>
        /// 分辨率设置为 1920x1080px25HZ
        /// </summary>
        public const string Set19201080px25 = "19C08.";

        /// <summary>
        /// 分辨率设置为 1280x720px50hz
        /// </summary>
        public const string Set1280720px50 = "20C08.";

        /// <summary>
        /// 分辨率设置为 1280x720x30hz
        /// </summary>
        public const string Set1280720x30 = "21C08.";

        /// <summary>
        /// 分辨率设置为 1920x1080ix60hz
        /// </summary>
        public const string Set19201080ix60 = "22C08.";

        /// <summary>
        /// 分辨率设置为 1920x1080ix50hz
        /// </summary>
        public const string Set19201080ix50 = "23C08.";

        #endregion

        #region <In/009/HDMI  /1920x1080x60Hz!/V1.3-HDMI> <Out/009/HDMI  /1920x1080x60Hz!/V1.2-HDMI>

        /// <summary>
        /// 设置为外部音频输出
        /// </summary>
        public const string SetOuterOutput = "11C02.";

        /// <summary>
        /// 设置为内部音频输出
        /// </summary>
        public const string SetInnerOutput = "12C02.";

        /// <summary>
        /// 音频正常
        /// </summary>
        public const string AudioOK = "13C02.";

        /// <summary>
        /// 音频静音
        /// </summary>
        public const string AudioMute = "14C02.";


        #endregion

    }
}
