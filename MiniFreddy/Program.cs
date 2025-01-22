
class MiniFreddy
{
    static bool terminado;
    static Sprite personaje;
    static Sprite enemigo;
    static int x, y;
    static int xEnemigo, yEnemigo;
    static int velocidadEnemigo;
    static Sprite[] items;
    static int cantidadItems;
    static void Main(string[] args)
    {
        InicializarJuego();

        while (!terminado)
        {
            DibujarPantalla();
            ComprobarEntradaUsuario();
            AnimarElementos();
            ComprobarEstadoDelJuego();
            PausaHastaFinDeFotograma();
        }
    }
    private static void InicializarJuego()
    {
        Hardware.Inicializar(1280, 720, 24);
        personaje = new Sprite("datos\\personaje.png");
        terminado = false;
        x = 600;
        y = 300;
        xEnemigo = 100;
        yEnemigo = 50;
        velocidadEnemigo = 5;
    }

    private static void DibujarPantalla()
    {
        Hardware.BorrarPantallaOculta();

        personaje.MoverA(x, y);
        personaje.Dibujar();

        Hardware.VisualizarOculta();
    }

    private static void ComprobarEntradaUsuario()
    {
        if (Hardware.TeclaPulsada(Hardware.TECLA_IZQ))
            x -= 3;
        // ...

        if (Hardware.TeclaPulsada(Hardware.TECLA_ESC))
            terminado = true;
    }

    private static void AnimarElementos()
    {
        if((xEnemigo <= 100) || (xEnemigo >= 1100)) velocidadEnemigo = -velocidadEnemigo;
        xEnemigo += velocidadEnemigo;
    }

    private static void ComprobarEstadoDelJuego()
    {
        // Nada por ahora
    }

    private static void PausaHastaFinDeFotograma()
    {
        Hardware.Pausa(20);
    }
}

