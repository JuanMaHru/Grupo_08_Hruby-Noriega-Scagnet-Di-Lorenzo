using UnityEngine;
using TMPro;

// Este script conecta la lista que implementamos (SimpleList<int>) con la interfaz gráfica de Unity. 

public class SimpleListTester : MonoBehaviour
{
    public TextMeshProUGUI outputText;

    // Creamos una instancia de nuestra lista.
    private SimpleList<int> lista = new SimpleList<int>();

    // Botón "Add Item": agrega un número aleatorio a la lista
    public void AddItem()
    {
        int random = Random.Range(1, 100);   // Genera un valor entre 1 y 99
        lista.Add(random);                   // Lo agrega al final de la lista
        UpdateUI($"Agregado: {random}");     // Actualiza la interfaz con el mensaje
    }

    // Botón "Remove Item": elimina el primer elemento de la lista (si existe)
    public void RemoveItem()
    {
        if (lista.Count > 0) // Solo si la lista no está vacía
        {
            int value = lista[0];                  // Tomamos el primer elemento
            bool removed = lista.Remove(value);    // Lo intentamos eliminar
            // Si se eliminó, mostramos qué valor se quitó; 
            // si no estaba, mostramos un mensaje de error
            UpdateUI(removed ? $"Eliminado: {value}" : "No se encontró");
        }
        else
        {
            // Caso especial: si la lista ya estaba vacía
            UpdateUI("La lista está vacía");
        }
    }

    // Botón "Clear List": borra todos los elementos de la lista
    public void ClearList()
    {
        lista.Clear();                 // Limpia la lista interna
        UpdateUI("Lista vacía");       // Lo comunicamos en la UI
    }

    // Botón "Show List": imprime el contenido actual de la lista
    public void ShowList()
    {
        // Llamamos a ToString() de SimpleList, que devuelve todos los elementos separados por coma
        UpdateUI($"Contenido: {lista.ToString()}");
    }


    // Método privado para centralizar la lógica de actualizar el panel de salida
    private void UpdateUI(string msg)
    {
        if (outputText != null)
            // Mostramos el mensaje recibido y, debajo, la lista completa
            outputText.text = msg + "\n\nLista actual: " + lista.ToString();
    }
}
