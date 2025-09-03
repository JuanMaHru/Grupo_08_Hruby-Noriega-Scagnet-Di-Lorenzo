using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    [Header("Refs UI")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text qtyText; // opcional en inventario

    public Item Item { get; private set; }

    public enum SlotContext { Store, Inventory }
    [SerializeField] private SlotContext context;

    public System.Action<Item> OnLeftClick;
    public System.Action<Item> OnRightClick;

    public void SetData(Item item, int? cantidad = null)
    {
        Item = item;
        if (icon) icon.sprite = item.Sprite;
        if (nameText) nameText.text = item.Nombre;
        if (priceText) priceText.text = "$" + item.Precio;
        if (qtyText) qtyText.text = cantidad.HasValue ? "x" + cantidad.Value : "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Item == null) return;

        if (eventData.button == PointerEventData.InputButton.Left)
            OnLeftClick?.Invoke(Item);
        else if (eventData.button == PointerEventData.InputButton.Right)
            OnRightClick?.Invoke(Item);
    }
}
