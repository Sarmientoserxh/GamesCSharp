namespace Mazmorras;

struct SpriteTexto
{
    public int x;
    public int y;
    public int velocidadX;
    public int velocidadY;
    public string caracter;
    public ConsoleColor color;
    public bool visible;
}

class Mazmorras
{
    static bool terminado;

    static SpriteTexto personaje;
    static SpriteTexto[] enemigos;
    static SpriteTexto[] tesoros;
    static SpriteTexto[] vidasExtras;
    static int puntos;
    static int vidas;
    static int itemsRestantes;

    static int numEnemigos, numTesoros, numVidasExtras;
    static Random random = new Random();

    static int anchoPantalla = 80;
    static int altoPantalla = 24;

    static string[] mapa =
    {
        "################################################################################",
        "#             #                           #               #                   #   #",
        "#     ####     ####      ####             #      ####     #      ####        #   #",
        "#     #              #                    #               #                   #   #",
        "#     #   ########   #########          #######    #########                #   #",
        "#     #                                                                      #   #",
        "#     #########         #########                                           #   #",
        "#                       #                                                    #   #",
        "#                       #      ##### #####                                   #   #",
        "#            #######    #      #         #                                   #   #",
        "#            #          #      #             #########   #########   ########   #",
        "#            #          #      #         #   #                                 #",
        "#            #          #      ###########   #                                 #",
        "#            #                      #         #      ####                      #",
        "#     ###########                   #         ###########                      #",
        "#     #                             #                                          #",
        "#     #                             ###########################################",
        "#     ##########################################################################",
    };

    static int xMapa, yMapa;

    static void Main(string[] args)
    {
        InitGame();

        while (!terminado)
        {
            DibujarPantalla();
            ComprobarEntrada();
            AnimarElementos();
            ComprobarEstado();
            PausarFotogramas();
        }
    }

    private static void InitGame()
    {
        terminado = false;
        puntos = 0;
        vidas = 3;
        xMapa = 0;
        yMapa = 0;

        personaje.x = 5;
        personaje.y = 2;
        personaje.color = ConsoleColor.Green;
        personaje.caracter = "A";

        numEnemigos = 4;
        numTesoros = 8;
        numVidasExtras = 2;
        itemsRestantes = numTesoros;

        // Inicializar enemigos
        enemigos = new SpriteTexto[numEnemigos];
        for (int i = 0; i < numEnemigos; i++)
        {
            do
            {
                enemigos[i].x = random.Next(1, anchoPantalla - 2);
                enemigos[i].y = random.Next(1, altoPantalla - 2);
            } while (!EsPosibleMoverA(enemigos[i].x, enemigos[i].y));

            enemigos[i].color = ConsoleColor.Red;
            enemigos[i].caracter = "D";
            enemigos[i].velocidadX = random.Next(1, 3) * (random.Next(0, 2) == 0 ? 1 : -1);
        }

        // Inicializar tesoros
        tesoros = new SpriteTexto[numTesoros];
        for (int i = 0; i < numTesoros; i++)
        {
            do
            {
                tesoros[i].x = random.Next(1, anchoPantalla - 2);
                tesoros[i].y = random.Next(1, altoPantalla - 2);
            } while (!EsPosibleMoverA(tesoros[i].x, tesoros[i].y));

            tesoros[i].color = ConsoleColor.Yellow;
            tesoros[i].caracter = "$";
            tesoros[i].visible = true;
        }

        // Inicializar vidas extra
        vidasExtras = new SpriteTexto[numVidasExtras];
        for (int i = 0; i < numVidasExtras; i++)
        {
            do
            {
                vidasExtras[i].x = random.Next(1, anchoPantalla - 2);
                vidasExtras[i].y = random.Next(1, altoPantalla - 2);
            } while (!EsPosibleMoverA(vidasExtras[i].x, vidasExtras[i].y));

            vidasExtras[i].color = ConsoleColor.Magenta;
            vidasExtras[i].caracter = "+";
            vidasExtras[i].visible = true;
        }
    }

    private static void DibujarPantalla()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Blue;
        for (int i = 0; i < mapa.Length; i++)
        {
            Console.SetCursorPosition(xMapa, yMapa + i);
            Console.Write(mapa[i]);
        }

