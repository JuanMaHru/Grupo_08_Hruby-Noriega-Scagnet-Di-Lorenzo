using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ItemSlotUI : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text nameText;
    public TMP_Text priceText;
    public Image icon;

    private Item item;
    private Action<Item> onLeftClick;
    private Action<Item> onRightClick;

    public void Setup(Item newItem, Action<Item> leftClick, Action<Item> rightClick)
    {
        item = newItem;
        onLeftClick = leftClick;
        onRightClick = rightClick;

        nameText.text = item.Name;
        priceText.text = $"$ {item.Price}";
        // icon.sprite = ... // Opcional si tenés imágenes para los ítems
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) onLeftClick?.Invoke(item);
        else if (eventData.button == PointerEventData.InputButton.Right) onRightClick?.Invoke(item);
    }
}
