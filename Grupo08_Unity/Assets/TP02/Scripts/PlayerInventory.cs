using System.Collections.Generic;
using UnityEngine;
using MyLinkedList;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<int, InventoryEntry> Items { get; private set; } = new Dictionary<int, InventoryEntry>();

    public void Agregar(Item item, int cantidad = 1)
    {
        if (Items.TryGetValue(item.ID, out var entry))
        {
            entry.Cantidad += cantidad;
        }
        else
        {
            Items[item.ID] = new InventoryEntry(item, cantidad);
        }
    }

    public bool Quitar(Item item, int cantidad = 1)
    {
        if (!Items.TryGetValue(item.ID, out var entry)) return false;
        if (entry.Cantidad < cantidad) return false;

        entry.Cantidad -= cantidad;
        if (entry.Cantidad == 0) Items.Remove(item.ID);
        return true;
    }

    public int GetCantidad(Item item) => Items.TryGetValue(item.ID, out var e) ? e.Cantidad : 0;

    public MyList<InventoryEntry> GetEntries()
    {
        var list = new MyList<InventoryEntry>();
        foreach (var kv in Items)
            list.Add(kv.Value);
        return list;
    }
}
