using UnityEngine;
using TMPro;

public class SimpleListTester : MonoBehaviour
{
    public TextMeshProUGUI outputText;

    private DefinitiveSimpleList<int> lista = new DefinitiveSimpleList<int>();

    public void AddItem()
    {
        int random = Random.Range(1, 100);  
        lista.Add(random);                  
        UpdateUI($"Agregado: {random}");     
    }

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

    public void ClearList()
    {
        lista.Clear();                
        UpdateUI("Lista vacía");       
    }

    public void ShowList()
    {
        UpdateUI($"Contenido: {lista.ToString()}");
    }

    private void UpdateUI(string msg)
    {
        if (outputText != null)
            outputText.text = msg + "\n\nLista actual: " + lista.ToString();
    }
}
