using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [Header("技能花费")]
    [SerializeField] private int skillPrice;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkill());
    }

    private void Start()
    {
        uI = GetComponentInParent<UI>();
        image = GetComponent<Image>();
        image.color = lockColor;
    }

    private void OnValidate()
    {
        gameObject.name = "SkillSlotUI - " + skillName;
    }

    public void UnlockSkill()
    {
        //防止重复扣钱
        if (unlocked) return;

        if (!PlayerManager.instance.MoneyEnough(skillPrice)) return;

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
        uI.skillTipUI.ShowSkillToolTip(skillDescription, skillName,skillPrice);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uI.skillTipUI.HideSkillToolTip();
    }
}
