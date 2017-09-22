Imports DatabaseLayer
Imports Microsoft.EntityFrameworkCore

Public Class Form1
    Private Async Sub btnAddDemoBaseData_Click(sender As Object, e As EventArgs) Handles btnAddDemoBaseData.Click

        Dim erContext As New EventRecorderContext

        AddHandler erContext.TriggerUpdate, Sub(s, eArgs)
                                                If eArgs.State = EntityState.Added Then
                                                    If eArgs.PropertyName = NameOf(ActionTarget.DateCreated) Then
                                                        eArgs.NewValue = DateTimeOffset.Now
                                                    End If
                                                    If eArgs.PropertyName = NameOf(ActionTarget.DateLastEdited) Then
                                                        eArgs.NewValue = DateTimeOffset.Now
                                                    End If
                                                ElseIf eArgs.State = EntityState.Modified Then
                                                    If eArgs.PropertyName = NameOf(ActionTarget.DateLastEdited) Then
                                                        eArgs.NewValue = DateTimeOffset.Now
                                                    End If
                                                End If
                                            End Sub

        'Find out, if there are action targets
        If Not Await ActionTargetDal.HasTableDataAsync(erContext) Then
            ActionTargetDal.AddDemoData(erContext)
        End If

        'Find out, if there are action targets
        If Not Await EventSourceDal.HasTableDataAsync(erContext) Then
            EventSourceDal.AddDemoData(erContext)
        End If

        Await erContext.SaveChangesAsync
    End Sub

    Private Async Sub btnPatchData_Click(sender As Object, e As EventArgs) Handles btnPatchData.Click

        Dim erContext As New EventRecorderContext

        Dim actionTarget = (From item In Await ActionTargetDal.GetAllActionTargetsAsync(erContext)
                            Where item.Name = "Kostenstelle 1" Or item.Name = "Kostenstelle 4").FirstOrDefault

        Dim idOfFoundActionTarget = actionTarget.IdActionTarget

        'The actionTarget we got is now completely detached.
        erContext.Dispose()

        'Now, let's change the actionTarget
        If actionTarget IsNot Nothing Then
            erContext = New EventRecorderContext

            If actionTarget.Name = "Kostenstelle 1" Then
                actionTarget.Name = "Kostenstelle 4"
            Else
                actionTarget.Name = "Kostenstelle 1"
            End If

            ActionTargetDal.PatchActionTargetName(erContext, actionTarget)
            erContext.Dispose()

            'Let's assert the change.
            erContext = New EventRecorderContext
            Dim changedActionTarget = Await ActionTargetDal.GetByIdAsync(erContext, idOfFoundActionTarget)
            Debug.Assert(changedActionTarget.Name = actionTarget.Name)
        End If

    End Sub
End Class
