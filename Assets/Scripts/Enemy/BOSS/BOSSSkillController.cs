using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSSkillController : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField]private Vector2 checkSize;
    [SerializeField]private LayerMask playerLayer;

    private CharaterStats myStat;


    public void SetUpStat(CharaterStats _stat)=>myStat = _stat; 
    public void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(check.position,checkSize, playerLayer);

        foreach (Collider2D collider in colliders)
        {
            if(collider.GetComponent<Player>() != null)
            {
                collider.GetComponent<Entity>().SetUpKnockBackDir(transform);
                myStat.DoingDamage(collider.GetComponent<CharaterStats>());
                Debug.Log("¹¥»÷µ½Íæ¼Ò");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(check.position, checkSize);
    }

    private void SelfDestory()=>Destroy(gameObject);
}
