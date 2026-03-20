using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{
    private float crystalTimer;

    private void Update()
    {
        crystalTimer -= Time.deltaTime;

        if (crystalTimer < 0)
        {
            selfDestroy();
        }
    }

    public void SetUpCrystal(float _crystalCd)
    {
        crystalTimer = _crystalCd;
    }

    private void selfDestroy() => Destroy(gameObject);
}
