using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("½ÇÉ«µôÂä")]
    [SerializeField] private float lostChance;

    public override void GenerateDropObject()
    {
        Inventory inventory = Inventory.instance;

        List<InventoryItem> currentEquipments = new List<InventoryItem>(inventory.equipment);

        foreach(InventoryItem item in currentEquipments)
        {
            if (Random.Range(0, 100) < lostChance)
            {
                ItemDataEquipment equip = item.data as ItemDataEquipment;
                inventory.UnEquip(equip);
                DropItemObject(item.data);
                CleanEquipSlot(equip.equipmentType);
            }
        }
        inventory.UpdateSlotUI();
    }

    private void CleanEquipSlot(EquipmentType equipmentType)
    {
        EquipmentUI[] slots = Inventory.instance.equipItemSlot;

      for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].slotType == equipmentType) slots[i].CleanUpSlot();
        }
    }
}
