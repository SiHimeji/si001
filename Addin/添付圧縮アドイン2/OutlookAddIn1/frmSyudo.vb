Public Class frmSyudo
    '---k.s start---
    Dim _FNAME As String = String.Empty
    Public Property FNAME As String
        Get
            FNAME = _FNAME
        End Get
        Set(value As String)
            _FNAME = value
        End Set
    End Property
    '---k.s end---

    Public Property StrFileName As String
    Public Property StrPassword As String
    Public Property intRt As Integer

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        intRt = 0               '返値クリア
        If MsgBox("キャンセルしますか？", vbYesNo, "手動圧縮") = vbYes Then
            intRt = vbYes       '6:yes
            Me.Close()
        Else
            intRt = vbYes       '7:no
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim pw As String

        pw = PassWord()

        txtPass.Text = pw
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        intRt = 0           '返値クリア
        If txtFile.Text = "" Then
            MsgBox("ファイル名が設定されていません。", vbOKOnly & vbCritical, "手動圧縮")
            Exit Sub
        End If
        If txtPass.Text = "" Then
            MsgBox("パスワードが設定されていません。", vbOKOnly & vbCritical, "手動圧縮")
            Exit Sub
        End If

        StrFileName = txtFile.Text & ".zip"
        StrPassword = txtPass.Text

        intRt = 1           '圧縮を行う
        Me.Close()
    End Sub

    Private Sub frmSyudo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '---k.s start---
        '1個目の添付ファイルを初期値としてセットする（拡張し無し）
        Dim intMojisuu As Integer = FNAME.Length
        Dim intI As Integer

        intI = FNAME.IndexOf(".")
        txtFile.Text = FNAME.Substring(0, intI)
        '---k.s end---
    End Sub
End Class