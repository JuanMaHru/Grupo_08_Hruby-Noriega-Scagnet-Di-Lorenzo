using UnityEngine;

public enum ItemRarity { Common, Uncommon, Rare, Epic, Legendary }
public enum ItemType { Consumable, Weapon, Armor, Material, Quest }

[System.Serializable]
public class Item
{
    public int ID;
    public string Nombre;
    public int Precio;
    public ItemRarity Rareza;
    public ItemType Tipo;
    public Sprite Sprite;

    public override string ToString() => $"{ID} - {Nombre} (${Precio}) [{Rareza} {Tipo}]";
}
