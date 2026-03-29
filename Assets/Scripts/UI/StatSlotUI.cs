using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class StatSlotUI : MonoBehaviour
{
    [SerializeField] private buffType type;
    [SerializeField] private string statName;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI nameText;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;

        if(nameText) nameText.text = statName;
    }

    private void Start()
    {
        UpdateStatValue();
    }

    public void UpdateStatValue()
    {
        PlayerStat stat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        if (stat) valueText.text = stat.SelectBuff(type).GetValue().ToString();
    }
}
