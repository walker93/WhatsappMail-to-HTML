<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
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

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ApriButton = New System.Windows.Forms.Button()
        Me.Filedialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'ApriButton
        '
        Me.ApriButton.Location = New System.Drawing.Point(12, 12)
        Me.ApriButton.Name = "ApriButton"
        Me.ApriButton.Size = New System.Drawing.Size(75, 23)
        Me.ApriButton.TabIndex = 0
        Me.ApriButton.Text = "Apri"
        Me.ApriButton.UseVisualStyleBackColor = True
        '
        'Filedialog1
        '
        Me.Filedialog1.Filter = "File txt|*.txt|Tutti i file|*.*"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 271)
        Me.Controls.Add(Me.ApriButton)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ApriButton As Button
    Friend WithEvents Filedialog1 As OpenFileDialog
End Class
