Public Class Keyboard
    Inherits Device
    Public Property Key As Integer
    Private Property Buffer As Byte()
    Private Property Mapping As Dictionary(Of Char, Integer)
    Sub New(Machine As Machine)
        MyBase.New(Machine, "Keyboard")
        Me.Buffer = New Byte(15) {}
        Me.Mapping = New Dictionary(Of Char, Integer)
        Me.Mapping.Add(Char.Parse("1"), &H0)
        Me.Mapping.Add(Char.Parse("2"), &H1)
        Me.Mapping.Add(Char.Parse("3"), &H2)
        Me.Mapping.Add(Char.Parse("4"), &H3)
        Me.Mapping.Add(Char.Parse("q"), &H4)
        Me.Mapping.Add(Char.Parse("w"), &H5)
        Me.Mapping.Add(Char.Parse("e"), &H6)
        Me.Mapping.Add(Char.Parse("r"), &H7)
        Me.Mapping.Add(Char.Parse("a"), &H8)
        Me.Mapping.Add(Char.Parse("s"), &H9)
        Me.Mapping.Add(Char.Parse("d"), &HA)
        Me.Mapping.Add(Char.Parse("f"), &HB)
        Me.Mapping.Add(Char.Parse("z"), &HC)
        Me.Mapping.Add(Char.Parse("x"), &HD)
        Me.Mapping.Add(Char.Parse("c"), &HE)
        Me.Mapping.Add(Char.Parse("v"), &HF)
    End Sub
    Public Sub KeyPressed(key As Char)
        If Me.Mapping.ContainsKey(key) Then
            Me.Buffer(Me.Mapping(key)) = &H1
            Me.Key = Me.Mapping(key)
        End If
    End Sub
    Public Sub KeyReleased(key As Char)
        If Me.Mapping.ContainsKey(key) Then
            Me.Buffer(Me.Mapping(key)) = &H0
            Me.Key = -1
        End If
    End Sub
    Public Function Wait() As Integer
        Dim pressed As Integer = -1
        For i As Integer = 0 To 15
            If Me.Buffer(i) <> &H0 Then pressed = i
        Next
        Return pressed
    End Function
    Public Shared Function ToChar(key As Keys) As Char
        Dim c As Char = ControlChars.NullChar
        If (key >= Keys.A) AndAlso (key <= Keys.Z) Then
            c = Chr(Convert.ToInt32(Char.Parse("a")) + CInt(key - Keys.A))
        ElseIf (key >= Keys.D0) AndAlso (key <= Keys.D9) Then
            c = Chr(Convert.ToInt32(Char.Parse("0")) + CInt(key - Keys.D0))
        End If
        Return c
    End Function
End Class
