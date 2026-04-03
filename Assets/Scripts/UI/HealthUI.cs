using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Entity entity;
    private RectTransform rectTransform;
    private CharaterStats myStat;
    private Slider slider;
    private void Start()
    {
        entity = GetComponentInParent<Entity>();
        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        myStat = GetComponentInParent<CharaterStats>();
        entity.onFlip += FlipUI;
        myStat.onHPChange += UpdateHp;

    }

    private void UpdateHp()
    {
        slider.maxValue = myStat.GetMaxHp();
        slider.value = myStat.currentHP;
    }

    private void FlipUI() => rectTransform.Rotate(0, 180, 0);
    


    private void OnDisable()
    {
        entity.onFlip -= FlipUI;
        myStat.onHPChange -= UpdateHp;
    }

}
