using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private SkillSlotUI[] shouldUnlock;
    [SerializeField] private SkillSlotUI[] shouldLock;

    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;

    [SerializeField] private Color lockColor;

    private Image image;
    private UI uI;
    public bool unlocked;


    private void Start()
    {
        uI = GetComponentInParent<UI>();
        image = GetComponent<Image>();
        image.color = lockColor;

        GetComponent<Button>().onClick.AddListener(() => UnlockSkill());
    }

    private void OnValidate()
    {
        gameObject.name = "SkillSlotUI - " + skillName;
    }

    public void UnlockSkill()
    {
        for(int i = 0; i < shouldUnlock.Length; i++)
        {
            if (!shouldUnlock[i].unlocked)
            {
                Debug.Log("前置技能未解锁完毕!");
                return;
            }
        }

        for (int i = 0; i < shouldLock.Length; i++)
        {
            if (shouldLock[i].unlocked)
            {
                Debug.Log("技能已转向其他分支!");
                return;
            }
        }

        unlocked = true;
        image.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uI.skillTipUI.ShowSkillToolTip(skillDescription, skillName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uI.skillTipUI.HideSkillToolTip();
    }
}
