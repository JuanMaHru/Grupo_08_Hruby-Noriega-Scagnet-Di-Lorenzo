using UnityEngine;
using TMPro;
using MyLinkedList;
using System;

public class MyListTester : MonoBehaviour
{
    public TMP_InputField inputValue; 
    public TMP_InputField inputIndex; 
    public TMP_Text output;           

    private MyList<string> lista = new MyList<string>();

    public void BtnAdd()
    {
        lista.Add(inputValue.text);
        Show("Add");
    }

    public void BtnInsert()
    {
        try
        {
            if (!int.TryParse(inputIndex.text, out int i))
            { 
                Debug.Log("Index inválido");
                return;
            }

            lista.Insert(i, inputValue.text);
            Show($"Insert at {i}");
        }
        catch (Exception e) 
        { 
            Debug.Log(e.Message);
        }
    }

    public void BtnRemoveValue()
    {
        bool ok = lista.Remove(inputValue.text);
        Show(ok ? $"Remove \"{inputValue.text}\"" : $"\"{inputValue.text}\" not found");
    }

    public void BtnRemoveAt()
    {
        try
        {
            if (!int.TryParse(inputIndex.text, out int i)) 
            {
                Debug.Log("Index inválido"); 
                return;
            }
            lista.RemoveAt(i);
            Show($"RemoveAt {i}");
        }
        catch (Exception e) 
        { 
            Debug.Log(e.Message);
        }
    }

    public void BtnAddRangeArray()
    {
        var parts = string.IsNullOrWhiteSpace(inputValue.text)
            ? Array.Empty<string>()
            : inputValue.text.Split(',');
        for (int j = 0; j < parts.Length; j++) parts[j] = parts[j].Trim();
        lista.AddRange(parts);
        Show("AddRange(T[])");
    }

    public void BtnClear()
    {
        lista.Clear();
        Show("Clear");
    }

    public void BtnShow() => Show("Show");

    private void Show(string action)
    {
        output.text = $"{action}\nCount: {lista.Count}\nEmpty: {lista.IsEmpty()}\nList: {lista}";
    }
}
