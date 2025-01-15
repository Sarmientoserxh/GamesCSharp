using System;
class Program
{
    public static void Main()
    {
        // El ordenador escoge una palabra al azar entre un conjunto predeterminado
        // preparamos una plabra a mostrar serie de guiones

        string[] palabras = { "Buscando a nemo", "commando creature", "SpiderMan", "Star Wars", "Hulk", "Batman" };
        int numeroAzar = new Random().Next(0, palabras.Length);
        string palabraAdivinar = palabras[numeroAzar].ToLower();

        string palabraMostrar = "";
        
        for (int i = 0; i < palabraAdivinar.Length; i++)
        {
            if (palabraAdivinar[i] == ' ')
            {
                palabraMostrar += " ";
            }
            else
                palabraMostrar += "-";
        }

        // resto de variables
        int intentos = 8;
        char letraActual = ' ';
        bool isAcierto = false;
        Console.WriteLine("=====Adivina la palabra========");
        do 
        {
            //mostrar la frase oculta y los intentos.
            Console.WriteLine("Palabra a adivinar: " + palabraMostrar);
            Console.WriteLine("Intentos restantes: " + intentos);
            MostrarHorca(intentos);

            //pedir una letra
            Console.Write("Introduce una letra: ");
            letraActual = Convert.ToChar(Console.ReadLine());
            Console.WriteLine("================================");

            if (!palabraAdivinar.Contains(letraActual)) 
            {
                intentos--;
            }
            string siguienteMostrar = "";
            for(int i = 0; i < palabraAdivinar.Length; i++)
            {
                if (palabraAdivinar[i] == letraActual)
                {
                    siguienteMostrar += letraActual;
                }
                else
                {
                    siguienteMostrar += palabraMostrar[i];
                }
            }

            palabraMostrar = siguienteMostrar;

            if (palabraMostrar.Contains(letraActual))
                Console.WriteLine("Ya haz escogido esa letra intenta otra");
               

            if (!palabraMostrar.Contains("-"))
            {
                Console.WriteLine("Has acertado la palabra: " + palabraAdivinar);
                isAcierto = true;
            }else if (intentos == 0)
            {
                Console.WriteLine("Has perdido, la palabra era: " + palabraAdivinar);
            }

        }
        while ((intentos > 0) && (!isAcierto));
    }

    static void MostrarHorca(int intentos)
    {
        switch (intentos)
        {
            case 7:
                Console.WriteLine("   ||\n   ||\n   ||\n   ||\n   ||\n   ||\n   ||\n   ||\n==============");
                break;
            case 6:
                Console.WriteLine("==============\n   ||\n   ||\n   ||\n   ||\n   ||\n   ||\n   ||\n==============");
                break;
            case 5:
                Console.WriteLine("==============\n   ||      |\n   ||      |\n   ||\n   ||\n   ||\n   ||\n   ||\n==============");
                break;
            case 4:
                Console.WriteLine("==============\n   ||      |\n   ||      |\n   ||      O\n   ||\n   ||\n   ||\n   ||\n   ||\n==============");
                break;
            case 3:
                Console.WriteLine("==============\n   ||      |\n   ||      |\n   ||      O\n   ||      |\n   ||      |\n   ||\n   ||\n   ||\n==============");
                break;
            case 2:
                Console.WriteLine("==============\n   ||      |\n   ||      |\n   ||      O\n   ||      |\n   ||      |\n   ||     / \\\n   ||\n   ||\n==============");
                break;
            case 1:
                Console.WriteLine("==============\n   ||      |\n   ||      |\n   ||      O\n   ||     /|\\\n   ||      |\n   ||     / \\\n   ||\n   ||\n==============");
                break;
        }
    }
}



