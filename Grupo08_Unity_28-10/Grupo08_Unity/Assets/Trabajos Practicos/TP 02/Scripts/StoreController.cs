using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TP02.Store
{
    public enum SortField { ID, Name, Price, Rarity, Type }

    public class StoreController : MonoBehaviour
    {
        [Header("Data Sources")]
        [Tooltip("Carg� los �tems ac� por Inspector (Opci�n 1).")]
        [SerializeField] private Item[] initialItems;

        // Cat�logo e inventario: diccionarios (Key = ID) como pide el TP.
        public Dictionary<int, Item> catalog = new Dictionary<int, Item>();
        public Dictionary<int, InventoryEntry> playerInventory = new Dictionary<int, InventoryEntry>();

        [Header("Player")]
        public float playerMoney = 1000f;

        [Header("UI References")]
        [SerializeField] private Text moneyText;
        [SerializeField] private Transform storeGrid;     // Content del ScrollView de Store
        [SerializeField] private Transform inventoryGrid; // Content del ScrollView de Inventario
        [SerializeField] private GameObject itemButtonPrefab;
        [SerializeField] private Text sortFieldLabel;

        [Header("Sorting")]
        public SortField currentSort = SortField.ID;
        public bool ascending = true;

        private void Awake()
        {
            // Opci�n 1: cargar cat�logo desde el Inspector
            if (initialItems != null)
            {
                for (int i = 0; i < initialItems.Length; i++)
                {
                    var item = initialItems[i];
                    if (item != null) AddToCatalog(item);
                }
            }
        }

        private void Start()
        {
            RefreshAll();
        }

        public void AddToCatalog(Item item)
        {
            if (item == null) return;
            catalog[item.id] = item; // Key=ID (evita duplicados y permite acceso O(1))
        }

        // ---------- Sorting ----------
        public void CycleSortField()
        {
            currentSort = (SortField)(((int)currentSort + 1) % Enum.GetValues(typeof(SortField)).Length);
            RefreshStore();
        }

        public void ToggleAscending()
        {
            ascending = !ascending;
            RefreshStore();
        }

        private Item[] SortedCatalog()
        {
            // Pasamos diccionario a array para ordenar sin usar List<T>
            var arr = new Item[catalog.Count];
            int k = 0; foreach (var kv in catalog) arr[k++] = kv.Value;

            Comparison<Item> cmp = BuildComparison(currentSort);
            SelectionSort(arr, cmp, ascending);
            return arr;
        }

        private static int CompareStr(string a, string b)
            => string.Compare(a ?? string.Empty, b ?? string.Empty, StringComparison.Ordinal);

        private Comparison<Item> BuildComparison(SortField field)
        {
            return (a, b) =>
            {
                switch (field)
                {
                    case SortField.ID: return a.id.CompareTo(b.id);
                    case SortField.Name: return CompareStr(a.name, b.name);
                    case SortField.Price: return a.price.CompareTo(b.price);
                    case SortField.Rarity: return a.rarity.CompareTo(b.rarity);
                    case SortField.Type: return a.type.CompareTo(b.type);
                    default: return 0;
                }
            };
        }

        // SelectionSort sobre array (sin List<T>)
        private static void SelectionSort<T>(T[] arr, Comparison<T> cmp, bool ascending)
        {
            int dir = ascending ? 1 : -1;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int best = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (dir * cmp(arr[j], arr[best]) < 0) best = j;
                }
                if (best != i) (arr[i], arr[best]) = (arr[best], arr[i]);
            }
        }

        // ---------- Buy / Sell ----------
        public void Buy(int itemId)
        {
            if (!catalog.TryGetValue(itemId, out var item)) return;
            if (playerMoney < item.price) return;

            playerMoney -= item.price;

            if (!playerInventory.TryGetValue(itemId, out var entry))
                playerInventory[itemId] = new InventoryEntry(item, 1);
            else
                entry.quantity++;

            RefreshMoney();
            RefreshInventory();
        }

        public void Sell(int itemId)
        {
            if (!playerInventory.TryGetValue(itemId, out var entry)) return;
            if (entry.quantity <= 0) return;

            // Regla simple: 50% del precio al vender
            float sellPrice = Mathf.Max(0.01f, entry.item.price * 0.5f);
            playerMoney += sellPrice;

            entry.quantity--;
            if (entry.quantity <= 0) playerInventory.Remove(itemId);

            RefreshMoney();
            RefreshInventory();
        }

        // ---------- UI Refresh ----------
        public void RefreshAll()
        {
            RefreshMoney();
            RefreshStore();
            RefreshInventory();
        }

        private void RefreshMoney()
        {
            if (moneyText != null)
                moneyText.text = $"Money: ${playerMoney:F2}";
        }

        private void ClearGrid(Transform grid)
        {
            if (grid == null) return;
            for (int i = grid.childCount - 1; i >= 0; i--)
                Destroy(grid.GetChild(i).gameObject);
        }

        private void RefreshStore()
        {
            if (sortFieldLabel != null)
                sortFieldLabel.text = $"Sort: {currentSort} {(ascending ? "ASC" : "DESC")}";

            if (storeGrid == null || itemButtonPrefab == null) return;

            ClearGrid(storeGrid);

            var items = SortedCatalog();
            for (int i = 0; i < items.Length; i++)
            {
                var go = Instantiate(itemButtonPrefab, storeGrid); // <-- hijo del Content
                var view = go.GetComponent<ItemButton>();
                view.BindStoreItem(items[i], this);
            }
        }

        private void RefreshInventory()
        {
            if (inventoryGrid == null || itemButtonPrefab == null) return;

            ClearGrid(inventoryGrid);

            // Pasamos inventario (diccionario) a array para listar
            var arr = new InventoryEntry[playerInventory.Count];
            int k = 0; foreach (var kv in playerInventory) arr[k++] = kv.Value;

            for (int i = 0; i < arr.Length; i++)
            {
                var go = Instantiate(itemButtonPrefab, inventoryGrid); // <-- hijo del Content
                var view = go.GetComponent<ItemButton>();
                view.BindInventoryEntry(arr[i], this);
            }
        }
    }
}
