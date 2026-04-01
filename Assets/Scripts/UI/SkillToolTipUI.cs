using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillToolTipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillText;
    [SerializeField] private TextMeshProUGUI skillName;

    public void ShowSkillToolTip(string _description,string _name)
    {
        skillText.text = _description;
        skillName.text = _name;
        gameObject.SetActive(true);
    }

    public void HideSkillToolTip() => gameObject.SetActive(false);
}
