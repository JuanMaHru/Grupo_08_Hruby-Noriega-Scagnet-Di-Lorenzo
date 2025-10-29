using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using Image = UnityEngine.UI.Image;

public class InventoryManager : MonoBehaviour
{
    public Player playerLeft;
    public Player playerRight;
    public GameObject RightInventoryPanel; // Referencia al panel con la grilla (Derecha)
    public GameObject LeftInventoryPanel;  // Referencia al panel con la grilla (Izquierda)
    public GameObject itemSlotPrefab;      // Prefab para los botones del inventario
    public List<Sprite> itemSprites;       // Lista de sprites para los ítems

    void Start()
    {
        playerLeft = new Player("Izquierda");
        playerRight = new Player("Derecha");

        // Crear 40 ítems y asignarlos a los jugadores
        CreateItems();

        // Mostrar el inventario de los jugadores
        DisplayInventory(playerLeft.inventory, LeftInventoryPanel);
        DisplayInventory(playerRight.inventory, RightInventoryPanel);
    }

    void CreateItems()
    {
        // Crear 40 ítems con iconos aleatorios (puedes usar texturas de bloques de Minecraft aquí)
        for (int i = 0; i < 40; i++)
        {
            string itemName = "Item_" + (i + 1);
            float itemPrice = Random.Range(10f, 100f); // Precio aleatorio entre 10 y 100
            Sprite itemIcon = itemSprites[Random.Range(0, itemSprites.Count)];
            Item newItem = new Item(itemName, itemPrice, itemIcon);

            // Alterna entre agregar ítems a los dos jugadores (Izquierda o Derecha)
            if (i % 2 == 0)
                playerLeft.inventory.AddItem(newItem);
            else
                playerRight.inventory.AddItem(newItem);
        }
    }

    // Crear un inventario ficticio para mostrar los ítems
    void PopulateInventory(Inventory inventory, GameObject panel)
    {
        // Limpiar el panel (si es necesario) antes de agregar nuevos elementos
        foreach (Transform child in panel.transform)
        {
            Destroy(child.gameObject); // Limpiar el inventario actual
        }

        // Instanciar los slots de ítems en el panel correspondiente
        foreach (var item in inventory.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, panel.transform);
            slot.GetComponentInChildren<Image>().sprite = item.icon; // Asignar el sprite del ítem
        }
    }

    // Mostrar el inventario del jugador (con su grilla de slots)
    void DisplayInventory(Inventory inventory, GameObject panel)
    {
        // Llamar a PopulateInventory pasando el panel adecuado
        PopulateInventory(inventory, panel);
    }
}
