using System.Collections.Generic;
using UnityEngine;
using MyLinkedList;

public class Store : MonoBehaviour
{
    public Dictionary<int, Item> Catalogo { get; private set; } = new Dictionary<int, Item>();

    [Header("Sprites de ejemplo")]
    [SerializeField] private Sprite sprPocion;
    [SerializeField] private Sprite sprEspada;
    [SerializeField] private Sprite sprArmadura;

    private void Awake()
    {
        // Datos de ejemplo — en un proyecto real, podrías cargar desde ScriptableObjects/JSON.
        AgregarNuevo(new Item { ID = 1, Nombre = "Poción", Precio = 50, Rareza = ItemRarity.Common, Tipo = ItemType.Consumable, Sprite = sprPocion });
        AgregarNuevo(new Item { ID = 2, Nombre = "Espada", Precio = 200, Rareza = ItemRarity.Uncommon, Tipo = ItemType.Weapon, Sprite = sprEspada });
        AgregarNuevo(new Item { ID = 3, Nombre = "Armadura", Precio = 350, Rareza = ItemRarity.Rare, Tipo = ItemType.Armor, Sprite = sprArmadura });
    }

    public bool TryGetItem(int id, out Item item) => Catalogo.TryGetValue(id, out item);

    public void AgregarNuevo(Item item)
    {
        if (!Catalogo.ContainsKey(item.ID))
            Catalogo.Add(item.ID, item);
        else
            Catalogo[item.ID] = item;
    }

    // Utilidad: obtener items como MyList<Item> (sin usar List<T>)
    public MyList<Item> GetItems()
    {
        var list = new MyList<Item>();
        foreach (var kv in Catalogo)
            list.Add(kv.Value);
        return list;
    }
}
