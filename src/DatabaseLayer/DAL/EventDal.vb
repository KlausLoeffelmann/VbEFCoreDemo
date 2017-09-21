Imports Microsoft.EntityFrameworkCore

Public Module EventDal

    Public Function HasTableData(ercontext As IEventRecorderContext) As Boolean
        Return (From item In ercontext.EventSources
                Take 1).FirstOrDefault IsNot Nothing
    End Function

    Public Async Function HasTableDataAsync(ercontext As IEventRecorderContext) As Task(Of Boolean)
        Return Await (From item In ercontext.EventSources
                      Take 1).FirstOrDefaultAsync IsNot Nothing
    End Function

    Public Async Function RandomEntriesAsync(rangeStart As DateTimeOffset,
                                            rangeEnd As DateTimeOffset,
                                            recordCount As Integer,
                                            eventSources As List(Of EventSource),
                                            actionTargets As List(Of ActionTarget)) _
                                            As Task(Of List(Of [Event]))

        Dim random As New Random(Date.Now.Millisecond)
        Dim returnList = New List(Of [Event])
        Dim daysInRange = CInt((rangeEnd - rangeStart).TotalDays)

        For count = 1 To recordCount
            Dim [event] = New [Event]
            With [event]
                .ActionTarget = actionTargets(random.Next(actionTargets.Count - 1))
                .EventSource = eventSources(random.Next(eventSources.Count - 1))
                Dim startTime = New TimeSpan(random.Next(23), random.Next(59), 0)
                Dim endTime = startTime + New TimeSpan(random.Next(12), random.Next(59), 0)
                Dim bookingDate = rangeStart.AddDays(random.Next(daysInRange))
                .BookingDate = bookingDate
                .SetTimeSpan(startTime, endTime)
            End With
        Next

        Return Await Task.FromResult(returnList)
    End Function

End Module
