Imports Outlook = Microsoft.Office.Interop.Outlook
Imports Office = Microsoft.Office.Core
Imports System.Diagnostics
Imports System.IO
Imports Microsoft.Office.Tools.Ribbon
Imports System.Runtime.InteropServices

'-----------------------------------------------------
'添付圧縮アドイン（7-ZIP用）
'
'【注意】本番用とテスト用で設定を切り替える
'　　　　パスワード自動生成　　　　："{"と"}"は使用しない（AppendFormatでエラーになる）
'　　　　パスワード送信メール定型文："C:\SI\定型文.txt"
'【本番用（32bit）】　
'   　　 SevenZManager："7-zip32.dll"
'        ターゲットCPU：「x86」
'【テスト用（64bit）】
'   　　 SevenZManager："7-zip64.dll"
'        ターゲットCPU：「x64」
'-----------------------------------------------------
Partial Public Class Ribbon1
    Private Sub Button1_Click(sender As Object, e As RibbonControlEventArgs) Handles Button1.Click
        Dim originalFile, compressFile, displayName, wp, pw As String
        Dim cnt, i As Integer
        Dim strBody As String

        Try
            '添付ファイルの数を取得
            Dim inspector = TryCast(MyBase.Context, Microsoft.Office.Interop.Outlook.Inspector)
            MailItem = TryCast(inspector.CurrentItem, Outlook.MailItem)
            cnt = MailItem.Attachments.Count
            If cnt = 0 Then
                MsgBox("添付ファイルが有りません", vbOKOnly & vbCritical, "圧縮エラー")
                Exit Sub
            End If

            wp = GetWorkPath() & "\"
            compressFile = wp & GetZipName()
            'cnt = MailItem.Attachments.Count
            pw = PassWord()

            '-----------------------------------------------------------
            Dim zip As SevenZManager = New SevenZManager()
            Dim aryFilePath As List(Of String) = New List(Of String)()
            If cnt = 1 Then
                Dim attachFile As Outlook.Attachment = MailItem.Attachments(1)
                originalFile = wp & attachFile.FileName
                attachFile.SaveAsFile(originalFile)
                aryFilePath.Add(originalFile)
                attachFile.Delete()
            Else
                'メールの添付ファイルを取得・削除
                For i = cnt - 1 To 0 Step -1
                    Dim attachFile As Outlook.Attachment = MailItem.Attachments(i + 1)
                    originalFile = wp & attachFile.FileName
                    attachFile.SaveAsFile(originalFile)
                    aryFilePath.Add(originalFile)
                    attachFile.Delete()
                Next
            End If

            'ファイルの圧縮
            zip.fnCompressFilesWithPassword(aryFilePath, compressFile, pw)
            '-----------------------------------------------------------

            displayName = Path.GetFileName(compressFile)
            MailItem.Attachments.Add(compressFile, DisplayName:=displayName)

            strBody = SetText(MailItem.Subject, displayName, pw)

            copyMail = MailItem.Copy()
            copyMail.BodyFormat = Outlook.OlBodyFormat.olFormatPlain
            copyMail.Subject = MailItem.Subject & " [パスワード通知]"
            copyMail.Body = strBody
            cnt = copyMail.Attachments.Count

            If cnt > 0 Then
                For i = cnt - 1 To 0
                    Dim attachFile As Outlook.Attachment = copyMail.Attachments(i + 1)
                    attachFile.Delete()
                Next
            End If


            PStrSubject(0) = MailItem.Subject
            PStrSubject(1) = copyMail.Subject

            copyMail.Display()

        Catch ex As System.Exception
            MsgBox("Message : " & ex.Message)
        End Try

    End Sub

    Private Sub Ribbon1_Load(ByVal sender As Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load
        If MailItem Is Nothing Then
            Dim inspector = TryCast(MyBase.Context, Microsoft.Office.Interop.Outlook.Inspector)
            MailItem = TryCast(inspector.CurrentItem, Outlook.MailItem)
        End If

        blnFlg(0) = False
        blnFlg(1) = False

        'ボタンは常に認識可とする
        'Button1.Enabled = (MailItem.Attachments.Count > 0)
        'Button2.Enabled = (MailItem.Attachments.Count > 0)
        Button1.Enabled = True
        Button2.Enabled = True

        AddHandler MailItem.BeforeAttachmentAdd, AddressOf Ribbon1_BeforeAttachmentAdd

        CreateWorkDir()
    End Sub

    Private Sub Ribbon1_BeforeAttachmentAdd()
        'ファイルが添付されたら実行される
        'ボタンは常に認識可とする
        'Button1.Enabled = (MailItem.Attachments.Count > 0)
        'Button2.Enabled = (MailItem.Attachments.Count > 0)
    End Sub

    Private Sub Ribbon1_Close(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Close
        'ディレクトリを削除する
        Dim wp As String = GetWorkPath()

        If Directory.Exists(wp) = True Then
            Directory.Delete(wp, True)
        End If

        If blnFlg(0) = False Then
            If PStrSubject(0) = "" Then
            Else
                DeleteSentMail(PStrSubject(0))
            End If
        End If
        If blnFlg(1) = False Then
            If PStrSubject(1) = "" Then
            Else
                DeleteSentMail(PStrSubject(1))
            End If
        End If

        '------------------------------------------------------------------------------
        '
        '　メール圧縮後に送信せずに作成メール閉じると、「送信トレイ」にゴミメールが残ってしまう
        '　送信した場合はゴミメールは残らない
        '　クローズの時に「送信トレイ」をクリアすると1件もメールが送信されなくなってしまう
        '　改善策がまだ見つかっていない！！
        ' 
        '------------------------------------------------------------------------------
        ''送信トレイ内を削除する
        '' Outlookアプリケーションを起動
        'Dim outlookApp As New Application()
        '' 名前空間を取得
        'Dim outlookNamespace As [NameSpace] = outlookApp.GetNamespace("MAPI")
        '' 送信トレイフォルダを取得
        'Dim sentItemsFolder As MAPIFolder = outlookNamespace.GetDefaultFolder(OlDefaultFolders.olFolderOutbox)
        '' 送信トレイ内のアイテムを削除
        'For i As Integer = sentItemsFolder.Items.Count To 1 Step -1
        '    Dim mailItem As Object = sentItemsFolder.Items(i)
        '    'MailItem.Delete()
        '    If TypeOf mailItem Is MailItem Then
        '        'mailItem.Delete()
        '        CType(mailItem, MailItem).Delete()
        '    End If
        'Next

    End Sub

    Private Sub Button2_Click(sender As Object, e As RibbonControlEventArgs) Handles Button2.Click
        Dim frmSyudo As New frmSyudo
        Dim originalFile, compressFile, displayName, wp, pw As String
        Dim cnt, i As Integer
        Dim strBody As String

        Try
            '添付ファイルの数を取得
            Dim inspector = TryCast(MyBase.Context, Microsoft.Office.Interop.Outlook.Inspector)
            MailItem = TryCast(inspector.CurrentItem, Outlook.MailItem)
            cnt = MailItem.Attachments.Count
            If cnt = 0 Then
                MsgBox("添付ファイルが有りません", vbOKOnly & vbCritical, "圧縮エラー")
                Exit Sub
            End If

            '「手動で圧縮」画面を開く
            frmSyudo.ShowDialog()

            '「実行」ボタンを押した時のみ圧縮を行う
            If frmSyudo.intRt <> 1 Then
                Exit Sub
            End If

            pw = frmSyudo.StrPassword

            displayName = frmSyudo.StrFileName

            wp = GetWorkPath() & "\"
            compressFile = wp & displayName
            cnt = MailItem.Attachments.Count

            '-----------------------------------------------------------
            Dim zip As SevenZManager = New SevenZManager()
            Dim aryFilePath As List(Of String) = New List(Of String)()

            'メールの添付ファイルを取得・削除
            For i = cnt - 1 To 0 Step -1
                Dim attachFile As Outlook.Attachment = MailItem.Attachments(i + 1)
                originalFile = wp & attachFile.FileName
                attachFile.SaveAsFile(originalFile)
                aryFilePath.Add(originalFile)
                attachFile.Delete()
            Next
            'ファイルの圧縮
            zip.fnCompressFilesWithPassword(aryFilePath, compressFile, pw)
            '-----------------------------------------------------------

            MailItem.Attachments.Add(compressFile, DisplayName:=displayName)

            strBody = SetText(MailItem.Subject, displayName, pw)

            Dim copyMail As Outlook.MailItem = MailItem.Copy()
            copyMail.BodyFormat = Outlook.OlBodyFormat.olFormatPlain
            copyMail.Subject = MailItem.Subject & " [パスワード通知]"
            copyMail.Body = strBody
            cnt = copyMail.Attachments.Count

            If cnt > 0 Then

                For i = cnt - 1 To 0
                    Dim attachFile As Outlook.Attachment = copyMail.Attachments(i + 1)
                    attachFile.Delete()
                Next
            End If
            copyMail.Display()
            'DeleteSentMail(MailItem.Subject)

        Catch ex As System.Exception
            MsgBox("Message : " & ex.Message)

        End Try

    End Sub
End Class