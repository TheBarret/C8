Public Class Instruction
    Public Property N As Int32
    Public Property NN As Int32
    Public Property NNN As Int32
    Public Property X As Int32
    Public Property Y As Int32
    Public Property Opcode As Int32
    Public Property Name As String
    Public Overrides Function ToString() As String
        Return String.Format("0x{0} N{1} NN{2} NNN{3} X:{4} Y:{5}", Me.Opcode.ToString, Me.N.ToString, Me.NN.ToString, Me.NNN.ToString, Me.X.ToString, Me.Y.ToString)
    End Function
End Class
Public Class Register
    Inherits List(Of Byte)
    Sub New()
        Me.Reset()
    End Sub
    Public Sub Reset()
        Me.Clear()
        Me.AddRange(New Byte() {&H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0, &H0})
    End Sub
    Public Overrides Function ToString() As String
        Return String.Format("{0}", String.Join(",", Me.ToList.Select(Function(x) x.ToString("X2"))))
    End Function
End Class
Public Class Sprite
    Public Property Buffer As Integer()
    Sub New(Size As Integer)
        Me.Buffer = New Integer(Size - 1) {}
    End Sub
End Class