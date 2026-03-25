using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item Data",menuName ="Data/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
}
