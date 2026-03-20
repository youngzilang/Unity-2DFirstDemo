using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [SerializeField]private GameObject crystalPrefab;
    [SerializeField] private float crystalCd;
    [SerializeField] private float growSpeed;
    private GameObject currentCtystal;
   

    public override void UseSkill()
    {
        base.UseSkill();

        if (!currentCtystal)
        {
            currentCtystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            currentCtystal.GetComponent<CrystalSkillController>().SetUpCrystal(crystalCd,growSpeed);
        }
        else
        {
            Vector2 originalPosition = player.transform.position;
            CrystalSkillController crystalSkillController = currentCtystal.GetComponent<CrystalSkillController>();
            player.transform.position = currentCtystal.transform.position;
            currentCtystal.transform.position = originalPosition;
            crystalSkillController.Boom();
        }
            
    }



}
