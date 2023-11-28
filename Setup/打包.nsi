; �ýű�ʹ�� HM VNISEdit �ű��༭���򵼲���

; ��װ�����ʼ���峣��
!define PRODUCT_NAME "ƴ��һ��PC�ͻ������"
!define PRODUCT_VERSION "V1.0.3"
!define PRODUCT_PUBLISHER "BL"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\SpliceMartix.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

SetCompressor lzma

; ------ MUI �ִ����涨�� (1.67 �汾���ϼ���) ------
!include "MUI.nsh"
!include "WordFunc.nsh"

; MUI Ԥ���峣��
!define MUI_ABORTWARNING
!define MUI_ICON "..\SpliceMartix\Resources\Image\logo.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; ��ӭҳ��
!insertmacro MUI_PAGE_WELCOME
; ���Э��ҳ��
!insertmacro MUI_PAGE_LICENSE "license.txt"
; ��װĿ¼ѡ��ҳ��
!insertmacro MUI_PAGE_DIRECTORY
;WordOperation
!insertmacro WordFind
; ��װ����ҳ��
!insertmacro MUI_PAGE_INSTFILES
; ��װ���ҳ��
!define MUI_FINISHPAGE_RUN "$INSTDIR\SpliceMartix.exe"
!insertmacro MUI_PAGE_FINISH

; ��װж�ع���ҳ��
!insertmacro MUI_UNPAGE_INSTFILES

; ��װ�����������������
!insertmacro MUI_LANGUAGE "SimpChinese"

; ��װԤ�ͷ��ļ�
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS
; ------ MUI �ִ����涨����� ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "ƴ��һ��PC�ͻ������.exe"

;����Ա������� 
RequestExecutionLevel admin ;Require admin rights on NT6+ (When UAC is turned on)

!include LogicLib.nsh


Function .onInit
FindProcDLL::FindProc "SpliceMartix.exe"
   Pop $R0
   IntCmp $R0 1 0 no_run
   MessageBox MB_OKCANCEL|MB_ICONSTOP  "��װ�����⵽ ${PRODUCT_NAME} �������С�$\r$\n$\r$\n��� ��ȷ���� ǿ�ƹر�${PRODUCT_NAME}��������װ��$\r$\n��� ��ȡ���� �˳���װ����" IDCANCEL Exit
   KillProcDLL::KillProc "SpliceMartix.exe"
   Sleep 1000
   FindProcDLL::FindProc "SpliceMartix.exe"
   Pop $R0
   IntCmp $R0 1 0 no_run
	Exit:
   Quit
   no_run:

FunctionEnd


InstallDir "$PROGRAMFILES\ƴ��һ��PC�ͻ������"
InstallDirRegKey HKLM "${PRODUCT_UNINST_KEY}" "UninstallString"
ShowInstDetails show
ShowUnInstDetails show
BrandingText "ƴ��һ��PC�ͻ������"
RequestExecutionLevel admin

Section "ƴ��һ��PC�ͻ������" SEC01
  ${WordFind} "$INSTDIR" "\" "-1" $R0
  ${If} $R0 != "ƴ��һ��PC�ͻ������"
  StrCpy $INSTDIR "$INSTDIR\ƴ��һ��PC�ͻ������"
  ${EndIf}
  
  SetOutPath "$INSTDIR"
  SetOverwrite ifnewer
  File /r "..\SpliceMartix\bin\Release\*.*"
  File "..\SpliceMartix\bin\Release\SpliceMartix.exe"
  CreateDirectory "$SMPROGRAMS\ƴ��һ��PC�ͻ������"
  CreateShortCut "$SMPROGRAMS\ƴ��һ��PC�ͻ������\ƴ��һ��PC�ͻ������.lnk" "$INSTDIR\SpliceMartix.exe"
  CreateShortCut "$DESKTOP\ƴ��һ��PC�ͻ������.lnk" "$INSTDIR\SpliceMartix.exe"
  ;ExecShell "runas" '"$INSTDIR\SpliceMartix.exe"' ; �Թ���Ա�������MyApp.exe
  WriteRegStr HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers" "$SMPROGRAMS\ƴ��һ��PC�ͻ������" "RUNASADMIN"
  WriteRegStr HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers" "$SMPROGRAMS\ƴ��һ��PC�ͻ������\ƴ��һ��PC�ͻ������.lnk" "RUNASADMIN"
  WriteRegStr HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers" "$DESKTOP\ƴ��һ��PC�ͻ������.lnk" "RUNASADMIN"
  WriteRegStr HKEY_LOCAL_MACHINE "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers" "$INSTDIR\SpliceMartix.exe" "RUNASADMIN"
  WriteRegStr HKCU "SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers" "$INSTDIR\SpliceMartix.exe" "RUNASADMIN"
