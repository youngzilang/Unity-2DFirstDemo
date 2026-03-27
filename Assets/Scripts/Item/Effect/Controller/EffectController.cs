using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    protected PlayerStat playerStat;

    protected virtual void Start()
    {
        playerStat = PlayerManager.instance.player.GetComponent<PlayerStat>();
    }
}
