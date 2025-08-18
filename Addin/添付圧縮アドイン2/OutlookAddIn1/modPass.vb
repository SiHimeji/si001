Imports System.IO

Module modPass
    Public WorkPath As String
    Public MailItem As Outlook.MailItem
    Public copyMail As Outlook.MailItem
    Public ReadOnly PStr As String = "!#$%&'()[]+-*;:,.?_0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Public TxtTeikeibun As String = "C:\SI\定型文.txt"       'パスワード送信メール定型文
    Public blnFlg(2) As Boolean    'フラグ
    Public PStrSubject(2) As String

    Public Sub CreateWorkDir()
        Dim workdir As String
        workdir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        workdir += "\OlPassWdGen"
        If Directory.Exists(workdir) = False Then Directory.CreateDirectory(workdir)
        WorkPath = workdir
    End Sub

    Public Function GetWorkPath() As String
        Dim workdir As String
        workdir = WorkPath & "\work"
        If Directory.Exists(workdir) = False Then Directory.CreateDirectory(workdir)
        Return workdir
    End Function

    Public Function PassWord() As String
        Dim i As Integer
        Dim pwd As String = ""
        Dim rd As New Random()
        Try
            For i = 0 To 18 - 1
                pwd += PStr(rd.[Next](1, 82))
            Next
        Catch ex As Exception
            MsgBox("Message : " & ex.Message)
        End Try

        Return pwd
    End Function

    Public Function GetZipName() As String
        Dim fname As String
        Dim dt As DateTime = DateTime.Now
        'fname = dt.ToString("yyyyMMdd-HHmm") & ".zip"          '---k.s---
        fname = dt.ToString("yyyyMMdd") & ".zip"
        Return fname
    End Function

    Public Function SetText(ByVal strT As String, ByVal strZipName As String, ByVal strPw As String) As String
        Dim strText As String
        Dim line As String = ""
        Dim al As New Collections.ArrayList

        strText = ""

        'Using sr As StreamReader = New StreamReader("C:\定型文.txt", Encoding.GetEncoding("UTF-8"))
        Using sr As StreamReader = New StreamReader(TxtTeikeibun, Encoding.GetEncoding("UTF-8"))

            line = sr.ReadLine()
            Do Until line Is Nothing
                If InStr(line, "[t]") <> 0 Then
                    line = Replace(line, "[t]", strT)
                End If
                If InStr(line, "[fn]") <> 0 Then
                    line = Replace(line, "[fn]", strZipName)
                End If
                If InStr(line, "[pw]") <> 0 Then
                    line = Replace(line, "[pw]", strPw)
                End If
                al.Add(line)
                line = sr.ReadLine()
            Loop

        End Using

        For i As Integer = 0 To al.Count - 1
            strText += al.Item(i) & vbCrLf
        Next i


        Return strText
    End Function

    Public Sub DeleteSentMail(ByVal strSubject As String)

        Dim objOutlook As Outlook.Application = Nothing
        Dim objNamespace As Outlook.NameSpace = Nothing
        Dim objOutbox As Outlook.Folder = Nothing
        Dim objItems As Outlook.Items = Nothing
        Dim objMail As Outlook.MailItem = Nothing

        Try
            ' Outlookアプリケーションオブジェクトのインスタンス化
            objOutlook = New Outlook.Application()

            'ネームスペースの取得
            objNamespace = objOutlook.GetNamespace("MAPI")

            '送信トレイフォルダの取得
            objOutbox = objNamespace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderOutbox)

            '送信トレイ内のアイテムの取得
            objItems = objOutbox.Items

            '送信トレイ内のアイテムをループ処理
            For Each item As Object In objItems
                'アイテムがメールの場合
                If TypeOf item Is Outlook.MailItem Then
                    'メールオブジェクトを取得
                    objMail = CType(item, Outlook.MailItem)

                    '件名が指定した文字列と一致する場合
                    If objMail.Subject = strSubject Then
                        'アイテムを削除
                        objMail.Delete()
                    End If
                End If
            Next

        Catch ex As Exception
            WriteLine("エラーが発生しました: " & ex.Message)
        Finally
            'オブジェクトの解放
            If objMail IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(objMail)
            If objItems IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(objItems)
            If objOutbox IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(objOutbox)
            If objNamespace IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(objNamespace)
            If objOutlook IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(objOutlook)

            objOutlook = Nothing
            objNamespace = Nothing
            objOutbox = Nothing
            objItems = Nothing
            objMail = Nothing

            GC.Collect()
        End Try

    End Sub

    '-------------------------------------s.k--
    '圧縮した時に送信者の上にコメントを入れる
    '------------------------------------------
    Public Function SetBodyText(ByVal strB As String) As String
        Dim strTxt As String = "※パスワードは別便にて送付いたします。"
        Dim outstrB As String

        Dim zipStr As String = strB
        Dim splitStr() As String = zipStr.Split(vbCrLf)
        Dim stritem As String

        outstrB = ""
        For Each item As String In splitStr
            stritem = item.Replace(vbLf, "")
            If Left(stritem, 4) = "送信者：" Then
                outstrB = outstrB & strTxt & vbCrLf & vbCrLf & stritem & vbCrLf
            Else
                outstrB = outstrB & stritem & vbCrLf
            End If
        Next
        Return outstrB
    End Function

End Module
