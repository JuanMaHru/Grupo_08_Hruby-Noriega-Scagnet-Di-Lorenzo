using System;

namespace QuickSortSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] data = { 2, 9, 5, 7 };

            // Ordenamos el array con QuickSort
            QuickSort(data, 0, data.Length - 1);

            // Mostramos el resultado final
            Console.WriteLine("Array ordenado:");
            foreach (int num in data)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine("\n\nPresioná Enter para salir...");
            Console.ReadLine();
        }

        static void QuickSort(int[] a, int low, int high)
        {
            if (low < high)
            {
                int p = Partition(a, low, high);
                QuickSort(a, low, p - 1);
                QuickSort(a, p + 1, high);
            }
        }

        // Partición de Lomuto (pivote = último elemento)
        static int Partition(int[] a, int low, int high)
        {
            int pivot = a[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (a[j] < pivot)
                {
                    i++;
                    Swap(ref a[i], ref a[j]);
                }
            }

            Swap(ref a[i + 1], ref a[high]);
            return i + 1;
        }

        static void Swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }
    }
}
