Public Class Cmos
    Inherits Device
    Public Const Entry As Int16 = &H200
    Public Enum Opcodes As UInt16
        CLS = &HE0
        RET = &HEE
        JP = &H1000
        CLL = &H2000
        SE = &H3000
        SNE = &H4000
        SER = &H5000
        SETB = &H6000
        ADD = &H7000
        STT = &H8000
        LOR = &H8001
        LAND = &H8002
        LXOR = &H8003
        ADDR = &H8004
        SB = &H8005
        SHR = &H8006
        SUBN = &H8007
        SHL = &H800E
        SNER = &H9000
        SETI = &HA000
        JPR = &HB000
        RND = &HC000
        DRW = &HD000
        SKP = &HE09E
        SKPN = &HE0A1
        DT = &HF007
        KEY = &HF00A
        DTR = &HF015
        ST = &HF018
        ADDI = &HF01E
        LDI = &HF029
        LDB = &HF033
        STR = &HF055
        FILL = &HF065
    End Enum
    Sub New(Machine As Machine)
        MyBase.New(Machine, "CMOS")
    End Sub
End Class
