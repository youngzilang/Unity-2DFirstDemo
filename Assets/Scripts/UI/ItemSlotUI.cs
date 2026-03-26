using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField]private Image image;
    [SerializeField]private TextMeshProUGUI itemText;

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
}
