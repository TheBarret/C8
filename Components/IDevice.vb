Public Interface IDevice
    Property Name As String
    Property Machine As Machine
End Interface
Public MustInherit Class Device
    Implements IDevice
    Sub New(Machine As Machine, Device As String)
        Me.Machine = Machine
        Me.Name = Device
    End Sub
    Public Property Machine As Machine Implements IDevice.Machine
    Public Property Name As String Implements IDevice.Name
End Class