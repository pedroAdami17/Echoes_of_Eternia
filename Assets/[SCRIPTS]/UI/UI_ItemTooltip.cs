using TMPro;
using UnityEngine;

public class UI_ItemTooltip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemEffect;

    [SerializeField] private int defaultFontSize = 35;

    public void ShowItemToolTip(ItemData_Equipment item)
    {
        if (item == null)
            return;

        itemNameText.text = item.itemName;
        itemTypeText.text = item.equipmentType.ToString();
        itemDescription.text = item.GetDescription();
        itemEffect.text = GetEffectDescriptions(item.itemEffects);

        AdjustFontSize(itemNameText);
        AdjustPosition();

        gameObject.SetActive(true);
    }

    private string GetEffectDescriptions(ItemEffect[] effects)
    {
        if (effects == null || effects.Length == 0)
            return string.Empty;

        System.Text.StringBuilder effectTextBuilder = new System.Text.StringBuilder();

        foreach (var effect in effects)
        {
            if (!string.IsNullOrEmpty(effect.effectDescription))
            {
                effectTextBuilder.AppendLine();
                effectTextBuilder.AppendLine("Unique: " + effect.effectDescription + "</color>");
            }
        }

        return effectTextBuilder.ToString();
    }

    public void HideToolTip()
    {
        itemNameText.fontSize = defaultFontSize;
        gameObject.SetActive(false);

    }
}
