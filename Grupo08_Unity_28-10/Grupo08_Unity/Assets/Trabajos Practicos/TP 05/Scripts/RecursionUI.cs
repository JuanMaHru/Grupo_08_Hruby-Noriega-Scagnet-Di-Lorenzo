using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;  
using UnityEngine;
using UnityEngine.UI;

public class RecursionUI : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text outputText;

    private int Fibonacci(int n)
    {
        if (n == 0) return 0;

        if (n == 1) return 1;

        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    public void OnFibonacci()
    {

        if (int.TryParse(inputField.text, out int cantidad))
        {
            if (cantidad < 0)
            {
                outputText.text = "Ingrese un número positivo.";
                return;
            }

            string resultado = "";

            for (int i = 0; i < cantidad; i++)
            {
                resultado += Fibonacci(i);

                if (i < cantidad - 1)
                {
                    resultado += ", ";
                }
            }

            outputText.text = "Fibonacci (" + cantidad + "): " + resultado;
        }
        else
        {
            outputText.text = "Ingrese un número válido.";
        }
    }

    private int Factorial(int n)
    {
        if (n <= 1) return 1; 
        return n * Factorial(n - 1); 
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

    private int SumaHasta(int n)
    {
        if (n <= 0) return 0; 
        return (n - 1) + SumaHasta(n - 1); 
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



private string ConstruirPiramideRec(int nivel, int altura)
{
    if (nivel > altura) return "";
        
    int cantidadX = 2 * nivel - 1; 
    int espacios = altura - nivel; 

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

    private bool EsPalindromo(string texto)
    {
        string limpio = new string(texto
            .ToLower()
            .Where(c => !char.IsWhiteSpace(c))
            .ToArray());

        return EsPalindromoRec(limpio, 0, limpio.Length - 1);
    }

    private bool EsPalindromoRec(string s, int izquierda, int derecha)
    {
        if (izquierda >= derecha) return true; 
        if (s[izquierda] != s[derecha]) return false; 
        return EsPalindromoRec(s, izquierda + 1, derecha - 1); 
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
            ? $"\"{texto}\" ES un palíndromo"
            : $"\"{texto}\" NO es un palíndromo";
    }

}

