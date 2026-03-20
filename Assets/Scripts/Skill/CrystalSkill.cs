using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkill : Skill
{
    [SerializeField]private GameObject crystalPrefab;
    [SerializeField] private float crystalCd;
    private GameObject currentCtystal;
   

    public override void UseSkill()
    {
        base.UseSkill();

        if (!currentCtystal)
        {
            currentCtystal = Instantiate(crystalPrefab, player.transform.position, Quaternion.identity);
            currentCtystal.GetComponent<CrystalSkillController>().SetUpCrystal(crystalCd);
        }
        else
        {
            player.transform.position = currentCtystal.transform.position;
            Destroy(currentCtystal);
        }
            
    }

}
