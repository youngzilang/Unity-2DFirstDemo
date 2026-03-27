using UnityEngine.EventSystems;

public class CraftSlotUI : ItemSlotUI
{
    private void OnEnable()
    {
        UpdateSlotUI(item);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ItemDataEquipment craft = item.data as ItemDataEquipment;
        Inventory.instance.CraftOrNot(craft,craft.craftMaterial);
    }
}
