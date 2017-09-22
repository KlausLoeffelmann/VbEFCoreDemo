Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<TriggerUpdate>
Public Class [Event]

    <Key>
    Public Property IdEvent As Guid
    Public Property ActionTarget As ActionTarget
    Public Property EventSource As EventSource
    Public Property StartTime As TimeSpan?
    Public Property EndTime As TimeSpan?
    Public Property BookingDate As DateTimeOffset?
    Public Property Duration As Long?

    <Column(TypeName:="nvarchar(200)")>
    Public Property Description As String

    <TriggerUpdate>
    Public Property DateCreated As DateTimeOffset
    <TriggerUpdate>
    Public Property DateLastEdited As DateTimeOffset

    Public Sub SetTimeSpan(startTime As TimeSpan, endTime As TimeSpan)
        Me.StartTime = startTime
        Me.EndTime = endTime
        CalculateDuration()
    End Sub

    Public Sub SetTimeSpan(startTime As TimeSpan, durationTicks As Long)
        SetTimeSpan(startTime, New TimeSpan(durationTicks))
    End Sub

    Private Sub CalculateDuration()
        If Not StartTime.HasValue OrElse Not EndTime.HasValue Then
            Duration = (EndTime - StartTime).Value.Ticks
        Else
            Duration = Nothing
        End If
    End Sub
End Class