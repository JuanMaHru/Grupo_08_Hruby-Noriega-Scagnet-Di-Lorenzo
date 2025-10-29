using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class SpaceMapController : MonoBehaviour
{
    [System.Serializable]
    public struct EdgeDef
    {
        public string from;
        public string to;
        public int weight;
    }

    [Header("Config del Grafo")]
    public bool undirected = false;           // si true, duplica aristas A->B y B->A
    public List<string> planets;              // nombres v�lidos de planetas
    public List<EdgeDef> edges;               // aristas definidas por Inspector

    [Header("UI")]
    public TMP_Text routeText;                // "Ruta actual: Terra -> Vega ..."
    public TMP_Text resultText;               // feedback validaci�n / errores

    // (Opcional) Para dibujar gizmos si ten�s transforms de planetas en la escena:
    [Header("Opcional: Posiciones para Gizmos")]
    public List<Transform> planetTransforms;  // cada Transform debe llamarse igual que el planeta

    private MyALGraph<string> graph = new MyALGraph<string>();
    private readonly List<string> currentPath = new List<string>();
    private Dictionary<string, Transform> nameToTransform;

    void Awake()
    {
        // Validaci�n b�sica de nombres
        planets = planets.Distinct().ToList();
        if (planets.Count == 0)
            Debug.LogWarning("No hay planetas cargados en 'planets'.");

        // Construcci�n grafo
        BuildGraphFromInspector();

        // Mapeo para gizmos
        nameToTransform = new Dictionary<string, Transform>();
        foreach (var t in planetTransforms)
        {
            if (t != null)
                nameToTransform[t.name] = t;
        }

        UpdateRouteUI();
        resultText.text = "Seleccion� planetas para construir la ruta.";
    }

    private void BuildGraphFromInspector()
    {
        // Asegurar v�rtices
        foreach (var p in planets)
            graph.AddVertex(p);

        // Aristas
        foreach (var e in edges)
        {
            if (string.IsNullOrWhiteSpace(e.from) || string.IsNullOrWhiteSpace(e.to))
                continue;

            if (!planets.Contains(e.from))
                Debug.LogWarning($"El planeta '{e.from}' no est� listado en 'planets'.");
            if (!planets.Contains(e.to))
                Debug.LogWarning($"El planeta '{e.to}' no est� listado en 'planets'.");

            graph.AddEdge(e.from, (e.to, e.weight));
            if (undirected)
                graph.AddEdge(e.to, (e.from, e.weight));
        }
    }

    public void ClickPlanet(string planetName)
    {
        if (!graph.ContainsVertex(planetName))
        {
            resultText.text = $"El planeta '{planetName}' no existe en el grafo.";
            return;
        }

        currentPath.Add(planetName);
        UpdateRouteUI();
        resultText.text = "OK.";
    }

    public void ClearPath()
    {
        currentPath.Clear();
        UpdateRouteUI();
        resultText.text = "Ruta limpia.";
    }

    public void UndoLast()
    {
        if (currentPath.Count == 0)
        {
            resultText.text = "Nada para deshacer.";
            return;
        }
        currentPath.RemoveAt(currentPath.Count - 1);
        UpdateRouteUI();
        resultText.text = "�ltimo planeta quitado.";
    }

    public void ValidatePath()
    {
        if (currentPath.Count < 2)
        {
            resultText.text = "Seleccion� al menos 2 planetas.";
            return;
        }

        int total = 0;
        for (int i = 0; i < currentPath.Count - 1; i++)
        {
            string from = currentPath[i];
            string to = currentPath[i + 1];

            int? w = graph.GetWeight(from, to);
            if (w == null)
            {
                resultText.text = $"Camino inv�lido: falta arista {from} -> {to}.";
                return;
            }
            total += w.Value;
        }

        resultText.text = $"Camino v�lido. Costo total: {total}.";
    }

    private void UpdateRouteUI()
    {
        routeText.text = currentPath.Count == 0
            ? "Ruta actual: �"
            : $"Ruta actual: {string.Join(" -> ", currentPath)}";
    }

    // --- Gizmos opcionales: dibuja aristas en el Scene View ---
    private void OnDrawGizmos()
    {
        if (planetTransforms == null || planetTransforms.Count == 0) return;

        // Intentar dibujar l�neas entre Transforms cuyos nombres coincidan con edges
        Gizmos.color = Color.white;

        // Dibujo s�lo si hay referencias y est�n mapeadas
        var map = new Dictionary<string, Transform>();
        foreach (var t in planetTransforms)
            if (t != null) map[t.name] = t;

        foreach (var e in edges)
        {
            if (map.TryGetValue(e.from, out var a) && map.TryGetValue(e.to, out var b))
            {
                Gizmos.DrawLine(a.position, b.position);
                // Opcional: dibujar puntito en el medio
                Vector3 mid = (a.position + b.position) * 0.5f;
                Gizmos.DrawSphere(mid, 0.05f);
            }
        }
    }
}
