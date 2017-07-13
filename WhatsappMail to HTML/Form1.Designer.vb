<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.ApriButton = New System.Windows.Forms.Button()
        Me.Filedialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ConvoName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ConvoParticipants = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ConvoPath = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
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
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ConvoName, Me.ConvoParticipants, Me.ConvoPath})
        Me.ListView1.Location = New System.Drawing.Point(12, 41)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(524, 251)
        Me.ListView1.TabIndex = 1
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ConvoName
        '
        Me.ConvoName.Text = "Nome"
        Me.ConvoName.Width = 120
        '
        'ConvoParticipants
        '
        Me.ConvoParticipants.Text = "Partecipanti"
        Me.ConvoParticipants.Width = 200
        '
        'ConvoPath
        '
        Me.ConvoPath.Text = "Percorso"
        Me.ConvoPath.Width = 300
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(548, 304)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.ApriButton)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ApriButton As Button
    Friend WithEvents Filedialog1 As OpenFileDialog
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ConvoName As ColumnHeader
    Friend WithEvents ConvoParticipants As ColumnHeader
    Friend WithEvents ConvoPath As ColumnHeader
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
End Class
