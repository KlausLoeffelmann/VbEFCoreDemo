Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

Partial Public Class EventSource
    Public Sub New()
        Events = New HashSet(Of [Event])()
    End Sub

    <Key>
    Public Property IdEventSource As Guid

    Public Property DateCreated As DateTimeOffset

    Public Property DateLastEdited As DateTimeOffset

    <StringLength(200)>
    Public Property Name As String

    Public Property Description As String

    Public Overridable Property Events As ICollection(Of [Event])
End Class
