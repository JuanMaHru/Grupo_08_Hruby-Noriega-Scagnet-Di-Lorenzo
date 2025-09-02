[System.Serializable]
public class InventoryEntry
{
    public Item Item;
    public int Cantidad;

    public InventoryEntry(Item item, int cantidad = 0)
    {
        Item = item;
        Cantidad = cantidad;
    }
}
