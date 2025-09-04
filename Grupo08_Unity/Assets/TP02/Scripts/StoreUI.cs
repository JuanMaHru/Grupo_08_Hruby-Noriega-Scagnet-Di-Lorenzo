using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class StoreUI : MonoBehaviour
{
    [Header("UI References")]
    public Transform contentParent;
    public GameObject itemSlotPrefab;
    public TMP_Text moneyText;
    public TMP_Dropdown dropdownSort;

    private Store store;
    private PlayerInventory player;

    void Start()
    {
        // Inicializar catálogo
        store = new Store();
        store.AddItem(new Item { Id = 1, Name = "Espada", Price = 100, Rarity = Rarity.Common, Type = ItemType.Weapon });
        store.AddItem(new Item { Id = 2, Name = "Poción", Price = 50, Rarity = Rarity.Uncommon, Type = ItemType.Consumable });
        store.AddItem(new Item { Id = 3, Name = "Armadura", Price = 150, Rarity = Rarity.Rare, Type = ItemType.Armor });

        // Inicializar jugador
        player = new PlayerInventory(500);

        // Configurar dropdown
        dropdownSort.onValueChanged.AddListener(_ => RefreshItems());

        RefreshMoney();
        RefreshItems();
    }

    private void RefreshMoney()
    {
        moneyText.text = $"Dinero: ${player.Money}";
    }

    private void RefreshItems()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        string sortKey = dropdownSort.options[dropdownSort.value].text;
        foreach (var item in store.GetSorted(sortKey))
        {
            var slot = Instantiate(itemSlotPrefab, contentParent);
            slot.GetComponent<ItemSlotUI>().Setup(item, OnLeftClickBuy, OnRightClickSell);
        }
    }

    private void OnLeftClickBuy(Item item)
    {
        if (player.Buy(item)) RefreshMoney();
        else Debug.Log("No tienes suficiente dinero");
    }

    private void OnRightClickSell(Item item)
    {
        if (player.Sell(item)) RefreshMoney();
        else Debug.Log("No tienes ese ítem en tu inventario");
    }
}
