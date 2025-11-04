using UnityEngine;
using TMPro;

// Este script conecta la lista que implementamos (SimpleList<int>) con la interfaz gráfica de Unity. 

public class SimpleListTester : MonoBehaviour
{
    public TextMeshProUGUI outputText;

    private SimpleList<int> lista = new SimpleList<int>();

    // Botón "Add Item": agrega un número aleatorio a la lista
    public void AddItem()
    {
        int random = Random.Range(1, 100);  
        lista.Add(random);                  
        UpdateUI($"Agregado: {random}");     
    }

    // Botón "Remove Item": elimina el primer elemento de la lista (si existe)
    public void RemoveItem()
    {
        if (lista.Count > 0) 
        {
            int value = lista[0];                  
            bool removed = lista.Remove(value);    
            
            UpdateUI(removed ? $"Eliminado: {value}" : "No se encontró");
        }
        else
        {
            UpdateUI("La lista está vacía");
        }
    }

    // Botón "Clear List": borra todos los elementos de la lista
    public void ClearList()
    {
        lista.Clear();                
        UpdateUI("Lista vacía");       
    }

    // Botón "Show List": imprime el contenido actual de la lista
    public void ShowList()
    {
        UpdateUI($"Contenido: {lista.ToString()}");
    }


    // Método privado para centralizar la lógica de actualizar el panel de salida
    private void UpdateUI(string msg)
    {
        if (outputText != null)
            outputText.text = msg + "\n\nLista actual: " + lista.ToString();
    }
}
