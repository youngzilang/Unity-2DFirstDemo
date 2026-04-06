using System;
using System.Collections.Generic;
[Serializable]
public class GameData
{
    public int currency;
    public SerializableDictionary<string, int> inventory;
    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, bool> checkPoints;
    public List<string> equipmentIds;
    public string closestCheckPointId;
    public int lostCurrencyAmount;
    public float lostCurrencyX;
    public float lostCurrencyY;

    public GameData()
    {
        currency = 0;
        inventory = new SerializableDictionary<string, int>();
        skillTree = new SerializableDictionary<string, bool>();
        checkPoints = new SerializableDictionary<string, bool>();
        equipmentIds = new List<string>();
        closestCheckPointId = string.Empty;
        lostCurrencyAmount = 0;
        lostCurrencyX = 0;
        lostCurrencyY = 0;
    }
}
