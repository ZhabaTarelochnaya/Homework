using UnityEngine;
using UnityEngine.UI;

public class FixedSizeVerticalLayoutGroup : MonoBehaviour
{
    [SerializeField] int maxSlots = 8;

    RectTransform rectTransform;
    VerticalLayoutGroup layoutGroup;
    
    void OnRectTransformDimensionsChange()
    {
        UpdateChildHeights();
    }

    public void UpdateChildHeights()
    {
        rectTransform = rectTransform ? rectTransform : GetComponent<RectTransform>();
        layoutGroup = layoutGroup ? layoutGroup : GetComponent<VerticalLayoutGroup>();
        float totalSpacing = layoutGroup.spacing * (maxSlots - 1);
        float availableHeight = rectTransform.rect.height - totalSpacing;

        float slotHeight = availableHeight / maxSlots;

        foreach (Transform child in transform)
        {
            var layout = child.GetComponent<LayoutElement>();
            layout.preferredHeight = slotHeight;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }
}
