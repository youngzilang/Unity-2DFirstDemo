using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatToolTipUI : ToolTipUI
{
    [SerializeField] private TextMeshProUGUI description;

    public void ShowStatToolTip(string text)
    {
        description.text = text;
        AdjustToolTipPosition();
        gameObject.SetActive(true);
    }

    public void HideStatToolTip()
    {
        description.text = "";
        gameObject.SetActive(false);
    }
}
