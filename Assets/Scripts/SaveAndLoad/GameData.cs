using System;
using System.Collections.Generic;
[Serializable]
public class GameData
{
    public SerializableDictionary<string, int> inventory;
    public SerializableDictionary<string, bool> skillTree;
    public SerializableDictionary<string, bool> checkPoints;
    public SerializableDictionary<string, float> volumeSetting;
    public List<string> equipmentIds;
    public int currency;
    public string closestCheckPointId;
    public int lostCurrencyAmount;
    public float lostCurrencyX;
    public float lostCurrencyY;

    public GameData()
    {
        inventory = new SerializableDictionary<string, int>();
        skillTree = new SerializableDictionary<string, bool>();
        checkPoints = new SerializableDictionary<string, bool>();
        volumeSetting = new SerializableDictionary<string, float>();
        equipmentIds = new List<string>();
        closestCheckPointId = string.Empty;
        currency = 0;
        lostCurrencyAmount = 0;
        lostCurrencyX = 0;
        lostCurrencyY = 0;
    }
}
