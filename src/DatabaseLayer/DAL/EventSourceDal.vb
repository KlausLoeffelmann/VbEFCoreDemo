Imports Microsoft.EntityFrameworkCore

Public Module EventSourceDal

    Public Function GetEventSourceCount(erContext As IEventRecorderContext) As Integer
        Return Aggregate item In erContext.EventSources
               Into Count
    End Function

    Public Async Function GetEventSourceCountAsync(erContext As IEventRecorderContext) As Task(Of Integer)
        'Find out, if there are action targets
        Return Await erContext.EventSources.CountAsync()
    End Function

    Public Function HasTableData(ercontext As IEventRecorderContext) As Boolean
        Return (From item In ercontext.EventSources
                Take 1).FirstOrDefault IsNot Nothing
    End Function

    Public Async Function HasTableDataAsync(ercontext As IEventRecorderContext) As Task(Of Boolean)
        Return Await (From item In ercontext.EventSources
                      Take 1).FirstOrDefaultAsync IsNot Nothing
    End Function

    Public Sub AddDemoData(erContext As IEventRecorderContext)

        erContext.EventSources.Add(New EventSource With {.IdEventSource = Guid.NewGuid,
                                                         .Name = "Max Mustermann",
                                                         .DateCreated = DateTimeOffset.Now,
                                                         .DateLastEdited = DateTimeOffset.Now})

        erContext.EventSources.Add(New EventSource With {.IdEventSource = Guid.NewGuid,
                                                             .Name = "John Doe",
                                                             .DateCreated = DateTimeOffset.Now,
                                                             .DateLastEdited = DateTimeOffset.Now})

        erContext.EventSources.Add(New EventSource With {.IdEventSource = Guid.NewGuid,
                                                             .Name = "Jane Doe",
                                                             .DateCreated = DateTimeOffset.Now,
                                                             .DateLastEdited = DateTimeOffset.Now})

        erContext.EventSources.Add(New EventSource With {.IdEventSource = Guid.NewGuid,
                                                             .Name = "Michelle Mustermann",
                                                             .DateCreated = DateTimeOffset.Now,
                                                             .DateLastEdited = DateTimeOffset.Now})
    End Sub

    Public Async Function AddDemoDataAsync(Of ContextType As {IEventRecorderContext, New})() As Task

        Dim erContext As New ContextType

        If Not Await HasTableDataAsync(erContext) Then
            erContext.ActionTargets.Add(New ActionTarget With {.IdActionTarget = Guid.NewGuid,
                                                           .Name = "Kostenstelle 1",
                                                           .DateCreated = DateTimeOffset.Now,
                                                           .DateLastEdited = DateTimeOffset.Now})
            erContext.ActionTargets.Add(New ActionTarget With {.IdActionTarget = Guid.NewGuid,
                                                           .Name = "Kostenstelle 2",
                                                           .DateCreated = DateTimeOffset.Now,
                                                           .DateLastEdited = DateTimeOffset.Now})
            erContext.ActionTargets.Add(New ActionTarget With {.IdActionTarget = Guid.NewGuid,
                                                           .Name = "Kostenstelle 3",
                                                           .DateCreated = DateTimeOffset.Now,
                                                           .DateLastEdited = DateTimeOffset.Now})
        End If

        Await erContext.SaveChangesAsync()

    End Function

End Module
