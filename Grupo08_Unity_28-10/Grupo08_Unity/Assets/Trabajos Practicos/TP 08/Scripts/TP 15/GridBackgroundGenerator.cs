using UnityEngine;

public class GridBackgroundGenerator : MonoBehaviour
{
    public GameObject slotPrefab;
    public int totalSlots = 120; // o 1200 si querés full inventario

    void Start()
    {
        for (int i = 0; i < totalSlots; i++)
        {
            Instantiate(slotPrefab, transform);
        }
    }
}
