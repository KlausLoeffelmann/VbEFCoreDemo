Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<TriggerUpdate>
Public Class ActionTarget

    <Key>
    Public Property IdActionTarget As Guid

    <Column(TypeName:="nvarchar(200)")>
    Public Property Name As String

    <TriggerUpdate>
    Public Property DateCreated As DateTimeOffset
    <TriggerUpdate>
    Public Property DateLastEdited As DateTimeOffset

End Class
