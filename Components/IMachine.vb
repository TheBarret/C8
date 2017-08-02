Imports System.IO
Imports System.Threading

Public Interface IMachine
    Property Viewport As PictureBox
    Function GetDevice(Of T)() As T
    Function HasDevice(Of T)() As Boolean
End Interface
Public Class Machine
    Inherits List(Of Device)
    Implements IMachine
    Public Property Latency As Integer
    Public Property Running As Boolean
    Public Property GameThread As Thread
    Public Property [Exit] As ManualResetEvent
    Public Property Viewport As PictureBox Implements IMachine.Viewport
    Public Sub Load(Filename As String, Viewport As PictureBox)
        If (File.Exists(Filename)) Then
            Me.Running = False
            Me.Viewport = Viewport
            '// Create devices
            Me.Add(New Sound(Me))
            Me.Add(New Timer(Me))
            Me.Add(New Memory(Me))
            Me.Add(New Cpu(Me))
            Me.Add(New Cmos(Me))
            Me.Add(New Keyboard(Me))
            Me.Add(New Display(Me))
            '// Load rom binary
            Using fs As FileStream = File.OpenRead(Filename)
                Dim data() As Byte = New Byte(Convert.ToInt32(fs.Length) - 1) {}
                fs.Read(data, 0, Convert.ToInt32(fs.Length) - 1)
                Me.GetDevice(Of Memory)().Write(Cmos.Entrypoint, data)
            End Using
        End If
    End Sub
    Public Sub Abort()
        Me.Running = False
    End Sub
    Public Sub Run()
        Me.Running = True
        Me.GameThread = New Thread(AddressOf Me.Worker)
        Me.GameThread.Start()
    End Sub
    Private Sub Worker()
        Dim count As Integer = 0, timer As New Stopwatch(), Display As Display = Me.GetDevice(Of Display)()
        Me.Exit = New ManualResetEvent(False)
        Do
            If Not timer.IsRunning Then
                count = 0
                timer.Reset()
                timer.Start()
            End If
            Me.GetDevice(Of Cpu).Clock()
            If (Display.Redraw) Then
                Display.DrawFrame()
                Display.Redraw = False
            End If
            count += 1
            If count >= (600 / 50) Then
                timer.Stop()
                If timer.ElapsedMilliseconds < (1000 / 50) Then
                    Me.Latency = CInt((1000 / 50) - timer.ElapsedMilliseconds)
                    Thread.Sleep(Me.Latency)
                End If
            End If
        Loop While Me.Running
        Me.Exit.Set()
    End Sub
    Public Function GetDevice(Of T)() As T Implements IMachine.GetDevice
        Dim dev As T = Me.Where(Function(x) TypeOf x Is T).Cast(Of T).FirstOrDefault
        If (dev IsNot Nothing) Then Return dev
        Throw New Exception(String.Format("Could not find device '{0}'", GetType(T).Name))
    End Function
    Public Function HasDevice(Of T)() As Boolean Implements IMachine.HasDevice
        Return Me.Where(Function(x) TypeOf x Is T).Any
    End Function
End Class