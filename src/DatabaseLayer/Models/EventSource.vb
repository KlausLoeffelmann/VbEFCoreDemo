Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Public Class EventSource

    <Key>
    Public Property IdEventSource As Guid

    <Column(TypeName:="nvarchar(200)")>
    Public Property Name As String
    Public Property Description As String
    Public Property DateCreated As DateTimeOffset
    Public Property DateLastEdited As DateTimeOffset

End Class
