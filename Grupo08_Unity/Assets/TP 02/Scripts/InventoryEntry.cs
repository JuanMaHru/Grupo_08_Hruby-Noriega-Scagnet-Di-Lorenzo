namespace TP02.Store
{
    [System.Serializable]
    public class InventoryEntry
    {
        public Item item;
        public int quantity;

        public InventoryEntry(Item item, int qty = 1)
        {
            this.item = item;
            this.quantity = qty;
        }
    }
}
