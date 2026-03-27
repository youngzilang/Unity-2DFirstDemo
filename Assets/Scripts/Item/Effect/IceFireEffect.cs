using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IceFire Effect", menuName = "Data/Item Effect/IceFire")]
public class IceFireEffect : ItemEffect
{
    [SerializeField] private GameObject icefirePrefab;
    [SerializeField] private float xV;

    public override void ExcuteEffect(Transform respondPosition)
    {
        Player player = PlayerManager.instance.player;
        bool isThirdAttack = player.attackState.attackCount == 2;

        if (isThirdAttack)
        {
            GameObject newPrefab = Instantiate(icefirePrefab, respondPosition.position, player.transform.rotation);

            newPrefab.GetComponent<Rigidbody2D>().velocity = new Vector2(xV*player.faceDir,0);

            Destroy(newPrefab, 5f);
        }
        
    }
}
