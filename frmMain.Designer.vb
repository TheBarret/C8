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
        Me.lbFiles = New System.Windows.Forms.ListBox()
        Me.Clock = New System.Windows.Forms.Timer(Me.components)
        Me.Viewport = New C8.Viewport()
        CType(Me.Viewport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbFiles
        '
        Me.lbFiles.FormattingEnabled = True
        Me.lbFiles.Location = New System.Drawing.Point(4, 203)
        Me.lbFiles.Name = "lbFiles"
        Me.lbFiles.Size = New System.Drawing.Size(388, 108)
        Me.lbFiles.TabIndex = 4
        '
        'Clock
        '
        Me.Clock.Enabled = True
        '
        'Viewport
        '
        Me.Viewport.BackColor = System.Drawing.Color.White
        Me.Viewport.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Viewport.Location = New System.Drawing.Point(4, 2)
        Me.Viewport.Name = "Viewport"
        Me.Viewport.Size = New System.Drawing.Size(388, 195)
        Me.Viewport.TabIndex = 5
        Me.Viewport.TabStop = False
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 314)
        Me.Controls.Add(Me.Viewport)
        Me.Controls.Add(Me.lbFiles)
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

    End Sub
    Friend WithEvents lbFiles As System.Windows.Forms.ListBox
    Friend WithEvents Viewport As C8.Viewport
    Friend WithEvents Clock As System.Windows.Forms.Timer
End Class
