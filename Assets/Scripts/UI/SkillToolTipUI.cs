using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillToolTipUI : ToolTipUI
{
    [SerializeField] private TextMeshProUGUI skillText;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillCost;

    public void ShowSkillToolTip(string _description,string _name,int _cost)
    {
        skillText.text = _description;
        skillName.text = _name;
        skillCost.text = "»¨·Ñ: " + _cost;
        AdjustToolTipPosition();
        gameObject.SetActive(true);
    }

    public void HideSkillToolTip() => gameObject.SetActive(false);
}
