using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : ItemSlotUI
{
    public EquipmentType slotType;

    private void OnValidate()
    {
        gameObject.name = "Equipment Slot -" + slotType.ToString();
    }
}
