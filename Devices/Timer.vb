Public Class Timer
    Inherits Device
    Public Property Timer As Int32
    Sub New(Machine As Machine)
        MyBase.New(Machine, "Timer")
    End Sub
End Class
