using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TP02.Store
{
    public class ItemButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text title;
        [SerializeField] private Text priceOrQty;

        private StoreController store;
        private Item boundItem;               
        private InventoryEntry boundEntry;    

        public void BindStoreItem(Item item, StoreController store)
        {
            this.store = store;
            boundItem = item;
            boundEntry = null;

            if (icon) icon.sprite = item.icon;
            if (title) title.text = $"{item.name}  [{item.rarity}]";
            if (priceOrQty) priceOrQty.text = $"${item.price:F2}";
        }

        public void BindInventoryEntry(InventoryEntry entry, StoreController store)
        {
            this.store = store;
            boundEntry = entry;
            boundItem = null;

            if (icon) icon.sprite = entry.item.icon;
            if (title) title.text = $"{entry.item.name}  x{entry.quantity}";
            if (priceOrQty) priceOrQty.text = $"(sell 50%) ${entry.item.price * 0.5f:F2}";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (store == null) return;

            bool left = eventData.button == PointerEventData.InputButton.Left;
            bool right = eventData.button == PointerEventData.InputButton.Right;

            if (boundItem != null)
            {
                if (left) store.Buy(boundItem.id);
                else if (right) store.Sell(boundItem.id);
            }
            else if (boundEntry != null)
            {
                if (left) store.Sell(boundEntry.item.id);
                else if (right) store.Buy(boundEntry.item.id);
            }
        }
    }
}
