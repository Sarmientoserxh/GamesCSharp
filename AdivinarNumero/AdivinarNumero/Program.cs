using System;
namespace AdivinarNumero;

class Program
{
    static void Main(string[] args)
    {
        //Generar numero Aleatorio.
        //Pedir el numero al usuario.
        // si el numero > aleatorio, "Te has pasado"
        // si el numero < aleatorio "te has quedado corto"
        // hasta que numero == aleatorio
        //tener unos tantos intento para que sea mas divertido.
        // se termina el juego por dos motivos si el numero es igual al aleatorio y si no
        //este era el numero aleatorio.
      
        int aleatorio = new Random().Next(1, 1001);
        int entrada;
        int intentos = 10;
        Console.WriteLine("=========Adivinar números=========");
        do
        {
            Console.WriteLine("Intentos restantes: " + intentos);
            Console.WriteLine("Dime un numero: ");
            entrada = Convert.ToInt32(Console.ReadLine());

            if (entrada > aleatorio)
            {
                Console.WriteLine("Te has pasado");
            }
            else
            {
                Console.WriteLine("Te has quedado corto");
            }

            intentos--;
            Console.WriteLine("================================");
        }

        while ((entrada != aleatorio) && (intentos > 0));


        if (entrada == aleatorio)
        {
            Console.WriteLine("Felicidades lo lograste");
        }
        else
        {
            Console.WriteLine("Lo siento pero el numero era: " + aleatorio);
        }
    }
}