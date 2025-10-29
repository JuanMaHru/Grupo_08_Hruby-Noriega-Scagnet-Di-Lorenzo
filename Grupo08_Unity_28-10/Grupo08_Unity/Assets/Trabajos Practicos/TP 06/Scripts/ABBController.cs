using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace TP06.ABB
{
    public class ABBController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ABBDrawer drawer;
        [SerializeField] private InputField inputField;     // Ingresá 1 número o varios "1, 2  3;4"
        [SerializeField] private Text heightText;
        [SerializeField] private Text balanceText;
        [SerializeField] private Text inOrderText;
        [SerializeField] private Text preOrderText;
        [SerializeField] private Text postOrderText;
        [SerializeField] private Text levelOrderText;

        // Arreglo del enunciado (botón "Cargar ejemplo")
        private static readonly int[] SampleValues = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };

        private MyABBTree<int> _tree;

        private void Awake()
        {
            _tree = new MyABBTree<int>();   // <-- empieza VACÍO
            Redraw();                       // limpia la vista
        }

        // Llamar desde un botón "Insertar"
        public void InsertFromInput()
        {
            if (_tree == null) _tree = new MyABBTree<int>();

            var ok = false;
            foreach (var token in SplitTokens(inputField != null ? inputField.text : ""))
            {
                if (int.TryParse(token, out int v))
                {
                    _tree.Insert(v);
                    ok = true;
                }
            }
            if (!ok) Debug.LogWarning("[ABBController] No se encontraron enteros válidos en el input.");

            Redraw();
        }

        // Llamar desde un botón "Cargar ejemplo"
        public void LoadSample()
        {
            _tree = new MyABBTree<int>();
            for (int i = 0; i < SampleValues.Length; i++)
                _tree.Insert(SampleValues[i]);     // arreglo del TP
            Redraw();
        }

        // Llamar desde un botón "Limpiar"
        public void ClearTree()
        {
            _tree = new MyABBTree<int>();
            Redraw();
        }

        // (opcional) Insertar N aleatorios
        public void InsertRandom(int count = 8, int min = 0, int max = 99)
        {
            if (_tree == null) _tree = new MyABBTree<int>();
            var rng = new System.Random();
            for (int i = 0; i < count; i++)
                _tree.Insert(rng.Next(min, max + 1));
            Redraw();
        }

        private void Redraw()
        {
            // Actualiza métricas y recorridos en UI
            if (_tree != null)
            {
                if (heightText) heightText.text = $"Height: {_tree.GetHeight()}";
                if (balanceText) balanceText.text = $"Balance(root): {_tree.GetBalanceFactor()}";

                if (inOrderText) inOrderText.text = "InOrder: " + string.Join(", ", _tree.InOrder());
                if (preOrderText) preOrderText.text = "PreOrder: " + string.Join(", ", _tree.PreOrder());
                if (postOrderText) postOrderText.text = "PostOrder: " + string.Join(", ", _tree.PostOrder());
                if (levelOrderText) levelOrderText.text = "LevelOrder: " + string.Join(", ", _tree.LevelOrder());
            }

            // Redibuja el árbol
            if (drawer != null) drawer.Draw(_tree);
        }

        private static string[] SplitTokens(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return Array.Empty<string>();
            // separadores: coma, punto y coma, espacios, tabs y saltos de línea
            return raw.Split(new[] { ',', ';', ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public void DeleteFromInput()
        {
            if (_tree == null) return;

            string raw = inputField ? inputField.text : "";
            bool any = false;

            foreach (var token in SplitTokens(raw))
            {
                if (int.TryParse(token, out int v))
                {
                    bool removed = _tree.RemoveOne(v);
                    any |= removed;
                }
            }

            Redraw();

            // (Opcional) centrar vista en la raíz o en el último insertado que mantengas trackeado
            // if (drawer != null) drawer.CenterOnPosition(Vector2.zero);
        }
    }
}
