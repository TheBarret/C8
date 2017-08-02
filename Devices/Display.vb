Imports System.Drawing
Imports System.Drawing.Imaging

Public Class Display
    Inherits Device
    Public Const Width As Integer = 64
    Public Const Height As Integer = 32
    Public Const Resolution As Integer = &H6
    Public Property Invert As Boolean
    Public Property Redraw As Boolean
    Public Property buffer As Integer(,)
    Public Property Background As Brush
    Public Property Foreground As Brush
    Sub New(Machine As Machine)
        MyBase.New(Machine, "Display")
        Me.Invert = False
        Me.Redraw = False
        Me.Background = New SolidBrush(Color.FromArgb(139, 255, 0))
        Me.Foreground = New SolidBrush(Color.FromArgb(0, 0, 0))
        Me.buffer = New Integer(Display.Width - 1, Display.Height - 1) {}
        Me.Clear()
    End Sub
    Public Function Draw(x As Integer, y As Integer, buffer As Integer()) As Byte
        Dim px As Integer = x, py As Integer = y, value As Byte = &H0
        For i As Integer = 0 To buffer.Length - 1
            Dim line As Char() = Me.BitToString(buffer(i))
            For pixel As Integer = 0 To 7
                If line(pixel) = Char.Parse("1") Then
                    If px + pixel < Width AndAlso py + i < Height Then
                        If Me.buffer(px + pixel, py + i) = &H1 Then
                            value = &H1
                        End If
                        Me.buffer(px + pixel, py + i) = Me.buffer(px + pixel, py + i) Xor &H1
                    End If
                End If
            Next
        Next
        Me.Redraw = True
        Return value
    End Function
    Public Sub DrawFrame()
        SyncLock Me.Machine.Viewport
            Using bm As New Bitmap(Me.Machine.Viewport.Size.Width, Me.Machine.Viewport.Size.Height)
                Using g As Graphics = Graphics.FromImage(bm)
                    g.Clear(Color.FromArgb(139, 255, 0))
                    Dim pixel As Integer = 0
                    For y As Integer = 0 To Display.Height - 1
                        For x As Integer = 0 To Display.Width - 1
                            Dim brush As Brush = If((Me.buffer(x, y) = &H0), Me.Background, Me.Foreground)
                            g.FillRectangle(brush, x * Display.Resolution, y * Display.Resolution, Display.Resolution, Display.Resolution)
                        Next
                    Next
                End Using
                Me.Machine.Viewport.Image = CType(bm.Clone, Image)
            End Using
        End SyncLock
    End Sub
    Public Sub Clear()
        For y As Integer = 0 To Height - 1
            For x As Integer = 0 To Width - 1
                Me.buffer(x, y) = &H0
            Next
        Next
        Me.Redraw = True
    End Sub
    Private Function BitToString(value As Integer) As Char()
        Return Convert.ToString(value, 2).PadLeft(8, "0"c).ToCharArray()
    End Function
End Class
