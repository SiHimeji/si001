Imports System.Diagnostics
'Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Windows.Forms
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Tools.Ribbon


Public Class Ribbon1

    Private Sub Ribbon1_Load(ByVal sender As System.Object, ByVal e As RibbonUIEventArgs) Handles MyBase.Load

    End Sub
    Private Function selectFolder()
        selectFolder = ""

        Using dialog As New FolderBrowserDialog()
            ' プロパティの設定
            With dialog
                ' ダイアログのタイトル
                .Description = "フォルダを選択してください。"
            End With

            ' ダイアログを表示し、戻り値が [OK] の場合は、選択したフォルダ名を表示する
            If dialog.ShowDialog() = DialogResult.OK Then
                selectFolder = dialog.SelectedPath
            End If
        End Using
    End Function


    Private Function setFileName()
        setFileName = ""

        Using dialog As New OpenFileDialog()

            ' プロパティの設定
            With dialog
                .CheckFileExists = False
                .Filter = "PDFファイル(*.pdf;*.PDF)|*.pdf;*.pdf|すべてのファイル(*.*)|*.*"
                .Title = "PDFファイルを選択してください。"
            End With
            If dialog.ShowDialog() = DialogResult.OK Then
                setFileName = dialog.FileName
            End If
        End Using


    End Function


    Private Sub Button1_Click(sender As Object, e As RibbonControlEventArgs) Handles ButtonItiran.Click

        Dim activeSheet As Microsoft.Office.Interop.Excel.Worksheet
        activeSheet = Globals.ThisAddIn.Application.ActiveSheet

        Dim folderName = selectFolder()
        If folderName = "" Then Return

        Dim y As Integer

        Dim di As New System.IO.DirectoryInfo(folderName)
        Dim files As System.IO.FileInfo() =
            di.GetFiles("*.PDF", System.IO.SearchOption.AllDirectories)

        y = 0
        For Each f As System.IO.FileInfo In files
            activeSheet.Cells(y + 1, 1).Value = f.FullName
            y = y + 1
        Next

    End Sub

    Private Sub Button2_Click(sender As Object, e As RibbonControlEventArgs) Handles Button2.Click

        Dim fillname As String = setFileName()
        If fillname <> "" Then JoinPdf(fillname)

    End Sub
    Private Function JoinPdf(sOutPDFPath As String) As Boolean

        Dim sStrList As List(Of String) = New List(Of String)()
        Dim bRet As Boolean = True
        Dim objITextDoc As Document
        Dim objPDFCopy As PdfCopy
        Dim nm As String
        Dim rg As String
        Dim y As Integer

        Dim activeSheet As Microsoft.Office.Interop.Excel.Worksheet = Globals.ThisAddIn.Application.ActiveSheet

        y = 1
        rg = "A" & y.ToString
        Do While activeSheet.Range(rg).Cells.Value <> ""
            nm = activeSheet.Range(rg).Cells.Value
            sStrList.Add(nm)
            y = y + 1
            rg = "A" & y.ToString
        Loop

        Try
            objITextDoc = New Document()
            objPDFCopy = New PdfCopy(objITextDoc, New FileStream(sOutPDFPath, FileMode.Create))
            objITextDoc.Open()

            objITextDoc.AddKeywords("")
            objITextDoc.AddAuthor("")
            objITextDoc.AddTitle("")
            objITextDoc.AddCreator("")
            objITextDoc.AddSubject("結合したPDFファイル")


            For Each pdfnm As String In sStrList
                Dim objPdfReader As PdfReader = New PdfReader(pdfnm)
                objPDFCopy.AddDocument(objPdfReader)
                objPdfReader.Close()
            Next

        Catch ex As Exception
            MessageBox.Show("PDF結合でエラー" & vbCr & ex.Message)
        Finally

            objITextDoc.Close()
            objPDFCopy.Close()
            objPDFCopy.Dispose()
        End Try

        MsgBox("結合終了" & vbCr & sOutPDFPath)

        Return True

    End Function

    Private Sub Buttonページ数_Click(sender As Object, e As RibbonControlEventArgs) Handles Buttonページ数.Click
        Dim activeSheet As Microsoft.Office.Interop.Excel.Worksheet = Globals.ThisAddIn.Application.ActiveSheet
        Dim nm As String
        Dim rg As String
        Dim y As Integer
        Dim pg As Integer

        y = 1
        rg = "A" & y.ToString
        Do While activeSheet.Range(rg).Cells.Value <> ""
            nm = activeSheet.Range(rg).Cells.Value
            pg = 0
            Dim doc As PdfReader = New PdfReader(nm)
            pg = doc.NumberOfPages
            doc.Close()
            doc.Dispose()
            rg = "B" & y.ToString
            activeSheet.Range(rg).Cells.Value = pg
            y = y + 1
            rg = "A" & y.ToString
        Loop

    End Sub
    '////// パスワード設定
    Private Sub Buttonパスワード_Click(sender As Object, e As RibbonControlEventArgs) Handles Buttonパスワード.Click
        Dim f1 As New FormPdfPass
        f1.ShowDialog()
    End Sub

    Private Sub Button分解_Click(sender As Object, e As RibbonControlEventArgs) Handles Button分解.Click

        Dim FileName As String = GetFileName()
        Dim Directory As String = System.IO.Path.GetDirectoryName(FileName)

        If SplitPDFfile(FileName, Directory) = False Then
            MessageBox.Show("エラーが発生したよ。")
        End If
    End Sub

    Private Function GetFileName()
        Dim ofd As New OpenFileDialog()
        ofd.FileName = "default.pdf"
        ofd.InitialDirectory = ""
        ofd.Filter = "PDFファイル(*.pdf;*.pdf)|*.pdf;*.pdf|すべてのファイル(*.*)|*.*"
        ofd.FilterIndex = 2
        ofd.Title = "開くファイルを選択してください"
        ofd.RestoreDirectory = True
        ofd.CheckFileExists = True
        ofd.CheckPathExists = True
        If ofd.ShowDialog() = DialogResult.OK Then
            GetFileName = ofd.FileName
        Else
            GetFileName = ""
        End If
    End Function


    '/// <summary>
    '    /// PDFファイルを1ページ毎に分割し、ファイル出力する。
    '    /// </summary>
    '    /// <param name="sOrigPDFPath">
    '    /// ]分割対象のPDFパス
    '    /// </param>
    '    /// <param name="sOutPutDir">
    '    /// 分割後出力ディレクトリパス
    '    /// </param>
    '    /// <returns>
    '    /// true:正常
    '    /// false:以上
    '    /// </returns>
    Private Function SplitPDFfile(sOrigPDFPath As String, sOutPutDir As String) As Boolean
        Dim bRet As Boolean = True                          '// 戻り値
        Dim sOrigFileName As String = Path.GetFileName(sOrigPDFPath)
        Dim objPdfReader As PdfReader = New PdfReader(sOrigPDFPath)
        Dim nCount As Integer

        For nCount = 1 To objPdfReader.NumberOfPages

            Dim objITextDoc As Document = Nothing
            Dim objPDFWriter As PdfWriter = Nothing
            Dim objPDFContByte As PdfContentByte = Nothing
            Dim objPDFImpPage As PdfImportedPage = Nothing

            Try
                Dim sNewPDFPath As String = sOutPutDir & "\" & nCount.ToString("D4") & "_" & sOrigFileName
                objITextDoc = New Document(objPdfReader.GetPageSize(nCount))
                objPDFWriter = PdfWriter.GetInstance(objITextDoc, New FileStream(sNewPDFPath, FileMode.OpenOrCreate))
                objPDFWriter.Open()
                'objPDFWriter.SetEncryption(PdfWriter.STRENGTH128BITS,       ' // 暗号化の強度（128bit暗号）
                '                                "pass1",                    ' // ユーザーパスワード 
                '                                "pass2",                    ' // オーナーパスワード
                '                                PdfWriter.ALLOW_COPY Or     ' // コピーを許可
                '                                PdfWriter.ALLOW_PRINTING)   ' // 印刷を許可
                objITextDoc.Open()
                objPDFContByte = objPDFWriter.DirectContent
                objITextDoc.NewPage()



                objPDFImpPage = objPDFWriter.GetImportedPage(objPdfReader, nCount)

                objPDFContByte.AddTemplate(objPDFImpPage, 0, 0)

                'objITextDoc.AddKeywords("PDF分割してみた、キーワード")
                'objITextDoc.AddAuthor("作者")
                'objITextDoc.AddTitle("PDFファイル分割")
                'objITextDoc.AddCreator("PDFファイル分割くん")
                'objITextDoc.AddSubject("分割してみた")
                'Debug.WriteLine(sNewPDFPath)

            Catch ex As Exception
                MessageBox.Show(ex.Message)
                bRet = False
            Finally
                objITextDoc.Close()
                objPDFWriter.Close()
            End Try
            If bRet = False Then
                Exit For
            End If
        Next
        objPdfReader.Close()
        Return bRet
    End Function




End Class
