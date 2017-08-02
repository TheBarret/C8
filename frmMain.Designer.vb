<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.txtDebug = New System.Windows.Forms.TextBox()
        Me.timeDebug = New System.Windows.Forms.Timer(Me.components)
        Me.lbFiles = New System.Windows.Forms.ListBox()
        Me.Viewport = New C8.Viewport()
        CType(Me.Viewport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDebug
        '
        Me.txtDebug.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDebug.Location = New System.Drawing.Point(203, 217)
        Me.txtDebug.Name = "txtDebug"
        Me.txtDebug.ReadOnly = True
        Me.txtDebug.Size = New System.Drawing.Size(385, 20)
        Me.txtDebug.TabIndex = 1
        '
        'timeDebug
        '
        Me.timeDebug.Interval = 10
        '
        'lbFiles
        '
        Me.lbFiles.FormattingEnabled = True
        Me.lbFiles.Location = New System.Drawing.Point(12, 12)
        Me.lbFiles.Name = "lbFiles"
        Me.lbFiles.Size = New System.Drawing.Size(185, 225)
        Me.lbFiles.TabIndex = 4
        '
        'Viewport
        '
        Me.Viewport.BackColor = System.Drawing.Color.White
        Me.Viewport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Viewport.Location = New System.Drawing.Point(203, 12)
        Me.Viewport.Name = "Viewport"
        Me.Viewport.Size = New System.Drawing.Size(384, 199)
        Me.Viewport.TabIndex = 5
        Me.Viewport.TabStop = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(601, 249)
        Me.Controls.Add(Me.Viewport)
        Me.Controls.Add(Me.lbFiles)
        Me.Controls.Add(Me.txtDebug)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "C8"
        CType(Me.Viewport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtDebug As System.Windows.Forms.TextBox
    Friend WithEvents timeDebug As System.Windows.Forms.Timer
    Friend WithEvents lbFiles As System.Windows.Forms.ListBox
    Friend WithEvents Viewport As C8.Viewport
End Class
