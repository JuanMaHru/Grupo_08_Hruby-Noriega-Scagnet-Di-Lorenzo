using UnityEngine;

public class TestMySet : MonoBehaviour
{
    void Start()
    {
        MySetArray<int> setArray = new MySetArray<int>();
        setArray.Add(1);
        setArray.Add(2);
        setArray.Add(3);
        setArray.Add(2); // duplicado, no se agrega
        Debug.Log("Array Set: " + setArray.ToString());

        MySetList<int> setList = new MySetList<int>();
        setList.Add(3);
        setList.Add(4);
        setList.Add(5);
        Debug.Log("List Set: " + setList.ToString());

        MySet<int> unionSet = setArray.Union(setList);
        Debug.Log("Union: " + unionSet.ToString());

        MySet<int> intersectSet = setArray.Intersect(setList);
        Debug.Log("Intersect: " + intersectSet.ToString());

        MySet<int> diffSet = setArray.Difference(setList);
        Debug.Log("Difference: " + diffSet.ToString());
    }
}
