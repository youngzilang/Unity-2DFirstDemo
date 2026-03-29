using UnityEngine;


[CreateAssetMenu(fileName = "Buff Effect", menuName = "Data/Item Effect/Buff")]
public class BuffEffect : ItemEffect
{
    [SerializeField] private buffType buff;
    [SerializeField] private int buffAmount;
    [SerializeField] private float buffContinueTime;

    private PlayerStat stat;

    public override void ExcuteEffect(Transform enemyPosition)
    {
         stat = PlayerManager.instance.player.GetComponent<PlayerStat>();
        stat.IncreaseBuff(buffAmount, buffContinueTime,stat.SelectBuff(buff) );

    }

   
}
