Public Class Cmos
    Inherits Device
    Public Const Entrypoint As Integer = &H200
    Public Const CLS As Integer = &HE0
    Public Const RET As Integer = &HEE
    Public Const JP As Integer = &H1000
    Public Const CLL As Integer = &H2000
    Public Const SE As Integer = &H3000
    Public Const SNE As Integer = &H4000
    Public Const SER As Integer = &H5000
    Public Const SETB As Integer = &H6000
    Public Const ADD As Integer = &H7000
    Public Const STT As Integer = &H8000
    Public Const LOR As Integer = &H8001
    Public Const LAND As Integer = &H8002
    Public Const LXOR As Integer = &H8003
    Public Const ADDR As Integer = &H8004
    Public Const SB As Integer = &H8005
    Public Const SHR As Integer = &H8006
    Public Const SUBN As Integer = &H8007
    Public Const SHL As Integer = &H800E
    Public Const SNER As Integer = &H9000
    Public Const SETI As Integer = &HA000
    Public Const JPR As Integer = &HB000
    Public Const RND As Integer = &HC000
    Public Const DRW As Integer = &HD000
    Public Const SKP As Integer = &HE09E
    Public Const SKPN As Integer = &HE0A1
    Public Const DT As Integer = &HF007
    Public Const KEY As Integer = &HF00A
    Public Const DTR As Integer = &HF015
    Public Const ST As Integer = &HF018
    Public Const ADDI As Integer = &HF01E
    Public Const LDI As Integer = &HF029
    Public Const LDB As Integer = &HF033
    Public Const STR As Integer = &HF055
    Public Const FILL As Integer = &HF065
    Sub New(Machine As Machine)
        MyBase.New(Machine, "CMOS")
    End Sub
End Class
