using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem item;
    public void UpdateSlotUI(InventoryItem _item)
    {
        item = _item;
        image.color = Color.white;

        if (item != null)
        {
            image.sprite = item.data.icon;

            if (item.stackSize > 1) itemText.text = item.stackSize.ToString();
            else itemText.text = "";
        }
    }

    public void CleanUpSlot()
    {
        item = null;
        image.sprite = null;
        image.color = Color.clear;
        itemText.text= "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (image.sprite == null) return;
        if (item.data.itemType == ItemType.Equipment)
        {
            Inventory.instance.Equip(item.data);
        }
    }
}
