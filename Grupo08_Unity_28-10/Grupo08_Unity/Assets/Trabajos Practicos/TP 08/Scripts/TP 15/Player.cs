public class Player
{
    public string name;
    public Inventory inventory;

    public Player(string playerName)
    {
        name = playerName;
        inventory = new Inventory();
    }

    public int GetItemCount()
    {
        return inventory.items.Count;
    }
}
