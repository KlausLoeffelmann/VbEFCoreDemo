Imports Microsoft.EntityFrameworkCore

Public Class TriggerUpdateEventArgs
    Inherits EventArgs

    Sub New()
        MyBase.New
    End Sub

    Sub New(entity As Object, propertyName As String,
            oldValue As Object, state As EntityState)
        Me.Entity = entity
        Me.PropertyName = propertyName
        Me.OldValue = oldValue
        Me.NewValue = oldValue
        Me.State = state
    End Sub

    ReadOnly Property Entity As Object
    ReadOnly Property PropertyName As String
    ReadOnly Property OldValue As Object
    Property NewValue As Object
    Public ReadOnly Property State As EntityState
End Class
