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
        if (items.Count < 20)
        {
            // No combinamos ni modificamos cantidad,
            // porque cada "item" ya viene con su propio quantity.
            items.Add(item);
        }
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }
}

