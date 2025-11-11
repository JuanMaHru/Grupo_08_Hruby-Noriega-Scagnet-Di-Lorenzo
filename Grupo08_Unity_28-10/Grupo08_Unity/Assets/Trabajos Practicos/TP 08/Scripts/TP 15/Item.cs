using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public Sprite icon;
    public int quantity;

    public Item(string name, int quantity, Sprite icon)
    {
        this.name = name;
        this.quantity = quantity;
        this.icon = icon;
    }

    // Comparación exacta (nombre + cantidad) — útil si alguna vez querés tratar instancias exactas.
    public override bool Equals(object obj)
    {
        if (obj is Item other)
            return name == other.name && quantity == other.quantity;
        return false;
    }

    public override int GetHashCode()
    {
        return (name + quantity.ToString()).GetHashCode();
    }

    // Comparación por tipo (solo nombre). Usar esto para lógica "común/único".
    public bool IsSameType(Item other)
    {
        return other != null && this.name == other.name;
    }
}
