using System;

class Program
{
    static void Main(string[] args)
    {
        int[] numeros = { 1, 2, 3, 4 };
        int resultado = SumarArray(numeros, 0);

        Console.WriteLine("La suma es: " + resultado);
    }

    static int SumarArray(int[] array, int indice)
    {
       
        if (indice >= array.Length)
            return 0;

        return array[indice] + SumarArray(array, indice + 1);
    }
}
