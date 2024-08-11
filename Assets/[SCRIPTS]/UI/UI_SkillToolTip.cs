using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillDescriptionText;
    [SerializeField] private TextMeshProUGUI skillNameText;

    public void ShowToolTip(string _skillDescription, string _skillName)
    {
        skillDescriptionText.text = _skillDescription;
        skillNameText.text = _skillName;
        gameObject.SetActive(true);
    }

    public void HideToolTip() => gameObject.SetActive(false);
}
