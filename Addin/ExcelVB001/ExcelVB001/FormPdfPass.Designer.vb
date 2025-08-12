<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormPdfPass
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button実行 = New System.Windows.Forms.Button()
        Me.Buttonキャンセル = New System.Windows.Forms.Button()
        Me.TextBoxFileName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button検索 = New System.Windows.Forms.Button()
        Me.TextBoxPass1 = New System.Windows.Forms.TextBox()
        Me.TextBoxPass2 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ファイル"
        '
        'Button実行
        '
        Me.Button実行.Location = New System.Drawing.Point(218, 83)
        Me.Button実行.Name = "Button実行"
        Me.Button実行.Size = New System.Drawing.Size(75, 23)
        Me.Button実行.TabIndex = 1
        Me.Button実行.Text = "実行"
        Me.Button実行.UseVisualStyleBackColor = True
        '
        'Buttonキャンセル
        '
        Me.Buttonキャンセル.Location = New System.Drawing.Point(400, 83)
        Me.Buttonキャンセル.Name = "Buttonキャンセル"
        Me.Buttonキャンセル.Size = New System.Drawing.Size(75, 23)
        Me.Buttonキャンセル.TabIndex = 2
        Me.Buttonキャンセル.Text = "キャンセル"
        Me.Buttonキャンセル.UseVisualStyleBackColor = True
        '
        'TextBoxFileName
        '
        Me.TextBoxFileName.Location = New System.Drawing.Point(69, 10)
        Me.TextBoxFileName.Name = "TextBoxFileName"
        Me.TextBoxFileName.Size = New System.Drawing.Size(325, 19)
        Me.TextBoxFileName.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "ユーザパスワード"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 12)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "オーナーパスワード"
        '
        'Button検索
        '
        Me.Button検索.Location = New System.Drawing.Point(400, 9)
        Me.Button検索.Name = "Button検索"
        Me.Button検索.Size = New System.Drawing.Size(75, 23)
        Me.Button検索.TabIndex = 6
        Me.Button検索.Text = "検索"
        Me.Button検索.UseVisualStyleBackColor = True
        '
        'TextBoxPass1
        '
        Me.TextBoxPass1.Location = New System.Drawing.Point(126, 33)
        Me.TextBoxPass1.Name = "TextBoxPass1"
        Me.TextBoxPass1.Size = New System.Drawing.Size(167, 19)
        Me.TextBoxPass1.TabIndex = 7
        '
        'TextBoxPass2
        '
        Me.TextBoxPass2.Location = New System.Drawing.Point(126, 58)
        Me.TextBoxPass2.Name = "TextBoxPass2"
        Me.TextBoxPass2.Size = New System.Drawing.Size(167, 19)
        Me.TextBoxPass2.TabIndex = 8
        '
        'FormPdfPass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(480, 137)
        Me.Controls.Add(Me.TextBoxPass2)
        Me.Controls.Add(Me.TextBoxPass1)
        Me.Controls.Add(Me.Button検索)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxFileName)
        Me.Controls.Add(Me.Buttonキャンセル)
        Me.Controls.Add(Me.Button実行)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FormPdfPass"
        Me.Text = "パスワード設定"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Button実行 As Windows.Forms.Button
    Friend WithEvents Buttonキャンセル As Windows.Forms.Button
    Friend WithEvents TextBoxFileName As Windows.Forms.TextBox
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents Button検索 As Windows.Forms.Button
    Friend WithEvents TextBoxPass1 As Windows.Forms.TextBox
    Friend WithEvents TextBoxPass2 As Windows.Forms.TextBox
End Class
