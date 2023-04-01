Imports System.Drawing.Drawing2D
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms.VisualStyles

Public Class Form1
    'Se establece el simbolo de cada jugador ("x" o "o")
    Dim simboloAgente As String = "x"
    Dim simboloJugador As String = "o"
    Dim matrizJuego As Array = {{"o", "", ""}, {"", "", ""}, {"", "", ""}}

    Private Sub imgCasilla1_Click(sender As Object, e As EventArgs) Handles imgCasilla1.Click
        matrizJuego(0, 0) = "o"
        Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego)
        matrizJuego = mejorMovimiento(2)(0)
        'Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego, simboloAgente)
        'matrizJuego = mejorMovimiento(3)(0)
        'Debug.WriteLine("Tus posibilidades de victoria son de " + mejorMovimiento(3)(1).ToString)
        actualizarTabla()
    End Sub

    Private Sub btnReiniciar_Click(sender As Object, e As EventArgs) Handles btnReiniciar.Click
        iniciarJuego()
    End Sub

    Sub iniciarJuego()
        'Se reinician los valores de la matriz de la tabla del juego
        matrizJuego = {{"", "", ""}, {"", "", ""}, {"", "", ""}}

        'Codigo para elegir quien jugara primero
        Dim turno = Math.Round(Rnd(2), 0)
        Debug.WriteLine("turno: " + turno.ToString)
        'Se obtendra un valor al azar entre 0 y 1. Si se obtiene un 0, sera el turno del agente, mientras que si se
        'obtiene un 1, sera el turno del jugador.
        Dim mejorMovimiento As Object
        If turno = 0 Then
            'mejorMovimiento = obtenerMejorMovimiento({{"", "", ""}, {"", "", ""}, {"", "", ""}}, simboloAgente)
            'matrizJuego = mejorMovimiento(3)(0)
            'Debug.WriteLine("Tus posibilidades de victoria son de " + mejorMovimiento(3)(1).ToString)
            'Debug.WriteLine("Turno del agente")
            mejorMovimiento = obtenerMejorMovimiento(matrizJuego)
            matrizJuego = mejorMovimiento(2)(0)
            For index = 0 To 2
                Debug.WriteLine(matrizJuego(index, 0) + "   " + matrizJuego(index, 1) + "   " + matrizJuego(index, 2))
            Next
        End If
        Debug.WriteLine("hola")
        actualizarTabla()
    End Sub

    'Function obtenerMejorMovimiento(matriz As Array, turno As String) As Array
    '    If (juegoGanado(matriz, turno)) Or (juegoEmpatado(matriz)) Then
    '        'Debug.WriteLine("gano agente")
    '        If turno = simboloAgente Then
    '            Return {matriz, True, 1, Nothing}
    '        Else
    '            Return {matriz, False, 0, Nothing}
    '        End If
    '    End If

    '    Dim mejorCamino As Array = {matriz, 0}
    '    Dim pesoNodo As Integer = 0
    '    Dim caminoGanador As Boolean = False

    '    For fila = 0 To 2
    '        For casilla = 0 To 2
    '            If matriz(fila, casilla) = "" Then
    '                'asignacion de los valores de matriz a matrizAux para que no tengan la misma referencia
    '                Dim matrizAux = {{"", "", ""}, {"", "", ""}, {"", "", ""}}
    '                For x = 0 To 2
    '                    For y = 0 To 2
    '                        matrizAux(x, y) = matriz(x, y)
    '                    Next
    '                Next
    '                matrizAux(fila, casilla) = turno
    '                Dim mejorMovimiento As Array
    '                If turno = simboloAgente Then
    '                    mejorMovimiento = obtenerMejorMovimiento(matrizAux, simboloJugador)
    '                Else
    '                    mejorMovimiento = obtenerMejorMovimiento(matrizAux, simboloAgente)
    '                End If

    '                If mejorMovimiento(0) Is matrizAux Then

    '                End If
    '                'Debug.WriteLine("mejorMovimiento: " + mejorMovimiento.ToString)
    '                If mejorMovimiento(1) Then
    '                    If mejorMovimiento(2) > mejorCamino(1) Then
    '                        mejorCamino = {matrizAux, mejorMovimiento(2)}
    '                        'Debug.WriteLine(mejorMovimiento(2).ToString + " le gano a " + mejorCamino(1).ToString)
    '                    Else
    '                        'Debug.WriteLine(mejorMovimiento(2).ToString + " no le gano a " + mejorCamino(1).ToString)

    '                    End If
    '                    pesoNodo += mejorMovimiento(2)
    '                    caminoGanador = True
    '                End If
    '            End If
    '        Next
    '    Next
    '    'Debug.WriteLine("Mejor camino: " + mejorCamino(0).ToString + ", peso: " + mejorCamino(1).ToString)
    '    Return {matriz, caminoGanador, pesoNodo, mejorCamino}

    'End Function

    Function obtenerMejorMovimiento(matriz As Array) As Array
        'For index = 0 To 2
        '    Debug.WriteLine(matriz(index, 0) + "   " + matriz(index, 1) + "   " + matriz(index, 2))
        'Next

        Dim mejorCamino = {matriz, 0, 0}
        Dim pesoNodo = 0
        Dim flag = False

        'Resultado base
        If juegoGanado(matriz, simboloJugador) Then
            Return {matriz, -1, mejorCamino}
        End If

        If (juegoGanado(matriz, simboloAgente) And Not juegoGanado(matriz, simboloJugador)) Then
            Return {matriz, 1, mejorCamino}
        End If

        If juegoEmpatado(matriz) Then
            Return {matriz, 1, mejorCamino}
        End If

        'Debug.WriteLine("----------------------------------------------------------------------")

        For filas = 0 To 2
            For casillas = 0 To 2
                If matriz(filas, casillas) = "" Then
                    Dim matrizAux = {{"", "", ""}, {"", "", ""}, {"", "", ""}}
                    'Se asignan los valores de la matriz original a la matriz auxiliar
                    For x = 0 To 2
                        For y = 0 To 2
                            matrizAux(x, y) = matriz(x, y)
                        Next
                    Next
                    matrizAux(filas, casillas) = simboloAgente
                    'Se toma la siguiente casilla vacia como el siguiente movimiento del oponente
                    Dim pesoCasilla = 0
                    Dim distanciaCasilla = 0
                    For filas2 = 0 To 2
                        For casillas2 = 0 To 2
                            If matrizAux(filas2, casillas2) = "" Then
                                Dim matrizAux2 As Array = {{"", "", ""}, {"", "", ""}, {"", "", ""}}
                                For x = 0 To 2
                                    For y = 0 To 2
                                        matrizAux2(x, y) = matrizAux(x, y)
                                    Next
                                Next
                                matrizAux2(filas2, casillas2) = simboloJugador
                                'La funcion se llama a si misma
                                Dim mejorMovimiento = obtenerMejorMovimiento(matrizAux2)
                                pesoCasilla += mejorMovimiento(1)
                                distanciaCasilla = mejorMovimiento(1)
                            End If
                        Next
                    Next
                    If (pesoCasilla > mejorCamino(1)) Or (distanciaCasilla + 1 < mejorCamino(2)) Or (Not flag) Then
                        mejorCamino(0) = matrizAux
                        mejorCamino(1) = pesoCasilla
                        mejorCamino(2) = distanciaCasilla + 1
                        'Debug.WriteLine(pesoCasilla.ToString + " mayor a " + mejorCamino(1).ToString)
                    Else
                        'Debug.WriteLine(pesoCasilla.ToString + " no es mayor a " + mejorCamino(1).ToString)
                    End If
                    pesoNodo += pesoCasilla
                    'Reporte de la casilla y sus posibilidades de ganar
                    'Debug.WriteLine("casilla(" + filas.ToString + "," + casillas.ToString + "), posibilidades de ganar: " + pesoCasilla.ToString)
                End If
            Next
        Next
        'Debug.WriteLine("el peso de la casilla mas pesada es de  " + mejorCamino(1).ToString)
        'Debug.WriteLine("----------------------------------------------------------------------")
        Return {matriz, pesoNodo, mejorCamino}
    End Function

    'Function obtenerMejorMovimiento(matriz As Array) As Array
    '    'For index = 0 To 2
    '    '    Debug.WriteLine(matriz(index, 0) + "   " + matriz(index, 1) + "   " + matriz(index, 2))
    '    'Next

    '    Dim mejorCamino = {matriz, 0, 0}
    '    Dim pesoNodo = 0

    '    'Resultado base
    '    If juegoGanado(matriz, simboloJugador) Then
    '        Return {matriz, -1, mejorCamino}
    '    End If

    '    If (juegoGanado(matriz, simboloAgente) And Not juegoGanado(matriz, simboloJugador)) Then
    '        Return {matriz, 1, mejorCamino}
    '    End If

    '    If juegoEmpatado(matriz) Then
    '        Return {matriz, 1, mejorCamino}
    '    End If

    '    'Debug.WriteLine("----------------------------------------------------------------------")

    '    For filas = 0 To 2
    '        For casillas = 0 To 2
    '            If matriz(filas, casillas) = "" Then
    '                Dim matrizAux = {{"", "", ""}, {"", "", ""}, {"", "", ""}}
    '                'Se asignan los valores de la matriz original a la matriz auxiliar
    '                For x = 0 To 2
    '                    For y = 0 To 2
    '                        matrizAux(x, y) = matriz(x, y)
    '                    Next
    '                Next
    '                matrizAux(filas, casillas) = simboloAgente
    '                'Se toma la siguiente casilla vacia como el siguiente movimiento del oponente
    '                Dim pesoCasilla = 0
    '                Dim distanciaCasilla = 0
    '                For filas2 = 0 To 2
    '                    For casillas2 = 0 To 2
    '                        If matrizAux(filas2, casillas2) = "" Then
    '                            Dim matrizAux2 As Array = {{"", "", ""}, {"", "", ""}, {"", "", ""}}
    '                            For x = 0 To 2
    '                                For y = 0 To 2
    '                                    matrizAux2(x, y) = matrizAux(x, y)
    '                                Next
    '                            Next
    '                            matrizAux2(filas2, casillas2) = simboloJugador
    '                            'La funcion se llama a si misma
    '                            Dim mejorMovimiento = obtenerMejorMovimiento(matrizAux2)
    '                            pesoCasilla += mejorMovimiento(1)
    '                            distanciaCasilla = mejorMovimiento(1)
    '                        End If
    '                    Next
    '                Next
    '                If (pesoCasilla > mejorCamino(1)) Or (distanciaCasilla + 1 < mejorCamino(2)) Or (filas = 0 And casillas = 0) Then
    '                    mejorCamino(0) = matrizAux
    '                    mejorCamino(1) = pesoCasilla
    '                    mejorCamino(2) = distanciaCasilla + 1
    '                    'Debug.WriteLine(pesoCasilla.ToString + " mayor a " + mejorCamino(1).ToString)
    '                Else
    '                    'Debug.WriteLine(pesoCasilla.ToString + " no es mayor a " + mejorCamino(1).ToString)
    '                End If
    '                pesoNodo += pesoCasilla
    '                'Reporte de la casilla y sus posibilidades de ganar
    '                'Debug.WriteLine("casilla(" + filas.ToString + "," + casillas.ToString + "), posibilidades de ganar: " + pesoCasilla.ToString)
    '            End If
    '        Next
    '    Next
    '    'Debug.WriteLine("el peso de la casilla mas pesada es de  " + mejorCamino(1).ToString)
    '    'Debug.WriteLine("----------------------------------------------------------------------")
    '    Return {matriz, pesoNodo, mejorCamino}
    'End Function
    Function obtenerJugadasPosibles(matriz As Array) As Integer

        Return 0
    End Function

    Function juegoGanado(matriz As Array, turno As String) As Boolean
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
            Return True
        Else
            Return False
        End If
    End Function

    'Function juegoSalvado(matriz As Array, turno As String) As Boolean
    '    Dim fila1 As Integer = 0
    '    Dim fila2 As Integer = 0
    '    Dim fila3 As Integer = 0

    '    Dim columna1 As Integer = 0
    '    Dim columna2 As Integer = 0
    '    Dim columna3 As Integer = 0

    '    Dim diagonal1 As Integer = 0
    '    Dim diagonal2 As Integer = 0


    '    For fila = 0 To 2
    '        For casilla = 0 To 2

    '            If fila = 0 And matriz(fila, casilla) = turno Then
    '                fila1 += 1
    '            End If

    '            If fila = 1 And matriz(fila, casilla) = turno Then
    '                fila2 += 1
    '            End If

    '            If fila = 2 And matriz(fila, casilla) = turno Then
    '                fila3 += 1
    '            End If

    '            'For index = 0 To 2
    '            '    Debug.WriteLine(matriz(index, 0) + "   " + matriz(index, 1) + "   " + matriz(index, 2))
    '            'Next

    '            'Debug.WriteLine("casilla: " + casilla.ToString + " fila: " + fila.ToString + " turno " + turno.ToString)

    '            If casilla = 0 And matriz(fila, casilla) = turno Then
    '                columna1 += 1
    '            End If

    '            If casilla = 1 And matriz(fila, casilla) = turno Then
    '                columna2 += 1
    '            End If

    '            If casilla = 2 And matriz(fila, casilla) = turno Then
    '                columna3 += 1
    '            End If

    '            If ((fila = 0 And casilla = 0) Or (fila = 1 And casilla = 1) Or (fila = 2 And casilla = 2)) And matriz(fila, casilla) = turno Then
    '                diagonal1 += 1
    '            End If

    '            If ((fila = 2 And casilla = 0) Or (fila = 1 And casilla = 1) Or (fila = 0 And casilla = 2)) And matriz(fila, casilla) = turno Then
    '                diagonal2 += 1
    '            End If
    '        Next
    '    Next

    '    If fila1 = 3 Or fila2 = 3 Or fila3 = 3 Or columna1 = 3 Or columna2 = 3 Or columna3 = 3 Or diagonal1 = 3 Or diagonal2 = 3 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

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

    Function posicionGanadora(matriz As Array) As Boolean

        Return 0
    End Function

    Sub actualizarTabla()
        Dim contador As Integer = 0
        Dim casillas = {Nothing,
            Nothing,
            Nothing,
            Nothing,
            Nothing,
            Nothing,
            Nothing,
            Nothing,
            Nothing}

        For fila = 0 To 2
            For casilla = 0 To 2
                If matrizJuego(fila, casilla) = "x" Then
                    casillas(contador) = Image.FromFile("..\..\..\images\x-totito.png")
                ElseIf matrizJuego(fila, casilla) = "o" Then
                    casillas(contador) = Image.FromFile("..\..\..\images\o-totito.png")
                End If
                contador += 1
            Next
        Next

        imgCasilla1.Image = casillas(0)
        imgCasilla2.Image = casillas(1)
        imgCasilla3.Image = casillas(2)
        imgCasilla4.Image = casillas(3)
        'Console.WriteLine("sdf", My.Application.Info.DirectoryPath)
        imgCasilla5.Image = casillas(4)
        imgCasilla6.Image = casillas(5)
        imgCasilla7.Image = casillas(6)
        imgCasilla8.Image = casillas(7)
        imgCasilla9.Image = casillas(8)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyBase.CenterToParent()
        'iniciarJuego()
        'Dim matrizPrueba = {{"x", "o", "x"}, {"o", "x", "o"}, {"o", "x", "o"}}
        'Debug.WriteLine("juego ganado: " + juegoEmpatado(matrizPrueba).ToString)
        'Debug.WriteLine("Hola mundo")
        Dim arbol As New Arbol(matrizJuego, simboloAgente)
        Debug.WriteLine("el mejor movimiento es :")
        Dim mejorMovimiento = arbol.obtenerMejorMovimiento()
        For index = 0 To 2
            Debug.WriteLine(mejorMovimiento.matriz(index, 0) + "-" + mejorMovimiento.matriz(index, 1) + "-" + mejorMovimiento.matriz(index, 2))
        Next
    End Sub

    Private Sub lblEsTuTurno_Click(sender As Object, e As EventArgs) Handles lblEsTuTurno.Click

    End Sub

    Private Sub imgCasilla2_Click(sender As Object, e As EventArgs) Handles imgCasilla2.Click
        matrizJuego(0, 1) = "o"
        Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego)
        matrizJuego = mejorMovimiento(2)(0)
        'Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego, simboloAgente)
        'matrizJuego = mejorMovimiento(3)(0)
        'Debug.WriteLine("Tus posibilidades de victoria son de " + mejorMovimiento(3)(1).ToString)
        actualizarTabla()
    End Sub

    Private Sub imgCasilla3_Click(sender As Object, e As EventArgs) Handles imgCasilla3.Click
        matrizJuego(0, 2) = "o"
        Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego)
        matrizJuego = mejorMovimiento(2)(0)
        'Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego, simboloAgente)
        'matrizJuego = mejorMovimiento(3)(0)
        actualizarTabla()
    End Sub

    Private Sub imgCasilla4_Click(sender As Object, e As EventArgs) Handles imgCasilla4.Click
        matrizJuego(1, 0) = "o"
        Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego)
        matrizJuego = mejorMovimiento(2)(0)
        'Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego, simboloAgente)
        'matrizJuego = mejorMovimiento(3)(0)
        actualizarTabla()
    End Sub

    Private Sub imgCasilla5_Click(sender As Object, e As EventArgs) Handles imgCasilla5.Click
        matrizJuego(1, 1) = "o"
        Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego)
        matrizJuego = mejorMovimiento(2)(0)
        'Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego, simboloAgente)
        'matrizJuego = mejorMovimiento(3)(0)
        'Debug.WriteLine("Tus posibilidades de victoria son de " + mejorMovimiento(3)(1).ToString)
        actualizarTabla()
    End Sub

    Private Sub imgCasilla6_Click(sender As Object, e As EventArgs) Handles imgCasilla6.Click
        matrizJuego(1, 2) = "o"
        Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego)
        matrizJuego = mejorMovimiento(2)(0)
        'Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego, simboloAgente)
        'matrizJuego = mejorMovimiento(3)(0)
        actualizarTabla()
    End Sub

    Private Sub imgCasilla7_Click(sender As Object, e As EventArgs) Handles imgCasilla7.Click
        matrizJuego(2, 0) = "o"
        Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego)
        matrizJuego = mejorMovimiento(2)(0)
        'Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego, simboloAgente)
        'matrizJuego = mejorMovimiento(3)(0)
        actualizarTabla()
    End Sub

    Private Sub imgCasilla8_Click(sender As Object, e As EventArgs) Handles imgCasilla8.Click
        matrizJuego(2, 1) = "o"
        Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego)
        matrizJuego = mejorMovimiento(2)(0)
        'Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego, simboloAgente)
        'matrizJuego = mejorMovimiento(3)(0)
        actualizarTabla()
    End Sub

    Private Sub imgCasilla9_Click(sender As Object, e As EventArgs) Handles imgCasilla9.Click
        matrizJuego(2, 2) = "o"
        Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego)
        matrizJuego = mejorMovimiento(2)(0)
        'Dim mejorMovimiento = obtenerMejorMovimiento(matrizJuego, simboloAgente)
        'matrizJuego = mejorMovimiento(3)(0)
        'Debug.WriteLine("Tus posibilidades de victoria son de " + mejorMovimiento(3)(1).ToString)
        actualizarTabla()
    End Sub
End Class
