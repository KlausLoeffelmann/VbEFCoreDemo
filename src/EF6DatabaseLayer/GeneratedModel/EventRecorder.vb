Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq

Partial Public Class EventRecorder
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=EventRecorder")
    End Sub

    Public Overridable Property ActionTargets As DbSet(Of ActionTarget)
    Public Overridable Property Events As DbSet(Of [Event])
    Public Overridable Property EventSources As DbSet(Of EventSource)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)

    End Sub
End Class
