using System.Drawing;

Ventana ventana;
Nave nave;
bool jugar = true;



void Iniciar()
{
    ventana = new Ventana(
    190,    // Ancho
    45,     // Alto
    ConsoleColor.Black,
    new Point(1, 1),    // Limite superior
    new Point(118, 28)  // Limite inferior 
);

    nave = new Nave(new Point(55,25), ConsoleColor.White, ventana);

    ventana.Init();
    nave.DibujarNave();
}

void Game()
{
    while (jugar)
    {
        nave.Mover(2);
    }
}

Iniciar();
Game();