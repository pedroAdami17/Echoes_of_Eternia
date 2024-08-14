using TMPro;
using UnityEngine;

public class UI_SkillToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI skillDescriptionText;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillCost;

    [SerializeField] private float defaultFontSize;

    public void ShowToolTip(string _skillDescription, string _skillName, int _price)
    {
        skillDescriptionText.text = _skillDescription;
        skillNameText.text = _skillName;
        skillCost.text = _price + " Time Fragments";

        AdjustPosition();

        AdjustFontSize(skillNameText);

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        skillNameText.fontSize = defaultFontSize;
        gameObject.SetActive(false);
    }
}
