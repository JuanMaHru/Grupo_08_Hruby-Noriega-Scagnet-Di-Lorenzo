using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> items;

    public Inventory()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (items.Count < 20) // Limitar a 20 slots
        {
            items.Add(item);
        }
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }
}
