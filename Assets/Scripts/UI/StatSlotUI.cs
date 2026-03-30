using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatSlotUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private buffType type;
    [SerializeField] private string statName;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField]private string statDescription;

    private UI uI;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;

        if(nameText) nameText.text = statName;
    }

    private void Start()
    {
        UpdateStatValue();

        uI = GetComponentInParent<UI>();
    }

    public void UpdateStatValue()
    {
        PlayerStat stat = PlayerManager.instance.player.GetComponent<PlayerStat>();

        if (stat)
        {
            valueText.text = stat.SelectBuff(type).GetValue().ToString();

            switch (type)
            {
                case buffType.damage:valueText.text=(stat.SelectBuff(buffType.strength).GetValue()+stat.SelectBuff(buffType.damage).GetValue()).ToString();break;
                case buffType.criticalChance: valueText.text = (stat.SelectBuff(buffType.intelligence).GetValue() + stat.SelectBuff(buffType.criticalChance).GetValue()).ToString(); break;
                case buffType.criticalDamage: valueText.text = (stat.SelectBuff(buffType.strength).GetValue() + stat.SelectBuff(buffType.criticalDamage).GetValue()).ToString(); break;
                case buffType.maxHP: valueText.text = (stat.SelectBuff(buffType.maxHP).GetValue() + stat.SelectBuff(buffType.vatility).GetValue()*5).ToString(); break;
                case buffType.evasion: valueText.text = (stat.SelectBuff(buffType.evasion).GetValue() + stat.SelectBuff(buffType.agility).GetValue()).ToString(); break;
                case buffType.fireDamage: valueText.text = (stat.SelectBuff(buffType.fireDamage).GetValue() + stat.SelectBuff(buffType.intelligence).GetValue()).ToString(); break;
                case buffType.iceDamage: valueText.text = (stat.SelectBuff(buffType.iceDamage).GetValue() + stat.SelectBuff(buffType.intelligence).GetValue()).ToString(); break;
                case buffType.lightDamage: valueText.text = (stat.SelectBuff(buffType.lightDamage).GetValue() + stat.SelectBuff(buffType.intelligence).GetValue()).ToString(); break;
            }



        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uI.statTipUI.ShowStatToolTip(statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uI.statTipUI.HideStatToolTip();
    }
}
