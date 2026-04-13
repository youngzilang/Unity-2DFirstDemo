using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSAnimationTrigger : SkeletonAnimationTrigger
{
   private Enemy_BOSS boss => GetComponentInParent<Enemy_BOSS>();

    private void Relocate()=> boss.FindPosition();
    private void MakeInvisible()=> boss.fX.Transprent(true);

        private void MakeVisible()=> boss.fX.Transprent(false);
     
}
