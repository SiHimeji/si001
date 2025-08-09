Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Text

#Const x86 = False
#Const x64 = True

Public Class SevenZManager

#If x86 Then
    <Runtime.InteropServices.DllImport("7-zip32.dll")>
    Private Shared Sub SevenZip(hwnd As IntPtr, szCmdLine As String, szOutput As StringBuilder, dwSize As Integer)
        'ここには何も記述しません。
    End Sub
#End If
#If x64 Then
    <Runtime.InteropServices.DllImport("7-zip64.dll")>
    Private Shared Sub SevenZip(hwnd As IntPtr, szCmdLine As String, szOutput As StringBuilder, dwSize As Integer)
        'ここには何も記述しません。
    End Sub
#End If

    'Private lockObject As New Object()
    Public Function fnCompressFilesWithPassword(aryFilePath As List(Of String), s7zFilePath As String, sPassword As String) As Boolean

        Dim sbCmdLine As StringBuilder = New StringBuilder(1024)    ' コマンドライン文字列
        Dim sbOutput As StringBuilder = New StringBuilder(1024)     ' 7-zip32.dll出力文字

        sbCmdLine.AppendFormat(
                "a -tzip -hide -mmt=on -y -p" & sPassword & " " & s7zFilePath & " ")

        For Each sFilePath As String In aryFilePath
            sbCmdLine.AppendFormat("""" & sFilePath & """ ")
        Next

        Dim sCmdLine As String = sbCmdLine.ToString()
        '---------------------------------------------------------------------------------
        '圧縮実行
        '---------------------------------------------------------------------------------
        SevenZip(0, sCmdLine, sbOutput, sbOutput.Capacity)

        Return 0

    End Function

End Class
