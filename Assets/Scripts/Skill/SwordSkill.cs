using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill :Skill
{
    [Header("·Éµ¶Êý¾Ý")]
    [SerializeField]private GameObject sword;
    [SerializeField] private Vector2 launchDirection;
    [SerializeField] private float swordGravity;

    public void CreatSword()
    {
        GameObject newSword = Instantiate(sword,player.transform.position,player.transform.rotation);
        SwordSkillController swordSkillController = newSword.GetComponent<SwordSkillController>();

        swordSkillController.SetUpSword(launchDirection, swordGravity);
    }

}
