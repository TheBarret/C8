Public Class Grid
    Implements IDisposable
    Public Property Size() As Size
    Public Property Cells() As Cell(,)
    Public Property Bounds() As Rectangle
    Sub New(Size As Size)
        Me.Size = Size
        Me.Cells = New Cell(Me.Size.Width - 1, Me.Size.Height - 1) {}
        For r As Integer = 0 To Me.Size.Height - 1
            For c As Integer = 0 To Me.Size.Width - 1
                Me.Cells(c, r) = New Cell(c, r)
            Next
        Next
    End Sub
    Public Sub SetDimensions(Rect As Rectangle)
        Me.Bounds = Rect
        Dim size As New Size(Bounds.Width \ Me.Size.Width, Bounds.Height \ Me.Size.Height)
        For r As Integer = 0 To Me.Size.Height - 1
            For c As Integer = 0 To Me.Size.Width - 1
                Me.Cells(c, r).Bounds = New Rectangle(Bounds.Left + (size.Width * c), Bounds.Top + (size.Height * r), size.Width, size.Height)
            Next
        Next
    End Sub
    Public Shared Sub Virtualize(dst As PictureBox, ParamArray Values() As UInt16)
        If (Values.Length > 0) Then
            Dim bytes As New List(Of Byte)
            For Each v As UInt16 In Values
                bytes.AddRange(BitConverter.GetBytes(v))
            Next
            Grid.Virtualize(dst, bytes.ToArray)
        End If
    End Sub
    Public Shared Sub Virtualize(dst As PictureBox, ParamArray Values() As Byte)
        Dim idx As Integer = 0
        Using Grid As New Grid(New Size(Values.Length, 1))
            Grid.SetDimensions(New Rectangle(0, 0, dst.Width, dst.Height))
            Using bm As New Bitmap(dst.Width, dst.Height)
                SyncLock bm
                    Using g As Graphics = Graphics.FromImage(bm)
                        For r As Integer = 0 To Grid.Size.Height - 1
                            For c As Integer = 0 To Grid.Size.Width - 1
                                g.FillRectangle(New SolidBrush(Color.FromArgb(Values(idx) Xor 255, Values(idx) Xor 255, Values(idx) Xor 255)), Grid.Cells(c, r).Bounds)
                                g.DrawRectangle(Pens.Black, Grid.Cells(c, r).Bounds)
                                idx += 1
                            Next
                        Next
                        g.DrawRectangle(Pens.Black, 0, 0, dst.Width - 1, dst.Height - 1)
                    End Using
                    dst.Image = CType(bm.Clone, Image)
                End SyncLock
            End Using
        End Using
    End Sub
    Public Class Cell
        Public Property Width As Integer
        Public Property Height As Integer
        Public Property Bounds As Rectangle
        Sub New(width As Integer, height As Integer)
            Me.Width = width
            Me.Height = height
        End Sub
    End Class
#Region "IDisposable Support"
    Private disposedValue As Boolean
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Me.Cells = Nothing
                Me.Size = Nothing
                Me.Bounds = Nothing
            End If
        End If
        Me.disposedValue = True
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class