Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class ActionTarget

    <Key>
    Public Property IdActionTarget As Guid

    <Column(TypeName:="nvarchar(200)")>
    Public Property Name As String
    Public Property DateCreated As DateTimeOffset
    Public Property DateLastEdited As DateTimeOffset

End Class
