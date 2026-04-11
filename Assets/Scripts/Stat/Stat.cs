using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int value;

    public List<int> buff;

    // 当值发生增减时，会传入 delta（正表示增加，负表示减少）
    public event Action<int> OnValueChanged;

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

    public void SetDefaultValue(int _value)
    {
        int delta = _value - value;
        value = _value;
        OnValueChanged?.Invoke(delta);
    }

    public void AddModify(int _value)
    {
        value += _value;
        OnValueChanged?.Invoke(_value);
    }

    public void RemoveModify(int _value)
    {
        value -= _value;
        OnValueChanged?.Invoke(-_value);
    }
}
