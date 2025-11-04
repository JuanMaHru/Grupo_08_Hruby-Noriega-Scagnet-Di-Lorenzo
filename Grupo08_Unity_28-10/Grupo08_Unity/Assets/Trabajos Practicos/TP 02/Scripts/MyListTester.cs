using UnityEngine;
using TMPro;
using MyLinkedList;
using System;

public class MyListTester : MonoBehaviour
{
    public TMP_InputField inputValue; 
    public TMP_InputField inputIndex; 
    public TMP_Text output;           

    private MyList<string> list = new MyList<string>();

    public void BtnAdd()
    {
        list.Add(inputValue.text);
        Show("Add");
    }

    public void BtnInsert()
    {
        try
        {
            if (!int.TryParse(inputIndex.text, out int i)) { ShowError("Index inválido"); return; }
            list.Insert(i, inputValue.text);
            Show($"Insert at {i}");
        }
        catch (Exception e) { ShowError(e.Message); }
    }

    public void BtnRemoveValue()
    {
        bool ok = list.Remove(inputValue.text);
        Show(ok ? $"Remove \"{inputValue.text}\"" : $"\"{inputValue.text}\" not found");
    }

    public void BtnRemoveAt()
    {
        try
        {
            if (!int.TryParse(inputIndex.text, out int i)) { ShowError("Index inválido"); return; }
            list.RemoveAt(i);
            Show($"RemoveAt {i}");
        }
        catch (Exception e) { ShowError(e.Message); }
    }

    public void BtnAddRangeArray()
    {
        var parts = string.IsNullOrWhiteSpace(inputValue.text)
            ? Array.Empty<string>()
            : inputValue.text.Split(',');
        for (int j = 0; j < parts.Length; j++) parts[j] = parts[j].Trim();
        list.AddRange(parts);
        Show("AddRange(T[])");
    }

    public void BtnClear()
    {
        list.Clear();
        Show("Clear");
    }

    public void BtnShow() => Show("Show");

    private void Show(string action)
    {
        output.text = $"{action}\nCount: {list.Count}\nEmpty: {list.IsEmpty()}\nList: {list}";
    }

    private void ShowError(string msg)
    {
        output.text = $"<color=#ff6666><b>Error:</b> {msg}</color>\n\n" +
                      $"Count: {list.Count}\nEmpty: {list.IsEmpty()}\nList: {list}";
    }
}
