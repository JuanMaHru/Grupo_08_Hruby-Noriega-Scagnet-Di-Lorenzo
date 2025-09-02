using UnityEngine;
using TMPro;
using MyLinkedList;

public class InventoryUI : MonoBehaviour
{
    [Header("Refs Lógicas")]
    [SerializeField] private PlayerInventory playerInv;
    [SerializeField] private Wallet wallet;

    [Header("UI")]
    [SerializeField] private Transform contentInventory;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private TMP_Text feedbackText;

    private void OnEnable() => Redibujar();

    public void Redibujar()
    {
        for (int i = contentInventory.childCount - 1; i >= 0; i--)
            Destroy(contentInventory.GetChild(i).gameObject);

        MyList<InventoryEntry> entries = playerInv.GetEntries();
        foreach (var entry in entries)
        {
            var go = Instantiate(slotPrefab, contentInventory);
            var slot = go.GetComponent<ItemSlotUI>();
            slot.SetData(entry.Item, entry.Cantidad);
            slot.OnLeftClick = _ => MostrarFeedback("Click derecho para vender.");
            slot.OnRightClick = item => Vender(item);
        }
    }

    private void Vender(Item item)
    {
        if (!playerInv.Quitar(item, 1))
        {
            MostrarFeedback("No tenés ese ítem.");
            return;
        }
        // Política de reventa simple: 50% del precio.
        int precioVenta = Mathf.Max(1, item.Precio / 2);
        wallet.Cobrar(precioVenta);
        MostrarFeedback($"Vendiste {item.Nombre} por ${precioVenta}.");
        Redibujar();
    }

    private void MostrarFeedback(string msg)
    {
        if (feedbackText == null) return;
        feedbackText.text = msg;
    }
}
