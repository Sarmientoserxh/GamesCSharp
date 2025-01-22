
using System.Drawing;

public class Nave
    {
        public float Vida { get; set; }
        public Point Position { get; set; }
        public ConsoleColor Color { get; set; }

    public List<Point>PosicionesNave { get; set; }
    public Ventana Ventana { get; set; }

    public Nave(Point position, ConsoleColor color, Ventana ventana)
    {
        this.Vida = 100;
        this.Position = position;
        this.Color = color;
        this.Ventana = ventana;
        PosicionesNave = new List<Point>();
    }

    public void DibujarNave()
    {
        Console.ForegroundColor = Color;
        int x = Position.X;
        int y = Position.Y;

        Console.SetCursorPosition(x+3, y);
        Console.Write("A");
        Console.SetCursorPosition(x + 1, y + 1);
        Console.Write("<{X}>");
        Console.SetCursorPosition(x, y + 2);
        Console.Write("±W   W±");

        PosicionesNave.Clear();

        PosicionesNave.Add(new Point(x + 3, y));
        PosicionesNave.Add(new Point(x + 1, y + 1));
        PosicionesNave.Add(new Point(x + 2, y + 1));
        PosicionesNave.Add(new Point(x + 3, y + 1));
        PosicionesNave.Add(new Point(x + 4, y + 1));
        PosicionesNave.Add(new Point(x + 5, y + 1));


        PosicionesNave.Add(new Point(x, y + 2));
        PosicionesNave.Add(new Point(x + 1, y + 2));
        PosicionesNave.Add(new Point(x +5, y + 2));
        PosicionesNave.Add(new Point(x + 6, y + 2));


    }

    public void Borrar() { 
        foreach(var posicion in PosicionesNave)
        {
            Console.SetCursorPosition(posicion.X, posicion.Y);
            Console.Write(" ");
        }
    }

    public void Mover(int velocidad)
    {
        if (Console.KeyAvailable)
        {
            Borrar();
            Point distancia = new Point();
            Teclado(ref distancia, velocidad);
            Colisiones(distancia);
            DibujarNave();
        }
    }

    public void Teclado(ref Point distancia, int velocidad)
    {
        ConsoleKeyInfo tecla = Console.ReadKey(true);

        if(tecla.Key == ConsoleKey.W) distancia = new Point(0, -1);
        if (tecla.Key == ConsoleKey.S) distancia = new Point(0, 1);
        if (tecla.Key == ConsoleKey.A) distancia = new Point(-1, 0);
        if (tecla.Key == ConsoleKey.D) distancia = new Point (1, 0);

        distancia.X *= velocidad;
        distancia.Y *= velocidad;
        Position = new Point(Position.X + distancia.X, Position.Y + distancia.Y);
    }

    public void Colisiones(Point distancia)
    {
        Point posicionAux = new Point(Position.X + distancia.X, Position.Y + distancia.Y );
        if(posicionAux.X <= Ventana.LimiteSuperior.X)posicionAux.X = Ventana.LimiteSuperior.X+1;
        if(posicionAux.X+6 >= Ventana.LimiteInferior.X) posicionAux.X = Ventana.LimiteInferior.X - 7;
        if(posicionAux.Y <= Ventana.LimiteSuperior.Y) posicionAux.Y = Ventana.LimiteSuperior.Y + 1;
        if (posicionAux.Y + 2 >= Ventana.LimiteInferior.Y) posicionAux.Y = Ventana.LimiteInferior.Y - 3;

        Position = posicionAux;

    }
}

