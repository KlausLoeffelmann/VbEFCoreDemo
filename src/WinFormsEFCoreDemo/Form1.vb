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
End Class
