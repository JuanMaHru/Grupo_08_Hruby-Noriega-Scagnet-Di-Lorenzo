using UnityEngine;
using TMPro;

// Este script conecta la lista que implementamos (SimpleList<int>) con la interfaz gr�fica de Unity. 

public class SimpleListTester : MonoBehaviour
{
    public TextMeshProUGUI outputText;

    // Creamos una instancia de nuestra lista.
    private SimpleList<int> lista = new SimpleList<int>();

    // Bot�n "Add Item": agrega un n�mero aleatorio a la lista
    public void AddItem()
    {
        int random = Random.Range(1, 100);   // Genera un valor entre 1 y 99
        lista.Add(random);                   // Lo agrega al final de la lista
        UpdateUI($"Agregado: {random}");     // Actualiza la interfaz con el mensaje
    }

    // Bot�n "Remove Item": elimina el primer elemento de la lista (si existe)
    public void RemoveItem()
    {
        if (lista.Count > 0) // Solo si la lista no est� vac�a
        {
            int value = lista[0];                  // Tomamos el primer elemento
            bool removed = lista.Remove(value);    // Lo intentamos eliminar
            // Si se elimin�, mostramos qu� valor se quit�; 
            // si no estaba, mostramos un mensaje de error
            UpdateUI(removed ? $"Eliminado: {value}" : "No se encontr�");
        }
        else
        {
            // Caso especial: si la lista ya estaba vac�a
            UpdateUI("La lista est� vac�a");
        }
    }

    // Bot�n "Clear List": borra todos los elementos de la lista
    public void ClearList()
    {
        lista.Clear();                 // Limpia la lista interna
        UpdateUI("Lista vac�a");       // Lo comunicamos en la UI
    }

    // Bot�n "Show List": imprime el contenido actual de la lista
    public void ShowList()
    {
        // Llamamos a ToString() de SimpleList, que devuelve todos los elementos separados por coma
        UpdateUI($"Contenido: {lista.ToString()}");
    }


    // M�todo privado para centralizar la l�gica de actualizar el panel de salida
    private void UpdateUI(string msg)
    {
        if (outputText != null)
            // Mostramos el mensaje recibido y, debajo, la lista completa
            outputText.text = msg + "\n\nLista actual: " + lista.ToString();
    }
}
