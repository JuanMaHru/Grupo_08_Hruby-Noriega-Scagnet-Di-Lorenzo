using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    public HS_AVLTree tree = new HS_AVLTree();

    [Header("UI")]
    public RectTransform contentParent; // Content del ScrollView
    public GameObject scoreRowPrefab;   // Prefab con un TMP_Text adentro
    public int initialCount = 100;

    public event Action OnScoresChanged;

    void Start()
    {
        GenerateInitialScores(initialCount);
        ShowLeaderboardOrder(); // muestra de mayor a menor al inicio
    }

    void GenerateInitialScores(int count)
    {
        var rnd = new System.Random();
        for (int i = 0; i < count; i++)
        {
            int points = rnd.Next(0, 10001); // 0..10000
            string name = RandomName(rnd);
            var s = new Score(points, name);
            tree.Insert(s);
        }
    }

    string RandomName(System.Random rnd)
    {
        string[] starts = { "Ax", "Be", "Ce", "Di", "El", "Fa", "Ga", "Hu", "Io", "Jo", "Ka", "Lu", "Ma", "No", "Oz" };
        string[] ends = { "ron", "lia", "mar", "tin", "s", "dor", "la", "net", "wen", "ski" };
        return starts[rnd.Next(starts.Length)] + ends[rnd.Next(ends.Length)] + rnd.Next(1, 999).ToString();
    }

    public void AddScore(int points, string playerName)
    {
        tree.Insert(new Score(points, playerName));
        OnScoresChanged?.Invoke();
        ShowLeaderboardOrder(); // actualiza visualmente
    }

    //UPDATE DE LA UI
    public void UpdateUI(List<Score> list)
    {
        // Borra filas anteriores
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        // Crea una fila por cada score
        foreach (var score in list)
        {
            GameObject row = Instantiate(scoreRowPrefab, contentParent);
            TMP_Text text = row.GetComponentInChildren<TMP_Text>();
            if (text != null)
                text.text = $"{score.playerName} - {score.points}";
        }
    }



    //METODOS DE ORDEN

    public void ShowPreOrder()
    {
        var list = tree.PreOrder();
        Debug.Log("Showing PreOrder in UI");
        UpdateUI(list);
    }

    public void ShowPostOrder()
    {
        var list = tree.PostOrder();
        Debug.Log("Showing PostOrder in UI");
        UpdateUI(list);
    }

    public void ShowLevelOrder()
    {
        var list = tree.LevelOrder();
        Debug.Log("Showing LevelOrder in UI");
        UpdateUI(list);
    }

    public void ShowInOrder()
    {
        var list = tree.InOrder(); // menor -> mayor
        Debug.Log("Mostrando InOrder (menor a mayor)");
        UpdateUI(list);
    }

    public void ShowLeaderboardOrder()
    {
        var list = tree.InOrder();
        list.Reverse(); // de mayor a menor
        Debug.Log("Mostrando leaderboard original (mayor a menor)");
        UpdateUI(list);
    }
}
