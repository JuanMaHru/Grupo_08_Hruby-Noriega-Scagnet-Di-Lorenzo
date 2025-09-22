using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;  // Para InputField y TextMeshPro
using UnityEngine;
using UnityEngine.UI;

public class RecursionUI : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text outputText;

    private int Fibonacci(int n)
    {
        // Caso base 1: cuando n es 0
        if (n == 0) return 0;

        // Caso base 2: cuando n es 1
        if (n == 1) return 1;

        // Caso recursivo:
        // Para cualquier otro número mayor a 1, 
        // vuelve a llamar la función con n-1 y n-2.
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    // Métodos que se llamarán desde los botones
    public void OnFibonacci()
    {
        // 1️⃣ Intenta convertir el texto del InputField a un número entero
        //    Si falla, 'int.TryParse' devuelve false.
        if (int.TryParse(inputField.text, out int cantidad))
        {
            // 2️⃣ Si el usuario pone un número negativo, mostramos error.
            if (cantidad < 0)
            {
                outputText.text = "Ingrese un número positivo.";
                return; // Salimos del método
            }

            // 3️⃣ Creamos un string vacío para concatenar la secuencia.
            string resultado = "";

            // 4️⃣ Usamos un bucle para calcular cada número de la secuencia
            for (int i = 0; i < cantidad; i++)
            {
                // Llamamos a la función recursiva para obtener Fib(i)
                // y lo concatenamos al string con coma y espacio
                resultado += Fibonacci(i);

                // Agregamos coma y espacio si no es el último número
                if (i < cantidad - 1)
                {
                    resultado += ", ";
                }
            }

            // 5️⃣ Mostramos en el Text el resultado
            outputText.text = "Fibonacci (" + cantidad + "): " + resultado;
        }
        else
        {
            // Si el usuario no puso un número válido, mostramos mensaje de error.
            outputText.text = "Ingrese un número válido.";
        }
    }

    // Factorial(n) = n * Factorial(n-1)
    // Caso base: Factorial(0) = 1
    private int Factorial(int n)
    {
        if (n <= 1) return 1; // caso base
        return n * Factorial(n - 1); // caso recursivo
    }

    public void OnFactorial()
    {
        if (int.TryParse(inputField.text, out int n))
        {
            if (n < 0)
            {
                outputText.text = "Ingrese un número positivo.";
                return;
            }

            int resultado = Factorial(n);
            outputText.text = "Factorial de " + n + " = " + resultado;
        }
        else
        {
            outputText.text = "Ingrese un número válido.";
        }
    }

    // SumaHasta(n) = n + SumaHasta(n-1)
    // Caso base: SumaHasta(0) = 0
    private int SumaHasta(int n)
    {
        if (n <= 0) return 0; // caso base
        return n + SumaHasta(n - 1); // caso recursivo
    }

    public void OnSuma()
    {
        if (int.TryParse(inputField.text, out int n))
        {
            if (n < 0)
            {
                outputText.text = "Ingrese un número positivo.";
                return;
            }

            int resultado = SumaHasta(n);
            outputText.text = "Suma de todos los números hasta " + n + " = " + resultado;
        }
        else
        {
            outputText.text = "Ingrese un número válido.";
        }
    }

    // Construye una pirámide de 'x' centrada.


private string ConstruirPiramideRec(int nivel, int altura)
{
    if (nivel > altura) return "";
        
    int cantidadX = 2 * nivel - 1; // cada línea tiene 2 más que la anterior
    int espacios = altura - nivel; // para centrar

    string linea = new string(' ', espacios) + new string('x', cantidadX) + "\n";

    return linea + ConstruirPiramideRec(nivel + 1, altura);
}

    public void OnPiramide()
    {
        if (int.TryParse(inputField.text, out int altura))
        {
            if (altura <= 0)
            {
                outputText.text = "Ingrese un número positivo.";
                return;
            }

            string piramide = ConstruirPiramideRec(1,altura);
            outputText.text = piramide;
        }
        else
        {
            outputText.text = "Ingrese un número válido.";
        }
    }


    // Verifica si una cadena es palíndromo ignorando espacios y mayúsculas.
    private bool EsPalindromo(string texto)
    {
        // Limpia espacios y convierte a minúsculas
        string limpio = new string(texto
            .ToLower()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());

        return EsPalindromoRec(limpio, 0, limpio.Length - 1);
    }

    private bool EsPalindromoRec(string s, int izquierda, int derecha)
    {
        if (izquierda >= derecha) return true; // caso base: llegó al centro
        if (s[izquierda] != s[derecha]) return false; // caso base: no coinciden
        return EsPalindromoRec(s, izquierda + 1, derecha - 1); // caso recursivo
    }
    public void OnPalindromo()
    {
        string texto = inputField.text;

        if (string.IsNullOrWhiteSpace(texto))
        {
            outputText.text = "Ingrese un texto.";
            return;
        }

        bool esPal = EsPalindromo(texto);

        outputText.text = esPal
            ? $"\"{texto}\" es un palíndromo"
            : $"\"{texto}\" NO es un palíndromo";
    }

}

