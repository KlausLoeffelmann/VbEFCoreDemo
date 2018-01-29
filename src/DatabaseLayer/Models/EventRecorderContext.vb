Imports System.Runtime.CompilerServices
Imports System.Threading
Imports DatabaseLayer
Imports Microsoft.EntityFrameworkCore

Public Interface IEventRecorderContext
    Property EventSources As DbSet(Of EventSource)
    Property ActionTargets As DbSet(Of ActionTarget)
    Property Events As DbSet(Of [Event])
    Function SaveChanges() As Integer
    Function SaveChangesAsync(Optional cancellationToken As CancellationToken = Nothing) As Task(Of Integer)
End Interface

Public Class EventRecorderContext
    Inherits DbContext
    Implements IEventRecorderContext

    Public Event TriggerUpdate(sender As Object, e As TriggerUpdateEventArgs)

    Public Property EventSources As DbSet(Of EventSource) Implements IEventRecorderContext.EventSources
    Public Property ActionTargets As DbSet(Of ActionTarget) Implements IEventRecorderContext.ActionTargets
    Public Property Events As DbSet(Of [Event]) Implements IEventRecorderContext.Events

    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        'optionsBuilder.UseSqlServer("Server=SPNSURF3\SQLE2016;Database=EventRecorder;User ID=sa;Password=P@$$w0rd2407")
        optionsBuilder.UseSqlServer("Server=(localdb)\mssqllocaldb;Database=EventRecorder;Trusted_Connection=True;")
        'optionsBuilder.UseSqlServer("Server=W10SPNSB3\SQL2016;Database=EventRecorder;Trusted_Connection=True;")
    End Sub

    Public Overrides Function SaveChanges() As Integer Implements IEventRecorderContext.SaveChanges
        'EntityUpdater()
        Return MyBase.SaveChanges()
    End Function

    Public Overrides Async Function SaveChangesAsync(Optional cancellationToken As CancellationToken = Nothing) As Task(Of Integer) Implements IEventRecorderContext.SaveChangesAsync
        Await EntityUpdaterAsync()
        Return Await MyBase.SaveChangesAsync()
    End Function

    Private Async Function EntityUpdaterAsync() As Task
        Dim entries = Me.ChangeTracker.Entries
        For Each item In entries
            Dim currentEntity = item.Entity

            Dim triggerAttribute = TryCast((From attItem In currentEntity.GetType.GetCustomAttributes(True)
                                            Where (GetType(TriggerUpdateAttribute).IsAssignableFrom(attItem.GetType))).FirstOrDefault,
                                            TriggerUpdateAttribute)
            If triggerAttribute IsNot Nothing Then
                Dim affectedProps = From [property] In currentEntity.GetType().GetProperties
                                    Let hasTriggerAttribute = (From attItem In [property].GetCustomAttributes(True)
                                                               Where (GetType(TriggerUpdateAttribute).IsAssignableFrom(attItem.GetType))).FirstOrDefault IsNot Nothing
                                    Where hasTriggerAttribute

                For Each propItem In affectedProps
                    Dim triggerEventArgs = New TriggerUpdateEventArgs(currentEntity,
                                                                      propItem.property.Name,
                                                                      propItem.property.GetValue(currentEntity),
                                                                      item.State)
                    OnTriggerUpdate(triggerEventArgs)
                    If triggerEventArgs.OldValue IsNot triggerEventArgs.NewValue Then
                        Try
                            propItem.property.SetValue(currentEntity, triggerEventArgs.NewValue)
                        Catch ex As Exception
                            Throw New ArgumentException("On assigning a new value in the TriggerUpdate event, an exception occured." & vbCrLf &
                                                        $"{ex.Message}")

                        End Try

                    End If
                Next
            End If
        Next
        Await Task.Delay(0)
    End Function

    Protected Overridable Sub OnTriggerUpdate(eArgs As TriggerUpdateEventArgs)
        RaiseEvent TriggerUpdate(Me, eArgs)
    End Sub
End Class

Public Class TestEventRecordContext
    Implements IEventRecorderContext

    Sub New()

    End Sub

    Public Property EventSources As DbSet(Of EventSource) Implements IEventRecorderContext.EventSources

    Public Property ActionTargets As DbSet(Of ActionTarget) Implements IEventRecorderContext.ActionTargets

    Public Property Events As DbSet(Of [Event]) Implements IEventRecorderContext.Events

    Public Function SaveChanges() As Integer Implements IEventRecorderContext.SaveChanges
        Throw New NotImplementedException()
    End Function

    Public Function SaveChangesAsync(Optional cancellationToken As CancellationToken = Nothing) As Task(Of Integer) Implements IEventRecorderContext.SaveChangesAsync
        Throw New NotImplementedException()
    End Function
End Class

Public Class TestDbSet(Of t As Class)
    Inherits DbSet(Of t)

    Sub test()

    End Sub

End Class

Module IEventRecordContextExtender

    <ExtensionAttribute>
    Public Function FirstOrDefaultAsync(Of TSource)(source As IQueryable(Of TSource)) As Task(Of TSource)
        Return Task.FromResult(Of TSource)(source.FirstOrDefault)
    End Function

End Module
