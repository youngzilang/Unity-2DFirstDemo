using UnityEngine.EventSystems;

public class CraftSlotUI : ItemSlotUI
{
    protected override void Start()
    {
        base.Start();
    }

    public void SetUpCraftSlot(ItemDataEquipment _equip)
    {
        if (!_equip) return;

        item.data = _equip;

        image.sprite = _equip.icon;
        itemText.text = _equip.itemName;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        uI.craftWindow.SetUpCraftWindow(item.data as ItemDataEquipment);
    }
}
