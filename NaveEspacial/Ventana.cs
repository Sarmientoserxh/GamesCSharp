using System;
using System.Drawing;

public class Ventana
{
    public int Ancho { get; set; }
    public int Alto { get; set; }
    public ConsoleColor Color { get; set; }
    public Point LimiteSuperior { get; set; }
    public Point LimiteInferior { get; set; }

    public Ventana(int ancho, int alto, ConsoleColor color, Point limiteSuperior, Point limiteInferior)
    {
        // Validar y ajustar al máximo permitido por la consola
        this.Ancho = Math.Min(ancho, Console.LargestWindowWidth);
        this.Alto = Math.Min(alto, Console.LargestWindowHeight);
        this.Color = color;

        // Ajustar límites dentro del rango válido
        this.LimiteSuperior = new Point(
            Math.Max(0, limiteSuperior.X),
            Math.Max(0, limiteSuperior.Y)
        );
        this.LimiteInferior = new Point(
            Math.Min(this.Ancho - 2, limiteInferior.X), // Restamos 2 para evitar desbordamiento
            Math.Min(this.Alto - 2, limiteInferior.Y)
        );
    }

    public void Init()
    {
        try
        {
            Console.SetWindowSize(Ancho, Alto);
            Console.SetBufferSize(Ancho, Alto);
        }
        catch
        {
            Console.WriteLine("No se pudo configurar el tamaño máximo de la ventana.");
        }

        Console.Title = "Nave Espacial";
        Console.BackgroundColor = Color;
        Console.Clear();
        Console.CursorVisible = false;
        DibujarMarco();
    }

    public void DibujarMarco()
    {
        // Dibujar líneas horizontales
        for (int i = LimiteSuperior.X; i <= LimiteInferior.X; i++)
        {
            // Línea superior
            Console.SetCursorPosition(i, LimiteSuperior.Y);
            Console.Write("═");
            // Línea inferior
            Console.SetCursorPosition(i, LimiteInferior.Y);
            Console.Write("═");
        }

        // Dibujar líneas verticales
        for (int i = LimiteSuperior.Y; i <= LimiteInferior.Y; i++)
        {
            // Línea izquierda
            Console.SetCursorPosition(LimiteSuperior.X, i);
            Console.Write("║");
            // Línea derecha
            Console.SetCursorPosition(LimiteInferior.X, i);
            Console.Write("║");
        }

        // Dibujar esquinas
        Console.SetCursorPosition(LimiteSuperior.X, LimiteSuperior.Y);
        Console.Write("╔");
        Console.SetCursorPosition(LimiteSuperior.X, LimiteInferior.Y);
        Console.Write("╚");
        Console.SetCursorPosition(LimiteInferior.X, LimiteSuperior.Y);
        Console.Write("╗");
        Console.SetCursorPosition(LimiteInferior.X, LimiteInferior.Y);
        Console.Write("╝");
    }
}
