Imports System.Threading
Imports System.Globalization
Public Class Cpu
    Inherits Device
    Public Property Address As UInt16
    Public Property Pointer As UInt16
    Public Property Register As Register
    Public Property Instruction As Instruction
    Public Property Stack As Stack(Of UInt16)
    Sub New(Machine As Machine)
        MyBase.New(Machine, "Cpu")
        Me.Address = &H0
        Me.Pointer = &H200
        Me.Register = New Register
        Me.Stack = New Stack(Of UInt16)(12)
    End Sub
    Public Sub Clock()
        Dim upper As Byte = Me.Machine.GetDevice(Of Memory).Read(Me.Pointer)
        Dim lower As Byte = Me.Machine.GetDevice(Of Memory).Read(Me.Pointer + 1)
        Me.Instruction = Me.GetInstruction(upper, lower)
        Me.SetPointer(2)
        Me.Execute(Me.Instruction)
        If Me.Machine.GetDevice(Of Timer).Timer > 0 Then
            Me.Machine.GetDevice(Of Timer).Timer -= 1
        End If
        Me.Machine.GetDevice(Of Sound).Update()
    End Sub
    Private Sub Execute(Instruction As Instruction)
        Select Case Instruction.Opcode
            Case Cmos.Opcodes.DRW : Me.Draw(Instruction)
            Case Cmos.Opcodes.CLS : Me.Machine.GetDevice(Of Display).Clear()
            Case Cmos.Opcodes.RET : Me.ReturnRoutine()
            Case Cmos.Opcodes.JP : Me.JumpTo(Instruction)
            Case Cmos.Opcodes.CLL : Me.CallRoutine(Instruction)
            Case Cmos.Opcodes.SE : Me.SkipNextRegisterVxEqualAddress(Instruction)
            Case Cmos.Opcodes.SNE : Me.SkipNextRegisterVxNotEqualAddress(Instruction)
            Case Cmos.Opcodes.SER : Me.SkipNextRegisterVxEqualVy(Instruction)
            Case Cmos.Opcodes.SETB : Me.SetVxToNn(Instruction)
            Case Cmos.Opcodes.ADD : Me.AddNnToVx(Instruction)
            Case Cmos.Opcodes.STT : Me.SetVxToVy(Instruction)
            Case Cmos.Opcodes.LOR : Me.SetVxToVxOrVy(Instruction)
            Case Cmos.Opcodes.LAND : Me.SetVxToVxAndVy(Instruction)
            Case Cmos.Opcodes.LXOR : Me.SetVxToVxXorVy(Instruction)
            Case Cmos.Opcodes.ADDR : Me.AddVyToVx(Instruction)
            Case Cmos.Opcodes.SB : Me.SubtractVyToVx(Instruction)
            Case Cmos.Opcodes.SHR : Me.ShiftVxRightByOne(Instruction)
            Case Cmos.Opcodes.SUBN : Me.SetVxToVyMinusVx(Instruction)
            Case Cmos.Opcodes.SHL : Me.ShiftVxLeftByOne(Instruction)
            Case Cmos.Opcodes.SNER : Me.SkipNextRegisterVxNotEqualVy(Instruction)
            Case Cmos.Opcodes.SETI : Me.SetIAddressNnn(Instruction)
            Case Cmos.Opcodes.JPR : Me.JumpToPlusV0(Instruction)
            Case Cmos.Opcodes.RND : Me.SetVxRandomNumberAndNn(Instruction)
            Case Cmos.Opcodes.SKP : Me.SkipIfKeyInVxPressed(Instruction)
            Case Cmos.Opcodes.SKPN : Me.SkipIfKeyInVxNotPressed(Instruction)
            Case Cmos.Opcodes.DT : Me.SetVxToDelayTimer(Instruction)
            Case Cmos.Opcodes.KEY : Me.StoreWaitingKeyInVx(Instruction)
            Case Cmos.Opcodes.DTR : Me.SetDelayTimerToVx(Instruction)
            Case Cmos.Opcodes.ST : Me.SetSoundTimerToVx(Instruction)
            Case Cmos.Opcodes.ADDI : Me.AddVxToI(Instruction)
            Case Cmos.Opcodes.LDI : Me.SetICharacterVx(Instruction)
            Case Cmos.Opcodes.LDB : Me.StoreInVxDecimalRegisterI(Instruction)
            Case Cmos.Opcodes.STR : Me.StoreV0ToVx(Instruction)
            Case Cmos.Opcodes.FILL : Me.FillV0ToVxFromMemory(Instruction)
                'Case Else : Me.Machine.Abort()
        End Select
    End Sub
    Private Sub SetPointer(Value As Int32, Optional Overwrite As Boolean = False)
        If (Overwrite) Then Me.Pointer = Convert.ToUInt16(Value) Else Me.Pointer = Convert.ToUInt16(Me.Pointer + Value)
    End Sub
    Private Sub ReturnRoutine()
        If Me.Stack.Count > 0 Then Me.SetPointer(Me.Stack.Pop, True) Else Me.SetPointer(Cmos.Entry, True)
    End Sub
    Private Sub JumpTo(instruction As Instruction)
        Me.SetPointer(instruction.NNN, True)
    End Sub
    Private Sub CallRoutine(instruction As Instruction)
        Me.Stack.Push(Me.Pointer)
        Me.SetPointer(instruction.NNN, True)
    End Sub
    Private Sub SkipNextRegisterVxEqualAddress(instruction As Instruction)
        If (Me.Register(instruction.X) = instruction.NN) Then Me.SetPointer(2)
    End Sub
    Private Sub SkipNextRegisterVxNotEqualAddress(instruction As Instruction)
        If Me.Register(instruction.X) <> instruction.NN Then Me.SetPointer(2)
    End Sub
    Private Sub SkipNextRegisterVxEqualVy(instruction As Instruction)
        If Me.Register(instruction.X) = Me.Register(instruction.Y) Then Me.SetPointer(2)
    End Sub
    Private Sub SkipNextRegisterVxNotEqualVy(instruction As Instruction)
        If Me.Register(instruction.X) <> Me.Register(instruction.Y) Then Me.SetPointer(2)
    End Sub
    Private Sub SetVxToNn(instruction As Instruction)
        Me.Register(instruction.X) = CByte(instruction.NN)
    End Sub
    Private Sub AddNnToVx(instruction As Instruction)
        Me.Register(instruction.X) = Me.Register(instruction.X) + CByte(instruction.NN)
    End Sub
    Private Sub SetVxToVy(instruction As Instruction)
        Me.Register(instruction.X) = Me.Register(instruction.Y)
    End Sub
    Private Sub SetVxToVxOrVy(instruction As Instruction)
        Me.Register(instruction.X) = Me.Register(instruction.X) Or Me.Register(instruction.Y)
        Me.Register(&HF) = 0
    End Sub
    Private Sub SetVxToVxAndVy(instruction As Instruction)
        Me.Register(instruction.X) = Me.Register(instruction.X) And Me.Register(instruction.Y)
        Me.Register(&HF) = 0
    End Sub
    Private Sub SetVxToVxXorVy(instruction As Instruction)
        Me.Register(instruction.X) = Me.Register(instruction.X) Xor Me.Register(instruction.Y)
        Me.Register(&HF) = 0
    End Sub
    Private Sub AddVyToVx(instruction As Instruction)
        Dim value As Integer = Me.Register(instruction.X) + Me.Register(instruction.Y)
        Me.Register(&HF) = CByte(If((value > Byte.MaxValue), &H1, &H0))
        Me.Register(instruction.X) = CByte(value)
    End Sub
    Private Sub SubtractVyToVx(instruction As Instruction)
        Me.Register(&HF) = CByte(If((Me.Register(instruction.X) > Me.Register(instruction.Y)), &H1, &H0))
        Me.Register(instruction.X) = Me.Register(instruction.X) - Me.Register(instruction.Y)
    End Sub
    Private Sub SetVxToVyMinusVx(instruction As Instruction)
        Me.Register(&HF) = CByte(If((Me.Register(instruction.Y) >= Me.Register(instruction.X)), &H1, &H0))
        Me.Register(instruction.X) = Me.Register(instruction.Y - Me.Register(instruction.X))
    End Sub
    Private Sub ShiftVxRightByOne(instruction As Instruction)
        Me.Register(&HF) = CByte(If((Me.Register(instruction.X) And &H1) = &H1, &H1, &H0))
        Me.Register(instruction.X) = CByte(Me.Register(instruction.X) / 2)
    End Sub
    Private Sub ShiftVxLeftByOne(instruction As Instruction)
        Me.Register(&HF) = CByte(If((Me.Register(instruction.X) And &H80) >> 7 = &H1, &H1, &H0))
        Me.Register(instruction.X) = CByte(Me.Register(instruction.X) * 2)
    End Sub
    Private Sub SetIAddressNnn(instruction As Instruction)
        Me.Address = Convert.ToUInt16(instruction.NNN)
    End Sub
    Private Sub JumpToPlusV0(instruction As Instruction)
        Me.SetPointer(instruction.NNN + Me.Register(&H0), True)
    End Sub
    Private Sub SetVxRandomNumberAndNn(instruction As Instruction)
        Static rnd As New Random(Environment.TickCount Xor Date.Now.Millisecond)
        Me.Register(instruction.X) = CByte(rnd.Next(255) And instruction.NN)
    End Sub
    Private Sub Draw(instruction As Instruction)
        Dim size As Integer = instruction.N, cnt As Integer = 0
        Dim sprite As New Sprite(size)
        For i As Integer = Me.Address To (Me.Address + size) - 1
            sprite.Buffer(cnt) = Me.Machine.GetDevice(Of Memory).Read(i)
            cnt += 1
        Next
        Me.Register(&HF) = Me.Machine.GetDevice(Of Display).Draw(Me.Register(instruction.X), Me.Register(instruction.Y), sprite.Buffer)
    End Sub
    Private Sub SkipIfKeyInVxPressed(instruction As Instruction)
        If Me.Register(instruction.X) = Me.Machine.GetDevice(Of Keyboard).Key Then
            Me.SetPointer(2)
        End If
    End Sub
    Private Sub SkipIfKeyInVxNotPressed(instruction As Instruction)
        If Me.Register(instruction.X) <> Me.Machine.GetDevice(Of Keyboard).Key Then
            Me.SetPointer(2)
        End If
    End Sub
    Private Sub SetVxToDelayTimer(instruction As Instruction)
        If Me.Machine.GetDevice(Of Timer).Timer < 0 Then
            Me.Machine.GetDevice(Of Timer).Timer = 0
        End If
        Me.Register(instruction.X) = CByte(Me.Machine.GetDevice(Of Timer).Timer)
    End Sub
    Private Sub StoreWaitingKeyInVx(instruction As Instruction)
        Dim key As Integer = Me.Machine.GetDevice(Of Keyboard).Wait
        If key > -1 Then Me.Register(instruction.X) = CByte(key)
    End Sub
    Private Sub SetDelayTimerToVx(instruction As Instruction)
        Me.Machine.GetDevice(Of Timer).Timer = Me.Register(instruction.X)
    End Sub
    Private Sub SetSoundTimerToVx(instruction As Instruction)
        Me.Machine.GetDevice(Of Sound).Timer = Me.Register(instruction.X)
    End Sub
    Private Sub AddVxToI(instruction As Instruction)
        If Me.Address + Me.Register(instruction.X) >= &H1000 Then
            Me.Address = Convert.ToUInt16(Memory.Size)
            Me.Register(&HF) = 1
        Else
            Me.Address += Me.Register(instruction.X)
        End If
    End Sub
    Private Sub SetICharacterVx(instruction As Instruction)
        Me.Address = Convert.ToUInt16(Me.Register(instruction.X) * 5)
    End Sub
    Private Sub StoreInVxDecimalRegisterI(instruction As Instruction)
        Dim value As Byte = Me.Register(instruction.X)
        Me.Machine.GetDevice(Of Memory).Write(Me.Address, CByte(value / 100))
        Me.Machine.GetDevice(Of Memory).Write(Me.Address + 1, CByte((value Mod 100) / 10))
        Me.Machine.GetDevice(Of Memory).Write(Me.Address + 2, CByte((value Mod 100) Mod 10))
    End Sub
    Private Sub StoreV0ToVx(instruction As Instruction)
        For i As Integer = 0 To instruction.X
            Me.Machine.GetDevice(Of Memory).Write(Me.Address + i, CByte(Me.Register(i)))
        Next
    End Sub
    Private Sub FillV0ToVxFromMemory(instruction As Instruction)
        For i As Integer = 0 To instruction.X
            Me.Register(i) = Me.Machine.GetDevice(Of Memory).Read(Me.Address + i)
        Next
    End Sub
    Public Function GetInstruction(x As Byte, y As Byte) As Instruction
        Dim value As Integer = BitConverter.ToInt32(New Byte() {y, x, 0, 0}, 0)
        Select Case value And &HF000
            Case &H0 : Return New Instruction() With {.Opcode = CUShort(value And &HFF), .NN = 0, .NNN = 0, .N = 0, .X = 0, .Y = 0}
            Case &H8000 : Return New Instruction() With {.Opcode = value And &HF00F, .NN = 0, .NNN = 0, .N = 0, .X = (value And &HF00) >> 8, .Y = (value And &HF0) >> 4}
            Case &HE000 : Return New Instruction() With {.Opcode = value And &HF0FF, .NN = 0, .NNN = 0, .N = 0, .X = (value And &HF00) >> 8, .Y = 0}
            Case &HF000 : Return New Instruction() With {.Opcode = value And &HF0FF, .NN = 0, .NNN = 0, .N = 0, .X = (value And &HF00) >> 8, .Y = 0}
            Case Else : Return New Instruction() With {.Opcode = value And &HF000, .NN = value And &HFF, .NNN = value And &HFFF, .N = value And &HF, .X = (value And &HF00) >> 8, .Y = (value And &HF0) >> 4}
        End Select
    End Function
End Class
