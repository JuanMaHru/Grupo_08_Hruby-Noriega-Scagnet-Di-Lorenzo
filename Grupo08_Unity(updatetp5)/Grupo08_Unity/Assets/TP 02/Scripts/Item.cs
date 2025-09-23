using UnityEngine;

namespace TP02.Store
{
    public enum ItemRarity { Common, Uncommon, Rare, Epic, Legendary }
    public enum ItemType { Consumable, Weapon, Armor, Key, Misc }

    [System.Serializable]
    public class Item
    {
        public int id;
        public string name;
        public float price;
        public ItemRarity rarity;
        public ItemType type;
        public Sprite icon;

        public override string ToString()
            => $"[{id}] {name} | ${price:F2} | {rarity} | {type}";
    }
}
