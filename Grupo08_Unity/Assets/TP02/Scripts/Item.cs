using System;

public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }
public enum ItemType { Weapon, Armor, Consumable, Material, Misc }

[Serializable]
public class Item
{
    public int Id;
    public string Name;
    public int Price;
    public Rarity Rarity;
    public ItemType Type;
    // opcional: public string Description;
}
