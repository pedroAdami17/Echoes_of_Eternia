using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillDescriptionText;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillCost;

    public void ShowToolTip(string _skillDescription, string _skillName, int _price)
    {
        skillDescriptionText.text = _skillDescription;
        skillNameText.text = _skillName;
        //skillCost.text = "Cost: " + _price;


        gameObject.SetActive(true);
    }

    public void HideToolTip() => gameObject.SetActive(false);
}
