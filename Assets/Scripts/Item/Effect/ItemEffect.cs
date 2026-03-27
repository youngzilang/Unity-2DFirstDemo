using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item Effect",menuName ="Data/Item Effect")]
public class ItemEffect : ScriptableObject
{
    public virtual void ExcuteEffect(Transform enemyPosition)
    {
        Debug.Log("buffÒÑµþ¼Ó£¡");
    }
}
