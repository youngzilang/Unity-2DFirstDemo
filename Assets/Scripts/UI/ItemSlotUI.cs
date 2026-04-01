using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image image;
    [SerializeField] protected TextMeshProUGUI itemText;

    protected UI uI;
    public InventoryItem item;

    protected virtual void Start()
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
            uI.tipUI.HideToolTip();
            return;
        }

        if (item.data.itemType == ItemType.Equipment)
        {
            Inventory.instance.Equip(item.data);
        }

        uI.tipUI.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null||item.data==null) return;


        Vector2 mousePosition = Input.mousePosition;

        float offsetX = 0;
        float offsetY = 0;

        if (mousePosition.x > 370) offsetX = -50;


        if (mousePosition.y > 200) offsetY = -30;
        else offsetY = 100;

        uI.tipUI.ShowToolTip(item.data as ItemDataEquipment);

        uI.tipUI.transform.position = new Vector2(mousePosition.x + offsetX, mousePosition.y + offsetY);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null || item.data == null) return;
        uI.tipUI.HideToolTip();
    }
}
