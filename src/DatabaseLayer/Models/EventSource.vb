Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<TriggerUpdate>
Public Class EventSource

    <Key>
    Public Property IdEventSource As Guid

    <Column(TypeName:="nvarchar(200)")>
    Public Property Name As String
    Public Property Description As String

    <TriggerUpdate>
    Public Property DateCreated As DateTimeOffset
    <TriggerUpdate>
    Public Property DateLastEdited As DateTimeOffset

End Class
