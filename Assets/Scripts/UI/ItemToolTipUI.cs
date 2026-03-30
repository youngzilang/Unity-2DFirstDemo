using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemToolTipUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public void ShowToolTip(ItemDataEquipment item )
    {
        itemName.text = item.itemName;
        itemType.text = item.equipmentType.ToString();
        itemDescription.text = item.Description();
        gameObject.SetActive(true);
    }
    public void HideToolTip() => gameObject.SetActive(false);
}
