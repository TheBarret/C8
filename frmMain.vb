Imports System.IO
Public Class frmMain
    Private Machine As Machine
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.PopulateRomFiles()
    End Sub
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If (Me.Machine IsNot Nothing AndAlso Me.Machine.Running) Then
            Me.timeDebug.Stop()
            Me.Machine.Abort()
            Me.Machine.Exit.WaitOne()
        End If
    End Sub
    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (Me.Machine IsNot Nothing AndAlso Me.Machine.Running) Then
            Me.txtDebug.Focus()
            Me.Machine.GetDevice(Of Keyboard)().KeyPressed(Keyboard.ToChar(e.KeyData))
        End If
    End Sub
    Private Sub frmMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If (Me.Machine IsNot Nothing AndAlso Me.Machine.Running) Then
            Me.Machine.GetDevice(Of Keyboard)().KeyReleased(Keyboard.ToChar(e.KeyData))
        End If
    End Sub
    Private Sub timeDebug_Tick(sender As Object, e As EventArgs) Handles timeDebug.Tick
        Me.UpdateGUI(Me.Machine.GetDevice(Of Cpu).Pointer, Me.Machine.GetDevice(Of Cpu).Current, Me.Machine.GetDevice(Of Cpu).Register, Me.Machine.Latency)
    End Sub
    Private Sub UpdateGUI(Pointer As UInt16, Instruction As Instruction, Register As Register, Latency As Integer)
        If (Me.InvokeRequired) Then
            Me.Invoke(Sub() Me.UpdateGUI(Pointer, Instruction, Register, Latency))
        Else
            Me.txtDebug.Text = String.Format("{0} 0x{1}: {2}", Latency.ToString("X2"), Pointer.ToString("x"), Register.ToString)
        End If
    End Sub
    Private Sub PopulateRomFiles()
        Me.lbFiles.Items.Clear()
        For Each fn As String In Directory.GetFiles(".\Roms\", "*.bin")
            Me.lbFiles.Items.Add(Path.GetFileName(fn))
        Next
        If (Me.lbFiles.Items.Count > 0) Then
            Me.lbFiles.SelectedIndex = 0
            Dim fn As String = String.Format(".\roms\{0}", Me.lbFiles.SelectedItem.ToString)
            Me.Start(fn)
        End If
    End Sub
    Private Sub Start(Filename As String)
        If (File.Exists(Filename)) Then
            If (Me.Machine IsNot Nothing AndAlso Me.Machine.Running) Then
                Me.timeDebug.Stop()
                Me.Machine.Abort()
                Me.Machine.Exit.WaitOne()
            End If
            Me.Machine = New Machine
            Me.Machine.Load(Filename, Me.Viewport)
            Me.Machine.Run()
            Me.timeDebug.Start()
        End If
    End Sub
    Private Sub lbFiles_DoubleClick(sender As Object, e As EventArgs) Handles lbFiles.DoubleClick
        If (Me.lbFiles.SelectedIndex <> -1) Then
            Dim fn As String = String.Format(".\roms\{0}", Me.lbFiles.SelectedItem.ToString)
            Me.Start(fn)
        End If
    End Sub
End Class
