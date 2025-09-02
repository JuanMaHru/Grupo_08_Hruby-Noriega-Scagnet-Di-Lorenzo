using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyLinkedList;
using System.Linq;

public class StoreUI : MonoBehaviour
{
    [Header("Refs Lógicas")]
    [SerializeField] private Store store;
    [SerializeField] private Wallet wallet;
    [SerializeField] private PlayerInventory playerInv;

    [Header("UI")]
    [SerializeField] private Transform contentStore;   // contenedor de slots
    [SerializeField] private GameObject slotPrefab;    // Prefab con ItemSlotUI
    [SerializeField] private TMP_Dropdown sortDropdown;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text feedbackText;    // mensajes breves (opcional)

    private void Start()
    {
        if (wallet != null)
        {
            wallet.MoneyChanged += OnMoneyChanged;
            OnMoneyChanged(wallet.Dinero);
        }

        if (sortDropdown != null)
        {
            sortDropdown.ClearOptions();
            sortDropdown.AddOptions(new System.Collections.Generic.List<string> {
                "ID ↑", "ID ↓", "Nombre A-Z", "Nombre Z-A", "Precio ↑", "Precio ↓", "Rareza ↑", "Rareza ↓", "Tipo ↑", "Tipo ↓"
            });
            sortDropdown.onValueChanged.AddListener(_ => Redibujar());
        }

        Redibujar();
    }

    private void OnDestroy()
    {
        if (wallet != null) wallet.MoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int nuevo) => moneyText.text = "$ " + nuevo;

    private void ClearContent()
    {
        for (int i = contentStore.childCount - 1; i >= 0; i--)
            Destroy(contentStore.GetChild(i).gameObject);
    }

    private MyList<Item> ObtenerItemsOrdenados()
    {
        var items = store.GetItems(); // MyList<Item>

        // Convertimos a IEnumerable para ordenar, luego volvemos a MyList
        System.Collections.Generic.IEnumerable<Item> seq = items;

        switch (sortDropdown?.value ?? 0)
        {
            case 0: seq = seq.OrderBy(i => i.ID); break;
            case 1: seq = seq.OrderByDescending(i => i.ID); break;
            case 2: seq = seq.OrderBy(i => i.Nombre); break;
            case 3: seq = seq.OrderByDescending(i => i.Nombre); break;
            case 4: seq = seq.OrderBy(i => i.Precio); break;
            case 5: seq = seq.OrderByDescending(i => i.Precio); break;
            case 6: seq = seq.OrderBy(i => i.Rareza); break;
            case 7: seq = seq.OrderByDescending(i => i.Rareza); break;
            case 8: seq = seq.OrderBy(i => i.Tipo); break;
            case 9: seq = seq.OrderByDescending(i => i.Tipo); break;
            default: break;
        }

        var salida = new MyList<Item>();
        foreach (var it in seq) salida.Add(it);
        return salida;
    }

    public void Redibujar()
    {
        ClearContent();
        var list = ObtenerItemsOrdenados();

        foreach (var item in list)
        {
            var go = Instantiate(slotPrefab, contentStore);
            var slot = go.GetComponent<ItemSlotUI>();
            slot.SetData(item);
            slot.OnLeftClick = OnComprarClick; // Izquierdo = Comprar
            slot.OnRightClick = _ => MostrarFeedback("Usá click izquierdo para comprar.");
        }
    }

    private void OnComprarClick(Item item)
    {
        if (!wallet.PuedePagar(item.Precio))
        {
            MostrarFeedback("Dinero insuficiente.");
            return;
        }

        wallet.Pagar(item.Precio);
        playerInv.Agregar(item, 1);
        MostrarFeedback($"Compraste {item.Nombre}.");
    }

    private void MostrarFeedback(string msg)
    {
        if (feedbackText == null) return;
        feedbackText.text = msg;
        // Podrías disparar una corrutina para limpiar el mensaje a los 2s.
    }
}
