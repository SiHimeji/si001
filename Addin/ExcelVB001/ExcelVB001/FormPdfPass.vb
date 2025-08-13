Imports System.IO
Imports iTextSharp.text.pdf
Imports System.Windows.Forms
Imports iTextSharp.text
Imports Microsoft.Office.Tools.Ribbon
Imports iTextSharp
'Imports iTextSharp.text.pdf
Imports System.Diagnostics

Public Class FormPdfPass
    Private Sub Button実行_Click(sender As Object, e As EventArgs) Handles Button実行.Click


        Dim ret As Boolean = SetPdfPassword(Me.TextBoxFileName.Text, Me.TextBoxPass1.Text, Me.TextBoxPass2.Text)
        If (ret) Then
            Me.Close()
        Else
            MsgBox("パスワード設定エラー")
        End If

    End Sub
    '/// <summary>
    '/// PDFファイルへパスワードを設定する
    '/// </summary>
    '/// <param name="sPdfPath">
    '/// パスワードを設定するPDF
    '/// </param>
    '/// <param name="sPass1">
    '/// ユーザパスワード
    '/// </param>
    '/// <param name="sPass2">
    '/// オーナーパスワード
    '/// </param>
    '/// <returns>
    '/// true:正常
    '/// false:異常
    '/// </returns> 
    Private Function SetPdfPassword(sPdfPath As String, sPass1 As String, sPass2 As String) As Boolean
        Dim bRet As Boolean = True
        Dim dtNow As DateTime = DateTime.Now


        Dim sLoclTm As String = String.Format("{0}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}{6}",
                                            dtNow.Year,
                                            dtNow.Month,
                                            dtNow.Day,
                                            dtNow.Hour,
                                            dtNow.Minute,
                                            dtNow.Second,
                                            dtNow.Millisecond)

        Dim sFilePath_PW As String = sPdfPath + "_" + sLoclTm + "_temp_pw.pdf"
        Dim objPdfReader As PdfReader = Nothing

        Try

            Dim byteUSER As Byte() = Encoding.ASCII.GetBytes(sPass1) '// ユーザパスワード
            Dim byteOWNER As Byte() = Encoding.ASCII.GetBytes(sPass2) '// オーナーパスワー

            If File.Exists(sPdfPath) = False Then
                Return False
            End If
            objPdfReader = New PdfReader(sPdfPath)

            Using objStreamOutput As Stream = New FileStream(sFilePath_PW,
                                                                 FileMode.Create,
                                                                 FileAccess.Write,
                                                                 FileShare.None)


                PdfEncryptor.Encrypt(objPdfReader,
                                     objStreamOutput,
                                     byteUSER,
                                      byteOWNER,
                                      PdfWriter.ALLOW_COPY Or PdfWriter.ALLOW_PRINTING,
                                      PdfWriter.STRENGTH128BITS)

                objPdfReader.Close()
                objPdfReader = Nothing
                File.Delete(sPdfPath)
                File.Move(sFilePath_PW, sPdfPath)

            End Using

        Catch ex As Exception
            bRet = False
        Finally
            'objPdfReader.Close()
        End Try

        Return bRet
    End Function

    Private Sub Buttonキャンセル_Click(sender As Object, e As EventArgs) Handles Buttonキャンセル.Click
        Me.Close()
    End Sub

    Private Sub Button検索_Click(sender As Object, e As EventArgs) Handles Button検索.Click
        'OpenFileDialogクラスのインスタンスを作成
        Dim ofd As New OpenFileDialog()

        'はじめのファイル名を指定する
        'はじめに「ファイル名」で表示される文字列を指定する
        ofd.FileName = "default.pdf"
        'はじめに表示されるフォルダを指定する
        '指定しない（空の文字列）の時は、現在のディレクトリが表示される
        ofd.InitialDirectory = ""
        '[ファイルの種類]に表示される選択肢を指定する
        '指定しないとすべてのファイルが表示される
        ofd.Filter = "PDFファイル(*.pdf;*.pdf)|*.pdf;*.pdf|すべてのファイル(*.*)|*.*"
        '[ファイルの種類]ではじめに選択されるものを指定する
        '2番目の「すべてのファイル」が選択されているようにする
        ofd.FilterIndex = 2
        'タイトルを設定する
        ofd.Title = "開くファイルを選択してください"
        'ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
        ofd.RestoreDirectory = True
        '存在しないファイルの名前が指定されたとき警告を表示する
        'デフォルトでTrueなので指定する必要はない
        ofd.CheckFileExists = True
        '存在しないパスが指定されたとき警告を表示する
        'デフォルトでTrueなので指定する必要はない
        ofd.CheckPathExists = True

        'ダイアログを表示する
        If ofd.ShowDialog() = DialogResult.OK Then
            'OKボタンがクリックされたとき、選択されたファイル名を表示する
            TextBoxFileName.Text = ofd.FileName
        End If


    End Sub
End Class