SectionEnd
  RequestExecutionLevel admin

Section -.NET
Call GetNetFrameworkVersion
Pop $R1
;��ȡ�汾���жԱ�
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
;MessageBox MB_ICONINFORMATION|MB_OK "��ǰϵͳ�����汾��$R1 $\r$\n �����V4.7.2����ж��ԭ�л��������������У�"
 ${If} $2 < 7
 SetDetailsPrint textonly
 DetailPrint "���ڰ�װ .NET Framework 4.7.2"
 SetDetailsPrint listonly
 SetOutPath "$TEMP"
 SetOverwrite on
 File "NDP472-KB4054530-x86-x64-AllOS-ENU.exe"
 ExecWait '$TEMP\NDP472-KB4054530-x86-x64-AllOS-ENU.exe ' $R1
 Delete "$TEMP\NDP472-KB4054530-x86-x64-AllOS-ENU.exe"
 ${EndIf}
SectionEnd
;/q /norestart /ChainingPackage FullX64Bootstrapper

;ʹ�����´�������װMicrosoft Visual C++
Section CheckVCRedist
  ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\VisualStudio\10.0\VC\VCRedist\x86" "Installed"
  ${If} $0 <> 1
    SetDetailsPrint textonly
    DetailPrint "���ڰ�װ Microsoft Visual C++ 2010"
    SetDetailsPrint listonly
    SetOutPath "$TEMP"
    SetOverwrite on
    File "vcredist2010x86.exe"
    ExecWait '$TEMP\vcredist2010x86.exe' $R1
    Delete "$TEMP\vcredist2010x86.exe"
  ${EndIf}
SectionEnd

Section -AdditionalIcons
  CreateShortCut "$SMPROGRAMS\ƴ��һ��PC�ͻ������\Uninstall.lnk" "$INSTDIR\uninst.exe"
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
 *  �����ǰ�װ�����ж�ز���  *
 ******************************/

Section Uninstall
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\SpliceMartix.exe"

  Delete "$SMPROGRAMS\ƴ��һ��PC�ͻ������\Uninstall.lnk"
  Delete "$DESKTOP\ƴ��һ��PC�ͻ������.lnk"
  Delete "$SMPROGRAMS\ƴ��һ��PC�ͻ������\ƴ��һ��PC�ͻ������.lnk"

  RMDir "$SMPROGRAMS\ƴ��һ��PC�ͻ������"

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

#-- ���� NSIS �ű��༭�������� Function ���α�������� Section ����֮���д���Ա��ⰲװ�������δ��Ԥ֪�����⡣--#
Function GetNetFrameworkVersion
;��ȡ.Net Framework�汾֧��
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

;��ʼж��ʱ��飺
Function un.onInit

FindProcDLL::FindProc "SpliceMartix.exe"
   Pop $R0
   IntCmp $R0 1 0 no_run
   MessageBox MB_OKCANCEL|MB_ICONSTOP  "��װ�����⵽ ${PRODUCT_NAME} �������С�$\r$\n$\r$\n��� ��ȷ���� ǿ�ƹر�${PRODUCT_NAME}������ж�ء�$\r$\n��� ��ȡ���� �˳�ж�س���" IDCANCEL Exit
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
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) �ѳɹ��ش����ļ�����Ƴ���"
FunctionEnd
