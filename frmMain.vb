Imports System.IO
Public Class frmMain
    Private Machine As Machine
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Initialize(True)
    End Sub
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (Me.Machine IsNot Nothing AndAlso Me.Machine.Running) Then
            Me.Machine.Abort()
            Me.Machine.Exit.WaitOne()
        End If
    End Sub
    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (Me.Machine IsNot Nothing AndAlso Me.Machine.Running) Then
            Me.Viewport.Focus()
            Me.Machine.GetDevice(Of Keyboard)().KeyPressed(Keyboard.ToChar(e.KeyData))
        End If
    End Sub
    Private Sub frmMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If (Me.Machine IsNot Nothing AndAlso Me.Machine.Running) Then
            Me.Machine.GetDevice(Of Keyboard)().KeyReleased(Keyboard.ToChar(e.KeyData))
        End If
    End Sub
    Private Sub lbFiles_DoubleClick(sender As Object, e As EventArgs) Handles lbFiles.DoubleClick
        If (Me.lbFiles.SelectedIndex <> -1) Then
            Me.Start(String.Format(".\roms\{0}", Me.lbFiles.SelectedItem.ToString))
        End If
    End Sub
    Private Sub Initialize(Autoload As Boolean)
        Me.lbFiles.Items.Clear()
        If (Directory.Exists(".\roms\")) Then
            For Each fn As String In Directory.GetFiles(".\Roms\", "*.bin")
                Me.lbFiles.Items.Add(Path.GetFileName(fn))
            Next
            Me.Start()
        End If
    End Sub
    Private Sub Start()
        If (Me.Machine IsNot Nothing AndAlso Me.Machine.Running) Then
            Me.Machine.Abort()
            Me.Machine.Exit.WaitOne()
        End If
        Me.Machine = New Machine
        Me.Machine.Load(My.Resources.Copyright, Me.Viewport)
        Me.Machine.Run()
    End Sub
    Private Sub Start(Filename As String)
        If (File.Exists(Filename)) Then
            If (Me.Machine IsNot Nothing AndAlso Me.Machine.Running) Then
                Me.Machine.Abort()
                Me.Machine.Exit.WaitOne()
            End If
            Me.Machine = New Machine
            Me.Machine.Load(Filename, Me.Viewport)
            Me.Machine.Run()
        End If
    End Sub
End Class
