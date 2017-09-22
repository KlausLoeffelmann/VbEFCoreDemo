Imports Microsoft.EntityFrameworkCore

Public Module ActionTargetDal

    Public Function GetActionTargetCount(erContext As IEventRecorderContext) As Integer
        Return Aggregate item In erContext.ActionTargets
               Into Count
    End Function

    Public Async Function GetActionTargetCountAsync(erContext As IEventRecorderContext) As Task(Of Integer)
        'Find out, if there are action targets
        Return Await erContext.ActionTargets.CountAsync()
    End Function

    Public Function HasTableData(ercontext As IEventRecorderContext) As Boolean
        Return (From item In ercontext.ActionTargets
                Take 1).FirstOrDefault IsNot Nothing
    End Function

    Public Async Function HasTableDataAsync(ercontext As IEventRecorderContext) As Task(Of Boolean)
        Dim retVal = Await (From item In ercontext.ActionTargets
                            Take 1).FirstOrDefaultAsync
        Return retVal IsNot Nothing
    End Function

    Public Sub AddDemoData(erContext As IEventRecorderContext)

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
    End Sub

    Public Sub PatchActionTargetName(erContext As IEventRecorderContext,
                                      actionTarget As ActionTarget)


        DirectCast(erContext, EventRecorderContext).
            ChangeTracker.TrackGraph(actionTarget,
                Sub(item)
                    item.Entry.State = EntityState.Unchanged

                    If TryCast(item.Entry.Entity, ActionTarget) IsNot Nothing Then
                        DirectCast(erContext, EventRecorderContext).Entry(
                                TryCast(item.Entry.Entity, ActionTarget)).
                                    Property(NameOf(actionTarget.Name)).IsModified = True
                    End If
                End Sub)

        erContext.SaveChanges()

    End Sub

    Public Async Function GetAllActionTargetsAsync(erContext As IEventRecorderContext) As Task(Of List(Of ActionTarget))

        Return Await erContext.ActionTargets.ToListAsync

    End Function

    Public Async Function GetByIdAsync(erContext As IEventRecorderContext, id As Guid) As Task(Of ActionTarget)
        Return Await erContext.ActionTargets.Where(
            Function(item) item.IdActionTarget = id).FirstOrDefaultAsync
    End Function
End Module
