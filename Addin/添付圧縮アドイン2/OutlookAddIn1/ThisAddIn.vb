Imports Outlook = Microsoft.Office.Interop.Outlook
Imports Office = Microsoft.Office.Core
Imports System.Diagnostics
Imports System.Runtime.InteropServices

Public Class ThisAddIn

    Private Sub ThisAddIn_Startup() Handles Me.Startup

    End Sub

    Private Sub ThisAddIn_Shutdown() Handles Me.Shutdown

    End Sub

    Private Sub ThisAddIn_send(ByVal Item As Object, ByRef Cancel As Boolean) Handles Application.ItemSend
        Dim MailItem As Outlook.MailItem = TryCast(Item, Outlook.MailItem)
        Dim strSubject As String = MailItem.Subject

        If PStrSubject(0) = strSubject Then
            PStrSubject(0) = ""
            blnFlg(0) = True
        End If

        If PStrSubject(1) = strSubject Then
            PStrSubject(1) = ""
            blnFlg(1) = True
        End If

    End Sub

End Class
