Imports System.IO
Imports System.Media
Imports System.Threading

Public Class Sound
    Inherits Device
    Public Property Timer As Int32
    Sub New(Machine As Machine)
        MyBase.New(Machine, "Sound")
    End Sub
    Public Sub Beep()
        '//TODO: Load wav, play beep until time exceeds
    End Sub
    Public Sub Update()
        If Me.Timer > 0 Then
            If Me.Timer = 1 Then Me.Beep()
            Me.Timer -= 1
        End If
    End Sub
End Class
