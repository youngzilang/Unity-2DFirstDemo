using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public ItemToolTipUI tipUI;
    public StatToolTipUI statTipUI;
    private void Start()
    {
        tipUI = GetComponentInChildren<ItemToolTipUI>(true);
        statTipUI = GetComponentInChildren<StatToolTipUI>(true);
    }
    public void SwitchTo(GameObject menu)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (menu) menu.SetActive(true);
    }
}
