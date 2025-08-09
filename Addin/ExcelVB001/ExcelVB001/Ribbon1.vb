Imports System.Windows.Forms
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports Microsoft.Office.Tools.Ribbon
Imports iTextSharp
'Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Diagnostics


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

End Class
