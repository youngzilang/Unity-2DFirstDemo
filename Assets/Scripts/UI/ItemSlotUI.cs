using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI itemText;

    public UI uI;
    public InventoryItem item;

    private void Start()
    {
        uI = GetComponentInParent<UI>();
    }

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
        if (item == null) return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.instance.RemoveItem(item.data);
            return;
        }

        if (item.data.itemType == ItemType.Equipment)
        {
            Inventory.instance.Equip(item.data);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null||item.data==null) return;
        uI.tipUI.ShowToolTip(item.data as ItemDataEquipment);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null || item.data == null) return;
        uI.tipUI.HideToolTip();
    }
}