        // Dibujar personaje
        Console.ForegroundColor = personaje.color;
        Console.SetCursorPosition(personaje.x, personaje.y);
        Console.Write(personaje.caracter);

        // Dibujar enemigos
        foreach (var enemigo in enemigos)
        {
            Console.ForegroundColor = enemigo.color;
            Console.SetCursorPosition(enemigo.x, enemigo.y);
            Console.Write(enemigo.caracter);
        }

        // Dibujar tesoros
        foreach (var tesoro in tesoros)
        {
            if (tesoro.visible)
            {
                Console.ForegroundColor = tesoro.color;
                Console.SetCursorPosition(tesoro.x, tesoro.y);
                Console.Write(tesoro.caracter);
            }
        }

        // Dibujar vidas extra
        foreach (var vida in vidasExtras)
        {
            if (vida.visible)
            {
                Console.ForegroundColor = vida.color;
                Console.SetCursorPosition(vida.x, vida.y);
                Console.Write(vida.caracter);
            }
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(80, 1);
        Console.Write($"Puntos: {puntos}  Vidas: {vidas}");
        Console.ResetColor();
    }

    private static void ComprobarEntrada()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.LeftArrow && EsPosibleMoverA(personaje.x - 1, personaje.y)) personaje.x--;
            if (tecla.Key == ConsoleKey.RightArrow && EsPosibleMoverA(personaje.x + 1, personaje.y)) personaje.x++;
            if (tecla.Key == ConsoleKey.UpArrow && EsPosibleMoverA(personaje.x, personaje.y - 1)) personaje.y--;
            if (tecla.Key == ConsoleKey.DownArrow && EsPosibleMoverA(personaje.x, personaje.y + 1)) personaje.y++;
        }
    }

    private static void AnimarElementos()
    {
        for (int i = 0; i < numEnemigos; i++)
        {
            enemigos[i].x += enemigos[i].velocidadX;

            // Cambiar dirección si chocan con un muro
            if (!EsPosibleMoverA(enemigos[i].x, enemigos[i].y))
            {
                enemigos[i].velocidadX = -enemigos[i].velocidadX;
                enemigos[i].x += enemigos[i].velocidadX;
            }
        }
    }

    private static void ComprobarEstado()
    {
        // Colisiones con enemigos
        foreach (var enemigo in enemigos)
        {
            if (personaje.x == enemigo.x && personaje.y == enemigo.y)
            {
                vidas--;
                Console.SetCursorPosition(1, altoPantalla - 1);
                Console.WriteLine("¡Te ha golpeado un dragón! Pierdes una vida.");
                if (vidas <= 0)
                {
                    terminado = true;
                    Console.SetCursorPosition(1, altoPantalla - 1);
                    Console.WriteLine("Has muerto. Fin del juego.");
                }
                return;
            }
        }

        // Colisiones con tesoros
        for (int i = 0; i < numTesoros; i++)
        {
            if (personaje.x == tesoros[i].x && personaje.y == tesoros[i].y && tesoros[i].visible)
            {
                puntos += 10;
                tesoros[i].visible = false;
                itemsRestantes--;
                if (itemsRestantes == 0)
                {
                    terminado = true;
                    Console.SetCursorPosition(1, altoPantalla - 1);
                    Console.WriteLine("¡Has ganado! Todos los tesoros recolectados.");
                }
                return;
            }
        }

        // Colisiones con vidas extra
        for (int i = 0; i < numVidasExtras; i++)
        {
            if (personaje.x == vidasExtras[i].x && personaje.y == vidasExtras[i].y && vidasExtras[i].visible)
            {
                vidas++;
                vidasExtras[i].visible = false;
                Console.SetCursorPosition(1, altoPantalla - 1);
                Console.WriteLine("¡Has encontrado una vida extra!");
                return;
            }
        }
    }

    private static void PausarFotogramas()
    {
        Thread.Sleep(100);
    }

    private static bool EsPosibleMoverA(int x, int y)
    {
        x -= xMapa;
        y -= yMapa;

        try
        {
            return mapa[y][x] != '#';
        }
        catch (Exception)
        {
            return false;
        }
    }
}
