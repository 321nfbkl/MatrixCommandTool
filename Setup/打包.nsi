; 该脚本使用 HM VNISEdit 脚本编辑器向导产生

; 安装程序初始定义常量
!define PRODUCT_NAME "拼矩一体PC客户端软件"
!define PRODUCT_VERSION "V1.0.3"
!define PRODUCT_PUBLISHER "BL"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\SpliceMartix.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

SetCompressor lzma

; ------ MUI 现代界面定义 (1.67 版本以上兼容) ------
!include "MUI.nsh"
!include "WordFunc.nsh"

; MUI 预定义常量
!define MUI_ABORTWARNING
!define MUI_ICON "..\SpliceMartix\Resources\Image\logo.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; 欢迎页面
!insertmacro MUI_PAGE_WELCOME
; 许可协议页面
!insertmacro MUI_PAGE_LICENSE "license.txt"
; 安装目录选择页面
!insertmacro MUI_PAGE_DIRECTORY
;WordOperation
!insertmacro WordFind
; 安装过程页面
!insertmacro MUI_PAGE_INSTFILES
; 安装完成页面
!define MUI_FINISHPAGE_RUN "$INSTDIR\SpliceMartix.exe"
!insertmacro MUI_PAGE_FINISH

; 安装卸载过程页面
!insertmacro MUI_UNPAGE_INSTFILES

; 安装界面包含的语言设置
!insertmacro MUI_LANGUAGE "SimpChinese"

; 安装预释放文件
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS
; ------ MUI 现代界面定义结束 ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "拼矩一体PC客户端软件.exe"

;管理员身份运行 
RequestExecutionLevel admin ;Require admin rights on NT6+ (When UAC is turned on)

!include LogicLib.nsh


Function .onInit
FindProcDLL::FindProc "SpliceMartix.exe"
   Pop $R0
   IntCmp $R0 1 0 no_run
   MessageBox MB_OKCANCEL|MB_ICONSTOP  "安装程序检测到 ${PRODUCT_NAME} 正在运行。$\r$\n$\r$\n点击 “确定” 强制关闭${PRODUCT_NAME}，继续安装。$\r$\n点击 “取消” 退出安装程序。" IDCANCEL Exit
   KillProcDLL::KillProc "SpliceMartix.exe"
   Sleep 1000
   FindProcDLL::FindProc "SpliceMartix.exe"
   Pop $R0
   IntCmp $R0 1 0 no_run
	Exit:
   Quit
   no_run:

FunctionEnd


InstallDir "$PROGRAMFILES\拼矩一体PC客户端软件"
InstallDirRegKey HKLM "${PRODUCT_UNINST_KEY}" "UninstallString"
ShowInstDetails show
ShowUnInstDetails show
BrandingText "拼矩一体PC客户端软件"
RequestExecutionLevel admin

Section "拼矩一体PC客户端软件" SEC01
  ${WordFind} "$INSTDIR" "\" "-1" $R0
  ${If} $R0 != "拼矩一体PC客户端软件"
  StrCpy $INSTDIR "$INSTDIR\拼矩一体PC客户端软件"
  ${EndIf}
  
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File /r "..\SpliceMartix\bin\Release\*.*"
  File "..\SpliceMartix\bin\Release\SpliceMartix.exe"
  CreateDirectory "$SMPROGRAMS\拼矩一体PC客户端软件"
  CreateShortCut "$SMPROGRAMS\拼矩一体PC客户端软件\拼矩一体PC客户端软件.lnk" "$INSTDIR\SpliceMartix.exe"
  CreateShortCut "$DESKTOP\拼矩一体PC客户端软件.lnk" "$INSTDIR\SpliceMartix.exe"
  ;ExecShell "runas" '"$INSTDIR\SpliceMartix.exe"' ; 以管理员身份运行MyApp.exe
  WriteRegStr HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers" "$SMPROGRAMS\拼矩一体PC客户端软件" "RUNASADMIN"
  WriteRegStr HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers" "$SMPROGRAMS\拼矩一体PC客户端软件\拼矩一体PC客户端软件.lnk" "RUNASADMIN"
  WriteRegStr HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers" "$DESKTOP\拼矩一体PC客户端软件.lnk" "RUNASADMIN"
  WriteRegStr HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers" "$INSTDIR\SpliceMartix.exe" "RUNASADMIN"
  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers" "$INSTDIR\SpliceMartix.exe" "RUNASADMIN"
SectionEnd
  RequestExecutionLevel admin

Section -.NET
Call GetNetFrameworkVersion
Pop $R1
;获取版本进行对比
StrCpy $0 $R1
StrCpy $1 0
loop:
    IntOp $1 $1 + 1 ; Character offset, from end of string
    StrCpy $2 $0 1 $1 ; Read 1 character into $2, -$1 offset from end
    StrCmp $2 '.' found
    StrCmp $2 '' stop loop ; No more characters or try again
