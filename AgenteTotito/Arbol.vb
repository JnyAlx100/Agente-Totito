Imports System.Net.Http.Json

Public Class Arbol
    Public raiz As Nodo
    Public matriz = {{"", "", ""}, {"", "", ""}, {"", "", ""}}
    Public turno As String
    Public simboloAgente = "x"
    Public simboloJugador = "o"

    'Constructor en donde se obtendra la matriz del juego, el turno del jugador
    'y se hara un arbol de posibles jugadas con respecto a ella
    Public Sub New(matriz As Array, turno As String)
        Me.matriz = matriz
        Me.turno = turno
        'Proceso para construir el arbol de posibilidades
        Me.raiz = obtenerJugadas(Me.matriz, Me.turno)
        'Codigo para mostrar la profundidad en cosola
        'Debug.WriteLine("Profundidad raiz: " + getProfundidad(raiz).ToString)
        'Debug.WriteLine("Hijos y su peso: ")
        'Debug.WriteLine("lenght de hijos de la raiz: " + raiz.hijos.Length.ToString)
        'Dim mayorPeso = raiz.hijos(0)
        'For index = 0 To raiz.hijos.Length - 1
        '    Debug.WriteLine("hijo" + index.ToString + ": " + Me.raiz.hijos(index).peso.ToString)
        '    If (raiz.hijos(index).peso > mayorPeso.peso) Then
        '        mayorPeso = raiz.hijos(index)
        '    End If
        'Next
        'Debug.WriteLine("hijo con mayor peso: ")
        'For index = 0 To 2
        '    Debug.WriteLine(mayorPeso.matriz(index, 0).ToString + "-" + mayorPeso.matriz(index, 1).ToString + "-" + mayorPeso.matriz(index, 2).ToString)
        'Next
        'Debug.WriteLine("peso: " + mayorPeso.peso.ToString)
    End Sub

    Function obtenerMejorMovimiento() As Nodo
        If raiz.hijos.Length = 0 Then
            Return raiz
        End If
        Dim movimiento = raiz.hijos(0)
        For x = 0 To raiz.hijos.Length - 1
            If (raiz.hijos(x).peso > movimiento.peso) Then
                movimiento = raiz.hijos(x)
            End If
        Next

        Return movimiento
    End Function

    'Function obtenerJugadas(matriz As Array) As Nodo
    '    Dim nodo As New Nodo(matriz)
    '    Dim hijos = {}

    '    'Si el turno es "x" entonces el oponente es "o" y viceversa
    '    Dim oponente As String
    '    If Me.turno = simboloAgente Then
    '        oponente = simboloJugador
    '    Else
    '        oponente = simboloAgente
    '    End If

    '    If juegoGanado(matriz, Me.turno) Or juegoEmpatado(matriz) Then
    '        nodo.peso = 1
    '        Return nodo
    '    End If

    '    If juegoGanado(matriz, oponente) Then
    '        nodo.peso = -1
    '        Return nodo
    '    End If

    '    For filas = 0 To 2
    '        For casillas = 0 To 2
    '            If matriz(filas, casillas) = "" Then
    '                'Se inicializa la matriz auxiliar
    '                Dim matrizAux = {{"", "", ""}, {"", "", ""}, {"", "", ""}}
    '                'Se anaden los elementos de la matriz a la matriz auxiliar para que no tengan la misma referencia
    '                For x = 0 To 2
    '                    For y = 0 To 2
    '                        matrizAux(x, y) = matriz(x, y)
    '                    Next
    '                Next
    '                'Se agrega el posible valor a la matriz auxiliar
    '                matrizAux(filas, casillas) = Me.turno
    '                For filas2 = 0 To 2
    '                    For casillas2 = 0 To 2
    '                        If matrizAux(filas2, casillas2) = "" Then
    '                            'Se crea una segunda matriz auxiliar para figurar el movimiento del oponente
    '                            Dim matrizAux2 = {{"", "", ""}, {"", "", ""}, {"", "", ""}}
    '                            'Se anaden los elementos de la matriz auxiliar a la matriz auxiliar 2 para que no tengan la misma referencia
    '                            For x = 0 To 2
    '                                For y = 0 To 2
    '                                    matrizAux2(x, y) = matrizAux(x, y)
    '                                Next
    '                            Next
    '                            matrizAux2(filas2, casillas2) = oponente
    '                            'Se anade la posibilidad a los hijos del nodo
    '                            Dim pos = hijos.Length
    '                            ReDim Preserve hijos(pos)
    '                            hijos(pos) = obtenerJugadas(matrizAux2)
    '                            nodo.peso += hijos(pos).peso
    '                        End If
    '                    Next
    '                Next
    '            End If
    '        Next
    '    Next

    '    nodo.hijos = hijos
    '    'Debug.WriteLine("----------------------------------------------")
    '    'Debug.WriteLine("Matriz del nodo: ")
    '    'For index = 0 To 2
    '    '    Debug.WriteLine(nodo.matriz(index, 0).ToString + "-" + nodo.matriz(index, 1).ToString + "-" + nodo.matriz(index, 2).ToString)
    '    'Next
    '    'Debug.WriteLine("Numero de hijos: " + nodo.hijos.Length.ToString)
    '    'Debug.WriteLine("Profundidad: " + getProfundidad(nodo).ToString)
    '    Return nodo
    'End Function

    Function obtenerJugadas(matriz As Array, turno As String) As Nodo
        Dim nodo As New Nodo(matriz)
        Dim hijos = {}

        'Si el turno es "x" entonces el oponente es "o" y viceversa
        Dim oponente As String
        If Me.turno = simboloAgente Then
            oponente = simboloJugador
        Else
            oponente = simboloAgente
        End If

        If juegoGanado(matriz, oponente) Then
            'Debug.WriteLine("el oponente ha ganado")
            nodo.peso = -1
            Return nodo
        End If

        If juegoEmpatado(matriz) Then
            nodo.peso = 1
            Return nodo
        End If

        If juegoGanado(matriz, Me.turno) Then
            nodo.peso = 1
            Return nodo
        End If

        For filas = 0 To 2
            For casillas = 0 To 2
                If matriz(filas, casillas) = "" Then
                    'Se inicializa la matriz auxiliar
                    Dim matrizAux = {{"", "", ""}, {"", "", ""}, {"", "", ""}}
                    'Se anaden los elementos de la matriz a la matriz auxiliar para que no tengan la misma referencia
                    For x = 0 To 2
                        For y = 0 To 2
                            matrizAux(x, y) = matriz(x, y)
                        Next
                    Next
                    'Se agrega el posible valor a la matriz auxiliar
                    matrizAux(filas, casillas) = turno
                    'Se determina a quien le tocara el siguiente movimiento
                    Dim siguienteTurno As String
                    If turno = simboloAgente Then
                        siguienteTurno = simboloJugador
                    Else
                        siguienteTurno = simboloAgente
                    End If
                    'Se crea un "arbol de posibilidades" para asumir el mejor movimiento del rival
                    Dim arbolOponente = New Arbol(matrizAux, siguienteTurno)

                    'Se anade la posibilidad a los hijos del nodo
                    Dim pos = hijos.Length
                    ReDim Preserve hijos(pos)
                    hijos(pos) = obtenerJugadas(arbolOponente.obtenerMejorMovimiento().matriz, turno)
                    hijos(pos).matriz = matrizAux
                    nodo.peso += hijos(pos).peso
                End If
            Next
        Next

        nodo.hijos = hijos
        'Debug.WriteLine("----------------------------------------------")
        'Debug.WriteLine("Matriz del nodo: ")
        'For index = 0 To 2
        '    Debug.WriteLine(nodo.matriz(index, 0).ToString + "-" + nodo.matriz(index, 1).ToString + "-" + nodo.matriz(index, 2).ToString)
        'Next
        'Debug.WriteLine("Numero de hijos: " + nodo.hijos.Length.ToString)
        'Debug.WriteLine("Profundidad: " + getProfundidad(nodo).ToString)
        Return nodo
    End Function

    Function juegoGanado(matriz As Array, turno As String) As Boolean
        'Debug.WriteLine("analizando si " + turno + " va a ganar ")
        Dim fila1 As Integer = 0
        Dim fila2 As Integer = 0
        Dim fila3 As Integer = 0

        Dim columna1 As Integer = 0
        Dim columna2 As Integer = 0
        Dim columna3 As Integer = 0

        Dim diagonal1 As Integer = 0
        Dim diagonal2 As Integer = 0


        For fila = 0 To 2
            For casilla = 0 To 2

                If fila = 0 And matriz(fila, casilla) = turno Then
                    fila1 += 1
                End If

                If fila = 1 And matriz(fila, casilla) = turno Then
                    fila2 += 1
                End If

                If fila = 2 And matriz(fila, casilla) = turno Then
                    fila3 += 1
                End If

                'For index = 0 To 2
                '    Debug.WriteLine(matriz(index, 0) + "   " + matriz(index, 1) + "   " + matriz(index, 2))
                'Next

                'Debug.WriteLine("casilla: " + casilla.ToString + " fila: " + fila.ToString + " turno " + turno.ToString)

                If casilla = 0 And matriz(fila, casilla) = turno Then
                    columna1 += 1
                End If

                If casilla = 1 And matriz(fila, casilla) = turno Then
                    columna2 += 1
                End If

                If casilla = 2 And matriz(fila, casilla) = turno Then
                    columna3 += 1
                End If

                If ((fila = 0 And casilla = 0) Or (fila = 1 And casilla = 1) Or (fila = 2 And casilla = 2)) And matriz(fila, casilla) = turno Then
                    diagonal1 += 1
                End If

                If ((fila = 2 And casilla = 0) Or (fila = 1 And casilla = 1) Or (fila = 0 And casilla = 2)) And matriz(fila, casilla) = turno Then
                    diagonal2 += 1
                End If
            Next
        Next

        If fila1 = 3 Or fila2 = 3 Or fila3 = 3 Or columna1 = 3 Or columna2 = 3 Or columna3 = 3 Or diagonal1 = 3 Or diagonal2 = 3 Then
            'Debug.WriteLine(turno + " gano... su matriz es:")
            'For index = 0 To 2
            '    Debug.WriteLine(matriz(index, 0).ToString + "-" + matriz(index, 1).ToString + "-" + matriz(index, 2).ToString)
            'Next
            Return True
        Else
            'Debug.WriteLine(turno + " no gano... su matriz es:")
            'For index = 0 To 2
            '    Debug.WriteLine(matriz(index, 0).ToString + "-" + matriz(index, 1).ToString + "-" + matriz(index, 2).ToString)
            'Next
            Return False
        End If
    End Function

    Function juegoEmpatado(matriz As Array) As Boolean
        Dim espaciosVacios = 0
        For filas = 0 To 2
            For casillas = 0 To 2
                If matriz(filas, casillas) = "" Then
                    espaciosVacios += 1
                End If
            Next
        Next
        If espaciosVacios = 0 And (Not juegoGanado(matriz, simboloAgente) And Not juegoGanado(matriz, simboloJugador)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Function getProfundidad(nodo As Nodo) As Integer
        If nodo Is Nothing Then
            Return 0
        End If
        Dim profundidad = 0
        For index = 0 To (nodo.hijos.Length - 1)
            Dim profundidadHijo = getProfundidad(nodo.hijos(index)) + 1
            If profundidadHijo > profundidad Then
                profundidad = profundidadHijo
            End If
        Next
        Return profundidad
    End Function

End Class
