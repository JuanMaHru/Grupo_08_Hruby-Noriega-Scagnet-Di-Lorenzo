using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MySetUI : MonoBehaviour
{
    public Text setAText;
    public Text setBText;
    public Text resultText;

    private MySetArray<int> setA = new MySetArray<int>();
    private MySetList<int> setB = new MySetList<int>();

    public InputField inputA;
    public InputField inputB;

    // ======= Agregar =======
    public void AddToA()
    {
        if (int.TryParse(inputA.text, out int value))
        {
            setA.Add(value);
            UpdateSetAText();
        }
    }

    public void AddToB()
    {
        if (int.TryParse(inputB.text, out int value))
        {
            setB.Add(value);
            UpdateSetBText();
        }
    }

    // ======= Remover =======
    public void RemoveFromA()
    {
        if (int.TryParse(inputA.text, out int value))
        {
            setA.Remove(value);
            UpdateSetAText();
        }
    }

    public void RemoveFromB()
    {
        if (int.TryParse(inputB.text, out int value))
        {
            setB.Remove(value);
            UpdateSetBText();
        }
    }

    // ======= Limpiar =======
    public void ClearA()
    {
        setA.Clear();
        UpdateSetAText();
    }

    public void ClearB()
    {
        setB.Clear();
        UpdateSetBText();
    }

    // ======= Operaciones de conjuntos =======
    public void UnionSets()
    {
        MySet<int> union = setA.Union(setB);
        resultText.text = "Union: " + union.ToString();
    }

    public void IntersectSets()
    {
        MySet<int> intersect = setA.Intersect(setB);
        resultText.text = "Intersect: " + intersect.ToString();
    }

    public void DifferenceSets()
    {
        MySet<int> diff = setA.Difference(setB);
        resultText.text = "Difference: " + diff.ToString();
    }

    // ======= Actualizar Text =======
    private void UpdateSetAText()
    {
        setAText.text = "Set A: " + setA.ToString();
    }

    private void UpdateSetBText()
    {
        setBText.text = "Set B: " + setB.ToString();
    }
}
