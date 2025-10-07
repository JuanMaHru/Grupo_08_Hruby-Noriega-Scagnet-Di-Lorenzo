using System.Linq;
using UnityEngine;

namespace TP06.ABB
{
    public class ABBDemo : MonoBehaviour
    {
        [Header("Data")]
        [Tooltip("Si está vacío, se usa el arreglo del enunciado.")]
        public int[] values;

        [Header("Refs")]
        public ABBDrawer drawer;

        private MyABBTree<int> _tree;

        private void Start()
        {
            // 1) Crear el ABB y poblarlo
            _tree = new MyABBTree<int>();
            var input = (values != null && values.Length > 0)
                ? values
                : new int[] { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 }; // enunciado

            foreach (var v in input) _tree.Insert(v);

            // 2) Mostrar info en consola (runtime)
            Debug.Log($"[ABB] Height:      {_tree.GetHeight()}");
            Debug.Log($"[ABB] Balance(root): {_tree.GetBalanceFactor()}");

            var inOrd = string.Join(", ", _tree.InOrder());
            var preOrd = string.Join(", ", _tree.PreOrder());
            var postOrd = string.Join(", ", _tree.PostOrder());
            var lvlOrd = string.Join(", ", _tree.LevelOrder());

            Debug.Log($"[ABB] InOrder:   {inOrd}");
            Debug.Log($"[ABB] PreOrder:  {preOrd}");
            Debug.Log($"[ABB] PostOrder: {postOrd}");
            Debug.Log($"[ABB] LevelOrder:{lvlOrd}");

            // 3) Dibujar en UI
            if (drawer != null) drawer.Draw(_tree);
        }
    }
}
