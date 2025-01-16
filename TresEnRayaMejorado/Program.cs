using System;
class TresEnRaya1
{
    static int[,] tablero;
    static char[] simbolos = { '.', 'O', 'X' };
    static int jugadorActual = 1;
    static bool terminado = false;
    static void Main()
    {
        tablero = new int[3, 3];

        do
        {
            DibujarPantalla();
            ComprobarEntrada();
            AnimarElementos();
            ComprobarEstado();
            PausaFotograma();
        }
        while (!terminado);

        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(0, 23);
        Console.WriteLine("Presiona cualquier tecla para salir...");
        Console.ReadKey();
    }



    private static void DibujarPantalla()
    {
        Console.Clear();
        DibujarRecuadro();
        for (int fila = 0; fila < 3; fila++)
            for (int columna = 0; columna < 3; columna++)
                DibujarSimbolo(fila,columna);
    }
    private static void DibujarRecuadro()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        for(int fila = 0;fila < 2; fila++)
        {
            Console.SetCursorPosition(11, fila*6 +7);
            Console.WriteLine(new string('-',18));
        }

        for (int columna = 0; columna < 2; columna++)
        {
           for (int fila = 0; fila < 18; fila++)
            {
                Console.SetCursorPosition(columna * 6 + 16, fila + 2);
                Console.WriteLine("|");
            }
        }
    }

    private static void DibujarSimbolo(int fila, int columna)
    {
        int filaPantalla = fila * 6 + 2;
        int columnaPantalla = columna * 6 + 11;

        int jugador = tablero[fila, columna];

        ConsoleColor colorOriginal = Console.ForegroundColor;
        if (jugador == 1) Console.ForegroundColor = ConsoleColor.Red;
        else if (jugador == 2) Console.ForegroundColor = ConsoleColor.Cyan;

        if (jugador == 1) // Círculo para el jugador 1
        {
            Console.SetCursorPosition(columnaPantalla, filaPantalla); Console.Write(" ooo ");
            Console.SetCursorPosition(columnaPantalla, filaPantalla + 1); Console.Write("o   o");
            Console.SetCursorPosition(columnaPantalla, filaPantalla + 2); Console.Write("o   o");
            Console.SetCursorPosition(columnaPantalla, filaPantalla + 3); Console.Write("o   o");
            Console.SetCursorPosition(columnaPantalla, filaPantalla + 4); Console.Write(" ooo ");
        }
        else if (jugador == 2) // Equis para el jugador 2
        {
            Console.SetCursorPosition(columnaPantalla, filaPantalla); Console.Write("x   x");
            Console.SetCursorPosition(columnaPantalla, filaPantalla + 1); Console.Write(" x x ");
            Console.SetCursorPosition(columnaPantalla, filaPantalla + 2); Console.Write("  x  ");
            Console.SetCursorPosition(columnaPantalla, filaPantalla + 3); Console.Write(" x x ");
            Console.SetCursorPosition(columnaPantalla, filaPantalla + 4); Console.Write("x   x");
        }

        // Restaura el color original
        Console.ForegroundColor = colorOriginal;
    }

    private static void ComprobarEntrada()
    {
        bool casillaValida = false;
        int fila = 0;
        int columna = 0;

        do
        {
            DibujarPantalla();

            // Dibuja indicadores en la posición actual
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(10, fila * 6 + 4);
            Console.Write(">");
            Console.SetCursorPosition(columna * 6 + 13, 0);
            Console.Write("v");

            // Captura la tecla presionada
            ConsoleKeyInfo tecla = Console.ReadKey(true);

            // Procesa movimientos del cursor
            if (tecla.Key == ConsoleKey.LeftArrow)
            {
                columna = (columna == 0) ? 2 : columna - 1;
            }
            else if (tecla.Key == ConsoleKey.RightArrow)
            {
                columna = (columna == 2) ? 0 : columna + 1;
            }
            else if (tecla.Key == ConsoleKey.UpArrow)
            {
                fila = (fila == 0) ? 2 : fila - 1;
            }
            else if (tecla.Key == ConsoleKey.DownArrow)
            {
                fila = (fila == 2) ? 0 : fila + 1;
            }
            else if (tecla.Key == ConsoleKey.Spacebar && tablero[fila, columna] == 0)
            {
                casillaValida = true;
            }
        } while (!casillaValida);

        tablero[fila, columna] = jugadorActual;
    }

    private static void AnimarElementos()
    {
    }
    private static void ComprobarEstado()
    {
     
        //Comprobar si el jugador ha ganado/ empate o cambiar turno.
        bool isPrueba = false;
        for (int fila = 0; fila < 3; fila++)
        {
            if (tablero[fila, 0] == jugadorActual && tablero[fila, 1] == jugadorActual && tablero[fila, 2] == jugadorActual)
            {
                DibujarPantalla();
                isPrueba = true;
            }
        }

        for (int columna = 0; columna < 3; columna++)
        {
            if ((tablero[0, columna] == tablero[1, columna]) && (tablero[0, columna] == tablero[2, columna]) && (tablero[0, columna] == jugadorActual))
            {
                DibujarPantalla();
                isPrueba = true;
            }
        }

        if ((tablero[0, 0] == jugadorActual && tablero[1, 1] == jugadorActual && tablero[2, 2] == jugadorActual) || (tablero[0, 2] == jugadorActual && tablero[1, 1] == jugadorActual && tablero[2, 0] == jugadorActual))
        {
            DibujarPantalla();
            isPrueba = true;
        }

        if (isPrueba)
        {
            Console.SetCursorPosition(0, 21);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Gana el jugador " + jugadorActual);
            terminado = true;
        }
        else
        {

            int cantidadVacias = 0;
            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    if (tablero[fila, columna] == 0) cantidadVacias++;
                }
            }


            if (cantidadVacias == 0)
            {
                Console.WriteLine("Empate");
                terminado = true;
            }

            if (jugadorActual == 1) jugadorActual = 2;
            else jugadorActual = 1;
        }
    }

    private static void PausaFotograma()
    {
    }
}
