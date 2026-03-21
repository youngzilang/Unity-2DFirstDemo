using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int value;

    public List<int> buff;

    public int GetValue()
    {
        return value;
    }

    public void AddBuff(int _buff)
    {
        buff.Add(_buff);
    }

    public void RemoveBuff(int _buff)
    {
        buff.Remove(_buff);
    }

}
