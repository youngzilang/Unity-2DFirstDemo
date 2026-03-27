using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thunder Effect", menuName = "Data/Item Effect/Thunder")]
public class ThunderEffect :ItemEffect
{
    [SerializeField] private GameObject thunderPrefab;

    public override void ExcuteEffect(Transform enemyPosition)
    {
        GameObject newThunder = Instantiate(thunderPrefab, enemyPosition.position,Quaternion.identity);
        Destroy(newThunder, .5f);
    }
}