found:
    IntOp $1 $1 + 1 ; Don't include / in extracted string
stop:
StrCpy $2 $0 1 $1 ; We know the length, extract final string part
;MessageBox MB_ICONINFORMATION|MB_OK "当前系统环境版本：$R1 $\r$\n 如低于V4.7.2，请卸载原有环境，再重新运行！"
 ${If} $2 < 7
 SetDetailsPrint textonly
 DetailPrint "正在安装 .NET Framework 4.7.2"
 SetDetailsPrint listonly
 SetOutPath "$TEMP"
 SetOverwrite on
 File "NDP472-KB4054530-x86-x64-AllOS-ENU.exe"
 ExecWait '$TEMP\NDP472-KB4054530-x86-x64-AllOS-ENU.exe ' $R1
 Delete "$TEMP\NDP472-KB4054530-x86-x64-AllOS-ENU.exe"
 ${EndIf}
SectionEnd
;/q /norestart /ChainingPackage FullX64Bootstrapper

;使用以下代码来安装Microsoft Visual C++
Section CheckVCRedist
  ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\VisualStudio\10.0\VC\VCRedist\x86" "Installed"
  ${If} $0 <> 1
    SetDetailsPrint textonly
    DetailPrint "正在安装 Microsoft Visual C++ 2010"
    SetDetailsPrint listonly
    SetOutPath "$TEMP"
    SetOverwrite on
    File "vcredist2010x86.exe"
    ExecWait '$TEMP\vcredist2010x86.exe' $R1
    Delete "$TEMP\vcredist2010x86.exe"
  ${EndIf}
SectionEnd

Section -AdditionalIcons
  CreateShortCut "$SMPROGRAMS\拼矩一体PC客户端软件\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\SpliceMartix.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\SpliceMartix.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd

/******************************
 *  以下是安装程序的卸载部分  *
 ******************************/

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\SpliceMartix.exe"

  Delete "$SMPROGRAMS\拼矩一体PC客户端软件\Uninstall.lnk"
  Delete "$DESKTOP\拼矩一体PC客户端软件.lnk"
  Delete "$SMPROGRAMS\拼矩一体PC客户端软件\拼矩一体PC客户端软件.lnk"

  RMDir "$SMPROGRAMS\拼矩一体PC客户端软件"

  ;RMDir /r "$INSTDIR\libs"
  ;RMDir /r "$INSTDIR\Files"
  RMDir /r "$INSTDIR\Files"
  RMDir /r "$INSTDIR\libs"
  RMDir /r "$INSTDIR\Resources"

  RMDir /r "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd

#-- 根据 NSIS 脚本编辑规则，所有 Function 区段必须放置在 Section 区段之后编写，以避免安装程序出现未可预知的问题。--#
Function GetNetFrameworkVersion
;获取.Net Framework版本支持
Push $1
Push $0
ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full""Install"
ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full""Version"
StrCmp $0 1 KnowNetFrameworkVersion +1
ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5""Install"
ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5""Version"
StrCmp $0 1 KnowNetFrameworkVersion +1
ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.0\Setup" "InstallSuccess"
ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.0\Setup" "Version"
StrCmp $0 1 KnowNetFrameworkVersion +1
ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727" "Install"
ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v2.0.50727" "Version"
StrCmp $1 "" +1 +2
StrCpy $1 "2.0.50727.832"
StrCmp $0 1 KnowNetFrameworkVersion +1
ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v1.1.4322" "Install"
ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v1.1.4322" "Version"
StrCmp $1 "" +1 +2
StrCpy $1 "1.1.4322.573"
StrCmp $0 1 KnowNetFrameworkVersion +1
ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\.NETFramework\policy\v1.0""Install"
ReadRegDWORD $1 HKLM "SOFTWARE\Microsoft\.NETFramework\policy\v1.0""Version"
StrCmp $1 "" +1 +2
StrCpy $1 "1.0.3705.0"
StrCmp $0 1 KnowNetFrameworkVersion +1
StrCpy $1 "not .NetFramework"
KnowNetFrameworkVersion:
Pop $0
Exch $1
FunctionEnd

;开始卸载时检查：
Function un.onInit

FindProcDLL::FindProc "SpliceMartix.exe"
   Pop $R0
   IntCmp $R0 1 0 no_run
   MessageBox MB_OKCANCEL|MB_ICONSTOP  "安装程序检测到 ${PRODUCT_NAME} 正在运行。$\r$\n$\r$\n点击 “确定” 强制关闭${PRODUCT_NAME}，继续卸载。$\r$\n点击 “取消” 退出卸载程序。" IDCANCEL Exit
   KillProcDLL::KillProc "SpliceMartix.exe"
   Sleep 1000
   FindProcDLL::FindProc "SpliceMartix.exe"
   Pop $R0
   IntCmp $R0 1 0 no_run
   Exit:
   Quit
   no_run:
FunctionEnd

Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) 已成功地从您的计算机移除。"
FunctionEnd
