using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipment
}


[CreateAssetMenu(fileName ="Item Data",menuName ="Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;
    [Range(0,100)]
    public int dropChance;

    protected StringBuilder sb = new StringBuilder();

    public virtual string Description()
    {
        return "";
    }
}
