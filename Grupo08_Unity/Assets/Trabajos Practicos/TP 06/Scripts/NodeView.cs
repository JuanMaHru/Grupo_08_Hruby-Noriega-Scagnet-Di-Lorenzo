using UnityEngine;
using UnityEngine.UI;

namespace TP06.ABB
{
    public class NodeView : MonoBehaviour
    {
        [SerializeField] private Text label; // si usas TMP, cambia a TextMeshProUGUI
        public RectTransform Rect => (RectTransform)transform;

        public void SetValue(string text)
        {
            if (label != null) label.text = text;
        }
    }
}
