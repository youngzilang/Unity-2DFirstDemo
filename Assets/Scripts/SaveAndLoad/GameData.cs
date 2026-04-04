using System;
using System.Collections.Generic;
[Serializable]
public class GameData
{
    public int currency;
    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentIds;
    public GameData()
    {
        currency = 0;
        inventory = new SerializableDictionary<string, int>();
        equipmentIds = new List<string>();
    }
}
