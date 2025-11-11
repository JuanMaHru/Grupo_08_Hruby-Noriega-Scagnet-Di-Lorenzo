using UnityEditor;
using UnityEngine;

public static class ItemSOFromPNG
{
    [MenuItem("Tools/Inventory/Create ItemSOs from PNGs No Stackable")]
    public static void CreateItemSOsFromPNGs()
    {
        string iconsPath = "Assets/Sprites/No stackable items Mine";
        string savePath = "Assets/Trabajos Practicos/TP 08/Scripts/TP 15/ScriptableObjects/No Stackable";

        // Crear carpeta si no existe
        if (!AssetDatabase.IsValidFolder(savePath))
            AssetDatabase.CreateFolder("Assets/Trabajos Practicos/TP 08/Scripts/TP 15/ScriptableObjects", "No Stackable");

        // Carga todos los sprites de la carpeta
        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { iconsPath });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            if (sprite == null) continue;

            // Nombre del archivo PNG sin extensión
            string name = System.IO.Path.GetFileNameWithoutExtension(assetPath);

            // Crear ScriptableObject
            ItemSO newItem = ScriptableObject.CreateInstance<ItemSO>();
            newItem.itemName = name;
            newItem.icon = sprite;
            newItem.isStackable = true; // o lo que quieras por default

            // Guardar asset con mismo nombre que PNG
            string finalPath = AssetDatabase.GenerateUniqueAssetPath($"{savePath}/{name}.asset");
            AssetDatabase.CreateAsset(newItem, finalPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("✅ ItemSOs creados a partir de los PNGs!");
    }
}
