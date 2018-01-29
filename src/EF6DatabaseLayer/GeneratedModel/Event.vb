Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity.Spatial

<Table("Events")>
Partial Public Class [Event]
    <Key>
    Public Property IdEvent As Guid

    Public Property ActionTargetIdActionTarget As Guid?

    Public Property DateCreated As DateTimeOffset

    Public Property DateLastEdited As DateTimeOffset

    <StringLength(200)>
    Public Property Description As String

    Public Property Duration As Long?

    Public Property EndTime As TimeSpan?

    Public Property EventSourceIdEventSource As Guid?

    Public Property StartTime As TimeSpan?

    Public Property BookingDate As DateTimeOffset?

    Public Overridable Property ActionTarget As ActionTarget

    Public Overridable Property EventSource As EventSource
End Class
