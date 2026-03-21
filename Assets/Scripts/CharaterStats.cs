using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterStats : MonoBehaviour
{
    public int damage;
    public int maxHP;

    [SerializeField]private int currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void BeDamaged(int _damage)
    {
        currentHP -= _damage;
        if (currentHP <= 0) Die();
    }

    private void Die()
    {

    }
}
