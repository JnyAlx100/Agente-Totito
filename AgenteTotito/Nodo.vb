Public Class Nodo
    Public matriz As Array
    Public peso As Integer
    Public hijos As Array

    Public Sub New(matriz As Array)
        Me.matriz = matriz
        Me.peso = 0
        Me.hijos = {}
    End Sub

End Class
