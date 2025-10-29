using UnityEngine; 

[System.Serializable]
public class Item
{
    public string name;
    public float price;
    public Sprite icon; // Sprite para la textura del ítem (para usar en la UI)

    public Item(string itemName, float itemPrice, Sprite itemIcon)
    {
        name = itemName;
        price = itemPrice;
        icon = itemIcon;
    }
}
