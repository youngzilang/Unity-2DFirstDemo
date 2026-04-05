using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int currency;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }
  
    public bool MoneyEnough(int _price)
    {
        if (_price > currency)
        {
            Debug.Log("»õ±Ò²»×ã!");
            return false;
        }

        currency -= _price;
        return true;
    }

    public int GetCurrency() => currency;

    public void LoadData(GameData _data)
    {
        currency = _data.currency;
    }

    public void SaveData(ref GameData _data)
    {
        _data.currency = currency;
    }
}
