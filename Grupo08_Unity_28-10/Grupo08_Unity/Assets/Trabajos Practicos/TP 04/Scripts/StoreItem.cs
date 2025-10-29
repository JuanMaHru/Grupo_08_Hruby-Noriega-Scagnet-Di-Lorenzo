using UnityEngine;

namespace TP04.Store
{
    public enum ItemRarity { Common, Uncommon, Rare, Epic, Legendary }
    public enum ItemType { Consumable, Weapon, Armor, Key, Misc }

    [System.Serializable]
    public class StoreItem
    {
        public int id;
        public string name;
        public float price;
        public ItemRarity rarity;
        public ItemType type;

        public override string ToString()
            => $"[{id}] {name} | ${price:F2} | {rarity} | {type}";
    }
}
