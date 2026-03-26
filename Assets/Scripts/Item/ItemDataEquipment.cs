using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,//ÎäÆ÷
    Armor,//¿ø¼×
    Amulet,//»¤·û
    Flask//Ò©Æ¿
}


[CreateAssetMenu(fileName = "Item Data", menuName = "Data/EquipMent")]
public class ItemDataEquipment : ItemData
{
    public EquipmentType equipmentType;
}
