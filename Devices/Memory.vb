Imports System.IO
Public Class Memory
    Inherits Device
    Public Const Size As Integer = &H1000
    Public Property Data As Byte()
    Sub New(Machine As Machine)
        MyBase.New(Machine, "Memory")
        Me.Data = New Byte(Memory.Size - 1) {}
        Me.Write(0, My.Resources.Sprites)
    End Sub
    Public Function Read(ptr As Integer) As Byte
        If (ptr >= 0 AndAlso ptr <= Memory.Size - 1) Then
            Return Me.Data(ptr)
        End If
        Throw New Exception(String.Format("Memory read address '0x{0}' out of range", ptr.ToString("X4")))
    End Function
    Public Function Read(ptr As Integer, len As Integer) As Byte()
        If (ptr >= 0 AndAlso (ptr + len) <= Memory.Size - 1) Then
            Dim rdata() As Byte = New Byte(len) {}
            Buffer.BlockCopy(Me.Data, ptr, rdata, 0, len)
            Return rdata
        End If
        Throw New Exception(String.Format("Memory read address '0x{0}' out of range", ptr.ToString("X4")))
    End Function
    Public Sub Write(ptr As Integer, data As Byte)
        If (ptr >= 0 AndAlso (ptr + 1) <= Memory.Size) Then
            Me.Data(ptr) = data
            Return
        End If
        Throw New Exception(String.Format("Memory write address '0x{0}' out of range", ptr.ToString("X4")))
    End Sub
    Public Sub Write(ptr As Integer, data() As Byte)
        If (ptr >= 0 AndAlso (ptr + data.Length) <= Memory.Size) Then
            data.CopyTo(Me.Data, ptr)
            Return
        End If
        Throw New Exception(String.Format("Memory write address '0x{0}' out of range", ptr.ToString("X4")))
    End Sub
End Class
