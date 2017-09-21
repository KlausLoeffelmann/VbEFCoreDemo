<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnAddDemoBaseData = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAddDemoBaseData
        '
        Me.btnAddDemoBaseData.Location = New System.Drawing.Point(401, 65)
        Me.btnAddDemoBaseData.Name = "btnAddDemoBaseData"
        Me.btnAddDemoBaseData.Size = New System.Drawing.Size(332, 77)
        Me.btnAddDemoBaseData.TabIndex = 0
        Me.btnAddDemoBaseData.Text = "Add Demo Base Data"
        Me.btnAddDemoBaseData.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 25.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1154, 904)
        Me.Controls.Add(Me.btnAddDemoBaseData)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnAddDemoBaseData As Button
End Class
