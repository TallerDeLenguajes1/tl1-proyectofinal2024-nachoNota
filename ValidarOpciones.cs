using System;
using MovimientosCombate;


public class ValidarOpciones
{
    public int ValidarOpcion(int max)
    {
        bool opcionValida = false;
        int opcion = 0;

        while (!opcionValida)
        {
            string input = Console.ReadLine();

            if (int.TryParse(input, out opcion) && (opcion >= 1 && opcion <= max))
            {
                opcionValida = true;
            }
            else
            {
                Console.WriteLine($"Por favor, ingrese un número válido entre 1 y {max}.\n");
            }
        }

        return opcion;
    }


    public int ValidarOponente()
    {
        bool opcionValida = false;
        int opcion = 0;
        while (!opcionValida)
        {
            Console.Write("Opcion: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out opcion) && (opcion == 1 || opcion == 0))
            {
                opcionValida = true;
            }
            else
            {
                Console.WriteLine($"Por favor, ingrese un número válido, ya sea 0 o 1.");
            }
        }
        return opcion;
    }

    public (int, Movimientos) ValidarMovimiento(Dictionary<int, Movimientos> movimientosClaves)
    {
        bool opcionValida = false;
        int opcion = 0;

        while (!opcionValida)
        {
            Console.Write("Opcion: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out opcion) && movimientosClaves.ContainsKey(opcion))
            {
                opcionValida = true;
            }
            else
            {
                Console.WriteLine($"Por favor, ingrese un número válido entre 1 y {movimientosClaves.Count}");
            }
        }
        return (opcion, movimientosClaves[opcion]);
    }

}