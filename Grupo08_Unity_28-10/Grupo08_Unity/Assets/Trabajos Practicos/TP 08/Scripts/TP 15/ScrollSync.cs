using UnityEngine;

public class ScrollSync : MonoBehaviour
{
    public RectTransform backgroundGrid;
    public RectTransform itemsGrid;

    void LateUpdate()
    {
        if (itemsGrid && backgroundGrid)
            itemsGrid.anchoredPosition = backgroundGrid.anchoredPosition;
    }
}
