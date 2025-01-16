using System;
class TresEnRaya1
{
    static int[,] tablero;
    static char[] simbolos = { '.', 'O', 'X'};
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
    }

 

    private static void DibujarPantalla()
    {
        Console.WriteLine();
        Console.WriteLine("_________________");
        for (int fila = 0; fila < 3; fila++)
        {
            Console.Write("|");
            for (int columna = 0; columna < 3; columna++)
            {
                Console.Write(" " + simbolos[tablero[fila,columna]] + " |");
            }
            Console.WriteLine();
        }
        Console.WriteLine("_________________");
    }


    private static void ComprobarEntrada()
    {
        bool casillaValida = false;
        int fila;
        int columna;
        do
        {
            Console.Write("Dime la fila (1,3): ");
            fila = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("Dime la columna (1,3): ");
            columna = Convert.ToInt32(Console.ReadLine()) - 1;

            if (
                (fila >= 0) && (fila < 3) 
                && (columna >= 0) && (columna < 3)
                && (tablero[fila, columna] == 0))
            {
                casillaValida = true;
            }
            else
            {
                Console.WriteLine("Esa casilla no es validad");
            }
        } while(!casillaValida);
        
        tablero[fila, columna] = jugadorActual;

    }
    private static void AnimarElementos()
    {
    }
    private static void ComprobarEstado()
    {
        //Comprobar si el jugador ha ganado/ empate o cambiar turno.
        for (int fila = 0; fila < 3; fila++)
        {
            if (tablero[fila, 0] == jugadorActual && tablero[fila, 1] == jugadorActual && tablero[fila, 2] == jugadorActual)
            {
                DibujarPantalla();
                Console.WriteLine("El jugador " + jugadorActual + " ha ganado");
                terminado = true;
            }
        }

        for (int columna=0; columna < 3; columna++) {
            if ((tablero[0, columna] == tablero[1,columna]) && (tablero[0,columna]== tablero[2, columna]) && (tablero[0,columna] == jugadorActual))
            {
                DibujarPantalla();
                Console.WriteLine("El jugador " + jugadorActual + " ha ganado");
                terminado = true;
            }
        }

        if((tablero[0, 0] == jugadorActual && tablero[1, 1] == jugadorActual && tablero[2, 2] == jugadorActual) || (tablero[0, 2] == jugadorActual && tablero[1, 1] == jugadorActual && tablero[2, 0] == jugadorActual))
        {
            DibujarPantalla();
            Console.WriteLine("El jugador " + jugadorActual + " ha ganado");
            terminado = true;
        }

        int cantidadVacias = 0;
        for (int fila = 0; fila < 3; fila++)
        {
            for (int columna = 0; columna < 3; columna++)
            {
                if (tablero[fila, columna] == 0) cantidadVacias++;
            }
        }

        if(cantidadVacias == 0)
        {
            Console.WriteLine("Empate");
            terminado = true;
        }

        if (jugadorActual == 1) jugadorActual = 2;
        else  jugadorActual = 1; 
    }

    private static void PausaFotograma()
    {
    }
}
