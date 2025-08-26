using System;
using System.Collections.Generic;
using System.Threading;

namespace Clase04
{
    class Program
    {
        static void Main(string[] args)
        {
            // PriorityQueue con tipos string(nombre) y int(prioridad)
            PriorityQueue<string, int> jugadores = new PriorityQueue<string, int>();

            jugadores.Enqueue("Emiliano Martinez", 1);
            jugadores.Enqueue("Nahuel Molina", 2);
            jugadores.Enqueue("Nicolas Otamendi", 2);
            jugadores.Enqueue("Cristian Romero", 2);
            jugadores.Enqueue("Nicolas Tagliafico", 2);
            jugadores.Enqueue("Enzo Fernandez", 3);
            jugadores.Enqueue("Alexis Mac Allister", 3);
            jugadores.Enqueue("Rodrigo De Paul", 3);
            jugadores.Enqueue("Lionel Messi", 4);
            jugadores.Enqueue("Juian Alvarez", 4);
            jugadores.Enqueue("Angel Di Maria", 4);
            jugadores.Enqueue("Marcos Acuña", 5);
            jugadores.Enqueue("Lisandro Martinez", 5);
            jugadores.Enqueue("German Pezzela", 5);
            jugadores.Enqueue("Leandro Paredes", 5);
            jugadores.Enqueue("Exequiel Palacios", 5);
            jugadores.Enqueue("Thiago Almada", 5);
            jugadores.Enqueue("Lautaro Martinez", 5);
            jugadores.Enqueue("Franco Armani", 5);
            jugadores.Enqueue("Franco Mastantuono", 5);

            Console.WriteLine("=== Jugadores convocados ===\n");

            // pacientes en orden de prioridad
            while (jugadores.Count > 0)
            {
                jugadores.TryDequeue(out string nombre, out int prioridad);

                string estado = (prioridad == 5) ? "Suplente" : "Titular";

                Console.WriteLine($"Jugador: {nombre} | Prioridad: {prioridad} | {estado}");

                // 1 segundo antes del siguiente paciente
                Thread.Sleep(1000);
            }

            Console.WriteLine("\nTodos los jugadores han sido presentados.");
        }
    }
}