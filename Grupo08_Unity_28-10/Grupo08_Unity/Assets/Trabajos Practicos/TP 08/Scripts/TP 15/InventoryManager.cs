using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using TMPro;
#if UNITY_EDITOR // para que suceda todo dentro del editor de Unity y no en la escena
using UnityEditor;
#endif

public class InventoryManager : MonoBehaviour
{
    public Player playerLeft;
    public Player playerRight;

    public GameObject RightInventoryPanel;
    public GameObject LeftInventoryPanel;
    public GameObject SharedInventoryPanel;

    public GameObject itemSlotPrefab;
    public TMP_Text outputText;
    public List<ItemSO> allCreatedItemsSO;

    void Awake()
    {
#if UNITY_EDITOR
        LoadAllItemSOsEditor();
#endif
    }
#if UNITY_EDITOR
    void LoadAllItemSOsEditor()
    {
        allCreatedItemsSO = new List<ItemSO>();
        string[] guids = AssetDatabase.FindAssets("t:ItemSO", new[] { "Assets/Trabajos Practicos/TP 08/Scripts/TP 15/ScriptableObjects" });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemSO item = AssetDatabase.LoadAssetAtPath<ItemSO>(path);
            if (item != null)
                allCreatedItemsSO.Add(item);
        }
    }
#endif

    void Start()
    {
        playerLeft = new Player("Izquierda");
        playerRight = new Player("Derecha");

        CreateItems();

        DisplayInventory(playerLeft.inventory, LeftInventoryPanel);
        DisplayInventory(playerRight.inventory, RightInventoryPanel);
        ShowCounts();
    }

    // ============================
    // CREACIÓN DE ÍTEMS ALEATORIOS
    // ============================
    void CreateItems()
    {
        // Llenamos ambos inventarios con 20 slots cada uno
        FillInventory(playerLeft.inventory);
        FillInventory(playerRight.inventory);
    }

    void FillInventory(Inventory inv)
    {
        int slots = 20;

        for (int i = 0; i < slots; i++)
        {
            // 70% chance de llenar el slot
            if (Random.value <= 0.7f)
            {
                // Elegir un ScriptableObject al azar (puede repetirse)
                var so = allCreatedItemsSO[Random.Range(0, allCreatedItemsSO.Count)];

                // Determinar cantidad random (1-64 si stackeable)
                int quantity = so.isStackable ? Random.Range(1, 65) : 1;

                // Crear un item y agregarlo (puede repetirse tipo “Tierra”)
                Item newItem = new Item(so.itemName, quantity, so.icon);
                inv.AddItem(newItem);
            }
        }
    }

    // ============================
    // MOSTRAR INVENTARIOS
    // ============================
    void PopulateInventory(Inventory inventory, GameObject panel)
    {
        foreach (Transform child in panel.transform)
            Destroy(child.gameObject);

        foreach (var item in inventory.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, panel.transform);

            //  esto evita que Unity escale raro el prefab
            slot.transform.localScale = Vector3.one;


            Image img = slot.GetComponentInChildren<Image>();
            if (img != null)
            {
                img.enabled = true;
                img.sprite = item.icon;
            }

            TMP_Text qtyText = slot.GetComponentInChildren<TMP_Text>();
            if (qtyText != null)
            {
                qtyText.text = item.quantity > 1 ? item.quantity.ToString() : "";
            }
        }
    }

    void DisplayInventory(Inventory inventory, GameObject panel)
    {
        PopulateInventory(inventory, panel);
    }

    // ---------------- BOTONES ----------------

    public void ShowCommonItems()
    {
        ClearSharedPanel();

        var commons = new List<Item>();

        foreach (var itemA in playerLeft.inventory.items)
        {
            foreach (var itemB in playerRight.inventory.items)
            {
                if (itemA.IsSameType(itemB)) // 👈 compara solo por tipo
                {
                    // Evita duplicar el mismo tipo en la lista común
                    if (!commons.Exists(i => i.IsSameType(itemA)))
                        commons.Add(itemA);
                    break;
                }
            }
        }

        // En lugar de DisplayInventory, forzamos manualmente el spawn sin cantidad
        foreach (var item in commons)
        {
            GameObject slot = Instantiate(itemSlotPrefab, SharedInventoryPanel.transform);
            Image img = slot.GetComponent<Image>();
            img.sprite = item.icon;

            TMP_Text qtyText = slot.GetComponentInChildren<TMP_Text>();
            if (qtyText != null)
                qtyText.text = ""; // 👈 no mostrar cantidad en los ítems compartidos
        }
    }

    public void ShowUniqueItems()
    {
        ClearSharedPanel();

        var namesLeft = new HashSet<string>();
        var namesRight = new HashSet<string>();

        foreach (var i in playerLeft.inventory.items)
            namesLeft.Add(i.name);

        foreach (var i in playerRight.inventory.items)
            namesRight.Add(i.name);

        var uniques = new List<Item>();

        // Ítems únicos del jugador izquierdo
        foreach (var i in playerLeft.inventory.items)
            if (!namesRight.Contains(i.name))
                uniques.Add(i);

        // Ítems únicos del jugador derecho
        foreach (var i in playerRight.inventory.items)
            if (!namesLeft.Contains(i.name))
                uniques.Add(i);

        // Spawn manual de ítems únicos (sin cantidad)
        foreach (var item in uniques)
        {
            GameObject slot = Instantiate(itemSlotPrefab, SharedInventoryPanel.transform);

            Image img = slot.GetComponentInChildren<Image>();
            if (img != null)
            {
                img.enabled = true;
                img.sprite = item.icon;
            }

            TMP_Text qtyText = slot.GetComponentInChildren<TMP_Text>();
            if (qtyText != null)
                qtyText.text = "";
        }
    }


    public void ShowItemsNoneHave()
    {
        ClearSharedPanel();

        var missing = new List<Item>();

        foreach (var so in allCreatedItemsSO)
        {
            bool found = false;

            foreach (var itemA in playerLeft.inventory.items)
                if (itemA.name == so.itemName)
                {
                    found = true;
                    break;
                }

            if (!found)
            {
                foreach (var itemB in playerRight.inventory.items)
                    if (itemB.name == so.itemName)
                    {
                        found = true;
                        break;
                    }
            }

            if (!found)
                missing.Add(new Item(so.itemName, 0, so.icon));
        }

        DisplayInventory(new Inventory { items = missing }, SharedInventoryPanel);
    }
    public void ShowCounts()
    {
        int totalA = 0;
        int totalB = 0;

        foreach (var item in playerLeft.inventory.items)
            totalA += item.quantity;

        foreach (var item in playerRight.inventory.items)
            totalB += item.quantity;

        outputText.text = $"Jugador Izquierda: {totalA} ítems | Jugador Derecha: {totalB} ítems";
    }

    void ClearSharedPanel()
    {
        foreach (Transform child in SharedInventoryPanel.transform)
            Destroy(child.gameObject);
    }
}
