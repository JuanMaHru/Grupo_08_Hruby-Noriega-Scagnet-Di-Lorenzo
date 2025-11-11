using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;   // Nombre único
    public Sprite icon;       // Icono
    public bool isStackable;  // True = se puede apilar (bloques), False = único (espadas, armaduras)
}
