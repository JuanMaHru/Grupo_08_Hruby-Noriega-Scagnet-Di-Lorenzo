using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;


namespace TP06.ABB
{
    public class ABBDrawer : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private RectTransform container;  // Un RectTransform vacío bajo el Canvas
        [SerializeField] private NodeView nodePrefab;      // Prefab (Button/Panel) 80x80 aprox
        [SerializeField] private Sprite edgeSprite;        // Cualquier sprite (puede ser Default-UI)
        [SerializeField] private Color edgeColor = new Color(0, 0, 0, 0.5f);
        [SerializeField] private float edgeThickness = 4f;

        [Header("Layout")]
        [SerializeField] private float hSpacing = 130f;
        [SerializeField] private float vSpacing = 120f;

        [SerializeField] private ScrollRect scrollRect;   // Scroll View que contiene 'container'
        [SerializeField] private Vector2 padding = new Vector2(80, 80); // margen interno
        private Dictionary<MyABBNode<int>, NodeView> _viewMap;
        private Dictionary<int, NodeView> _valueMap;

        // Public: dibuja un ABB<int>
        public void Draw(MyABBTree<int> tree)
        {
            ClearContainer();
            _viewMap = new Dictionary<MyABBNode<int>, NodeView>();
            _valueMap = new Dictionary<int, NodeView>();

            if (tree == null || tree.Root == null) return;

            // 1) Calcular posiciones (InOrder X / profundidad Y)
            var posMap = new Dictionary<MyABBNode<int>, Vector2>();
            int xIndex = 0;
            AssignPositions(tree.Root, 0, ref xIndex, posMap);

            // 2) Bounds (min/max) para auto-ajustar el Content
            float minX = posMap.Values.Min(p => p.x);
            float maxX = posMap.Values.Max(p => p.x);
            float minY = posMap.Values.Min(p => p.y);
            float maxY = posMap.Values.Max(p => p.y);

            // Tamaño de Content = rango + padding + tamaño aprox del nodo
            Vector2 nodeApprox = nodePrefab != null ? nodePrefab.Rect.sizeDelta : new Vector2(80, 80);
            float width = (maxX - minX) + padding.x * 2f + nodeApprox.x;
            float height = (maxY - minY) + padding.y * 2f + nodeApprox.y;

            container.sizeDelta = new Vector2(Mathf.Max(width, 600f), Mathf.Max(height, 400f)); // mínimo razonable

            // Offset para “meter” todo en el Content con padding
            Vector2 offset = new Vector2(-minX + padding.x + nodeApprox.x * 0.5f,
                                         -minY + padding.y + nodeApprox.y * 0.5f);

            // 3) Crear vistas
            foreach (var kv in posMap)
            {
                var view = Instantiate(nodePrefab, container);
                view.SetValue(kv.Key.Value.ToString());
                var rt = view.Rect;
                rt.anchoredPosition = kv.Value + offset;

                _viewMap[kv.Key] = view;
                _valueMap[kv.Key.Value] = view; // valores únicos para buscar rápido
            }

            // 4) Aristas
            foreach (var kv in posMap)
            {
                var node = kv.Key;
                if (node.Left != null) CreateEdge(_viewMap[node], _viewMap[node.Left]);
                if (node.Right != null) CreateEdge(_viewMap[node], _viewMap[node.Right]);
            }
        }

        private void AssignPositions(MyABBNode<int> node, int depth, ref int xIndex, Dictionary<MyABBNode<int>, Vector2> map)
        {
            if (node == null) return;
            AssignPositions(node.Left, depth + 1, ref xIndex, map);

            float x = xIndex * hSpacing;
            float y = -depth * vSpacing;
            map[node] = new Vector2(x, y);
            xIndex++;

            AssignPositions(node.Right, depth + 1, ref xIndex, map);
        }

        private void CreateEdge(NodeView a, NodeView b)
        {
            var go = new GameObject("Edge", typeof(Image));
            var img = go.GetComponent<Image>();
            img.sprite = edgeSprite;
            img.color = edgeColor;

            var r = go.GetComponent<RectTransform>();
            r.SetParent(container, false);
            r.anchorMin = r.anchorMax = new Vector2(0.5f, 0.5f);
            r.pivot = new Vector2(0.5f, 0.5f);

            Vector2 pa = a.Rect.anchoredPosition;
            Vector2 pb = b.Rect.anchoredPosition;
            Vector2 d = pb - pa;
            float dist = d.magnitude;
            float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;

            r.sizeDelta = new Vector2(dist, edgeThickness);
            r.anchoredPosition = (pa + pb) * 0.5f;
            r.localRotation = Quaternion.Euler(0, 0, angle);
            r.SetAsFirstSibling(); // líneas detrás de nodos
        }

        private void ClearContainer()
        {
            for (int i = container.childCount - 1; i >= 0; i--)
                Destroy(container.GetChild(i).gameObject);
        }

        public void CenterOnValue(int value)
        {
            if (scrollRect == null) return;
            if (_valueMap != null && _valueMap.TryGetValue(value, out var view))
                CenterOnPosition(view.Rect.anchoredPosition);
        }

        public void CenterOnPosition(Vector2 contentLocalPos)
        {
            if (scrollRect == null || scrollRect.content == null) return;

            var viewport = (RectTransform)scrollRect.viewport;
            var content = scrollRect.content;

            Vector2 vp = viewport.rect.size;
            // Centramos el punto en el medio del viewport
            Vector2 target = -contentLocalPos + vp * 0.5f;
            content.anchoredPosition = target;
        }
    }
}
